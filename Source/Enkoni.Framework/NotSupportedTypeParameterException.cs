//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="NotSupportedTypeParameterException.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a specific exceptiontype that can be used when an unsupported type parameter is used.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;

namespace Enkoni.Framework {
  /// <summary>This exception can be thrown when a method or class definition is called with an unsupported typeparameter.</summary>
  [Serializable]
  public class NotSupportedTypeParameterException : Exception {
    /// <summary>Initializes a new instance of the <see cref="NotSupportedTypeParameterException"/> class.</summary>
    public NotSupportedTypeParameterException()
      : base() {
    }

    /// <summary> Initializes a new instance of the <see cref="NotSupportedTypeParameterException"/> class with a specified errormessage.</summary>
    /// <param name="message">The message that describes the error.</param>
    public NotSupportedTypeParameterException(string message)
      : base(message) {
    }

    /// <summary>Initializes a new instance of the <see cref="NotSupportedTypeParameterException"/> class with a specified errormessage and a 
    /// reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> reference if no inner 
    /// exception is specified.</param>
    public NotSupportedTypeParameterException(string message, Exception innerException)
      : base(message, innerException) {
    }

    /// <summary>Initializes a new instance of the <see cref="NotSupportedTypeParameterException"/> class with serialized data.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected NotSupportedTypeParameterException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
    }
  }
}
