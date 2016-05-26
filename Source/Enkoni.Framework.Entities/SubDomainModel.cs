using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

using Enkoni.Framework.Linq;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class defines the public API of a class that represents the sub domain of the domain model.</summary>
  /// <typeparam name="T">The type to which this sub domain applies.</typeparam>
  public abstract class SubDomainModel<T> : ISubDomainModel<T> where T : IEntity<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="SubDomainModel{T}"/> class.</summary>
    protected SubDomainModel() {
    }

    /// <summary>Initializes a new instance of the <see cref="SubDomainModel{T}"/> class.</summary>
    /// <param name="parentDomainModel">The parent model that gives access to the other subdomains.</param>
    [Obsolete("Since the DomainModel-class is marked obsolete, so is this constructor. Use the parameterless constructor instead")]
    protected SubDomainModel(DomainModel parentDomainModel) {
      this.ParentDomainModel = parentDomainModel;
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the parent domain model.</summary>
    [Obsolete("Since the DomainModel-class is marked obsolete, so is this property. It will be removed in a future version of this framework")]
    protected virtual DomainModel ParentDomainModel { get; private set; }
    #endregion

    #region Public methods
    /// <summary>Creates an empty instance of type T.</summary>
    /// <returns>The created instance.</returns>
    public T CreateEmptyEntity() {
      return this.CreateEmptyEntityCore();
    }

    /// <summary>Finds all the entities.</summary>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    public IList<T> FindEntities() {
      return this.FindEntitiesCore(Specification.All<T>());
    }

    /// <summary>Finds all the entities.</summary>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    public IList<T> FindEntities(string[] includePaths) {
      Guard.ArgumentIsNotNull(includePaths, nameof(includePaths));

      if(includePaths.Length == 0 || includePaths.Any(path => string.IsNullOrEmpty(path))) {
        throw new ArgumentException("The include paths cannot be empty ans cannot contain empty or null values.", "includePaths");
      }

      ISpecification<T> specification = Specification.All<T>();
      includePaths.ForEach(path => specification.Include(path));
      return this.FindEntitiesCore(specification);
    }

    /// <summary>Finds all the entities that match the specified specification.</summary>
    /// <param name="searchSpecification">The specification that describes the query that must be performed.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    public IList<T> FindEntities(ISpecification<T> searchSpecification) {
      Guard.ArgumentIsNotNull(searchSpecification, nameof(searchSpecification));
      
      return this.FindEntitiesCore(searchSpecification);
    }

    /// <summary>Finds all the entities that match the specified expression.</summary>
    /// <param name="searchExpression">The expression that describes the query that must be performed.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    /// <remarks>This method has no support for order by specifications and/or maximum result specifications. Use the overload version that takes a 
    /// <see cref="ISpecification{T}"/> if more detailed control is required.</remarks>
    public IList<T> FindEntities(Expression<Func<T, bool>> searchExpression) {
      Guard.ArgumentIsNotNull(searchExpression, nameof(searchExpression));
      
      return this.FindEntitiesCore(Specification.Lambda(searchExpression));
    }

    /// <summary>Finds all the entities that match the specified expression.</summary>
    /// <param name="searchExpression">The expression that describes the query that must be performed.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    /// <remarks>This method has no support for order by specifications and/or maximum result specifications. Use the overload version that takes a 
    /// <see cref="ISpecification{T}"/> if more detailed control is required.</remarks>
    public IList<T> FindEntities(Expression<Func<T, bool>> searchExpression, string[] includePaths) {
      Guard.ArgumentIsNotNull(searchExpression, nameof(searchExpression));
      Guard.ArgumentIsNotNull(includePaths, nameof(includePaths));

      if(includePaths.Length == 0 || includePaths.Any(path => string.IsNullOrEmpty(path))) {
        throw new ArgumentException("The include paths cannot be empty ans cannot contain empty or null values.", "includePaths");
      }

      ISpecification<T> specification = Specification.Lambda(searchExpression);
      includePaths.ForEach(path => specification.Include(path));
      return this.FindEntitiesCore(specification);
    }

    /// <summary>Finds one entities that matches the specified specification.</summary>
    /// <param name="searchSpecification">The specification that describes the query that must be performed.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    public T FindEntity(ISpecification<T> searchSpecification) {
      Guard.ArgumentIsNotNull(searchSpecification, nameof(searchSpecification));
      
      return this.FindEntityCore(searchSpecification);
    }

    /// <summary>Finds one entities that matches the specified expression.</summary>
    /// <param name="searchExpression">The expression that describes the query that must be performed.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    /// <remarks>This method has no support for order by specifications and/or maximum result specifications. Use the overload version that takes a 
    /// <see cref="ISpecification{T}"/> if more detailed control is required.</remarks>
    public T FindEntity(Expression<Func<T, bool>> searchExpression) {
      Guard.ArgumentIsNotNull(searchExpression, nameof(searchExpression));

      return this.FindEntityCore(Specification.Lambda(searchExpression));
    }

    /// <summary>Finds one entities that matches the specified expression.</summary>
    /// <param name="searchExpression">The expression that describes the query that must be performed.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    /// <remarks>This method has no support for order by specifications and/or maximum result specifications. Use the overload version that takes a 
    /// <see cref="ISpecification{T}"/> if more detailed control is required.</remarks>
    public T FindEntity(Expression<Func<T, bool>> searchExpression, string[] includePaths) {
      Guard.ArgumentIsNotNull(searchExpression, nameof(searchExpression));
      Guard.ArgumentIsNotNull(includePaths, nameof(includePaths));

      if(includePaths.Length == 0 || includePaths.Any(path => string.IsNullOrEmpty(path))) {
        throw new ArgumentException("The include paths cannot be empty ans cannot contain empty or null values.", "includePaths");
      }

      ISpecification<T> specification = Specification.Lambda(searchExpression);
      includePaths.ForEach(path => specification.Include(path));

      return this.FindEntityCore(specification);
    }

    /// <summary>Finds a single entity with the specified entity-ID.</summary>
    /// <param name="entityId">The ID of the entity that must be found.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    public T FindEntityById(int entityId) {
      return this.FindEntityByIdCore(entityId);
    }

    /// <summary>Finds a single entity with the specified entity-ID.</summary>
    /// <param name="entityId">The ID of the entity that must be found.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    public T FindEntityById(int entityId, string[] includePaths) {
      Guard.ArgumentIsNotNull(includePaths, nameof(includePaths));

      if(includePaths.Length == 0 || includePaths.Any(path => string.IsNullOrEmpty(path))) {
        throw new ArgumentException("The include paths cannot be empty ans cannot contain empty or null values.", "includePaths");
      }

      ISpecification<T> specification = Specification.Lambda((T t) => t.RecordId == entityId);
      includePaths.ForEach(path => specification.Include(path));

      return this.FindEntityCore(specification);
    }

    /// <summary>Validates the entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The results of the validation.</returns>
    public ICollection<ValidationResult> ValidateEntity(T entity) {
      Guard.ArgumentIsNotNull(entity, nameof(entity));

      return this.ValidateEntityCore(entity);
    }

    /// <summary>Adds the specified entity to the domain. Before it is added, the entity is validated to ensure that only validated entities are 
    /// added in the domain.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <exception cref="ValidationException">The entity is invalid.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T AddEntity(T entity) {
      Guard.ArgumentIsNotNull(entity, nameof(entity));

      return this.AddEntityCore(entity);
    }

    /// <summary>Updates the specified entity in the domain. Before it is updated, the entity is validated to ensure that only validated entities are 
    /// added in the domain.</summary>
    /// <param name="originalEntityId">The ID of the entity that must be updated.</param>
    /// <param name="updatedEntity">The entity that contains the new values.</param>
    /// <exception cref="ValidationException">The entity is invalid.</exception>
    /// <returns>The entity with the most recent values.</returns>
    public T UpdateEntity(int originalEntityId, T updatedEntity) {
      Guard.ArgumentIsNotNull(updatedEntity, nameof(updatedEntity));

      T existingEntity = this.FindEntityById(originalEntityId);
      if(existingEntity == null) {
        throw new ArgumentException("The entity cannot be updated because it does not yet exist in the model", "originalEntityId");
      }
      else if(!object.ReferenceEquals(existingEntity, updatedEntity)) {
        existingEntity.CopyFrom(updatedEntity);
        return this.UpdateEntityCore(existingEntity);
      }
      else {
        return this.UpdateEntityCore(updatedEntity);
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
    /// <param name="specification">The specification that describes the query that must be performed.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    protected abstract IList<T> FindEntitiesCore(ISpecification<T> specification);

    /// <summary>Finds one entities that matches the specified specification.</summary>
    /// <param name="specification">The specification that describes the query that must be performed.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    protected abstract T FindEntityCore(ISpecification<T> specification);

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

    /// <summary>Finds one entities with the specified entity-ID.</summary>
    /// <param name="entityId">The ID of the entity that must be found.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    protected virtual T FindEntityByIdCore(int entityId) {
      return this.FindEntityCore(Specification.Lambda((T t) => t.RecordId == entityId));
    }

    /// <summary>Validates the entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The results of the validation.</returns>
    protected virtual ICollection<ValidationResult> ValidateEntityCore(T entity) {
      ValidationContext validationContext = new ValidationContext(entity, null, null);
      ICollection<ValidationResult> validationResults = new List<ValidationResult>();
      Validator.TryValidateObject(entity, validationContext, validationResults, true);
      return validationResults;
    }
    #endregion
  }
}
