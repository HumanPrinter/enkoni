using System;
using System.Configuration;

namespace Enkoni.Framework.DataAnnotations.Configuration {
  /// <summary>Defines the configuration element collection that holds the domains that should either be included or excluded from the list of domains
  /// that must be validated by the <see cref="EmailAttribute"/>.</summary>
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
      Guard.ArgumentIsNotNull(element, nameof(element));
      Guard.ArgumentIsOfType<EmailDomainConfigElement>(element, nameof(element), "The supplied parameter is not of the expected type EnvironmentConfigurationElement");

      EmailDomainConfigElement configElement = element as EmailDomainConfigElement;
      return configElement.Pattern ?? string.Empty;
    }

    #endregion
  }
}
