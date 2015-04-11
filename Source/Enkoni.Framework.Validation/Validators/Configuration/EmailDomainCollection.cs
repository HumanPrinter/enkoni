using System;
using System.Configuration;

namespace Enkoni.Framework.Validation.Validators.Configuration {
  /// <summary>Defines the configuration element collection that holds the domains that should either be included or excluded from the list of domains
  /// that must be validated by the <see cref="EmailValidator"/>.</summary>
  public class EmailDomainCollection : ConfigurationElementCollection {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="EmailDomainCollection"/> class.</summary>
    public EmailDomainCollection() {
    }
    #endregion

    #region Properties
    /// <summary>Gets the type of the <see cref="ConfigurationElementCollection"/>.</summary>
    public override ConfigurationElementCollectionType CollectionType {
      get {
        return ConfigurationElementCollectionType.BasicMap;
      }
    }

    /// <summary>Gets the name used to identify this collection of elements in the configuration file.</summary>
    protected override string ElementName {
      get { return "domain"; }
    }

    /// <summary>Gets or sets a property, attribute, or child element of this configuration element.</summary>
    /// <param name="index">The index of the <see cref="EmailDomainConfigElement"/> to access.</param>
    /// <returns>The specified property, attribute, or child element.</returns>
    public EmailDomainConfigElement this[int index] {
      get {
        return (EmailDomainConfigElement)this.BaseGet(index); 
      }

      set {
        if(this.BaseGet(index) != null) {
          this.BaseRemoveAt(index);
        }

        this.BaseAdd(index, value);
      }
    }

    /// <summary>Gets or sets a property, attribute, or child element of this configuration element.</summary>
    /// <param name="pattern">The pattern of the <see cref="EmailDomainConfigElement"/> to access.</param>
    /// <returns>The specified property, attribute, or child element.</returns>
    public new EmailDomainConfigElement this[string pattern] {
      get { return (EmailDomainConfigElement)this.BaseGet(pattern); }
    }
    #endregion

    #region Public methods
    ///// <summary>Gets the index of the specified element.</summary>
    ///// <param name="element">The element of which the index must be returned.</param>
    ///// <returns>The index of the element.</returns>
    //public int IndexOf(EmailDomainConfigElement element) {
    //  return this.BaseIndexOf(element);
    //}

    ///// <summary>Adds an element to the collection.</summary>
    ///// <param name="element">The element that must be added.</param>
    //public void Add(EmailDomainConfigElement element) {
    //  this.BaseAdd(element);
    //}

    ///// <summary>Removes an element from the collection.</summary>
    ///// <param name="element">The element that must be removed.</param>
    //public void Remove(EmailDomainConfigElement element) {
    //  if(element == null) {
    //    return;
    //  }

    //  if(this.BaseIndexOf(element) >= 0) {
    //    this.BaseRemove(element.Pattern);
    //  }
    //}

    ///// <summary>Removes an element from the collection.</summary>
    ///// <param name="areaCode">The key of the element that must be removed.</param>
    //public void Remove(string areaCode) {
    //  this.BaseRemove(areaCode);
    //}

    ///// <summary>Removes an element from the collection.</summary>
    ///// <param name="index">The index of the element that must be removed.</param>
    //public void RemoveAt(int index) {
    //  this.BaseRemoveAt(index);
    //}

    ///// <summary>Clears the collection.</summary>
    //public void Clear() {
    //  this.BaseClear();
    //}
    #endregion

    #region Protected methods
    /// <summary>Creates a new <see cref="ConfigurationElement"/>.</summary>
    /// <returns>A new <see cref="ConfigurationElement"/>.</returns>
    protected override ConfigurationElement CreateNewElement() {
      return new EmailDomainConfigElement();
    }

    /// <summary>Gets the element key for a specified configuration element.</summary>
    /// <param name="element">The <see cref="ConfigurationElement"/> to return the key for.</param>
    /// <returns>An <see langword="object"/> that acts as the key for the specified <see cref="ConfigurationElement"/>.</returns>
    protected override object GetElementKey(ConfigurationElement element) {
      if(element == null) {
        throw new ArgumentNullException("element");
      }

      EmailDomainConfigElement configElement = element as EmailDomainConfigElement;
      if(configElement == null) {
        throw new ArgumentException("The supplied parameter is not of the expected type EnvironmentConfigurationElement");
      }

      return configElement.Pattern ?? string.Empty;
    }
    #endregion
  }
}
