using System;
using System.Runtime.Serialization;

namespace Enkoni.Framework {
  /// <summary>This exception can be thrown when a method or class definition is used with an illegal type parameter.</summary>
  [Serializable]
  public partial class InvalidTypeParameterException {
    /// <summary>Initializes a new instance of the <see cref="InvalidTypeParameterException"/> class with serialized data.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected InvalidTypeParameterException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
    }
  }
}
