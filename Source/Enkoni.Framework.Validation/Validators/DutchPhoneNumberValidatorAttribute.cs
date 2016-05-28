using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Enkoni.Framework.Validation.Validators {
  /// <summary>Attribute to specify Dutch phone number range validation on a property, method or field.</summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true,
    Inherited = false)]
  public sealed class DutchPhoneNumberValidatorAttribute : ValueValidatorAttribute {
    #region Instance variables

    /// <summary>The actual configured value for the allowCountryCallingCode flag.</summary>
    private bool? allowCountryCallingCode;

    /// <summary>The actual configured value for the allowCarrierPreselect flag.</summary>
    private bool? allowCarrierPreselect;

    /// <summary>The actual value of the IncludeAreaCodes property.</summary>
    private string includeAreaCodes;

    /// <summary>Indicates whether the value for IncludeAreaCodes was set explicitly.</summary>
    private bool includeAreaCodesSetExplicitly;

    /// <summary>The actual value of the ExcludeAreaCodes property.</summary>
    private string excludeAreaCodes;

    /// <summary>Indicates whether the value for ExcludeAreaCodes was set explicitly.</summary>
    private bool excludeAreaCodesSetExplicitly;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    public DutchPhoneNumberValidatorAttribute() {
      this.Name = DutchPhoneNumberValidator.DefaultName;
      this.Categories = PhoneNumberCategories.Default;
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets the categories of phone numbers that must be considered valid.</summary>
    public PhoneNumberCategories Categories { get; set; }

    /// <summary>Gets or sets the name of the validator.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets a value indicating whether the international dialing prefix and country calling code (eg. 0031 or +31) is allowed
    /// in the phone number. Defaults to <see langword="true"/>.</summary>
    public bool AllowCountryCallingCode {
      get { return this.allowCountryCallingCode.GetValueOrDefault(true); }
      set { this.allowCountryCallingCode = value; }
    }

    /// <summary>Gets or sets a value indicating whether a carrier preselect code (eg. 1642) is allowed in the phone number. Defaults to
    /// <see langword="false"/>.</summary>
    public bool AllowCarrierPreselect {
      get { return this.allowCarrierPreselect.GetValueOrDefault(false); }
      set { this.allowCarrierPreselect = value; }
    }

    /// <summary>Gets or sets the semicolon-separated area codes that are considered valid. This overrides the default area codes for this validator.
    /// </summary>
    public string IncludeAreaCodes {
      get {
        return this.includeAreaCodes;
      }

      set {
        this.includeAreaCodes = value;
        this.includeAreaCodesSetExplicitly = true;
      }
    }

    /// <summary>Gets or sets the semicolon separated area codes that are considered invalid.</summary>
    public string ExcludeAreaCodes {
      get {
        return this.excludeAreaCodes;
      }

      set {
        this.excludeAreaCodes = value;
        this.excludeAreaCodesSetExplicitly = true;
      }
    }

    #endregion

    #region ValueValidatorAttribute overrides

    /// <summary>Creates the <see cref="DutchPhoneNumberValidator"/> described by the configuration object.</summary>
    /// <param name="targetType">The type of object that will be validated by the validator.</param>
    /// <returns>The created Validator.</returns>
    protected override Validator DoCreateValidator(Type targetType) {
      DutchPhoneNumberValidator validator = new DutchPhoneNumberValidator(this.Name, this.MessageTemplate, this.Tag, this.Negated) {
        Categories = this.Categories
      };

      if(this.allowCountryCallingCode.HasValue) {
        validator.AllowCountryCallingCode = this.AllowCountryCallingCode;
      }

      if(this.allowCarrierPreselect.HasValue) {
        validator.AllowCarrierPreselect = this.AllowCarrierPreselect;
      }

      if(this.includeAreaCodesSetExplicitly) {
        validator.IncludeAreaCodes = this.IncludeAreaCodes;
      }

      if(this.excludeAreaCodesSetExplicitly) {
        validator.ExcludeAreaCodes = this.ExcludeAreaCodes;
      }

      return validator;
    }

    #endregion
  }
}
