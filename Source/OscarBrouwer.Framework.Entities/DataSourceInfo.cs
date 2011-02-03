//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="DataSourceInfo.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines a class that contains information about a certain datasource that is used by a repository.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This class contains information about a specific datasource. This can be any kind of datasource. This class 
  /// is utilized by the various repositories. Check the documentation of the used repository for more information about 
  /// the required or supported datasource-information.</summary>
  public class DataSourceInfo {
    #region Instance variables
    /// <summary>The collection that holds the keys with the associated values.</summary>
    private Dictionary<string, object> sourceInfoItems;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DataSourceInfo"/> class.</summary>
    public DataSourceInfo() {
      this.sourceInfoItems = new Dictionary<string, object>();
    }

    /// <summary>Initializes a new instance of the <see cref="DataSourceInfo"/> class using the specified values.</summary>
    /// <param name="defaultValues">The default values that must be loaded.</param>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    public DataSourceInfo(Dictionary<string, object> defaultValues) {
      if(defaultValues == null) {
        throw new ArgumentNullException("defaultValues");
      }

      /* Add all the specified default values to the source information. */
      foreach(KeyValuePair<string, object> defaultValue in defaultValues) {
        this[defaultValue.Key] = defaultValue.Value;
      }
    }
    #endregion

    #region Public properties
    /// <summary>Gets or sets the value that is associated with the specified key.</summary>
    /// <param name="key">The key with which the value is identified.</param>
    /// <returns>The value that is associated with the specified key.</returns>
    public object this[string key] {
      get { return this.sourceInfoItems[key]; }
      set { this.sourceInfoItems[key] = value; }
    }
    #endregion

    #region Public methods
    /// <summary>Determines if a value is specified by looking up the specified key in the internal storage.</summary>
    /// <param name="key">The key that identifies the desired value.</param>
    /// <returns><see langword="true"/> if the key (and accompanying value) is defined; <see langword="false"/> otherwise.
    /// </returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public bool IsValueSpecified(string key) {
      return this.sourceInfoItems.ContainsKey(key) && this.sourceInfoItems[key] != null;
    }
    #endregion
  }
}
