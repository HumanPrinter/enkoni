//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="EntityValidatorTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the EntityValidator class.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OscarBrouwer.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="EntityValidator{T}"/> class.</summary>
  [TestClass]
  public class EntityValidatorTest {
    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformShallowValidation(T)"/> method using
    /// an object that should pass the shallow validation, but not the deep validation.</summary>
    [TestMethod]
    public void TestCase01_ShallowValidation_ValidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "xxyyyzzzz", NumericValue = 11, BooleanValue = false };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ValidationResults results = validator.PerformShallowValidation(dummy);

      Assert.IsNotNull(results);
      Assert.IsTrue(results.IsValid);
      Assert.AreEqual(0, results.Count);
    }

    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformShallowValidation(T)"/> method using
    /// an object that should not pass the shallow validation.</summary>
    [TestMethod]
    public void TestCase02_ShallowValidation_InvalidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "xxyyyzzzz", NumericValue = 16, BooleanValue = null };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ValidationResults results = validator.PerformShallowValidation(dummy);

      Assert.IsNotNull(results);
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(2, results.Count);
      Assert.AreEqual("NumericValue", results.First().Key, true);
      Assert.IsInstanceOfType(results.First().Validator, typeof(RangeValidator));
      Assert.AreEqual("BooleanValue", results.Last().Key, true);
      Assert.IsInstanceOfType(results.Last().Validator, typeof(NotNullValidator));
    }

    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformDeepValidation(T)"/> method using
    /// an object that should pass the shallow validation and the deep validation.</summary>
    [TestMethod]
    public void TestCase03_DeepValidation_ValidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "xxabc1234", NumericValue = 9, BooleanValue = true };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ValidationResults results = validator.PerformDeepValidation(dummy);

      Assert.IsNotNull(results);
      Assert.IsTrue(results.IsValid);
      Assert.AreEqual(0, results.Count);
    }

    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformDeepValidation(T)"/> method using
    /// an object that should not pass the shallow validation.</summary>
    [TestMethod]
    public void TestCase04_DeepValidation_ShallowInvalidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "xxyyyzzzz", NumericValue = 16, BooleanValue = null };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ValidationResults results = validator.PerformDeepValidation(dummy);

      Assert.IsNotNull(results);
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(2, results.Count);
      Assert.AreEqual("NumericValue", results.First().Key, true);
      Assert.IsInstanceOfType(results.First().Validator, typeof(RangeValidator));
      Assert.AreEqual("BooleanValue", results.Last().Key, true);
      Assert.IsInstanceOfType(results.Last().Validator, typeof(NotNullValidator));
    }

    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformDeepValidation(T)"/> method using
    /// an object that should pass the shallow validation, but not the deep validation.</summary>
    [TestMethod]
    public void TestCase05_DeepValidation_DeepInvalidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "xxyyyzzzz", NumericValue = 9, BooleanValue = false };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ValidationResults results = validator.PerformDeepValidation(dummy);

      Assert.IsNotNull(results);
      Assert.IsFalse(results.IsValid);
      Assert.AreEqual(2, results.Count);
      Assert.AreEqual("TextValue", results.First().Key, true);
      Assert.IsInstanceOfType(results.First().Validator, typeof(RegexValidator));
      Assert.AreEqual("BooleanValue", results.Last().Key, true);
    }

    #region Private helper classes
    /// <summary>A helper class to support the testcases.</summary>
    [HasSelfValidation]
    private class TestDummy {
      /// <summary>Gets or sets a text value.</summary>
      [StringLengthValidator(2, 10, Ruleset="shallow", ErrorMessage = "StringValue has wrong length")]
      [RegexValidator("^.*abc\\d+$", RegexOptions.Compiled|RegexOptions.Singleline, Ruleset="deep", ErrorMessage="StringValue has wrong content")]
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      [RangeValidator(5, RangeBoundaryType.Inclusive, 15, RangeBoundaryType.Exclusive, Ruleset = "shallow", ErrorMessage = "NumericValue has wrong value")]
      [RangeValidator(8, RangeBoundaryType.Inclusive, 10, RangeBoundaryType.Exclusive, Ruleset = "deep", ErrorMessage = "NumericValue has illegal value")]
      public int NumericValue { get; set; }

      /// <summary>Gets or sets an optional boolean value.</summary>
      [NotNullValidator(Ruleset = "shallow", ErrorMessage = "BooleanValue is mandatory")]
      public bool? BooleanValue { get; set; }

      [SelfValidation(Ruleset = "deep")]
      private void CustomValidate(ValidationResults results) {
        if(this.BooleanValue.Value == false) {
          results.AddResult(new ValidationResult("BooleanValue has wrong value", this, "BooleanValue", null, null));
        }
      }
    }
    #endregion
  }
}
