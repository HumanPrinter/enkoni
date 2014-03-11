namespace Enkoni.Framework.Entities.Database.Bugs {
  /// <summary>A helper class to support the testcases.</summary>
  public class TestDummy : IEntity<TestDummy> {
    /// <summary>Gets or sets a unique record ID.</summary>
    public int RecordId { get; set; }

    /// <summary>Gets or sets a text value.</summary>
    public string TextValue { get; set; }

    /// <summary>Gets or sets a numeric value.</summary>
    public int NumericValue { get; set; }

    /// <summary>Gets or sets a value indicating whether something is true or not.</summary>
    public bool BooleanValue { get; set; }

    /// <summary>Copies the values from <paramref name="source"/> to this instance.</summary>
    /// <param name="source">The object that contains the correct values.</param>
    public void CopyFrom(TestDummy source) {
      this.RecordId = source.RecordId;
      this.TextValue = source.TextValue;
      this.NumericValue = source.NumericValue;
      this.BooleanValue = source.BooleanValue;
    }
  }
}
