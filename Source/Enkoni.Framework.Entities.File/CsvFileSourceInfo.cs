//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvFileSourceInfo.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines a class that contains information about a CSV-filebased datasource that is used by the CsvFileRepository.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Enkoni.Framework.Entities {
	/// <summary>This class can be used by the <see cref="CsvFileRepository{TEntity}"/> or any of its descendants to retrieve valuable information 
	/// about the file that is to be used. This class is added for improved usability of the DataSourceInfo in combination with the CsvFileRepository.
	/// </summary>
	[Obsolete("The CsvFileSourceInfo is no longer supported. Use the FileSourceInfo instead", false)]
	public class CsvFileSourceInfo : FileSourceInfo {
		#region Constructors
		/// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using default values.</summary>
		public CsvFileSourceInfo()
			: base() {
		}

		/// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using the specified encoding for the sourcefile.</summary>
		/// <param name="sourceFileEncoding">The encoding of the sourcefile.</param>
		public CsvFileSourceInfo(Encoding sourceFileEncoding)
			: base((FileInfo)null, sourceFileEncoding) {
		}

		/// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using the specified values.</summary>
		/// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
		/// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
		/// <param name="changeCompleteTimeout">The timeout that is used to determine if a filechange has completed or not.</param>
		public CsvFileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile, int changeCompleteTimeout)
			: base(sourceFileInfo, monitorSourceFile, changeCompleteTimeout) {
		}

		/// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using the specified values.</summary>
		/// <param name="sourceFileInfo">The file information about the file that is used as datasource.</param>
		/// <param name="monitorSourceFile">Indicates if the monitor that watches the sourcefile must be started.</param>
		/// <param name="changeCompleteTimeout">The timeout that is used to determine if a filechange has completed or not.</param>
		/// <param name="sourceFileEncoding">The encoding of the sourcefile.</param>
		public CsvFileSourceInfo(FileInfo sourceFileInfo, bool monitorSourceFile, int changeCompleteTimeout, Encoding sourceFileEncoding)
			: base(sourceFileInfo, monitorSourceFile, changeCompleteTimeout, sourceFileEncoding) {
		}

		/// <summary>Initializes a new instance of the <see cref="CsvFileSourceInfo"/> class using the specified default values. If the default values do 
		/// not specify the supported properties, the default values will be used.</summary>
		/// <param name="defaultValues">The default values that are to be used.</param>
		public CsvFileSourceInfo(Dictionary<string, object> defaultValues)
			: base(defaultValues) {
		}
		#endregion
	}
}
