using System.Globalization;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using System.Xml.Schema;

using Enkoni.Framework.Logging;
using Enkoni.Framework.ServiceModel.Properties;

namespace Enkoni.Framework.ServiceModel {
  /// <summary>Implements a message inspector that inspects a received message.</summary>
  public class SchemaValidationMessageInspector : IDispatchMessageInspector {
    #region Instance variables
    /// <summary>The schemas that are used to validate the messages.</summary>
    private readonly XmlSchemaSet schemas;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="SchemaValidationMessageInspector"/> class.</summary>
    /// <param name="schemas">The schemas that are to be used.</param>
    public SchemaValidationMessageInspector(XmlSchemaSet schemas) {
      this.schemas = schemas;
    }
    #endregion

    #region IDispatchMessageInspector implementation
    /// <summary>Called after an inbound message has been received but before the message is dispatched to the intended operation.</summary>
    /// <param name="request">The request message.</param>
    /// <param name="channel">The incoming channel.</param>
    /// <param name="instanceContext">The current service instance.</param>
    /// <returns>The object used to correlate state. This object is passed back in the BeforeSendReply(Message@,object) method.</returns>
    public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext) {
      if(request == null) {
        return null;
      }
      
      /* Create a buffer in order to make it possible to work with copies of the message */
      MessageBuffer buffer = request.CreateBufferedCopy(int.MaxValue);

      this.OnPreValidation(buffer.CreateMessage(), channel, instanceContext);

      /* Create a copy of the message and send it to the validation */
      request = buffer.CreateMessage();
      try {
        this.ValidateMessage(request);
      }
      catch(XmlSchemaValidationException ex) {
        this.OnValidationError(buffer.CreateMessage(), channel, instanceContext, ex);

        FaultReasonText reasonText = new FaultReasonText(Resources.MessageDoesNotComplyWithSchema, CultureInfo.InvariantCulture);
        throw new FaultException<string>(ex.Message, new FaultReason(reasonText), FaultCode.CreateSenderFaultCode(new FaultCode("InvalidMessage")));
      }

      this.OnValidationSuccess(buffer.CreateMessage(), channel, instanceContext);

      /* Validation was succesfull. Create a new copy of the message and pass it to the WCF process. */
      request = buffer.CreateMessage();

      /* There is no need to correlate the AfterReceiveRequest en BeforeSendReply calls, so simply return null */
      return null;
    }

    /// <summary>Called after the operation has returned but before the reply message is sent.</summary>
    /// <param name="reply">The reply message. This value is null if the operation is one way.</param>
    /// <param name="correlationState">The correlation object returned from the AfterReceiveRequest(Message@,IClientChannel,InstanceContext) method.
    /// </param>
    public void BeforeSendReply(ref Message reply, object correlationState) {
      /* Nothing to do */
    }
    #endregion

    #region Protected extension methods
    /// <summary>When overriden executes some logic before starting the validation.</summary>
    /// <param name="receivedMessage">The request message.</param>
    /// <param name="channel">The incoming channel.</param>
    /// <param name="instanceContext">The current service instance.</param>
    protected virtual void OnPreValidation(Message receivedMessage, IClientChannel channel, InstanceContext instanceContext) {
    }

    /// <summary>Executes some logic when the validation of the received message failed. By default a warning logmessage is emitted.</summary>
    /// <param name="receivedMessage">The request message.</param>
    /// <param name="channel">The incoming channel.</param>
    /// <param name="instanceContext">The current service instance.</param>
    /// <param name="validationException">The exception that was thrown by the validation logic.</param>
    protected virtual void OnValidationError(Message receivedMessage, IClientChannel channel, InstanceContext instanceContext, XmlSchemaValidationException validationException) {
      Logger logger = LogManager.CreateLogger();
      logger.Warn(LogMessages.WarningReceivedMessageIsInvalid, "enkoni.framework", validationException);
    }

    /// <summary>When overriden executes some logic after the validation was completed succesfully.</summary>
    /// <param name="receivedMessage">The request message.</param>
    /// <param name="channel">The incoming channel.</param>
    /// <param name="instanceContext">The current service instance.</param>
    protected virtual void OnValidationSuccess(Message receivedMessage, IClientChannel channel, InstanceContext instanceContext) {
    }
    #endregion

    #region Private methods
    /// <summary>Validates the message using the supplied XSD-schemas.</summary>
    /// <param name="message">The message that is to be validated.</param>
    /// <exception cref="XmlSchemaValidationException">The body of the message does not comply with the schema.</exception>
    private void ValidateMessage(Message message) {
      XmlDocument bodyDoc = new XmlDocument();

      /* Write the contents (body) of the message to a memorystream */
      using(Stream stream = new MemoryStream()) {
        using(XmlWriter writer = XmlWriter.Create(stream)) {
          message.WriteBody(writer);
          writer.Flush();
          stream.Position = 0;

          /* Laad de inhoud van de memorystream in een Xmldocument */
          bodyDoc.Load(stream);
        }
      }

      /* Valideer de inhoud van het document */
      XmlReaderSettings settings = new XmlReaderSettings();
      settings.Schemas.Add(this.schemas);
      settings.ValidationType = ValidationType.Schema;

      using(XmlReader reader = XmlReader.Create(new XmlNodeReader(bodyDoc), settings)) {
        /* Within this while-loop the actual validation is executed. If the validation fails, a XmlSchemaValidationException will be thrown */
        while(reader.Read()) {
          /* Nothing specific to do, just keep reading */
        }
      }
    }
    #endregion
  }
}