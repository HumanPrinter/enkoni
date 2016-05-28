using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Enkoni.Framework.Validation.Properties;
using Enkoni.Framework.Validation.RegularExpressions;
using Enkoni.Framework.Validation.Validators.Configuration;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Enkoni.Framework.Validation.Validators {
  /// <summary>Performs validation on <see cref="string"/> instances by checking if they contain valid Dutch phone numbers.</summary>
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
  /// <para>All properties of the <see cref="DutchPhoneNumberValidator"/> except for the <see cref="Categories"/> property can be set through configuration.
  /// First of all, the configuration section must be specified:</para>
  /// <code>
  /// <![CDATA[
  /// <configuration>
  ///   <configSections>
  ///     <section name="Enkoni.Validators" type="Enkoni.Framework.Validation.Validators.Configuration.ValidatorsSection, Enkoni.Framework.Validation"/>
  ///   </configSections>
  /// </configuration>
  /// ]]>
  /// </code>
  /// <para>Then inside the section, the validator can be configured:</para>
  /// <code>
  /// <![CDATA[
  /// <Enkoni.Validators>
  ///   <DutchPhoneNumberValidator allowCountryCallingCode="false" allowCarrierPreselect="true">
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
  ///   </DutchPhoneNumberValidator>
  /// </Enkoni.Validators>
  /// ]]>
  /// </code>
  /// <para>It is also possible to specify multiple configurations for different instances of validators by specifying the name attribute. At most one
  /// nameless validator can be specified in the configuration. The nameless configuration will be used by validators that do not have a name specified
  /// or whose name is not explicitly configured.</para>
  /// <code>
  /// <![CDATA[
  /// <Enkoni.Validators>
  ///   <!-- Since this validator does not have a name specified, it will be used as the default configuration -->
  ///   <DutchPhoneNumberValidator allowCountryCallingCode="false" allowCarrierPreselect="true">
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
  ///   </DutchPhoneNumberValidator>
  ///   <!-- This configuration will only be applied to validators with the name "MyValidator" -->
  ///   <DutchPhoneNumberValidator name="MyValidator" allowCountryCallingCode="false" allowCarrierPreselect="true">
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
  ///   </DutchPhoneNumberValidator>
  /// </Enkoni.Validators>
  /// ]]>
  /// </code>
  /// </remarks>
  public class DutchPhoneNumberValidator : ValueValidator<string> {
    #region Constants

    /// <summary>Defines the default name for the validator.</summary>
    internal const string DefaultName = "00661f4a-faa2-452f-8fd9-af6c776bfc49";

    #endregion

    #region Variables

    /// <summary>Holds the name of the configuration section that must be used.</summary>
    private static string configurationSectionName;

    #endregion

    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    /// <param name="messageTemplate">The template to use when logging validation results, or null we the default message template is to be used.</param>
    /// <param name="tag">The tag to set when logging validation results, or null.</param>
    /// <param name="negated">Indicates if the validation logic represented by the validator should be negated.</param>
    public DutchPhoneNumberValidator(string messageTemplate, string tag, bool negated)
      : this(DefaultName, messageTemplate, tag, negated) {
    }

    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    /// <param name="name">The name for the validator.</param>
    /// <param name="messageTemplate">The template to use when logging validation results, or null we the default message template is to be used.</param>
    /// <param name="tag">The tag to set when logging validation results, or null.</param>
    /// <param name="negated">Indicates if the validation logic represented by the validator should be negated.</param>
    public DutchPhoneNumberValidator(string name, string messageTemplate, string tag, bool negated)
      : base(messageTemplate, tag, negated) {
      this.Name = name;
      this.Categories = PhoneNumberCategories.Default;
      this.AllowCountryCallingCode = true;

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

    #region Properties

    /// <summary>Gets or sets the name of the configuration section that is used to preconfigure the validator. The default value is set to <see cref="ValidatorsSection.DefaultSectionName"/>.
    /// </summary>
    public static string ConfigurationSectionName {
      get { return configurationSectionName ?? ValidatorsSection.DefaultSectionName; }
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

    /// <summary>Gets the Default Message Template when the validator is non negated.</summary>
    protected override string DefaultNonNegatedMessageTemplate {
      get { return "Value '{0}' is not a valid phone number"; }
    }

    /// <summary>Gets the Default Message Template when the validator is negated.</summary>
    protected override string DefaultNegatedMessageTemplate {
      get { return "Value '{0}' is a valid phone number"; }
    }

    /// <summary>Gets the values that were set through the configuration.</summary>
    private static Dictionary<string, ConfiguredValuesContainer> ConfiguredValues {
      get { return ConfiguredValuesSingletonContainer.ConfiguredValues; }
    }

    #endregion

    #region Validator overrides

    /// <summary>Implements the validation logic for the receiver.</summary>
    /// <param name="objectToValidate">The object to validate.</param>
    /// <param name="currentTarget">The object on the behalf of which the validation is performed.</param>
    /// <param name="key">The key that identifies the source of objectToValidate.</param>
    /// <param name="validationResults">The validation results to which the outcome of the validation should be stored.</param>
    protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults) {
      if(string.IsNullOrEmpty(objectToValidate) || this.Categories == PhoneNumberCategories.None) {
        if(!this.Negated) {
          this.LogValidationResult(validationResults, this.GetMessage(objectToValidate, key), currentTarget, key);
        }

        return;
      }

      string strippedObjectToValidate = objectToValidate.Replace(" ", string.Empty);

      bool isValid = false;

      if((this.Categories & PhoneNumberCategories.Regular) == PhoneNumberCategories.Regular) {
        isValid = ValidateRegularNumber(strippedObjectToValidate, this.AllowCountryCallingCode, this.AllowCarrierPreselect, this.IncludeAreaCodes, this.ExcludeAreaCodes);
      }

      if((!isValid || (isValid && this.Negated)) && (this.Categories & PhoneNumberCategories.Mobile) == PhoneNumberCategories.Mobile) {
        isValid |= ValidateMobileNumber(strippedObjectToValidate, this.AllowCountryCallingCode, this.AllowCarrierPreselect);
      }

      if((!isValid || (isValid && this.Negated)) && (this.Categories & PhoneNumberCategories.Emergency) == PhoneNumberCategories.Emergency) {
        isValid |= ValidateEmergencyNumber(strippedObjectToValidate);
      }

      if((!isValid || (isValid && this.Negated)) && (this.Categories & PhoneNumberCategories.Service) == PhoneNumberCategories.Service) {
        isValid |= ValidateServiceNumber(strippedObjectToValidate);
      }

      if((!isValid || (isValid && this.Negated)) && (this.Categories & PhoneNumberCategories.Other) == PhoneNumberCategories.Other) {
        isValid |= ValidateOtherNumber(strippedObjectToValidate, this.AllowCarrierPreselect);
      }

      isValid = this.Negated ? !isValid : isValid;

      if(!isValid) {
        this.LogValidationResult(validationResults, this.GetMessage(objectToValidate, key), currentTarget, key);
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
      if(allowCarrierPreselect) {
        return allowCountryCallingCode
          ? new DutchPhoneValidatorMobileRegexWithCarrierPreselect().IsMatch(input)
          : new DutchPhoneValidatorMobileRegexNoCountryAccessCodeWithCarrierPreselect().IsMatch(input);
      }
      else {
        return allowCountryCallingCode
          ? new DutchPhoneValidatorMobileRegex().IsMatch(input)
          : new DutchPhoneValidatorMobileRegexNoCountryAccessCode().IsMatch(input);
      }
    }

    /// <summary>Validates whether the input is a valid Dutch phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateOtherNumber(string input, bool allowCarrierPreselect) {
      return allowCarrierPreselect
        ? new DutchPhoneValidatorOtherRegexWithCarrierPreselect().IsMatch(input)
        : new DutchPhoneValidatorOtherRegex().IsMatch(input);
    }

    /// <summary>Validates whether the input is a valid Dutch regular phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <param name="allowCountryCallingCode">Indicates whether the country calling code is allowed in the phone number.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <param name="includeAreaCodes">Overrides the area codes that are part of the regular expression.</param>
    /// <param name="excludeAreaCodes">The area codes that must be excluded from the standard list of area codes.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateRegularNumber(string input, bool allowCountryCallingCode, bool allowCarrierPreselect, string includeAreaCodes, string excludeAreaCodes) {
      if(string.IsNullOrEmpty(includeAreaCodes) && string.IsNullOrEmpty(excludeAreaCodes)) {
        if(allowCarrierPreselect) {
          return allowCountryCallingCode
            ? new DutchPhoneValidatorDefaultRegularRegexWithCarrierPreselect().IsMatch(input)
            : new DutchPhoneValidatorDefaultRegularRegexNoCountryAccessCodeWithCarrierPreselect().IsMatch(input);
        }
        else {
          return allowCountryCallingCode
            ? new DutchPhoneValidatorDefaultRegularRegex().IsMatch(input)
            : new DutchPhoneValidatorDefaultRegularRegexNoCountryAccessCode().IsMatch(input);
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

      return Regex.IsMatch(input, pattern, RegexOptions.Singleline);
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
      /// <param name="sectionName">The name of the config section that must be read. Use <see langword="null"/> or <see cref="string.Empty"/> to use the default section name.</param>
      /// <returns>The values that were read from the configuration or <see langword="null"/> if there was no configuration.</returns>
      private static Dictionary<string, ConfiguredValuesContainer> ReadConfiguration(string sectionName) {
        if(string.IsNullOrEmpty(sectionName)) {
          sectionName = ValidatorsSection.DefaultSectionName;
        }

        ValidatorsSection validatorsSection = ConfigurationManager.GetSection(sectionName) as ValidatorsSection;

        if(validatorsSection == null || validatorsSection.DutchPhoneNumberValidators.Count == 0) {
          return null;
        }

        Dictionary<string, ConfiguredValuesContainer> configuredValues = new Dictionary<string, ConfiguredValuesContainer>();
        foreach(DutchPhoneNumberValidatorConfigElement validatorConfig in validatorsSection.DutchPhoneNumberValidators.Values) {
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
