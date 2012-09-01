//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="FlatWsdlBehaviorAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//   Implementatie van een behavior waarmee de gegenereerde WSDL wordt platgeslagen.
// </summary>
// <remark>
//   Implementatie gebaseerd op de code van Glav (http://weblogs.asp.net/pglavich/archive/2010/03/16/making-wcf-output-a-single-wsdl-file-for-interop-purposes.aspx)
// </remark>
// --------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml.Schema;

using ServiceDescription = System.Web.Services.Description.ServiceDescription;

namespace Enkoni.Framework.ServiceModel {
  /// <summary>This class implements a custom endpoint behavior that flattens the automatically generated WSDL into a single document.</summary>
  /// <remarks>
  /// The implementation is based on the code of Glav (http://weblogs.asp.net/pglavich/archive/2010/03/16/making-wcf-output-a-single-wsdl-file-for-interop-purposes.aspx).
  /// </remarks>
  [AttributeUsage(AttributeTargets.Class)]
  public sealed class FlatWsdlBehaviorAttribute : Attribute, IWsdlExportExtension, IEndpointBehavior {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="FlatWsdlBehaviorAttribute"/> class.</summary>
    public FlatWsdlBehaviorAttribute() {
    }
    #endregion

    #region IWsdlExportExtension-implementation
    /// <summary> Writes custom Web Services Description Language (WSDL) elements into the generated WSDL for a contract.</summary>
    /// <param name="exporter">The <see cref="WsdlExporter"/> that exports the contract information.</param>
    /// <param name="context">Provides mappings from exported WSDL elements to the contract description.</param>
    public void ExportContract(WsdlExporter exporter, WsdlContractConversionContext context) {
      /* Geen implementatie vereist */
    }

    /// <summary>Writes custom Web Services Description Language (WSDL) elements into the generated WSDL for an endpoint.</summary>
    /// <param name="exporter">The <see cref="WsdlExporter"/> that exports the endpoint information.</param>
    /// <param name="context">Provides mappings from exported WSDL elements to the endpoint description.</param>
    public void ExportEndpoint(WsdlExporter exporter, WsdlEndpointConversionContext context) {
      if(exporter == null) {
        throw new ArgumentNullException("exporter");
      }

      /* Bepaal de gegenereerde XML schemas */
      XmlSchemaSet schemaSet = exporter.GeneratedXmlSchemas;

      foreach(ServiceDescription wsdl in exporter.GeneratedWsdlDocuments) {
        /* De administratie van de geïmporteerde schemas */
        List<XmlSchema> importsList = new List<XmlSchema>();

        foreach(XmlSchema schema in wsdl.Types.Schemas) {
          AddImportedSchemas(schema, schemaSet, importsList);
        }

        wsdl.Types.Schemas.Clear();

        foreach(XmlSchema schema in importsList) {
          RemoveXsdImports(schema);
          wsdl.Types.Schemas.Add(schema);
        }
      }
    }
    #endregion

    #region IEndpointBehavior-implementation
    /// <summary>Implement to pass data at runtime to bindings to support custom behavior.</summary>
    /// <param name="endpoint">The endpoint to modify.</param>
    /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) {
      /* No implementation required. */
    }

    /// <summary>Implements a modification or extension of the client across an endpoint.</summary>
    /// <param name="endpoint">The endpoint that is to be customized.</param>
    /// <param name="clientRuntime">The client runtime to be customized.</param>
    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) {
      /* No implementation required. */
    }

    /// <summary>Implements a modification or extension of the service across an endpoint.</summary>
    /// <param name="endpoint">The endpoint that exposes the contract.</param>
    /// <param name="endpointDispatcher">The endpoint dispatcher to be modified or extended.</param>
    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) {
      /* No implementation required. */
    }

    /// <summary>Implement to confirm that the endpoint meets some intended criteria.</summary>
    /// <param name="endpoint">The endpoint to validate.</param>
    public void Validate(ServiceEndpoint endpoint) {
      /* No implementation required. */
    }
    #endregion

    #region Private static helper methods
    /// <summary>Imports a schema and includes it.</summary>
    /// <param name="schema">The imported schema.</param>
    /// <param name="schemaSet">The generated XML schema.</param>
    /// <param name="importsList">The list to which the schema must be added.</param>
    private static void AddImportedSchemas(XmlSchema schema, XmlSchemaSet schemaSet, List<XmlSchema> importsList) {
      foreach(XmlSchemaImport import in schema.Includes) {
        ICollection realSchemas = schemaSet.Schemas(import.Namespace);

        foreach(XmlSchema ixsd in realSchemas) {
          if(!importsList.Contains(ixsd)) {
            importsList.Add(ixsd);
            AddImportedSchemas(ixsd, schemaSet, importsList);
          }
        }
      }
    }

    /// <summary>Removes the imported schemas from the schema.</summary>
    /// <param name="schema">The schema that must be cleaned.</param>
    private static void RemoveXsdImports(XmlSchema schema) {
      for(int i = 0; i < schema.Includes.Count; i++) {
        if(schema.Includes[i] is XmlSchemaImport) {
          schema.Includes.RemoveAt(i--);
        }
      }
    }
    #endregion
  }
}
