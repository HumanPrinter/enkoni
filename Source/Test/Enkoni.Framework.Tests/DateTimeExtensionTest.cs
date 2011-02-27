//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeExtensionTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the extension methods for the DateTime-struct.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Globalization;
using System.Threading;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the DateTime-struct.</summary>
  [TestClass]
  public class DateTimeExtensionTest {
    /// <summary>Tests the functionality of the <see cref="Extensions.GetWeekNumber(DateTime)"/> extension method.</summary>
    [TestMethod]
    public void TestCase01_GetWeekNumberNoCulture() {
      Thread.CurrentThread.CurrentCulture = new CultureInfo("nl-NL");

      /* January 1st 2011 is a Saturday and still is part of week 52 of 2010 */
      DateTime startValue = new DateTime(2011, 1, 1);
      int result = startValue.GetWeekNumber();
      Assert.AreEqual(52, result);

      /* January 2nd 2011 is a Sunday (the last day of the week) and still is part of week 52 of 2010 */
      startValue = new DateTime(2011, 1, 2);
      result = startValue.GetWeekNumber();
      Assert.AreEqual(52, result);

      /* January 3rd 2011 is a Monday and is the first day of week 1 of 2011 */
      startValue = new DateTime(2011, 1, 3);
      result = startValue.GetWeekNumber();
      Assert.AreEqual(1, result);

      /* May 9th is an arbitrary Monday in 2011 */
      startValue = new DateTime(2011, 5, 9);
      result = startValue.GetWeekNumber();
      Assert.AreEqual(19, result);

      /* May 15th is an arbitrary Sunday in 2011 */
      startValue = new DateTime(2011, 5, 15);
      result = startValue.GetWeekNumber();
      Assert.AreEqual(19, result);

      /* December 26nd is the first day of the last week of 2011 */
      startValue = new DateTime(2011, 12, 26);
      result = startValue.GetWeekNumber();
      Assert.AreEqual(52, result);

      /* December 31st 2011 is a Saturday and is still part of week 52 of 2011 */
      startValue = new DateTime(2011, 12, 31);
      result = startValue.GetWeekNumber();
      Assert.AreEqual(52, result);

      /* December 31st 2012 is a Monday and is part of week 1 of 2012 */
      startValue = new DateTime(2012, 12, 31);
      result = startValue.GetWeekNumber();
      Assert.AreEqual(1, result);
    }
  }
}
