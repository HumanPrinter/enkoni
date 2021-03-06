﻿using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Scope = "type", Target = "Enkoni.Framework.Validation.Validators.Configuration.DutchPhoneNumberAreaCodeCollection", Justification = "This class implements a collection type from the configuration namespace")]
[assembly: SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Scope = "type", Target = "Enkoni.Framework.Validation.Validators.Configuration.EmailDomainCollection", Justification = "This class implements a collection type from the configuration namespace")]

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Enkoni.Framework.Validation", Justification = "The types in this namespace do not belong in one of the sub-namespaces")]
[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Enkoni.Framework.Validation.Validators", Justification = "This is a false positive detected by Code Analysis")]

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Validation.Validators.Configuration.ValidatorsSection.#DeserializeElement(System.Xml.XmlReader,System.Boolean)", Justification = "The validity of the parameter is guaranteed by the base class.")]

[assembly: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "Enkoni.Framework.Validation.Validators.DutchPhoneNumberValidator+ConfiguredValuesSingletonContainer.#.cctor()", Justification = "The static constructor is added to prevent the C# compiler from marking the type as 'beforefieldinit'")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "Enkoni.Framework.Validation.Validators.EmailValidator+ConfiguredValuesSingletonContainer.#.cctor()", Justification = "The static constructor is added to prevent the C# compiler from marking the type as 'beforefieldinit'")]
