//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSourceInfo.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines a class that contains information about a filebased datasource that is used by the FileRepository.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This class can be used by the <see cref="FileRepository{TEntity}"/> or any of its descendants to retrieve valuable 
  /// information about the file that is to be used. This class is added for improved usability of the DataSourceInfo in 
  /// combination with the FileRepository.</summary>
  public class FileSourceInfo : DataSourceInfo {
    #region Public constants
    /// <summary>Defines the key that is used to store and retrieve the FileInfo that points to the desired file.</summary>
    public const string SourceFileInfoKey = "SourceFileInfo";

    /// <summary>Defines the key that is used to store and retrieve the boolean that indicates wheter or not to monitor the sourcefile.</summary>
    public const string MonitorSourceFileKey = "MonitorSourceFile";
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using a default value of
    /// <see langword="null"/> for the SourceFileInfo and <see langword="true"/> for the MonitorSourceFile.</summary>
    public FileSourceInfo()
      : this((FileInfo)null, true) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using the specified 
    /// <see cref="FileInfo"/> and <see langword="bool"/> values.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
    public FileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile)
      : base() {
      this.SourceFileInfo = sourceFileInfo;
      this.MonitorSourceFile = monitorSourceFile;
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using the specified default values.
    /// If the default values do not specify the FileInfo and monitor-flag using the correct key and/or type, the value 
    /// <see langword="null"/> and <see langword="true"/> will be used.</summary>
    /// <param name="defaultValues">The default values that are to be used.</param>
    public FileSourceInfo(Dictionary<string, object> defaultValues)
      : base(defaultValues) {
      /* Check if the dictionary contains the reserved key and, if so, the key denotes a value of the correct type */
      if(defaultValues.ContainsKey(SourceFileInfoKey) && !(defaultValues[SourceFileInfoKey] is FileInfo)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.SourceFileInfo = null;
      }

      if(defaultValues.ContainsKey(MonitorSourceFileKey) && !(defaultValues[MonitorSourceFileKey] is bool)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.MonitorSourceFile = true;
      }
    }
    #endregion

    #region Public properties
    /// <summary>Gets or sets the FileInfo that points to the file that is used as datasource.</summary>
    public FileInfo SourceFileInfo {
      get { return (FileInfo)this[SourceFileInfoKey]; }
      set { this[SourceFileInfoKey] = value; }
    }

    /// <summary>Gets or sets a value indicating whether the sourcefile must be monitored for any changes.</summary>
    public bool MonitorSourceFile {
      get { return (bool)this[MonitorSourceFileKey]; }
      set { this[MonitorSourceFileKey] = value; }
    }
    #endregion

    #region Public static methods
    /// <summary>Determines if the source FileInfo is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns><see langword="true"/> if the FileInfo is defined; <see langword="false"/> otherwise.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public static bool IsSourceFileInfoSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(SourceFileInfoKey);
    }

    /// <summary>Selects the source FileInfo from the specified datasource information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns>The FileInfo that is stored in the datasource information or <see langword="null"/> if the FileInfo could
    /// not be found.</returns>
    public static FileInfo SelectSourceFileInfo(DataSourceInfo dataSourceInfo) {
      if(IsSourceFileInfoSpecified(dataSourceInfo)) {
        return dataSourceInfo[SourceFileInfoKey] as FileInfo;
      }
      else {
        return null;
      }
    }

    /// <summary>Determines if the monitor-flag is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns><see langword="true"/> if the flag is defined; <see langword="false"/> otherwise.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public static bool IsMonitorSourceFileSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(MonitorSourceFileKey);
    }

    /// <summary>Selects the monitor-flag from the specified datasource information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns>The monitor-flag that is stored in the datasource information or <see langword="true"/> if the flag could
    /// not be found.</returns>
    public static bool SelectMonitorSourceFile(DataSourceInfo dataSourceInfo) {
      if(IsMonitorSourceFileSpecified(dataSourceInfo)) {
        return (bool)dataSourceInfo[MonitorSourceFileKey];
      }
      else {
        return true;
      }
    }
    #endregion

    #region Public methods
    /// <summary>Determines if the source FileInfo is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the FileInfo is defined; <see langword="false"/> otherwise.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public bool IsSourceFileInfoSpecified() {
      return this.IsValueSpecified(SourceFileInfoKey);
    }

    /// <summary>Determines if the monitor-flag is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the flag is defined; <see langword="false"/> otherwise.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public bool IsMonitorSourceFileSpecified() {
      return this.IsValueSpecified(MonitorSourceFileKey);
    }
    #endregion
  }
}
