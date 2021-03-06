﻿using System;
using System.Diagnostics;
using System.Xml.Serialization;

using Enkoni.Framework.Serialization;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  [CsvRecord(IgnoreHeaderOnRead = true, WriteHeader = true, CultureName = "en-US"), XmlRoot, DebuggerDisplay("RecordId: {RecordId} TextValue: {TextValue}")]
  public class TestDummy : Entity<TestDummy>, ICloneable {
    /// <summary>Gets or sets the unique ID for this entity.</summary>
    [XmlIgnore]
    public override int RecordId {
      get { return base.RecordId; }
      set { base.RecordId = value; }
    }

    /// <summary>Gets or sets a text value.</summary>
    [CsvColumn(0), XmlElement]
    public string TextValue { get; set; }

    /// <summary>Gets or sets a numeric value.</summary>
    [CsvColumn(1), XmlElement]
    public int NumericValue { get; set; }

    /// <summary>Gets or sets a value indicating whether something is true or not.</summary>
    [CsvColumn(2), XmlElement]
    public bool BooleanValue { get; set; }

    /// <summary>Creates a clone of this object.</summary>
    /// <returns>The copy of this instance.</returns>
    public object Clone() {
      return this.MemberwiseClone();
    }
  }
}
