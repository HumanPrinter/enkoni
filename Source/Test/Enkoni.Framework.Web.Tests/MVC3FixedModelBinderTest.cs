//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Mvc3FixedModelBinderTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the Mvc3FixedModelBinder class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.Mvc;
using Enkoni.Framework.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Web.Tests {
  /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder"/> class.</summary>
  [TestClass]
  public class Mvc3FixedModelBinderTest {
    #region Private helper enums
    /// <summary>A basic dummy enum to support the testcases.</summary>
    private enum TestEnum {
      ValueA = 0,
      ValueB = 1,
      ValueC = 3
    }
    #endregion

    #region Testcases
    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the 
    /// default behavior still works.</summary>
    [TestMethod]
    public void TestCase01_BindModel_DefaultBehavior() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert an integer */
      int integerResult = (int)testSubject.BindModel(controllerContext, CreateModelBindingContext("42", typeof(int)));
      Assert.AreEqual(42, integerResult);

      /* Test if the model binder can convert a boolean */
      bool booleanResult = (bool)testSubject.BindModel(controllerContext, CreateModelBindingContext("True", typeof(bool)));
      Assert.AreEqual(true, booleanResult);

      /* Test if the model binder can convert a string */
      string stringResult = (string)testSubject.BindModel(controllerContext, CreateModelBindingContext("42", typeof(string)));
      Assert.AreEqual("42", stringResult);

      /* Test if the model binder can convert an integer */
      double doubleResult = (double)testSubject.BindModel(controllerContext, CreateModelBindingContext("42.42", typeof(double)));
      Assert.AreEqual(42.42, doubleResult);

      /* Test if the model binder can convert an enum the default way */
      TestEnum enumResult = (TestEnum)testSubject.BindModel(controllerContext, CreateModelBindingContext("ValueC", typeof(TestEnum)));
      Assert.AreEqual(TestEnum.ValueC, enumResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the MVC3 
    /// fix works.</summary>
    [TestMethod]
    public void TestCase02_BindModel_EnumBehavior() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert a enum that actually exists*/
      TestEnum enumResult = (TestEnum)testSubject.BindModel(controllerContext, CreateModelBindingContext("1", typeof(TestEnum)));
      Assert.AreEqual(TestEnum.ValueB, enumResult);

      /* Test if the model binder can convert a decimal with one decimal position*/
      enumResult = (TestEnum)testSubject.BindModel(controllerContext, CreateModelBindingContext("2", typeof(TestEnum)));
      Assert.IsFalse(Enum.IsDefined(typeof(TestEnum), enumResult));

      /* Test if the model binder can convert an enum the default way */
      enumResult = (TestEnum)testSubject.BindModel(controllerContext, CreateModelBindingContext("ValueC", typeof(TestEnum)));
      Assert.AreEqual(TestEnum.ValueC, enumResult);
    }
    #endregion

    #region Private static helper methods
    /// <summary>Creates a <see cref="ModelBindingContext"/> that is used to pass to the model binder that is being tested.</summary>
    /// <param name="formValue">The value that is &quot;send&quot; to the binder.</param>
    /// <returns>The created context.</returns>
    private static ModelBindingContext CreateModelBindingContext(string formValue, Type destinationType) {
      IValueProvider valueProvider = new DictionaryValueProvider<string>(new Dictionary<string, string> { { "SomeValue", formValue } }, null);
      ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, destinationType);
      ModelBindingContext bindingContext = new ModelBindingContext { ModelName = "SomeValue", ValueProvider = valueProvider, ModelMetadata = metadata };
      return bindingContext;
    }
    #endregion
  }
}
