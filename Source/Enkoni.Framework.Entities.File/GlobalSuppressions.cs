﻿//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds the project's suppressions.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.FileSourceInfo.#SelectMonitorSourceFile(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked through the Is*Specified method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.FileSourceInfo.#SelectSourceFileInfo(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked through the Is*Specified method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.FileSourceInfo.#SelectChangeCompleteTimeout(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked through the Is*Specified method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.FileSourceInfo.#.ctor(System.Collections.Generic.Dictionary`2<System.String,System.Object>)", Justification = "Validation is done by the base constructor.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.CsvFileSourceInfo.#SelectSourceFileEncoding(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The parameter is checked through the Is*Specified method")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.CsvFileSourceInfo.#.ctor(System.Collections.Generic.Dictionary`2<System.String,System.Object>)", Justification = "Validation is done by the base constructor.")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.CsvFileRepository`1.#ReadAllRecordsFromFile(System.IO.FileInfo,Enkoni.Framework.Entities.DataSourceInfo)", Justification = "Validation is done by the baseclass")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.CsvFileRepository`1.#WriteAllRecordsToFile(System.IO.FileInfo,Enkoni.Framework.Entities.DataSourceInfo,System.Collections.Generic.IEnumerable`1<!0>)", Justification = "Validation is done by the baseclass")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.XmlFileRepository`1.#ReadAllRecordsFromFile(System.IO.FileInfo,Enkoni.Framework.Entities.DataSourceInfo)", Justification = "Validation is done by the baseclass")]
[assembly: SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Scope = "member", Target = "Enkoni.Framework.Entities.XmlFileRepository`1.#WriteAllRecordsToFile(System.IO.FileInfo,Enkoni.Framework.Entities.DataSourceInfo,System.Collections.Generic.IEnumerable`1<!0>)", Justification = "Validation is done by the baseclass")]

[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "FileRepository", Scope = "member", Target = "Enkoni.Framework.Entities.FileRepository`1.#.ctor(Enkoni.Framework.Entities.DataSourceInfo)", Justification = "The spelling here is fine")]
