using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="EntityValidator{T}"/> class.</summary>
  [TestClass]
  public class EntityValidatorTest {
    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformValidation(T)"/> method.</summary>
    [TestMethod]
    public void TestCase01_Validation_ValidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "zzzzabc12", NumericValue = 11, BooleanValue = false };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ICollection<ValidationResult> results = validator.PerformValidation(dummy);

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count);
    }

    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformValidation(T)"/> method.</summary>
    [TestMethod]
    public void TestCase02_Validation_InvalidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "zzzzabc12", NumericValue = 16, BooleanValue = null };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ICollection<ValidationResult> results = validator.PerformValidation(dummy);

      Assert.IsNotNull(results);
      Assert.AreEqual(2, results.Count);
    }

    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformValidation(T)"/> method.</summary>
    [TestMethod]
    public void TestCase03_Validation_ThrowOnError_ValidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "zzzzabc12", NumericValue = 11, BooleanValue = false };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ICollection<ValidationResult> results = validator.PerformValidation(dummy, true);

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count);
    }

    /// <summary>Tests the functionality of the <see cref="EntityValidator{T}.PerformValidation(T)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ValidationException))]
    public void TestCase04_Validation_ThrowOnError_InvalidEntity() {
      TestDummy dummy = new TestDummy { TextValue = "zzzzabc12", NumericValue = 16, BooleanValue = null };

      EntityValidator<TestDummy> validator = new EntityValidator<TestDummy>();
      ICollection<ValidationResult> results = validator.PerformValidation(dummy, true);
    }

    #region Private helper classes
    /// <summary>A helper class to support the testcases.</summary>
    private class TestDummy {
      /// <summary>Gets or sets a text value.</summary>
      [MinLength(2, ErrorMessage = "StringValue has wrong length")]
      [MaxLength(10, ErrorMessage = "StringValue has wrong length")]
      [RegularExpression("^.*abc\\d+$", ErrorMessage = "StringValue has wrong content")]
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      [Range(5, 15, ErrorMessage = "NumericValue has wrong value")]
      public int NumericValue { get; set; }

      /// <summary>Gets or sets an optional boolean value.</summary>
      [Required(ErrorMessage = "BooleanValue is mandatory")]
      public bool? BooleanValue { get; set; }
    }
    #endregion
  }
}
