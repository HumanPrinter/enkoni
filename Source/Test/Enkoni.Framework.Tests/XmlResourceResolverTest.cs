using System;
using System.IO;
using System.Reflection;
using System.Xml;
using Enkoni.Framework.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>Tests the functionality of the <see cref="XmlResourceResolver"/> class.</summary>
  [TestClass]
  public class XmlResourceResolverTest {
    #region Properties
    /// <summary>Gets or sets the test context for these unit tests.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region Test methods
    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver(Assembly, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Constructor_NullValueForAssembly_ThrowsException() {
      /* Arrange */
      string resourceNamespace = "Enkoni.Framework.Tests.TestData";

      /* Act */
      XmlResourceResolver testSubject = new XmlResourceResolver(null, resourceNamespace);
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver(Assembly, string)"/> method.</summary>
    [TestMethod]
    public void Constructor_NullValueForNamespace_DoesNotThrowException() {
      /* Arrange */
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();

      /* Act */
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, null);

      /* Assert */
      Assert.IsNotNull(testSubject);
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver(Assembly, string)"/> method.</summary>
    [TestMethod]
    public void Constructor_EmptyValueForNamespace_DoesNotThrowException() {
      /* Arrange */
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();

      /* Act */
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, string.Empty);

      /* Assert */
      Assert.IsNotNull(testSubject);
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver.GetEntity(Uri, string, Type)"/> method.</summary>
    [TestMethod]
    public void GetEntity_ReadEmbeddedSchemaWithoutImport_ReturnsValidStream() {
      /* Arrange */
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();
      string resourceNamespace = "Enkoni.Framework.Tests.TestData";
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, resourceNamespace);

      /* Act */
      object result = testSubject.GetEntity(new Uri("file://mypath/EmbeddedChildSchema.xsd"), null, null);

      /* Assert */
      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(Stream));
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver.GetEntity(Uri, string, Type)"/> method.</summary>
    [TestMethod]
    public void GetEntity_ReadEmbeddedSchemaWithImport_ReturnsValidStream() {
      /* Arrange */
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();
      string resourceNamespace = "Enkoni.Framework.Tests.TestData";
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, resourceNamespace);

      /* Act */
      object result = testSubject.GetEntity(new Uri("file://mypath/ParentSchemaWithEmbeddedChild.xsd"), null, null);

      /* Assert */
      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(Stream));
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver.GetEntity(Uri, string, Type)"/> method.</summary>
    [TestMethod]
    [DeploymentItem("TestData\\FileBasedChildSchema.xsd")]
    public void GetEntity_FileBasedSchemaWithoutImport_ReturnsValidStream() {
      /* Arrange */
      string schemalocation = Path.Combine(this.TestContext.DeploymentDirectory, "FileBasedChildSchema.xsd");
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();
      string resourceNamespace = "UnitTests.TestData";
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, resourceNamespace);

      /* Act */
      object result = testSubject.GetEntity(new Uri(schemalocation), null, null);

      /* Assert */
      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(Stream));
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver.GetEntity(Uri, string, Type)"/> method.</summary>
    [TestMethod]
    [DeploymentItem("TestData\\ParentSchemaWithFileBasedChild.xsd")]
    [DeploymentItem("TestData\\FileBasedChildSchema.xsd")]
    public void GetEntity_FileBasedSchemaWithImport_ReturnsValidStream() {
      /* Arrange */
      string schemalocation = Path.Combine(this.TestContext.DeploymentDirectory, "ParentSchemaWithFileBasedChild.xsd");
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();
      string resourceNamespace = "UnitTests.TestData";
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, resourceNamespace);

      /* Act */
      object result = testSubject.GetEntity(new Uri(schemalocation), null, null);

      /* Assert */
      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(Stream));
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver.GetEntity(Uri, string, Type)"/> method.</summary>
    [TestMethod]
    [DeploymentItem("TestData\\FileBasedChildSchema.xsd")]
    [ExpectedException(typeof(NullReferenceException))]
    public void GetEntity_NullValueForUri_ExecutesBaseClassBehavior() {
      /* Arrange */
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();
      string resourceNamespace = "UnitTests.TestData";
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, resourceNamespace);

      /* Act */
      object result = testSubject.GetEntity(null, null, null);
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver.GetEntity(Uri, string, Type)"/> method.</summary>
    [TestMethod]
    public void GetEntity_HttpTypeUri_ExecutesBaseClassBehavior() {
      /* Arrange */
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();
      string resourceNamespace = "UnitTests.TestData";
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, resourceNamespace);

      /* Act */
      object result = testSubject.GetEntity(new Uri("http://tempuri.org/MySchema.xsd"), null, null);

      /* Assert */
      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(Stream));
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver.GetEntity(Uri, string, Type)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(XmlException))]
    public void GetEntity_ReturnTypeNotStream_ExecutesBaseClassBehavior() {
      /* Arrange */
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();
      string resourceNamespace = "UnitTests.TestData";
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, resourceNamespace);

      /* Act */
      object result = testSubject.GetEntity(new Uri("http://tempuri.org/MySchema.xsd"), null, typeof(string));
    }

    /// <summary>Tests the functionality of the <see cref="XmlResourceResolver.GetEntity(Uri, string, Type)"/> method.</summary>
    [TestMethod]
    public void GetEntity_ReturnTypeIsStream_ReturnsValidStream() {
      /* Arrange */
      Assembly resourceAssembly = Assembly.GetExecutingAssembly();
      string resourceNamespace = "Enkoni.Framework.Tests.TestData";
      XmlResourceResolver testSubject = new XmlResourceResolver(resourceAssembly, resourceNamespace);

      /* Act */
      object result = testSubject.GetEntity(new Uri("file://mypath/ParentSchemaWithEmbeddedChild.xsd"), null, typeof(Stream));

      /* Assert */
      Assert.IsNotNull(result);
      Assert.IsInstanceOfType(result, typeof(Stream));
    }
    #endregion
  }
}
