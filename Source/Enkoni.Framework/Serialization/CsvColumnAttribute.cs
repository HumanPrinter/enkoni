//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvColumnAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
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

    /// <summary>Gets or sets the format string that is to be used when parsing the column value.<br />
    /// De following format strings are supported:<br/>
    /// <list type="bullet">
    ///   <item>
    ///     <term>XXX</term>
    ///     <description>Serialize the property according to the format. I.e.: '000' in combination with integer value 7 results in "007".</description>
    ///   </item>
    ///   <item>
    ///     <term>[prefix]{0:XXX}[postfix]</term>
    ///     <description>Serialize the property according to the format. I.e.: 'a{0:000}b' in combination with integer value 7 results in "a007b".</description>
    ///   </item>
    ///   <item>
    ///     <term>X or -X (<see cref="String"/> properties only)</term>
    ///     <description>Serialize the property according to the format. I.e.: '-5' in combination with string value 'abc' results in "abc  ".</description>
    ///   </item>
    ///   <item>
    ///     <term>[prefix]{0,X}[postfix] or [prefix]{0,-X}[postfix] (<see cref="String"/> properties only)</term>
    ///     <description>Serialize the property according to the format. I.e.: 'y{0,-5}z' in combination with string value 'abc' results in "yabc  z".</description>
    ///   </item>
    ///   <item>
    ///     <term>true:[true string]|false:[false string] (<see cref="Boolean"/> properties only)</term>
    ///     <description>Serialize the property according to the format. I.e.: 'true:Y|false:N' in combination with boolean value 'True' results in "Y".</description>
    ///   </item>
    /// </list>
    /// </summary>
    public string FormatString { get; set; }

    /// <summary>Gets or sets the string that is used to identify a null-value. During serialization, this value is used when the property to which 
    /// this attribute is applied equals <see langword="null"/>. During deserialization, if the serialized value equals this null-string, 
    /// <see langword="null"/> or the default value is used.</summary>
    public string NullString { get; set; }

    /// <summary>Gets or sets the name of the culture that must be used to parse the field value. If this property is set, it overrides any 
    /// culture-settings that may have been set in the CsvRecord-attribute.</summary>
    public string CultureName { get; set; }
    #endregion
  }
}
