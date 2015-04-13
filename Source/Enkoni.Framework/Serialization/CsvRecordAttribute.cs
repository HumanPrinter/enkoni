using System;
using System.ComponentModel;

namespace Enkoni.Framework.Serialization {
  /// <summary>This attribute defines how an object must be serialized and deserialized to and from a CSV file.</summary>
  [AttributeUsage(AttributeTargets.Class)]
  public sealed class CsvRecordAttribute : Attribute {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="CsvRecordAttribute"/> class using a default separator.</summary>
    public CsvRecordAttribute()
      : this(',') {
    }

    /// <summary>Initializes a new instance of the <see cref="CsvRecordAttribute"/> class.</summary>
    /// <param name="separator">The separator that is used to separate the fields.</param>
    public CsvRecordAttribute(char separator) {
      this.Separator = separator;
    }
    #endregion

    #region Properties
    /// <summary>Gets the separator character. Defaults to ','.</summary>
    public char Separator { get; private set; }

    /// <summary>Gets or sets a value indicating whether the first line in the file must be ignored when deserializing the object.</summary>
    [DefaultValue(false)]
    public bool IgnoreHeaderOnRead { get; set; }

    /// <summary>Gets or sets a value indicating whether the header must be included in the file when serializing the object.</summary>
    [DefaultValue(false)]
    public bool WriteHeader { get; set; }

    /// <summary>Gets or sets the name of the culture that must be used to parse the field values.</summary>
    [DefaultValue(null)]
    public string CultureName { get; set; }
    #endregion
  }
}
