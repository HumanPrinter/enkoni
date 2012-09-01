//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidationException.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Holds a specific exceptiontype that can be used when a validation did not succeed.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Validation {
  /// <summary>The Exception that is thrown when an object cannot be properly validated.</summary>
  [Serializable]
  public class ValidationException : Exception {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="ValidationException"/> class.</summary>
    public ValidationException()
      : base() {
    }

    /// <summary>Initializes a new instance of the <see cref="ValidationException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    public ValidationException(string message)
      : this(message, (ValidationResults)null) {
    }

    /// <summary>Initializes a new instance of the <see cref="ValidationException"/> class.</summary>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="validationResults">The results of the validation.</param>
    public ValidationException(string message, ValidationResults validationResults)
      : base(message) {
      this.ValidationResults = validationResults;
    }

    /// <summary>Initializes a new instance of the <see cref="ValidationException"/> class.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> reference (Nothing in 
    /// Visual Basic) if no inner exception is specified.</param>
    public ValidationException(string message, Exception innerException)
      : this(message, null, innerException) {
    }

    /// <summary>Initializes a new instance of the <see cref="ValidationException"/> class.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="validationResults">The results of the validation.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> reference (Nothing in 
    /// Visual Basic) if no inner exception is specified.</param>
    public ValidationException(string message, ValidationResults validationResults, Exception innerException)
      : base(message, innerException) {
      this.ValidationResults = validationResults;
    }

    /// <summary>Initializes a new instance of the <see cref="ValidationException"/> class.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected ValidationException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
      if(info == null) {
        throw new ArgumentNullException("info");
      }

      this.ValidationResults = info.GetValue("ValidationResults", typeof(ValidationResults)) as ValidationResults;
    }
    #endregion

    #region Properties
    /// <summary>Gets the results of the validation.</summary>
    public ValidationResults ValidationResults { get; private set; }
    #endregion

    #region Methods
    /// <summary>Sets the <paramref name="info"/> with information about the exception.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      if(info == null) {
        throw new ArgumentNullException("info");
      }

      base.GetObjectData(info, context);

      info.AddValue("ValidationResults", this.ValidationResults);
    }
    #endregion
  }
}
