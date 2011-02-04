//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvFileRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that uses a CSV-file as datasource.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Text;

using Enkoni.Framework.Serialization;

namespace Enkoni.Framework.Entities {
  /// <summary>This class extends the abstract <see cref="FileRepository{TEntity}"/> class and implements the 
  /// functionality to read and write from or to a CSV-file.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class CsvFileRepository<TEntity> : FileRepository<TEntity>
    where TEntity : class, IEntity<TEntity>, new() {
    #region Instance variables
    /// <summary>The instance that is used to (de)serialize the entities.</summary>
    private CsvSerializer<TEntity> serializer;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="CsvFileRepository{TEntity}"/> class using the specified
    /// <see cref="DataSourceInfo"/>.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the sourcefile.</param>
    public CsvFileRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
      this.serializer = new CsvSerializer<TEntity>();

      this.SourceFileEncoding = CsvFileSourceInfo.SelectSourceFileEncoding(dataSourceInfo);
    }
    #endregion

    #region Protected properties
    /// <summary>Gets the encoding of the sourcefile.</summary>
    protected Encoding SourceFileEncoding { get; private set; }
    #endregion

    #region FileRepository<T> overrides
    /// <summary>Reads all the available records from the sourcefile.</summary>
    /// <param name="sourceFile">Information about the file that must be read.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <returns>The entities that were read from the file.</returns>
    protected override IEnumerable<TEntity> ReadAllRecordsFromFile(FileInfo sourceFile, DataSourceInfo dataSourceInfo) {
      Encoding encoding = this.SourceFileEncoding;
      if(CsvFileSourceInfo.IsSourceFileEncodingSpecified(dataSourceInfo)) {
        encoding = CsvFileSourceInfo.SelectSourceFileEncoding(dataSourceInfo);
      }

      return this.serializer.Deserialize(sourceFile.FullName, encoding);
    }

    /// <summary>Writes the specified records to the destination file.</summary>
    /// <param name="destinationFile">Information about the file in which the contents must be saved.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <param name="contents">The new contents of the file.</param>
    /// <returns>The entities after they have been written to the file (in case the saving resulted in some updated values).
    /// </returns>
    protected override IEnumerable<TEntity> WriteAllRecordsToFile(FileInfo destinationFile, DataSourceInfo dataSourceInfo, 
      IEnumerable<TEntity> contents) {
      Encoding encoding = this.SourceFileEncoding;
      if(CsvFileSourceInfo.IsSourceFileEncodingSpecified(dataSourceInfo)) {
        encoding = CsvFileSourceInfo.SelectSourceFileEncoding(dataSourceInfo);
      }

      this.serializer.Serialize(contents, destinationFile.FullName, encoding);
      return contents;
    }
    #endregion
  }
}
