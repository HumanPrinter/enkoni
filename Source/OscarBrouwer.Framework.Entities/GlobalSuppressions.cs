//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds the project's suppressions.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db", Scope = "member", Target = "OscarBrouwer.Framework.Entities.DatabaseRepository`1.#DbContext", Justification = "This naming follows the naming convention of the EntityFramework CF CTP-library")]
