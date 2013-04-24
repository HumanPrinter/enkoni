//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvSerializer.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Defines the class that is capable of (de)serializing CSV data.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Enkoni.Framework.Serialization {
  /// <summary>Serializes or deserializes a list of objects to and from a CSV file.</summary>
  /// <typeparam name="T">Type of the object that has to be serialized.</typeparam>
  public class CsvSerializer<T> : Serializer<T> where T : new() {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="CsvSerializer{T}"/> class.</summary>
    /// <exception cref="InvalidTypeParameterException">The specified type-parameter cannot be serialized using this serializer.</exception>
    /// <exception cref="InvalidOperationException">The specified type-parameter contains illegal metadata that prevents it from being 
    /// (de)serialized.</exception>
    public CsvSerializer()
      : base() {
      CsvTransformer<T> transformer = new CsvTransformer<T>();

      this.ColumnNameMappings = transformer.ColumnNameMappings;
      this.EmitHeader = transformer.EmitHeader;
      this.IgnoreHeaderOnRead = transformer.IgnoreHeaderOnRead;
      this.Separator = transformer.Separator;
      this.Transformer = transformer;
    }
    #endregion

    #region Properties
    /// <summary>Gets the mappings of the columnnames. The dictionary uses the columnindex as key and columnname as value.</summary>
    protected Dictionary<int, string> ColumnNameMappings { get; private set; }

    /// <summary>Gets or sets a value indicating whether a header-line must be included when serializing the objects.</summary>
    protected bool EmitHeader { get; set; }

    /// <summary>Gets or sets a value indicating whether the header should be ignored when reading the file.</summary>
    protected bool IgnoreHeaderOnRead { get; set; }

    /// <summary>Gets or sets the separator-character.</summary>
    protected char Separator { get; set; }
    #endregion

    #region Protected methods
    /// <summary>Serializes a collection of items by transforming each item using the <see cref="Serializer{T}.Transformer"/> property and writing the 
    /// transformed item to the <paramref name="stream"/>. Each item will be seperated using the new line character(s) of the current environment.
    /// </summary>
    /// <param name="objects">The objects that must be serialized.</param>
    /// <param name="encoding">The encoding that must be used.</param>
    /// <param name="stream">The stream to which the serialized items must be sent.</param>
    /// <returns>The number of bytes that have been written to the stream.</returns>
    protected override int Serialize(IEnumerable<T> objects, Encoding encoding, Stream stream) {
      if(encoding == null) {
        throw new ArgumentNullException("encoding");
      }

      if(stream == null) {
        throw new ArgumentNullException("stream");
      }

      int writeCount = 0;
      if(this.EmitHeader) {
        StringBuilder headerBuilder = new StringBuilder();
        bool firstField = true;
        for(int columnIndex = 0; columnIndex <= this.ColumnNameMappings.Max(kvp => kvp.Key); ++columnIndex) {
          string propertyName = this.ColumnNameMappings.ContainsKey(columnIndex) ? this.ColumnNameMappings[columnIndex] : "Placeholder";
          if(firstField) {
            headerBuilder.AppendFormat("{0}", propertyName);
            firstField = false;
          }
          else {
            headerBuilder.AppendFormat("{0}{1}", this.Separator, propertyName);
          }
        }

        string header = headerBuilder.ToString();
        byte[] headerBytes = encoding.GetBytes(header);
        stream.Write(headerBytes, 0, headerBytes.Length);
        writeCount = headerBytes.Length;

        if(objects.Count() > 0) {
          byte[] newLine = encoding.GetBytes(Environment.NewLine);
          stream.Write(newLine, 0, newLine.Length);
          writeCount += newLine.Length;
        }
      }

      if(objects.Count() > 0) {
        writeCount += base.Serialize(objects, encoding, stream);
      }

      return writeCount;
    }

    /// <summary>Deserializes a collection of objects using the data that is accesible through the specified stream reader.</summary>
    /// <param name="reader">The object that gives access to the underlying stream.</param>
    /// <returns>The collection of deserialized objects.</returns>
    protected override ICollection<T> Deserialize(StreamReader reader) {
      if(reader == null) {
        throw new ArgumentNullException("reader");
      }

      if(this.IgnoreHeaderOnRead) {
        /* Discard the first line */
        reader.ReadLine();
      }

      return base.Deserialize(reader);
    }
    #endregion
  }
}
