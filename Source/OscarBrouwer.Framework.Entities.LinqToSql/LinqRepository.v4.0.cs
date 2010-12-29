//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="LinqRepository.v4.0.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds a repository that uses Linq-to-SQL.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.Linq.SqlClient;
using System.Linq;

using OscarBrouwer.Framework.Linq;

namespace OscarBrouwer.Framework.Entities.LinqToSql {
  /// <summary>This abstract class extends the abstract <see cref="Repository{T}"/> class and implements some of the 
  /// functionality using LINQ-to-SQL.</summary>
  /// <typeparam name="TContract">The contract-type of the entity that is handled by this repository.</typeparam>
  /// <typeparam name="TActual">The actual-type of the entity that is handled by this repository.</typeparam>
  public class LinqRepository<TContract, TActual> : Repository<TContract>, ILinqRepository
    where TContract : class
    where TActual : class, TContract, new() {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="LinqRepository{TContract,TActual}"/> class using the 
    /// specified <see cref="DataContext"/>.</summary>
    /// <param name="dataContext">The datacontext that must be used to access the database.</param>
    public LinqRepository(DataContext dataContext)
      : base() {
      this.DataContext = dataContext;
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the DataContext that is used to access the database.</summary>
    protected DataContext DataContext { get; private set; }

    /// <summary>Gets the wildcard that is used to match a single character.</summary>
    protected override string SinglePositionWildcard {
      get { return "_"; }
    }

    /// <summary>Gets the wildcard that is used to match zero or more character.</summary>
    protected override string MultiplePositionWildcard {
      get { return "%"; }
    }
    #endregion

    #region ILinqRepository methods
    /// <summary>Replaces the current DataContext with the specified one. The current DataContext is first disposed.
    /// </summary>
    /// <param name="dataContext">The new DataContext that must be used.</param>
    public void ReloadDataContext(DataContext dataContext) {
      this.DataContext.Dispose();
      this.DataContext = dataContext;
    }
    #endregion

    #region Repository<T> overrides
    /// <summary>Submits all the changes to the database.</summary>
    protected override void SaveChangesCore() {
      this.DataContext.SubmitChanges(ConflictMode.FailOnFirstConflict);
    }

    /// <summary>Creates a new entity of type <typeparamref name="TContract"/>. This is done by calling the default
    /// constructor of <typeparamref name="TActual"/>.</summary>
    /// <returns>The created entity.</returns>
    protected override TContract CreateEntityCore() {
      TContract entity = new TActual();
      return entity;
    }

    /// <summary>Inserts a new entity to the repository.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    /// <returns>The entity with the most recent values.</returns>
    protected override TContract AddEntityCore(TContract entity) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      TActual castedEntity = entity as TActual;
      this.DataContext.GetTable<TActual>().InsertOnSubmit(castedEntity);
      return castedEntity;
    }

    /// <summary>Updates the repository with the changes made to <paramref name="entity"/>. Since LINQ-to-SQL already
    /// monitors the state of entities, no additional functionality is required. This method is therefore empty.
    /// </summary>
    /// <param name="entity">The entity that was updated.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TContract UpdateEntityCore(TContract entity) {
      /* By default, LINQ-objects do not require any additional logic to update the values */
      return entity;
    }

    /// <summary>Deletes an entity from the repository.</summary>
    /// <param name="entity">The entity that must be deleted.</param>
    /// <exception cref="ArgumentNullException">If <paramref name="entity"/> is <see langword="null"/>.</exception>
    protected override void DeleteEntityCore(TContract entity) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      TActual castedEntity = entity as TActual;
      this.DataContext.GetTable<TActual>().DeleteOnSubmit(castedEntity);
    }

    /// <summary>Creates an expression that can be used to perform a 'Like' operation.</summary>
    /// <returns>The created expression.</returns>
    protected override Func<string, string, bool> CreateLikeExpressionCore() {
      Func<string, string, bool> expression = new Func<string, string, bool>(SqlMethods.Like);
      return expression;
    }

    /// <summary>Finds all the entities of type <typeparamref name="TContract"/>.</summary>
    /// <returns>All the available entities.</returns>
    protected override IEnumerable<TContract> FindAllCore() {
      return this.DataContext.GetTable<TActual>();
    }

    /// <summary>Finds all the available entities that match the specified expression.</summary>
    /// <param name="expression">The expression to which the entities must match.</param>
    /// <returns>The entities that match the specified expression.</returns>
    protected override IEnumerable<TContract> FindAllCore(Func<TContract, bool> expression) {
      return this.DataContext.GetTable<TActual>().Where(expression);
    }

    /// <summary>Finds a single entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <returns>The found entity.</returns>
    protected override TContract FindSingleCore(Func<TContract, bool> expression) {
      return this.DataContext.GetTable<TActual>().Single(expression);
    }

    /// <summary>Finds a single entity that matches the expression. If no result was found, the specified default-value
    /// is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TContract FindSingleCore(Func<TContract, bool> expression, TContract defaultValue) {
      return this.DataContext.GetTable<TActual>().SingleOrDefault(expression, defaultValue);
    }

    /// <summary>Finds the first entity that matches the expression.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <returns>The found entity.</returns>
    protected override TContract FindFirstCore(Func<TContract, bool> expression) {
      return this.DataContext.GetTable<TActual>().First(expression);
    }

    /// <summary>Finds the first entity that matches the expression. If no result was found, the specified default-value
    /// is returned.</summary>
    /// <param name="expression">The expression to which the entity must match.</param>
    /// <param name="defaultValue">The value that will be returned when no match was found.</param>
    /// <returns>The found entity or <paramref name="defaultValue"/> if there was no result.</returns>
    protected override TContract FindFirstCore(Func<TContract, bool> expression, TContract defaultValue) {
      return this.DataContext.GetTable<TActual>().FirstOrDefault(expression, defaultValue);
    }
    #endregion
  }
}
