using System;

namespace Enkoni.Framework {
  /// <summary>This exception can be thrown when a method or class definition is used with an illegal type parameter.</summary>
  public partial class InvalidTypeParameterException : Exception {
    /// <summary>Initializes a new instance of the <see cref="InvalidTypeParameterException"/> class.</summary>
    public InvalidTypeParameterException()
      : base() {
    }

    /// <summary> Initializes a new instance of the <see cref="InvalidTypeParameterException"/> class with a specified error message.</summary>
    /// <param name="message">The message that describes the error.</param>
    public InvalidTypeParameterException(string message)
      : base(message) {
    }

    /// <summary>Initializes a new instance of the <see cref="InvalidTypeParameterException"/> class with a specified error message and a reference to
    /// the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> reference if no inner
    /// exception is specified.</param>
    public InvalidTypeParameterException(string message, Exception innerException)
      : base(message, innerException) {
    }
  }
}
