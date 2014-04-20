using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

using Enkoni.Framework.Collections;
using Enkoni.Framework.Linq;

namespace Enkoni.Framework.Entities {
  using System.Timers;

  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the functionality using basic file 
  /// I/O. This implementation can be used a base for any fileformat-specific filerepositories.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public abstract class FileRepository<TEntity> : Repository<TEntity>
    where TEntity : class, IEntity<TEntity>, new() {
    #region Instance variables
    /// <summary>The instance that is used to monitor changes in the sourcefile.</summary>
    private FileSystemWatcher sourceFileMonitor;

    /// <summary>The internal cache that is used to cache the entities from the file.</summary>
    private List<TEntity> internalCache;

    /// <summary>The collection of entities that are to be added to the datasource. </summary>
    private List<TEntity> additionCache;

    /// <summary>The collection of entities that are to be updated in the datasource.</summary>
    private List<TEntity> updateCache;

    /// <summary>The collection of entities that are to be removed from the datasource.</summary>
    private List<TEntity> deletionCache;

    /// <summary>A lock that is used to synchronize access to the internal storage.</summary>
    private ReaderWriterLockSlim storageLock = new ReaderWriterLockSlim();

    /// <summary>The timer that is used to determine if a file-change has finished.</summary>
    private Timer changeEventTimer;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="FileRepository{TEntity}"/> class using the specified <see cref="DataSourceInfo"/>.
    /// </summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the sourcefile.</param>
    /// <exception cref="InvalidOperationException"><paramref name="dataSourceInfo"/> does not specify a valid source file.</exception>
    protected FileRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
      /* Initializes the internal collections */
      this.internalCache = new List<TEntity>();
      this.additionCache = new List<TEntity>();
      this.updateCache = new List<TEntity>();
      this.deletionCache = new List<TEntity>();

      this.ChangeCompleteTimeout = FileSourceInfo.SelectChangeCompleteTimeout(dataSourceInfo);
      this.SourceFileEncoding = FileSourceInfo.SelectSourceFileEncoding(dataSourceInfo);

      /* Determine if the sourcefile has been specified */
      if(FileSourceInfo.IsSourceFileInfoSpecified(dataSourceInfo)) {
        this.SourceFile = FileSourceInfo.SelectSourceFileInfo(dataSourceInfo);
      }

      if(this.SourceFile == null) {
        throw new InvalidOperationException("The source file must be specified when creating a FileRepository.");
      }

      /* If the sourcefile has been specified, create a monitor to watch the sourcefile. */
      this.CreateSourceFileMonitor();

      if(FileSourceInfo.SelectMonitorSourceFile(dataSourceInfo)) {
        this.sourceFileMonitor.EnableRaisingEvents = true;
        this.SourceFileMonitorIsRunning = true;
      }
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the FileInfo that references the used sourcefile.</summary>
    protected FileInfo SourceFile { get; private set; }

    /// <summary>Gets a value indicating whether the filemonitor is currently running.</summary>
    protected bool SourceFileMonitorIsRunning { get; private set; }

    /// <summary>Gets the timeout value that is used to determine if a filechange has finished or not.</summary>
    protected int ChangeCompleteTimeout { get; private set; }

    /// <summary>Gets the encoding of the sourcefile.</summary>
    protected Encoding SourceFileEncoding { get; private set; }

    /// <summary>Gets the entities that are available through the caching mechanism.</summary>
    protected IEnumerable<TEntity> Cache {
      get { return this.internalCache; }
    }
    #endregion

    #region Repository<T> overrides
    /// <summary>Resets the repository by undoing any unsaved changes.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    protected override void ResetCore(DataSourceInfo dataSourceInfo) {
      this.additionCache.Clear();
      this.deletionCache.Clear();
      this.updateCache.Clear();
      
      this.ClearCache();
    }

