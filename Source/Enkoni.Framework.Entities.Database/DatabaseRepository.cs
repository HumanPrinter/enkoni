//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that uses the Entity Framework to communicate with a database.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Enkoni.Framework.Linq;

using LinqKit;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the functionality using the Entity 
  /// Framework.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class DatabaseRepository<TEntity> : Repository<TEntity>, IDatabaseRepository
    where TEntity : class, IEntity<TEntity>, new() {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="DatabaseRepository{TEntity}"/> class using the specified <see cref="DataSourceInfo"/>.
    /// </summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the database.</param>
    public DatabaseRepository(DataSourceInfo dataSourceInfo)
      : base() {
      if(DatabaseSourceInfo.IsDbContextSpecified(dataSourceInfo)) {
        this.DbContext = DatabaseSourceInfo.SelectDbContext(dataSourceInfo);
      }
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
      this.SelectDbContext(dataSourceInfo).SaveChanges();
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
      this.SelectDbContext(dataSourceInfo).Set<TEntity>().Add(entity);
      return entity;
    }

    /// <summary>Updates the repository with the changes made to <paramref name="entity"/>. Since the entity framework already monitors the state of 
    /// entities, no additional functionality is required. This method is therefore empty.</summary>
    /// <param name="entity">The entity that was updated.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity UpdateEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      Expression<Func<TEntity, bool>> selectExpression = t => t.RecordId == entity.RecordId;
      TEntity existingEntity = this.FindSingleCore(selectExpression, dataSourceInfo, null);
      if(existingEntity != null) {
        existingEntity.CopyFrom(entity);
      }
      else {
        throw new InvalidOperationException("The entity seems to be new and can therefore not be updated.");
      }
      
      return existingEntity;
    }

    /// <summary>Deletes an entity from the repository.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      Expression<Func<TEntity, bool>> selectExpression = t => t.RecordId == entity.RecordId;
      TEntity existingEntity = this.FindSingleCore(selectExpression, dataSourceInfo, null);
      if(existingEntity != null) {
        this.SelectDbContext(dataSourceInfo).Set<TEntity>().Remove(existingEntity);
      }
      else {
        throw new InvalidOperationException("The entity seems to be new and can therefore not be deleted.");
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
      /* First query the database directly (this will also populate the local cache) */
      IQueryable<TEntity> databaseQuery = this.SelectDbContext(dataSourceInfo).Set<TEntity>().AsExpandable().Where(expression);

      /* Add the ordering to the query */
      databaseQuery = databaseQuery.OrderBy(sortRules);
            
      if(maximumResults != -1) {
        /* Take the maximum into account */
        databaseQuery = databaseQuery.Take(maximumResults);
      }
      
      /* Force the execution of the query */
      IEnumerable<TEntity> databaseData = databaseQuery.AsEnumerable().ToList();

      /* Then query the local cache */
      IEnumerable<TEntity> cachedData = this.SelectDbContext(dataSourceInfo).Set<TEntity>().Local.Where(expression.Compile());
      
      /* Combine the databasedata and the local cache using the cache as the master (since it may contain unsaved updates) */
      /* IMPORTANT: It is possible that the combined collection contains an unsaved deletion. There is no way to detect this without losing the 
       * optimization of limiting the results retrieved directly from the database*/
      IEnumerable<TEntity> result = cachedData.Union(databaseData);

      result = result.OrderBy(sortRules);
      
      if(maximumResults != -1) {
        /* Take the maximum into account */
        result = result.Take(maximumResults);
      }

      return result;
    }

    /// <summary>Finds a single entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TEntity FindSingleCore(Expression<Func<TEntity, bool>> expression, DataSourceInfo dataSourceInfo, 
      TEntity defaultValue) {
      /* First query the database directly (this will also populate the local cache) */
      TEntity databaseData = this.SelectDbContext(dataSourceInfo).Set<TEntity>().AsExpandable().SingleOrDefault(expression, defaultValue);
      /* Then query the local cache */
      TEntity cachedData = this.SelectDbContext(dataSourceInfo).Set<TEntity>().Local.SingleOrDefault(expression.Compile(), defaultValue);

      if(databaseData == null && cachedData == null) {
        /* Both the database ans cache drew a blank, there is no such object */
        return null;
      }
      else if(databaseData != null && cachedData != null) {
        /* The object exists in both the database and the cache. Return the cached-value because it may contain unsaved updates */
        return cachedData;
      }
      else if(databaseData == null && cachedData != null) {
        /* The object only exists in the cache. Return the cached value because it most likely is an unsaved addition */
        return cachedData;
      }
      else {
        /* The object only seems to live in the database. If it was a new record (not yet cached), it would have been in the cache after the 
         * database-query. Since it isn't, it most likely is an unsaved deletion therefore 'null' is returned. */
        return null;
      }
    }

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TEntity FindFirstCore(Expression<Func<TEntity, bool>> expression, 
      SortSpecifications<TEntity> sortRules, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      /* First query the database directly (this will also populate the local cache) */
      IQueryable<TEntity> databaseQuery = this.SelectDbContext(dataSourceInfo).Set<TEntity>().AsExpandable();

      /* Add the ordering to the query */
      databaseQuery = databaseQuery.OrderBy(sortRules);

      TEntity databaseData = databaseQuery.FirstOrDefault(expression, defaultValue);
      
      /* Then query the local cache */
      IEnumerable<TEntity> cacheQuery = this.SelectDbContext(dataSourceInfo).Set<TEntity>().Local;
      cacheQuery = cacheQuery.OrderBy(sortRules);

      TEntity cachedData = cacheQuery.FirstOrDefault(expression.Compile(), defaultValue);

      if(databaseData == null && cachedData == null) {
        /* Both the database ans cache drew a blank, there is no such object */
        return null;
      }
      else if(databaseData != null && cachedData != null) {
        /* The object exists in both the database and the cache. Return the cached-value because it may contain unsaved updates */
        return cachedData;
      }
      else if(databaseData == null && cachedData != null) {
        /* The object only exists in the cache. Return the cached value because it most likely is an unsaved addition */
        return cachedData;
      }
      else {
        /* The object only seems to live in the database. If it was a new record (not yet cached), it would have been in the cache after the 
         * database-query. Since it isn't, it most likely is an unsaved deletion therefore 'null' is returned. */
        return null;
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
  }
}
