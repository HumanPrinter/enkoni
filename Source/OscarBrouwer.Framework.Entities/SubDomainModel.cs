//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="SubDomainModel.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a SubDomainModel type.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Enkoni.Framework.Validation;

using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class defines the public API of a class that represents the subdomain of the domain model.
  /// </summary>
  /// <typeparam name="T">The type to which this subdomain applies.</typeparam>
  public abstract class SubDomainModel<T> where T : IEntity<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="SubDomainModel{T}"/> class.</summary>
    /// <param name="parentDomainModel">The parent model that gives access to the other subdomains.</param>
    protected SubDomainModel(DomainModel parentDomainModel) {
      this.ParentDomainModel = parentDomainModel;
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the parent domainmodel.</summary>
    protected virtual DomainModel ParentDomainModel { get; private set; }
    #endregion

    #region Public methods
    /// <summary>Creates an empty instance of type T.</summary>
    /// <returns>The created instance.</returns>
    public T CreateEmptyEntity() {
      return this.CreateEmptyEntityCore();
    }

    /// <summary>Finds all the entities that match the specified specification.</summary>
    /// <param name="searchSpecification">The specification that desribes the query that must be performed.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    public IList<T> FindEntities(ISpecification<T> searchSpecification) {
      if(searchSpecification == null) {
        throw new ArgumentNullException("searchSpecification");
      }

      return this.FindEntitiesCore(searchSpecification);
    }

    /// <summary>Finds one entities that matches the specified specification.</summary>
    /// <param name="searchSpecification">The specification that desribes the query that must be performed.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    public T FindEntity(ISpecification<T> searchSpecification) {
      if(searchSpecification == null) {
        throw new ArgumentNullException("searchSpecification");
      }

      return this.FindEntityCore(searchSpecification);
    }

    /// <summary>Finds one entities with the specified entity-ID.</summary>
    /// <param name="entityId">The ID of the entity that must be found.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    public T FindEntityById(int entityId) {
      return this.FindEntityCore(Specification.Lambda<T>(t => t.RecordId == entityId));
    }

    /// <summary>Adds the specified entity to the domain. Before it is added, the entity is validated to ensure that 
    /// only validated entities are added in the domain.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <exception cref="ValidationException">The entity is invalid.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T AddEntity(T entity) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      ValidationResults results = this.ValidateEntity(entity);
      if(!results.IsValid) {
        throw new ValidationException("The entity cannot be added to the model, because it did not pass the validation", results);
      }
      else {
        return this.AddEntityCore(entity);
      }
    }

    /// <summary>Updates the specified entity in the domain. Before it is updated, the entity is validated to ensure that 
    /// only validated entities are added in the domain.</summary>
    /// <param name="originalEntityId">The ID of the entity that must be updated.</param>
    /// <param name="updatedEntity">The entity that contains the new values.</param>
    /// <exception cref="ValidationException">The entity is invalid.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T UpdateEntity(int originalEntityId, T updatedEntity) {
      if(updatedEntity == null) {
        throw new ArgumentNullException("updatedEntity");
      }

      ValidationResults results = this.ValidateEntity(updatedEntity);
      if(!results.IsValid) {
        throw new ValidationException("The entity cannot be updated in the model, because it did not pass the validation.", results);
      }
      else {
        T existingEntity = this.FindEntityById(originalEntityId);
        if(existingEntity == null) {
          throw new ArgumentException("The entity cannot be updated because it does not yet exist in the model", "originalEntityId");
        }
        else {
          existingEntity.CopyFrom(updatedEntity);
          return this.UpdateEntityCore(existingEntity);
        }
      }
    }

    /// <summary>Removes the entity from the domain.</summary>
    /// <param name="originalEntityId">The ID of the entity that must be removed.</param>
    public void DeleteEntity(int originalEntityId) {
      T existingEntity = this.FindEntityById(originalEntityId);
      if(existingEntity == null) {
        throw new ArgumentException("The entity cannot be deleted because it does not yet exist in the model", "originalEntityId");
      }
      else {
        this.DeleteEntityCore(existingEntity);
      }
    }
    #endregion

    #region Extensibility methods
    /// <summary>Creates an empty instance of type T.</summary>
    /// <returns>The created instance.</returns>
    protected abstract T CreateEmptyEntityCore();

    /// <summary>Finds all the entities that match the specified specification.</summary>
    /// <param name="specification">The specification that desribes the query that must be performed.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    protected abstract IList<T> FindEntitiesCore(ISpecification<T> specification);

    /// <summary>Finds one entities that matches the specified specification.</summary>
    /// <param name="specification">The specification that desribes the query that must be performed.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    protected abstract T FindEntityCore(ISpecification<T> specification);

    /// <summary>Validates the entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The results of the validation.</returns>
    protected abstract ValidationResults ValidateEntity(T entity);

    /// <summary>Adds the entity to the domain.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected abstract T AddEntityCore(T entity);

    /// <summary>Updates the entity in the domain.</summary>
    /// <param name="entity">The entity that must is updated.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected abstract T UpdateEntityCore(T entity);

    /// <summary>Removes the entity from the domain.</summary>
    /// <param name="entity">The entity that must be removed.</param>
    protected abstract void DeleteEntityCore(T entity);
    #endregion
  }
}
