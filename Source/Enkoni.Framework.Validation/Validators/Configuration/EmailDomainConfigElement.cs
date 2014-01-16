//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailDomainConfigElement.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains a custom configurationelement used to include or exclude domains in or from the list of supported domains.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Configuration;

namespace Enkoni.Framework.Validation.Validators.Configuration {
  /// <summary>Defines the configuration element that holds a single domain that should either be included or excluded from the list of domains
  /// that must be validated by the <see cref="EmailValidator"/>.</summary>
  public class EmailDomainConfigElement : ConfigurationElement {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="EmailDomainConfigElement"/> class.</summary>
    public EmailDomainConfigElement() {
    }

    /// <summary>Initializes a new instance of the <see cref="EmailDomainConfigElement"/> class using the specified area code.</summary>
    /// <param name="pattern">The pattern that must be added to the list.</param>
    public EmailDomainConfigElement(string pattern) {
      this.Pattern = pattern;
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the pattern that is set through this element.</summary>
    [ConfigurationProperty("pattern", IsKey = true, IsRequired = true, DefaultValue = "")]
    //[RegexStringValidator(@"^\d{1,4}$")]
    public string Pattern {
      get { return (string)this["pattern"]; }
      set { this["pattern"] = value; }
    }
    #endregion
  }
}
