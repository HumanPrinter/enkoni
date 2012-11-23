//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="FlatWsdlBehaviorTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the FlatWsdlBehaviorTest classes.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.ServiceModel.Tests {
  /// <summary>This class defines the testcases that test the functionality of the FlatWsdlBehaviorTest classes.</summary>
  [TestClass]
  public class FlatWsdlBehaviorTest {
    private const string ServiceLocation = "http://localhost:15542";

    /// <summary>Serves as a reference test to check the default behavior of the WSDL generation.</summary>
    [TestMethod]
    public void TestCase01_DefaultBehavior() {
      /* Read the WSDL for the service */
      Tuple<XDocument, XmlNamespaceManager> result = ReadGeneratedWsdl(typeof(TestService1));

      IEnumerable<XElement> imports = result.Item1.XPathSelectElements("//wsdl:definitions/wsdl:types/xsd:schema/xsd:import", result.Item2);

      /* By default, there are three imports generated */
      Assert.AreEqual(3, imports.Count());
    }

    /// <summary>Tests the functionality of the FlatWsdlBehavior classes when applied to a service using an attribute.</summary>
    [TestMethod]
    public void TestCase02_FlatWsdlUsingAttribute() {
      /* Read the WSDL for the service */
      Tuple<XDocument, XmlNamespaceManager> result = ReadGeneratedWsdl(typeof(TestService2));

      IEnumerable<XElement> imports = result.Item1.XPathSelectElements("//wsdl:definitions/wsdl:types/xsd:schema/xsd:import", result.Item2);

      /* By default, there are three imports generated */
      Assert.AreEqual(3, imports.Count());
    }

    /// <summary>Tests the functionality of the FlatWsdlBehavior classes when applied to a service through code.</summary>
    [TestMethod]
    public void TestCase03_FlatWsdlUsingCode() {
    }

    /// <summary>Tests the functionality of the FlatWsdlBehavior classes when applied to a service using configuration.</summary>
    [TestMethod]
    public void TestCase04_FlatWsdlUsingConfiguration() {
    }

    private static ServiceHost StartService(Type serviceImplementation, params IEndpointBehavior[] behaviors) {
      ServiceHost host = new ServiceHost(serviceImplementation, new Uri(ServiceLocation));
      host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

      BasicHttpBinding serviceBinding = new BasicHttpBinding();
      serviceBinding.Security.Mode = BasicHttpSecurityMode.None;

      ServiceEndpoint endPoint = host.AddServiceEndpoint(typeof(ITestService), serviceBinding, "test");
      foreach(IEndpointBehavior behavior in behaviors) {
        endPoint.Behaviors.Add(behavior);
      }

      host.Open();
      return host;
    }

    private static Tuple<XDocument, XmlNamespaceManager> ReadGeneratedWsdl(Type serviceImplementation, params IEndpointBehavior[] behaviors) {
      /* Start a service host that hosts the test service */
      XDocument wsdlDocument;
      XmlNameTable nameTable;
      using(ServiceHost host = StartService(typeof(TestService1), behaviors)) {
        /* Compose a request to extract the WSDL from the service */
        WebRequest wsdlRequest = WebRequest.Create(ServiceLocation + "/?wsdl");
        wsdlRequest.Method = "GET";

        /* Send the request, read and parse the response */
        WebResponse response = wsdlRequest.GetResponse();
        Stream responseStream = response.GetResponseStream();
        using(XmlReader reader = new XmlTextReader(responseStream)) {
          wsdlDocument = XDocument.Load(reader);
          nameTable = reader.NameTable;
        }
      }

      /* Read the namespace declarations from the document */
      XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
      IEnumerable<XAttribute> attributes = wsdlDocument.Root.Attributes();
      IEnumerable<XAttribute> namespaceDeclarations = attributes.Where(attr => attr.IsNamespaceDeclaration);
      foreach(XAttribute namespaceDeclaration in namespaceDeclarations) {
        namespaceManager.AddNamespace(namespaceDeclaration.Name.LocalName, namespaceDeclaration.Value);
      }

      return new Tuple<XDocument, XmlNamespaceManager>(wsdlDocument, namespaceManager);
    }
  }
}
