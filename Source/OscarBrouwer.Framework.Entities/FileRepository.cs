//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="FileRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that uses a file as datasource.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

using OscarBrouwer.Framework.Linq;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the 
  /// functionality using basic file I/O. This implementation can be used a base for any fileformat-specific 
  /// filerepositories.</summary>
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

    /// <summary>A lock that is used to synchronize access to the internal cache.</summary>
    private object internalCacheLock = new object();

    /// <summary>A lock that is used to synchronize access to the addition-cache.</summary>
    private object additionCacheLock = new object();

    /// <summary>A lock that is used to synchronize access to the update-cache.</summary>
    private object updateCacheLock = new object();

    /// <summary>A lock that is used to synchronize access to the deletion-cache.</summary>
    private object deletionCacheLock = new object();
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="FileRepository{TEntity}"/> class using the specified
    /// <see cref="DataSourceInfo"/>.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the sourcefile.</param>
    protected FileRepository(DataSourceInfo dataSourceInfo)
      : base() {
      /* Initializes the internal collections */
      this.internalCache = new List<TEntity>();
      this.additionCache = new List<TEntity>();
      this.updateCache = new List<TEntity>();
      this.deletionCache = new List<TEntity>();

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

    /// <summary>Gets the entities that are available through the caching mechanism.</summary>
    protected IEnumerable<TEntity> Cache {
      get { return this.internalCache; }
    }
    #endregion

    #region Repository<T> overrides
    /// <summary>Save the changes that were made to the repository. It is possible to supply datasource information that 
    /// specifies a specific destinationfile. In that case, the contents will be written to that file and the internal 
    /// cache is untouched. Otherwise, the changes are written back to the sourcefile and the internal cache will be 
    /// refresehed.</summary>
    /// <param name="dataSourceInfo">Optional datasourceinformation that may contain a reference to a destinationfile other
    /// than the original sourcefile.</param>
    protected override void SaveChangesCore(DataSourceInfo dataSourceInfo) {
      /* First, apply new identifiers to the new entities */
      this.ApplyIdentifiers(this.additionCache, this.internalCache.Max(t => t.RecordId) + 1);

      /* Build the new contents */
      IEnumerable<TEntity> newContent = this.ConcatCache();
      if(FileSourceInfo.IsSourceFileInfoSpecified(dataSourceInfo) &&
        FileSourceInfo.SelectSourceFileInfo(dataSourceInfo).FullName != this.SourceFile.FullName) {
        /* The destination file is different from the sourcefile, so just write the contents and leave it at that. */
        newContent = this.WriteAllRecordsToFile(FileSourceInfo.SelectSourceFileInfo(dataSourceInfo), dataSourceInfo, newContent);
      }
      else {
        /* Otherwise, write the changes back to the sourcefile and refresh the cache. */
        newContent = this.WriteAllRecordsToFile(this.SourceFile, dataSourceInfo, newContent);

        /* Re-apply the identifiers (since most entities read from file don't have an identifier) */
        this.ApplyIdentifiers(newContent);

        /* Refresh the cache */
        this.RefreshCache(newContent);
        this.additionCache.Clear();
        this.updateCache.Clear();
        this.deletionCache.Clear();
      }
    }

    /// <summary>Creates a new entity of type <typeparamref name="TEntity"/>. This is done by calling the default
    /// constructor of <typeparamref name="TEntity"/>.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This 
    /// parameter is not used.</param>
    /// <returns>The created entity.</returns>
    protected override TEntity CreateEntityCore(DataSourceInfo dataSourceInfo) {
      TEntity entity = new TEntity();
      return entity;
    }

    /// <summary>Creates an expression that can be used to perform a 'Like' operation.</summary>
    /// <returns>The created expression.</returns>
    protected override Func<string, string, bool> CreateLikeExpressionCore() {
      Func<string, string, bool> expression = (field, pattern) => Regex.IsMatch(field, pattern, RegexOptions.None);
      return expression;
    }

    /// <summary>Finds all the entities of type <typeparamref name="TEntity"/>.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.<br/>
    /// If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache.
    /// Otherwise, the concatenated cache is simply returned.</param>
    /// <returns>All the available entities.</returns>
    protected override IEnumerable<TEntity> FindAllCore(DataSourceInfo dataSourceInfo) {
      if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
        IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
        this.ApplyIdentifiers(readEntities);
        this.RefreshCache(readEntities);
      }

      return this.ConcatCache();
    }

    /// <summary>Finds all the entities of type <typeparamref name="TEntity"/> that match the expression.</summary>
    /// <param name="expression">The expression that is used as a filter.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.<br/>
    /// If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache.
    /// Otherwise, the concatenated cache is simply returned.</param>
    /// <returns>All the available entities that match the expression.</returns>
    protected override IEnumerable<TEntity> FindAllCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo) {
      if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
        IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
        this.ApplyIdentifiers(readEntities);
        this.RefreshCache(readEntities);
      }

      return this.ConcatCache().Where(expression);
    }

    /// <summary>Finds the first entity of type <typeparamref name="TEntity"/> that matches the expression.</summary>
    /// <param name="expression">The expression that is used as a filter.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.<br/>
    /// If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache.
    /// Otherwise, the concatenated cache is simply returned.</param>
    /// <returns>The first entity that matches the expression or <see langword="null"/> if there were no results.</returns>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo) {
      if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
        IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
        this.ApplyIdentifiers(readEntities);
        this.RefreshCache(readEntities);
      }

      return this.ConcatCache().FirstOrDefault(expression, null);
    }

    /// <summary>Finds the first entity of type <typeparamref name="TEntity"/> that matches the expression.</summary>
    /// <param name="expression">The expression that is used as a filter.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.<br/>
    /// If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache.
    /// Otherwise, the concatenated cache is simply returned.</param>
    /// <param name="defaultValue">The value that must be returned if the query yielded no results.</param>
    /// <returns>The first entity that matches the expression or the defaultvalue if there were no results.</returns>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
        IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
        this.ApplyIdentifiers(readEntities);
        this.RefreshCache(readEntities);
      }

      return this.ConcatCache().FirstOrDefault(expression, defaultValue);
    }

    /// <summary>Finds the single entity of type <typeparamref name="TEntity"/> that matches the expression.</summary>
    /// <param name="expression">The expression that is used as a filter.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.<br/>
    /// If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache.
    /// Otherwise, the concatenated cache is simply returned.</param>
    /// <returns>The single entity that matches the expression or <see langword="null"/> if there were no results.</returns>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo) {
      if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
        IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
        this.ApplyIdentifiers(readEntities);
        this.RefreshCache(readEntities);
      }

      return this.ConcatCache().SingleOrDefault(expression, null);
    }

    /// <summary>Finds the single entity of type <typeparamref name="TEntity"/> that matches the expression.</summary>
    /// <param name="expression">The expression that is used as a filter.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.<br/>
    /// If the cache is empty but the sourcefile exists, the file is read first and its contents are placed in the cache.
    /// Otherwise, the concatenated cache is simply returned.</param>
    /// <param name="defaultValue">The value that must be returned if the query yielded no results.</param>
    /// <returns>The single entity that matches the expression or the defaultvalue if there were no results.</returns>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      if(this.internalCache.Count == 0 && this.SourceFile.Exists) {
        IEnumerable<TEntity> readEntities = this.ReadAllRecordsFromFile(this.SourceFile, dataSourceInfo);
        this.ApplyIdentifiers(readEntities);
        this.RefreshCache(readEntities);
      }

      return this.ConcatCache().SingleOrDefault(expression, defaultValue);
    }

    /// <summary>Adds a new entity to the repository. It is added to the addition cache untill it is saved using the 
    /// <see cref="M:SaveChanges()"/> method. A temporary (negative) RecordID is assigned to the entity. This will be reset 
    /// when the entity is saved.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.</param>
    /// <returns>The entity as it was added to the repository.</returns>
    protected override TEntity AddEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      if(entity.RecordId > 0) {
        lock(this.deletionCacheLock) {
          if(this.deletionCache.Contains(entity, entityComparer)) {
            this.deletionCache.Remove(entity, entityComparer);
            lock(this.updateCacheLock) {
              this.updateCache.Add(entity);
            }

            return entity;
          }
        }
      }

      int newRecordId = -1;

      lock(this.additionCacheLock) {
        if(this.additionCache.Count > 0) {
          newRecordId = this.additionCache.Min(t => t.RecordId) - 1;
        }

        entity.RecordId = newRecordId;
        this.additionCache.Add(entity);
      }

      return entity;
    }

    /// <summary>Removes an entity from the repository. Depending on the status of the entity, it is removed from the 
    /// addition-cache or it is added to the deletion-cache untill it is saved using the <see cref="M:SaveChanges()"/> 
    /// method.</summary>
    /// <param name="entity">The entity that must be removed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.</param>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }
      else if(entity.RecordId < 0) {
        lock(this.additionCacheLock) {
          if(this.additionCache.Contains(entity, entityComparer)) {
            this.additionCache.Remove(entity, entityComparer);
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the addition-cache.");
          }
        }
      }
      else {
        lock(this.updateCacheLock) {
          if(this.updateCache.Contains(entity, entityComparer)) {
            this.updateCache.Remove(entity, entityComparer);
          }
        }

        lock(this.internalCacheLock) {
          if(this.internalCache.Contains(entity, entityComparer)) {
            lock(this.deletionCacheLock) {
              if(!this.deletionCache.Contains(entity, entityComparer)) {
                this.deletionCache.Add(entity);
              }
              else {
                throw new InvalidOperationException("Cannot delete the same entity more then once.");
              }
            }
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the internal cache.");
          }
        }
      }
    }

    /// <summary>Updates an entity in the repository. Depending on the status of the entity, it is updated in the addition- 
    /// cache or it is added to the update-cache.</summary>
    /// <param name="entity">The entity that contains the updated values.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This
    /// parameter is not used.</param>
    /// <returns>The entity as it was stored in the repository.</returns>
    protected override TEntity UpdateEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }
      else if(entity.RecordId < 0) {
        lock(this.additionCacheLock) {
          if(this.additionCache.Contains(entity, entityComparer)) {
            int indexOfEntity = this.additionCache.IndexOf(entity, entityComparer);
            this.additionCache[indexOfEntity] = entity;
            return entity;
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the addition-cache.");
          }
        }
      }
      else {
        lock(this.internalCacheLock) {
          if(this.internalCache.Contains(entity, entityComparer)) {
            lock(this.deletionCacheLock) {
              if(this.deletionCache.Contains(entity, entityComparer)) {
                throw new InvalidOperationException("Cannot update the entity since it already marked for deletion.");
              }
            }

            lock(this.updateCacheLock) {
              if(this.updateCache.Contains(entity, entityComparer)) {
                int indexOfEntity = this.updateCache.IndexOf(entity, entityComparer);
                this.updateCache[indexOfEntity] = entity;
              }
              else {
                this.updateCache.Add(entity);
              }

              return entity;
            }
          }
          else {
            throw new InvalidOperationException("Could not find the entity in the internal cache.");
          }
        }
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
    /// <returns>The entities after they have been written to the file (in case the saving resulted in some updated values).
    /// </returns>
    protected abstract IEnumerable<TEntity> WriteAllRecordsToFile(FileInfo destinationFile, DataSourceInfo dataSourceInfo, IEnumerable<TEntity> contents);
    #endregion

    #region Dispose methods
    /// <summary>Disposes all the managed resources that are held by this instance.</summary>
    protected override void DisposeManagedResources() {
      try {
        this.sourceFileMonitor.Dispose();
      }
      finally {
        base.DisposeManagedResources();
      }
    }
    #endregion

    #region Protected methods
    /// <summary>Reads all the records from the file that match a specific criteria. The default implementation simply reads 
    /// all the records from the file and selects the records that match the criteria. Override this method if a more
    /// efficient approach is feasible.</summary>
    /// <param name="sourceFile">The file that must be read.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <param name="expression">The expression that must be used to select the correct records.</param>
    /// <returns>The entities that match the expression or an empty collection if there were no results.</returns>
    protected virtual IEnumerable<TEntity> ReadMultipleRecordsFromFile(FileInfo sourceFile, DataSourceInfo dataSourceInfo, Func<TEntity, bool> expression) {
      IEnumerable<TEntity> allEntities = this.ReadAllRecordsFromFile(sourceFile, dataSourceInfo);
      return allEntities.Where(expression);
    }

    /// <summary>Reads a single the records from the file that matches a specific criteria. The default implementation 
    /// simply reads all the records from the file and selects the first record that matches the criteria. Override this 
    /// method if a more efficient approach is feasible.</summary>
    /// <param name="sourceFile">The file that must be read.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <param name="expression">The expression that must be used to select the correct records.</param>
    /// <returns>The entity that matches the expression or <see langword="null"/> if there were no results.</returns>
    protected virtual TEntity ReadSingleRecordFromFile(FileInfo sourceFile, DataSourceInfo dataSourceInfo, Func<TEntity, bool> expression) {
      IEnumerable<TEntity> allEntities = this.ReadAllRecordsFromFile(sourceFile, dataSourceInfo);
      return allEntities.FirstOrDefault(expression, null);
    }

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

    /// <summary>Performs some actions in response to the sourcefile being deleted. This will normally result in clearing 
    /// the cache.</summary>
    protected virtual void OnSourceFileDeleted() {
      this.ClearCache();
    }

    /// <summary>Performs some actions in response to the sourcefile being created. This will normally result in filling 
    /// the cache.</summary>
    protected virtual void OnSourceFileCreated() {
      IEnumerable<TEntity> fileEntities = this.ReadAllRecordsFromFile(this.SourceFile, null);
      this.RefreshCache(fileEntities);
    }

    /// <summary>Performs some actions in response to the sourcefile being changed. This will normally result in refreshing 
    /// the cache.</summary>
    protected virtual void OnSourceFileChanged() {
      IEnumerable<TEntity> fileEntities = this.ReadAllRecordsFromFile(this.SourceFile, null);
      this.RefreshCache(fileEntities);
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
      }
    }

    /// <summary>Stops the monitor that monitors the sourcefile. It is save to call this method multiple times.</summary>
    protected void StopSourceFileMonitor() {
      if(this.sourceFileMonitor != null && this.sourceFileMonitor.EnableRaisingEvents) {
        this.sourceFileMonitor.EnableRaisingEvents = false;
      }
    }

    /// <summary>Resumes the monitor that monitors the sourcefile. This method is added as a counterpart for the 
    /// <see cref="M:PauseSourceFileMonitor()"/> method, however since the filemonitor does not have any specific pause/
    /// resume behaviour, calling this method is the same as calling <see cref="M:StartSourceFileMonitor()"/>.</summary>
    protected void ResumeSourceFileMonitor() {
      this.StartSourceFileMonitor();
    }

    /// <summary>Pauses the monitor that monitors the sourcefile. This method is added as a counterpart for the 
    /// <see cref="M:ResumeSourceFileMonitor()"/> method, however since the filemonitor does not have any specific pause/
    /// resume behaviour, calling this method is the same as calling <see cref="M:StopSourceFileMonitor()"/>.</summary>
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
    }

    /// <summary>Concatenates the internal caches into one complete and up-to-date cache.</summary>
    /// <returns>The concatenated cache-values.</returns>
    private IEnumerable<TEntity> ConcatCache() {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      /* First, concatenate the basic cache with the added entities... */
      IEnumerable<TEntity> totalCache = this.internalCache.Concat(this.additionCache);
      /*...then remove the entities that were updated from the basic cache and replace them with the updated versions... */
      totalCache = totalCache.Except(this.updateCache, entityComparer).Concat(this.updateCache);
      /* ...finally, remove the deleted entities */
      totalCache = totalCache.Except(this.deletionCache, entityComparer);

      return totalCache;
    }
    #endregion

    #region Private event handlers
    /// <summary>Handles the situation in which the used sourcefile is renamed. If the file is renamed to something other 
    /// then the original filename, it is regarded to be the same as deleting the file, since the file can no longer be 
    /// found under its original name. Otherwise, the event is treated as a created-event.</summary>
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
      this.OnSourceFileChanged();
    }
    #endregion
  }
}
