//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvColumnAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines an attribute that is used for the CSV (de)serialization capabilities.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

namespace Enkoni.Framework.Serialization {
  /// <summary>This attribute defines how a property must be serialized and deserialized to and from a column in a CSV file.</summary>
  [AttributeUsage(AttributeTargets.Property)]
  public sealed class CsvColumnAttribute : Attribute {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="CsvColumnAttribute"/> class using a default field index.</summary>
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

    /// <summary>Gets or sets the format string that is to be used when parsing the column value.</summary>
    public string FormatString { get; set; }

    /// <summary>Gets or sets the name of the culture that must be used to parse the field value. If this property is set, it overrides any 
    /// culture-settings that may have been set in the CsvRecord-attribute.</summary>
    public string CultureName { get; set; }
    #endregion
  }
}
