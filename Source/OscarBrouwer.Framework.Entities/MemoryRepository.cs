//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that stores and retrieves entities to and from memory.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using Enkoni.Framework.Linq;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the 
  /// functionality using memorystorage.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class MemoryRepository<TEntity> : Repository<TEntity>
    where TEntity : class, IEntity<TEntity>, new() {
    #region Instance variables
    /// <summary>The storage that contains the items that are not yet saved.</summary>
    private Dictionary<TEntity, StorageAction> temporaryStorage = new Dictionary<TEntity, StorageAction>();

    /// <summary>A lock that controls access to the temporary storage.</summary>
    private ReaderWriterLockSlim temporaryStorageLock = new ReaderWriterLockSlim();

    /// <summary>Indicates if the type of entity implements the ICloneable interface.</summary>
    private bool typeImplementsICloneable;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="MemoryRepository{TEntity}"/> class using the specified
    /// <see cref="DataSourceInfo"/>.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the datasource.</param>
    public MemoryRepository(DataSourceInfo dataSourceInfo)
      : base() {
      this.typeImplementsICloneable = typeof(TEntity).GetInterfaces().Contains(typeof(ICloneable));
      
      this.MemoryStore = MemorySourceInfo.SelectMemoryStore<TEntity>(dataSourceInfo);

      if(this.MemoryStore == null) {
        throw new InvalidOperationException("The memory store is mandatory.");
      }
    }
    #endregion

    #region Private enumerations
    /// <summary>Contains the supported storage actions.</summary>
    private enum StorageAction {
      /// <summary>Indicates that the item is to be added to the global storage.</summary>
      Add,

      /// <summary>Indicates that the item is to be updated in the global storage.</summary>
      Update,

      /// <summary>Indicates that the item is to be deleted from the global storage.</summary>
      Delete
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the DbContext that is used to access the database.</summary>
    protected MemoryStore<TEntity> MemoryStore { get; private set; }
    #endregion

    #region Dispose methods
    /// <summary>Disposes all the managed resources that are held by this instance.</summary>
    protected override void DisposeManagedResources() {
      try {
        this.temporaryStorageLock.Dispose();
      }
      finally {
        base.DisposeManagedResources();
      }
    }
    #endregion

    #region Repository<T> overrides
    /// <summary>Adds the entity to the storage.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The entity with the updated recordID.</returns>
    protected override TEntity AddEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      try {
        this.temporaryStorageLock.EnterWriteLock();

        if(entity.RecordId > 0) {
          if(this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Delete).Select(kvp => kvp.Key).Contains(entity, entityComparer)) {
            LambdaEqualityComparer<KeyValuePair<TEntity, StorageAction>, int> comparer =
              new LambdaEqualityComparer<KeyValuePair<TEntity, StorageAction>, int>(kvp => kvp.Key.RecordId);
            this.temporaryStorage.Remove(new KeyValuePair<TEntity, StorageAction>(entity, StorageAction.Delete), comparer);
            this.temporaryStorage.Add(entity, StorageAction.Update);

            return entity;
          }
        }

        int newRecordId = -1;
        if(this.temporaryStorage.Any(kvp => kvp.Value == StorageAction.Add)) {
          newRecordId = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Add).Min(kvp => kvp.Key.RecordId) - 1;
        }

        entity.RecordId = newRecordId;
        this.temporaryStorage.Add(entity, StorageAction.Add);
        this.temporaryStorageLock.ExitWriteLock();
        return entity;
      }
      finally {
        memoryStore.ExitWriteLock();

        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Creates a new instance of type T.</summary>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The constructed object.</returns>
    protected override TEntity CreateEntityCore(DataSourceInfo dataSourceInfo) {
      return new TEntity();
    }

    /// <summary>Deletes the entity from the storage.</summary>
    /// <param name="entity">The entity that must be removed.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();
      LambdaEqualityComparer<KeyValuePair<TEntity, StorageAction>, int> comparer =
        new LambdaEqualityComparer<KeyValuePair<TEntity, StorageAction>, int>(kvp => kvp.Key.RecordId);

      try {
        /* First search the temporary storage */
        this.temporaryStorageLock.EnterWriteLock();
        if(entity.RecordId < 0) {
          if(this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Add).Select(kvp => kvp.Key).Contains(entity, entityComparer)) {
            this.temporaryStorage.Remove(new KeyValuePair<TEntity, StorageAction>(entity, StorageAction.Add), comparer);
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the memory.");
          }
        }
        else {
          if(this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Update).Select(kvp => kvp.Key).Contains(entity, entityComparer)) {
            this.temporaryStorage.Remove(new KeyValuePair<TEntity, StorageAction>(entity, StorageAction.Update), comparer);
          }

          if(this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Delete).Select(kvp => kvp.Key).Contains(entity, entityComparer)) {
            throw new InvalidOperationException("Cannot delete the same entity more then once.");
          }
          else {
            /* add it to the global storage with a delete-action */
            this.temporaryStorage.Add(entity, StorageAction.Delete);
          }
        }
      }
      finally {
        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Finds all the entities that match the expression.</summary>
    /// <param name="expression">The search-specification.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to 
    /// ignore the ordering.</param>
    /// <param name="maximumResults">The maximum number of results that must be retrieved. Use '-1' to retrieve all results.
    /// </param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The items that match the expression.</returns>
    protected override IEnumerable<TEntity> FindAllCore(Func<TEntity, bool> expression, 
      SortSpecifications<TEntity> sortRules, int maximumResults, DataSourceInfo dataSourceInfo) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        this.temporaryStorageLock.EnterReadLock();
        memoryStore.EnterReadLock();
        IEnumerable<TEntity> results = this.MemoryStore.Storage.Where(expression).OrderBy(sortRules);
        if(this.typeImplementsICloneable) {
          results = results.Select(t => ((ICloneable)t).Clone() as TEntity);
        }

        results = results.Concat(this.temporaryStorage.Where(item => expression(item.Key)).Select(item => item.Key).OrderBy(sortRules));

        if(maximumResults == -1) {
          return results;
        }
        else {
          return results.Take(maximumResults);
        }
      }
      finally {
        memoryStore.ExitReadLock();
        this.temporaryStorageLock.ExitReadLock();
      }
    }

    /// <summary>Finds the first entity that matches the expression or returns the defaultvalue if there were no matches.
    /// </summary>
    /// <param name="expression">The search-specification.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to 
    /// ignore the ordering.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <param name="defaultValue">The value that must be returned if there were no matches.</param>
    /// <returns>The first result or the defaultvalue.</returns>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, SortSpecifications<TEntity> sortRules, 
      DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        this.temporaryStorageLock.EnterReadLock();
        bool resultIsToBeDeleted = false;

        TEntity result = this.temporaryStorage.Where(kvp => kvp.Value != StorageAction.Delete).Select(kvp => kvp.Key)
          .OrderBy(sortRules).FirstOrDefault(expression);
        
        if(result == null) {
          result = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Delete).Select(kvp => kvp.Key)
            .OrderBy(sortRules).FirstOrDefault(expression);
          
          if(result != null) {
            resultIsToBeDeleted = true;
          }
        }

        this.temporaryStorageLock.ExitReadLock();
        if(resultIsToBeDeleted) {
          return null;
        }

        if(result != null) {
          return result;
        }
        else {
          memoryStore.EnterReadLock();
          result = this.MemoryStore.Storage.OrderBy(sortRules).FirstOrDefault(expression, defaultValue);
          memoryStore.ExitReadLock();
          if(result != null && !object.ReferenceEquals(result, defaultValue) && this.typeImplementsICloneable) {
            return ((ICloneable)result).Clone() as TEntity;
          }
          else {
            return result;
          }
        }
      }
      finally {
        memoryStore.ExitReadLock();

        if(this.temporaryStorageLock.IsReadLockHeld) {
          this.temporaryStorageLock.ExitReadLock();
        }
      }
    }

    /// <summary>Finds the single entity that matches the expression or returns the defaultvalue if there were no matches.
    /// </summary>
    /// <param name="expression">The search-specification.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <param name="defaultValue">The value that must be returned if there were no matches.</param>
    /// <returns>The single result or the defaultvalue.</returns>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo, 
      TEntity defaultValue) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        this.temporaryStorageLock.EnterReadLock();
        bool resultIsToBeDeleted = false;

        TEntity result = this.temporaryStorage.Where(kvp => kvp.Value != StorageAction.Delete).FirstOrDefault(item => expression(item.Key)).Key;
        if(result == null) {
          result = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Delete).FirstOrDefault(item => expression(item.Key)).Key;
          if(result != null) {
            resultIsToBeDeleted = true;
          }
        }

        this.temporaryStorageLock.ExitReadLock();
        if(resultIsToBeDeleted) {
          return null;
        }

        if(result != null) {
          return result;
        }
        else {
          memoryStore.EnterReadLock();
          result = this.MemoryStore.Storage.SingleOrDefault(item => expression(item), defaultValue);
          memoryStore.ExitReadLock();
          if(result != null && !object.ReferenceEquals(result, defaultValue) && this.typeImplementsICloneable) {
            return ((ICloneable)result).Clone() as TEntity;
          }
          else {
            return result;
          }
        }
      }
      finally {
        memoryStore.ExitReadLock();

        if(this.temporaryStorageLock.IsReadLockHeld) {
          this.temporaryStorageLock.ExitReadLock();
        }
      }
    }

    /// <summary>Merges the temporary storage with the global storage.</summary>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    protected override void SaveChangesCore(DataSourceInfo dataSourceInfo) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        memoryStore.EnterWriteLock();
        this.temporaryStorageLock.EnterWriteLock();
        IEnumerable<TEntity> addedEntities = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Add).Select(kvp => kvp.Key).ToList();
        IEnumerable<TEntity> updatedEntities = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Update).Select(kvp => kvp.Key).ToList();
        IEnumerable<TEntity> deletedEntities = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Delete).Select(kvp => kvp.Key).ToList();

        /* First, apply new identifiers to the new entities */
        this.ApplyIdentifiers(addedEntities, memoryStore.Storage.DefaultIfEmpty(new TEntity { RecordId = 0 }).Max(t => t.RecordId) + 1);

        /* Add the added entities to the global storage */
        Action<TEntity> addAction;
        if(this.typeImplementsICloneable) {
          addAction = t => this.MemoryStore.Storage.Add(((ICloneable)t).Clone() as TEntity);
        }
        else {
          addAction = t => this.MemoryStore.Storage.Add(t);
        }

        addedEntities.ForEach(entity => addAction(entity));

        /* Update the updated entities in the global storage */
        foreach(TEntity entity in updatedEntities) {
          var storedEntity = this.MemoryStore.Storage.Select((item, index) => new { Entity = item, Index = index }).FirstOrDefault(a => a.Entity.RecordId == entity.RecordId);
          if(storedEntity == null) {
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Cannot update entity {0} since it does not exist in the global storage", entity.RecordId));
          }
          else {
            if(this.typeImplementsICloneable) {
              this.MemoryStore.Storage[storedEntity.Index] = ((ICloneable)entity).Clone() as TEntity;
            }
            else {
              this.MemoryStore.Storage[storedEntity.Index] = entity;
            }
          }
        }

        /* Delete the deleted entities from the global storage */
        foreach(TEntity entity in deletedEntities) {
          var storedEntity = this.MemoryStore.Storage.Select((item, index) => new { Entity = item, Index = index }).FirstOrDefault(a => a.Entity.RecordId == entity.RecordId);
          if(storedEntity == null) {
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Cannot delete entity {0} since it does not exist in the global storage", entity.RecordId));
          }
          else {
            this.MemoryStore.Storage.RemoveAt(storedEntity.Index);
          }
        }

        this.temporaryStorage.Clear();
      }
      finally {
        memoryStore.ExitWriteLock();
        this.temporaryStorageLock.ExitWriteLock();
      }
    }

    /// <summary>Updates an entity in the storage.</summary>
    /// <param name="entity">The enity that must be updated.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The updated entity.</returns>
    protected override TEntity UpdateEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }

      try {
        /* First search the temporary storage */
        this.temporaryStorageLock.EnterWriteLock();

        if(this.temporaryStorage.Any(kvp => kvp.Key.RecordId == entity.RecordId)) {
          KeyValuePair<TEntity, StorageAction> storedEntry = this.temporaryStorage.Single(item => item.Key.RecordId == entity.RecordId);
          if(storedEntry.Value == StorageAction.Delete) {
            /* Add as a new entity */
            throw new InvalidOperationException("Cannot update the entity since is was already deleted.");
          }
          else if(storedEntry.Value == StorageAction.Update) {
            this.temporaryStorage.Remove(storedEntry.Key);
            this.temporaryStorage.Add(entity, StorageAction.Update);
          }
          else if(storedEntry.Value == StorageAction.Add) {
            this.temporaryStorage.Remove(storedEntry.Key);
            this.temporaryStorage.Add(entity, StorageAction.Add);
          }
        }
        else {
          /* add it to the global storage with a delete-action */
          this.temporaryStorage.Add(entity, StorageAction.Update);
        }

        this.temporaryStorageLock.ExitWriteLock();
        return entity;
      }
      finally {
        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }
    #endregion

    #region Protected helper methods
    /// <summary>Applies new identifiers to the entities starting with identifier '1'. More often then not, entities that 
    /// are read from a file do not have any identifiers. Therefore, they are applied here. If the sourcefile already 
    /// specifies identifiers for each record, override this method with an empty implementation to disable this behaviour.
    /// </summary>
    /// <param name="entities">The entities to which the identifiers must be applied.</param>
    protected virtual void ApplyIdentifiers(IEnumerable<TEntity> entities) {
      this.ApplyIdentifiers(entities, 1);
    }

    /// <summary>Applies new identifiers to the entities starting with the specified startvalue. More often then not, 
    /// entities that are read from a file do not have any identifiers. Therefore, they are applied here. If the sourcefile 
    /// already specifies identifiers for each record, override this method with an empty implementation to disable this 
    /// behaviour.</summary>
    /// <param name="entities">The entities to which the identifiers must be applied.</param>
    /// <param name="startIdentifier">The first identifier that must be applied.</param>
    protected virtual void ApplyIdentifiers(IEnumerable<TEntity> entities, int startIdentifier) {
      if(entities == null) {
        throw new ArgumentNullException("entities");
      }

      foreach(TEntity entity in entities) {
        entity.RecordId = startIdentifier++;
      }
    }
    #endregion

    #region Private helper methods
    /// <summary>Selects the MemoryStore that must be used. If the specified DataSourceInfo contains a valid MemoryStore, it 
    /// is used; otherwise the value of the property 'MemoryStore' is used.</summary>
    /// <param name="dataSourceInfo">Any information regarding the datastore that is used as datasource.</param>
    /// <returns>The MemoryStore that must be used.</returns>
    private MemoryStore<TEntity> SelectMemoryStore(DataSourceInfo dataSourceInfo) {
      if(MemorySourceInfo.IsMemoryStoreSpecified(dataSourceInfo)) {
        return MemorySourceInfo.SelectMemoryStore<TEntity>(dataSourceInfo);
      }
      else {
        return this.MemoryStore;
      }
    }
    #endregion
  }
}
