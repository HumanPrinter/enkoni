using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="EventArgs{T}"/> class.</summary>
  [TestClass]
  public class EventArgsTest {
    /// <summary>Tests the functionality of the <see cref="EventArgs{T}"/> class.</summary>
    [TestMethod]
    public void EventArgs_ValueSetThroughConstructor_ValueIsRetrievableThroughProperty() {
      /* Create the test subject */
      EventArgs<int> testSubject = new EventArgs<int>(42);

      /* Test the values of the test subject */
      Assert.AreEqual(42, testSubject.EventValue);
    }

    /// <summary>Tests the functionality of the <see cref="EventArgs{T}"/> class.</summary>
    [TestMethod]
    public void EventArgs_NullValueSetThroughConstructor_ValueIsRetrievableThroughProperty() {
      /* Create the test subject */
      EventArgs<string> testSubject = new EventArgs<string>(null);

      /* Test the values of the test subject */
      Assert.IsNull(testSubject.EventValue);
    }
  }
}
