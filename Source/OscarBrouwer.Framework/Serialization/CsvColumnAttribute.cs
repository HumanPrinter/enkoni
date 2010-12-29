//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvColumnAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines an attribute that is used for the CSV (de)serialization capabilities.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;

namespace OscarBrouwer.Framework.Serialization {
  /// <summary>This attribute defines how a property must be serialized and deserialized to and from a column in a CVS file.
  /// </summary>
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class CsvColumnAttribute : Attribute {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="CsvColumnAttribute"/> class using a default field index.
    /// </summary>
    public CsvColumnAttribute()
      : this(0) {
    }

    /// <summary>Initializes a new instance of the <see cref="CsvColumnAttribute"/> class.</summary>
    /// <param name="fieldIndex">The index of the field in the CSV file.</param>
    public CsvColumnAttribute(int fieldIndex) {
      this.FieldIndex = fieldIndex;
    }
    #endregion

    #region Properties
    /// <summary>Gets the index of the field in the CSV file.</summary>
    public int FieldIndex { get; private set; }
    #endregion
  }
}
