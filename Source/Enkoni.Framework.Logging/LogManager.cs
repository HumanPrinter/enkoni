//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="LogManager.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines the class that manages and gives access to a logger-instance.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Enkoni.Framework.Logging {
  /// <summary>Manages and gives access to a <see cref="Logger"/> instance that is capable of logging messages through the Logging Application Block
  /// which is part of the Microsoft Enterprise Library.</summary>
  public static class LogManager {
    #region Private static variables
    /// <summary>The actual logger that is used.</summary>
    private static LogWriter logWriter = AssignLogWriter();
    #endregion

    #region Public static methods
    /// <summary>Creates a new instance of <see cref="Logger"/> which can be used to send logmessages to the Logging Application Block.</summary>
    /// <returns>The created <see cref="Logger"/>.</returns>
    public static Logger CreateLogger() {
      return new Logger(logWriter);
    }
    #endregion

    #region Private static helper methods
    /// <summary>Gets a configured <see cref="LogWriter"/> from the Enterprise Library configuration. If there is no logger configured, 
    /// <see langword="null"/> is returned.</summary>
    /// <returns>The <see cref="LogWriter"/> that is configured in the configuration of <see langword="null"/> if no logger is configured.</returns>
    private static LogWriter AssignLogWriter() {
      IEnumerable<LogWriter> writers = EnterpriseLibraryContainer.Current.GetAllInstances<LogWriter>();
      return writers.FirstOrDefault();
    }
    #endregion
  }
}
