//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DecimalModelBinderTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the DecimalModelBinder class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Enkoni.Framework.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Web.Tests {
  /// <summary>Tests the functionality of the <see cref="DecimalModelBinder"/> class.</summary>
  [TestClass]
  public class DecimalModelBinderTest {
    #region Testcases
    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// without a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelPositiveDecimalTwoDecimalsWithDefaultFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder();

      /* Test if the model binder can convert a default decimal */
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("2.13"));
      Assert.AreEqual(2.13M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// without a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelPositiveDecimalOneDecimalWithDefaultFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder();

      /* Test if the model binder can convert a decimal with one decimal position*/
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("2.1"));
      Assert.AreEqual(2.10M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// without a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelPositiveDecimalNoDecimalsWithDefaultFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder();

      /* Test if the model binder can convert a decimal without a decimal position*/
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("2"));
      Assert.AreEqual(2.00M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// without a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelNegativeDecimalTwoDecimalsWithDefaultFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder();

      /* Test if the model binder can convert a default negative decimal */
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("-2.13"));
      Assert.AreEqual(-2.13M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// without a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelNegativeDecimalOneDecimalWithDefaultFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder();

      /* Test if the model binder can convert a negative decimal with one decimal position*/
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("-2.1"));
      Assert.AreEqual(-2.10M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// without a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelNegativeDecimalNoDecimalsWithDefaultFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder();

      /* Test if the model binder can convert a negative decimal without a decimal position*/
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("-2"));
      Assert.AreEqual(-2.00M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// without a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelNullValueWithDefaultFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder();

      /* Test if the model binder can convert a negative decimal without a decimal position*/
      object result = testSubject.BindModel(null, CreateModelBindingContext(null));
      Assert.IsNull(result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// without a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelEmptyValueWithDefaultFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder();

      /* Test if the model binder can convert a negative decimal without a decimal position*/
      object result = testSubject.BindModel(null, CreateModelBindingContext(string.Empty));
      Assert.IsNull(result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// with a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelPositiveDecimalTwoDecimalsWithSpecificFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder(new CultureInfo("nl-NL"));

      /* Test if the model binder can convert a default decimal */
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("2,13"));
      Assert.AreEqual(2.13M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// with a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelPositiveDecimalOneDecimalWithSpecificFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder(new CultureInfo("nl-NL"));

      /* Test if the model binder can convert a decimal with one decimal position*/
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("2,1"));
      Assert.AreEqual(2.10M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// with a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelPositiveDecimalNoDecimalsWithSpecificFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder(new CultureInfo("nl-NL"));

      /* Test if the model binder can convert a decimal without a decimal position*/
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("2"));
      Assert.AreEqual(2.00M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// with a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelNegativeDecimalTwoDecimalsWithSpecificFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder(new CultureInfo("nl-NL"));

      /* Test if the model binder can convert a default negative decimal */
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("-2,13"));
      Assert.AreEqual(-2.13M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// with a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelNegativeDecimalOneDecimalWithSpecificFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder(new CultureInfo("nl-NL"));

      /* Test if the model binder can convert a negative decimal with one decimal position*/
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("-2,1"));
      Assert.AreEqual(-2.10M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// with a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelNegativeDecimalNoDecimalsWithSpecificFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder(new CultureInfo("nl-NL"));

      /* Test if the model binder can convert a negative decimal without a decimal position*/
      decimal result = (decimal)testSubject.BindModel(null, CreateModelBindingContext("-2"));
      Assert.AreEqual(-2.00M, result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// with a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelNullValueWithSpecificFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder(new CultureInfo("nl-NL"));

      /* Test if the model binder can convert a negative decimal without a decimal position*/
      object result = testSubject.BindModel(null, CreateModelBindingContext(null));
      Assert.IsNull(result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when initialized
    /// with a specific format provider.</summary>
    [TestMethod]
    public void DecimalModelBinder_BindModelEmptyValueWithSpecificFormatProvider_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      DecimalModelBinder testSubject = new DecimalModelBinder(new CultureInfo("nl-NL"));

      /* Test if the model binder can convert a negative decimal without a decimal position*/
      object result = testSubject.BindModel(null, CreateModelBindingContext(string.Empty));
      Assert.IsNull(result);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder(IFormatProvider)"/> when initialized
    /// with a null value.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void DecimalModelBinder_CtorWithNullFormatProvider_ExceptionIsThrown() {
      DecimalModelBinder testSubject = new DecimalModelBinder(null);
    }

    /// <summary>Tests the functionality of the <see cref="DecimalModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> when called with
    /// a null-modelbindingcontext.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void DecimalModelBinder_BindModelWithNullBindingContext_ExceptionIsThrown() {
      DecimalModelBinder testSubject = new DecimalModelBinder();

      testSubject.BindModel(null, null);
    }
    #endregion

    #region Private static helper methods
    /// <summary>Creates a <see cref="ModelBindingContext"/> that is used to pass to the model binder that is being tested.</summary>
    /// <param name="formValue">The value that is &quot;send&quot; to the binder.</param>
    /// <returns>The created context.</returns>
    private static ModelBindingContext CreateModelBindingContext(string formValue) {
      IValueProvider valueProvider = new DictionaryValueProvider<string>(new Dictionary<string, string> { { "SomeValue", formValue } }, null);
      ModelBindingContext bindingContext = new ModelBindingContext { ModelName = "SomeValue", ValueProvider = valueProvider };
      return bindingContext;
    }
    #endregion
  }
}
