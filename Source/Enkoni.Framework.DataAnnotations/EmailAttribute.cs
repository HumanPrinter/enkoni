﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

using Enkoni.Framework.DataAnnotations.Configuration;
using Enkoni.Framework.Validation.RegularExpressions;

namespace Enkoni.Framework.DataAnnotations {
  /// <summary>Attribute to specify e-mail validation on a property, method or field.</summary>
  /// <remarks>This validator can be configured through code or through the configuration file. By default all domains and /or IP addresses are considered valid
  /// as long as their syntax is correct (note that IP addresses will only be considered valid if <see cref="AllowIPAddresses"/> is set to <see langword="true"/>).
  /// <br/>
  /// To specifically include or exclude domains or IP addresses, two approaches can be used.<br/>
  /// <h3>Code</h3>
  /// By setting the <see cref="IncludeDomains"/> and/or <see cref="ExcludeDomains"/> properties, the white and black list of valid domains or IP addresses can be
  /// manipulated. To specify multiple domains, separate the domains with a semi colon (';'). When setting both the <see cref="IncludeDomains"/>  and the
  /// <see cref="ExcludeDomains"/> properties, the values in <see cref="ExcludeDomains"/> takes precedence over the values in <see cref="IncludeDomains"/>.<br/>
  /// <br/>
  /// <h3>Configuration</h3>
  /// <para>All properties of the <see cref="EmailAttribute"/> except for the <see cref="Category"/> property can be set through configuration.
  /// First of all, the configuration section must be specified:</para>
  /// <code>
  /// <![CDATA[
  /// <configuration>
  ///   <configSections>
  ///     <section name="Enkoni.DataAnnotations" type="Enkoni.Framework.DataAnnotations.Configuration.ValidationSection, Enkoni.Framework.DataAnnotations"/>
  ///   </configSections>
  /// </configuration>
  /// ]]>
  /// </code>
  /// <para>Then inside the section, the validator can be configured:</para>
  /// <code>
  /// <![CDATA[
  /// <Enkoni.DataAnnotations>
  ///   <EmailValidator allowComments="false" allowIPAddresses="false" requireTopLevelDomain="false">
  ///     <includeDomains>
  ///       <domain pattern="microsoft.com" />
  ///       <domain pattern="live.nl" />
  ///       <!-- Wildcards can be used to specify multiple domains. The wildcards * and ? are supported -->
  ///       <domain pattern="*.be" />
  ///     </includeDomains>
  ///     <excludeDomains>
  ///       <domain pattern="yahoo.com" />
  ///       <domain pattern="gmail.com" />
  ///       <!-- Wildcards can be used to specify multiple domains. The wildcards * and ? are supported -->
  ///       <domain pattern="yahoo.be" />
  ///     </excludeDomains>
  ///   </EmailValidator>
  /// </Enkoni.DataAnnotations>
  /// ]]>
  /// </code>
  /// <para>It is also possible to specify multiple configurations for different instances of validators by specifying the name attribute. At most one
  /// nameless validator can be specified in the configuration. The nameless configuration will be used by validators that do not have a name specified
  /// or whose name is not explicitly configured.</para>
  /// <code>
  /// <![CDATA[
  /// <Enkoni.DataAnnotations>
  ///   <!-- Since this validator does not have a name specified, it will be used as the default configuration -->
  ///   <EmailValidator allowComments="false" allowIPAdresses="false" />
  ///   <!-- This configuration will only be applied to validators with the name "MyValidator" -->
  ///   <EmailValidator name="MyValidator" allowComments="false" allowIPAddresses="true">
  ///     <includeDomains>
  ///       <!--The add-tag can be used to include area codes -->
  ///       <domain pattern="192.168.*" />
  ///     </includeDomains>
  ///   </EmailValidator>
  /// </Enkoni.DataAnnotations>
  /// ]]>
  /// </code>
  /// </remarks>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true,
    Inherited = false)]
  public sealed class EmailAttribute : ValidationAttribute {
    #region Constants

    /// <summary>Defines the default name for the validator.</summary>
    internal const string DefaultName = "00661f4a-faa2-452f-8fd9-af6c776bfc49";

    #endregion

    #region Instance variables

    /// <summary>Holds the name of the configuration section that must be used.</summary>
    private static string configurationSectionName;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="EmailAttribute"/> class.</summary>
    public EmailAttribute()
      : this(DefaultName) {
    }

    /// <summary>Initializes a new instance of the <see cref="EmailAttribute"/> class.</summary>
    /// <param name="name">The name of the validator.</param>
    public EmailAttribute(string name)
      : base() {
      this.Name = name;
      this.Category = EmailCategory.Basic;

      this.LoadConfiguration();
    }

    /// <summary>Initializes a new instance of the <see cref="EmailAttribute"/> class.</summary>
    /// <param name="errorMessageAccessor">The function that enables access to validation resources.</param>
    /// <exception cref="ArgumentNullException"><paramref name="errorMessageAccessor"/> is <see langword="null"/>.</exception>
    public EmailAttribute(Func<string> errorMessageAccessor)
      : this(DefaultName, errorMessageAccessor) {
    }

    /// <summary>Initializes a new instance of the <see cref="EmailAttribute"/> class.</summary>
    /// <param name="name">The name of the validator.</param>
    /// <param name="errorMessageAccessor">The function that enables access to validation resources.</param>
    /// <exception cref="ArgumentNullException"><paramref name="errorMessageAccessor"/> is <see langword="null"/>.</exception>
    public EmailAttribute(string name, Func<string> errorMessageAccessor)
      : base(errorMessageAccessor) {
      this.Name = name;
      this.Category = EmailCategory.Basic;

      this.LoadConfiguration();
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets the name of the configuration section that is used to preconfigure the validator. The default value is set to <see cref="ValidationSection.DefaultSectionName"/>.
    /// </summary>
    public static string ConfigurationSectionName {
      get { return configurationSectionName ?? ValidationSection.DefaultSectionName; }
      set { configurationSectionName = value; }
    }

    /// <summary>Gets or sets the categories of phone numbers that must be considered valid.</summary>
    public EmailCategory Category { get; set; }

    /// <summary>Gets the name of the validator.</summary>
    public string Name { get; private set; }

    /// <summary>Gets or sets a value indicating whether comments are allowed in the e-mail address. Defaults to <see langword="false"/>.</summary>
    public bool AllowComments { get; set; }

    /// <summary>Gets or sets a value indicating whether IP addresses are allowed in the e-mail address. Defaults to <see langword="false"/>.</summary>
    public bool AllowIPAddresses { get; set; }

    /// <summary>Gets or sets a value indicating whether the domain part of the e-mail address must contain a top level domain. Defaults to <see langword="false"/>.</summary>
    public bool RequireTopLevelDomain { get; set; }

    /// <summary>Gets or sets the semicolon separated domains that are white listed.</summary>
    public string IncludeDomains { get; set; }

    /// <summary>Gets or sets the semicolon separated domains that are black listed.</summary>
    public string ExcludeDomains { get; set; }

    /// <summary>Gets the values that were set through the configuration.</summary>
    private static Dictionary<string, ConfiguredValuesContainer> ConfiguredValues {
      get { return ConfiguredValuesSingletonContainer.ConfiguredValues; }
    }

    #endregion

    #region ValidationAttribute overrides

    /// <summary>Determines whether the specified value of the object is valid.</summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns><see langword="true"/> if the specified value is valid; otherwise, <see langword="false"/>.</returns>
    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "EmailCategory", Justification = "The name 'EmailCategory' is the name of the enumeration and the spelling can therefore be ignored")]
    public override bool IsValid(object value) {
      string valueToValidate = value as string;
      if(string.IsNullOrEmpty(valueToValidate)) {
        /* This may seem strange, but in order to reject empty values the RequiredAttribute should be used */
        return true;
      }

      bool isValid = false;

      Regex localPartRegex;
      switch(this.Category) {
        case EmailCategory.Basic:
          localPartRegex = new EmailValidatorLocalPartBasicRegex();
          break;
        case EmailCategory.Extended:
          localPartRegex = new EmailValidatorLocalPartExtendedRegex();
          break;
        case EmailCategory.Complete:
          localPartRegex = new EmailValidatorLocalPartCompleteRegex();
          break;
        default:
          throw new NotSupportedException("Unrecognized EmailCategory encountered");
      }

      isValid = ValidateMailAddress(valueToValidate, localPartRegex, this.AllowComments, this.AllowIPAddresses, this.RequireTopLevelDomain, this.IncludeDomains, this.ExcludeDomains);
      return isValid;
    }

    #endregion

    #region Private helper methods

    /// <summary>Validates whether the input is a valid e-mail address.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <param name="localPartRegex">The regular expression that must be used to validate the local part of the e-mail address.</param>
    /// <param name="allowComments">Indicates whether comments are allowed in the e-mail address.</param>
    /// <param name="allowIPAddresses">Indicates whether IP addresses are allowed in the e-mail address.</param>
    /// <param name="requireTopLevelDomain">Indicates whether a top level domain is required.</param>
    /// <param name="includeDomains">A semicolon separated list of domains that are white listed.</param>
    /// <param name="excludeDomains">A semicolon separated list of domains that are black listed.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateMailAddress(string input, Regex localPartRegex, bool allowComments, bool allowIPAddresses, bool requireTopLevelDomain, string includeDomains, string excludeDomains) {
      string[] parts = input.Split('@');
      if(parts.Length == 1) {
        return false;
      }

      /* Extract the local and domain part from the input */
      string localPart = string.Join("@", parts.Take(parts.Length - 1));
      string domainPart = parts.Last();

      /* Validate the domain part */
      if(!ValidateDomainPart(domainPart, allowComments, allowIPAddresses, requireTopLevelDomain, includeDomains, excludeDomains)) {
        return false;
      }

      /* Validate the local part */
      Match localMatch = localPartRegex.Match(localPart);
      if(!localMatch.Success) {
        return false;
      }

      /* Validate any comments */
      if(!allowComments && localMatch.Groups["comment1"] != null && localMatch.Groups["comment1"].Success && !string.IsNullOrEmpty(localMatch.Groups["comment1"].Value)) {
        return false;
      }

      if(!allowComments && localMatch.Groups["comment2"] != null && localMatch.Groups["comment2"].Success && !string.IsNullOrEmpty(localMatch.Groups["comment2"].Value)) {
        return false;
      }

      return true;
    }

    /// <summary>Validates the domain part of an e-mail address.</summary>
    /// <param name="domainPart">The domain part of the e-mail address that must be validated.</param>
    /// <param name="allowComments">Indicates whether comments are allowed in the domain part.</param>
    /// <param name="allowIPAddresses">Indicates whether an IP address is allowed in the domain part.</param>
    /// <param name="requireTopLevelDomain">Indicates if a top level domain is required.</param>
    /// <param name="includeDomains">A semicolon separated list of domains that are white listed.</param>
    /// <param name="excludeDomains">A semicolon separated list of domains that are black listed.</param>
    /// <returns><see langword="true"/> if the domain part is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateDomainPart(string domainPart, bool allowComments, bool allowIPAddresses, bool requireTopLevelDomain, string includeDomains, string excludeDomains) {
      string fullDomain;
      bool isIPAddress;

      /* Validate the domain part using the general validation rules */
      if(!ValidateDomainPart(domainPart, allowComments, allowIPAddresses, out fullDomain, out isIPAddress)) {
        return false;
      }

      if(string.IsNullOrEmpty(fullDomain)) {
        return false;
      }

      if(requireTopLevelDomain && (isIPAddress || fullDomain.IndexOf('.') < 0)) {
        return false;
      }

      /* Make sure the host name does not exist on the black list (if any) */
      if(!ValidateAgainstBlackList(fullDomain, excludeDomains)) {
        return false;
      }

      /* Make sure the host name exists on the white list (if any) */
      return ValidateAgainstWhiteList(fullDomain, includeDomains);
    }

    /// <summary>Validates the domain by checking if it matches a black listed domain.</summary>
    /// <param name="domain">The domain that must be validated.</param>
    /// <param name="excludeDomains">The black listed domains.</param>
    /// <returns><see langword="true"/> if the domain is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateAgainstBlackList(string domain, string excludeDomains) {
      if(!string.IsNullOrEmpty(excludeDomains)) {
        string[] splittedDomains = excludeDomains.Split(';');
        foreach(string blackListDomain in splittedDomains.Where(pattern => !pattern.Contains("*") && !pattern.Contains("?"))) {
          if(domain.Equals(blackListDomain, StringComparison.OrdinalIgnoreCase)) {
            return false;
          }
        }

        IEnumerable<Regex> domainRegexes = splittedDomains.Where(pattern => pattern.Contains("*") || pattern.Contains("?"))
          .Select(domainPattern => new Regex("^" + domainPattern.Replace(".", @"\.").Replace("*", ".*").Replace("?", ".?") + "$"));
        foreach(Regex domainRegex in domainRegexes) {
          if(domainRegex.IsMatch(domain)) {
            return false;
          }
        }
      }

      return true;
    }

    /// <summary>Validates the domain by checking if it matches a white listed domain.</summary>
    /// <param name="domain">The domain that must be validated.</param>
    /// <param name="includeDomains">The white listed domains.</param>
    /// <returns><see langword="true"/> if the domain is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateAgainstWhiteList(string domain, string includeDomains) {
      if(!string.IsNullOrEmpty(includeDomains)) {
        string[] splittedDomains = includeDomains.Split(';');
        foreach(string whiteListDomain in splittedDomains.Where(pattern => !pattern.Contains("*") && !pattern.Contains("?"))) {
          if(domain.Equals(whiteListDomain, StringComparison.OrdinalIgnoreCase)) {
            return true;
          }
        }

        IEnumerable<Regex> domainRegexes = splittedDomains.Where(pattern => pattern.Contains("*") || pattern.Contains("?"))
          .Select(domainPattern => new Regex("^" + domainPattern.Replace("*", ".*").Replace("?", ".?") + "$"));
        foreach(Regex domainRegex in domainRegexes) {
          if(domainRegex.IsMatch(domain)) {
            return true;
          }
        }

        return false;
      }

      return true;
    }

    /// <summary>Validates the domain part of an e-mail address.</summary>
    /// <param name="domainPart">The domain part of the e-mail address that must be validated.</param>
    /// <param name="allowComments">Indicates whether comments are allowed in the domain part.</param>
    /// <param name="allowIPAddresses">Indicates whether an IP address is allowed in the domain part.</param>
    /// <param name="fullDomain">The actual domain that makes up the domain part.</param>
    /// <param name="isIPAddress">Holds a value that indicates if the domain is an IP address or a regular domain.</param>
    /// <returns><see langword="true"/> if the domain part is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateDomainPart(string domainPart, bool allowComments, bool allowIPAddresses, out string fullDomain, out bool isIPAddress) {
      fullDomain = null;
      isIPAddress = false;

      /* Perform the first validation by using the regular expression */
      Match domainMatch = new EmailValidatorDomainPartRegex().Match(domainPart);
      if(!domainMatch.Success) {
        return false;
      }

      /* Validate any comments in the domain part */
      if(!allowComments && domainMatch.Groups["comment1"] != null && domainMatch.Groups["comment1"].Success && !string.IsNullOrEmpty(domainMatch.Groups["comment1"].Value)) {
        return false;
      }

      if(!allowComments && domainMatch.Groups["comment2"] != null && domainMatch.Groups["comment2"].Success && !string.IsNullOrEmpty(domainMatch.Groups["comment2"].Value)) {
        return false;
      }

      /* If the domain part consists out of an IP address, validate the IP address */
      Group ipAddressGroup = domainMatch.Groups["ipAddress"];
      if(!allowIPAddresses && ipAddressGroup != null && ipAddressGroup.Success) {
        isIPAddress = true;
        return false;
      }

      if(ipAddressGroup != null && ipAddressGroup.Success) {
        isIPAddress = true;
        string ipAddress = ipAddressGroup.Value;
        if(string.IsNullOrEmpty(ipAddress)) {
          return false;
        }

        ipAddress = ipAddress.Trim('[', ']');
        fullDomain = ipAddress;
        IPAddress parsedAddress;
        if(ipAddress.StartsWith("IPv6:", StringComparison.Ordinal)) {
          ipAddress = ipAddress.Substring(5);
          fullDomain = ipAddress;
          if(!IPAddress.TryParse(ipAddress, out parsedAddress)) {
            return false;
          }

          return parsedAddress.AddressFamily == AddressFamily.InterNetworkV6;
        }
        else {
          if(!IPAddress.TryParse(ipAddress, out parsedAddress)) {
            return false;
          }

          return parsedAddress.AddressFamily == AddressFamily.InterNetwork;
        }
      }

      /* Make sure a domain/host name is specified */
      Group domainGroup = domainMatch.Groups["domain"];
      if(domainGroup == null || !domainGroup.Success) {
        return false;
      }

      /* Validate the length of the domain part */
      fullDomain = domainGroup.Value;
      if(fullDomain.Length > 255) {
        return false;
      }

      /* Make sure the domain is not actually an IP address */
      IPAddress dummy;
      return !IPAddress.TryParse(fullDomain, out dummy);
    }

    /// <summary>Initializes the properties with the values from the configuration.</summary>
    private void LoadConfiguration() {
      if(ConfiguredValues != null && ConfiguredValues.Count > 0) {
        ConfiguredValuesContainer container = null;
        if(ConfiguredValues.ContainsKey(this.Name)) {
          container = ConfiguredValues[this.Name];
        }
        else if(ConfiguredValues.ContainsKey(DefaultName)) {
          container = ConfiguredValues[DefaultName];
        }

        if(container != null) {
          this.AllowComments = container.AllowComments;
          this.AllowIPAddresses = container.AllowIPAddresses;
          this.RequireTopLevelDomain = container.RequireTopLevelDomain;
          this.IncludeDomains = container.IncludeDomains;
          this.ExcludeDomains = container.ExcludeDomains;
        }
      }
    }

    #endregion

    #region Private classes

    /// <summary>Holds the values that were set through the configuration.</summary>
    private class ConfiguredValuesContainer {
      /// <summary>Gets or sets a value indicating whether the setting for 'AllowComments' was set to <see langword="true"/>.</summary>
      public bool AllowComments { get; set; }

      /// <summary>Gets or sets a value indicating whether the setting for 'AllowIPAddresses' was set to <see langword="true"/>.</summary>
      public bool AllowIPAddresses { get; set; }

      /// <summary>Gets or sets a value indicating whether the setting for 'RequireTopLevelDomain' was set to <see langword="true"/>.</summary>
      public bool RequireTopLevelDomain { get; set; }

      /// <summary>Gets or sets the configured collection of white listed domains.</summary>
      public string IncludeDomains { get; set; }

      /// <summary>Gets or sets the configured collection of black listed domains.</summary>
      public string ExcludeDomains { get; set; }
    }

    /// <summary>Holds the singleton instance of the <see cref="ConfiguredValuesContainer"/> class.</summary>
    /// <remarks>This implementation is based on the article on Singletons by Jon Skeet (http://csharpindepth.com/Articles/General/Singleton.aspx).</remarks>
    private class ConfiguredValuesSingletonContainer {
      /// <summary>The actual singleton instance.</summary>
      internal static readonly Dictionary<string, ConfiguredValuesContainer> ConfiguredValues = ReadConfiguration(ConfigurationSectionName);

      /// <summary>Initializes static members of the <see cref="ConfiguredValuesSingletonContainer"/> class.</summary>
      /// <remarks>Even though this constructor does nothing by itself (it has an empty body), declaring this static constructor prevents the C#
      /// compiler from marking this type as <c>beforefieldinit</c> which is required in order to get the laziness behavior that is intended.</remarks>
      static ConfiguredValuesSingletonContainer() {
      }

      /// <summary>Prevents a default instance of the <see cref="ConfiguredValuesSingletonContainer"/> class from being created.</summary>
      private ConfiguredValuesSingletonContainer() {
      }

      /// <summary>Reads the configuration and sets the configured values.</summary>
      /// <param name="sectionName">The name of the config section that must be read. Use <see langword="null"/> or <see cref="string.Empty"/> to use the default section name.</param>
      /// <returns>The values that were read from the configuration or <see langword="null"/> if there was no configuration.</returns>
      private static Dictionary<string, ConfiguredValuesContainer> ReadConfiguration(string sectionName) {
        if(string.IsNullOrEmpty(sectionName)) {
          sectionName = ValidationSection.DefaultSectionName;
        }

        ValidationSection validatorsSection = ConfigurationManager.GetSection(sectionName) as ValidationSection;
        if(validatorsSection == null || validatorsSection.EmailValidators.Count == 0) {
          return null;
        }

        Dictionary<string, ConfiguredValuesContainer> configuredValues = new Dictionary<string, ConfiguredValuesContainer>();
        foreach(EmailValidationConfigElement validatorConfig in validatorsSection.EmailValidators.Values) {
          ConfiguredValuesContainer valuesContainer = new ConfiguredValuesContainer {
            AllowComments = validatorConfig.AllowComments,
            AllowIPAddresses = validatorConfig.AllowIPAddresses,
            RequireTopLevelDomain = validatorConfig.RequireTopLevelDomain
          };

          List<string> configuredIncludeDomains = new List<string>(validatorConfig.IncludeDomains.Count);
          foreach(EmailDomainConfigElement domain in validatorConfig.IncludeDomains) {
            configuredIncludeDomains.Add(domain.Pattern);
          }

          List<string> configuredExcludeDomains = new List<string>(validatorConfig.ExcludeDomains.Count);
          foreach(EmailDomainConfigElement domain in validatorConfig.ExcludeDomains) {
            configuredExcludeDomains.Add(domain.Pattern);
          }

          valuesContainer.IncludeDomains = string.Join(";", configuredIncludeDomains.ToArray());
          valuesContainer.ExcludeDomains = string.Join(";", configuredExcludeDomains.ToArray());

          configuredValues.Add(validatorConfig.Name, valuesContainer);
        }

        return configuredValues;
      }
    }

    #endregion
  }
}
