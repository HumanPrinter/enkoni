using System;

namespace Enkoni.Framework {
  /// <summary>This exception can be thrown when a problem with the configuration has been discovered.</summary>
  public partial class ConfigurationException : Exception {
    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="ConfigurationException"/> class.</summary>
    public ConfigurationException()
      : base() {
    }

    /// <summary> Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified error message.</summary>
    /// <param name="message">The message that describes the error.</param>
    public ConfigurationException(string message)
      : base(message) {
    }

    /// <summary> Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified error message.</summary>
    /// <param name="configurationParameter">The name of the configuration parameter that causes the problem.</param>
    /// <param name="message">The message that describes the error.</param>
    public ConfigurationException(string configurationParameter, string message)
      : base(message) {
      this.ConfigurationParameter = configurationParameter;
    }

    /// <summary>Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified error message and a reference to the
    /// inner exception that is the cause of this exception.</summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> reference if no inner
    /// exception is specified.</param>
    public ConfigurationException(string message, Exception innerException)
      : base(message, innerException) {
    }

    /// <summary>Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified error message and a reference to the
    /// inner exception that is the cause of this exception.</summary>
    /// <param name="configurationParameter">The name of the configuration parameter that causes the problem.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a <see langword="null"/> reference if no inner
    /// exception is specified.</param>
    public ConfigurationException(string configurationParameter, string message,
      Exception innerException)
      : base(message, innerException) {
      this.ConfigurationParameter = configurationParameter;
    }

    #endregion

    #region Properties

    /// <summary>Gets the name of the configuration parameter that caused the problem.</summary>
    public string ConfigurationParameter { get; private set; }

    #endregion
  }
}
