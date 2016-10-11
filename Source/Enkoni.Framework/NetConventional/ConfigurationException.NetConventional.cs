using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Enkoni.Framework {
  /// <summary>This exception can be thrown when a problem with the configuration has been discovered.</summary>
  [Serializable]
  public partial class ConfigurationException {
    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="ConfigurationException"/> class with serialized data.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    /// <exception cref="ArgumentNullException">The info parameter is <see langword="null"/>.</exception>
    protected ConfigurationException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
      Guard.ArgumentIsNotNull(info, nameof(info));

      this.ConfigurationParameter = info.GetString("ConfigurationParameter");
    }

    #endregion

    #region Public methods

    /// <summary>Sets the <see cref="SerializationInfo"/> with information about the exception.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or destination.</param>
    /// <exception cref="ArgumentNullException">The info parameter is <see langword="null"/>.</exception>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      Guard.ArgumentIsNotNull(info, nameof(info));

      base.GetObjectData(info, context);

      info.AddValue("ConfigurationParameter", this.ConfigurationParameter);
    }

    #endregion
  }
}
