using System.Configuration;
using System.Xml;

using Enkoni.Framework.Validation.Properties;

namespace Enkoni.Framework.Validation.Validators.Configuration {
  /// <summary>Defines the configuration element that can be used to configure the <see cref="EmailValidator"/>.</summary>
  public class EmailValidatorConfigElement : ConfigurationElement {
    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="EmailValidatorConfigElement"/> class.</summary>
    public EmailValidatorConfigElement() {
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets the name of the validator.</summary>
    [ConfigurationProperty("name", IsKey = true, IsRequired = false, DefaultValue = EmailValidator.DefaultName)]
    public string Name {
      get { return (string)this["name"]; }
      set { this["name"] = value; }
    }

    /// <summary>Gets or sets a value indicating whether comments are allowed in the e-mail address.</summary>
    [ConfigurationProperty("allowComments", IsKey = false, IsRequired = false, DefaultValue = false)]
    public bool AllowComments {
      get { return (bool)this["allowComments"]; }
      set { this["allowComments"] = value; }
    }

    /// <summary>Gets or sets a value indicating whether IP addresses are allowed as domain part.</summary>
    [ConfigurationProperty("allowIPAddresses", IsKey = false, IsRequired = false, DefaultValue = false)]
    public bool AllowIPAddresses {
      get { return (bool)this["allowIPAddresses"]; }
      set { this["allowIPAddresses"] = value; }
    }

    /// <summary>Gets or sets a value indicating whether the domain part must contain a top level domain.</summary>
    [ConfigurationProperty("requireTopLevelDomain", IsKey = false, IsRequired = false, DefaultValue = false)]
    public bool RequireTopLevelDomain {
      get { return (bool)this["requireTopLevelDomain"]; }
      set { this["requireTopLevelDomain"] = value; }
    }

    /// <summary>Gets the collection of domains that are white listed.</summary>
    [ConfigurationProperty("includeDomains", IsRequired = false, IsDefaultCollection = false)]
    public EmailDomainCollection IncludeDomains {
      get {
        return this["includeDomains"] as EmailDomainCollection;
      }
    }

    /// <summary>Gets the collection of domains that are black listed.</summary>
    [ConfigurationProperty("excludeDomains", IsRequired = false, IsDefaultCollection = false)]
    public EmailDomainCollection ExcludeDomains {
      get {
        return this["excludeDomains"] as EmailDomainCollection;
      }
    }

    #endregion

    #region Internal methods

    /// <summary>Reads XML from the configuration file.</summary>
    /// <param name="reader">The <see cref="XmlReader"/> that reads from the configuration file.</param>
    /// <param name="serializeCollectionKey"><see langword="true"/> to serialize only the collection key properties; otherwise,
    /// <see langword="false"/>.</param>
    internal void ReadFromConfig(XmlReader reader, bool serializeCollectionKey) {
      this.DeserializeElement(reader, serializeCollectionKey);
    }

    #endregion
  }
}
