using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.Schema;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>This class defines the testcases that test the functionality of the SchemaValidationBehavior classes.</summary>
  [TestClass]
  public class SchemaValidationBehaviorTest {
    #region Constants
    /// <summary>Defines the base address for the service.</summary>
    private const string ServiceLocation = "http://localhost:15542";
    #endregion

    #region Test methods
    /// <summary>Tests the functionality of the <see cref="SchemaValidationBehavior.ApplyDispatchBehavior(System.ServiceModel.Description.ServiceEndpoint, EndpointDispatcher)"/>
    /// method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestDataContract.xsd", @"SchemaValidationBehaviorTest\ApplyDispatchBehavior_NullValue")]
    public void SchemaValidationBehavior_ApplyDispatchBehavior_NullValue() {
      /* Create the schema set and test subject */
      XmlSchemaSet schemaSet = new XmlSchemaSet();
      using(Stream stream = File.OpenRead(@"SchemaValidationBehaviorTest\ApplyDispatchBehavior_NullValue\TestDataContract.xsd")) {
        using(XmlReader reader = XmlReader.Create(stream)) {
          XmlSchema schema = XmlSchema.Read(reader, null);
          schemaSet.Add(schema);
        }
      }

      SchemaValidationBehavior testSubject = new SchemaValidationBehavior(true, schemaSet);

      /* The first parameter is not used, so it's always save to pass null */
      /* Pass null for the second parameter, this should not throw any exception */
      testSubject.ApplyDispatchBehavior(null,  null);

      /* If this point was reached, no exception was thrown and the test succeeded */
    }

    /// <summary>Tests the functionality of the <see cref="SchemaValidationBehavior.ApplyDispatchBehavior(System.ServiceModel.Description.ServiceEndpoint, EndpointDispatcher)"/>
    /// method.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestDataContract.xsd", @"SchemaValidationBehaviorTest\ApplyDispatchBehavior_Disabled")]
    public void SchemaValidationBehavior_ApplyDispatchBehavior_Disabled() {
      /* Create the schema set and test subject */
      XmlSchemaSet schemaSet = new XmlSchemaSet();
      using(Stream stream = File.OpenRead(@"SchemaValidationBehaviorTest\ApplyDispatchBehavior_Disabled\TestDataContract.xsd")) {
        using(XmlReader reader = XmlReader.Create(stream)) {
          XmlSchema schema = XmlSchema.Read(reader, null);
          schemaSet.Add(schema);
        }
      }

      SchemaValidationBehavior testSubject = new SchemaValidationBehavior(false, schemaSet);
      EndpointDispatcher dispatcher = new EndpointDispatcher(new EndpointAddress("http://localhost:4999/testservice"), "Enkoni.Framework.ServiceModel.Tests.TestDataContract", "http://test.enkoni.sourceforge.org/contracts");

      /* The first parameter is not used, so it's always save to pass null */
      testSubject.ApplyDispatchBehavior(null, dispatcher);

      IEnumerable<SchemaValidationMessageInspector> inspectors = dispatcher.DispatchRuntime.MessageInspectors.OfType<SchemaValidationMessageInspector>();
      Assert.AreEqual(0, inspectors.Count());
    }

    /// <summary>Tests the functionality of the <see cref="SchemaValidationBehavior.ApplyDispatchBehavior(System.ServiceModel.Description.ServiceEndpoint, EndpointDispatcher)"/>
    /// method.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestDataContract.xsd", @"SchemaValidationBehaviorTest\ApplyDispatchBehavior_Enabled")]
    public void SchemaValidationBehavior_ApplyDispatchBehavior_Enabled() {
      /* Create the schema set and test subject */
      XmlSchemaSet schemaSet = new XmlSchemaSet();
      using(Stream stream = File.OpenRead(@"SchemaValidationBehaviorTest\ApplyDispatchBehavior_Enabled\TestDataContract.xsd")) {
        using(XmlReader reader = XmlReader.Create(stream)) {
          XmlSchema schema = XmlSchema.Read(reader, null);
          schemaSet.Add(schema);
        }
      }

      SchemaValidationBehavior testSubject = new SchemaValidationBehavior(true, schemaSet);
      EndpointDispatcher dispatcher = new EndpointDispatcher(new EndpointAddress("http://localhost:4999/testservice"), "Enkoni.Framework.ServiceModel.Tests.TestDataContract", "http://test.enkoni.sourceforge.org/contracts");

      /* The first parameter is not used, so it's always save to pass null */
      testSubject.ApplyDispatchBehavior(null, dispatcher);

      IEnumerable<SchemaValidationMessageInspector> inspectors = dispatcher.DispatchRuntime.MessageInspectors.OfType<SchemaValidationMessageInspector>();
      Assert.AreEqual(1, inspectors.Count());
    }
    #endregion
  }
}
