﻿using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Enkoni.Framework.Entities", Justification = "A typical false positive from FxCop")]

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "2", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#FindFirstCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.Boolean>>,Enkoni.Framework.SortSpecifications`1<!0>,System.String[],Enkoni.Framework.Entities.DataSourceInfo,!0)", Justification = "The parameter is checked in the public method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#FindSingleCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.Boolean>>,System.String[],Enkoni.Framework.Entities.DataSourceInfo,!0)", Justification = "The parameter is checked in the public method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1+EntityCastRemoverVisitor.#VisitUnary(System.Linq.Expressions.UnaryExpression)", Justification = "The parameter is checked in the base implementation")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1709:IdentifiersShouldBeCasedCorrectly", MessageId = "Db", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#DbContext", Justification = "This naming follows the naming convention of the EntityFramework CF CTP-library")]

[assembly: SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "removeResult", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#AddExistingEntities(System.Collections.Generic.IEnumerable`1<!0>,System.Collections.Generic.List`1<!0>,System.Collections.Generic.List`1<!0>,System.Collections.Generic.List`1<!0>,Enkoni.Framework.Entities.EntityEqualityComparer`1<!0>,System.Data.Entity.DbContext,System.Collections.Generic.Dictionary`2<!0,!0>)", Justification = "Variable is used in Debug-mode")]

[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EndsWith", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#CreateLikeExpressionCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.String>>,System.String)", Justification = "This is the name of the method")]
[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "StartsWith", Scope = "member", Target = "Enkoni.Framework.Entities.DatabaseRepository`1.#CreateLikeExpressionCore(System.Linq.Expressions.Expression`1<System.Func`2<!0,System.String>>,System.String)", Justification = "This is the name of the method")]