//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ShallowDatabaseRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository with limited functionality.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Enkoni.Framework.Entities {
  /// <summary>This class extends the <see cref="DatabaseRepository{TEntity}"/> class and implements some of the functionality using the Entity 
  /// Framework. The repository can be used for types that only need to be created, but not saved or retrieved as this is done by the parent-type's
  /// repository.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class ShallowDatabaseRepository<TEntity> : DatabaseRepository<TEntity>
    where TEntity : class, IEntity<TEntity>, new() {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="ShallowDatabaseRepository{TEntity}"/> class using the specified
    /// <see cref="DataSourceInfo"/>.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the database.</param>
    public ShallowDatabaseRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
    }
    #endregion

    #region ShallowDatabaseRepository overrides
    /// <summary>Since addition of entities will be handled by the parent entity's repository, nothing is done here.</summary>
    /// <param name="entity">The entity that is to be added.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity AddEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      /* Adding will be done by the parent-entity's repository */
      return entity;
    }

    /// <summary>Since updating of entities will be handled by the parent entity's repository, nothing is done here.</summary>
    /// <param name="entity">The entity that is to be updated.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TEntity UpdateEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      /* Updating will be done by the parent-entity's repository */
      return entity;
    }

    /// <summary>Since deletion of entities will be handled by the parent entity's repository, nothing is done here.</summary>
    /// <param name="entity">The entity that is to be deleted.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    protected override void DeleteEntityCore(TEntity entity, DataSourceInfo dataSourceInfo) {
      /* Deleting will be done by the parent-entity's repository */
    }

    /// <summary>Since any changes to these entities will be handled by the parent entity's repository, nothing is done here.</summary>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    protected override void SaveChangesCore(DataSourceInfo dataSourceInfo) {
      /* Saving will be done by the parent-entity's repository */
    }

    /// <summary>Since retrieval of these entities is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="maximumResults">The maximum number of results that must be retrieved. Use '-1' to retrieve all results.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always, since this operation is not supported by this type of repository.</exception>
    protected override IEnumerable<TEntity> FindAllCore(Func<TEntity, bool> expression, SortSpecifications<TEntity> sortRules, int maximumResults,
      DataSourceInfo dataSourceInfo) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <param name="sortRules">The specification of the sortrules that must be applied. Use <see langword="null"/> to ignore the ordering.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always, since this operation is not supported by this type of repository.</exception>
    protected override TEntity FindFirstCore(Func<TEntity, bool> expression, SortSpecifications<TEntity> sortRules, DataSourceInfo dataSourceInfo,
      TEntity defaultValue) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }

    /// <summary>Since retrieval of the entity is handled by the parent entity's repository, this method throws a
    /// <see cref="NotSupportedException"/>.</summary>
    /// <param name="expression">The parameter is not used.</param>
    /// <param name="dataSourceInfo">Information about the datasource that may not have been set at an earlier stage.</param>
    /// <param name="defaultValue">The parameter is not used.</param>
    /// <returns>Not applicable.</returns>
    /// <exception cref="NotSupportedException">Always, since this operation is not supported by this type of repository.</exception>
    protected override TEntity FindSingleCore(Func<TEntity, bool> expression, DataSourceInfo dataSourceInfo, TEntity defaultValue) {
      throw new NotSupportedException("This repository cannot be used to retrieve entities.");
    }
    #endregion
  }
}
