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
    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using inclusive and exclusive boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_InclusiveExclusive() {
      TimeSpanRangeValidator testSubject = new TimeSpanRangeValidator(new TimeSpan(0, 10, 0), RangeBoundaryType.Inclusive, new TimeSpan(0, 59, 45), RangeBoundaryType.Exclusive, false);

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValid = new TimeSpan(0, 10, 0);
      TimeSpan inputUpperBoundValid = new TimeSpan(0, 59, 44);
      TimeSpan inputLowerBoundInvalidAtBoundary = new TimeSpan(0, 9, 59);
      TimeSpan inputLowerBoundInvalidBelowBoundary = new TimeSpan(0, 5, 12);
      TimeSpan inputUpperBoundInvalidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundInvalidAboveBoundary = new TimeSpan(1, 20, 42);
      
      EntLib.ValidationResults results = testSubject.Validate(inputInBetween);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputLowerBoundValid);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputUpperBoundValid);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputLowerBoundInvalidAtBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputLowerBoundInvalidBelowBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputUpperBoundInvalidAtBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputUpperBoundInvalidAboveBoundary);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using inclusive and exclusive boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_InclusiveExclusive_Negate() {
      TimeSpanRangeValidator testSubject = new TimeSpanRangeValidator(new TimeSpan(0, 10, 0), RangeBoundaryType.Inclusive, new TimeSpan(0, 59, 45), RangeBoundaryType.Exclusive, true);

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValid = new TimeSpan(0, 10, 0);
      TimeSpan inputUpperBoundValid = new TimeSpan(0, 59, 44);
      TimeSpan inputLowerBoundInvalidAtBoundary = new TimeSpan(0, 9, 59);
      TimeSpan inputLowerBoundInvalidBelowBoundary = new TimeSpan(0, 5, 12);
      TimeSpan inputUpperBoundInvalidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundInvalidAboveBoundary = new TimeSpan(1, 20, 42);

      EntLib.ValidationResults results = testSubject.Validate(inputInBetween);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputLowerBoundValid);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputUpperBoundValid);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputLowerBoundInvalidAtBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputLowerBoundInvalidBelowBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputUpperBoundInvalidAtBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputUpperBoundInvalidAboveBoundary);
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using exclusive and ignore boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_ExclusiveIgnore() {
      TimeSpanRangeValidator testSubject = new TimeSpanRangeValidator(new TimeSpan(0, 10, 0), RangeBoundaryType.Exclusive, new TimeSpan(0, 59, 45), RangeBoundaryType.Ignore, false);

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValid = new TimeSpan(0, 10, 1);
      TimeSpan inputUpperBoundValidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundValidAboveBoundary = new TimeSpan(1, 20, 42);
      TimeSpan inputLowerBoundInvalidAtBoundary = new TimeSpan(0, 10, 0);
      TimeSpan inputLowerBoundInvalidBelowBoundary = new TimeSpan(0, 5, 12);

      EntLib.ValidationResults results = testSubject.Validate(inputInBetween);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputLowerBoundValid);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputUpperBoundValidAtBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputUpperBoundValidAboveBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputLowerBoundInvalidAtBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputLowerBoundInvalidBelowBoundary);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using exclusive and ignore boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_ExclusiveIgnore_Negate() {
      TimeSpanRangeValidator testSubject = new TimeSpanRangeValidator(new TimeSpan(0, 10, 0), RangeBoundaryType.Exclusive, new TimeSpan(0, 59, 45), RangeBoundaryType.Ignore, true);

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValid = new TimeSpan(0, 10, 1);
      TimeSpan inputUpperBoundValidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundValidAboveBoundary = new TimeSpan(1, 20, 42);
      TimeSpan inputLowerBoundInvalidAtBoundary = new TimeSpan(0, 10, 0);
      TimeSpan inputLowerBoundInvalidBelowBoundary = new TimeSpan(0, 5, 12);

      EntLib.ValidationResults results = testSubject.Validate(inputInBetween);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputLowerBoundValid);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputUpperBoundValidAtBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputUpperBoundValidAboveBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputLowerBoundInvalidAtBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputLowerBoundInvalidBelowBoundary);
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using ignore and inclusive boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_IgnoreInclusive() {
      TimeSpanRangeValidator testSubject = new TimeSpanRangeValidator(new TimeSpan(0, 10, 0), RangeBoundaryType.Ignore, new TimeSpan(0, 59, 45), RangeBoundaryType.Inclusive, false);

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValidAtBoundary = new TimeSpan(0, 10, 0);
      TimeSpan inputLowerBoundValidBelowBoundary = new TimeSpan(0, 5, 15);
      TimeSpan inputUpperBoundValidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundInvalidAboveBoundary = new TimeSpan(1, 20, 42);
      
      EntLib.ValidationResults results = testSubject.Validate(inputInBetween);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputLowerBoundValidAtBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputLowerBoundValidBelowBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputUpperBoundValidAtBoundary);
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate(inputUpperBoundInvalidAboveBoundary);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class using ignore and inclusive boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_IgnoreInclusive_Negate() {
      TimeSpanRangeValidator testSubject = new TimeSpanRangeValidator(new TimeSpan(0, 10, 0), RangeBoundaryType.Ignore, new TimeSpan(0, 59, 45), RangeBoundaryType.Inclusive, true);

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValidAtBoundary = new TimeSpan(0, 10, 0);
      TimeSpan inputLowerBoundValidBelowBoundary = new TimeSpan(0, 5, 15);
      TimeSpan inputUpperBoundValidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundInvalidAboveBoundary = new TimeSpan(1, 20, 42);

      EntLib.ValidationResults results = testSubject.Validate(inputInBetween);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputLowerBoundValidAtBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputLowerBoundValidBelowBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputUpperBoundValidAtBoundary);
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate(inputUpperBoundInvalidAboveBoundary);
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidatorAttribute"/> class using inclusive and exclusive boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_Attribute_InclusiveExclusive() {
      TestDummy_InclusiveExclusive dummy = new TestDummy_InclusiveExclusive();

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValid = new TimeSpan(0, 10, 0);
      TimeSpan inputUpperBoundValid = new TimeSpan(0, 59, 44);
      TimeSpan inputLowerBoundInvalidAtBoundary = new TimeSpan(0, 9, 59);
      TimeSpan inputLowerBoundInvalidBelowBoundary = new TimeSpan(0, 5, 12);
      TimeSpan inputUpperBoundInvalidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundInvalidAboveBoundary = new TimeSpan(1, 20, 42);

      dummy.TimeSpanValue = inputInBetween;
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundValid;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundValid;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundInvalidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundInvalidBelowBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundInvalidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundInvalidAboveBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidatorAttribute"/> class using inclusive and exclusive boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_Attribute_InclusiveExclusive_Negate() {
      TestDummy_InclusiveExclusive_Negate dummy = new TestDummy_InclusiveExclusive_Negate();

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValid = new TimeSpan(0, 10, 0);
      TimeSpan inputUpperBoundValid = new TimeSpan(0, 59, 44);
      TimeSpan inputLowerBoundInvalidAtBoundary = new TimeSpan(0, 9, 59);
      TimeSpan inputLowerBoundInvalidBelowBoundary = new TimeSpan(0, 5, 12);
      TimeSpan inputUpperBoundInvalidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundInvalidAboveBoundary = new TimeSpan(1, 20, 42);

      dummy.TimeSpanValue = inputInBetween;
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundValid;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundValid;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundInvalidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundInvalidBelowBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundInvalidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundInvalidAboveBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidatorAttribute"/> class using exclusive and ignore boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_Attribute_ExclusiveIgnore() {
      TestDummy_ExclusiveIgnore dummy = new TestDummy_ExclusiveIgnore();

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValid = new TimeSpan(0, 10, 1);
      TimeSpan inputUpperBoundValidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundValidAboveBoundary = new TimeSpan(1, 20, 42);
      TimeSpan inputLowerBoundInvalidAtBoundary = new TimeSpan(0, 10, 0);
      TimeSpan inputLowerBoundInvalidBelowBoundary = new TimeSpan(0, 5, 12);

      dummy.TimeSpanValue = inputInBetween;
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundValid;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundValidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundValidAboveBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundInvalidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundInvalidBelowBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidatorAttribute"/> class using exclusive and ignore boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_Attribute_ExclusiveIgnore_Negate() {
      TestDummy_ExclusiveIgnore_Negate dummy = new TestDummy_ExclusiveIgnore_Negate();

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValid = new TimeSpan(0, 10, 1);
      TimeSpan inputUpperBoundValidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundValidAboveBoundary = new TimeSpan(1, 20, 42);
      TimeSpan inputLowerBoundInvalidAtBoundary = new TimeSpan(0, 10, 0);
      TimeSpan inputLowerBoundInvalidBelowBoundary = new TimeSpan(0, 5, 12);

      dummy.TimeSpanValue = inputInBetween;
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundValid;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundValidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundValidAboveBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundInvalidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundInvalidBelowBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidatorAttribute"/> class using ignore and inclusive boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_Attribute_IgnoreInclusive() {
      TestDummy_IgnoreInclusive dummy = new TestDummy_IgnoreInclusive();

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValidAtBoundary = new TimeSpan(0, 10, 0);
      TimeSpan inputLowerBoundValidBelowBoundary = new TimeSpan(0, 5, 15);
      TimeSpan inputUpperBoundValidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundInvalidAboveBoundary = new TimeSpan(1, 20, 42);

      dummy.TimeSpanValue = inputInBetween;
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundValidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundValidBelowBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundValidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundInvalidAboveBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidatorAttribute"/> class using ignore and inclusive boundary types.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_Attribute_IgnoreInclusive_Negate() {
      TestDummy_IgnoreInclusive_Negate dummy = new TestDummy_IgnoreInclusive_Negate();

      TimeSpan inputInBetween = new TimeSpan(0, 30, 0);
      TimeSpan inputLowerBoundValidAtBoundary = new TimeSpan(0, 10, 0);
      TimeSpan inputLowerBoundValidBelowBoundary = new TimeSpan(0, 5, 15);
      TimeSpan inputUpperBoundValidAtBoundary = new TimeSpan(0, 59, 45);
      TimeSpan inputUpperBoundInvalidAboveBoundary = new TimeSpan(1, 20, 42);

      dummy.TimeSpanValue = inputInBetween;
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundValidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputLowerBoundValidBelowBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundValidAtBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.TimeSpanValue = inputUpperBoundInvalidAboveBoundary;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="TimeSpanRangeValidator"/> class when applied to a non-TimeSpan attribute.</summary>
    [TestMethod]
    public void TimeSpanRangeValidator_InvalidPropertyType() {
      TestDummy_InvalidPropertyType dummy = new TestDummy_InvalidPropertyType();
      dummy.TextValue = "00:30:00";
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(1, results.Count);
    }
    #endregion

    #region Inner test classes
    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_InclusiveExclusive {
      /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
      [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Inclusive, "00:59:45", RangeBoundaryType.Exclusive, Ruleset = "ValidationTest",
        MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
      public TimeSpan TimeSpanValue { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_InclusiveExclusive_Negate {
      /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
      [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Inclusive, "00:59:45", RangeBoundaryType.Exclusive, Negated = true, 
        Ruleset = "ValidationTest", MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
      public TimeSpan TimeSpanValue { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_ExclusiveIgnore {
      /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
      [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Exclusive, "00:59:45", RangeBoundaryType.Ignore, Ruleset = "ValidationTest",
        MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
      public TimeSpan TimeSpanValue { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_ExclusiveIgnore_Negate {
      /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
      [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Exclusive, "00:59:45", RangeBoundaryType.Ignore, Negated = true, Ruleset = "ValidationTest",
        MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
      public TimeSpan TimeSpanValue { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_IgnoreInclusive {
      /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
      [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Ignore, "00:59:45", RangeBoundaryType.Inclusive, Ruleset = "ValidationTest",
        MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
      public TimeSpan TimeSpanValue { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_IgnoreInclusive_Negate {
      /// <summary>Gets or sets a <see cref="TimeSpan"/> value.</summary>
      [TimeSpanRangeValidator("00:10:00", RangeBoundaryType.Ignore, "00:59:45", RangeBoundaryType.Inclusive, Ruleset = "ValidationTest", Negated = true,
        MessageTemplate = "The property {1} has an invalid value ('{0}'). It must be between '{3}' ({4}) and '{5}' ({6})")]
      public TimeSpan TimeSpanValue { get; set; }
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
}
