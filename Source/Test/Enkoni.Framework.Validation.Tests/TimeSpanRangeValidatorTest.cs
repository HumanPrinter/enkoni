//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeSpanRangeValidatorTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the TimeSpanRangeValidator class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

using Enkoni.Framework.Validation.Validators;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntLib = Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Validation.Tests {
  /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> and 
  /// <see cref="TimeSpanRangeValidatorAttribute"/> classes.</summary>
  [TestClass]
  public class TimeSpanRangeValidatorTest {
    #region TestCases
    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using values that should easily pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase01_ValidValues() {
      /* Use valid values in combination with an Inclusive-Exclusive boundary combination. */
      TestDummy_InclusiveExclusive dummyA = new TestDummy_InclusiveExclusive();
      dummyA.TimeSpanValueA = new TimeSpan(0, 30, 0);
      dummyA.TimeSpanValueB = new TimeSpan(0, 5, 0);

      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_InclusiveExclusive>(dummyA, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      /* Use valid values in combination with an Exclusive-Ignore boundary combination. */
      TestDummy_ExclusiveIgnore dummyB = new TestDummy_ExclusiveIgnore();
      dummyB.TimeSpanValueA = new TimeSpan(0, 30, 0);
      dummyB.TimeSpanValueB = new TimeSpan(0, 5, 0);

      results = EntLib.Validation.Validate<TestDummy_ExclusiveIgnore>(dummyB, "ValidationTest");
      Assert.IsTrue(results.IsValid);
      /* Make sure that the upper-boundary is correctly ignored. */
      dummyB.TimeSpanValueA = new TimeSpan(1, 30, 0);

      results = EntLib.Validation.Validate<TestDummy_ExclusiveIgnore>(dummyB, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      /* Use valid values in combination with an Ignore-Inclusive boundary combination. */
      TestDummy_IgnoreInclusive dummyC = new TestDummy_IgnoreInclusive();
      dummyC.TimeSpanValueA = new TimeSpan(0, 30, 0);
      dummyC.TimeSpanValueB = new TimeSpan(1, 30, 0);

      results = EntLib.Validation.Validate<TestDummy_IgnoreInclusive>(dummyC, "ValidationTest");
      Assert.IsTrue(results.IsValid);
      /* Make sure that the lower-boundary is correctly ignored. */
      dummyC.TimeSpanValueA = new TimeSpan(0, 5, 0);
      results = EntLib.Validation.Validate<TestDummy_IgnoreInclusive>(dummyC, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using values that should just pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase02_JustValidValues() {
      /* Use valid values in combination with an Inclusive-Exclusive boundary combination. */
      TestDummy_InclusiveExclusive dummyA = new TestDummy_InclusiveExclusive();
      /* Test the lower bound */
      dummyA.TimeSpanValueA = new TimeSpan(0, 10, 0);
      dummyA.TimeSpanValueB = new TimeSpan(0, 9, 59);

      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_InclusiveExclusive>(dummyA, "ValidationTest");
      Assert.IsTrue(results.IsValid);
      /* Test the upper bound */
      dummyA.TimeSpanValueA = new TimeSpan(0, 59, 44);
      dummyA.TimeSpanValueB = new TimeSpan(0, 59, 45);
      results = EntLib.Validation.Validate<TestDummy_InclusiveExclusive>(dummyA, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      /* Use valid values in combination with an Exclusive-Ignore boundary combination. */
      TestDummy_ExclusiveIgnore dummyB = new TestDummy_ExclusiveIgnore();
      /* Test the lower bound */
      dummyB.TimeSpanValueA = new TimeSpan(0, 10, 1);
      dummyB.TimeSpanValueB = new TimeSpan(0, 10, 0);

      results = EntLib.Validation.Validate<TestDummy_ExclusiveIgnore>(dummyB, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      /* Use valid values in combination with an Ignore-Inclusive boundary combination. */
      TestDummy_IgnoreInclusive dummyC = new TestDummy_IgnoreInclusive();
      /* Test the upper bound */
      dummyC.TimeSpanValueA = new TimeSpan(0, 59, 45);
      dummyC.TimeSpanValueB = new TimeSpan(0, 59, 46);

      results = EntLib.Validation.Validate<TestDummy_IgnoreInclusive>(dummyC, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using values that should not pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase03_InvalidValues() {
      /* Use valid values in combination with an Inclusive-Exclusive boundary combination. */
      TestDummy_InclusiveExclusive dummyA = new TestDummy_InclusiveExclusive();
      /* Test the lower bound */
      dummyA.TimeSpanValueA = new TimeSpan(0, 9, 59);
      dummyA.TimeSpanValueB = new TimeSpan(0, 10, 00);

      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_InclusiveExclusive>(dummyA, "ValidationTest");
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(2, results.Count);

      /* Test the upper bound */
      dummyA.TimeSpanValueA = new TimeSpan(0, 59, 45);
      dummyA.TimeSpanValueB = new TimeSpan(0, 59, 44);
      results = EntLib.Validation.Validate<TestDummy_InclusiveExclusive>(dummyA, "ValidationTest");
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(2, results.Count);

      /* Use valid values in combination with an Exclusive-Ignore boundary combination. */
      TestDummy_ExclusiveIgnore dummyB = new TestDummy_ExclusiveIgnore();
      /* Test the lower bound */
      dummyB.TimeSpanValueA = new TimeSpan(0, 10, 0);
      dummyB.TimeSpanValueB = new TimeSpan(0, 10, 1);

      results = EntLib.Validation.Validate<TestDummy_ExclusiveIgnore>(dummyB, "ValidationTest");
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(2, results.Count);

      /* Use valid values in combination with an Ignore-Inclusive boundary combination. */
      TestDummy_IgnoreInclusive dummyC = new TestDummy_IgnoreInclusive();
      /* Test the upper bound */
      dummyC.TimeSpanValueA = new TimeSpan(0, 59, 46);
      dummyC.TimeSpanValueB = new TimeSpan(0, 59, 45);

      results = EntLib.Validation.Validate<TestDummy_IgnoreInclusive>(dummyC, "ValidationTest");
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(2, results.Count);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class when applied to a non-TimeSpan attribute.</summary>
    [TestMethod]
    public void TestCase04_InvalidPropertyType() {
      TestDummy_InvalidPropertyType dummy = new TestDummy_InvalidPropertyType();
      dummy.TextValue = "00:30:00";
      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_InvalidPropertyType>(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(1, results.Count);
    }
    #endregion
  }

  #region Helper classes
  /// <summary>A helper class to support the testcases.</summary>
  public class TestDummy_InclusiveExclusive {
    /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
    [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Inclusive, "00:59:45", RangeBoundaryType.Exclusive, Ruleset = "ValidationTest",
      MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
    public TimeSpan TimeSpanValueA { get; set; }

    /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
    [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Inclusive, "00:59:45", RangeBoundaryType.Exclusive, Negated = true,
      Ruleset = "ValidationTest", MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
    public TimeSpan TimeSpanValueB { get; set; }
  }

  /// <summary>A helper class to support the testcases.</summary>
  public class TestDummy_ExclusiveIgnore {
    /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
    [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Exclusive, "00:59:45", RangeBoundaryType.Ignore, Ruleset = "ValidationTest",
      MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
    public TimeSpan TimeSpanValueA { get; set; }

    /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
    [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Exclusive, "00:59:45", RangeBoundaryType.Ignore, Negated = true,
      Ruleset = "ValidationTest", MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
    public TimeSpan TimeSpanValueB { get; set; }
  }

  /// <summary>A helper class to support the testcases.</summary>
  public class TestDummy_IgnoreInclusive {
    /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
    [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Ignore, "00:59:45", RangeBoundaryType.Inclusive, Ruleset = "ValidationTest",
      MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
    public TimeSpan TimeSpanValueA { get; set; }

    /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
    [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Ignore, "00:59:45", RangeBoundaryType.Inclusive, Negated = true,
      Ruleset = "ValidationTest", MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
    public TimeSpan TimeSpanValueB { get; set; }
  }

  /// <summary>A helper class to support the testcases.</summary>
  public class TestDummy_InvalidPropertyType {
    /// <summary>Gets or sets a <see langword="string"/> value.</summary>
    [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Ignore, "00:59:45", RangeBoundaryType.Inclusive, Ruleset = "ValidationTest",
      MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
    public string TextValue { get; set; }
  }
  #endregion
}
