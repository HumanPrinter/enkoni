//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlFileRepository.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Holds the default implementation of a repository that uses an XML-file as datasource.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Enkoni.Framework.Entities {
  /// <summary>This class extends the abstract <see cref="FileRepository{TEntity}"/> class and implements the functionality to read and write from or 
  /// to an XML-file.</summary>
  /// <typeparam name="TEntity">The type of the entity that is handled by this repository.</typeparam>
  public class XmlFileRepository<TEntity> : FileRepository<TEntity>
    where TEntity : class, IEntity<TEntity>, new() {
    #region Instance variables
    /// <summary>The instance that is used to (de)serialize the entities.</summary>
    private XmlSerializer serializer;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="XmlFileRepository{TEntity}"/> class using the specified <see cref="DataSourceInfo"/>.
    /// </summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the sourcefile.</param>
    public XmlFileRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
      this.serializer = new XmlSerializer(typeof(List<TEntity>));
    }
    #endregion

    #region FileRepository<T> overrides
    /// <summary>Reads all the available records from the sourcefile.</summary>
    /// <param name="sourceFile">Information about the file that must be read.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <returns>The entities that were read from the file.</returns>
    protected override IEnumerable<TEntity> ReadAllRecordsFromFile(FileInfo sourceFile, DataSourceInfo dataSourceInfo) {
      Encoding encoding = this.SourceFileEncoding;
      if(FileSourceInfo.IsSourceFileEncodingSpecified(dataSourceInfo)) {
        encoding = FileSourceInfo.SelectSourceFileEncoding(dataSourceInfo);
      }

      FileStream fileStream = null;
      try {
        fileStream = new FileStream(sourceFile.FullName, FileMode.Open, FileAccess.Read);
        using(StreamReader reader = new StreamReader(fileStream, encoding)) {
          fileStream = null;
          IEnumerable<TEntity> result = this.serializer.Deserialize(reader) as List<TEntity>;
          return result;
        }
      }
      finally {
        if(fileStream != null) {
          fileStream.Dispose();
        }
      }
    }

    /// <summary>Writes the specified records to the destination file.</summary>
    /// <param name="destinationFile">Information about the file in which the contents must be saved.</param>
    /// <param name="dataSourceInfo">Optional information about the datasource.</param>
    /// <param name="contents">The new contents of the file.</param>
    /// <returns>The entities after they have been written to the file (in case the saving resulted in some updated values).</returns>
    protected override IEnumerable<TEntity> WriteAllRecordsToFile(FileInfo destinationFile, DataSourceInfo dataSourceInfo,
      IEnumerable<TEntity> contents) {
      Encoding encoding = this.SourceFileEncoding;
      if(FileSourceInfo.IsSourceFileEncodingSpecified(dataSourceInfo)) {
        encoding = FileSourceInfo.SelectSourceFileEncoding(dataSourceInfo);
      }

      Stream fileStream = null;
      try {
        fileStream = new FileStream(destinationFile.FullName, FileMode.Create, FileAccess.Write);
        using(StreamWriter writer = new StreamWriter(fileStream, encoding)) {
          fileStream = null;
          this.serializer.Serialize(writer, contents.ToList());
          return contents;
        }
      }
      finally {
        if(fileStream != null) {
          fileStream.Dispose();
        }
      }
    }
    #endregion
  }
}
