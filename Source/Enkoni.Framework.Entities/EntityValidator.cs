using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
    /// <summary>Performs a validation of the specified entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <returns>The results of the validation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
    public ICollection<ValidationResult> PerformValidation(T entity) {
      return this.PerformValidation(entity, false);
    }

    /// <summary>Performs a validation of the specified entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <param name="throwOnError">Indicates whether an exception must be thrown in case of a validation violation.</param>
    /// <returns>The results of the validation.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="entity"/> is null.</exception>
    /// <exception cref="ValidationException"><paramref name="throwOnError"/> is <see langword="true"/> and a property of <paramref name="entity"/> 
    /// was found not to be valid.</exception>
    public ICollection<ValidationResult> PerformValidation(T entity, bool throwOnError) {
      Guard.ArgumentIsNotNull(entity, nameof(entity));
      
      ICollection<ValidationResult> results = this.PerformValidationCore(entity, throwOnError);
      
      return results;
    }
    #endregion

    #region Extensibility methods
    /// <summary>Performs a validation of the entity.</summary>
    /// <param name="entity">The entity that must be validated.</param>
    /// <param name="throwOnError">Indicates whether an exception must be thrown in case of a validation violation.</param>
    /// <returns>The result of the validation. If no violations were found, an empty collection is returned.</returns>
    /// <exception cref="ValidationException"><paramref name="throwOnError"/> is <see langword="true"/> and a property of <paramref name="entity"/> 
    /// was found not to be valid.</exception>
    protected virtual ICollection<ValidationResult> PerformValidationCore(T entity, bool throwOnError) {
      ValidationContext validationContext = new ValidationContext(entity, null, null);

      if(throwOnError) {
        Validator.ValidateObject(entity, validationContext, true);
        return new List<ValidationResult>();
      }
      else {
        ICollection<ValidationResult> validationResults = new List<ValidationResult>();
        Validator.TryValidateObject(entity, validationContext, validationResults, true);
        return validationResults;
      }
    }
    #endregion
  }
}
