using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Transactions;
using Enkoni.Framework.Collections;
using Enkoni.Framework.Linq;
using LinqKit;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the functionality using the Entity 
  /// Framework.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class DatabaseRepository<TEntity> : Repository<TEntity>, IDatabaseRepository
    where TEntity : class, IEntity<TEntity>, new() {
    #region Private instance variables
    /// <summary>The collection of entities that are to be added to the data source. </summary>
    private List<TEntity> additionCache;

    /// <summary>The collection of entities that are to be updated in the data source.</summary>
    private List<TEntity> updateCache;

    /// <summary>The collection of entities that are to be removed from the data source.</summary>
    private List<TEntity> deletionCache;

    /// <summary>The types that are part of the graph of the entity type that is handled by this repository.</summary>
    private List<Type> graphMembers;

    /// <summary>A lock that is used to synchronize access to the internal storage.</summary>
    private ReaderWriterLockSlim storageLock = new ReaderWriterLockSlim();
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="DatabaseRepository{TEntity}"/> class using the specified <see cref="DataSourceInfo"/>.
    /// </summary>
    /// <param name="dataSourceInfo">The data source information that must be used to access the database.</param>
    public DatabaseRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
      if(DatabaseSourceInfo.IsDbContextSpecified(dataSourceInfo)) {
        this.DbContext = DatabaseSourceInfo.SelectDbContext(dataSourceInfo);
      }

      /* Initializes the internal collections */
      this.additionCache = new List<TEntity>();
      this.updateCache = new List<TEntity>();
      this.deletionCache = new List<TEntity>();

      if(DatabaseSourceInfo.IsSaveGraphSpecified(dataSourceInfo)) {
        this.graphMembers = DetermineGraphTypes();
      }
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the <see cref="DbContext"/> that is used to access the database.</summary>
    protected DbContext DbContext { get; private set; }
    #endregion

    #region IDatabaseRepository methods
    /// <summary>Replaces the current <see cref="DbContext"/> with the specified one. The current <see cref="DbContext"/> is first disposed.</summary>
    /// <param name="dbContext">The new <see cref="DbContext"/> that must be used.</param>
    public void ReloadObjectContext(DbContext dbContext) {
      this.DbContext.Dispose();
      this.DbContext = dbContext;
    }
    #endregion

    #region Repository<T> overrides
    /// <summary>Resets the repository by undoing any unsaved changes.</summary>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    protected override void ResetCore(DataSourceInfo dataSourceInfo) {
      try {
        this.storageLock.EnterWriteLock();

        this.ResetLocalCacheNoLock();

        this.ResetDbContextNoLock(dataSourceInfo);
      }
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Submits all the changes to the database.</summary>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    protected override void SaveChangesCore(DataSourceInfo dataSourceInfo) {
      DbContext context = this.SelectDbContext(dataSourceInfo);

      using(TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required)) {
        try {
          this.storageLock.EnterWriteLock();

          this.PrepareAdditions(context);

          this.PrepareDeletions(context);

          Dictionary<DbEntityEntry, EntityState> detachedObjects = this.PrepareUpdates(context);

          context.SaveChanges();

          foreach(KeyValuePair<DbEntityEntry, EntityState> kvp in detachedObjects) {
            kvp.Key.State = kvp.Value;
          }

          this.ResetLocalCacheNoLock();
          transaction.Complete();
        }
        catch(DataException ex) {
          throw new InvalidOperationException("The changes cannot be saved. The operation has been rolled back", ex);
        }
        finally {
          if(this.storageLock.IsWriteLockHeld) {
            this.storageLock.ExitWriteLock();
          }
        }
      }
    }

    /// <summary>Creates a new entity of type <typeparamref name="TEntity"/>. This is done by calling the default constructor of 
    /// <typeparamref name="TEntity"/>.</summary>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <returns>The created entity.</returns>
    protected override TEntity CreateEntityCore(DataSourceInfo dataSourceInfo) {
      TEntity entity = new TEntity();
      return entity;
    }

    /// <summary>Inserts a new entity to the repository.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity AddEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      try {
        this.storageLock.EnterWriteLock();

        return this.AddEntityNoLock(entity, dataSourceInfo);
      }
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Adds a collection of new entities to the repository. They are added to the addition cache until it is saved using the 
    /// <see cref="Repository{T}.SaveChanges()"/> method. A temporary (negative) RecordID is assigned to the entities. This will be reset when the 
    /// entity is saved.</summary>
    /// <param name="entities">The entities that must be added.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they were added to the repository.</returns>
    protected override IEnumerable<TEntity> AddEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      try {
        this.storageLock.EnterWriteLock();

        return this.AddEntitiesNoLock(entities, dataSourceInfo);
      }
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Updates the repository with the changes made to <paramref name="entity"/>. Since the entity framework already monitors the state of 
    /// entities, no additional functionality is required. This method is therefore empty.</summary>
    /// <param name="entity">The entity that was updated.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity UpdateEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }

      try {
        this.storageLock.EnterWriteLock();

        return this.UpdateEntityNoLock(entity, dataSourceInfo);
      }
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
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

      this.storageLock.EnterWriteLock();
      try {
        return this.UpdateEntitiesNoLock(entities, dataSourceInfo);
      }
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Deletes an entity from the repository.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      try {
        this.storageLock.EnterWriteLock();

        this.DeleteEntityNoLock(entity, dataSourceInfo);
      }
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Removes a collection of entities from the repository. Depending on the status of each entity, it is removed from the addition-cache 
    /// or it is added to the deletion-cache until it is saved using the <see cref="Repository{T}.SaveChanges()"/> method.</summary>
    /// <param name="entities">The entities that must be removed.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    protected override void DeleteEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      if(entities.Any(e => e.RecordId == 0)) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      try {
        this.storageLock.EnterWriteLock();
        this.DeleteEntitiesNoLock(entities, dataSourceInfo);
      }
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Finds all the available entities that match the specified expression.</summary>
    /// <param name="expression">The expression to which the entities must match.</param>
    /// <param name="sortRules">The specification of the sort rules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="maximumResults">The maximum number of results that must be retrieved. Use '-1' to retrieve all results.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <returns>The entities that match the specified expression.</returns>
    protected override IEnumerable<TEntity> FindAllCore(Expression<Func<TEntity, bool>> expression,
      SortSpecifications<TEntity> sortRules, int maximumResults, string[] includePaths, DataSourceInfo dataSourceInfo) {
      DbContext context = this.SelectDbContext(dataSourceInfo);

      /* First query the database directly (this will also populate the local cache) */
      expression = EntityCastRemoverVisitor.Convert(expression);
      IQueryable<TEntity> databaseQuery = context.Set<TEntity>();
      if(includePaths != null && includePaths.Length > 0) {
        foreach(string path in includePaths) {
          databaseQuery = databaseQuery.Include(path);
        }
      }
      
      databaseQuery = databaseQuery.AsExpandable().Where(expression);

      /* Add the ordering to the query */
      databaseQuery = databaseQuery.OrderBy(sortRules);

      if(maximumResults != -1) {
        /* Take the maximum into account */
        databaseQuery = databaseQuery.Take(maximumResults);
      }

      /* Force the execution of the query */
      IEnumerable<TEntity> result = databaseQuery.AsEnumerable().ToList();

      IEqualityComparer<TEntity> comparer = new EntityEqualityComparer<TEntity>();
      
      /* Remove the unsaved deletions from the the result */
      result = result.Except(this.deletionCache, comparer);

      if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
        result = result.Select(t => ((ICloneable)t).Clone() as TEntity);
      }

      /* Add any unsaved additions that match the expression */
      result = result.Concat(this.additionCache.Where(expression.Compile()));

      /* Finally, order the results */
      result = result.OrderBy(sortRules);

      if(maximumResults != -1) {
        /* Take the maximum into account */
        result = result.Take(maximumResults);
      }

      return result.ToList();
    }

    /// <summary>Finds a single entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TEntity FindSingleCore(Expression<Func<TEntity, bool>> expression, string[] includePaths, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      expression = EntityCastRemoverVisitor.Convert(expression);
      Func<TEntity, bool> compiledExpression = expression.Compile();
      /* First, query the addition cache */
      TEntity result = this.additionCache.SingleOrDefault(compiledExpression, null);

      if(result != null) {
        return result;
      }

      /* Then, query the update cache */
      result = this.updateCache.SingleOrDefault(compiledExpression, null);

      if(result != null) {
        return result;
      }

      /* Then, query the deletion cache */
      result = this.deletionCache.SingleOrDefault(compiledExpression, null);
      if(result != null) {
        /* An entity matching the expression, has been marked for deletion, return the default value */
        return defaultValue;
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);
      IQueryable<TEntity> databaseQuery = context.Set<TEntity>();
      if(includePaths != null && includePaths.Length > 0) {
        foreach(string path in includePaths) {
          databaseQuery = databaseQuery.Include(path);
        }
      }
      
      databaseQuery = databaseQuery.AsExpandable();

      TEntity foundEntity = null;

      /* Check if any matching entities are marked for deletion. If so, we need to select more elements from the database
       * to make up for the deleted entities. */
      IEnumerable<TEntity> deletedEntities = this.deletionCache.Where(compiledExpression);
      if(deletedEntities.Count() > 0) {
        IEnumerable<TEntity> databaseEntities = databaseQuery.Where(expression).Take(deletedEntities.Count() + 1);
        foundEntity = databaseEntities.Except(deletedEntities, new EntityEqualityComparer<TEntity>()).SingleOrDefault();
      }
      else {
        /* No matching entity has been marked for deletion. Therefore, simply get the first from the database */
        foundEntity = databaseQuery.SingleOrDefault(expression);
      }

      if(foundEntity == null) {
        return defaultValue;
      }
      else {
        if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
          return ((ICloneable)foundEntity).Clone() as TEntity;
        }
        else {
          return foundEntity;
        }
      }
    }

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="sortRules">The specification of the sort rules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TEntity FindFirstCore(Expression<Func<TEntity, bool>> expression, SortSpecifications<TEntity> sortRules, string[] includePaths,
      DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      expression = EntityCastRemoverVisitor.Convert(expression);
      Func<TEntity, bool> compiledExpression = expression.Compile();

      /* This repository defines that any unsaved additions take precedence over previously saved entities, therefore the the addition cache is 
       * queried first */
      TEntity result = this.additionCache.OrderBy(sortRules).FirstOrDefault(compiledExpression, null);

      if(result != null) {
        return result;
      }

      /* The update cache is queried second */
      result = this.updateCache.OrderBy(sortRules).FirstOrDefault(compiledExpression, null);

      if(result != null) {
        return result;
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);
      IQueryable<TEntity> databaseQuery = context.Set<TEntity>();
      if(includePaths != null && includePaths.Length > 0) {
        foreach(string path in includePaths) {
          databaseQuery = databaseQuery.Include(path);
        }
      }

      databaseQuery = databaseQuery.AsExpandable();

      /* Add the ordering to the query */
      if(sortRules != null) {
        databaseQuery = databaseQuery.OrderBy(sortRules);
      }

      TEntity foundEntity = null;

      /* Check if any matching entities are marked for deletion. If so, we need to select more elements from the database
       * to make up for the deleted entities. */
      IEnumerable<TEntity> deletedEntities = this.deletionCache.Where(compiledExpression);
      if(deletedEntities.Count() > 0) {
        IEnumerable<TEntity> databaseEntities = databaseQuery.Where(expression).Take(deletedEntities.Count() + 1);
        foundEntity = databaseEntities.Except(deletedEntities, new EntityEqualityComparer<TEntity>()).FirstOrDefault();
      }
      else {
        /* No matching entity has been marked for deletion. Therefore, simply get the first from the database */
        foundEntity = databaseQuery.FirstOrDefault(expression);
      }

      if(foundEntity == null) {
        return defaultValue;
      }
      else {
        if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
          return ((ICloneable)foundEntity).Clone() as TEntity;
        }
        else {
          return foundEntity;
        }
      }
    }

    /// <summary>Creates a LIKE-expression using the specified field and search pattern.</summary>
    /// <param name="field">The field of type <c>T</c> that must match the pattern.</param>
    /// <param name="pattern">The pattern to which the field must apply. The pattern may contain a '*' and '?' wildcard.</param>
    /// <returns>The created expression.</returns>
    protected override Expression<Func<TEntity, bool>> CreateLikeExpressionCore(Expression<Func<TEntity, string>> field,
      string pattern) {
      throw new NotSupportedException("The Entity Framework currently does not support LIKE-queries. Use StartsWith, EndsWith or Contains instead.");

      /* If the Entity Framework would have supported LIKE queries the way standard Linq-to-SQL does, the implementation would have looked like 
       * this: */
      /* Replace the wildcards */
      /*pattern = pattern.Replace("*", "%").Replace("?", "_");*/

      /* Get the methodinfo for the 'Like' method of the 'SqlMethods' class */
      /*System.Reflection.MethodInfo likeMethod = typeof(SqlMethods).GetMethod("Like", new Type[]{typeof(string),typeof(string)});*/
      /* Create a constantexpression for the pattern */
      /*ConstantExpression patternExpr = Expression.Constant(pattern, typeof(string));*/
      /* Create a methodexpression that executes the Like-method */
      /*MethodCallExpression likeCallExpression = Expression.Call(likeMethod, field.Body, patternExpr);*/
      /* Convert the expression into a lambdaexpression */
      /*Expression<Func<TEntity, bool>>  resultExpression = Expression.Lambda<Func<TEntity, bool>>(likeCallExpression, field.Parameters[0]);*/
      /*return resultExpression;*/
    }
    #endregion

    #region Protected overridable helper methods
    /// <summary>Selects the <see cref="DbContext"/> that must be used. If the specified DataSourceInfo contains a valid <see cref="DbContext"/>, it is used; otherwise the value 
    /// of the property '<see cref="DbContext"/>' is used.</summary>
    /// <param name="dataSourceInfo">Any information regarding the database that is used as data source.</param>
    /// <returns>The <see cref="DbContext"/> that must be used.</returns>
    protected virtual DbContext SelectDbContext(DataSourceInfo dataSourceInfo) {
      if(DatabaseSourceInfo.IsDbContextSpecified(dataSourceInfo)) {
        return DatabaseSourceInfo.SelectDbContext(dataSourceInfo);
      }
      else {
        return this.DbContext;
      }
    }

    /// <summary>Resets the repository by undoing any unsaved changes. This implementation does not acquire a write lock on the local storage and can 
    /// therefore be called from within a context that already has a write lock on the internal storage.</summary>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    protected virtual void ResetDbContextNoLock(DataSourceInfo dataSourceInfo) {
      DbContext context = this.SelectDbContext(dataSourceInfo);

      /* Retrieve the unsaved changes */
      context.ChangeTracker.DetectChanges();
      List<DbEntityEntry<TEntity>> changedEntries = context.ChangeTracker.Entries<TEntity>().Where(x => x.State != System.Data.Entity.EntityState.Unchanged).ToList();

      /* Undo any modifications */
      foreach(DbEntityEntry<TEntity> entry in changedEntries.Where(x => x.State == System.Data.Entity.EntityState.Modified)) {
        entry.CurrentValues.SetValues(entry.OriginalValues);
        entry.State = System.Data.Entity.EntityState.Unchanged;
      }

      /* Undo any additions */
      foreach(DbEntityEntry<TEntity> entry in changedEntries.Where(x => x.State == System.Data.Entity.EntityState.Added)) {
        entry.State = System.Data.Entity.EntityState.Detached;
      }

      /* Undo any deletions */
      foreach(DbEntityEntry<TEntity> entry in changedEntries.Where(x => x.State == System.Data.Entity.EntityState.Deleted)) {
        entry.State = System.Data.Entity.EntityState.Unchanged;
        entry.CurrentValues.SetValues(entry.OriginalValues);
      }
    }
    #endregion

    #region Private helper methods
    /// <summary>Determines which types make up the graph of this entity.</summary>
    /// <returns>The list of types that make up the graph.</returns>
    private static List<Type> DetermineGraphTypes() {
      List<Type> graphTypes = new List<Type> { typeof(TEntity) };

      graphTypes = DetermineGraphTypes(typeof(TEntity), graphTypes);
      
      return graphTypes;
    }

    /// <summary>Recursively determines the types that make up the graph of this entity.</summary>
    /// <param name="objectType">The type whose graph types must be determined.</param>
    /// <param name="graphTypes">The types that have already been discovered.</param>
    /// <returns>The list of types that make up the graph.</returns>
    private static List<Type> DetermineGraphTypes(Type objectType, List<Type> graphTypes) {
      Type entityType = typeof(IEntity<>);
      Type collectionType = typeof(IEnumerable<>);

      IEnumerable<PropertyInfo> properties = objectType.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(propInfo => !graphTypes.Contains(propInfo.PropertyType));
      properties = properties.Where(propInfo => propInfo.PropertyType.Implements(entityType) || propInfo.PropertyType.Implements(collectionType)).ToArray();
      
      graphTypes.AddRange(properties.Where(propInfo => propInfo.PropertyType.Implements(entityType)).Select(propInfo => propInfo.PropertyType));
      
      foreach(PropertyInfo propertyInfo in properties) {
        if(propertyInfo.PropertyType.Implements(collectionType)) {
          Type genericType = propertyInfo.PropertyType.GetGenericArguments().FirstOrDefault();
          if(genericType != null && genericType.Implements(entityType) && !graphTypes.Contains(genericType)) {
            graphTypes.Add(genericType);
            graphTypes = DetermineGraphTypes(genericType, graphTypes);
          }
        }
        else {
          graphTypes = DetermineGraphTypes(propertyInfo.PropertyType, graphTypes);
        }
      }

      return graphTypes;
    }

    /// <summary>Handles the update of entities that are added to this repository but are not yet saved.</summary>
    /// <param name="entities">The entities that are updated.</param>
    /// <param name="tempAdditionCache">A copy of the addition cache.</param>
    /// <param name="entityComparer">The comparer that is used to compare the entities based on their record ID.</param>
    /// <returns>The entities from the addition cache that already have the updates applied.</returns>
    private static List<TEntity> UpdateAddedEntities(IEnumerable<TEntity> entities, List<TEntity> tempAdditionCache, EntityEqualityComparer<TEntity> entityComparer) {
      List<TEntity> newlyAddedEntities = new List<TEntity>();

      IEnumerable<TEntity> addedEntities = entities.Where(e => e.RecordId < 0);
      foreach(TEntity addedEntity in addedEntities) {
        /* If the entity is marked for addition, update the entity in the addition cache */
        if(tempAdditionCache.Contains(addedEntity, entityComparer)) {
          int indexOfEntity = tempAdditionCache.IndexOf(addedEntity, entityComparer);
          TEntity repositoryEntity = tempAdditionCache[indexOfEntity];

          /* Copy the  values of the updated entity into the cached entity */
          repositoryEntity.CopyFrom(addedEntity);
          newlyAddedEntities.Add(repositoryEntity);
        }
        else {
          throw new InvalidOperationException("Could not find the entity in the addition-cache.");
        }
      }

      return newlyAddedEntities;
    }

    /// <summary>Adds new entities that, based on their record ID, appear to already exist in the repository.</summary>
    /// <param name="entities">The entities that must be added.</param>
    /// <param name="unhandledEntities">The entities that have not yet been analyzed.</param>
    /// <param name="tempUpdateCache">A copy of the update cache.</param>
    /// <param name="tempDeletionCache">A copy of the deletion cache.</param>
    /// <param name="entityComparer">The comparer that is used to compare the entities based on their record ID.</param>
    /// <param name="context">The database context that gives access to the persistency.</param>
    /// <param name="handledEntities">Holds the combination of entities that have been added.</param>
    /// <returns>The entities from the repository that already have the updates applied.</returns>
    private static List<TEntity> AddExistingEntities(IEnumerable<TEntity> entities, List<TEntity> unhandledEntities, List<TEntity> tempUpdateCache, List<TEntity> tempDeletionCache, 
        EntityEqualityComparer<TEntity> entityComparer, DbContext context, Dictionary<TEntity, TEntity> handledEntities) {
      List<TEntity> updatedEntities = new List<TEntity>();
      if(entities.Any(e => e.RecordId > 0)) {
        List<TEntity> updatableEntities = new List<TEntity>();
        IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);
        ReferenceEqualityComparer<TEntity> referenceComparer = new ReferenceEqualityComparer<TEntity>();
        /* At least some of the entities already have an ID which suggests that they came from the original data source */
        foreach(TEntity existingEntity in existingEntities) {
          if(tempDeletionCache.Contains(existingEntity, entityComparer)) {
            /* The entity has been marked for deletion, undelete it... */
            tempDeletionCache.Remove(existingEntity, entityComparer);
            /* ...and mark it as updated in case any of the fields have been altered. */
            updatableEntities.Add(existingEntity);

            bool removeResult = unhandledEntities.Remove(existingEntity, referenceComparer);
            Debug.Assert(removeResult, "Somehow the result could not be removed from the collection of handled entities.");
          }
        }

        foreach(TEntity updatableEntity in updatableEntities) {
          TEntity storedEntity = context.Set<TEntity>().SingleOrDefault(t => t.RecordId == updatableEntity.RecordId);
          if(storedEntity == null) {
            /* This is not very likely, otherwise how could the entity exist in the deletion cache */
            throw new InvalidOperationException("Could not find the entity in the internal cache.");
          }
          else {
            storedEntity.CopyFrom(updatableEntity);
            updatedEntities.Add(storedEntity);
            tempUpdateCache.Add(storedEntity);
            handledEntities.Add(updatableEntity, storedEntity);
          }
        }
      }

      return updatedEntities;
    }

    /// <summary>Adds new entities to the repository.</summary>
    /// <param name="unhandledEntities">The entities that have not yet been analyzed.</param>
    /// <param name="tempAdditionCache">A copy of the addition cache.</param>
    /// <param name="handledEntities">Holds the combination of entities that have been added.</param>
    /// <returns>The entities that have been added.</returns>
    private static List<TEntity> AddNewEntities(List<TEntity> unhandledEntities, List<TEntity> tempAdditionCache, Dictionary<TEntity, TEntity> handledEntities) {
      List<TEntity> addedEntities = new List<TEntity>();
      if(unhandledEntities.Count > 0) {
        /* At least some of the entities are either new or came from another data source */
        /* Determine the new temporary ID for the entities */
        int newRecordId = -1;
        if(tempAdditionCache.Count > 0) {
          newRecordId = tempAdditionCache.Min(t => t.RecordId) - 1;
        }

        foreach(TEntity unhandledEntity in unhandledEntities) {
          TEntity copyOfEntity = unhandledEntity.CreateCopyOrClone();

          copyOfEntity.RecordId = newRecordId;
          --newRecordId;
          /* Add it to the addition cache */
          tempAdditionCache.Add(copyOfEntity);
          addedEntities.Add(copyOfEntity);
          handledEntities.Add(unhandledEntity, copyOfEntity);
        }
      }

      return addedEntities;
    }

    /// <summary>Prepares the context with the registered additions of the entities.</summary>
    /// <param name="context">The context that keeps track of the entities.</param>
    private void PrepareDeletions(DbContext context) {
      foreach(TEntity deletedEntity in this.deletionCache) {
        context.Set<TEntity>().Remove(deletedEntity);
      }
    }

    /// <summary>Prepares the context with the registered deletions of the entities.</summary>
    /// <param name="context">The context that keeps track of the entities.</param>
    private void PrepareAdditions(DbContext context) {
      foreach(TEntity addedEntity in this.additionCache) {
        context.Set<TEntity>().Add(addedEntity);
      }
    }

    /// <summary>Prepares the context with the registered updates of the entities.</summary>
    /// <param name="context">The context that keeps track of the entities.</param>
    /// <returns>The objects and their original state that were set to Unchanged to prevent them from being saved.</returns>
    private Dictionary<DbEntityEntry, EntityState> PrepareUpdates(DbContext context) {
      /* Get all the modified entries from the context */
      IEnumerable<DbEntityEntry> modifiedEntries = context.ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);

      /* Filter out any modified entities that are not part of the responsibility of this repository */
      IEnumerable<DbEntityEntry> unwantedChanges;
      if(this.graphMembers == null || this.graphMembers.Count == 0) {
        unwantedChanges = modifiedEntries.Where(entry => !(entry.Entity is TEntity));
      }
      else {
        unwantedChanges = modifiedEntries.Where(entry => !this.graphMembers.Contains(entry.Entity.GetType()));
      }

      /* Get the modified entries that belong to this repository directly */
      IEnumerable<DbEntityEntry<TEntity>> modifiedEntities = modifiedEntries.Except(unwantedChanges).Where(entry => entry.Entity.GetType() == typeof(TEntity)).Select(entry => entry.Cast<TEntity>());
      List<int> updatedRecordIds = this.updateCache.Select(entity => entity.RecordId).ToList();

      /* If an entry was modified, but its record ID is not known by this repository, it must have been modified from outside this repository. Therefore the change must be ignored */
      IEnumerable<DbEntityEntry<TEntity>> unwantedEntityChanges = modifiedEntities.Where(entry => !updatedRecordIds.Contains(entry.Entity.RecordId));
      unwantedChanges = unwantedChanges.Concat(unwantedEntityChanges.Select(entity => (DbEntityEntry)entity));

      /* Force the state of the unwanted changes to Unchanged to avoid them from being saved automatically */
      Dictionary<DbEntityEntry, EntityState> detachedObjects = new Dictionary<DbEntityEntry, EntityState>();
      ObjectContext objectContext = ((IObjectContextAdapter)context).ObjectContext;
      foreach(DbEntityEntry entry in unwantedChanges) {
        /* Add the entry to the collection of detachedObjects */
        detachedObjects.Add(entry, entry.State);
        
        /* Set the state to Unchanged. By setting the state using the Object STate Manager, the changed property values are preserved */
        objectContext.ObjectStateManager.GetObjectStateEntry(entry.Entity).ChangeState(EntityState.Unchanged);
      }

      /* In case some of the updated entries where forced to Unchanged (either by this repository or another repository), force them back to Modified */
      IEnumerable<DbEntityEntry<TEntity>> updatedEntries = context.ChangeTracker.Entries<TEntity>().Where(entry => updatedRecordIds.Contains(entry.Entity.RecordId));
      foreach(DbEntityEntry<TEntity> updatedEntry in updatedEntries) {
        updatedEntry.State = EntityState.Modified;
      }

      return detachedObjects;
    }

    /// <summary>Updates the repository with the changes made to <paramref name="entity"/>. This implementation does not acquire a write lock on the
    /// local storage and can therefore be called from within a context that already has a write lock on the internal storage.</summary>
    /// <param name="entity">The entity that was updated.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <returns>The entity with the most recent values.</returns>
    private TEntity UpdateEntityNoLock(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      if(entity.RecordId < 0) {
        if(this.additionCache.Contains(entity, entityComparer)) {
          int indexOfEntity = this.additionCache.IndexOf(entity, entityComparer);
          TEntity repositoryEntity = this.additionCache[indexOfEntity];

          /* Copy the  values of the updated entity into the cached entity */
          repositoryEntity.CopyFrom(entity);

          if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
            return ((ICloneable)repositoryEntity).Clone() as TEntity;
          }
          else {
            entity.CopyFrom(repositoryEntity);
            return entity;
          }
        }
        else {
          throw new InvalidOperationException("Could not find the entity in the addition-cache.");
        }
      }
      else {
        Expression<Func<TEntity, bool>> queryById = EntityCastRemoverVisitor.Convert((TEntity t) => t.RecordId == entity.RecordId);
        TEntity existingEntity = context.Set<TEntity>().SingleOrDefault(queryById);

        if(existingEntity != null) {
          if(this.deletionCache.Contains(entity, entityComparer)) {
            throw new InvalidOperationException("Cannot update the entity since it already marked for deletion.");
          }

          if(this.updateCache.Contains(entity, entityComparer)) {
            /* Retrieve the previous updated version of the entity from the cache */
            int indexOfEntity = this.updateCache.IndexOf(entity, entityComparer);
            existingEntity = this.updateCache[indexOfEntity];

            /* Copy the  values of the updated entity into the cached entity */
            existingEntity.CopyFrom(entity);
          }
          else {
            existingEntity.CopyFrom(entity);
            this.updateCache.Add(existingEntity);
          }

          if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
            return ((ICloneable)existingEntity).Clone() as TEntity;
          }
          else {
            entity.CopyFrom(existingEntity);
            return entity;
          }
        }
        else {
          throw new InvalidOperationException("The entity seems to be new and can therefore not be updated.");
        }
      }
    }

    /// <summary>Updates a collection of entities in the repository. This implementation does not acquire a write lock on the local storage and can 
    /// therefore be called from within a context that already has a write lock on the internal storage.</summary>
    /// <param name="entities">The entities that contain the updated values.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they are stored in the repository.</returns>
    private IEnumerable<TEntity> UpdateEntitiesNoLock(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      if(entities.Any(e => e.RecordId == 0)) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);

      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempAdditionCache = this.additionCache.ToList();
      List<TEntity> tempDeletionCache = this.deletionCache.ToList();

      List<TEntity> newlyAddedEntities = UpdateAddedEntities(entities, tempAdditionCache, entityComparer);

      /* Create a collection for all the entities that can be updated 
        * Key: updated entity
        * Value: stored entity */
      Dictionary<TEntity, TEntity> updatableEntities = this.UpdateExistingEntities(entities, tempDeletionCache, context, entityComparer);

      /* Everything checks out, start the update-process */
      List<TEntity> updatedEntities = new List<TEntity>(updatableEntities.Count);
      foreach(KeyValuePair<TEntity, TEntity> kvp in updatableEntities) {
        kvp.Value.CopyFrom(kvp.Key);
        updatedEntities.Add(kvp.Value);
      }

      /* Replace the original caches to complete the 'transaction' */
      this.additionCache = tempAdditionCache;
      this.deletionCache = tempDeletionCache;

      if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
        return newlyAddedEntities.Concat(updatedEntities).Select(entity => ((ICloneable)entity).Clone() as TEntity).ToList();
      }
      else {
        IEnumerable<TEntity> resultList = newlyAddedEntities.Concat(updatedEntities);
        Linq.Extensions.ForEach(resultList, entity => entities.Single(e => e.RecordId == entity.RecordId).CopyFrom(entity));
        return entities;
      }
    }

    /// <summary>Handles the update of entities that are already present in this repository.</summary>
    /// <param name="entities">The entities that are updated.</param>
    /// <param name="tempDeletionCache">A copy of the deletion cache.</param>
    /// <param name="context">The database context that gives access to the persistency.</param>
    /// <param name="entityComparer">The comparer that is used to compare the entities based on their record ID.</param>
    /// <returns>The entities from the repository that already have the updates applied.</returns>
    private Dictionary<TEntity, TEntity> UpdateExistingEntities(IEnumerable<TEntity> entities, List<TEntity> tempDeletionCache, DbContext context, EntityEqualityComparer<TEntity> entityComparer) {
      Dictionary<TEntity, TEntity> updatableEntities = new Dictionary<TEntity, TEntity>();
      IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);
      foreach(TEntity existingEntity in existingEntities) {
        /* Check if the entity already exists, by querying the context directly */
        Expression<Func<TEntity, bool>> queryById = EntityCastRemoverVisitor.Convert((TEntity t) => t.RecordId == existingEntity.RecordId);
        TEntity storedEntity = context.Set<TEntity>().SingleOrDefault(queryById);
        if(storedEntity != null) {
          /* If the entity is already marked for deletion, it can no longer be updated. */
          if(tempDeletionCache.Contains(existingEntity, entityComparer)) {
            throw new InvalidOperationException("Cannot update the entity since it already marked for deletion.");
          }

          if(this.updateCache.Contains(existingEntity, entityComparer)) {
            /* Retrieve the previous updated version of the entity from the cache */
            int indexOfEntity = this.updateCache.IndexOf(existingEntity, entityComparer);
            storedEntity = this.updateCache[indexOfEntity];
            updatableEntities.Add(existingEntity, storedEntity);
          }
          else {
            this.updateCache.Add(storedEntity);
            updatableEntities.Add(existingEntity, storedEntity);
          }
        }
        else {
          throw new InvalidOperationException("Could not find the entity in the internal cache.");
        }
      }

      return updatableEntities;
    }

    /// <summary>Inserts a new entity to the repository. This implementation does not acquire a write lock on the local storage and can therefore be 
    /// called from within a context that already has a write lock on the internal storage.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <returns>The entity with the most recent values.</returns>
    private TEntity AddEntityNoLock(TEntity entity, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      if(entity.RecordId > 0) {
        /* The entity already has an ID which suggests that it came from the original data source */
        if(this.deletionCache.Contains(entity, entityComparer)) {
          /* The entity has been marked for deletion, undelete it... */
          this.deletionCache.Remove(entity, entityComparer);
          /* ...and mark it as updated in case any of the fields have been altered. */
          return this.UpdateEntityNoLock(entity, dataSourceInfo);
        }
      }

      /* The entity is either new or came from another data source */
      /* Determine the new temporary ID for the entity */
      int newRecordId = -1;

      if(this.additionCache.Count > 0) {
        newRecordId = this.additionCache.Min(t => t.RecordId) - 1;
      }

      TEntity copyOfEntity = entity.CreateCopyOrClone();
      copyOfEntity.RecordId = newRecordId;

      /* Add it to the addition cache */
      this.additionCache.Add(copyOfEntity);

      if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
        return ((ICloneable)copyOfEntity).Clone() as TEntity;
      }
      else {
        entity.CopyFrom(copyOfEntity);
        return entity;
      }
    }

    /// <summary>Adds a collection of new entities to the repository. They are added to the addition cache until it is saved using the 
    /// <see cref="Repository{T}.SaveChanges()"/> method. A temporary (negative) RecordID is assigned to the entities. This will be reset when the 
    /// entity is saved. This implementation does not acquire a write lock on the local storage and can therefore be called from within a context that 
    /// already has a write lock on the internal storage.</summary>
    /// <param name="entities">The entities that must be added.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they were added to the repository.</returns>
    private IEnumerable<TEntity> AddEntitiesNoLock(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      /* Place the entities in a list to keep track of the entities that have been handled */
      List<TEntity> unhandledEntities = entities.ToList();

      DbContext context = this.SelectDbContext(dataSourceInfo);

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempDeletionCache = this.deletionCache.ToList();
      List<TEntity> tempUpdateCache = this.updateCache.ToList();
      List<TEntity> tempAdditionCache = this.additionCache.ToList();

      Dictionary<TEntity, TEntity> handledEntities = new Dictionary<TEntity, TEntity>();

      List<TEntity> updatedEntities = AddExistingEntities(entities, unhandledEntities, tempUpdateCache, tempDeletionCache, entityComparer, context, handledEntities);
      List<TEntity> addedEntities = AddNewEntities(unhandledEntities, tempAdditionCache, handledEntities);

      /* Replace the original caches to complete the 'transaction' */
      this.deletionCache = tempDeletionCache;
      this.updateCache = tempUpdateCache;
      this.additionCache = tempAdditionCache;

      if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
        return addedEntities.Concat(updatedEntities).Select(entity => ((ICloneable)entity).Clone() as TEntity).ToList();
      }
      else {
        Linq.Extensions.ForEach(handledEntities, kvp => kvp.Key.CopyFrom(kvp.Value));
        return entities;
      }
    }

    /// <summary>Deletes an entity from the repository. This implementation does not acquire a write lock on the local storage and can therefore be 
    /// called from within a context that already has a write lock on the internal storage.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    private void DeleteEntityNoLock(TEntity entity, DataSourceInfo dataSourceInfo) {
      DbContext context = this.SelectDbContext(dataSourceInfo);
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

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

        /* If the entity exists in the original data source and has not yet been marked for deletion, mark it now */
        Expression<Func<TEntity, bool>> queryById = EntityCastRemoverVisitor.Convert((TEntity t) => t.RecordId == entity.RecordId);
        TEntity existingEntity = context.Set<TEntity>().SingleOrDefault(queryById);
        if(existingEntity != null) {
          if(!this.deletionCache.Contains(existingEntity, entityComparer)) {
            this.deletionCache.Add(existingEntity);
          }
          else {
            throw new InvalidOperationException("Cannot delete the same entity more then once.");
          }
        }
        else {
          throw new InvalidOperationException("The entity seems to be new and can therefore not be deleted.");
        }
      }
    }

    /// <summary>Removes a collection of entities from the repository. Depending on the status of each entity, it is removed from the addition-cache 
    /// or it is added to the deletion-cache until it is saved using the <see cref="Repository{T}.SaveChanges()"/> method. This implementation does 
    /// not acquire a write lock on the local storage and can therefore be called from within a context that already has a write lock on the internal 
    /// storage.</summary>
    /// <param name="entities">The entities that must be removed.</param>
    /// <param name="dataSourceInfo">Information about the data source that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    private void DeleteEntitiesNoLock(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      DbContext context = this.SelectDbContext(dataSourceInfo);
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      IEnumerable<TEntity> addedEntities = entities.Where(e => e.RecordId < 0);
      IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempAdditionCache = this.additionCache.ToList();
      List<TEntity> tempDeletionCache = this.deletionCache.ToList();

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
          this.updateCache.Remove(existingEntity, entityComparer);
        }

        /* If the entity exists in the original data source and has not yet been marked for deletion, mark it now */
        Expression<Func<TEntity, bool>> queryById = EntityCastRemoverVisitor.Convert((TEntity t) => t.RecordId == existingEntity.RecordId);
        TEntity storedEntity = context.Set<TEntity>().SingleOrDefault(queryById);
        if(storedEntity != null) {
          if(!tempDeletionCache.Contains(storedEntity, entityComparer)) {
            tempDeletionCache.Add(storedEntity);
          }
          else {
            throw new InvalidOperationException("Cannot delete the same entity more then once.");
          }
        }
        else {
          throw new InvalidOperationException("The entity seems to be new and can therefore not be deleted.");
        }
      }

      /* Replace the original caches to complete the 'transaction' */
      this.additionCache = tempAdditionCache;
      this.deletionCache = tempDeletionCache;
    }

    /// <summary>Resets the local addition and deletion cache. This implementation does not acquire a write lock on the local storage and can therefore 
    /// be called from within a context that already has a write lock on the internal storage.</summary>
    private void ResetLocalCacheNoLock() {
      this.additionCache.Clear();
      this.updateCache.Clear();
      this.deletionCache.Clear();
    }
    #endregion

    #region Private inner classes
    /// <summary>Implements a custom <see cref="System.Linq.Expressions.ExpressionVisitor"/> that removes the generated Convert-operation from the expression.</summary>
    private sealed class EntityCastRemoverVisitor : System.Linq.Expressions.ExpressionVisitor {
      /// <summary>Converts the specified expression into an expression that can be used by the Entity Framework.</summary>
      /// <param name="expression">The expression that must be converted.</param>
      /// <returns>The converted expression.</returns>
      public static Expression<Func<TEntity, bool>> Convert(Expression<Func<TEntity, bool>> expression) {
        EntityCastRemoverVisitor visitor = new EntityCastRemoverVisitor();

        Expression visitedExpression = visitor.Visit(expression);

        return (Expression<Func<TEntity, bool>>)visitedExpression;
      }

      /// <summary>Visits the children of the <see cref="UnaryExpression"/>.</summary>
      /// <param name="node">The expression to visit.</param>
      /// <returns>The modified expression, if it or any subexpression was modified; otherwise, returns the original expression.</returns>
      protected override Expression VisitUnary(UnaryExpression node) {
        if(node.NodeType == ExpressionType.Convert && node.Type == typeof(IEntity<TEntity>)) {
          return node.Operand;
        }

        return base.VisitUnary(node);
      }
    }
    #endregion
  }
}
