//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizedDescriptionAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains an attribute that can be used to define how an class member must be converted to a string.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.ComponentModel;

namespace Enkoni.Framework {
  /// <summary>Defines an attribute that can be used to define a localizable description for a type or member.</summary>
  [AttributeUsage(AttributeTargets.All)]
  public sealed class LocalizedDescriptionAttribute : DescriptionAttribute {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="LocalizedDescriptionAttribute"/> class.</summary>
    /// <param name="resourceKey">The key with which the description must be retrieved from the resources.</param>
    public LocalizedDescriptionAttribute(string resourceKey)
      : this(resourceKey, null) {
      this.ResourceKey = resourceKey;
    }

    /// <summary>Initializes a new instance of the <see cref="LocalizedDescriptionAttribute"/> class.</summary>
    /// <param name="resourceKey">The key with which the description must be retrieved from the resources.</param>
    /// <param name="resourceType">The type of the resources that contains the description.</param>
    public LocalizedDescriptionAttribute(string resourceKey, Type resourceType)
      : base(string.Empty) {
      this.ResourceKey = resourceKey;
      this.ResourceType = resourceType;
      this.DefaultDescription = null;
    }
    #endregion

    #region Properties
    /// <summary>Gets the key with which the description must be retrieved from the resources.</summary>
    public string ResourceKey { get; private set; }

    /// <summary>Gets the type of the resources that contains the description.</summary>
    public Type ResourceType { get; private set; }

    /// <summary>Gets or sets the default description in case the resource key could not be found in the resources.</summary>
    public string DefaultDescription {
      get { return this.DescriptionValue; }
      set { this.DescriptionValue = value; }
    }
    #endregion
  }
}
