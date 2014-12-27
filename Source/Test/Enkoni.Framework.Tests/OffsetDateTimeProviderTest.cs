using System;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="OffsetDateTimeProvider"/> class.</summary>
  [TestClass]
  public class OffsetDateTimeProviderTest {
    /// <summary>Tests the functionality of <see cref="DateTimeProvider.MaxValue"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_MaxValueReturnsActualMaxValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new OffsetDateTimeProvider(new TimeSpan(2, 35, 42));

      Assert.AreEqual(DateTime.MaxValue, testSubject.MaxValue);
    }

    /// <summary>Tests the functionality of <see cref="DateTimeProvider.MinValue"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_MinValueReturnsActualMinValue() {
      /* Create the test subject */
      DateTimeProvider testSubject = new OffsetDateTimeProvider(new TimeSpan(2, 35, 42));

      Assert.AreEqual(DateTime.MinValue, testSubject.MinValue);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider.Now"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_RegularPositiveOffset_NowReturnsExpectedValue() {
      /* Create the test subject */
      TimeSpan offset = new TimeSpan(2, 35, 42);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.Now;
      DateTime expectedValue = DateTime.Now.AddHours(2).AddMinutes(35).AddSeconds(42);

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests the functionality of <see cref="OffsetDateTimeProvider.Today"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_RegularPositiveOffset_TodayReturnsExpectedValue() {
      /* Create the test subject */
      TimeSpan offset = new TimeSpan(2, 35, 42);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.Today;
      DateTime expectedValue = DateTime.Now.AddHours(2).AddMinutes(35).AddSeconds(42).Date;

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider.UtcNow"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_RegularPositiveOffset_UtcNowReturnsExpectedValue() {
      /* Create the test subject */
      TimeSpan offset = new TimeSpan(2, 35, 42);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.UtcNow;
      DateTime expectedValue = DateTime.UtcNow.AddHours(2).AddMinutes(35).AddSeconds(42);

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider.Now"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_RegularNegativeOffset_NowReturnsExpectedValue() {
      /* Create the test subject */
      TimeSpan offset = new TimeSpan(-2, 35, 42);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.Now;
      DateTime expectedValue = DateTime.Now.AddHours(-2).AddMinutes(35).AddSeconds(42);

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests the functionality of <see cref="OffsetDateTimeProvider.Today"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_RegularNegativeOffset_TodayReturnsExpectedValue() {
      /* Create the test subject */
      TimeSpan offset = new TimeSpan(-2, 35, 42);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.Today;
      DateTime expectedValue = DateTime.Now.AddHours(-2).AddMinutes(35).AddSeconds(42).Date;

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider.UtcNow"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_RegularNegativeOffset_UtcNowReturnsExpectedValue() {
      /* Create the test subject */
      TimeSpan offset = new TimeSpan(-2, 35, 42);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.UtcNow;
      DateTime expectedValue = DateTime.UtcNow.AddHours(-2).AddMinutes(35).AddSeconds(42);

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider.Now"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_ZeroOffset_NowReturnsCurrentNow() {
      /* Create the test subject */
      TimeSpan offset = TimeSpan.Zero;
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.Now;
      DateTime expectedValue = DateTime.Now;

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests the functionality of <see cref="OffsetDateTimeProvider.Today"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_ZeroOffset_TodayReturnsCurrentToday() {
      /* Create the test subject */
      TimeSpan offset = TimeSpan.Zero;
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.Today;
      DateTime expectedValue = DateTime.Today;

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider.UtcNow"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_ZeroOffset_UtcNowReturnsCurrentUtcNow() {
      /* Create the test subject */
      TimeSpan offset = TimeSpan.Zero;
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      DateTime testvalue = testSubject.UtcNow;
      DateTime expectedValue = DateTime.UtcNow;

      Assert.AreEqual(expectedValue, testvalue);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider(TimeSpan)"/>.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void OffsetDateTimeProvider_MaxOffset_ConstructorThrowsException() {
      /* Create the test subject */
      TimeSpan offset = TimeSpan.MaxValue;
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider(TimeSpan)"/>.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void OffsetDateTimeProvider_MinOffset_ConstructorThrowsException() {
      /* Create the test subject */
      TimeSpan offset = TimeSpan.MinValue;
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider(TimeSpan)"/>.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void OffsetDateTimeProvider_TooLargePositiveOffset_ConstructorThrowsException() {
      /* Create the test subject */
      TimeSpan maximumAllowedOffset = DateTime.MaxValue - DateTime.Now;
      TimeSpan offset = new TimeSpan(maximumAllowedOffset.Ticks + 2500);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider(TimeSpan)"/>.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void OffsetDateTimeProvider_TooLargeNegativeOffset_ConstructorThrowsException() {
      /* Create the test subject */
      TimeSpan maximumAllowedOffset = DateTime.Now - DateTime.MinValue;
      TimeSpan offset = new TimeSpan((maximumAllowedOffset.Ticks * -1) - 2500);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider(TimeSpan)"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_MaximumAllowedPositiveOffset_ConstructorDoesNotThrowException() {
      /* Create the test subject */
      TimeSpan maximumAllowedOffset = DateTime.MaxValue - DateTime.Now;
      /* Build in a small safety margin */
      TimeSpan offset = new TimeSpan(maximumAllowedOffset.Ticks - 10);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      Assert.IsNotNull(testSubject);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider(TimeSpan)"/>.</summary>
    [TestMethod]
    public void OffsetDateTimeProvider_MaximumAllowedNegativeOffset_ConstructorDoesNotThrowException() {
      /* Create the test subject */
      TimeSpan maximumAllowedOffset = DateTime.Now - DateTime.MinValue;
      /* Build in a small safety margin */
      TimeSpan offset = new TimeSpan(maximumAllowedOffset.Ticks + 10);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      Assert.IsNotNull(testSubject);
    }

    /// <summary>Tests functionality of <see cref="OffsetDateTimeProvider(TimeSpan)"/>.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void OffsetDateTimeProvider_MaximumAllowedPositiveOffset_NowThrowsException() {
      /* Create the test subject */
      TimeSpan maximumAllowedOffset = DateTime.MaxValue - DateTime.Now;
      /* Build in a small safety margin */
      TimeSpan offset = new TimeSpan(maximumAllowedOffset.Ticks - 10);
      DateTimeProvider testSubject = new OffsetDateTimeProvider(offset);

      /* Wait a small amount of time (at least ten ticks) */
      Thread.Sleep(50);

      DateTime testvalue = testSubject.Now;
    }
  }
}
