//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="FileSourceInfo.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines a class that contains information about a filebased datasource that is used by the FileRepository.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Enkoni.Framework.Entities {
  /// <summary>This class can be used by the <see cref="FileRepository{TEntity}"/> or any of its descendants to retrieve valuable information about 
  /// the file that is to be used. This class is added for improved usability of the DataSourceInfo in combination with the FileRepository.</summary>
  public class FileSourceInfo : DataSourceInfo {
    #region Public constants
    /// <summary>Defines the key that is used to store and retrieve the FileInfo that points to the desired file.</summary>
    public const string SourceFileInfoKey = "SourceFileInfo";

    /// <summary>Defines the key that is used to store and retrieve the boolean that indicates wheter or not to monitor the sourcefile.</summary>
    public const string MonitorSourceFileKey = "MonitorSourceFile";

    /// <summary>Defines the key that is used to store and retrieve the numeric value that is used to determine if a change in the sourcefile is 
    /// finished.</summary>
    public const string ChangeCompleteTimeoutKey = "ChangeCompleteTimeout";

    /// <summary>Defines the key that is used to store and retrieve the encoding of the source file.</summary>
    public const string SourceFileEncodingKey = "SourceFileEncoding";

    /// <summary>The default value for the <see cref="MonitorSourceFile"/> that will be used when no custom value has been 
    /// specified.</summary>
    public const bool DefaultMonitorSourceFile = true;

    /// <summary>The default value for the <see cref="ChangeCompleteTimeout"/> that will be used when no custom value has 
    /// been specified.</summary>
    public const int DefaultChangeCompleteTimeout = 3000;

    /// <summary>The default value for the <see cref="SourceFileEncoding"/> that will be used when no custom value has been
    /// specified.</summary>
    public static readonly Encoding DefaultSourceFileEncoding = Encoding.UTF8;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using default values for the properties.</summary>
    public FileSourceInfo()
      : this((FileInfo)null, DefaultMonitorSourceFile, FileSourceInfo.DefaultChangeCompleteTimeout, FileSourceInfo.DefaultSourceFileEncoding,
      DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using default values for the properties.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    public FileSourceInfo(FileInfo sourceFileInfo)
      : this(sourceFileInfo, DefaultMonitorSourceFile, FileSourceInfo.DefaultChangeCompleteTimeout, FileSourceInfo.DefaultSourceFileEncoding,
      DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using default values for the properties.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the datasource should be cloned or not.</param>
    public FileSourceInfo(FileInfo sourceFileInfo, bool cloneDataSourceItems)
      : this(sourceFileInfo, DefaultMonitorSourceFile, FileSourceInfo.DefaultChangeCompleteTimeout, FileSourceInfo.DefaultSourceFileEncoding,
      cloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using default values for the properties.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="sourceFileEncoding">The encoding of the sourcefile.</param>
    public FileSourceInfo(FileInfo sourceFileInfo, Encoding sourceFileEncoding)
      : this(sourceFileInfo, DefaultMonitorSourceFile, FileSourceInfo.DefaultChangeCompleteTimeout, sourceFileEncoding,
      DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using default values for the properties.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="sourceFileEncoding">The encoding of the sourcefile.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the datasource should be cloned or not.</param>
    public FileSourceInfo(FileInfo sourceFileInfo, Encoding sourceFileEncoding, bool cloneDataSourceItems)
      : this(sourceFileInfo, DefaultMonitorSourceFile, FileSourceInfo.DefaultChangeCompleteTimeout, sourceFileEncoding, cloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using the specified <see cref="FileInfo"/>, 
    /// <see langword="bool"/> and <see langword="int"/> values.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
    /// <param name="changeCompleteTimeout">The timeout that is used to determine if a filechange has completed or not.</param>
    public FileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile, int changeCompleteTimeout)
      : this(sourceFileInfo, monitorSourceFile, changeCompleteTimeout, FileSourceInfo.DefaultSourceFileEncoding,
      DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using the specified <see cref="FileInfo"/>, 
    /// <see langword="bool"/> and <see langword="int"/> values.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
    /// <param name="changeCompleteTimeout">The timeout that is used to determine if a filechange has completed or not.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the datasource should be cloned or not.</param>
    public FileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile, int changeCompleteTimeout, bool cloneDataSourceItems)
      : this(sourceFileInfo, monitorSourceFile, changeCompleteTimeout, FileSourceInfo.DefaultSourceFileEncoding, cloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using the specified <see cref="FileInfo"/>, 
    /// <see langword="bool"/> and <see langword="int"/> values.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
    /// <param name="changeCompleteTimeout">The timeout that is used to determine if a filechange has completed or not.</param>
    /// <param name="sourceFileEncoding">The encoding of the sourcefile.</param>
    public FileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile, int changeCompleteTimeout, Encoding sourceFileEncoding)
      : this(sourceFileInfo, monitorSourceFile, changeCompleteTimeout, sourceFileEncoding, DataSourceInfo.DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using the specified <see cref="FileInfo"/>, 
    /// <see langword="bool"/> and <see langword="int"/> values.</summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
    /// <param name="changeCompleteTimeout">The timeout that is used to determine if a filechange has completed or not.</param>
    /// <param name="sourceFileEncoding">The encoding of the sourcefile.</param>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the datasource should be cloned or not.</param>
    public FileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile, int changeCompleteTimeout, Encoding sourceFileEncoding,
      bool cloneDataSourceItems)
      : base(cloneDataSourceItems) {
      this.SourceFileInfo = sourceFileInfo;
      this.MonitorSourceFile = monitorSourceFile;
      this.ChangeCompleteTimeout = changeCompleteTimeout;
      this.SourceFileEncoding = sourceFileEncoding;
    }

    /// <summary>Initializes a new instance of the <see cref="FileSourceInfo"/> class using the specified default values. If the default values do 
    /// not specify supported properties using the correct key and/or type, the default values will be used.</summary>
    /// <param name="defaultValues">The default values that are to be used.</param>
    public FileSourceInfo(Dictionary<string, object> defaultValues)
      : base(defaultValues) {
      /* Check if the dictionary contains the reserved key and, if so, the key denotes a value of the correct type */
      if(defaultValues.ContainsKey(SourceFileInfoKey) && !(defaultValues[SourceFileInfoKey] is FileInfo)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.SourceFileInfo = null;
      }

      if(defaultValues.ContainsKey(MonitorSourceFileKey) && !(defaultValues[MonitorSourceFileKey] is bool)) {
        /* The key is present, but the value is of the wrong type; use 'true' as the default value. */
        this.MonitorSourceFile = DefaultMonitorSourceFile;
      }

      if(defaultValues.ContainsKey(ChangeCompleteTimeoutKey) && !(defaultValues[ChangeCompleteTimeoutKey] is int)) {
        /* The key is present, but the value is of the wrong type; use '3000' as the default value. */
        this.ChangeCompleteTimeout = DefaultChangeCompleteTimeout;
      }

      if(defaultValues.ContainsKey(SourceFileEncodingKey) && !(defaultValues[SourceFileEncodingKey] is Encoding)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.SourceFileEncoding = DefaultSourceFileEncoding;
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

    /// <summary>Gets or sets the timeout value that is used to determine if a filechange has been completed.</summary>
    public int ChangeCompleteTimeout {
      get { return (int)this[ChangeCompleteTimeoutKey]; }
      set { this[ChangeCompleteTimeoutKey] = value; }
    }

    /// <summary>Gets or sets the Encoding of the sourcefile.</summary>
    public Encoding SourceFileEncoding {
      get { return (Encoding)this[SourceFileEncodingKey]; }
      set { this[SourceFileEncodingKey] = value; }
    }
    #endregion

    #region Public static methods
    /// <summary>Determines if the source FileInfo is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns><see langword="true"/> if the FileInfo is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsSourceFileInfoSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(SourceFileInfoKey);
    }

    /// <summary>Selects the source FileInfo from the specified datasource information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns>The FileInfo that is stored in the datasource information or <see langword="null"/> if the FileInfo could not be found.</returns>
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
    public static bool IsMonitorSourceFileSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(MonitorSourceFileKey);
    }

    /// <summary>Selects the monitor-flag from the specified datasource information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns>The monitor-flag that is stored in the datasource information or <see langword="true"/> if the flag could not be found.</returns>
    public static bool SelectMonitorSourceFile(DataSourceInfo dataSourceInfo) {
      if(IsMonitorSourceFileSpecified(dataSourceInfo)) {
        return (bool)dataSourceInfo[MonitorSourceFileKey];
      }
      else {
        return DefaultMonitorSourceFile;
      }
    }

    /// <summary>Determines if the changecomplete timeout is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsChangeCompleteTimeoutSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(ChangeCompleteTimeoutKey);
    }

    /// <summary>Selects the changecomplete timeout from the specified datasource information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns>The value that is stored in the datasource information or the default value if the flag could not be found.</returns>
    public static int SelectChangeCompleteTimeout(DataSourceInfo dataSourceInfo) {
      if(IsChangeCompleteTimeoutSpecified(dataSourceInfo)) {
        return (int)dataSourceInfo[ChangeCompleteTimeoutKey];
      }
      else {
        return DefaultChangeCompleteTimeout;
      }
    }

    /// <summary>Determines if the sourcefile's encoding is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsSourceFileEncodingSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(SourceFileEncodingKey);
    }

    /// <summary>Selects the sourcefile's encoding from the specified datasource information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns>The value that is stored in the datasource information or <see langword="null"/> if the value could not be found.</returns>
    public static Encoding SelectSourceFileEncoding(DataSourceInfo dataSourceInfo) {
      if(IsSourceFileEncodingSpecified(dataSourceInfo)) {
        return dataSourceInfo[SourceFileEncodingKey] as Encoding;
      }
      else {
        return DefaultSourceFileEncoding;
      }
    }
    #endregion

    #region Public methods
    /// <summary>Determines if the source FileInfo is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the FileInfo is defined; <see langword="false"/> otherwise.</returns>
    public bool IsSourceFileInfoSpecified() {
      return this.IsValueSpecified(SourceFileInfoKey);
    }

    /// <summary>Determines if the monitor-flag is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the flag is defined; <see langword="false"/> otherwise.</returns>
    public bool IsMonitorSourceFileSpecified() {
      return this.IsValueSpecified(MonitorSourceFileKey);
    }

    /// <summary>Determines if the changecomplete timeout is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public bool IsChangeCompleteTimeoutSpecified() {
      return this.IsValueSpecified(ChangeCompleteTimeoutKey);
    }

    /// <summary>Determines if the sourcefile's encoding is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    public bool IsSourceFileEncodingSpecified() {
      return this.IsValueSpecified(SourceFileEncodingKey);
    }
    #endregion
  }
}
