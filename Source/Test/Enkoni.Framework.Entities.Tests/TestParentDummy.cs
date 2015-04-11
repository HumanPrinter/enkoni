using System;
using System.Collections.Generic;
using System.Linq;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  public class TestParentDummy : Entity<TestParentDummy>, ICloneable {
    /// <summary>Gets or sets the unique ID for this entity.</summary>
    public override int RecordId {
      get { return base.RecordId; }
      set { base.RecordId = value; }
    }

    /// <summary>Gets or sets a text value.</summary>
    public string TextValue { get; set; }

    /// <summary>Gets or sets a numeric value.</summary>
    public int NumericValue { get; set; }

    /// <summary>Gets or sets a value indicating whether something is true or not.</summary>
    public bool BooleanValue { get; set; }

    /// <summary>Gets or sets the related sub dummies.</summary>
    public ICollection<TestSubDummy> Children { get; set; }

    /// <summary>Creates a clone of this object.</summary>
    /// <returns>The copy of this instance.</returns>
    public object Clone() {
      return this.MemberwiseClone();
    }
  }
}
