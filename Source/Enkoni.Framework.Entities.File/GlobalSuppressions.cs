using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1020:AvoidNamespacesWithFewTypes", Scope = "namespace", Target = "Enkoni.Framework.Entities", Justification = "A typical false positive from FxCop")]

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.CsvFileRepository`1.#ReadAllRecordsFromFile(System.IO.FileInfo,Enkoni.Framework.Entities.DataSourceInfo)", Justification = "Validation is done by the baseclass")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.CsvFileRepository`1.#WriteAllRecordsToFile(System.IO.FileInfo,Enkoni.Framework.Entities.DataSourceInfo,System.Collections.Generic.IEnumerable`1<!0>)", Justification = "Validation is done by the baseclass")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.XmlFileRepository`1.#ReadAllRecordsFromFile(System.IO.FileInfo,Enkoni.Framework.Entities.DataSourceInfo)", Justification = "Validation is done by the baseclass")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.XmlFileRepository`1.#WriteAllRecordsToFile(System.IO.FileInfo,Enkoni.Framework.Entities.DataSourceInfo,System.Collections.Generic.IEnumerable`1<!0>)", Justification = "Validation is done by the baseclass")]

[assembly: SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "removeResult", Scope = "member", Target = "Enkoni.Framework.Entities.FileRepository`1.#AddEntitiesCore(System.Collections.Generic.IEnumerable`1<!0>,Enkoni.Framework.Entities.DataSourceInfo)", Justification = "Variable is used in Debug-mode")]

[assembly: SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Scope = "member", Target = "Enkoni.Framework.Entities.FileSourceInfo.#DefaultSourceFileEncoding", Justification = "The type Encoding is immutable")]

[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "FileRepository", Scope = "member", Target = "Enkoni.Framework.Entities.FileRepository`1.#.ctor(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The spelling here is fine")]

[assembly: SuppressMessage("StyleCop.CSharp.OrderingRules", "SA1200:Using directives must be placed correctly", Scope = "namespace", Target = "~N:Enkoni.Framework.Entities", Justification = "Required to fix name collision with System.Threading.Timer")]