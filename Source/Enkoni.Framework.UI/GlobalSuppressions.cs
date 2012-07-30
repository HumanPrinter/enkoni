//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains the project's suppressions.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "Enkoni.Framework.UI.UIDispatcher.#Dispatcher", Justification = "The type Dispatcher is in fact immutable")]
