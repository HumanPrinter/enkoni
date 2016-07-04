namespace Enkoni.Framework {
  /// <summary>Contains the options that can be passed to a double comparison function to specify the method to compare doubles.</summary>
  public enum DoubleCompareOption {
    /// <summary>Compare two doubles by looking if the difference is within a specific margin.</summary>
    Margin,

    /// <summary>Compare two doubles by looking at their significant digits.</summary>
    SignificantDigits
  }
}
