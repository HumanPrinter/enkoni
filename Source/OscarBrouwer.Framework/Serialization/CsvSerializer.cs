//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvSerializer.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines the class that is capable of (de)serializing CSV data.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace OscarBrouwer.Framework.Serialization {
  /// <summary>Serializes or deserializes a list of objects to and from a CSV file.</summary>
  /// <typeparam name="T">Type of the object that has to be serialized.</typeparam>
  public class CsvSerializer<T> where T : new() {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="CsvSerializer{T}"/> class.</summary>
    /// <exception cref="InvalidTypeParameterException">The specified type-parameter cannot be serialized using this 
    /// serializer.</exception>
    /// <exception cref="InvalidOperationException">The specified type-parameter contains illegal metadata that prevents it 
    /// from being (de)serialized.</exception>
    public CsvSerializer() {
      this.ColumnNameMappings = new Dictionary<int, string>();
      this.PropertyDelegates = new Dictionary<int, Delegate>();

      MemberInfo info = typeof(T);

      /* Get the CsvRecord attribute */
      CsvRecordAttribute recordAttr = Attribute.GetCustomAttribute(info, typeof(CsvRecordAttribute), false) as CsvRecordAttribute;
      if(recordAttr == null) {
        throw new InvalidTypeParameterException("The specified type does not contain the CsvRecord-attribute. This attribute must be applied before it can be serialized to a CSV file.");
      }

      /* Get some properties of the attribute */
      this.Separator = recordAttr.Separator;
      this.IgnoreHeaderOnRead = recordAttr.IgnoreHeaderOnRead;
      this.EmitHeader = recordAttr.WriteHeader;

      /* Get the property attributes and map them */
      Type typeOfT = typeof(T);
      PropertyInfo[] properties = typeOfT.GetProperties();

      foreach(PropertyInfo property in properties) {
        CsvColumnAttribute colAttr = Attribute.GetCustomAttribute(property, typeof(CsvColumnAttribute), false) as CsvColumnAttribute;
        if(colAttr != null) {
          if(this.ColumnNameMappings.ContainsKey(colAttr.FieldIndex)) {
            /* The field-index is already in use */
            throw new InvalidOperationException("Cannot use the same field index for more than one property.");
          }
          else {
            this.ColumnNameMappings.Add(colAttr.FieldIndex, property.Name);

            ParameterExpression objExpression = Expression.Parameter(typeof(T), "obj");
            Expression propertyExpression = Expression.Property(objExpression, property.Name);
            LambdaExpression lambdaExpression = Expression.Lambda(propertyExpression, objExpression);
            Delegate functionDelegate = lambdaExpression.Compile();
            this.PropertyDelegates.Add(colAttr.FieldIndex, functionDelegate);
          }
        }
      }

      /* Make sure the field-indexes are correct */
      /* These two checks could be combined, this allows for more describing errormessages */
      if(this.ColumnNameMappings.Keys.Min() != 0) {
        throw new InvalidOperationException("The specified field index values must start at '0'");
      }

      if(this.ColumnNameMappings.Count != this.ColumnNameMappings.Keys.Max() + 1) {
        throw new InvalidOperationException("The specified field index values must be sequential");
      }
    }
    #endregion

    #region Properties
    /// <summary>Gets the mappings of the columnnames. The dictionary uses the columnindex as key and columnname as value.
    /// </summary>
    protected Dictionary<int, string> ColumnNameMappings { get; private set; }

    /// <summary>Gets the delegates that give access to the properties of the instances that need to be serialized and 
    /// deserialized.</summary>
    protected Dictionary<int, Delegate> PropertyDelegates { get; private set; }

    /// <summary>Gets or sets a value indicating whether a header-line must be included when serializing the objects.</summary>
    protected bool EmitHeader { get; set; }

    /// <summary>Gets or sets a value indicating whether the header should be ignored when reading the file.</summary>
    protected bool IgnoreHeaderOnRead { get; set; }

    /// <summary>Gets or sets the separator-character.</summary>
    protected char Separator { get; set; }
    #endregion

    #region Public methods
    /// <summary>Serializes a list of objects to a CSV file using a default encoding of UTF-32.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="filePath">The name of the outputfile.</param>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    public void Serialize(IEnumerable<T> objects, string filePath) {
      this.Serialize(objects, filePath, Encoding.UTF32);
    }

    /// <summary>Serializes a list of objects to a CSV file.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="filePath">The name of the outputfile.</param>
    /// <param name="encoding">The encoding that must be used to serialize the data.</param>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    public void Serialize(IEnumerable<T> objects, string filePath, Encoding encoding) {
      /* Validate the parameters */
      if(objects == null) {
        throw new ArgumentNullException("objects", "The parameter cannot be null.");
      }

      if(filePath == null) {
        throw new ArgumentNullException("filePath", "The parameter cannot be null.");
      }

      if(encoding == null) {
        throw new ArgumentNullException("encoding", "The parameter cannot be null.");
      }

      /* The check for null and empty can be combined into one test, but this approach allows for more fine-grained 
       * exception management. */
      /* Although we already know that the string isn't 'null', the IsNullOrEmpty performs better than an Equals */
      if(string.IsNullOrEmpty(filePath.Trim())) {
        throw new ArgumentException("The filePath cannot be empty cannot be null.", "filePath");
      }

      if(filePath.Intersect(Path.GetInvalidPathChars()).Count() > 0) {
        throw new ArgumentException("The filepath contains illegal characters.", "filePath");
      }

      string output = this.Serialize(objects);

      File.WriteAllText(filePath, output, encoding);
    }

    /// <summary>Serializes a list of objects to a CSV format and writes the data to a stream using a default encoding of
    /// UTF-32.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="stream">The name of the outputfile.</param>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    public void Serialize(IEnumerable<T> objects, Stream stream) {
      this.Serialize(objects, stream, Encoding.UTF32);
    }

    /// <summary>Serializes a list of objects to a CSV format and writes the data to a stream.</summary>
    /// <param name="objects">The collection of objects that must be serialized.</param>
    /// <param name="stream">The name of the outputfile.</param>
    /// <param name="encoding">The encoding that must be used to serialize the data.</param>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    public void Serialize(IEnumerable<T> objects, Stream stream, Encoding encoding) {
      /* Validate the parameters */
      if(objects == null) {
        throw new ArgumentNullException("objects", "The parameter cannot be null.");
      }

      if(encoding == null) {
        throw new ArgumentNullException("encoding", "The parameter cannot be null.");
      }

      if(stream == null) {
        throw new ArgumentNullException("stream", "The parameter cannot be null.");
      }

      if(!stream.CanWrite) {
        throw new ArgumentException("Specified stream is not writable.", "stream");
      }

      string output = this.Serialize(objects);

      byte[] outputBytes = encoding.GetBytes(output);
      stream.Write(outputBytes, 0, outputBytes.Length);
      if(stream.CanSeek) {
        stream.Seek(outputBytes.LongLength * -1L, SeekOrigin.Current);
      }
    }

    /// <summary>Deserializes a CSV to a list of objects using a default encoding of UTF-32.</summary>
    /// <param name="filePath">Filepath to csv file.</param>
    /// <returns>List with objects.</returns>
    public ICollection<T> Deserialize(string filePath) {
      return this.Deserialize(filePath, Encoding.UTF32);
    }

    /// <summary>Deserializes a CSV to a list of objects.</summary>
    /// <param name="filePath">Filepath to csv file.</param>
    /// <param name="encoding">The encoding that must be used to deserialize the data.</param>
    /// <returns>List with objects.</returns>
    public ICollection<T> Deserialize(string filePath, Encoding encoding) {
      /* Validate the parameters */
      if(filePath == null) {
        throw new ArgumentNullException("filePath", "The parameter cannot be null.");
      }

      if(encoding == null) {
        throw new ArgumentNullException("encoding", "The parameter cannot be null.");
      }
      
      /* The check for null and empty can be combined into one test, but this approach allows for more fine-grained 
       * exception management. */
      /* Although we already know that the string isn't 'null', the IsNullOrEmpty performs better than an Equals */
      if(string.IsNullOrEmpty(filePath.Trim())) { 
        throw new ArgumentException("The filepath cannot be empty cannot be null.", "filePath");
      }

      if(filePath.Intersect(Path.GetInvalidPathChars()).Count() > 0) {
        throw new ArgumentException("The filepath contains illegal characters.", "filePath");
      }

      /* Deserialize the data */
      string[] lines = File.ReadAllLines(filePath, encoding);

      return this.Deserialize(lines);
    }

    /// <summary>Deserializes CSV data to a list of objects using a default encoding of UTF-32.</summary>
    /// <param name="stream">The stream that contains the csv data.</param>
    /// <returns>List with objects.</returns>
    public ICollection<T> Deserialize(Stream stream) {
      return this.Deserialize(stream, Encoding.UTF32);
    }

    /// <summary>Deserializes a CSV to a list of objects.</summary>
    /// <param name="stream">The stream that contains the csv data.</param>
    /// <param name="encoding">The encoding that must be used to deserialize the data.</param>
    /// <returns>List with objects.</returns>
    public ICollection<T> Deserialize(Stream stream, Encoding encoding) {
      /* Validate the parameters */
      if(encoding == null) {
        throw new ArgumentNullException("encoding", "The parameter cannot be null.");
      }
      
      if(stream == null) {
        throw new ArgumentNullException("stream", "The parameter cannot be null.");
      }

      if(!stream.CanRead) {
        throw new ArgumentException("The specified stream is not readable.", "stream");
      }

      /* Deserialize the data */
      List<string> lines = new List<string>();
      using(StreamReader reader = new StreamReader(stream, encoding)) {
        while(!reader.EndOfStream) {
          lines.Add(reader.ReadLine());
        }
      }

      return this.Deserialize(lines);
    }
    #endregion

    #region Protected methods
    /// <summary>Serializes the collection of objects to a string in which all lines are seperated using the line-terminator 
    /// for the current environment.</summary>
    /// <param name="objects">The objects that must be serialized.</param>
    /// <returns>The string representation of the collection of objects.</returns>
    protected virtual string Serialize(IEnumerable<T> objects) {
      StringBuilder lines = new StringBuilder();
      bool firstObject = true;
      if(this.EmitHeader) {
        firstObject = false;
        bool firstField = true;
        foreach(string propertyName in this.ColumnNameMappings.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value)) {
          if(firstField) {
            lines.AppendFormat("{0}", propertyName);
            firstField = false;
          }
          else {
            lines.AppendFormat("{0}{1}", this.Separator, propertyName);
          }
        }
      }

      foreach(T obj in objects) {
        if(!firstObject) {
          lines.AppendLine();
        }
        else {
          firstObject = false;
        }

        bool firstField = true;
        foreach(Delegate retrievePropertyFunc in this.PropertyDelegates.OrderBy(kvp => kvp.Key).Select(kvp => kvp.Value)) {
          object propertyValue = retrievePropertyFunc.DynamicInvoke(obj);
          if(propertyValue == null) {
            propertyValue = "null";
          }

          if(firstField) {
            lines.AppendFormat("{0}", propertyValue);
            firstField = false;
          }
          else {
            lines.AppendFormat("{0}{1}", this.Separator, propertyValue);
          }
        }
      }

      return lines.ToString();
    }

    /// <summary>Deserializes a collection of strings to a collection of objects.</summary>
    /// <param name="lines">The lines that must be deserialized.</param>
    /// <returns>The collection of deserialized objects.</returns>
    protected virtual ICollection<T> Deserialize(IEnumerable<string> lines) {
      ICollection<T> objList = new List<T>();

      int startLine = this.IgnoreHeaderOnRead ? 1 : 0;
      for(int lineIndex = startLine; lineIndex < lines.Count(); ++lineIndex) {
        T obj = new T();

        string[] columns = lines.ElementAt(lineIndex).Split(this.Separator);
        for(int colIndex = 0; colIndex < columns.Count(); colIndex++) {
          PropertyInfo propertyInfo = obj.GetType().GetProperty(this.ColumnNameMappings[colIndex]);

          SetPropertyValue(propertyInfo, obj, columns[colIndex]);
        }

        objList.Add(obj);
      }

      return objList;
    }
    #endregion

    #region Helper methods
    /// <summary>Set a property value of an object.</summary>
    /// <param name="propertyInfo">The property that must be assigned.</param>
    /// <param name="obj">Reference to the object whose property must be assigned.</param>
    /// <param name="value">The value that must be assigned.</param>
    /// <exception cref="NotSupportedException">The type of the property that must be assigned is not supported.
    /// </exception>
    private static void SetPropertyValue(PropertyInfo propertyInfo, T obj, string value) {
      CultureInfo culture = CultureInfo.InvariantCulture;

      if(propertyInfo.PropertyType == typeof(string)) {
        propertyInfo.SetValue(obj, value, null);
      }
      else if(propertyInfo.PropertyType == typeof(int)) {
        value = string.IsNullOrEmpty(value) ? default(int).ToString(culture) : value;
        propertyInfo.SetValue(obj, int.Parse(value, culture), null);
      }
      else if(propertyInfo.PropertyType == typeof(float)) {
        value = string.IsNullOrEmpty(value) ? default(float).ToString(culture) : value;
        propertyInfo.SetValue(obj, float.Parse(value, culture), null);
      }
      else if(propertyInfo.PropertyType == typeof(double)) {
        value = string.IsNullOrEmpty(value) ? default(double).ToString(culture) : value;
        propertyInfo.SetValue(obj, double.Parse(value, culture), null);
      }
      else if(propertyInfo.PropertyType == typeof(DateTime)) {
        value = string.IsNullOrEmpty(value) ? default(DateTime).ToString(culture) : value;
        propertyInfo.SetValue(obj, DateTime.Parse(value, culture), null);
      }
      else {
        throw new NotSupportedException("Unable to parse data for data-type " + propertyInfo.PropertyType);
      }
    }
    #endregion
  }
}
