//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvRecordAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines an attribute that is used for the CSV (de)serialization capabilities.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace OscarBrouwer.Framework.Serialization {
  /// <summary>This attribute defines how an object must be serialized and deserialized to and from a CVS file.</summary>
  [AttributeUsage(AttributeTargets.Class)]
  public sealed class CsvRecordAttribute : Attribute {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="CsvRecordAttribute"/> class using a default seperator.
    /// </summary>
    public CsvRecordAttribute()
      : this(',') {
    }

    /// <summary>Initializes a new instance of the <see cref="CsvRecordAttribute"/> class.</summary>
    /// <param name="separator">The separator that is used to seperate the fields.</param>
    public CsvRecordAttribute(char separator) {
      this.Separator = separator;
    }
    #endregion

    #region Properties
    /// <summary>Gets the seperator character. Defaults to ','.</summary>
    public char Separator { get; private set; }

    /// <summary>Gets or sets a value indicating whether the first line in the file must be ignored when deserializing the
    /// object.</summary>
    [DefaultValue(false)]
    public bool IgnoreHeaderOnRead { get; set; }

    /// <summary>Gets or sets a value indicating whether the header must be included in the file when serializing the object.
    /// </summary>
    [DefaultValue(false)]
    public bool WriteHeader { get; set; }
    #endregion
  }
}
