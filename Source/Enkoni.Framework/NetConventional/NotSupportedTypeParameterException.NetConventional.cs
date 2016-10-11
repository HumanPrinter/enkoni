using System;
using System.Runtime.Serialization;

namespace Enkoni.Framework {
  /// <summary>This exception can be thrown when a method or class definition is called with an unsupported type parameter.</summary>
  [Serializable]
  public partial class NotSupportedTypeParameterException {
    /// <summary>Initializes a new instance of the <see cref="NotSupportedTypeParameterException"/> class with serialized data.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    protected NotSupportedTypeParameterException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
    }
  }
}
