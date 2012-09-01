//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Holds the project's suppressions.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseSourceInfo.#SelectDbContext(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked through the Is*Specified method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseSourceInfo.#.ctor(System.Collections.Generic.Dictionary`2<System.String,System.Object>)", Justification = "Validation is done by the base constructor.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#FindAllCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.Boolean>>,Enkoni.Framework.SortSpecifications`1<!0>,System.Int32,Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked in the public method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#FindFirstCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.Boolean>>,Enkoni.Framework.SortSpecifications`1<!0>,Enkoni.Framework.Entities.DataSourceInfo,!0)", Justification = "The parameter is checked in the public method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#FindSingleCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.Boolean>>,Enkoni.Framework.Entities.DataSourceInfo,!0)", Justification = "The parameter is checked in the public method")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#DbContext", Justification = "This naming follows the naming convention of the EntityFramework CF CTP-library")]

[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EndsWith", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#CreateLikeExpressionCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.String>>,System.String)", Justification = "This is the name of the method")]
[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "StartsWith", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#CreateLikeExpressionCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.String>>,System.String)", Justification = "This is the name of the method")]
