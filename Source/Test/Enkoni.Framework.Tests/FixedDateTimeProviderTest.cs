using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="FixedDateTimeProvider"/> class.</summary>
  [TestClass]
  public class FixedDateTimeProviderTest {
    /// <summary>Tests the functionality of <see cref="DateTimeProvider.MaxValue"/>.</summary>
    [TestMethod]
    public void FixedDateTimeProvider_MaxValueReturnsActualMaxValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new FixedDateTimeProvider(new DateTime(2014, 12, 23));

      Assert.AreEqual(DateTime.MaxValue, testSubject.MaxValue);
    }

    /// <summary>Tests the functionality of <see cref="DateTimeProvider.MinValue"/>.</summary>
    [TestMethod]
    public void FixedDateTimeProvider_MinValueReturnsActualMinValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new FixedDateTimeProvider(new DateTime(2014, 12, 23));

      Assert.AreEqual(DateTime.MinValue, testSubject.MinValue);
    }

    /// <summary>Tests functionality of <see cref="FixedDateTimeProvider.Now"/>.</summary>
    [TestMethod]
    public void FixedDateTimeProvider_LocalTime_NowReturnsConfiguredValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new FixedDateTimeProvider(new DateTime(2014, 12, 23, 9, 33, 42, DateTimeKind.Local));

      DateTime testvalue = testSubject.Now;
      DateTime expectedValue = new DateTime(2014, 12, 23, 9, 33, 42, DateTimeKind.Local);

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="FixedDateTimeProvider.Now"/>.</summary>
    [TestMethod]
    public void FixedDateTimeProvider_UtcTime_NowReturnsConfiguredValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new FixedDateTimeProvider(new DateTime(2014, 12, 23, 9, 33, 42, DateTimeKind.Utc));

      DateTime testvalue = testSubject.Now;
      DateTime expectedValue = new DateTime(2014, 12, 23, 9, 33, 42, DateTimeKind.Utc).ToLocalTime();

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests the functionality of <see cref="FixedDateTimeProvider.Today"/>.</summary>
    [TestMethod]
    public void FixedDateTimeProvider_TodayReturnsConfiguredValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new FixedDateTimeProvider(new DateTime(2014, 12, 23, 9, 33, 42));

      DateTime testvalue = testSubject.Today;
      DateTime expectedValue = new DateTime(2014, 12, 23);

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="FixedDateTimeProvider.UtcNow"/>.</summary>
    [TestMethod]
    public void FixedDateTimeProvider_LocalTime_UtcNowReturnsConfiguredValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new FixedDateTimeProvider(new DateTime(2014, 12, 23, 9, 33, 42, DateTimeKind.Local));

      DateTime testvalue = testSubject.UtcNow;
      DateTime expectedValue = new DateTime(2014, 12, 23, 9, 33, 42, DateTimeKind.Local).ToUniversalTime();

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="FixedDateTimeProvider.UtcNow"/>.</summary>
    [TestMethod]
    public void FixedDateTimeProvider_UtcTime_UtcNowReturnsConfiguredValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new FixedDateTimeProvider(new DateTime(2014, 12, 23, 9, 33, 42, DateTimeKind.Utc));

      DateTime testvalue = testSubject.UtcNow;
      DateTime expectedValue = new DateTime(2014, 12, 23, 9, 33, 42, DateTimeKind.Utc);

      Assert.AreEqual(expectedValue, testvalue);
    }
  }
}
