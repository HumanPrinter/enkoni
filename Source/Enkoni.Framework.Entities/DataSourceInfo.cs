using System;
using System.Collections.Generic;

namespace Enkoni.Framework.Entities {
  /// <summary>This class contains information about a specific data source. This can be any kind of data source. This class is utilized by the various 
  /// repositories. Check the documentation of the used repository for more information about the required or supported data source-information.
  /// </summary>
  public class DataSourceInfo {
    #region Public constants
    /// <summary>Defines the key that is used to store and retrieve the boolean that indicates if any entity that originates from the data source must 
    /// be cloned or not.</summary>
    public const string CloneDataSourceItemsKey = "CloneDataSourceItems";

    /// <summary>The default value for the <see cref="CloneDataSourceItems"/> that will be used when no custom value has been specified.</summary>
    public const bool DefaultCloneDataSourceItems = false;
    #endregion

    #region Instance variables
    /// <summary>The collection that holds the keys with the associated values.</summary>
    private Dictionary<string, object> sourceInfoItems;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DataSourceInfo"/> class.</summary>
    public DataSourceInfo()
      : this(DefaultCloneDataSourceItems) {
    }

    /// <summary>Initializes a new instance of the <see cref="DataSourceInfo"/> class.</summary>
    /// <param name="cloneDataSourceItems">Indicates whether or not any entity that originate from the data source should be cloned or not.</param>
    public DataSourceInfo(bool cloneDataSourceItems) {
      this.sourceInfoItems = new Dictionary<string, object>();
      this.CloneDataSourceItems = cloneDataSourceItems;
    }

    /// <summary>Initializes a new instance of the <see cref="DataSourceInfo"/> class using the specified values.</summary>
    /// <param name="defaultValues">The default values that must be loaded.</param>
    /// <exception cref="ArgumentNullException">The parameter is null.</exception>
    public DataSourceInfo(Dictionary<string, object> defaultValues) {
      Guard.ArgumentIsNotNull(defaultValues, nameof(defaultValues));
      
      /* Add all the specified default values to the source information. */
      foreach(KeyValuePair<string, object> defaultValue in defaultValues) {
        this[defaultValue.Key] = defaultValue.Value;
      }

      /* Check if the dictionary contains the reserved key and, if so, the key denotes a value of the correct type */
      if(defaultValues.ContainsKey(CloneDataSourceItemsKey) && !(defaultValues[CloneDataSourceItemsKey] is bool)) {
        /* The key is present, but the value is of the wrong type; use the default value. */
        this.CloneDataSourceItems = DefaultCloneDataSourceItems;
      }
    }
    #endregion

    #region Public properties
    /// <summary>Gets or sets a value indicating whether any retrieved entity that originates from the data source should be cloned or not.</summary>
    public bool CloneDataSourceItems {
      get { return (bool)this[CloneDataSourceItemsKey]; }
      set { this[CloneDataSourceItemsKey] = value; }
    }

    /// <summary>Gets or sets the value that is associated with the specified key.</summary>
    /// <param name="key">The key with which the value is identified.</param>
    /// <returns>The value that is associated with the specified key.</returns>
    public object this[string key] {
      get { return this.sourceInfoItems[key]; }
      set { this.sourceInfoItems[key] = value; }
    }
    #endregion

    #region Public static methods
    /// <summary>Determines if the 'clone data source items'-flag is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns><see langword="true"/> if the flag is defined; <see langword="false"/> otherwise.</returns>
    public static bool IsCloneDataSourceItemsSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(CloneDataSourceItemsKey);
    }

    /// <summary>Selects the 'clone data source items'-flag from the specified data source information.</summary>
    /// <param name="dataSourceInfo">The data source information that is queried.</param>
    /// <returns>The flag that is stored in the data source information or <see langword="false"/> if the flag could not be found.</returns>
    public static bool SelectCloneDataSourceItems(DataSourceInfo dataSourceInfo) {
      if(IsCloneDataSourceItemsSpecified(dataSourceInfo)) {
        return (bool)dataSourceInfo[CloneDataSourceItemsKey];
      }
      else {
        return DefaultCloneDataSourceItems;
      }
    }
    #endregion

    #region Public methods
    /// <summary>Determines if a value is specified by looking up the specified key in the internal storage.</summary>
    /// <param name="key">The key that identifies the desired value.</param>
    /// <returns><see langword="true"/> if the key (and accompanying value) is defined; <see langword="false"/> otherwise.</returns>
    public bool IsValueSpecified(string key) {
      return this.sourceInfoItems.ContainsKey(key) && this.sourceInfoItems[key] != null;
    }

    /// <summary>Determines if the 'clone data source items'-flag is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the flag is defined; <see langword="false"/> otherwise.</returns>
    public bool IsCloneDataSourceItemsSpecified() {
      return this.IsValueSpecified(CloneDataSourceItemsKey);
    }
    #endregion
  }
}
