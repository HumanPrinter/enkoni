//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DutchPhoneNumberValidatorAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains an attribute for the validation capabilities.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Enkoni.Framework.Validation.Validators {
  /// <summary>Attribute to specify Dutch phone number range validation on a property, method or field.</summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true,
    Inherited = false)]
  public sealed class DutchPhoneNumberValidatorAttribute : ValueValidatorAttribute {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    public DutchPhoneNumberValidatorAttribute() {
      this.AllowCountryCallingCode = true;
      this.Categories = PhoneNumberCategories.Default;
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the categories of phone numbers that must be considered valid.</summary>
    public PhoneNumberCategories Categories { get; set; }

    /// <summary>Gets or sets a value indicating whether the international dialing prefix and country calling code (eg. 0031 or +31) is allowed 
    /// in the phone number. Defaults to <see langword="true"/>.</summary>
    public bool AllowCountryCallingCode { get; set; }

    /// <summary>Gets or sets a value indicating whether a carrier preselect code (eg. 1642) is allowed in the phone number. Defaults to 
    /// <see langword="false"/>.</summary>
    public bool AllowCarrierPreselect { get; set; }

    /// <summary>Gets or sets the semicolon-seperated area codes that are considered valid. This overrides the default area codes for this validator.
    /// </summary>
    public string IncludeAreaCodes { get; set; }

    /// <summary>Gets or sets the semicolon-seperated area codes that are considered invalid.</summary>
    public string ExcludeAreaCodes { get; set; }
    #endregion

    #region ValueValidatorAttribute overrides
    /// <summary>Creates the <see cref="DutchPhoneNumberValidator"/> described by the configuration object.</summary>
    /// <param name="targetType">The type of object that will be validated by the validator.</param>
    /// <returns>The created Validator.</returns>
    protected override Validator DoCreateValidator(Type targetType) {
      return new DutchPhoneNumberValidator(this.MessageTemplate, this.Tag, this.Negated) { 
        AllowCountryCallingCode = this.AllowCountryCallingCode, 
        Categories = this.Categories, 
        AllowCarrierPreselect = this.AllowCarrierPreselect,
        IncludeAreaCodes = this.IncludeAreaCodes, 
        ExcludeAreaCodes = this.ExcludeAreaCodes
      };
    }
    #endregion
  }
}
