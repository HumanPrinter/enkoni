using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Enkoni.Framework.ServiceModel", Justification = "A tipical false positive because of the resource files")]

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.ServiceModel.Extensions.#SafeClose(System.ServiceModel.ICommunicationObject)", Justification = "The parameter is validated by the Guard-class")]

[assembly: SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Enkoni.Framework.ServiceModel.SchemaValidationMessageInspector.#ValidateMessage(System.ServiceModel.Channels.Message)", Justification = "Once the instance is out of scope, it will be collected by the GC")]

[assembly: SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Enkoni.Framework.ServiceModel.SchemaValidationBehaviorExtensionElement.#CreateBehavior()", Justification = "This type can handle multiple calls to Dispose")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Enkoni.Framework.ServiceModel.SchemaValidationMessageInspector.#ValidateMessage(System.ServiceModel.Channels.Message)", Justification = "This type can handle multiple calls to Dispose")]