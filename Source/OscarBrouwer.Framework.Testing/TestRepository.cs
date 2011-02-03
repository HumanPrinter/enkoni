//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds a repository that can be used for testing purposes.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

using OscarBrouwer.Framework.Entities;
using OscarBrouwer.Framework.Linq;

namespace OscarBrouwer.Framework.Testing {
  /// <summary>This class implements a repository that is independant of any real-life storagesystem. Instead it uses an
  /// internal list to store and retrieve objects. This makes it possible to use it as a replacement repository in a 
  /// testenvironment.</summary>
  /// <typeparam name="T">The type of entity that is handled by this repository.</typeparam>
  public class TestRepository<T> : Repository<T> where T : class, IEntity<T>, new() {
    #region Static variables
    /// <summary>The storage itself.</summary>
    private static List<T> storage = new List<T>();

    /// <summary>A lock that controls access to the storage.</summary>
    private static ReaderWriterLockSlim storageLock = new ReaderWriterLockSlim();

    /// <summary>Holds the identifier of the entity that was last added to the storage.</summary>
    private static int lastStorageIdentifier = 0;
    #endregion

    #region Instance variables
    /// <summary>The storage that contains the items that are not yet saved.</summary>
    private Dictionary<T, StorageAction> temporaryStorage = new Dictionary<T, StorageAction>();

