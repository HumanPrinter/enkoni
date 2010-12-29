//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="ShallowLinqRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds a repository with limited capabilities.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Linq;

namespace OscarBrouwer.Framework.Entities.LinqToSql {
  /// <summary>This class extends the abstract <see cref="LinqRepository{TContract,TActual}"/> class and implements 
  /// some of the functionality using LINQ-to-SQL. The repository can be used for types that only need to be created, 
  /// but not saved or retrieved as this is done by the parent-type's repository.</summary>
  /// <typeparam name="TContract">The contract-type of the entity that is handled by this repository.</typeparam>
  /// <typeparam name="TActual">The actual-type of the entity that is handled by this repository.</typeparam>
  public class ShallowLinqRepository<TContract, TActual> : LinqRepository<TContract, TActual>
    where TContract : class
    where TActual : class, TContract, new() {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="ShallowLinqRepository{TContract,TActual}"/> class using
    /// the specified <see cref="DataContext"/>.</summary>
    /// <param name="dataContext">The datacontext that must be used to access the database.</param>
    public ShallowLinqRepository(DataContext dataContext)
      : base(dataContext) {
    }
    #endregion

    #region LinqRepository overrides
    /// <summary>Since addition of entities will be handled by the parent entity's repository, nothing is done here.
    /// </summary>
    /// <param name="entity">The entity that is to be added.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TContract AddEntityCore(TContract entity) {
      /* Adding will be done by the parent-entity's repository */
      return entity;
    }

    /// <summary>Since updating of entities will be handled by the parent entity's repository, nothing is done here.
    /// </summary>
    /// <param name="entity">The entity that is to be updated.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TContract UpdateEntityCore(TContract entity) {
      /* Updating will be done by the parent-entity's repository */
      return entity;
    }

    /// <summary>Since deletion of entities will be handled by the parent entity's repository, nothing is done here.
    /// </summary>
    /// <param name="entity">The entity that is to be deleted.</param>
    protected override void DeleteEntityCore(TContract entity) {
      /* Deleting will be done by the parent-entity's repository */
    }

    /// <summary>Since any changes to these entities will be handled by the parent entity's repository, nothing is done 
    /// here.</summary>
    protected override void SaveChangesCore() {
      /* Saving will be done by the parent-entity's repository */
    }

    /// <summary>Since retrieval of these entities is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override IEnumerable<TContract> FindAllCore() {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of these entities is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override IEnumerable<TContract> FindAllCore(Func<TContract, bool> expression) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override TContract FindFirstCore(Func<TContract, bool> expression) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <param name="defaultValue">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override TContract FindFirstCore(Func<TContract, bool> expression, TContract defaultValue) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override TContract FindSingleCore(Func<TContract, bool> expression) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <param name="defaultValue">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override TContract FindSingleCore(Func<TContract, bool> expression, TContract defaultValue) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }
    #endregion
  }
}
