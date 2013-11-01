//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaValidationBehaviorTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the SchemaValidationBehavior classes.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;

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
    /// <summary>Serves as a reference test to check the default behavior of the XML validation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestData\ValidRequest.xml", @"SchemaValidationBehaviorTest\TestCase01")]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestData\InvalidRequest.xml", @"SchemaValidationBehaviorTest\TestCase01")]
    //This construction is a work-around for a bug in VS2012. Unless a referenced DLL is not directly used inside the unit-test, the DLL is not copied to the Testresults folder causing the test to fail.
#if DEBUG
    [DeploymentItem(@"bin\Debug\Enkoni.Framework.ServiceModel.dll", "")]
    [DeploymentItem(@"bin\Debug\Enkoni.Framework.ServiceModel.pdb", "")]
#else
    [DeploymentItem(@"bin\Release\Enkoni.Framework.ServiceModel.dll", "")]
    [DeploymentItem(@"bin\Release\Enkoni.Framework.ServiceModel.pdb", "")]
#endif
    public void TestCase01_DefaultBehavior() {
      /* First, send a valid request */
      XDocument request = XDocument.Load(@"SchemaValidationBehaviorTest\TestCase01\ValidRequest.xml");
      XDocument result = null;
      try {
        result = ReadServiceResponse(request, typeof(TestService), "test_default");
      }
      catch(AddressAccessDeniedException) {
        Assert.Inconclusive("This test case could not be executed at the test controller due to insufficient rights");
        return;
      }

      /* Check if the response is as expected */
      Assert.IsTrue(DocumentContainsExpectedValues(result, new DateTime(2012, 11, 24), "Hello TestValue", 43));

      /* Second, send a request that should be rejected (but isn't because of a bug in the default DataContractSerializer xml validation */
      request = XDocument.Load(@"SchemaValidationBehaviorTest\TestCase01\InvalidRequest.xml");
      result = ReadServiceResponse(request, typeof(TestService), "test_default");

      Assert.IsTrue(DocumentContainsExpectedValues(result, DateTime.MinValue, "Hello TestValue", 43));
    }

    /// <summary>Tests if the <see cref="SchemaValidationBehavior"/> is disabled accordingly with the configuration parameter.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestData\ValidRequest.xml", @"SchemaValidationBehaviorTest\TestCase02")]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestData\InvalidRequest.xml", @"SchemaValidationBehaviorTest\TestCase02")]
    public void TestCase02_SchemaValidationBehaviorDisabled() {
      /* First, send a valid request */
      XDocument request = XDocument.Load(@"SchemaValidationBehaviorTest\TestCase02\ValidRequest.xml");
      XDocument result = null;
      try {
        result = ReadServiceResponse(request, typeof(TestService), "test_default");
      }
      catch(AddressAccessDeniedException) {
        Assert.Inconclusive("This test case could not be executed at the test controller due to insufficient rights");
        return;
      }

      /* Check if the response is as expected */
      Assert.IsTrue(DocumentContainsExpectedValues(result, new DateTime(2012, 11, 24), "Hello TestValue", 43));

      /* Second, send a request that should be rejected (but isn't because of a bug in the default DataContractSerializer xml validation */
      request = XDocument.Load(@"SchemaValidationBehaviorTest\TestCase02\InvalidRequest.xml");
      result = ReadServiceResponse(request, typeof(TestService), "test_disabled");

      Assert.IsTrue(DocumentContainsExpectedValues(result, DateTime.MinValue, "Hello TestValue", 43));
    }

    /// <summary>Tests the functionality of the <see cref="SchemaValidationBehavior"/> with a schema loaded as an assembly resource.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestData\ValidRequest.xml", @"SchemaValidationBehaviorTest\TestCase03")]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestData\InvalidRequest.xml", @"SchemaValidationBehaviorTest\TestCase03")]
    public void TestCase03_SchemaValidationBehaviorSchemaFromResource() {
      XDocument request = XDocument.Load(@"SchemaValidationBehaviorTest\TestCase03\ValidRequest.xml");
      XDocument result = null;
      try {
        result = ReadServiceResponse(request, typeof(TestService), "test_default");
      }
      catch(AddressAccessDeniedException) {
        Assert.Inconclusive("This test case could not be executed at the test controller due to insufficient rights");
        return;
      }

      /* Check if the response is as expected */
      Assert.IsTrue(DocumentContainsExpectedValues(result, new DateTime(2012, 11, 24), "Hello TestValue", 43));

      request = XDocument.Load(@"SchemaValidationBehaviorTest\TestCase03\InvalidRequest.xml");
      result = ReadServiceResponse(request, typeof(TestService), "test_resource");

      Assert.IsTrue(DocumentDescribesError(result));
    }

    /// <summary>Tests the functionality of the <see cref="SchemaValidationBehavior"/> with a schema loaded as a file.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestData\ValidRequest.xml", @"SchemaValidationBehaviorTest\TestCase04")]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestData\InvalidRequest.xml", @"SchemaValidationBehaviorTest\TestCase04")]
    [DeploymentItem(@"Test\Enkoni.Framework.ServiceModel.Tests\TestDataContract.xsd", @"SchemaValidationBehaviorTest\TestCase04")]
    public void TestCase04_SchemaValidationBehaviorSchemaFromFile() {
      XDocument request = XDocument.Load(@"SchemaValidationBehaviorTest\TestCase04\ValidRequest.xml");
      XDocument result = null;
      try {
        result = ReadServiceResponse(request, typeof(TestService), "test_default");
      }
      catch(AddressAccessDeniedException) {
        Assert.Inconclusive("This test case could not be executed at the test controller due to insufficient rights");
        return;
      }

      Assert.IsTrue(DocumentContainsExpectedValues(result, new DateTime(2012, 11, 24), "Hello TestValue", 43));

      request = XDocument.Load(@"SchemaValidationBehaviorTest\TestCase04\InvalidRequest.xml");
      result = ReadServiceResponse(request, typeof(TestService), "test_file");

      Assert.IsTrue(DocumentDescribesError(result));
    }
    #endregion

    #region Private static helper methods
    /// <summary>Checks if the XML document contains the specified expected values.</summary>
    /// <param name="document">The document that must be checked.</param>
    /// <param name="expectedSomeDate">The expected value of the 'SomeDate' element.</param>
    /// <param name="expectedSomeName">The expected value of the 'SomeName' element.</param>
    /// <param name="expectedSomeNumber">The expected value of the 'SomeNumber' element.</param>
    /// <returns><see langword="true"/> if the document meets the expectations, <see langword="false"/> otherwise.</returns>
    private static bool DocumentContainsExpectedValues(XDocument document, DateTime expectedSomeDate, string expectedSomeName, int expectedSomeNumber) {
      XElement envelope = document.Root;
      if(envelope == null) {
        return false;
      }

      XElement body = envelope.FirstNode as XElement;
      if(body == null) {
        return false;
      }

      XElement processObjectResponse = body.FirstNode as XElement;
      if(processObjectResponse == null) {
        return false;
      }

      XElement processObjectResult = processObjectResponse.FirstNode as XElement;
      if(processObjectResult == null) {
        return false;
      }

      XElement[] properties = processObjectResult.Elements().ToArray();
      return properties.Length == 3
        && properties[0].Value == expectedSomeDate.ToString("yyyy-MM-ddTHH:mm:ss")
        && properties[1].Value == expectedSomeName
        && properties[2].Value == expectedSomeNumber.ToString();
    }

    /// <summary>Checks if the document contains a SOAP fault message.</summary>
    /// <param name="document">The document that must be examined.</param>
    /// <returns><see langword="true"/> if the document contains a SOAP fault message, <see langword="false"/> otherwise.</returns>
    private static bool DocumentDescribesError(XDocument document) {
      XElement envelope = document.Root;
      if(envelope == null) {
        return false;
      }

      XElement body = envelope.FirstNode as XElement;
      if(body == null) {
        return false;
      }

      XElement fault = body.FirstNode as XElement;
      return fault != null && fault.Name.LocalName.Equals("Fault", StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>Starts a ServiceHost containing the specified service implementation.</summary>
    /// <param name="serviceImplementation">The service implementation that must be hosted.</param>
    /// <returns>The created service host.</returns>
    private static ServiceHost StartService(Type serviceImplementation) {
      ServiceHost host = new ServiceHost(serviceImplementation, new Uri(ServiceLocation));
      host.Open();

      return host;
    }

    /// <summary>Sends a SOAP message to the service and returns the received response.</summary>
    /// <param name="request">The message that must be send.</param>
    /// <param name="serviceImplementation">The service implementation that is hosted.</param>
    /// <param name="endpointAddress">The address of the endpoint that must be targeted.</param>
    /// <returns>The received response.</returns>
    private static XDocument ReadServiceResponse(XDocument request, Type serviceImplementation, string endpointAddress) {
      /* Start a service host that hosts the test service */
      XDocument responseDocument;
      using(ServiceHost host = StartService(typeof(TestService))) {
        /* Compose a request to extract the WSDL from the service */
        WebClient client = new WebClient();
        client.Headers.Add("SOAPAction", "http://test.enkoni.sourceforge.org/contracts/ITestService/ProcessObject");
        client.Headers.Add("Content-Type", "text/xml;charset=UTF-8");

        Stream responseStream;
        try {
          string responseString = client.UploadString(ServiceLocation + "/" + endpointAddress, request.ToString());
          responseStream = new MemoryStream(Encoding.UTF8.GetBytes(responseString));
        }
        catch(WebException ex) {
          responseStream = ex.Response.GetResponseStream();
        }

        using(XmlReader reader = new XmlTextReader(responseStream)) {
          responseDocument = XDocument.Load(reader);
        }
      }

      return responseDocument;
    }
    #endregion
  }
}