    /// <summary>A lock that controls access to the temporary storage.</summary>
    private ReaderWriterLockSlim temporaryStorageLock = new ReaderWriterLockSlim();
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestRepository{T}"/> class.</summary>
    public TestRepository() {
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

    #region Repository overrides
    /// <summary>Adds the entity to the storage.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The entity with the updated recordID.</returns>
    protected override T AddEntityCore(T entity, DataSourceInfo dataSourceInfo) {
      try {
        storageLock.EnterWriteLock();
        int newIdentifier = ++lastStorageIdentifier;
        storageLock.ExitWriteLock();

        this.temporaryStorageLock.EnterWriteLock();
        entity.RecordId = newIdentifier;
        this.temporaryStorage.Add(entity, StorageAction.Add);
        this.temporaryStorageLock.ExitWriteLock();
        return entity;
      }
      finally {
        if(storageLock.IsWriteLockHeld) {
          storageLock.ExitWriteLock();
        }

        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Creates a new instance of type T.</summary>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The constructed object.</returns>
    protected override T CreateEntityCore(DataSourceInfo dataSourceInfo) {
      return new T();
    }

    /// <summary>Deletes the entity from the storage.</summary>
    /// <param name="entity">The entity that must be removed.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    protected override void DeleteEntityCore(T entity, DataSourceInfo dataSourceInfo) {
      try {
        /* First search the temporary storage */
        this.temporaryStorageLock.EnterWriteLock();
        if(this.temporaryStorage.Any(kvp => kvp.Key.RecordId == entity.RecordId)) {
          KeyValuePair<T, StorageAction> storedEntry = this.temporaryStorage.Single(item => item.Key.RecordId == entity.RecordId);
          this.temporaryStorage.Remove(storedEntry.Key);
        }
        else {
          /* add it to the global storage with a delete-action */
          this.temporaryStorage.Add(entity, StorageAction.Delete);
        }

        this.temporaryStorageLock.ExitWriteLock();
      }
      finally {
        if(this.temporaryStorageLock.IsWriteLockHeld) {
          this.temporaryStorageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Finds all the entities that match the expression.</summary>
    /// <param name="expression">The search-specification.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The items that match the expression.</returns>
    protected override IEnumerable<T> FindAllCore(Func<T, bool> expression, DataSourceInfo dataSourceInfo) {
      try {
        this.temporaryStorageLock.EnterReadLock();
        storageLock.EnterReadLock();
        return storage.Where(item => expression(item)).Concat(this.temporaryStorage.Where(item => expression(item.Key)).Select(item => item.Key));
      }
      finally {
        storageLock.ExitReadLock();
        this.temporaryStorageLock.ExitReadLock();
      }
    }

    /// <summary>Finds all the entities that where added to the storage.</summary>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>All the items in the storage.</returns>
    protected override IEnumerable<T> FindAllCore(DataSourceInfo dataSourceInfo) {
      try {
        this.temporaryStorageLock.EnterReadLock();
        storageLock.EnterReadLock();
        return storage.Select(item => item).Concat(this.temporaryStorage.Select(item => item.Key));
      }
      finally {
        storageLock.ExitReadLock();
        this.temporaryStorageLock.ExitReadLock();
      }
    }

    /// <summary>Finds the first entity that matches the expression or returns the defaultvalue if there were no matches.
    /// </summary>
    /// <param name="expression">The search-specification.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <param name="defaultValue">The value that must be returned if there were no matches.</param>
    /// <returns>The first result or the defaultvalue.</returns>
    protected override T FindFirstCore(Func<T, bool> expression, DataSourceInfo dataSourceInfo, T defaultValue) {
      try {
        this.temporaryStorageLock.EnterReadLock();
        T result = this.temporaryStorage.FirstOrDefault(item => expression(item.Key)).Key;
        this.temporaryStorageLock.ExitReadLock();
        if(result != null) {
          return result;
        }
        else {
          storageLock.EnterReadLock();
          result = storage.FirstOrDefault(item => expression(item), defaultValue);
          storageLock.ExitReadLock();
          return result;
        }
      }
      finally {
        if(storageLock.IsReadLockHeld) {
          storageLock.ExitReadLock();
        }

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
    protected override T FindSingleCore(Func<T, bool> expression, DataSourceInfo dataSourceInfo, T defaultValue) {
      try {
        this.temporaryStorageLock.EnterReadLock();
        T result = this.temporaryStorage.SingleOrDefault(item => expression(item.Key)).Key;
        this.temporaryStorageLock.ExitReadLock();
        if(result != null) {
          return result;
        }
        else {
          storageLock.EnterReadLock();
          result = storage.SingleOrDefault(item => expression(item), defaultValue);
          storageLock.ExitReadLock();
          return result;
        }
      }
      finally {
        if(storageLock.IsReadLockHeld) {
          storageLock.ExitReadLock();
        }

        if(this.temporaryStorageLock.IsReadLockHeld) {
          this.temporaryStorageLock.ExitReadLock();
        }
      }
    }

    /// <summary>Merges the temporary storage with the global storage.</summary>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    protected override void SaveChangesCore(DataSourceInfo dataSourceInfo) {
      try {
        storageLock.EnterWriteLock();
        this.temporaryStorageLock.EnterWriteLock();
        IEnumerable<T> addedEntities = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Add).Select(kvp => kvp.Key).ToList();
        IEnumerable<T> updatedEntities = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Update).Select(kvp => kvp.Key).ToList();
        IEnumerable<T> deletedEntities = this.temporaryStorage.Where(kvp => kvp.Value == StorageAction.Delete).Select(kvp => kvp.Key).ToList();

        /* Add the added entities to the global storage */
        addedEntities.ForEach(entity => storage.Add(entity));

        /* Update the updated entities in the global storage */
        foreach(T entity in updatedEntities) {
          var storedEntity = storage.Select((item, index) => new { Entity = item, Index = index }).FirstOrDefault(a => a.Entity.RecordId == entity.RecordId);
          if(storedEntity == null) {
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Cannot update entity {0} since it does not exist in the global storage", entity.RecordId));
          }
          else {
            storage[storedEntity.Index] = entity;
          }
        }

        /* Delete the deleted entities from the global storage */
        foreach(T entity in deletedEntities) {
          var storedEntity = storage.Select((item, index) => new { Entity = item, Index = index }).FirstOrDefault(a => a.Entity.RecordId == entity.RecordId);
          if(storedEntity == null) {
            throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, "Cannot delete entity {0} since it does not exist in the global storage", entity.RecordId));
          }
          else {
            storage.RemoveAt(storedEntity.Index);
          }
        }

        this.temporaryStorage.Clear();
      }
      finally {
        storageLock.ExitWriteLock();
        this.temporaryStorageLock.ExitWriteLock();
      }
    }

    /// <summary>Updates an entity in the storage.</summary>
    /// <param name="entity">The enity that must be updated.</param>
    /// <param name="dataSourceInfo">The parameter is not used.</param>
    /// <returns>The updated entity.</returns>
    protected override T UpdateEntityCore(T entity, DataSourceInfo dataSourceInfo) {
      try {
        /* First search the temporary storage */
        this.temporaryStorageLock.EnterWriteLock();
        if(this.temporaryStorage.Any(kvp => kvp.Key.RecordId == entity.RecordId)) {
          KeyValuePair<T, StorageAction> storedEntry = this.temporaryStorage.Single(item => item.Key.RecordId == entity.RecordId);
          if(storedEntry.Value == StorageAction.Delete) {
            /* Add as a new entity */
            throw new InvalidOperationException("Cannot update the entity since is was already deleted.");
          }
          else {
            this.temporaryStorage.Remove(storedEntry.Key);
            this.temporaryStorage.Add(entity, StorageAction.Update);
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
  }
}
