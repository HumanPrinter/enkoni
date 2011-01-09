//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Contains the project's suppressions.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------
 
using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "OscarBrouwer.Framework.Linq.Extensions.#IndexOf`1(System.Collections.Generic.List`1<!!0>,!!0,System.Int32,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "The method provides an 'overload' for a List-method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "OscarBrouwer.Framework.Linq.Extensions.#IndexOf`1(System.Collections.Generic.List`1<!!0>,!!0,System.Int32,System.Int32,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "The method provides an 'overload' for a List-method")]

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "OscarBrouwer.Framework.Linq", Justification = "The types in this namespace extend the functionality of the default Linq library")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "OscarBrouwer.Framework.Serialization", Justification = "The types in this namespace are only used for serialization")]

[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "OscarBrouwer.Framework.Extensions.#Fire(System.EventHandler,System.Object,System.EventArgs)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "OscarBrouwer.Framework.Extensions.#Fire`1(System.EventHandler`1<!!0>,System.Object,!!0)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "OscarBrouwer.Framework.Extensions.#FireAsync(System.EventHandler,System.Object,System.EventArgs)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "OscarBrouwer.Framework.Extensions.#FireAsync`1(System.EventHandler`1<!!0>,System.Object,!!0)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "OscarBrouwer.Framework.Extensions.#FireInParallel(System.EventHandler,System.Object,System.EventArgs)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "OscarBrouwer.Framework.Extensions.#FireInParallel`1(System.EventHandler`1<!!0>,System.Object,!!0)", Justification = "Intentionaly constructed as a extension method")]

[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OscarBrouwer.Framework.Workflow.#StopWorkflowHelper(System.Object)", Justification = "The exception is stored and rethrown later on, so no harm's done")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OscarBrouwer.Framework.Workflow.#StartWorkflowHelper(System.Object)", Justification = "The exception is stored and rethrown later on, so no harm's done")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OscarBrouwer.Framework.Workflow.#PauseWorkflowHelper(System.Object)", Justification = "The exception is stored and rethrown later on, so no harm's done")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "OscarBrouwer.Framework.Workflow.#ContinueWorkflowHelper(System.Object)", Justification = "The exception is stored and rethrown later on, so no harm's done")]

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "OscarBrouwer.Framework.Serialization.CsvSerializer`1.#Serialize(System.Collections.Generic.IEnumerable`1<!0>)", Justification = "The parameter is already checked in the public method")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "And", Scope = "member", Target = "OscarBrouwer.Framework.ISpecification`1.#And(OscarBrouwer.Framework.ISpecification`1<!0>)", Justification = "There simply is no better name for it")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Not", Scope = "member", Target = "OscarBrouwer.Framework.ISpecification`1.#Not()", Justification = "There simply is no better name for it")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Or", Scope = "member", Target = "OscarBrouwer.Framework.ISpecification`1.#Or(OscarBrouwer.Framework.ISpecification`1<!0>)", Justification = "There simply is no better name for it")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Like", Scope = "member", Target = "OscarBrouwer.Framework.SpecificationFactory`1.#Like(System.Func`2<!0,System.String>,System.String)", Justification = "There simply is no better name for it")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Continue", Scope = "member", Target = "OscarBrouwer.Framework.IWorkflow.#Continue()", Justification = "There simply is no better name for it")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop", Scope = "member", Target = "OscarBrouwer.Framework.IWorkflow.#Stop()", Justification = "There simply is no better name for it")]

[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OscarBrouwer.Framework.Workflow.#EndContinue(System.IAsyncResult)", Justification = "By keeping it an instance-member, its use is more logical for endusers")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OscarBrouwer.Framework.Workflow.#EndPause(System.IAsyncResult)", Justification = "By keeping it an instance-member, its use is more logical for endusers")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OscarBrouwer.Framework.Workflow.#EndStart(System.IAsyncResult)", Justification = "By keeping it an instance-member, its use is more logical for endusers")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "OscarBrouwer.Framework.Workflow.#EndStop(System.IAsyncResult)", Justification = "By keeping it an instance-member, its use is more logical for endusers")]

[assembly: SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "OscarBrouwer.Framework.AsyncResultVoid.#AsyncWaitHandle", Justification = "Disposing is handled at a different place")]

[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CompareTo", Scope = "member", Target = "OscarBrouwer.Framework.Comparer`1.#Compare(!0,!0)", Justification = "The spelling is correct")]
[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CsvRecord-attribute", Scope = "member", Target = "OscarBrouwer.Framework.Serialization.CsvSerializer`1.#.ctor()", Justification = "The spelling is correct")]
