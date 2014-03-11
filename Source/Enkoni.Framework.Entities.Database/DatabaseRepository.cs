using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
    /// <summary>The collection of entities that are to be added to the datasource. </summary>
    private List<TEntity> additionCache;

    /// <summary>The collection of entities that are to be removed from the datasource.</summary>
    private List<TEntity> deletionCache;

    /// <summary>A lock that is used to synchronize access to the internal storage.</summary>
    private ReaderWriterLockSlim storageLock = new ReaderWriterLockSlim();
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="DatabaseRepository{TEntity}"/> class using the specified <see cref="DataSourceInfo"/>.
    /// </summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the database.</param>
    public DatabaseRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
      if(DatabaseSourceInfo.IsDbContextSpecified(dataSourceInfo)) {
        this.DbContext = DatabaseSourceInfo.SelectDbContext(dataSourceInfo);
      }

      /* Initializes the internal collections */
      this.additionCache = new List<TEntity>();
      this.deletionCache = new List<TEntity>();
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the DbContext that is used to access the database.</summary>
    protected DbContext DbContext { get; private set; }
    #endregion

    #region IDatabaseRepository methods
    /// <summary>Replaces the current DbContext with the specified one. The current DbContext is first disposed.</summary>
    /// <param name="dbContext">The new DbContext that must be used.</param>
    public void ReloadObjectContext(DbContext dbContext) {
      this.DbContext.Dispose();
      this.DbContext = dbContext;
    }
    #endregion

    #region Repository<T> overrides
    /// <summary>Submits all the changes to the database.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    protected override void SaveChangesCore(DataSourceInfo dataSourceInfo) {
      DbContext context = this.SelectDbContext(dataSourceInfo);

      using(TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required)) {
        try {
          this.storageLock.EnterWriteLock();

          foreach(TEntity addedEntity in this.additionCache) {
            context.Set<TEntity>().Add(addedEntity);
          }

          foreach(TEntity deletedEntity in this.deletionCache) {
            context.Set<TEntity>().Remove(deletedEntity);
          }

          context.SaveChanges();
          this.additionCache.Clear();
          this.deletionCache.Clear();
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
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The created entity.</returns>
    protected override TEntity CreateEntityCore(DataSourceInfo dataSourceInfo) {
      TEntity entity = new TEntity();
      return entity;
    }

    /// <summary>Inserts a new entity to the repository.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity AddEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      try {
        this.storageLock.EnterWriteLock();
        if(entity.RecordId > 0) {
          /* The entity already has an ID which suggests that it came from the original datasource */
          if(this.deletionCache.Contains(entity, entityComparer)) {
            /* The entity has been marked for deletion, undelete it... */
            this.deletionCache.Remove(entity, entityComparer);
            /* ...and mark it as updated in case any of the fields have been altered. */
            return this.UpdateEntityNoLock(entity, dataSourceInfo);
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
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Adds a collection of new entities to the repository. They are added to the addition cache untill it is saved using the 
    /// <see cref="Repository{T}.SaveChanges()"/> method. A temporary (negative) RecordID is assigned to the entities. This will be reset when the 
    /// entity is saved.</summary>
    /// <param name="entities">The entities that must be added.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they were added to the repository.</returns>
    protected override IEnumerable<TEntity> AddEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      /* Place the entities in a list to keep track of the entities that have been handled */
      List<TEntity> unhandledEntities = entities.ToList();

      DbContext context = this.SelectDbContext(dataSourceInfo);

      this.storageLock.EnterWriteLock();

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempDeletionCache = this.deletionCache.ToList();
      List<TEntity> tempAdditionCache = this.additionCache.ToList();

      try {
        if(entities.Any(e => e.RecordId > 0)) {
          List<TEntity> updatableEntities = new List<TEntity>();
          IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);
          ReferenceEqualityComparer<TEntity> referenceComparer = new ReferenceEqualityComparer<TEntity>();
          /* At least some of the entities already have an ID which suggests that they came from the original datasource */
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
              /* This is not very likely, otherwise how could the entity exist in the deleteion cache */
              throw new InvalidOperationException("Could not find the entity in the internal cache.");
            }
            else {
              storedEntity.CopyFrom(updatableEntity);
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
        this.additionCache = tempAdditionCache;

        return entities;
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
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
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
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
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
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity.RecordId == 0) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      try {
        this.storageLock.EnterWriteLock();
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
          /* If the entity exists in the original datasource and has not yet been marked for deletion, mark it now */
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
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Removes a collection of entities from the repository. Depending on the status of each entity, it is removed from the addition-cache 
    /// or it is added to the deletion-cache untill it is saved using the <see cref="Repository{T}.SaveChanges()"/> method.</summary>
    /// <param name="entities">The entities that must be removed.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    protected override void DeleteEntitiesCore(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      if(entities.Any(e => e.RecordId == 0)) {
        throw new InvalidOperationException("Cannot delete an entity whose identifier is zero.");
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);
      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      this.storageLock.EnterWriteLock();

      IEnumerable<TEntity> addedEntities = entities.Where(e => e.RecordId < 0);
      IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempAdditionCache = this.additionCache.ToList();
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
          /* If the entity exists in the original datasource and has not yet been marked for deletion, mark it now */
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
      finally {
        if(this.storageLock.IsWriteLockHeld) {
          this.storageLock.ExitWriteLock();
        }
      }
    }

    /// <summary>Finds all the available entities that match the specified expression.</summary>
    /// <param name="expression">The expression to which the entities must match.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="maximumResults">The maximum number of results that must be retrieved. Use '-1' to retrieve all results.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entities that match the specified expression.</returns>
    protected override IEnumerable<TEntity> FindAllCore(Expression<Func<TEntity, bool>> expression,
      SortSpecifications<TEntity> sortRules, int maximumResults, DataSourceInfo dataSourceInfo) {
      DbContext context = this.SelectDbContext(dataSourceInfo);

      /* First query the database directly (this will also populate the local cache) */
      expression = EntityCastRemoverVisitor.Convert(expression);
      IQueryable<TEntity> databaseQuery = context.Set<TEntity>().AsExpandable().Where(expression);

      /* Add the ordering to the query */
      databaseQuery = databaseQuery.OrderBy(sortRules);

      if(maximumResults != -1) {
        /* Take the maximum into account */
        databaseQuery = databaseQuery.Take(maximumResults);
      }

      /* Force the execution of the query */
      IEnumerable<TEntity> databaseData = databaseQuery.AsEnumerable().ToList();

      /* Then query the local cache */
      IEnumerable<TEntity> cachedData = context.Set<TEntity>().Local.Where(expression.Compile());

      /* Combine the databasedata and the local cache using the cache as the master (since it may contain unsaved updates) */
      IEqualityComparer<TEntity> comparer = new EntityEqualityComparer<TEntity>();
      IEnumerable<TEntity> result = cachedData.Union(databaseData, comparer);
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
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TEntity FindSingleCore(Expression<Func<TEntity, bool>> expression, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      expression = EntityCastRemoverVisitor.Convert(expression);
      Func<TEntity, bool> compiledExpression = expression.Compile();
      /* First, query the addition cache */
      TEntity result = this.additionCache.SingleOrDefault(compiledExpression, null);

      if(result != null) {
        return result;
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);

      /* Then, query the deletion cache */
      result = this.deletionCache.SingleOrDefault(compiledExpression, null);
      if(result != null) {
        /* An entity matching the expression, has been marked for deletion, check the local cache */
        TEntity cachedResult = context.Set<TEntity>().Local.SingleOrDefault(compiledExpression, null);
        if(cachedResult != null) {
          /* An entity that matches the expression exists both in the deletion cache and the local cache. */
          if(cachedResult.RecordId == result.RecordId) {
            /* Both results denote the same entity. Since it was marked for deletion, return the default value */
            return defaultValue;
          }
          else {
            /* The local cahche takes precedence over the deletion cache (perhaps an update caused an other entity to also match the expression), 
             * return the cached result */
            if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
              return ((ICloneable)cachedResult).Clone() as TEntity;
            }
            else {
              return cachedResult;
            }
          }
        }
        else {
          /* The entity only lives in the deletion cache. Since it was marked for deletion, return the default value */
          return defaultValue;
        }
      }

      /* No entity in either the addition cache or the deletion cache matches the expression, continue the search */
      /* First query the database directly (this will also populate the local cache) */
      TEntity databaseData = context.Set<TEntity>().AsExpandable().SingleOrDefault(expression, null);
      /* Then query the local cache */
      TEntity cachedData = context.Set<TEntity>().Local.SingleOrDefault(compiledExpression, null);

      if(databaseData == null && cachedData == null) {
        /* Both the database ans cache drew a blank, there is no such object */
        return defaultValue;
      }
      else {
        /* The object exists in both the database and the cache. Return the cached-value because it may contain unsaved updates */
        if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
          return ((ICloneable)cachedData).Clone() as TEntity;
        }
        else {
          return cachedData;
        }
      }
    }

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TEntity FindFirstCore(Expression<Func<TEntity, bool>> expression, SortSpecifications<TEntity> sortRules,
      DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      expression = EntityCastRemoverVisitor.Convert(expression);
      Func<TEntity, bool> compiledExpression = expression.Compile();

      /* This repository defines that any unsaved additions take precedence over previously saved entities, therefore the the addition cache is 
       * queried first */
      TEntity result = this.additionCache.OrderBy(sortRules).FirstOrDefault(compiledExpression, null);

      if(result != null) {
        return result;
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);

      /* There is no entity in the addition cache matching the expression, continue */
      /* First query the database directly (this will also populate the local cache) */
      IQueryable<TEntity> databaseQuery = context.Set<TEntity>().AsExpandable();

      /* Add the ordering to the query */
      if(sortRules != null) {
        databaseQuery = databaseQuery.OrderBy(sortRules);
      }

      TEntity databaseData = databaseQuery.FirstOrDefault(expression, null);

      /* Then query the local cache */
      IEnumerable<TEntity> cacheQuery = context.Set<TEntity>().Local;
      cacheQuery = cacheQuery.OrderBy(sortRules);

      TEntity cachedData = cacheQuery.FirstOrDefault(compiledExpression, null);

      if(databaseData == null && cachedData == null) {
        /* Both the database ans cache drew a blank, there is no such object */
        return defaultValue;
      }
      else {
        /* The entity exists in the database (or local cache). Check if it has been marked for deletion */
        if(this.deletionCache.Contains(cachedData, new EntityEqualityComparer<TEntity>())) {
          /* The entity has been marked for deletion */
          return defaultValue;
        }
        else {
          if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
            return ((ICloneable)cachedData).Clone() as TEntity;
          }
          else {
            return cachedData;
          }
        }
      }
    }

    /// <summary>Creates a LIKE-expression using the specified field and searchpattern.</summary>
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
    /// <summary>Selects the DbContext that must be used. If the specified DataSourceInfo contains a valid DbContext, it is used; otherwise the value 
    /// of the property 'DbContext' is used.</summary>
    /// <param name="dataSourceInfo">Any information regarding the database that is used as datasource.</param>
    /// <returns>The DbContext that must be used.</returns>
    protected virtual DbContext SelectDbContext(DataSourceInfo dataSourceInfo) {
      if(DatabaseSourceInfo.IsDbContextSpecified(dataSourceInfo)) {
        return DatabaseSourceInfo.SelectDbContext(dataSourceInfo);
      }
      else {
        return this.DbContext;
      }
    }
    #endregion

    #region Private helper methods
    /// <summary>Updates the repository with the changes made to <paramref name="entity"/>. This implementation does not aquire a write lock on the
    /// local storage and can therefore be called from within a context that already has a write lock on the internal storage.</summary>
    /// <param name="entity">The entity that was updated.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
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
          this.additionCache[indexOfEntity] = entity;
          return entity;
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

          existingEntity.CopyFrom(entity);
          if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
            return ((ICloneable)existingEntity).Clone() as TEntity;
          }
          else {
            return existingEntity;
          }
        }
        else {
          throw new InvalidOperationException("The entity seems to be new and can therefore not be updated.");
        }
      }
    }

    /// <summary>Updates a collection of entities in the repository. This implementation does not aquire a write lock on the local storage and can 
    /// therefore be called from within a context that already has a write lock on the internal storage.</summary>
    /// <param name="entities">The entities that contain the updated values.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage. This parameter is not used.
    /// </param>
    /// <returns>The entities as they are stored in the repository.</returns>
    private IEnumerable<TEntity> UpdateEntitiesNoLock(IEnumerable<TEntity> entities, DataSourceInfo dataSourceInfo) {
      if(entities.Any(e => e.RecordId == 0)) {
        throw new InvalidOperationException("Cannot update an entity whose identifier is zero.");
      }

      DbContext context = this.SelectDbContext(dataSourceInfo);

      EntityEqualityComparer<TEntity> entityComparer = new EntityEqualityComparer<TEntity>();

      IEnumerable<TEntity> addedEntities = entities.Where(e => e.RecordId < 0);
      IEnumerable<TEntity> existingEntities = entities.Where(e => e.RecordId > 0);

      /* Make a copy of the caches. That way, if any thing goes wrong all the changes can be made undone */
      List<TEntity> tempAdditionCache = this.additionCache.ToList();
      List<TEntity> tempDeletionCache = this.deletionCache.ToList();

      List<TEntity> newlyAddedEntities = new List<TEntity>();

      foreach(TEntity addedEntity in addedEntities) {
        /* If the entity is marked for addition, update the entity in the addition cache */
        if(tempAdditionCache.Contains(addedEntity, entityComparer)) {
          int indexOfEntity = tempAdditionCache.IndexOf(addedEntity, entityComparer);
          tempAdditionCache[indexOfEntity] = addedEntity;
          newlyAddedEntities.Add(addedEntity);
        }
        else {
          throw new InvalidOperationException("Could not find the entity in the addition-cache.");
        }
      }

      /* Create a collection for all the entities that can be updated 
        * Key: updated entity
        * Value: stored entity */
      Dictionary<TEntity, TEntity> updatableEntities = new Dictionary<TEntity, TEntity>();
      foreach(TEntity existingEntity in existingEntities) {
        /* Check if the entity already exists, by querying the context directly */
        Expression<Func<TEntity, bool>> queryById = EntityCastRemoverVisitor.Convert((TEntity t) => t.RecordId == existingEntity.RecordId);
        TEntity storedEntity = context.Set<TEntity>().SingleOrDefault(queryById);
        if(storedEntity != null) {
          /* If the entity is already marked for deletion, it can no longer be updated. */
          if(tempDeletionCache.Contains(existingEntity, entityComparer)) {
            throw new InvalidOperationException("Cannot update the entity since it already marked for deletion.");
          }

          updatableEntities.Add(existingEntity, storedEntity);
        }
        else {
          throw new InvalidOperationException("Could not find the entity in the internal cache.");
        }
      }

      /* Everything checks out, start the update-process */
      foreach(KeyValuePair<TEntity, TEntity> kvp in updatableEntities) {
        kvp.Value.CopyFrom(kvp.Key);
      }

      /* Replace the original caches to complete the 'transaction' */
      this.additionCache = tempAdditionCache;
      this.deletionCache = tempDeletionCache;

      if(this.SelectCloneDataSourceItems(dataSourceInfo)) {
        return newlyAddedEntities.Concat(updatableEntities.Select(kvp => ((ICloneable)kvp.Value).Clone() as TEntity)).ToList();
      }
      else {
        return newlyAddedEntities.Concat(updatableEntities.Select(kvp => kvp.Value)).ToList();
      }
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
