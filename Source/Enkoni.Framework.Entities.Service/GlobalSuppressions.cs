//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds the project's suppressions.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.ServiceSourceInfo.#SelectEndpointConfigurationName(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked through the Is*Specified method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.ServiceSourceInfo.#SelectBinding(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked through the Is*Specified method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.ServiceSourceInfo.#SelectRemoteAddress(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked through the Is*Specified method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.ServiceSourceInfo.#.ctor(System.Collections.Generic.Dictionary`2<System.String,System.Object>)", Justification = "Validation is done by the base constructor.")]
