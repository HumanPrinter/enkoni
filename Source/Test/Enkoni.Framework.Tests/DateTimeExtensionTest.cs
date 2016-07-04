using System;
using System.Globalization;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the DateTime-struct.</summary>
  [TestClass]
  public class DateTimeExtensionTest {
    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_GetWeekNumber2011_01_01ReturnsWeek52() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* January 1st 2011 is a Saturday and still is part of week 52 of 2010 */
      DateTime startValue = new DateTime(2011, 1, 1);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(52, result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_GetWeekNumber2011_01_02ReturnsWeek52() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* January 2nd 2011 is a Sunday (the last day of the week) and still is part of week 52 of 2010 */
      DateTime startValue = new DateTime(2011, 1, 2);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(52, result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_GetWeekNumber2011_01_03ReturnsWeek1() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* January 3rd 2011 is a Monday and is the first day of week 1 of 2011 */
      DateTime startValue = new DateTime(2011, 1, 3);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(1, result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_GetWeekNumber2011_05_09ReturnsWeek19() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* May 9th is an arbitrary Monday in 2011 */
      DateTime startValue = new DateTime(2011, 5, 9);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(19, result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_GetWeekNumber2011_05_15ReturnsWeek19() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* May 15th is an arbitrary Sunday in 2011 */
      DateTime startValue = new DateTime(2011, 5, 15);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(19, result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_GetWeekNumber2011_12_26ReturnsWeek52() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* December 26nd is the first day of the last week of 2011 */
      DateTime startValue = new DateTime(2011, 12, 26);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(52, result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_GetWeekNumber2011_12_31ReturnsWeek52() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* December 31st 2011 is a Saturday and is still part of week 52 of 2011 */
      DateTime startValue = new DateTime(2011, 12, 31);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(52, result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_GetWeekNumber2012_12_31ReturnsWeek1() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* December 31st 2012 is a Monday and is part of week 1 of 2012 */
      DateTime startValue = new DateTime(2012, 12, 31);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(1, result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_ValueBetweenBoundariesReturnsTrue() {
      DateTime inputValue = new DateTime(2014, 10, 16);
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_ValueBelowLowerBoundaryReturnsFalse() {
      DateTime inputValue = new DateTime(2014, 10, 14);
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_ValueEqualToLowerBoundaryReturnsFalse() {
      DateTime inputValue = new DateTime(2014, 10, 15);
      DateTime lowerBound = new DateTime(2014, 10, 15);
      DateTime upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_ValueAboveUpperBoundaryReturnsFalse() {
      DateTime inputValue = new DateTime(2014, 10, 18);
      DateTime lowerBound = new DateTime(2014, 10, 15);
      DateTime upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_ValueEqualToUpperBoundaryReturnsFalse() {
      DateTime inputValue = new DateTime(2014, 10, 17);
      DateTime lowerBound = new DateTime(2014, 10, 15);
      DateTime upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_ValueWithNullLowerBoundaryReturnsTrue() {
      DateTime inputValue = new DateTime(2014, 10, 16);
      DateTime? lowerBound = null;
      DateTime? upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_ValueWithNullUpperBoundaryReturnsTrue() {
      DateTime inputValue = new DateTime(2014, 10, 16);
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime? upperBound = null;

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_ValueWithNullLowerAndUpperBoundaryReturnsTrue() {
      DateTime inputValue = new DateTime(2014, 10, 16);
      DateTime? lowerBound = null;
      DateTime? upperBound = null;

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullableValueBetweenBoundariesReturnsTrue() {
      DateTime? inputValue = new DateTime(2014, 10, 16);
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime? upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullableValueBelowLowerBoundaryReturnsFalse() {
      DateTime? inputValue = new DateTime(2014, 10, 14);
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime? upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullableValueEqualToLowerBoundaryReturnsFalse() {
      DateTime? inputValue = new DateTime(2014, 10, 15);
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime? upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullableValueAboveUpperBoundaryReturnsFalse() {
      DateTime? inputValue = new DateTime(2014, 10, 18);
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime? upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullableValueEqualToUpperBoundaryReturnsFalse() {
      DateTime? inputValue = new DateTime(2014, 10, 17);
      DateTime lowerBound = new DateTime(2014, 10, 15);
      DateTime upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullableValueWithNullLowerBoundaryReturnsTrue() {
      DateTime? inputValue = new DateTime(2014, 10, 16);
      DateTime? lowerBound = null;
      DateTime? upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullableValueWithNullUpperBoundaryReturnsTrue() {
      DateTime? inputValue = new DateTime(2014, 10, 16);
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime? upperBound = null;

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullValueBetweenBoundariesReturnsFalse() {
      DateTime? inputValue = null;
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime? upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullValueWithNullLowerBoundaryReturnsTrue() {
      DateTime? inputValue = null;
      DateTime? lowerBound = null;
      DateTime? upperBound = new DateTime(2014, 10, 17);

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullValueWithNullUpperBoundaryReturnsFalse() {
      DateTime? inputValue = null;
      DateTime? lowerBound = new DateTime(2014, 10, 15);
      DateTime? upperBound = null;

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DateTimeExtensions.Between(DateTime, Nullable{DateTime}, Nullable{DateTime})"/> extension method.</summary>
    [TestMethod]
    public void DateTimeExtensions_Between_NullValueWithNullLowerAndUpperBoundaryReturnsTrue() {
      DateTime? inputValue = null;
      DateTime? lowerBound = null;
      DateTime? upperBound = null;

      bool result = inputValue.Between(lowerBound, upperBound);
      Assert.IsTrue(result);
    }
  }
}
