using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Xml;
using System.Xml.Schema;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>This class defines the testcases that test the functionality of the SchemaValidationMessageInspector class.</summary>
  [TestClass]
  public class SchemaValidationMessageInspectorTest {
    #region Test methods
    /// <summary>Tests the functionality of the <see cref="SchemaValidationMessageInspector.AfterReceiveRequest(ref Message, IClientChannel, InstanceContext)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestDataContract.xsd", @"SchemaValidationMessageInspectorTest\AfterReceiveRequest_NullRequest")]
    public void SchemaValidationMessageInspector_AfterReceiveRequest_NullRequest() {
      /* Create the schema set and test subject */
      XmlSchemaSet schemaSet = new XmlSchemaSet();
      using(Stream stream = File.OpenRead(@"SchemaValidationMessageInspectorTest\AfterReceiveRequest_NullRequest\TestDataContract.xsd")) {
        using(XmlReader reader = XmlReader.Create(stream)) {
          XmlSchema schema = XmlSchema.Read(reader, null);
          schemaSet.Add(schema);
        }
      }

      SchemaValidationMessageInspector testSubject = new SchemaValidationMessageInspector(schemaSet);
      Message message = null;
      object result = testSubject.AfterReceiveRequest(ref message, null, null);
      Assert.IsNull(result);
    }

    /// <summary>Tests the functionality of the <see cref="SchemaValidationMessageInspector.AfterReceiveRequest(ref Message, IClientChannel, InstanceContext)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestDataContract.xsd", @"SchemaValidationMessageInspectorTest\AfterReceiveRequest_ValidRequest")]
    [DeploymentItem(@"TestData\ValidRequest.xml", @"SchemaValidationMessageInspectorTest\AfterReceiveRequest_ValidRequest")]
    public void SchemaValidationMessageInspector_AfterReceiveRequest_ValidRequest() {
      /* Create the schema set and test subject */
      XmlSchemaSet schemaSet = new XmlSchemaSet();
      using(Stream stream = File.OpenRead(@"SchemaValidationMessageInspectorTest\AfterReceiveRequest_ValidRequest\TestDataContract.xsd")) {
        using(XmlReader reader = XmlReader.Create(stream)) {
          XmlSchema schema = XmlSchema.Read(reader, null);
          schemaSet.Add(schema);
        }
      }

      SchemaValidationMessageInspector testSubject = new SchemaValidationMessageInspector(schemaSet);
      Message message = Message.CreateMessage(MessageVersion.Soap11, "POST", XmlReader.Create(@"SchemaValidationMessageInspectorTest\AfterReceiveRequest_ValidRequest\ValidRequest.xml"));
      object result = testSubject.AfterReceiveRequest(ref message, null, null);
      Assert.IsNull(result);
    }

    /// <summary>Tests the functionality of the <see cref="SchemaValidationMessageInspector.AfterReceiveRequest(ref Message, IClientChannel, InstanceContext)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestDataContract.xsd", @"SchemaValidationMessageInspectorTest\AfterReceiveRequest_InvalidRequest")]
    [DeploymentItem(@"TestData\InvalidRequest.xml", @"SchemaValidationMessageInspectorTest\AfterReceiveRequest_InvalidRequest")]
    public void SchemaValidationMessageInspector_AfterReceiveRequest_InvalidRequest() {
      /* Create the schema set and test subject */
      XmlSchemaSet schemaSet = new XmlSchemaSet();
      using(Stream stream = File.OpenRead(@"SchemaValidationMessageInspectorTest\AfterReceiveRequest_InvalidRequest\TestDataContract.xsd")) {
        using(XmlReader reader = XmlReader.Create(stream)) {
          XmlSchema schema = XmlSchema.Read(reader, null);
          schemaSet.Add(schema);
        }
      }

      SchemaValidationMessageInspector testSubject = new SchemaValidationMessageInspector(schemaSet);
      Message message = Message.CreateMessage(MessageVersion.Soap11, "POST", XmlReader.Create(@"SchemaValidationMessageInspectorTest\AfterReceiveRequest_InvalidRequest\InvalidRequest.xml"));
      try {
        object result = testSubject.AfterReceiveRequest(ref message, null, null);
        Assert.Fail("The expected FaultException<string> was not thrown");
      }
      catch(FaultException<string> ex) {
        Assert.IsNotNull(ex.Code);
        Assert.IsTrue(ex.Code.IsSenderFault);
        Assert.AreEqual("InvalidMessage", ex.Code.SubCode.Name);
        Assert.IsNotNull(ex.Reason);
        Assert.AreEqual("The received message does not comply with the required schema", ex.Reason.GetMatchingTranslation(CultureInfo.InvariantCulture).Text);
      }
    }
    #endregion
  }
}
