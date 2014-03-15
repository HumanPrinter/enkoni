using System;
using System.Diagnostics;
using System.Xml.Serialization;

using Enkoni.Framework.Serialization;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  [CsvRecord(IgnoreHeaderOnRead = true, WriteHeader = true, CultureName = "en-US"), XmlRoot, DebuggerDisplay("RecordId: {RecordId} TextValue: {TextValue}")]
  public class TestDummy : IEntity<TestDummy>, ICloneable {
    /// <summary>Gets or sets a unique record ID.</summary>
    [XmlIgnore]
    public int RecordId { get; set; }

    /// <summary>Gets or sets a text value.</summary>
    [CsvColumn(0), XmlElement]
    public string TextValue { get; set; }

    /// <summary>Gets or sets a numeric value.</summary>
    [CsvColumn(1), XmlElement]
    public int NumericValue { get; set; }

    /// <summary>Gets or sets a value indicating whether something is true or not.</summary>
    [CsvColumn(2), XmlElement]
    public bool BooleanValue { get; set; }

    /// <summary>Copies the values from <paramref name="source"/> to this instance.</summary>
    /// <param name="source">The object that contains the correct values.</param>
    public void CopyFrom(TestDummy source) {
      this.RecordId = source.RecordId;
      this.TextValue = source.TextValue;
      this.NumericValue = source.NumericValue;
      this.BooleanValue = source.BooleanValue;
    }

    /// <summary>Creates a clone of this object.</summary>
    /// <returns>The copy of this instance.</returns>
    public object Clone() {
      return this.MemberwiseClone();
    }
  }
}
