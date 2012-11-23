//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains the project's suppressions.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Enkoni.Framework.ServiceModel.SchemaValidationMessageInspector.#ValidateMessage(System.ServiceModel.Channels.Message)", Justification = "Once the instance is out of scope, it will be collected by the GC")]

[assembly: SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Enkoni.Framework.ServiceModel.SchemaValidationBehaviorExtensionElement.#CreateBehavior()", Justification = "This type can handle multiple calls to Dispose")]
[assembly: SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Scope = "member", Target = "Enkoni.Framework.ServiceModel.SchemaValidationMessageInspector.#ValidateMessage(System.ServiceModel.Channels.Message)", Justification = "This type can handle multiple calls to Dispose")]
