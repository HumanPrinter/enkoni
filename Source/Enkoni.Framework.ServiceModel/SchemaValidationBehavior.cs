//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SchemaValidationBehavior.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//   Implementatie van een behavior waarmee het ontvangen bericht op basis van een los XSD-schema gevalideerd kan worden.
// </summary>
// <remark>
//   Implementatie gebaseerd op de code van Microsoft (http://msdn.microsoft.com/en-us/library/ff647820.aspx)
// </remark>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using System.Xml.Schema;

namespace Enkoni.Framework.ServiceModel {
  /// <summary>This class implements a custom endpoint behavior that validates the received message using a seperate XSD schema.</summary>
  /// <remarks>The implementation is based on the code of Microsoft (http://msdn.microsoft.com/en-us/library/ff647820.aspx).</remarks>
  public class SchemaValidationBehavior : IEndpointBehavior {
    #region Instance variables
    /// <summary>De set van schemas die gebruikt wordt voor het valideren van ontvangen berichten.</summary>
    private readonly XmlSchemaSet schemaSet;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="SchemaValidationBehavior"/> class.</summary>
    /// <param name="enabled">Indicates whether or not the behavior is enabled.</param>
    /// <param name="schemaSet">Defines the schemas that must be used.</param>
    internal SchemaValidationBehavior(bool enabled, XmlSchemaSet schemaSet) {
      this.Enabled = enabled;
      this.schemaSet = schemaSet;
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets a value indicating whether the behavior is enabled or not.</summary>
    public bool Enabled { get; set; }
    #endregion

    #region IEnpointBehavior-implementations
    /// <summary>Implements a modification or extension of the service across an endpoint.</summary>
    /// <param name="endpoint">The endpoint that exposes the contract.</param>
    /// <param name="endpointDispatcher">The endpoint dispatcher to be modified or extended.</param>
    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher) {
      if(endpointDispatcher == null) {
        return;
      }

      /* If enable is not true in the config we do not apply the Parameter Inspector */
      if(!this.Enabled) {
        return;
      }

      if(!endpointDispatcher.DispatchRuntime.MessageInspectors.OfType<SchemaValidationMessageInspector>().Any()) {
        SchemaValidationMessageInspector inspector = new SchemaValidationMessageInspector(this.schemaSet);
        endpointDispatcher.DispatchRuntime.MessageInspectors.Add(inspector);
      }
    }

    /// <summary>Implement to pass data at runtime to bindings to support custom behavior.</summary>
    /// <param name="endpoint">The endpoint to modify.</param>
    /// <param name="bindingParameters">The objects that binding elements require to support the behavior.</param>
    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters) {
      /* Nothing to do here */
    }

    /// <summary>Implements a modification or extension of the client across an endpoint.</summary>
    /// <param name="endpoint">The endpoint that is to be customized.</param>
    /// <param name="clientRuntime">The client runtime to be customized.</param>
    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) {
      /* Nothing to do here */
    }

    /// <summary>Implement to confirm that the endpoint meets some intended criteria.</summary>
    /// <param name="endpoint">The endpoint to validate.</param>
    public void Validate(ServiceEndpoint endpoint) {
      /* Nothing to do here */
    }
    #endregion
  }
}