    /// <summary>Save the changes that were made to the repository. It is possible to supply datasource information that specifies a specific 
    /// destinationfile. In that case, the contents will be written to that file and the internal cache is untouched. Otherwise, the changes are 
    /// written back to the sourcefile and the internal cache will be refresehed.</summary>
    /// <param name="dataSourceInfo">Optional datasourceinformation that may contain a reference to a destinationfile other than the original 
    /// sourcefile.</param>
    protected override void SaveChangesCore(DataSourceInfo dataSourceInfo) {
      bool useGlobalSourceFile = this.UseGlobalSourceFile(dataSourceInfo);

      /* Make sure, no one has access to the internal cache */
      this.storageLock.EnterWriteLock();

      try {
        /* Prepare a copy of the internal cache */
        List<TEntity> tempInternalCache = null;
        if(!useGlobalSourceFile) {
          /* Since the output is different from the global sourcefile, make a copy of the internal cache */
          tempInternalCache = this.internalCache.ToList();
        }

        if(!useGlobalSourceFile || (useGlobalSourceFile && this.internalCache.Count == 0)) {
          /* The internal cache is still empty, read the file first */
          IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SelectSourceFileInfo(dataSourceInfo), dataSourceInfo);
          this.ApplyIdentifiers(readEntities);
          this.RefreshCache(readEntities);
        }

        this.PrepareNewContent(dataSourceInfo);

        /* Build the new contents */
        IEnumerable<TEntity> newContent;
        if(!this.UseGlobalSourceFile(dataSourceInfo)) {
          /* The destination file is different from the sourcefile, so just write the contents and leave it at that. */
          newContent = this.WriteAllRecordsToFile(this.SelectSourceFileInfo(dataSourceInfo), dataSourceInfo, this.internalCache);
          /* Set the cache back to its original contents */
          this.internalCache = tempInternalCache;
        }
        else {
          bool useFileMonitor = this.SourceFileMonitorIsRunning;
          this.PauseSourceFileMonitor();

          /* Otherwise, write the changes back to the sourcefile and refresh the cache. */
          newContent = this.WriteAllRecordsToFile(this.SourceFile, dataSourceInfo, this.internalCache);

          if(useFileMonitor) {
            this.ResumeSourceFileMonitor();
          }

          /* Re-apply the identifiers (since most entities read from file don't have an identifier) */
          this.ApplyIdentifiers(newContent);

          /* Refresh the cache */
          this.RefreshCache(newContent);
          this.additionCache.Clear();
          this.updateCache.Clear();
          this.deletionCache.Clear();
        }
      }
      finally {
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Creates a new entity of type <typeparamref name="TEntity"/>. This is done by calling the default constructor of 
    /// <typeparamref name="TEntity"/>.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The created entity.</returns>
    protected override TEntity CreateEntityCore(DataSourceInfo dataSourceInfo) {
      TEntity entity = new TEntity();
      return entity;
    }

    /// <summary>Finds all the entities of type <typeparamref name="TEntity"/> that match the expression.</summary>
    /// <param name="expression">The expression that is used as a filter.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="maximumResults">The maximum number of results that must be retrieved. Use '-1' to retrieve all results.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// <br/>If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache. Otherwise, the 
    /// concatenated cache is simply returned.</param>
    /// <returns>All the available entities that match the expression.</returns>
    protected override IEnumerable<TEntity> FindAllCore(Func<TEntity, bool> expression, SortSpecifications<TEntity> sortRules, int maximumResults,
      DataSourceInfo dataSourceInfo) {
      bool useGlobalSourceFile = this.UseGlobalSourceFile(dataSourceInfo);

      if(!useGlobalSourceFile) {
        if(this.SelectSourceFileInfo(dataSourceInfo).Exists) {
          IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SelectSourceFileInfo(dataSourceInfo), dataSourceInfo);
          IEnumerable<TEntity> result = readEntities.Where(expression).OrderBy(sortRules);
          if(maximumResults == -1) {
            return result;
          }
          else {
            return result.Take(maximumResults);
          }
        }
        else {
          return Enumerable.Empty<TEntity>();
        }
      }

      this.storageLock.EnterUpgradeableReadLock();
      try {
        if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
          this.storageLock.EnterWriteLock();
          try {
            /* The internal cache is still empty, read the file first */
            IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
            this.ApplyIdentifiers(readEntities);
            this.RefreshCache(readEntities);
          }
          finally {
            this.storageLock.ExitWriteLock();
          }
        }

        /* Find the entities in the in-memory cache */
        IEnumerable<TEntity> result = this.ConcatCache().Where(expression).OrderBy(sortRules);
        if(maximumResults == -1) {
          return result.ToList();
        }
        else {
          return result.Take(maximumResults).ToList();
        }
      }
      finally {
        this.storageLock.ExitUpgradeableReadLock();
      }
    }

    /// <summary>Finds the first entity of type <typeparamref name="TEntity"/> that matches the expression.</summary>
    /// <param name="expression">The expression that is used as a filter.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// <br/>If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache. Otherwise, the 
    /// concatenated cache is simply returned.</param>
    /// <param name="defaultValue">The value that must be returned if the query yielded no results.</param>
    /// <returns>The first entity that matches the expression or the defaultvalue if there were no results.</returns>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, SortSpecifications<TEntity> sortRules, DataSourceInfo dataSourceInfo,
      TEntity defaultValue) {
      bool useGlobalSourceFile = this.UseGlobalSourceFile(dataSourceInfo);

      if(!useGlobalSourceFile) {
        if(this.SelectSourceFileInfo(dataSourceInfo).Exists) {
          IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SelectSourceFileInfo(dataSourceInfo), dataSourceInfo);
          return readEntities.OrderBy(sortRules).FirstOrDefault(expression, defaultValue);
        }
        else {
          return defaultValue;
        }
      }

      this.storageLock.EnterUpgradeableReadLock();
      try {
        if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
          this.storageLock.EnterWriteLock();
          try {
            /* The internal cache is still empty, read the file first */
            IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
            this.ApplyIdentifiers(readEntities);
            this.RefreshCache(readEntities);
          }
          finally {
            this.storageLock.ExitWriteLock();
          }
        }

        /* Find the entities in the in-memory cache */
        return this.ConcatCache().OrderBy(sortRules).FirstOrDefault(expression, defaultValue);
      }
      finally {
        this.storageLock.ExitUpgradeableReadLock();
      }
    }

    /// <summary>Finds the single entity of type <typeparamref name="TEntity"/> that matches the expression.</summary>
    /// <param name="expression">The expression that is used as a filter.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// <br/>If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache. Otherwise, the 
    /// concatenated cache is simply returned.</param>
    /// <param name="defaultValue">The value that must be returned if the query yielded no results.</param>
    /// <returns>The single entity that matches the expression or the defaultvalue if there were no results.</returns>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo,
      TEntity defaultValue) {
      bool useGlobalSourceFile = this.UseGlobalSourceFile(dataSourceInfo);

      if(!useGlobalSourceFile) {
        if(this.SelectSourceFileInfo(dataSourceInfo).Exists) {
          IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SelectSourceFileInfo(dataSourceInfo), dataSourceInfo);
          return readEntities.SingleOrDefault(expression, defaultValue);
        }
        else {
          return defaultValue;
        }
      }

      this.storageLock.EnterUpgradeableReadLock();
      try {
        if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
          this.storageLock.EnterWriteLock();
          try {
            /* The internal cache is still empty, read the file first */
            IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
            this.ApplyIdentifiers(readEntities);
            this.RefreshCache(readEntities);
          }
          finally {
            this.storageLock.ExitWriteLock();
          }
        }

        /* Find the entities in the in-memory cache */
        return this.ConcatCache().SingleOrDefault(expression, defaultValue);
      }
      finally {
        this.storageLock.ExitUpgradeableReadLock();
      }
    }

    /// <summary>Adds a new entity to the repository. It is added to the addition cache untill it is saved using the 
    /// <see cref="Repository{T}.SaveChanges()"/> method. A temporary (negative) RecordID is assigned to the entity. This will be reset when the 
    /// entity is saved.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entity as it was added to the repository.</returns>
    protected override TEntity AddEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      this.storageLock.EnterWriteLock();
      try {
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

        /* The entity is either new or came from another datasource */
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
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Adds a collection of new entities to the repository. They are added to the addition cache untill it is saved using the 
    /// <see cref="Repository{T}.SaveChanges()"/> method. A temporary (negative) RecordID is assigned to the entities. This will be reset when 
    /// the entity is saved.</summary>
    /// <param name="entities">The entities that must be added.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they were added to the repository.</returns>
    protected override IEnumerable<TEntity> AddEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      /* Place the entities in a list to keep track of the entities that have been handled */
      List<TEntity> unhandledEntities = entities.ToList();

      this.storageLock.EnterWriteLock();

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
          /* At least some of the entities are either new or came from another datasource */
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
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Removes an entity from the repository. Depending on the status of the entity, it is removed from the addition-cache or it is added 
    /// to the deletion-cache untill it is saved using the <see cref="Repository{T}.SaveChanges()"/> method.</summary>
    /// <param name="entity">The entity that must be removed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      this.storageLock.EnterWriteLock();
      try {
        if(entity.RecordId < 0) {
          /* If the ID is negative, it should be marked for addition */
          if(this.additionCache.Contains(entity, entityComparer)) {
            this.additionCache.Remove(entity, entityComparer);
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the addition-cache.");
          }
        }
        else {
          /* If the entity was marked for update, remove that mark */
          if(this.updateCache.Contains(entity, entityComparer)) {
            this.updateCache.Remove(entity, entityComparer);
          }

          /* If the entity exists in the original datasource and has not yet been marked for deletion, mark it now */
          if(this.internalCache.Contains(entity, entityComparer)) {
            if(!this.deletionCache.Contains(entity, entityComparer)) {
              this.deletionCache.Add(entity);
            }
            else {
              throw new InvalidOperationException("Cannot delete the same entity more then once.");
            }
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the internal cache.");
          }
        }
      }
      finally {
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Removes a collection of entities from the repository. Depending on the status of each entity, it is removed from the addition-cache 
    /// or it is added to the deletion-cache untill it is saved using the <see cref="Repository{T}.SaveChanges()"/> method.</summary>
    /// <param name="entities">The entities that must be removed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    protected override void DeleteEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      if(entities.Any(e => e.RecordId == 0)) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      this.storageLock.EnterWriteLock();

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
            throw new InvalidOperationException("Could not find the entity in the addition-cache.");
          }
        }

        foreach(TEntity existingEntity in existingEntities) {
          /* If the entity was marked for update, remove that mark */
          if(this.updateCache.Contains(existingEntity, entityComparer)) {
            tempUpdateCache.Remove(existingEntity, entityComparer);
          }

          /* If the entity exists in the original datasource and has not yet been marked for deletion, mark it now */
          if(this.internalCache.Contains(existingEntity, entityComparer)) {
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
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Updates an entity in the repository. Depending on the status of the entity, it is updated in the addition-cache or it is added to 
    /// the update-cache.</summary>
    /// <param name="entity">The entity that contains the updated values.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entity as it was stored in the repository.</returns>
    protected override TEntity UpdateEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }

      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      this.storageLock.EnterWriteLock();
      try {
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
          if(this.internalCache.Contains(entity, entityComparer)) {
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
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Updates a collection of entities in the repository. Depending on the status of each entity, it is updated in the addition-cache or 
    /// it is added to the update-cache.</summary>
    /// <param name="entities">The entities that contain the updated values.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they are stored in the repository.</returns>
    protected override IEnumerable<TEntity> UpdateEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      if(entities.Any(e => e.RecordId == 0)) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }

      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      IEnumerable<TEntity> addedEntities = entities.Where(e => e.RecordId < 0);
      IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);

      this.storageLock.EnterWriteLock();

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
          if(this.internalCache.Contains(existingEntity, entityComparer)) {
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
        this.storageLock.ExitWriteLock();
      }
    }
    #endregion

    #region FileRepository extensibility methods
    /// <summary>Reads all the available records from the sourcefile.</summary>
    /// <param name="sourceFile">Information about the file that must be read.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <returns>The entities that were read from the file.</returns>
    protected abstract IEnumerable<TEntity> ReadAllRecordsFromFile(FileInfo sourceFile, DataSourceInfo dataSourceInfo);

    /// <summary>Writes the specified records to the destination file.</summary>
    /// <param name="destinationFile">Information about the file in which the contents must be saved.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <param name="contents">The new contents of the file.</param>
    /// <returns>The entities after they have been written to the file (in case the saving resulted in some updated values).</returns>
    protected abstract IEnumerable<TEntity> WriteAllRecordsToFile(FileInfo destinationFile, DataSourceInfo dataSourceInfo,
      IEnumerable<TEntity> contents);
    #endregion

    #region Dispose methods
    /// <summary>Disposes all the managed resources that are held by this instance.</summary>
    protected override void DisposeManagedResources() {
      try {
        this.sourceFileMonitor.Dispose();
        this.changeEventTimer.Dispose();
        this.storageLock.Dispose();
      }
      finally {
        base.DisposeManagedResources();
      }
    }
    #endregion

    #region Protected methods
    /// <summary>Determines wheter or not the globally specified source file must be used during an operation or if the specified source information 
    /// specifies a different file.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be examined.</param>
    /// <returns><see langword="true"/> if the global source file must be used, <see langword="false"/> otherwise.</returns>
    protected virtual bool UseGlobalSourceFile(DataSourceInfo dataSourceInfo) {
      return !FileSourceInfo.IsSourceFileInfoSpecified(dataSourceInfo) ||
        FileSourceInfo.SelectSourceFileInfo(dataSourceInfo).FullName == this.SourceFile.FullName;
    }

    /// <summary>Selects the FileInfo that must be used. If the specified DataSourceInfo contains a valid FileInfo, it is used; otherwise the value 
    /// of the property 'SourceFile' is used.</summary>
    /// <param name="dataSourceInfo">Any information regarding the file that is used as datasource.</param>
    /// <returns>The FileInfo that must be used.</returns>
    protected virtual FileInfo SelectSourceFileInfo(DataSourceInfo dataSourceInfo) {
      if(FileSourceInfo.IsSourceFileInfoSpecified(dataSourceInfo)) {
        return FileSourceInfo.SelectSourceFileInfo(dataSourceInfo);
      }
      else {
        return this.SourceFile;
      }
    }

    /// <summary>Reads all the records from the file that match a specific criteria. The default implementation simply reads all the records from the 
    /// file and selects the records that match the criteria. Override this method if a more efficient approach is feasible.</summary>
    /// <param name="sourceFile">The file that must be read.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <param name="expression">The expression that must be used to select the correct records.</param>
    /// <returns>The entities that match the expression or an empty collection if there were no results.</returns>
    protected virtual IEnumerable<TEntity> ReadMultipleRecordsFromFile(FileInfo sourceFile, DataSourceInfo dataSourceInfo,
      Func<TEntity, bool> expression) {
      IEnumerable<TEntity> allEntities = this.ReadAllRecordsFromFile(sourceFile, dataSourceInfo);
      return allEntities.Where(expression);
    }

    /// <summary>Reads a single the records from the file that matches a specific criteria. The default implementation simply reads all the records 
    /// from the file and selects the first record that matches the criteria. Override this 
    /// method if a more efficient approach is feasible.</summary>
    /// <param name="sourceFile">The file that must be read.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <param name="expression">The expression that must be used to select the correct records.</param>
    /// <returns>The entity that matches the expression or <see langword="null"/> if there were no results.</returns>
    protected virtual TEntity ReadSingleRecordFromFile(FileInfo sourceFile, DataSourceInfo dataSourceInfo, Func<TEntity, bool> expression) {
      IEnumerable<TEntity> allEntities = this.ReadAllRecordsFromFile(sourceFile, dataSourceInfo);
      return allEntities.FirstOrDefault(expression, null);
    }

    /// <summary>Applies new identifiers to the entities starting with identifier '1'. More often then not, entities that are read from a file do not 
    /// have any identifiers. Therefore, they are applied here. If the sourcefile already specifies identifiers for each record, override this method 
    /// with an empty implementation to disable this behaviour.</summary>
    /// <param name="entities">The entities to which the identifiers must be applied.</param>
    protected virtual void ApplyIdentifiers(IEnumerable<TEntity> entities) {
      this.ApplyIdentifiers(entities, 1);
    }

    /// <summary>Applies new identifiers to the entities starting with the specified startvalue. More often then not, entities that are read from a 
    /// file do not have any identifiers. Therefore, they are applied here. If the sourcefile already specifies identifiers for each record, override 
    /// this method with an empty implementation to disable this behaviour.</summary>
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

    /// <summary>Performs some actions in response to the sourcefile being deleted. This will normally result in clearing the cache.</summary>
    protected virtual void OnSourceFileDeleted() {
      this.storageLock.EnterWriteLock();
      try {
        this.ClearCache();
      }
      finally {
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Performs some actions in response to the sourcefile being created. This will normally result in filling the cache.</summary>
    protected virtual void OnSourceFileCreated() {
      this.storageLock.EnterWriteLock();
      try {
        IEnumerable<TEntity> fileEntities = this.ReadAllRecordsFromFile(this.SourceFile, null);
        this.RefreshCache(fileEntities);
      }
      finally {
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Performs some actions in response to the sourcefile being changed. This will normally result in refreshing the cache.</summary>
    protected virtual void OnSourceFileChanged() {
      this.storageLock.EnterWriteLock();
      try {
        IEnumerable<TEntity> fileEntities = this.ReadAllRecordsFromFile(this.SourceFile, null);
        this.RefreshCache(fileEntities);
      }
      finally {
        this.storageLock.ExitWriteLock();
      }
    }

    /// <summary>Starts the monitor that monitors the sourcefile. It is save to call this method multiple times.</summary>
    /// <exception cref="InvalidOperationException">If both the filemonitor and sourcefile have not yet been set.</exception>
    protected void StartSourceFileMonitor() {
      if(this.sourceFileMonitor == null && this.SourceFile == null) {
        throw new InvalidOperationException("Cannot start the file monitor if there is no source file specified.");
      }
      else if(this.sourceFileMonitor == null && this.SourceFile != null) {
        this.CreateSourceFileMonitor();
      }

      if(!this.sourceFileMonitor.EnableRaisingEvents) {
        this.sourceFileMonitor.EnableRaisingEvents = true;
        this.SourceFileMonitorIsRunning = true;
      }
    }

    /// <summary>Stops the monitor that monitors the sourcefile. It is save to call this method multiple times.</summary>
    protected void StopSourceFileMonitor() {
      if(this.sourceFileMonitor != null && this.sourceFileMonitor.EnableRaisingEvents) {
        this.sourceFileMonitor.EnableRaisingEvents = false;
        this.SourceFileMonitorIsRunning = false;
      }
    }

    /// <summary>Resumes the monitor that monitors the sourcefile. This method is added as a counterpart for the 
    /// <see cref="PauseSourceFileMonitor()"/> method, however since the filemonitor does not have any specific pause/resume behaviour, calling 
    /// this method is the same as calling <see cref="StartSourceFileMonitor()"/>.</summary>
    protected void ResumeSourceFileMonitor() {
      this.StartSourceFileMonitor();
    }

    /// <summary>Pauses the monitor that monitors the sourcefile. This method is added as a counterpart for the 
    /// <see cref="ResumeSourceFileMonitor()"/> method, however since the filemonitor does not have any specific pause/resume behaviour, calling 
    /// this method is the same as calling <see cref="StopSourceFileMonitor()"/>.</summary>
    protected void PauseSourceFileMonitor() {
      this.StopSourceFileMonitor();
    }

    /// <summary>Clears the internal cache.</summary>
    protected void ClearCache() {
      this.internalCache.Clear();
    }

    /// <summary>Refreshes the internal cache by clearing it and filling it with the specified values.</summary>
    /// <param name="cacheValues">The values that must be placed in the cache.</param>
    protected void RefreshCache(IEnumerable<TEntity> cacheValues) {
      this.internalCache.Clear();
      this.internalCache.AddRange(cacheValues);
    }
    #endregion

    #region Private helper methods
    /// <summary>Create the filemonitor and hooks all the event-handlers.</summary>
    private void CreateSourceFileMonitor() {
      this.sourceFileMonitor = new FileSystemWatcher(this.SourceFile.DirectoryName, this.SourceFile.Name);
      this.sourceFileMonitor.IncludeSubdirectories = false;
      this.sourceFileMonitor.NotifyFilter = NotifyFilters.LastWrite;

      this.sourceFileMonitor.Changed += this.HandleSourceFileChanged;
      this.sourceFileMonitor.Created += this.HandleSourceFileCreated;
      this.sourceFileMonitor.Deleted += this.HandleSourceFileDeleted;
      this.sourceFileMonitor.Renamed += this.HandleSourceFileRenamed;

      this.changeEventTimer = new Timer();
      this.changeEventTimer.AutoReset = false;
      this.changeEventTimer.Elapsed += this.HandleChangeEventTimeout;
    }

    /// <summary>Concatenates the internal caches into one complete and up-to-date cache.</summary>
    /// <returns>The concatenated cache-values.</returns>
    private IEnumerable<TEntity> ConcatCache() {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      /* First, concatenate the basic cache with the added entities... */
      IEnumerable<TEntity> totalCache = this.internalCache;
      if(this.SelectCloneDataSourceItems(null)) {
        totalCache = totalCache.Select(t => ((ICloneable)t).Clone() as TEntity);
      }

      totalCache = totalCache.Concat(this.additionCache);
      /*...then remove the entities that were updated from the basic cache and replace them with the updated versions... */
      totalCache = totalCache.Except(this.updateCache, entityComparer).Concat(this.updateCache);
      /* ...finally, remove the deleted entities */
      totalCache = totalCache.Except(this.deletionCache, entityComparer);

      return totalCache.OrderBy(t => t.RecordId);
    }

    /// <summary>Prepares the new contents for the datasource file by checking the consistency of the caches and merging the caches.</summary>
    /// <param name="dataSourceInfo">Any information regarding the used data source.</param>
    private void PrepareNewContent(DataSourceInfo dataSourceInfo) {
      /* First, check if all the updated entities still exist in the global storage */
      Dictionary<int, TEntity> updatedEntities = new Dictionary<int, TEntity>();
      foreach(TEntity entity in this.updateCache) {
        var storedEntity = this.internalCache.Select((item, index) => new { Entity = item, Index = index })
          .FirstOrDefault(a => a.Entity.RecordId == entity.RecordId);
        if(storedEntity == null) {
          throw new InvalidOperationException(
            string.Format(CultureInfo.CurrentCulture, "Cannot update entity {0} since it does not exist in the data source", entity.RecordId));
        }
        else {
          updatedEntities.Add(storedEntity.Index, entity);
        }
      }

      /* Then, check if all the deleted entities still exist in the global storage */
      Dictionary<int, TEntity> deletedEntities = new Dictionary<int, TEntity>();
      foreach(TEntity entity in this.deletionCache) {
        var storedEntity = this.internalCache.Select((item, index) => new { Entity = item, Index = index })
          .FirstOrDefault(a => a.Entity.RecordId == entity.RecordId);
        if(storedEntity == null) {
          throw new InvalidOperationException(
            string.Format(CultureInfo.CurrentCulture, "Cannot delete entity {0} since it does not exist in the data source", entity.RecordId));
        }
        else {
          deletedEntities.Add(storedEntity.Index, entity);
        }
      }

      /* All the pre-checks have been completed, start the saving */
      /* First, perform the updates */
      foreach(KeyValuePair<int, TEntity> updatedEntity in updatedEntities) {
        if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
          this.internalCache[updatedEntity.Key] = ((ICloneable)updatedEntity.Value).Clone() as TEntity;
        }
        else {
          this.internalCache[updatedEntity.Key] = updatedEntity.Value;
        }
      }

      /* Then perform the deletions */
      int iteration = 0;
      foreach(KeyValuePair<int, TEntity> deletedEntity in deletedEntities.OrderBy(kvp => kvp.Key)) {
        this.internalCache.RemoveAt(deletedEntity.Key - iteration);
        ++iteration;
      }

      /* Then apply new identifiers to the new entities */
      int startId = this.internalCache.DefaultIfEmpty(new TEntity { RecordId = 0 }).Max(t => t.RecordId) + 1;
      this.ApplyIdentifiers(this.additionCache, startId);

      /* Then add the added entities to the cache */
      foreach(TEntity addedEntity in this.additionCache) {
        if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
          this.internalCache.Add(((ICloneable)addedEntity).Clone() as TEntity);
        }
        else {
          this.internalCache.Add(addedEntity);
        }
      }
    }
    #endregion

    #region Private event handlers
    /// <summary>Handles the situation in which the used sourcefile is renamed. If the file is renamed to something other then the original filename, 
    /// it is regarded to be the same as deleting the file, since the file can no longer be found under its original name. Otherwise, the event is 
    /// treated as a created-event.</summary>
    /// <param name="sender">The instance that raised the event.</param>
    /// <param name="args">Some additional information regarding the event.</param>
    private void HandleSourceFileRenamed(object sender, RenamedEventArgs args) {
      if(this.SourceFile.FullName.Equals(args.FullPath, StringComparison.OrdinalIgnoreCase)) {
        this.OnSourceFileCreated();
      }
      else {
        this.OnSourceFileDeleted();
      }
    }

    /// <summary>Handles the situation in which the used sourcefile is deleted.</summary>
    /// <param name="sender">The instance that raised the event.</param>
    /// <param name="args">Some additional information regarding the event.</param>
    private void HandleSourceFileDeleted(object sender, FileSystemEventArgs args) {
      this.OnSourceFileDeleted();
    }

    /// <summary>Handles the situation in which the used sourcefile is created.</summary>
    /// <param name="sender">The instance that raised the event.</param>
    /// <param name="args">Some additional information regarding the event.</param>
    private void HandleSourceFileCreated(object sender, FileSystemEventArgs args) {
      this.OnSourceFileCreated();
    }

    /// <summary>Handles the situation in which the used sourcefile is changed.</summary>
    /// <param name="sender">The instance that raised the event.</param>
    /// <param name="args">Some additional information regarding the event.</param>
    private void HandleSourceFileChanged(object sender, FileSystemEventArgs args) {
      if(args.ChangeType == WatcherChangeTypes.Changed) {
        this.changeEventTimer.Interval = this.ChangeCompleteTimeout;
        this.changeEventTimer.Enabled = true;
      }
    }

    /// <summary>Handles the elapsed event of the change-eventtimer. If the timer elapsed, the assumption is made that the filechange has finished 
    /// and it is safe to call the <see cref="OnSourceFileChanged()"/> method.</summary>
    /// <param name="sender">The instance that raised the event.</param>
    /// <param name="args">Some additional information regarding the event.</param>
    private void HandleChangeEventTimeout(object sender, ElapsedEventArgs args) {
      this.OnSourceFileChanged();
    }
    #endregion
  }
}
