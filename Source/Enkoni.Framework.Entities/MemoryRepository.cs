//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that stores and retrieves entities to and from memory.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading;

using Enkoni.Framework.Collections;
using Enkoni.Framework.Linq;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the functionality using
  /// memorystorage.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class MemoryRepository<TEntity> : Repository<TEntity>
    where TEntity : class, IEntity<TEntity>, new() {
    #region Instance variables
    /// <summary>The collection of entities that are to be added to the datasource. </summary>
    private List<TEntity> additionCache;

    /// <summary>The collection of entities that are to be updated in the datasource.</summary>
    private List<TEntity> updateCache;

    /// <summary>The collection of entities that are to be removed from the datasource.</summary>
    private List<TEntity> deletionCache;

    /// <summary>A lock that controls access to the temporary storage.</summary>
    private ReaderWriterLockSlim temporaryStorageLock = new ReaderWriterLockSlim();
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="MemoryRepository{TEntity}"/> class using the specified <see cref="DataSourceInfo"/>.
    /// </summary>
    /// <param name="dataSourceInfo">The data source information that must be used to access the data source.</param>
    /// <exception cref="InvalidOperationException"><paramref name="dataSourceInfo"/> does not specify a valid <see cref="T:MemoryStore{T}"/>.
    /// </exception>
    public MemoryRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
      /* Initializes the internal collections */
      this.additionCache = new List<TEntity>();
      this.updateCache = new List<TEntity>();
      this.deletionCache = new List<TEntity>();

      this.MemoryStore = MemorySourceInfo.SelectMemoryStore<TEntity>(dataSourceInfo);

      if(this.MemoryStore == null) {
        throw new InvalidOperationException("The memory store is mandatory.");
      }
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the DbContext that is used to access the database.</summary>
    protected MemoryStore<TEntity> MemoryStore { get; private set; }
    #endregion

    #region Repository<T> overrides
    /// <summary>Resets the repository by undoing any unsaved changes.</summary>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    protected override void ResetCore(DataSourceInfo dataSourceInfo) {
      this.additionCache.Clear();
      this.deletionCache.Clear();
      this.updateCache.Clear();
    }

    /// <summary>Merges the temporary storage with the global storage.</summary>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    protected override void SaveChangesCore(DataSourceInfo dataSourceInfo) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        /* Make sure no one has access to the global and local storage */
        memoryStore.EnterWriteLock();
        this.temporaryStorageLock.EnterWriteLock();

        /* First, check if all the updated entities still exist in the global storage */
        Dictionary<int, TEntity> updatedEntities = new Dictionary<int, TEntity>();
        foreach(TEntity entity in this.updateCache) {
          var storedEntity = memoryStore.Storage.Select((item, index) => new { Entity = item, Index = index })
            .FirstOrDefault(a => a.Entity.RecordId == entity.RecordId);
          if(storedEntity == null) {
            throw new InvalidOperationException(
              string.Format(CultureInfo.CurrentCulture, "Cannot update entity {0} since it does not exist in the global storage", entity.RecordId));
          }
          else {
            updatedEntities.Add(storedEntity.Index, entity);
          }
        }

        /* Then, check if all the deleted entities still exist in the global storage */
        Dictionary<int, TEntity> deletedEntities = new Dictionary<int, TEntity>();
        foreach(TEntity entity in this.deletionCache) {
          var storedEntity = memoryStore.Storage.Select((item, index) => new { Entity = item, Index = index })
            .FirstOrDefault(a => a.Entity.RecordId == entity.RecordId);
          if(storedEntity == null) {
            throw new InvalidOperationException(
              string.Format(CultureInfo.CurrentCulture, "Cannot delete entity {0} since it does not exist in the global storage", entity.RecordId));
          }
          else {
            deletedEntities.Add(storedEntity.Index, entity);
          }
        }

        /* All the pre-checks have been completed, start the saving */
        /* First, perform the updates */
        foreach(KeyValuePair<int, TEntity> updatedEntity in updatedEntities) {
          if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
            memoryStore.Storage[updatedEntity.Key] = ((ICloneable)updatedEntity.Value).Clone() as TEntity;
          }
          else {
            memoryStore.Storage[updatedEntity.Key] = updatedEntity.Value;
          }
        }

        /* Then perform the deletions */
        int iteration = 0;
        foreach(KeyValuePair<int, TEntity> deletedEntity in deletedEntities.OrderBy(kvp => kvp.Key)) {
          memoryStore.Storage.RemoveAt(deletedEntity.Key - iteration);
          ++iteration;
        }

        /* Then apply new identifiers to the new entities */
        int startId = memoryStore.Storage.DefaultIfEmpty(new TEntity { RecordId = 0 }).Max(t => t.RecordId) + 1;
        this.ApplyIdentifiers(this.additionCache, startId);

        /* Then add the added entities to the global storage */
        foreach(TEntity addedEntity in this.additionCache) {
          if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
            memoryStore.Storage.Add(((ICloneable)addedEntity).Clone() as TEntity);
          }
          else {
            memoryStore.Storage.Add(addedEntity);
          }
        }

        /* Finally, clear the local storage */
        this.additionCache.Clear();
        this.updateCache.Clear();
        this.deletionCache.Clear();
      }
      finally {
        memoryStore.ExitWriteLock();
        this.temporaryStorageLock.ExitWriteLock();
      }
    }

    /// <summary>Creates a new instance of type T.</summary>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The constructed object.</returns>
    protected override TEntity CreateEntityCore(DataSourceInfo dataSourceInfo) {
      return new TEntity();
    }

    /// <summary>Finds all the entities that match the expression.</summary>
    /// <param name="expression">The search-specification.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="maximumResults">The maximum number of results that must be retrieved. Use '-1' to retrieve all results.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The items that match the expression.</returns>
    protected override IEnumerable<TEntity> FindAllCore(Func<TEntity, bool> expression,
      SortSpecifications<TEntity> sortRules, int maximumResults, DataSourceInfo dataSourceInfo) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        this.temporaryStorageLock.EnterReadLock();
        memoryStore.EnterReadLock();

        IEnumerable<TEntity> results = this.ConcatStorage(dataSourceInfo);
        results = results.Where(item => expression(item)).OrderBy(sortRules);

        if(maximumResults == -1) {
          return results.ToList();
        }
        else {
          return results.Take(maximumResults).ToList();
        }
      }
      finally {
        memoryStore.ExitReadLock();
        this.temporaryStorageLock.ExitReadLock();
      }
    }

    /// <summary>Finds the first entity that matches the expression or returns the defaultvalue if there were no matches.</summary>
    /// <param name="expression">The search-specification.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <param name="defaultValue">The value that must be returned if there were no matches.</param>
    /// <returns>The first result or the defaultvalue.</returns>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, SortSpecifications<TEntity> sortRules, DataSourceInfo dataSourceInfo,
      TEntity defaultValue) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        this.temporaryStorageLock.EnterReadLock();
        memoryStore.EnterReadLock();

        return this.ConcatStorage(dataSourceInfo).OrderBy(sortRules).FirstOrDefault(expression, defaultValue);
      }
      finally {
        memoryStore.ExitReadLock();

        if(this.temporaryStorageLock.IsReadLockHeld) {
          this.temporaryStorageLock.ExitReadLock();
        }
      }
    }

    /// <summary>Finds the single entity that matches the expression or returns the defaultvalue if there were no matches.</summary>
    /// <param name="expression">The search-specification.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <param name="defaultValue">The value that must be returned if there were no matches.</param>
    /// <returns>The single result or the defaultvalue.</returns>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        this.temporaryStorageLock.EnterReadLock();
        memoryStore.EnterReadLock();

        return this.ConcatStorage(dataSourceInfo).SingleOrDefault(expression, defaultValue);
      }
      finally {
        memoryStore.ExitReadLock();

        if(this.temporaryStorageLock.IsReadLockHeld) {
          this.temporaryStorageLock.ExitReadLock();
        }
      }
    }

    /// <summary>Adds the entity to the storage.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The entity with the updated recordID.</returns>
    protected override TEntity AddEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      try {
        this.temporaryStorageLock.EnterWriteLock();

        if(entity.RecordId > 0) {
          /* The entity already has an ID which suggests that it came from the original datasource */
          if(this.deletionCache.Contains(entity, entityComparer)) {
            /* The entity has been marked for deletion, undelete it... */
            this.deletionCache.Remove(entity, entityComparer);
            /* ...and mark it as updated in case any of the fields have been altered. */
            this.updateCache.Add(entity);
            return entity;
          }
        }

        /* The entity is either new or came from another data source */
        /* Determine the new temporary ID for the entity */
        int newRecordId = -1;
        if(this.additionCache.Count > 0) {
          newRecordId = this.additionCache.Min(t => t.RecordId) - 1;
        }

        entity.RecordId = newRecordId;

        /* Add it to the addition cache */
        this.additionCache.Add(entity);

        return entity;
      }
      finally {
        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Adds a collection of new entities to the repository. They are added to the addition cache untill it is saved using the 
    /// <see cref="Repository{T}.SaveChanges()"/> method. A temporary (negative) RecordID is assigned to the entities. This will be reset when the entity is 
    /// saved.</summary>
    /// <param name="entities">The entities that must be added.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they were added to the repository.</returns>
    protected override IEnumerable<TEntity> AddEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      /* Place the entities in a list to keep track of the entities that have been handled */
      List<TEntity> unhandledEntities = entities.ToList();

      this.temporaryStorageLock.EnterWriteLock();

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempDeletionCache = this.deletionCache.ToList();
      List<TEntity> tempUpdateCache = this.updateCache.ToList();
      List<TEntity> tempAdditionCache = this.additionCache.ToList();

      try {
        if(entities.Any(e => e.RecordId > 0)) {
          IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);
          ReferenceEqualityComparer<TEntity> referenceComparer = new ReferenceEqualityComparer<TEntity>();
          /* At least some of the entities already have an ID which suggests that they came from the original datasource */
          foreach(TEntity existingEntity in existingEntities) {
            if(tempDeletionCache.Contains(existingEntity, entityComparer)) {
              /* The entity has been marked for deletion, undelete it... */
              tempDeletionCache.Remove(existingEntity, entityComparer);
              /* ...and mark it as updated in case any of the fields have been altered. */
              tempUpdateCache.Add(existingEntity);

              bool removeResult = unhandledEntities.Remove(existingEntity, referenceComparer);
              Debug.Assert(removeResult, "Somehow the result could not be removed from the collection of handled entities.");
            }
          }
        }

        if(unhandledEntities.Count > 0) {
          /* At least some of the entities are either new or came from another data source */
          /* Determine the new temporary ID for the entities */
          int newRecordId = -1;
          if(tempAdditionCache.Count > 0) {
            newRecordId = tempAdditionCache.Min(t => t.RecordId) - 1;
          }

          foreach(TEntity unhandledEntity in unhandledEntities) {
            unhandledEntity.RecordId = newRecordId;
            --newRecordId;
            /* Add it to the addition cache */
            tempAdditionCache.Add(unhandledEntity);
          }
        }

        /* Replace the original caches to complete the 'transaction' */
        this.deletionCache = tempDeletionCache;
        this.updateCache = tempUpdateCache;
        this.additionCache = tempAdditionCache;

        return entities;
      }
      finally {
        this.temporaryStorageLock.ExitWriteLock();
      }
    }

    /// <summary>Deletes the entity from the storage.</summary>
    /// <param name="entity">The entity that must be removed.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      IEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        /* First search the temporary storage */
        this.temporaryStorageLock.EnterWriteLock();
        memoryStore.EnterReadLock();

        if(entity.RecordId < 0) {
          /* If the ID is negative, it should be marked for addition */
          if(this.additionCache.Contains(entity, entityComparer)) {
            this.additionCache.Remove(entity, entityComparer);
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the memory.");
          }
        }
        else {
          /* If the entity was marked for update, remove that mark */
          if(this.updateCache.Contains(entity, entityComparer)) {
            this.updateCache.Remove(entity, entityComparer);
          }

          /* If the entity exists in the original data source and has not yet been marked for deletion, mark it now */
          if(memoryStore.Storage.Contains(entity, entityComparer)) {
            if(!this.deletionCache.Contains(entity, entityComparer)) {
              this.deletionCache.Add(entity);
            }
            else {
              throw new InvalidOperationException("Cannot delete the same entity more then once.");
            }
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the memory.");
          }
        }
      }
      finally {
        memoryStore.ExitReadLock();

        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Removes a collection of entities from the repository. Depending on the status of each entity, it is removed from the addition-cache 
    /// or it is added to the deletion-cache untill it is saved using the <see cref="Repository{T}.SaveChanges()"/> method.</summary>
    /// <param name="entities">The entities that must be removed.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    protected override void DeleteEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      if(entities.Any(e => e.RecordId == 0)) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      IEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      this.temporaryStorageLock.EnterWriteLock();
      memoryStore.EnterReadLock();

      IEnumerable<TEntity> addedEntities = entities.Where(e => e.RecordId < 0);
      IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempAdditionCache = this.additionCache.ToList();
      List<TEntity> tempUpdateCache = this.updateCache.ToList();
      List<TEntity> tempDeletionCache = this.deletionCache.ToList();

      try {
        foreach(TEntity addedEntity in addedEntities) {
          /* If the ID is negative, it should be marked for addition */
          if(tempAdditionCache.Contains(addedEntity, entityComparer)) {
            tempAdditionCache.Remove(addedEntity, entityComparer);
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the memory.");
          }
        }

        foreach(TEntity existingEntity in existingEntities) {
          /* If the entity was marked for update, remove that mark */
          if(tempUpdateCache.Contains(existingEntity, entityComparer)) {
            tempUpdateCache.Remove(existingEntity, entityComparer);
          }

          /* If the entity exists in the original data source and has not yet been marked for deletion, mark it now */
          if(memoryStore.Storage.Contains(existingEntity, entityComparer)) {
            if(!tempDeletionCache.Contains(existingEntity, entityComparer)) {
              tempDeletionCache.Add(existingEntity);
            }
            else {
              throw new InvalidOperationException("Cannot delete the same entity more then once.");
            }
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the internal cache.");
          }
        }

        /* Replace the original caches to complete the 'transaction' */
        this.additionCache = tempAdditionCache;
        this.updateCache = tempUpdateCache;
        this.deletionCache = tempDeletionCache;
      }
      finally {
        memoryStore.ExitReadLock();
        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
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

      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      try {
        /* First search the temporary storage */
        this.temporaryStorageLock.EnterWriteLock();
        memoryStore.EnterReadLock();

        if(entity.RecordId < 0) {
          if(this.additionCache.Contains(entity, entityComparer)) {
            int indexOfEntity = this.additionCache.IndexOf(entity, entityComparer);
            this.additionCache[indexOfEntity] = entity;
            return entity;
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the addition-cache.");
          }
        }
        else {
          if(memoryStore.Storage.Contains(entity, entityComparer)) {
            if(this.deletionCache.Contains(entity, entityComparer)) {
              throw new InvalidOperationException("Cannot update the entity since it already marked for deletion.");
            }

            if(this.updateCache.Contains(entity, entityComparer)) {
              int indexOfEntity = this.updateCache.IndexOf(entity, entityComparer);
              this.updateCache[indexOfEntity] = entity;
            }
            else {
              this.updateCache.Add(entity);
            }

            return entity;
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the internal cache.");
          }
        }
      }
      finally {
        memoryStore.ExitReadLock();
        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Updates a collection of entities in the repository. Depending on the status of each entity, it is updated in the addition-cache or 
    /// it is added to the update-cache.</summary>
    /// <param name="entities">The entities that contain the updated values.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they are stored in the repository.</returns>
    protected override IEnumerable<TEntity> UpdateEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      if(entities.Any(e => e.RecordId == 0)) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }

      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();
      MemoryStore<TEntity> memoryStore = this.SelectMemoryStore(dataSourceInfo);

      IEnumerable<TEntity> addedEntities = entities.Where(e => e.RecordId < 0);
      IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);

      this.temporaryStorageLock.EnterWriteLock();
      memoryStore.EnterReadLock();

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempAdditionCache = this.additionCache.ToList();
      List<TEntity> tempUpdateCache = this.updateCache.ToList();
      List<TEntity> tempDeletionCache = this.deletionCache.ToList();

      try {
        foreach(TEntity addedEntity in addedEntities) {
          /* If the entity is marked for addition, update the entity in the addition cache */
          if(tempAdditionCache.Contains(addedEntity, entityComparer)) {
            int indexOfEntity = tempAdditionCache.IndexOf(addedEntity, entityComparer);
            tempAdditionCache[indexOfEntity] = addedEntity;
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the addition-cache.");
          }
        }

        foreach(TEntity existingEntity in existingEntities) {
          if(memoryStore.Storage.Contains(existingEntity, entityComparer)) {
            /* If the entity is already marked for deletion, it can no longer be updated. */
            if(tempDeletionCache.Contains(existingEntity, entityComparer)) {
              throw new InvalidOperationException("Cannot update the entity since it already marked for deletion.");
            }

            if(tempUpdateCache.Contains(existingEntity, entityComparer)) {
              /* If the entity was already marked for update, replace it in the update cache */
              int indexOfEntity = tempUpdateCache.IndexOf(existingEntity, entityComparer);
              tempUpdateCache[indexOfEntity] = existingEntity;
            }
            else {
              /* otherwise, simply add it to the update cache */
              tempUpdateCache.Add(existingEntity);
            }
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the internal cache.");
          }
        }

        /* Replace the original caches to complete the 'transaction' */
        this.additionCache = tempAdditionCache;
        this.updateCache = tempUpdateCache;
        this.deletionCache = tempDeletionCache;

        return entities;
      }
      finally {
        memoryStore.ExitReadLock();
        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }
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

    #region Protected helper methods
    /// <summary>Applies new identifiers to the entities starting with identifier '1'.</summary>
    /// <param name="entities">The entities to which the identifiers must be applied.</param>
    protected virtual void ApplyIdentifiers(IEnumerable<TEntity> entities) {
      this.ApplyIdentifiers(entities, 1);
    }

    /// <summary>Applies new identifiers to the entities starting with the specified startvalue.</summary>
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
    /// <summary>Selects the MemoryStore that must be used. If the specified DataSourceInfo contains a valid MemoryStore, it is used; otherwise the 
    /// value of the property 'MemoryStore' is used.</summary>
    /// <param name="dataSourceInfo">Any information regarding the datastore that is used as data source.</param>
    /// <returns>The MemoryStore that must be used.</returns>
    private MemoryStore<TEntity> SelectMemoryStore(DataSourceInfo dataSourceInfo) {
      if(MemorySourceInfo.IsMemoryStoreSpecified(dataSourceInfo)) {
        return MemorySourceInfo.SelectMemoryStore<TEntity>(dataSourceInfo);
      }
      else {
        return this.MemoryStore;
      }
    }

    /// <summary>Concatenates the global caches and local cache into one complete and up-to-date cache.</summary>
    /// <param name="dataSourceInfo">Any information regarding the datastore that is used as data source.</param>
    /// <returns>The concatenated cache-values.</returns>
    private IEnumerable<TEntity> ConcatStorage(DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      IEnumerable<TEntity> totalCache = this.SelectMemoryStore(dataSourceInfo).Storage;
      if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
        totalCache = totalCache.Select(t => ((ICloneable)t).Clone() as TEntity);
      }

      totalCache = totalCache.Concat(this.additionCache);
      /*...then remove the entities that were updated from the basic cache and replace them with the updated versions... */
      totalCache = totalCache.Except(this.updateCache, entityComparer).Concat(this.updateCache);
      /* ...finally, remove the deleted entities */
      totalCache = totalCache.Except(this.deletionCache, entityComparer);

      return totalCache.OrderBy(t => t.RecordId);
    }
    #endregion
  }
}
