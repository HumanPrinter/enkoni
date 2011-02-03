//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="MemorySourceInfo.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines a class that contains information about a memory datasource that is used by the MemoryRepository.
//     Note: Since the static MemorySourceInfo class is only a container for the static members of the generic 
//     MemorySourceInfo class, they are both placed in the same file.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace OscarBrouwer.Framework.Entities {
  /// <summary>This class contains static members that would normally be part of the <see cref="MemorySourceInfo{T}"/> 
  /// class, but since that class is generic the static members are placed in this non-generic counterpart to avoid possible
  /// confussion about the use of the methods.</summary>
  public static class MemorySourceInfo {
    #region Public constants
    /// <summary>Defines the key that is used to store and retrieve the MemoryStore.</summary>
    public const string MemoryStoreKey = "MemoryStore";
    #endregion

    #region Public static methods
    /// <summary>Determines if the MemoryStore is specified in the source information.</summary>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns><see langword="true"/> if the MemoryStore is defined; <see langword="false"/> otherwise.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public static bool IsMemoryStoreSpecified(DataSourceInfo dataSourceInfo) {
      return dataSourceInfo != null && dataSourceInfo.IsValueSpecified(MemoryStoreKey);
    }

    /// <summary>Selects the MemoryStore from the specified datasource information.</summary>
    /// <typeparam name="T">The type of object that is stored in the memorystore.</typeparam>
    /// <param name="dataSourceInfo">The datasource information that is queried.</param>
    /// <returns>The MemoryStore that is stored in the datasource information or <see langword="null"/> if the MemoryStore 
    /// could not be found.</returns>
    public static MemoryStore<T> SelectMemoryStore<T>(DataSourceInfo dataSourceInfo) where T : class {
      if(IsMemoryStoreSpecified(dataSourceInfo)) {
        return dataSourceInfo[MemoryStoreKey] as MemoryStore<T>;
      }
      else {
        return null;
      }
    }
    #endregion
  }

  /// <summary>This class can be used by the <see cref="MemoryRepository{TEntity}"/> to retrieve valuable information about 
  /// the datastore that is to be used. This class is added for improved usability of the DataSourceInfo in combination with 
  /// the MemoryRepository.</summary>
  /// <typeparam name="T">The type of object that is stored in memory.</typeparam>
  [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
      Justification = "Since the static class is merely a container for the static members of the non-static class, they can be in the same file")]
  public class MemorySourceInfo<T> : DataSourceInfo where T : class {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="MemorySourceInfo{T}"/> class using a default value of
    /// <see langword="null"/> for the MemoryStore.</summary>
    public MemorySourceInfo()
      : this((MemoryStore<T>)null) {
    }

    /// <summary>Initializes a new instance of the <see cref="MemorySourceInfo{T}"/> class using the specified memorystore
    /// instance.</summary>
    /// <param name="memoryStore">The memorystore that must be use to store the entities.</param>
    public MemorySourceInfo(MemoryStore<T> memoryStore)
      : base() {
      this.MemoryStore = memoryStore;
    }

    /// <summary>Initializes a new instance of the <see cref="MemorySourceInfo{T}"/> class using the specified default 
    /// values. If the default values do not specify the MemoryStore using the correct key and/or type, the value 
    /// <see langword="null"/> will be used.</summary>
    /// <param name="defaultValues">The default values that are to be used.</param>
    [SuppressMessage("Microsoft.StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis",
      Justification = "StyleCop doesn't correctly recognizes this construction, but using 'this' to reference MemorySourceInfo is not possible")]
    public MemorySourceInfo(Dictionary<string, object> defaultValues)
      : base(defaultValues) {
      /* Check if the dictionary contains the reserved key and, if so, the key denotes a value of the correct type */
      if(defaultValues.ContainsKey(MemorySourceInfo.MemoryStoreKey) && !(defaultValues[MemorySourceInfo.MemoryStoreKey] is MemoryStore<T>)) {
        /* The key is present, but the value is of the wrong type; use 'null' as the default value. */
        this.MemoryStore = null;
      }
    }
    #endregion

    #region Public properties
    /// <summary>Gets or sets the MemoryStore that is to be used by the MemoryRepository.</summary>
    [SuppressMessage("Microsoft.StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis",
      Justification = "StyleCop doesn't correctly recognizes this construction, but using 'this' to reference MemorySourceInfo is not possible")]
    public MemoryStore<T> MemoryStore {
      get { return (MemoryStore<T>)this[MemorySourceInfo.MemoryStoreKey]; }
      set { this[MemorySourceInfo.MemoryStoreKey] = value; }
    }
    #endregion

    #region Public methods
    /// <summary>Determines if the MemoryStore is specified in the source information.</summary>
    /// <returns><see langword="true"/> if the MemoryStore is defined; <see langword="false"/> otherwise.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    [SuppressMessage("Microsoft.StyleCop.CSharp.ReadabilityRules", "SA1101:PrefixLocalCallsWithThis",
      Justification = "StyleCop doesn't correctly recognizes this construction, but using 'this' to reference MemorySourceInfo is not possible")]
    public bool IsMemoryStoreSpecified() {
      return this.IsValueSpecified(MemorySourceInfo.MemoryStoreKey);
    }
    #endregion
  }
}
