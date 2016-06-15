using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

using Enkoni.Framework.Linq;

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
    /// <param name="dataSourceInfo">The data source information that must be used to access the source file.</param>
    public XmlFileRepository(DataSourceInfo dataSourceInfo)
      : base(dataSourceInfo) {
      XmlAttributeOverrides overrides = ConstructAttributeOverrides();
      this.serializer = new XmlSerializer(typeof(List<TEntity>), overrides);
    }

    #endregion

    #region FileRepository<T> overrides

    /// <summary>Reads all the available records from the source file.</summary>
    /// <param name="sourceFile">Information about the file that must be read.</param>
    /// <param name="dataSourceInfo">Optional information about the data source.</param>
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
    /// <param name="dataSourceInfo">Optional information about the data source.</param>
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

    #region Private helper methods

    /// <summary>Constructs an instance of the <see cref="XmlAttributeOverrides"/> class that is used to instruct an <see cref="XmlSerializer"/> how to (de)serialize
    /// an entity.</summary>
    /// <returns>The constructed instance.</returns>
    private static XmlAttributeOverrides ConstructAttributeOverrides() {
      /* Construct the attribute overrides */
      XmlAttributeOverrides overrides = new XmlAttributeOverrides();

      Type type = typeof(TEntity);
      Type[] baseTypes = type.GetBaseClasses();

      /* Get the properties of the types and determine which properties they have in common (those will be overridden properties) */
      PropertyInfo[] propertiesOfEntity = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
      PropertyInfo[] propertiesOfBaseTypes = baseTypes.SelectMany(t => t.GetProperties(BindingFlags.Instance | BindingFlags.Public)).ToArray();
      IEnumerable<PropertyInfo> overriddenProperties = propertiesOfEntity.Intersect(propertiesOfBaseTypes, propertiesOfEntity.CreateEqualityComparer(p => p.Name));

      foreach(PropertyInfo propertyOfEntity in overriddenProperties) {
        /* Select the property from the base type */
        PropertyInfo propertyOfBaseType = propertiesOfBaseTypes.First(p => p.Name == propertyOfEntity.Name);
        XmlAttributes xmlAttributes = new XmlAttributes(new CustomAttributeProvider(propertyOfEntity));
        overrides.Add(propertyOfBaseType.DeclaringType, propertyOfEntity.Name, xmlAttributes);
      }

      return overrides;
    }

    #endregion

    #region Private classes

    /// <summary>Implements a custom attribute provider that is used to construct an <see cref="XmlAttributes"/> instance.</summary>
    private class CustomAttributeProvider : ICustomAttributeProvider {
      /// <summary>The PropertyInfo that describes the property whose attributes must be retrieved.</summary>
      private PropertyInfo propertyInfo;

      /// <summary>Initializes a new instance of the <see cref="CustomAttributeProvider"/> class.</summary>
      /// <param name="propertyInfo">The instance that is the source for this provider.</param>
      public CustomAttributeProvider(PropertyInfo propertyInfo) {
        this.propertyInfo = propertyInfo;
      }

      /// <summary>Returns an array of all of the custom attributes defined on this member, excluding named attributes, or an empty array if there
      /// are no custom attributes.</summary>
      /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
      /// <returns>An array of Objects representing custom attributes, or an empty array.</returns>
      public object[] GetCustomAttributes(bool inherit) {
        return this.propertyInfo.GetCustomAttributes(inherit);
      }

      /// <summary>Returns an array of custom attributes defined on this member, identified by type, or an empty array if there are no custom
      /// attributes of that type.</summary>
      /// <param name="attributeType">The type of the custom attributes.</param>
      /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
      /// <returns>An array of Objects representing custom attributes, or an empty array.</returns>
      public object[] GetCustomAttributes(Type attributeType, bool inherit) {
        return this.propertyInfo.GetCustomAttributes(attributeType, inherit);
      }

      /// <summary>Indicates whether one or more instance of attributeType is defined on this member.</summary>
      /// <param name="attributeType">The type of the custom attributes.</param>
      /// <param name="inherit">When true, look up the hierarchy chain for the inherited custom attribute.</param>
      /// <returns><see langword="true"/> if the attributeType is defined on this member; <see langword="false"/> otherwise.</returns>
      public bool IsDefined(Type attributeType, bool inherit) {
        return this.propertyInfo.IsDefined(attributeType, inherit);
      }
    }

    #endregion
  }
}