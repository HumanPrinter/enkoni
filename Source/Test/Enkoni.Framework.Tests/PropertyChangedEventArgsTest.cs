using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="PropertyChangedEventArgs{T}"/> class.</summary>
  [TestClass]
  public class PropertyChangedEventArgsTest {
    /// <summary>Tests the functionality of the <see cref="PropertyChangedEventArgs{T}"/> class.</summary>
    [TestMethod]
    public void PropertyChangedEventArgs_ValuesSetThroughConstructor_ValuesAreRetrievableThroughProperties() {
      /* Create the test subject */
      PropertyChangedEventArgs<int> testSubject = new PropertyChangedEventArgs<int>("MagicNumber", 21, 42);

      /* Test the values of the test subject */
      Assert.AreEqual("MagicNumber", testSubject.PropertyName);
      Assert.AreEqual(21, testSubject.OldValue);
      Assert.AreEqual(42, testSubject.NewValue);
    }

    /// <summary>Tests the functionality of the <see cref="PropertyChangedEventArgs{T}"/> class.</summary>
    [TestMethod]
    public void PropertyChangedEventArgs_NullValuesSetThroughConstructor_ValuesAreRetrievableThroughProperties() {
      /* Create the test subject */
      PropertyChangedEventArgs<string> testSubject = new PropertyChangedEventArgs<string>("MagicNumber", null, null);

      /* Test the values of the test subject */
      Assert.AreEqual("MagicNumber", testSubject.PropertyName);
      Assert.IsNull(testSubject.OldValue);
      Assert.IsNull(testSubject.NewValue);
    }
  }
}
