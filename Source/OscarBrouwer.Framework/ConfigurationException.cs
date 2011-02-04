//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="ConfigurationException.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds a specific exceptiontype that can be used when an configuration-related exception occurs.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Enkoni.Framework {
  /// <summary>This exception can be thrown when a problem with the configuration has been discovered.</summary>
  [Serializable]
  public class ConfigurationException : Exception {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="ConfigurationException"/> class.</summary>
    public ConfigurationException()
      : base() {
    }

    /// <summary> Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified
    /// error message.</summary>
    /// <param name="message">The message that describes the error.</param>
    public ConfigurationException(string message)
      : base(message) {
    }

    /// <summary> Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified
    /// error message.</summary>
    /// <param name="configurationParameter">The name of the configurationparameter that causes the problem.</param>
    /// <param name="message">The message that describes the error.</param>
    public ConfigurationException(string configurationParameter, string message)
      : base(message) {
      this.ConfigurationParameter = configurationParameter;
    }

    /// <summary>Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified
    /// error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a 
    /// <see langword="null"/> reference if no inner exception is specified.</param>
    public ConfigurationException(string message, Exception innerException)
      : base(message, innerException) {
    }

    /// <summary>Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified
    /// error message and a reference to the inner exception that is the cause of this exception.</summary>
    /// <param name="configurationParameter">The name of the configurationparameter that causes the problem.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a 
    /// <see langword="null"/> reference if no inner exception is specified.</param>
    public ConfigurationException(string configurationParameter, string message,
      Exception innerException)
      : base(message, innerException) {
      this.ConfigurationParameter = configurationParameter;
    }

    /// <summary>Initializes a new instance of the <see cref="ConfigurationException"/> class with serialized  
    /// data.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the 
    /// exception being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source
    /// or destination.</param>
    /// <exception cref="ArgumentNullException">The info parameter is <see langword="null"/>.</exception>
    protected ConfigurationException(SerializationInfo info, StreamingContext context)
      : base(info, context) {
      if(info == null) {
        throw new ArgumentNullException("info");
      }

      this.ConfigurationParameter = info.GetString("ConfigurationParameter");
    }
    #endregion

    #region Properties
    /// <summary>Gets the name of the configurationparameter that caused the problem.</summary>
    public string ConfigurationParameter { get; private set; }
    #endregion

    #region Public methods
    /// <summary>Sets the <see cref="SerializationInfo"/> with information about the exception.</summary>
    /// <param name="info">The <see cref="SerializationInfo"/> that holds the serialized object data about the exception 
    /// being thrown.</param>
    /// <param name="context">The <see cref="StreamingContext"/> that contains contextual information about the source or 
    /// destination.</param>
    /// <exception cref="ArgumentNullException">The info parameter is <see langword="null"/>.</exception>
    [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
    public override void GetObjectData(SerializationInfo info, StreamingContext context) {
      if(info == null) {
        throw new ArgumentNullException("info");
      }

      base.GetObjectData(info, context);

      info.AddValue("ConfigurationParameter", this.ConfigurationParameter);
    }
    #endregion
  }
}
