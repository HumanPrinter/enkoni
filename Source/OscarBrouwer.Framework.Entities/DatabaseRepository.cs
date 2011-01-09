//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that uses the Entity Framework to communicate with a database.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using OscarBrouwer.Framework.Linq;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the 
  /// functionality using the Entity Framework.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class DatabaseRepository<TEntity> : Repository<TEntity>, IDatabaseRepository
    where TEntity : class, new() {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="DatabaseRepository{TEntity}"/> class using the specified
    /// <see cref="DataSourceInfo"/>.</summary>
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

    /// <summary>Gets the wildcard that is used to match a single character.</summary>
    protected override string SinglePositionWildcard {
      get { return "_"; }
    }

    /// <summary>Gets the wildcard that is used to match zero or more character.</summary>
    protected override string MultiplePositionWildcard {
      get { return "%"; }
    }
    #endregion

    #region IDatabaseRepository methods
    /// <summary>Replaces the current DbContext with the specified one. The current DbContext is first disposed.
    /// </summary>
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

    /// <summary>Creates a new entity of type <typeparamref name="TEntity"/>. This is done by calling the default
    /// constructor of <typeparamref name="TEntity"/>.</summary>
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
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      this.SelectDbContext(dataSourceInfo).Set<TEntity>().Add(entity);
      return entity;
    }

    /// <summary>Updates the repository with the changes made to <paramref name="entity"/>. Since the entity framework 
    /// already monitors the state of entities, no additional functionality is required. This method is therefore empty.
    /// </summary>
    /// <param name="entity">The entity that was updated.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity UpdateEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      /* By default, EntityFramework-objects do not require any additional logic to update the values */
      return entity;
    }

    /// <summary>Deletes an entity from the repository.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      this.SelectDbContext(dataSourceInfo).Set<TEntity>().Remove(entity);
    }

    /// <summary>Creates an expression that can be used to perform a 'Like' operation.</summary>
    /// <returns>The created expression.</returns>
    protected override Func<string, string, bool> CreateLikeExpressionCore() {
      Func<string, string, bool> expression = (field, pattern) => field.Contains(pattern);
      return expression;
    }

    /// <summary>Finds all the entities of type <typeparamref name="TEntity"/>.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>All the available entities.</returns>
    protected override IEnumerable<TEntity> FindAllCore(DataSourceInfo dataSourceInfo) {
      return this.SelectDbContext(dataSourceInfo).Set<TEntity>();
    }

    /// <summary>Finds all the available entities that match the specified expression.</summary>
    /// <param name="expression">The expression to which the entities must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entities that match the specified expression.</returns>
    protected override IEnumerable<TEntity> FindAllCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo) {
      return this.SelectDbContext(dataSourceInfo).Set<TEntity>().Where(expression);
    }

    /// <summary>Finds a single entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The found entity.</returns>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo) {
      return this.SelectDbContext(dataSourceInfo).Set<TEntity>().Single(expression);
    }

    /// <summary>Finds a single entity that matches the expression. If no result was found, the specified default-value
    /// is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      return this.SelectDbContext(dataSourceInfo).Set<TEntity>().SingleOrDefault(expression, defaultValue);
    }

    /// <summary>Finds the first entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The found entity.</returns>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo) {
      return this.SelectDbContext(dataSourceInfo).Set<TEntity>().First(expression);
    }

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value
    /// is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      return this.SelectDbContext(dataSourceInfo).Set<TEntity>().FirstOrDefault(expression, defaultValue);
    }
    #endregion

    #region Protected overridable helper methods
    /// <summary>Selects the DbContext that must be used. If the specified DataSourceInfo contains a valid DbContext, it is
    /// used; otherwise the value of the property 'DbContext' is used.</summary>
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
