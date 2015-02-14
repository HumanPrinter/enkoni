using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="DateTimeProvider"/> class.</summary>
  [TestClass]
  public class DateTimeProviderTest {
    /// <summary>Tests the functionality of <see cref="DateTimeProvider.MaxValue"/>.</summary>
    [TestMethod]
    public void DateTimeProvider_MaxValueReturnsActualMaxValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new DateTimeProvider();

      Assert.AreEqual(DateTime.MaxValue, testSubject.MaxValue);
    }

    /// <summary>Tests the functionality of <see cref="DateTimeProvider.MinValue"/>.</summary>
    [TestMethod]
    public void DateTimeProvider_MinValueReturnsActualMinValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new DateTimeProvider();

      Assert.AreEqual(DateTime.MinValue, testSubject.MinValue);
    }

    /// <summary>Tests functionality of <see cref="DateTimeProvider.Now"/>.</summary>
    [TestMethod]
    public void DateTimeProvider_NowReturnsCurrentNowValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new DateTimeProvider();

      DateTime testvalue = testSubject.Now;
      DateTime expectedValue = DateTime.Now;

      /* Because test executions are not always performed with the same speed, the milliseconds are not considered in the equation */
      expectedValue = new DateTime(expectedValue.Year, expectedValue.Month, expectedValue.Day, expectedValue.Hour, expectedValue.Minute, expectedValue.Second);
      testvalue = new DateTime(testvalue.Year, testvalue.Month, testvalue.Day, testvalue.Hour, testvalue.Minute, testvalue.Second);

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests the functionality of <see cref="DateTimeProvider.Today"/>.</summary>
    [TestMethod]
    public void DateTimeProvider_TodayReturnsCurrentToday() {
      /* Create the test subject */
      DateTimeProvider testSubject = new DateTimeProvider();

      Assert.AreEqual(DateTime.Today, testSubject.Today);
    }

    /// <summary>Tests functionality of <see cref="DateTimeProvider.UtcNow"/>.</summary>
    [TestMethod]
    public void DateTimeProvider_UtcNowReturnsCurrentUtcNowValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new DateTimeProvider();

      DateTime testvalue = testSubject.UtcNow;
      DateTime expectedValue = DateTime.UtcNow;

      /* Because test executions are not always performed with the same speed, the milliseconds are not considered in the equation */
      expectedValue = new DateTime(expectedValue.Year, expectedValue.Month, expectedValue.Day, expectedValue.Hour, expectedValue.Minute, expectedValue.Second);
      testvalue = new DateTime(testvalue.Year, testvalue.Month, testvalue.Day, testvalue.Hour, testvalue.Minute, testvalue.Second);

      Assert.AreEqual(expectedValue, testvalue);
    }
  }
}
