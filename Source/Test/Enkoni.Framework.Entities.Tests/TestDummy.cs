//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TestDummy.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains a dummy class that is used by the repository testcases.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Xml.Serialization;

using Enkoni.Framework.Serialization;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  [CsvRecord(IgnoreHeaderOnRead = true, WriteHeader = true, CultureName = "en-US"), XmlRoot, Table("TestDummy"), DebuggerDisplay("RecordId: {RecordId} TextValue: {TextValue}")]
  public class TestDummy : IEntity<TestDummy>, ICloneable {
    /// <summary>Gets or sets a unique record ID.</summary>
    [XmlIgnore, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RecordId { get; set; }

    /// <summary>Gets or sets a text value.</summary>
    [CsvColumn(0), XmlElement, Required, StringLength(100)]
    public string TextValue { get; set; }

    /// <summary>Gets or sets a numeric value.</summary>
    [CsvColumn(1), XmlElement, Required]
    public int NumericValue { get; set; }

    /// <summary>Gets or sets a value indicating whether something is true or not.</summary>
    [CsvColumn(2), XmlElement, Required]
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
