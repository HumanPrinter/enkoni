using System.Collections.Generic;
using System.Linq;

using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Enkoni.Framework.Logging {
  /// <summary>Manages and gives access to a <see cref="Logger"/> instance that is capable of logging messages through the Logging Application Block
  /// which is part of the Microsoft Enterprise Library.</summary>
  public static class LogManager {
    #region Public static methods
    /// <summary>Creates a new instance of <see cref="Logger"/> which can be used to send log messages to the Logging Application Block.</summary>
    /// <returns>The created <see cref="Logger"/>.</returns>
    public static Logger CreateLogger() {
      IEnumerable<LogWriter> writers = EnterpriseLibraryContainer.Current.GetAllInstances<LogWriter>();
      LogWriter logWriter = writers.FirstOrDefault();
      return new Logger(logWriter);
    }
    #endregion
  }
}
