using System;
using System.Collections.Generic;
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
    public void Mvc3FixedModelBinder_BindModelIntValueAsString_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert an integer */
      int integerResult = (int)testSubject.BindModel(controllerContext, CreateModelBindingContext("42", typeof(int)));
      Assert.AreEqual(42, integerResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the 
    /// default behavior still works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelIntValueAsInt_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert an integer */
      int integerResult = (int)testSubject.BindModel(controllerContext, CreateModelBindingContext(42, typeof(int)));
      Assert.AreEqual(42, integerResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the 
    /// default behavior still works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelBoolValueAsString_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert a boolean */
      bool booleanResult = (bool)testSubject.BindModel(controllerContext, CreateModelBindingContext("True", typeof(bool)));
      Assert.AreEqual(true, booleanResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the 
    /// default behavior still works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelBoolValueAsBool_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert a boolean */
      bool booleanResult = (bool)testSubject.BindModel(controllerContext, CreateModelBindingContext(true, typeof(bool)));
      Assert.AreEqual(true, booleanResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the 
    /// default behavior still works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelStringValue_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert a string */
      string stringResult = (string)testSubject.BindModel(controllerContext, CreateModelBindingContext("42", typeof(string)));
      Assert.AreEqual("42", stringResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the 
    /// default behavior still works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelDoubleValueAsString_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert an integer */
      double doubleResult = (double)testSubject.BindModel(controllerContext, CreateModelBindingContext("42.42", typeof(double)));
      Assert.AreEqual(42.42, doubleResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the 
    /// default behavior still works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelDoubleValueAsDouble_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert an integer */
      double doubleResult = (double)testSubject.BindModel(controllerContext, CreateModelBindingContext(42.42, typeof(double)));
      Assert.AreEqual(42.42, doubleResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the 
    /// default behavior still works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelEnumValueAsString_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert an enum the default way */
      TestEnum enumResult = (TestEnum)testSubject.BindModel(controllerContext, CreateModelBindingContext("ValueC", typeof(TestEnum)));
      Assert.AreEqual(TestEnum.ValueC, enumResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the MVC3 
    /// fix works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelEnumValueAsInt_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert a enum that actually exists*/
      TestEnum enumResult = (TestEnum)testSubject.BindModel(controllerContext, CreateModelBindingContext("1", typeof(TestEnum)));
      Assert.AreEqual(TestEnum.ValueB, enumResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the MVC3 
    /// fix works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelEnumValueAsEnum_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert a enum that actually exists*/
      TestEnum enumResult = (TestEnum)testSubject.BindModel(controllerContext, CreateModelBindingContext(TestEnum.ValueB, typeof(TestEnum)));
      Assert.AreEqual(TestEnum.ValueB, enumResult);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the MVC3 
    /// fix works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelUndefinedEnumValue_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      /* Test if the model binder can convert a enum that does not exist*/
      TestEnum enumResult = (TestEnum)testSubject.BindModel(controllerContext, CreateModelBindingContext("2", typeof(TestEnum)));
      Assert.IsFalse(Enum.IsDefined(typeof(TestEnum), enumResult));
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the MVC3 
    /// fix works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelComplexTypeWithEnumAsString_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      IValueProvider valueProvider = new DictionaryValueProvider<string>(new Dictionary<string, string> { { "SomeValue", "ValueC" }, { "SomeOtherValue", "42" } }, null);
      ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MyModel));
      ModelBindingContext bindingContext = new ModelBindingContext { ModelName = string.Empty, ValueProvider = valueProvider, ModelMetadata = metadata };

      MyModel model = (MyModel)testSubject.BindModel(controllerContext, bindingContext);
      Assert.IsNotNull(model);
      Assert.AreEqual(TestEnum.ValueC, model.SomeValue);
      Assert.AreEqual(42, model.SomeOtherValue);
    }

    /// <summary>Tests the functionality of the <see cref="Mvc3FixedModelBinder.BindModel(ControllerContext,ModelBindingContext)"/> to see if the MVC3 
    /// fix works.</summary>
    [TestMethod]
    public void Mvc3FixedModelBinder_BindModelComplexTypeWithEnumAsInt_ValueIsCorrectlyParsed() {
      /* Create the test subject */
      Mvc3FixedModelBinder testSubject = new Mvc3FixedModelBinder();

      ControllerContext controllerContext = new ControllerContext();

      IValueProvider valueProvider = new DictionaryValueProvider<object>(new Dictionary<string, object> { { "SomeValue", 3 }, { "SomeOtherValue", "42" } }, null);
      ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MyModel));
      ModelBindingContext bindingContext = new ModelBindingContext { ModelName = string.Empty, ValueProvider = valueProvider, ModelMetadata = metadata };

      MyModel model = (MyModel)testSubject.BindModel(controllerContext, bindingContext);
      Assert.IsNotNull(model);
      Assert.AreEqual(TestEnum.ValueC, model.SomeValue);
      Assert.AreEqual(42, model.SomeOtherValue);
    }
    #endregion

    #region Private static helper methods
    /// <summary>Creates a <see cref="ModelBindingContext"/> that is used to pass to the model binder that is being tested.</summary>
    /// <param name="formValue">The value that is &quot;send&quot; to the binder.</param>
    /// <param name="destinationType">The destination type.</param>
    /// <returns>The created context.</returns>
    private static ModelBindingContext CreateModelBindingContext(object formValue, Type destinationType) {
      IValueProvider valueProvider = new DictionaryValueProvider<object>(new Dictionary<string, object> { { "SomeValue", formValue } }, null);
      ModelMetadata metadata = ModelMetadataProviders.Current.GetMetadataForType(null, destinationType);
      ModelBindingContext bindingContext = new ModelBindingContext { ModelName = "SomeValue", ValueProvider = valueProvider, ModelMetadata = metadata };
      return bindingContext;
    }
    #endregion

    #region Private helper class
    /// <summary>A dummy model.</summary>
    private class MyModel {
      /// <summary>Gets or sets a enum value.</summary>
      public TestEnum SomeValue { get; set; }

      /// <summary>Gets or sets an int value.</summary>
      public int SomeOtherValue { get; set; }
    }
    #endregion
  }
}
