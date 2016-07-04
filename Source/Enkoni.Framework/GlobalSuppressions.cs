﻿using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Enkoni.Framework.Collections.Extensions.#IndexOf`1(System.Collections.Generic.List`1<!!0>,!!0,System.Int32,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "The method provides an 'overload' for a List-method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists", Scope = "member", Target = "Enkoni.Framework.Collections.Extensions.#IndexOf`1(System.Collections.Generic.List`1<!!0>,!!0,System.Int32,System.Int32,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "The method provides an 'overload' for a List-method")]

[assembly: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Enkoni.Framework.Guard.#ArgumentIsNotOfType`1(System.Boolean,System.Object,System.String,System.String)", Justification = "This is an accepted type")]
[assembly: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Enkoni.Framework.Guard.#ArgumentIsNotOfType`1(System.Object,System.String,System.String)", Justification = "This is an accepted type")]
[assembly: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Enkoni.Framework.Guard.#ArgumentIsOfType`1(System.Boolean,System.Object,System.String,System.String)", Justification = "This is an accepted type")]
[assembly: SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter", Scope = "member", Target = "Enkoni.Framework.Guard.#ArgumentIsOfType`1(System.Object,System.String,System.String)", Justification = "This is an accepted type")]

[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Enkoni.Framework.Linq.Queryable.#FirstOrDefault`1(System.Linq.IQueryable`1<!!0>,System.Linq.Expressions.Expression`1<System.Func`2<!!0,System.Boolean>>,!!0)", Justification = "This is an accepted type and the user will hardly ever see it")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Enkoni.Framework.Linq.Queryable.#LastOrDefault`1(System.Linq.IQueryable`1<!!0>,System.Linq.Expressions.Expression`1<System.Func`2<!!0,System.Boolean>>,!!0)", Justification = "This is an accepted type and the user will hardly ever see it")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Enkoni.Framework.Linq.Queryable.#SingleOrDefault`1(System.Linq.IQueryable`1<!!0>,System.Linq.Expressions.Expression`1<System.Func`2<!!0,System.Boolean>>,!!0)", Justification = "This is an accepted type and the user will hardly ever see it")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Enkoni.Framework.Linq.ExpressionExtensions.#Not`1(System.Linq.Expressions.Expression`1<System.Func`2<!!0,System.Boolean>>)", Justification = "This is an accepted type and the user will hardly ever see it")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Enkoni.Framework.Linq.Enumerable.#Partition`2(System.Collections.Generic.IEnumerable`1<!!1>,System.Func`2<!!1,!!0>)", Justification = "This is an accepted type and the user will hardly ever see it")]
[assembly: SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Scope = "member", Target = "Enkoni.Framework.Linq.Enumerable.#Partition`2(System.Collections.Generic.IEnumerable`1<!!1>,System.Func`2<!!1,!!0>,System.Collections.Generic.IEqualityComparer`1<!!0>)", Justification = "This is an accepted type and the user will hardly ever see it")]

[assembly: SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters", Scope = "member", Target = "Enkoni.Framework.Linq.ExpressionExtensions.#Not`1(System.Linq.Expressions.Expression`1<System.Func`2<!!0,System.Boolean>>)", Justification = "This type is more applicable")]

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Enkoni.Framework.Linq", Justification = "The types in this namespace extend the functionality of the default Linq library")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Enkoni.Framework.Xml", Justification = "The types in this namespace extend the functionality of the default Xml library")]

[assembly: SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Scope = "member", Target = "Enkoni.Framework.Collections.CircularStack`1.#GetEnumeratorCore()", Justification = "A method is more appropriate here")]

[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Enkoni.Framework.EventHandlerExtensions.#Fire(System.EventHandler,System.Object,System.EventArgs)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Enkoni.Framework.EventHandlerExtensions.#Fire`1(System.EventHandler`1<!!0>,System.Object,!!0)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Enkoni.Framework.EventHandlerExtensions.#FireAsync(System.EventHandler,System.Object,System.EventArgs)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Enkoni.Framework.EventHandlerExtensions.#FireAsync`1(System.EventHandler`1<!!0>,System.Object,!!0)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Enkoni.Framework.EventHandlerExtensions.#FireInParallel(System.EventHandler,System.Object,System.EventArgs)", Justification = "Intentionaly constructed as a extension method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate", Scope = "member", Target = "Enkoni.Framework.EventHandlerExtensions.#FireInParallel`1(System.EventHandler`1<!!0>,System.Object,!!0)", Justification = "Intentionaly constructed as a extension method")]

[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Enkoni.Framework.Workflow.#StopWorkflowHelper(System.Object)", Justification = "The exception is stored and rethrown later on, so no harm's done")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Enkoni.Framework.Workflow.#StartWorkflowHelper(System.Object)", Justification = "The exception is stored and rethrown later on, so no harm's done")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Enkoni.Framework.Workflow.#PauseWorkflowHelper(System.Object)", Justification = "The exception is stored and rethrown later on, so no harm's done")]
[assembly: SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Scope = "member", Target = "Enkoni.Framework.Workflow.#ContinueWorkflowHelper(System.Object)", Justification = "The exception is stored and rethrown later on, so no harm's done")]

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Collections.CircularStack`1.#CopyTo(System.Array,System.Int32)", Justification = "The parameter is already checked in the public method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Collections.CircularStack`1.#CopyToCore(!0[],System.Int32)", Justification = "The parameter is already checked in the public method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "Enkoni.Framework.Guard.#ArgumentIsNotOfType`1(System.Boolean,System.Object,System.String,System.String)", Justification = "The parameter is validated by the Guard-class")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1", Scope = "member", Target = "Enkoni.Framework.Guard.#ArgumentIsOfType`1(System.Boolean,System.Object,System.String,System.String)", Justification = "The parameter is validated by the Guard-class")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Scope = "type", Target = "Enkoni.Framework.Collections.CircularStack`1", Justification = "The class behaves as a stack even though it doesn't extend it")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope = "type", Target = "Enkoni.Framework.Collections.CircularStack`1", Justification = "The class behaves as a stack even though it doesn't extend it")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Continue", Scope = "member", Target = "Enkoni.Framework.IWorkflow.#Continue()", Justification = "There simply is no better name for it")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Stop", Scope = "member", Target = "Enkoni.Framework.IWorkflow.#Stop()", Justification = "There simply is no better name for it")]

[assembly: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flag", Scope = "member", Target = "Enkoni.Framework.EnumHelper.#SetFlag`1(!!0,!!0)", Justification = "The term 'flag' is appropriate here")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flag", Scope = "member", Target = "Enkoni.Framework.EnumHelper.#UnsetFlag`1(!!0,!!0)", Justification = "The term 'flag' is appropriate here")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "Flag", Scope = "member", Target = "Enkoni.Framework.EnumHelper.#ToggleFlag`1(!!0,!!0)", Justification = "The term 'flag' is appropriate here")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag", Scope = "member", Target = "Enkoni.Framework.EnumHelper.#SetFlag`1(!!0,!!0)", Justification = "The term 'flag' is appropriate here")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag", Scope = "member", Target = "Enkoni.Framework.EnumHelper.#UnsetFlag`1(!!0,!!0)", Justification = "The term 'flag' is appropriate here")]
[assembly: SuppressMessage("Microsoft.Naming", "CA1726:UsePreferredTerms", MessageId = "flag", Scope = "member", Target = "Enkoni.Framework.EnumHelper.#ToggleFlag`1(!!0,!!0)", Justification = "The term 'flag' is appropriate here")]

[assembly: SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "source", Scope = "member", Target = "Enkoni.Framework.Linq.Enumerable.#CreateEqualityComparer`2(System.Collections.Generic.IEnumerable`1<!!0>,System.Func`2<!!0,!!1>)", Justification = "The parameter is crucial for the extension mechanism to work")]

[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Enkoni.Framework.Workflow.#EndContinue(System.IAsyncResult)", Justification = "By keeping it an instance-member, its use is more logical for endusers")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Enkoni.Framework.Workflow.#EndPause(System.IAsyncResult)", Justification = "By keeping it an instance-member, its use is more logical for end users")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Enkoni.Framework.Workflow.#EndStart(System.IAsyncResult)", Justification = "By keeping it an instance-member, its use is more logical for end users")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic", Scope = "member", Target = "Enkoni.Framework.Workflow.#EndStop(System.IAsyncResult)", Justification = "By keeping it an instance-member, its use is more logical for end users")]

[assembly: SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Enkoni.Framework.AsyncResultVoid.#AsyncWaitHandle", Justification = "Disposing is handled at a different place")]
[assembly: SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Scope = "member", Target = "Enkoni.Framework.AsyncResult`1.#AsyncWaitHandle", Justification = "Disposing is handled at a different place")]

[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "CompareTo", Scope = "member", Target = "Enkoni.Framework.Collections.Comparer`1.#Compare(!0,!0)", Justification = "The spelling is correct")]
[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "GetEnumerator", Scope = "member", Target = "Enkoni.Framework.Collections.CircularStack`1+Enumerator.#Current", Justification = "Literal is the actual name of the method")]