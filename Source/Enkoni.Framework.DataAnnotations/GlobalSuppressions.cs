using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Scope = "type", Target = "Enkoni.Framework.DataAnnotations.Configuration.DutchPhoneNumberAreaCodeCollection", Justification = "This class implements a collection type from the configuration namespace")]
[assembly: SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface", Scope = "type", Target = "Enkoni.Framework.DataAnnotations.Configuration.EmailDomainCollection", Justification = "This class implements a collection type from the configuration namespace")]

[assembly: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "Enkoni.Framework.DataAnnotations.IbanAttribute", Justification = "This is a design choice in the DataAnnotations subsystem made by Microsoft")]
[assembly: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "Enkoni.Framework.DataAnnotations.EmailAttribute", Justification = "This is a design choice in the DataAnnotations subsystem made by Microsoft")]
[assembly: SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Scope = "type", Target = "Enkoni.Framework.DataAnnotations.DutchPhoneNumberAttribute", Justification = "This is a design choice in the DataAnnotations subsystem made by Microsoft")]

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.DataAnnotations.Configuration.ValidationSection.#DeserializeElement(System.Xml.XmlReader,System.Boolean)", Justification = "The validity of the parameter is guaranteed by the base class.")]

[assembly: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "Enkoni.Framework.DataAnnotations.DutchPhoneNumberAttribute+ConfiguredValuesSingletonContainer.#.cctor()", Justification = "The static constructor is added to prevent the C# compiler from marking the type as 'beforefieldinit'")]
[assembly: SuppressMessage("Microsoft.Performance", "CA1810:InitializeReferenceTypeStaticFieldsInline", Scope = "member", Target = "Enkoni.Framework.DataAnnotations.EmailAttribute+ConfiguredValuesSingletonContainer.#.cctor()", Justification = "The static constructor is added to prevent the C# compiler from marking the type as 'beforefieldinit'")]

[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EnvironmentConfigurationElement", Scope = "member", Target = "Enkoni.Framework.DataAnnotations.Configuration.EmailDomainCollection.#GetElementKey(System.Configuration.ConfigurationElement)", Justification = "The spelling is correct")]