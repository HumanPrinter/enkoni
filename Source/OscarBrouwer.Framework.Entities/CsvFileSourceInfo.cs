//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvFileSourceInfo.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines a class that contains information about a CSV-filebased datasource that is used by the CsvFileRepository.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This class can be used by the <see cref="CsvFileRepository{TEntity}"/> or any of its descendants to retrieve 
  /// valuable information about the file that is to be used. This class is added for improved usability of the 
  /// DataSourceInfo in combination with the CsvFileRepository.</summary>
  public class CsvFileSourceInfo : FileSourceInfo {
    #region Public constants
    /// <summary>Defines the key that is used to store and retrieve the encoding of the source file.</summary>
    public const string SourceFileEncodingKey = "SourceFileEncoding";
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using default values.</summary>
    public CsvFileSourceInfo()
      : this(Encoding.UTF32) {
    }

    /// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using the specified encoding for 
    /// the sourcefile.</summary>
    /// <param name="sourceFileEncoding">The encoding of the sourcefile.</param>
    public CsvFileSourceInfo(Encoding sourceFileEncoding)
      : base() {
        this.SourceFileEncoding = sourceFileEncoding;
    }

    /// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using the specified values.
    /// </summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
    /// <param name="changeCompleteTimeout">The timeout that is used to determine if a filechange has completed or not.
    /// </param>
    public CsvFileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile, int changeCompleteTimeout)
      : this(sourceFileInfo, monitorSourceFile, changeCompleteTimeout, Encoding.UTF32) {
    }

    /// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using the specified values.
    /// </summary>
    /// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
    /// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
    /// <param name="changeCompleteTimeout">The timeout that is used to determine if a filechange has completed or not.
    /// </param>
    /// <param name="sourceFileEncoding">The encoding of the sourcefile.</param>
    public CsvFileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile, int changeCompleteTimeout, 
      Encoding sourceFileEncoding)
      : base(sourceFileInfo, monitorSourceFile, changeCompleteTimeout) {
      this.SourceFileEncoding = sourceFileEncoding;
    }

    /// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using the specified default values.
    /// If the default values do not specify the supported properties, the default values will be used.</summary>
    /// <param name="defaultValues">The default values that are to be used.</param>
    public CsvFileSourceInfo(Dictionary<string, object> defaultValues)
      : base(defaultValues) {
      /* Check if the dictionary contains the reserved key and, if so, the key denotes a value of the correct type */
      if(defaultValues.ContainsKey(SourceFileEncodingKey) && !(defaultValues[SourceFileEncodingKey] is Encoding)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.SourceFileEncoding = Encoding.UTF32;
      }
    }
    #endregion

    #region Public properties
    /// <summary>Gets or sets the Encoding of the sourcefile.</summary>
    public Encoding SourceFileEncoding {
      get { return (Encoding)this[SourceFileEncodingKey]; }
      set { this[SourceFileEncodingKey] = value; }
    }
    #endregion

    #region Public static methods
    /// <summary>Determines if the sourcefile's encoding is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public static bool IsSourceFileEncodingSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(SourceFileEncodingKey);
    }

    /// <summary>Selects the sourcefile's encoding from the specified datasource information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns>The value that is stored in the datasource information or <see langword="null"/> if the value could not be
    /// found.</returns>
    public static Encoding SelectSourceFileEncoding(DataSourceInfo dataSourceInfo) {
      if(IsSourceFileEncodingSpecified(dataSourceInfo)) {
        return dataSourceInfo[SourceFileEncodingKey] as Encoding;
      }
      else {
        return Encoding.UTF32;
      }
    }
    #endregion

    #region Public methods
    /// <summary>Determines if the sourcefile's encoding is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the value is defined; <see langword="false"/> otherwise.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public bool IsSourceFileEncodingSpecified() {
      return this.IsValueSpecified(SourceFileEncodingKey);
    }
    #endregion
  }
}
