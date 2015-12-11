using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;

using Enkoni.Framework.DataAnnotations.Configuration;
using Enkoni.Framework.DataAnnotations.Properties;
using Enkoni.Framework.Validation.RegularExpressions;

namespace Enkoni.Framework.DataAnnotations {
  /// <summary>Performs validation on <see cref="String"/> instances by checking if they contain valid Dutch phone numbers.</summary>
  /// <remarks>This validator can be configured through code or through the configuration file. When the validator is configured to include the category 
  /// <see cref="PhoneNumberCategories.Regular"/> (which is the default setting), all area codes that are valid according to the Dutch 'numbering plan
  /// for phone and ISDN services' that was published at November 12th 2013 (<a href="http://wetten.overheid.nl/BWBR0010198/geldigheidsdatum_13-11-2013">link</a>).
  /// <br/>
  /// To override the validated area codes, two approaches can be used.<br/>
  /// <h3>Code</h3>
  /// By setting the <see cref="IncludeAreaCodes"/> and/or <see cref="ExcludeAreaCodes"/> properties, the collection of valid area codes can be 
  /// manipulated. To specify multiple area codes, separate the area codes with a semi colon (';'). When setting the <see cref="IncludeAreaCodes"/> 
  /// property, the default list of valid area codes will be ignored and only the specified area codes will be considered valid. Eg. when setting 
  /// <see cref="IncludeAreaCodes"/> to <c>"010;020;030"</c>, only phone numbers with those area codes will be considered valid.<br/>
  /// <br/>
  /// <h3>Configuration</h3>
  /// <para>All properties of the <see cref="DutchPhoneNumberAttribute"/> except for the <see cref="Categories"/> property can be set through configuration.
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
  ///   <DutchPhoneNumber allowCountryCallingCode="false" allowCarrierPreselect="true">
  ///     <areaCodes>
  ///       <clear /> <!-- The clear-tag is optional and will reset the default collection of area codes -->
  ///       <!--The add-tag can be used to include area codes -->
  ///       <add areaCode="010" />
  ///       <add areaCode="020" />
  ///       <add areaCode="030" />
  ///       <!-- The remove-tag can be used to exclude area codes -->
  ///       <remove areaCode="023" />
  ///       <remove areaCode="058" />
  ///     </areaCodes>
  ///   </DutchPhoneNumber>
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
  ///   <DutchPhoneNumber allowCountryCallingCode="false" allowCarrierPreselect="true">
  ///     <areaCodes>
  ///       <clear /> <!-- The clear-tag is optional and will reset the default collection of area codes -->
  ///       <!--The add-tag can be used to include area codes -->
  ///       <add areaCode="010" />
  ///       <add areaCode="020" />
  ///       <add areaCode="030" />
  ///       <!-- The remove-tag can be used to exclude area codes -->
  ///       <remove areaCode="023" />
  ///       <remove areaCode="058" />
  ///     </areaCodes>
  ///   </DutchPhoneNumber>
  ///   <!-- This configuration will only be applied to validators with the name "MyValidator" -->
  ///   <DutchPhoneNumber name="MyValidator" allowCountryCallingCode="false" allowCarrierPreselect="true">
  ///     <areaCodes>
  ///       <clear /> <!-- The clear-tag is optional and will reset the default collection of area codes -->
  ///       <!--The add-tag can be used to include area codes -->
  ///       <add areaCode="010" />
  ///       <add areaCode="020" />
  ///       <add areaCode="030" />
  ///       <!-- The remove-tag can be used to exclude area codes -->
  ///       <remove areaCode="023" />
  ///       <remove areaCode="058" />
  ///     </areaCodes>
  ///   </DutchPhoneNumber>
  /// </Enkoni.DataAnnotations>
  /// ]]>
  /// </code>
  /// </remarks>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true,
    Inherited = false)]
  public sealed class DutchPhoneNumberAttribute : ValidationAttribute, IClientValidatable {
    #region Constants
    /// <summary>Defines the default name for the validator.</summary>
    internal const string DefaultName = "00661f4a-faa2-452f-8fd9-af6c776bfc49";
    #endregion

    #region Variables
    /// <summary>Holds the name of the configuration section that must be used.</summary>
    private static string configurationSectionName;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    public DutchPhoneNumberAttribute()
      : this(DefaultName) {
    }

    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    /// <param name="name">The name for the validator.</param>
    public DutchPhoneNumberAttribute(string name)
      : base() {
      this.Name = name;
      this.Categories = PhoneNumberCategories.Default;
      this.AllowCountryCallingCode = true;

      this.LoadConfiguration();
    }

    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    /// <param name="errorMessageAccessor">The function that enables access to validation resources.</param>
    /// <exception cref="ArgumentNullException"><paramref name="errorMessageAccessor"/> is <see langword="null"/>.</exception>
    public DutchPhoneNumberAttribute(Func<string> errorMessageAccessor)
      : this(DefaultName, errorMessageAccessor) {
    }

    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    /// <param name="name">The name of the validator.</param>
    /// <param name="errorMessageAccessor">The function that enables access to validation resources.</param>
    /// <exception cref="ArgumentNullException"><paramref name="errorMessageAccessor"/> is <see langword="null"/>.</exception>
    public DutchPhoneNumberAttribute(string name, Func<string> errorMessageAccessor)
      : base(errorMessageAccessor) {
      this.Name = name;
      this.Categories = PhoneNumberCategories.Default;
      this.AllowCountryCallingCode = true;

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

    /// <summary>Gets the name of the validator.</summary>
    public string Name { get; private set; }

    /// <summary>Gets or sets the categories of phone numbers that must be considered valid.</summary>
    public PhoneNumberCategories Categories { get; set; }

    /// <summary>Gets or sets a value indicating whether the international dialing prefix and country calling code (eg. 0031 or +31) code is allowed 
    /// in the phone number. Defaults to <see langword="true"/>.</summary>
    public bool AllowCountryCallingCode { get; set; }

    /// <summary>Gets or sets a value indicating whether a carrier preselect code (eg. 1642) code is allowed in the phone number. Defaults to
    /// <see langword="false"/>.</summary>
    public bool AllowCarrierPreselect { get; set; }

    /// <summary>Gets or sets the semicolon separated area codes that are considered valid. This overrides the default area codes for this validator.
    /// </summary>
    public string IncludeAreaCodes { get; set; }

    /// <summary>Gets or sets the semicolon separated area codes that are considered invalid.</summary>
    public string ExcludeAreaCodes { get; set; }

    /// <summary>Gets the values that were set through the configuration.</summary>
    private static Dictionary<string, ConfiguredValuesContainer> ConfiguredValues {
      get { return ConfiguredValuesSingletonContainer.ConfiguredValues; }
    }
    #endregion

    #region ValidationAttribute overrides
    /// <summary>Determines whether the specified value of the object is valid.</summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns><see langword="true"/> if the specified value is valid; otherwise, <see langword="false"/>.</returns>
    public override bool IsValid(object value) {
      string valueToValidate = value as string;
      if(string.IsNullOrEmpty(valueToValidate) || this.Categories == PhoneNumberCategories.None) {
        /* This may seem strange, but in order to reject empty values the RequiredAttribute should be used */
        return true;
      }

      string strippedValueToValidate = valueToValidate.Replace(" ", string.Empty);

      bool isValid = false;

      if((this.Categories & PhoneNumberCategories.Regular) == PhoneNumberCategories.Regular) {
        isValid = ValidateRegularNumber(strippedValueToValidate, this.AllowCountryCallingCode, this.AllowCarrierPreselect, this.IncludeAreaCodes, this.ExcludeAreaCodes);
      }

      if(!isValid && (this.Categories & PhoneNumberCategories.Mobile) == PhoneNumberCategories.Mobile) {
        isValid |= ValidateMobileNumber(strippedValueToValidate, this.AllowCountryCallingCode, this.AllowCarrierPreselect);
      }

      if(!isValid && (this.Categories & PhoneNumberCategories.Emergency) == PhoneNumberCategories.Emergency) {
        isValid |= ValidateEmergencyNumber(strippedValueToValidate);
      }

      if(!isValid && (this.Categories & PhoneNumberCategories.Service) == PhoneNumberCategories.Service) {
        isValid |= ValidateServiceNumber(strippedValueToValidate);
      }

      if(!isValid && (this.Categories & PhoneNumberCategories.Other) == PhoneNumberCategories.Other) {
        isValid |= ValidateOtherNumber(strippedValueToValidate, this.AllowCarrierPreselect);
      }

      return isValid;
    }
    #endregion

    #region IClientValidatable implementation
    /// <summary>Returns client validation rules for IBAN validation.</summary>
    /// <param name="metadata">The model metadata.</param>
    /// <param name="context">The controller context.</param>
    /// <returns>The client validation rules that apply to this validator.</returns>
    public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context) {
      string name = metadata == null ? string.Empty : metadata.GetDisplayName();
      
      List<string> patterns = new List<string>();
      if((this.Categories & PhoneNumberCategories.Regular) == PhoneNumberCategories.Regular) {
        string pattern = ConstructRegularNumberRegex(this.AllowCountryCallingCode, this.AllowCarrierPreselect, this.IncludeAreaCodes, this.ExcludeAreaCodes).ToString();
        patterns.Add(pattern.TrimStart('^').TrimEnd('$'));
      }

      if((this.Categories & PhoneNumberCategories.Mobile) == PhoneNumberCategories.Mobile) {
        string pattern = ConstructMobileNumberRegex(this.AllowCountryCallingCode, this.AllowCarrierPreselect).ToString();
        patterns.Add(pattern.TrimStart('^').TrimEnd('$'));
      }

      if((this.Categories & PhoneNumberCategories.Emergency) == PhoneNumberCategories.Emergency) {
        string pattern = new DutchPhoneValidatorEmergencyRegex().ToString();
        patterns.Add(pattern.TrimStart('^').TrimEnd('$'));
      }

      if((this.Categories & PhoneNumberCategories.Service) == PhoneNumberCategories.Service) {
        string pattern = new DutchPhoneValidatorServiceRegex().ToString();
        patterns.Add(pattern.TrimStart('^').TrimEnd('$'));
      }

      if((this.Categories & PhoneNumberCategories.Other) == PhoneNumberCategories.Other) {
        string pattern = ConstructOtherNumber(this.AllowCarrierPreselect).ToString();
        patterns.Add(pattern.TrimStart('^').TrimEnd('$'));
      }

      if(patterns.Count == 0) {
        yield return new ModelClientValidationRegexRule(this.FormatErrorMessage(name), "^.*$");
      }
      else if(patterns.Count == 1) {
        yield return new ModelClientValidationRegexRule(this.FormatErrorMessage(name), "^" + patterns[0] + "$");
      }
      else {
        string completePattern = "^((" + string.Join(")|(", patterns) + "))$";
        yield return new ModelClientValidationRegexRule(this.FormatErrorMessage(name), completePattern);
      }
    }
    #endregion

    #region Private helper methods
    /// <summary>Validates whether the input is a valid Dutch service phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateServiceNumber(string input) {
      return new DutchPhoneValidatorServiceRegex().IsMatch(input);
    }

    /// <summary>Validates whether the input is a valid Dutch emergency phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateEmergencyNumber(string input) {
      return new DutchPhoneValidatorEmergencyRegex().IsMatch(input);
    }

    /// <summary>Validates whether the input is a valid Dutch mobile phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <param name="allowCountryCallingCode">Indicates whether the country calling code is allowed in the phone number.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateMobileNumber(string input, bool allowCountryCallingCode, bool allowCarrierPreselect) {
      Regex expression = ConstructMobileNumberRegex(allowCountryCallingCode, allowCarrierPreselect);
      return expression.IsMatch(input);
    }

    /// <summary>Constructs a regex that validates a valid Dutch mobile phone number.</summary>
    /// <param name="allowCountryCallingCode">Indicates whether the country calling code is allowed in the phone number.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static Regex ConstructMobileNumberRegex(bool allowCountryCallingCode, bool allowCarrierPreselect) {
      if(allowCarrierPreselect) {
        return allowCountryCallingCode
          ? (Regex)new DutchPhoneValidatorMobileRegexWithCarrierPreselect()
          : (Regex)new DutchPhoneValidatorMobileRegexNoCountryAccessCodeWithCarrierPreselect();
      }
      else {
        return allowCountryCallingCode
          ? (Regex)new DutchPhoneValidatorMobileRegex()
          : (Regex)new DutchPhoneValidatorMobileRegexNoCountryAccessCode();
      }
    }

    /// <summary>Validates whether the input is a valid Dutch phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateOtherNumber(string input, bool allowCarrierPreselect) {
      Regex expression = ConstructOtherNumber(allowCarrierPreselect);
      return expression.IsMatch(input);
    }

    /// <summary>Constructs a regex that validates a valid Dutch phone number.</summary>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static Regex ConstructOtherNumber(bool allowCarrierPreselect) {
      return allowCarrierPreselect
        ? (Regex)new DutchPhoneValidatorOtherRegexWithCarrierPreselect()
        : (Regex)new DutchPhoneValidatorOtherRegex();
    }

    /// <summary>Validates whether the input is a valid Dutch regular phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <param name="allowCountryCallingCode">Indicates whether the country calling code is allowed in the phone number.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <param name="includeAreaCodes">Overrides the area codes that are part of the regular expression.</param>
    /// <param name="excludeAreaCodes">The area codes that must be excluded from the standard list of area codes.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateRegularNumber(string input, bool allowCountryCallingCode, bool allowCarrierPreselect, string includeAreaCodes, string excludeAreaCodes) {
      Regex expression = ConstructRegularNumberRegex(allowCountryCallingCode, allowCarrierPreselect, includeAreaCodes, excludeAreaCodes);
      return expression.IsMatch(input);
    }

    /// <summary>Constructs a regex that validates a valid Dutch regular phone number.</summary>
    /// <param name="allowCountryCallingCode">Indicates whether the country calling code is allowed in the phone number.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <param name="includeAreaCodes">Overrides the area codes that are part of the regular expression.</param>
    /// <param name="excludeAreaCodes">The area codes that must be excluded from the standard list of area codes.</param>
    /// <returns>The constructed regular expression.</returns>
    private static Regex ConstructRegularNumberRegex(bool allowCountryCallingCode, bool allowCarrierPreselect, string includeAreaCodes, string excludeAreaCodes) {
      if(string.IsNullOrEmpty(includeAreaCodes) && string.IsNullOrEmpty(excludeAreaCodes)) {
        if(allowCarrierPreselect) {
          return allowCountryCallingCode
            ? (Regex)new DutchPhoneValidatorDefaultRegularRegexWithCarrierPreselect()
            : (Regex)new DutchPhoneValidatorDefaultRegularRegexNoCountryAccessCodeWithCarrierPreselect();
        }
        else {
          return allowCountryCallingCode
            ? (Regex)new DutchPhoneValidatorDefaultRegularRegex()
            : (Regex)new DutchPhoneValidatorDefaultRegularRegexNoCountryAccessCode();
        }
      }

      IEnumerable<string> areaCodes = Resources.AreaCodes_NL.Split(';');
      if(!string.IsNullOrEmpty(includeAreaCodes)) {
        areaCodes = includeAreaCodes.Split(';').Select(code => code.TrimStart('0'));
      }
      else if(!string.IsNullOrEmpty(excludeAreaCodes)) {
        IEnumerable<string> exclusions = excludeAreaCodes.Split(';').Select(code => code.TrimStart('0'));
        areaCodes = areaCodes.Except(exclusions);
      }

      IEnumerable<string> shortAreaCodes = areaCodes.Where(code => code.Length == 2);
      IEnumerable<string> longAreaCodes = areaCodes.Where(code => code.Length == 3);
      string shortAreaCodesRegexPart = string.Join("|", shortAreaCodes.ToArray());
      string longAreaCodesRegexPart = string.Join("|", longAreaCodes.ToArray());

      string pattern;
      if(allowCarrierPreselect) {
        if(allowCountryCallingCode) {
          pattern = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPatternWithCarrierPreselect_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);
        }
        else {
          pattern = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPatternNoCountryAccessCodeWithCarrierPreselect_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);
        }
      }
      else {
        if(allowCountryCallingCode) {
          pattern = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPattern_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);
        }
        else {
          pattern = string.Format(CultureInfo.InvariantCulture, Resources.RegularRegexPatternNoCountryAccessCode_NL, shortAreaCodesRegexPart, longAreaCodesRegexPart);
        }
      }

      return new Regex(pattern, RegexOptions.Singleline);
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
          this.AllowCountryCallingCode = container.AllowCountryCallingCode;
          this.AllowCarrierPreselect = container.AllowCarrierPreselect;
          if(container.AreaCodesOverridden) {
            this.IncludeAreaCodes = container.AreaCodes;
          }
        }
      }
    }
    #endregion

    #region Private classes
    /// <summary>Holds the values that were set through the configuration.</summary>
    private class ConfiguredValuesContainer {
      /// <summary>Gets or sets a value indicating whether the setting for 'AllowCountryCallingCode' was set to <see langword="true"/>.</summary>
      public bool AllowCountryCallingCode { get; set; }

      /// <summary>Gets or sets a value indicating whether the setting for 'AllowCarrierPreselect' was set to <see langword="true"/>.</summary>
      public bool AllowCarrierPreselect { get; set; }

      /// <summary>Gets or sets a value indicating whether the collection of area codes were overridden through configuration.</summary>
      public bool AreaCodesOverridden { get; set; }
      
      /// <summary>Gets or sets the configured collection of area codes.</summary>
      public string AreaCodes { get; set; }
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
      /// <param name="sectionName">The name of the config section that must be read. Use <see langword="null"/> or <see cref="String.Empty"/> to use the default section name.</param>
      /// <returns>The values that were read from the configuration or <see langword="null"/> if there was no configuration.</returns>
      private static Dictionary<string, ConfiguredValuesContainer> ReadConfiguration(string sectionName) {
        if(string.IsNullOrEmpty(sectionName)) {
          sectionName = ValidationSection.DefaultSectionName;
        }

        ValidationSection validatorsSection = ConfigurationManager.GetSection(sectionName) as ValidationSection;

        if(validatorsSection == null || validatorsSection.DutchPhoneNumberValidators.Count == 0) {
          return null;
        }

        Dictionary<string, ConfiguredValuesContainer> configuredValues = new Dictionary<string, ConfiguredValuesContainer>();
        foreach(DutchPhoneNumberValidationConfigElement validatorConfig in validatorsSection.DutchPhoneNumberValidators.Values) {
          List<string> configuredAreaCodes = new List<string>(validatorConfig.AreaCodes.Count);
          foreach(DutchPhoneNumberAreaCodeConfigElement areaCode in validatorConfig.AreaCodes) {
            configuredAreaCodes.Add(areaCode.AreaCode.TrimStart('0'));
          }

          string[] defaultAreaCodes = Resources.AreaCodes_NL.Split(';');
          IEnumerable<string> difference = defaultAreaCodes.Except(configuredAreaCodes);
          difference.Concat(configuredAreaCodes.Except(defaultAreaCodes));

          ConfiguredValuesContainer valuesContainer = new ConfiguredValuesContainer {
            AllowCountryCallingCode = validatorConfig.AllowCountryCallingCode,
            AllowCarrierPreselect = validatorConfig.AllowCarrierPreselect,
            AreaCodesOverridden = difference.Any(),
            AreaCodes = string.Join(";", configuredAreaCodes.ToArray())
          };
          configuredValues.Add(validatorConfig.Name, valuesContainer);
        }

        return configuredValues;
      }
    }
    #endregion
  }
}
