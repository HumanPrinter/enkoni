using System;
using System.Reflection;
using System.Xml.Schema;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>This class defines the testcases that test the functionality of the SchemaValidationBehaviorExtensionElement class.</summary>
  [TestClass]
  public class SchemaValidationBehaviorExtensionElementTest {
    #region Constants
    /// <summary>Defines the base address for the service.</summary>
    private const string ServiceLocation = "http://localhost:15542";
    #endregion

    #region Test methods
    /// <summary>Serves as a reference test to check the default behavior of the XML validation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestDataContract.xsd", @"SchemaValidationBehaviorExtensionElement\CreateBehavior_Disabled")]
    public void SchemaValidationBehaviorExtensionElement_CreateBehavior_Disabled() {
      SchemaValidationBehaviorExtensionElementTestWrapper testSubject = new SchemaValidationBehaviorExtensionElementTestWrapper();
      testSubject.Enabled = false;
      /* Since the behavior will be disabled, passing an non-existing file should not matter */
      testSubject.SchemaFile = @"SchemaValidationBehaviorExtensionElement\CreateBehavior_Disabled\UnknownContract.xsd";
      
      object result = testSubject.CreateBehavior();
      Assert.IsInstanceOfType(result, typeof(SchemaValidationBehavior));
      SchemaValidationBehavior castedResult = result as SchemaValidationBehavior;
      Assert.IsFalse(castedResult.Enabled);
    }

    /// <summary>Serves as a reference test to check the default behavior of the XML validation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestDataContract.xsd", @"SchemaValidationBehaviorExtensionElement\CreateBehavior_ValidFile")]
    public void SchemaValidationBehaviorExtensionElement_CreateBehavior_ValidFile() {
      SchemaValidationBehaviorExtensionElementTestWrapper testSubject = new SchemaValidationBehaviorExtensionElementTestWrapper();
      testSubject.Enabled = true;
      testSubject.SchemaFile = @"SchemaValidationBehaviorExtensionElement\CreateBehavior_ValidFile\TestDataContract.xsd";

      object result = testSubject.CreateBehavior();
      Assert.IsInstanceOfType(result, typeof(SchemaValidationBehavior));
      SchemaValidationBehavior castedResult = result as SchemaValidationBehavior;
      Assert.IsTrue(castedResult.Enabled);

      FieldInfo schemaSetField = typeof(SchemaValidationBehavior).GetField("schemaSet", BindingFlags.NonPublic | BindingFlags.Instance);
      XmlSchemaSet schemaSet = schemaSetField.GetValue(castedResult) as XmlSchemaSet;
      Assert.IsNotNull(schemaSet);
    }

    /// <summary>Serves as a reference test to check the default behavior of the XML validation.</summary>
    [TestMethod]
    public void SchemaValidationBehaviorExtensionElement_CreateBehavior_ValidResource() {
      SchemaValidationBehaviorExtensionElementTestWrapper testSubject = new SchemaValidationBehaviorExtensionElementTestWrapper();
      testSubject.Enabled = true;
      testSubject.SchemaFile = @"resource://Enkoni.Framework.ServiceModel.Tests.TestDataContract.xsd, Enkoni.Framework.ServiceModel.Tests";

      object result = testSubject.CreateBehavior();
      Assert.IsInstanceOfType(result, typeof(SchemaValidationBehavior));
      SchemaValidationBehavior castedResult = result as SchemaValidationBehavior;
      Assert.IsTrue(castedResult.Enabled);

      FieldInfo schemaSetField = typeof(SchemaValidationBehavior).GetField("schemaSet", BindingFlags.NonPublic | BindingFlags.Instance);
      XmlSchemaSet schemaSet = schemaSetField.GetValue(castedResult) as XmlSchemaSet;
      Assert.IsNotNull(schemaSet);
    }

    /// <summary>Serves as a reference test to check the default behavior of the XML validation.</summary>
    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void SchemaValidationBehaviorExtensionElement_CreateBehavior_NullResource() {
      SchemaValidationBehaviorExtensionElementTestWrapper testSubject = new SchemaValidationBehaviorExtensionElementTestWrapper();
      testSubject.Enabled = true;
      testSubject.SchemaFile = string.Empty;

      object result = testSubject.CreateBehavior();
    }
    #endregion
  }
}
