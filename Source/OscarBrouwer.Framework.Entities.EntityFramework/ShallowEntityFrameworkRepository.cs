//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="ShallowEntityFrameworkRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository with limited functionality.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace OscarBrouwer.Framework.Entities.EntityFramework {
  /// <summary>This class extends the abstract <see cref="EntityFrameworkRepository{TEntity}"/> class and implements some of
  /// the functionality using the Entity Framework. The repository can be used for types that only need to be created, but
  /// not saved or retrieved as this is done by the parent-type's repository.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class ShallowEntityFrameworkRepository<TEntity> : EntityFrameworkRepository<TEntity>
    where TEntity : class, new() {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="ShallowEntityFrameworkRepository{TEntity}"/> class using the
    /// specified <see cref="DbContext"/>.</summary>
    /// <param name="dbContext">The DbContext that must be used to access the database.</param>
    public ShallowEntityFrameworkRepository(DbContext dbContext)
      : base(dbContext) {
    }
    #endregion

    #region EntityFrameworkRepository overrides
    /// <summary>Since addition of entities will be handled by the parent entity's repository, nothing is done here.
    /// </summary>
    /// <param name="entity">The entity that is to be added.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity AddEntityCore(TEntity entity) {
      /* Adding will be done by the parent-entity's repository */
      return entity;
    }

    /// <summary>Since updating of entities will be handled by the parent entity's repository, nothing is done here.
    /// </summary>
    /// <param name="entity">The entity that is to be updated.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity UpdateEntityCore(TEntity entity) {
      /* Updating will be done by the parent-entity's repository */
      return entity;
    }

    /// <summary>Since deletion of entities will be handled by the parent entity's repository, nothing is done here.
    /// </summary>
    /// <param name="entity">The entity that is to be deleted.</param>
    protected override void DeleteEntityCore(TEntity entity) {
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
    protected override IEnumerable<TEntity> FindAllCore() {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of these entities is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override IEnumerable<TEntity> FindAllCore(Func<TEntity, bool> expression) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <param name="defaultValue">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, TEntity defaultValue) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <param name="defaultValue">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always.</exception>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, TEntity defaultValue) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }
    #endregion
  }
}
