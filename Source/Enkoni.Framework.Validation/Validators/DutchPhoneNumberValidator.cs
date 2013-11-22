//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DutchPhoneNumberValidator.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains a custom validator that validates Dutch phone numbers.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

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
  /// <summary>Performs validation on <see cref="String"/> instances by checking if they contain valid Dutch phone numbers.</summary>
  public class DutchPhoneNumberValidator : ValueValidator<string> {
    #region Constants
    /// <summary>Defines the default name for the validator.</summary>
    internal const string DefaultName = "00661f4a-faa2-452f-8fd9-af6c776bfc49";
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

    /// <summary>Gets or sets the semicolon-seperated area codes that are considered valid. This overrides the default area codes for this validator.
    /// </summary>
    public string IncludeAreaCodes { get; set; }

    /// <summary>Gets or sets the semicolon-seperated area codes that are considered invalid.</summary>
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
      return new ServiceRegexNetherlands().IsMatch(input);
    }

    /// <summary>Validates whether the input is a valid Dutch emergency phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateEmergencyNumber(string input) {
      return new EmergencyRegexNetherlands().IsMatch(input);
    }

    /// <summary>Validates whether the input is a valid Dutch mobile phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <param name="allowCountryCallingCode">Indicates whether the country calling code is allowed in the phone number.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateMobileNumber(string input, bool allowCountryCallingCode, bool allowCarrierPreselect) {
      if(allowCarrierPreselect) {
        return allowCountryCallingCode
          ? new MobileRegexNetherlandsWithCarrierPreselect().IsMatch(input)
          : new MobileRegexNetherlandsNoCountryAccessCodeWithCarrierPreselect().IsMatch(input);
      }
      else {
        return allowCountryCallingCode
          ? new MobileRegexNetherlands().IsMatch(input)
          : new MobileRegexNetherlandsNoCountryAccessCode().IsMatch(input);
      }
    }

    /// <summary>Validates whether the input is a valid Dutch phone number.</summary>
    /// <param name="input">The string that must be validated.</param>
    /// <param name="allowCarrierPreselect">Indicates whether a carrier preselect code is allowed in the phone number.</param>
    /// <returns><see langword="true"/> is the input is valid; otherwise, <see langword="false"/>.</returns>
    private static bool ValidateOtherNumber(string input, bool allowCarrierPreselect) {
      return allowCarrierPreselect
        ? new OtherRegexNetherlandsWithCarrierPreselect().IsMatch(input)
        : new OtherRegexNetherlands().IsMatch(input);
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
            ? new DefaultRegularRegexNetherlandsWithCarrierPreselect().IsMatch(input)
            : new DefaultRegularRegexNetherlandsNoCountryAccessCodeWithCarrierPreselect().IsMatch(input);
        }
        else {
          return allowCountryCallingCode
            ? new DefaultRegularRegexNetherlands().IsMatch(input)
            : new DefaultRegularRegexNetherlandsNoCountryAccessCode().IsMatch(input);
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
      internal static readonly Dictionary<string, ConfiguredValuesContainer> ConfiguredValues = ReadConfiguration();

      /// <summary>Initializes static members of the <see cref="ConfiguredValuesSingletonContainer"/> class.</summary>
      /// <remarks>Even though this constructor does nothing by itself (it has an empty body), declaring this static constructor prevents the C# 
      /// compiler from marking this type as <c>beforefieldinit</c> which is required in order to get the laziness behavior that is intended.</remarks>
      static ConfiguredValuesSingletonContainer() { 
      }

      /// <summary>Prevents a default instance of the <see cref="ConfiguredValuesSingletonContainer"/> class from being created.</summary>
      private ConfiguredValuesSingletonContainer() {
      }

      /// <summary>Reads the configuration and sets the configured values.</summary>
      /// <returns>The values that were read from the configuration or <see langword="null"/> if there was no configuration.</returns>
      private static Dictionary<string, ConfiguredValuesContainer> ReadConfiguration() {
        string sectionTypeName = typeof(ValidatorsSection).AssemblyQualifiedName;
        ValidatorsSection validatorsSection = null;
        System.Configuration.Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        validatorsSection = configuration.Sections["Enkoni.Validators"] as ValidatorsSection;
        if(validatorsSection == null) {
          foreach(ConfigurationSection section in configuration.Sections) {
            if(sectionTypeName.StartsWith(section.SectionInformation.Type, StringComparison.Ordinal)) {
              validatorsSection = section as ValidatorsSection;
              break;
            }
          }
        }

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
