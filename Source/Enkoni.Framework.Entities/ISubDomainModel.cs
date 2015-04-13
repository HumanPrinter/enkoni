using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class defines the public API of a class that represents the sub domain of the domain model.</summary>
  /// <typeparam name="T">The type to which this sub domain applies.</typeparam>
  public interface ISubDomainModel<T> where T : IEntity<T> {
    #region Public methods
    /// <summary>Creates an empty instance of type T.</summary>
    /// <returns>The created instance.</returns>
    T CreateEmptyEntity();

    /// <summary>Finds all the entities.</summary>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    IList<T> FindEntities();

    /// <summary>Finds all the entities.</summary>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    IList<T> FindEntities(string[] includePaths);

    /// <summary>Finds all the entities that match the specified specification.</summary>
    /// <param name="searchSpecification">The specification that describes the query that must be performed.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    IList<T> FindEntities(ISpecification<T> searchSpecification);

    /// <summary>Finds all the entities that match the specified expression.</summary>
    /// <param name="searchExpression">The expression that describes the query that must be performed.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    /// <remarks>This method has no support for orderby specifications and/or maximum result specifications. Use the overload version that takes a 
    /// <see cref="ISpecification{T}"/> if more detailed control is required.</remarks>
    IList<T> FindEntities(Expression<Func<T, bool>> searchExpression);

    /// <summary>Finds all the entities that match the specified expression.</summary>
    /// <param name="searchExpression">The expression that describes the query that must be performed.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    /// <remarks>This method has no support for orderby specifications and/or maximum result specifications. Use the overload version that takes a 
    /// <see cref="ISpecification{T}"/> if more detailed control is required.</remarks>
    IList<T> FindEntities(Expression<Func<T, bool>> searchExpression, string[] includePaths);

    /// <summary>Finds one entities that matches the specified specification.</summary>
    /// <param name="searchSpecification">The specification that describes the query that must be performed.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    T FindEntity(ISpecification<T> searchSpecification);

    /// <summary>Finds one entities that matches the specified expression.</summary>
    /// <param name="searchExpression">The expression that describes the query that must be performed.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    /// <remarks>This method has no support for orderby specifications and/or maximum result specifications. Use the overload version that takes a 
    /// <see cref="ISpecification{T}"/> if more detailed control is required.</remarks>
    T FindEntity(Expression<Func<T, bool>> searchExpression);

    /// <summary>Finds one entities that matches the specified expression.</summary>
    /// <param name="searchExpression">The expression that describes the query that must be performed.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    /// <remarks>This method has no support for orderby specifications and/or maximum result specifications. Use the overload version that takes a 
    /// <see cref="ISpecification{T}"/> if more detailed control is required.</remarks>
    T FindEntity(Expression<Func<T, bool>> searchExpression, string[] includePaths);

    /// <summary>Finds a single entity with the specified entity-ID.</summary>
    /// <param name="entityId">The ID of the entity that must be found.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    T FindEntityById(int entityId);

    /// <summary>Finds a single entity with the specified entity-ID.</summary>
    /// <param name="entityId">The ID of the entity that must be found.</param>
    /// <param name="includePaths">The dot-separated lists of related objects to return in the query results.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    T FindEntityById(int entityId, string[] includePaths);

    /// <summary>Validates the entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The results of the validation.</returns>
    ICollection<ValidationResult> ValidateEntity(T entity);

    /// <summary>Adds the specified entity to the domain. Before it is added, the entity is validated to ensure that only validated entities are 
    /// added in the domain.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <exception cref="ValidationException">The entity is invalid.</exception>
    /// <returns>The entity with the most recent values.</returns>
    T AddEntity(T entity);

    /// <summary>Updates the specified entity in the domain. Before it is updated, the entity is validated to ensure that only validated entities are 
    /// added in the domain.</summary>
    /// <param name="originalEntityId">The ID of the entity that must be updated.</param>
    /// <param name="updatedEntity">The entity that contains the new values.</param>
    /// <exception cref="ValidationException">The entity is invalid.</exception>
    /// <returns>The entity with the most recent values.</returns>
    T UpdateEntity(int originalEntityId, T updatedEntity);

    /// <summary>Removes the entity from the domain.</summary>
    /// <param name="originalEntityId">The ID of the entity that must be removed.</param>
    void DeleteEntity(int originalEntityId);
    #endregion
  }
}
