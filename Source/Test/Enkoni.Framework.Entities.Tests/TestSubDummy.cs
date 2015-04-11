using System;

using Enkoni.Framework.Serialization;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>A helper class to support the testcases.</summary>
  public class TestSubDummy : Entity<TestSubDummy>, ICloneable {
    /// <summary>Gets or sets the unique ID for this entity.</summary>
    public override int RecordId {
      get { return base.RecordId; }
      set { base.RecordId = value; }
    }

    /// <summary>Gets or sets a text value.</summary>
    public string TextValue { get; set; }

    /// <summary>Gets or sets the ID of the parent entity.</summary>
    public int ParentId { get; set; }

    /// <summary>Creates a clone of this object.</summary>
    /// <returns>The copy of this instance.</returns>
    public object Clone() {
      return this.MemberwiseClone();
    }
  }
}
