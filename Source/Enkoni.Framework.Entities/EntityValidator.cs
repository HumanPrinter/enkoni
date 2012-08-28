//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityValidator.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//    Holds the generic implementation of an entity validator.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

using EntLib = Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class defines the public API of a class that can validate entities.</summary>
  /// <typeparam name="T">The type of entity that must be validated.</typeparam>
  public class EntityValidator<T> where T : class {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="EntityValidator{T}"/> class.</summary>
    public EntityValidator() {
    }
    #endregion

    #region Public API
    /// <summary>Performs a shallow validation of the specified entity. This will only validate the consistency of the entity itself without looking 
    /// at the references to other entities or the underlying persistency.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The results of the validation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
    public EntLib.ValidationResults PerformShallowValidation(T entity) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      return this.PerformShallowValidationCore(entity);
    }

    /// <summary>Performs a deep validation of the specified entity. Besides performing a shallow validation, it may also look at the underlying
    /// persistency for instance to make sure that no uniqueness-rules are violated.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The results of the validation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
    public EntLib.ValidationResults PerformDeepValidation(T entity) {
      if(entity == null) {
        throw new ArgumentNullException("entity");
      }

      EntLib.ValidationResults results = this.PerformShallowValidation(entity);
      if(results.IsValid) {
        results = this.PerformDeepValidationCore(entity);
      }

      return results;
    }
    #endregion

    #region Extensibility methods
    /// <summary>Performs a shallow validation of the entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The result of the validation.</returns>
    protected virtual EntLib.ValidationResults PerformShallowValidationCore(T entity) {
      return EntLib.Validation.Validate<T>(entity, "shallow");
    }

    /// <summary>Performs a deep validation of the entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The result of the validation.</returns>
    protected virtual EntLib.ValidationResults PerformDeepValidationCore(T entity) {
      return EntLib.Validation.Validate<T>(entity, "deep");
    }
    #endregion
  }
}
