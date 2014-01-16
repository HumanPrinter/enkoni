//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailValidatorAttribute.cs" company="Oscar Brouwer">
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
  /// <summary>Attribute to specify e-mail validation on a property, method or field.</summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true,
    Inherited = false)]
  public sealed class EmailValidatorAttribute : ValueValidatorAttribute {
    #region Instance variables
    /// <summary>The actual configured value for the allowComments flag.</summary>
    private bool? allowComments;

    /// <summary>The actual configured value for the allowIPAddresses flag.</summary>
    private bool? allowIPAddresses;

    /// <summary>The actual value of the IncludeDomains property.</summary>
    private string includeDomains;

    /// <summary>Indicates whether the value for IncludeDomains was set explicitly.</summary>
    private bool includeDomainsSetExplicitly;

    /// <summary>The actual value of the ExcludeDomains property.</summary>
    private string excludeDomains;

    /// <summary>Indicates whether the value for ExcludeDomains was set explicitly.</summary>
    private bool excludeDomainsSetExplicitly;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="EmailValidatorAttribute"/> class.</summary>
    public EmailValidatorAttribute() {
      this.Name = EmailValidator.DefaultName;
      this.Category = EmailCategory.Basic;
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the categories of phone numbers that must be considered valid.</summary>
    public EmailCategory Category { get; set; }

    /// <summary>Gets or sets the name of the validator.</summary>
    public string Name { get; set; }

    /// <summary>Gets or sets a value indicating whether comments are allowed in the e-mail address. Defaults to <see langword="false"/>.</summary>
    public bool AllowComments {
      get { return this.allowComments.GetValueOrDefault(false); }
      set { this.allowComments = value; }
    }

    /// <summary>Gets or sets a value indicating whether IP addresses are allowed in the e-mail address. Defaults to <see langword="false"/>.</summary>
    public bool AllowIPAddresses {
      get { return this.allowIPAddresses.GetValueOrDefault(false); }
      set { this.allowIPAddresses = value; }
    }

    /// <summary>Gets or sets the semicolon-seperated domains that are white listed.</summary>
    public string IncludeDomains {
      get { 
        return this.includeDomains; 
      }

      set {
        this.includeDomains = value;
        this.includeDomainsSetExplicitly = true;
      } 
    }

    /// <summary>Gets or sets the semicolon-seperated domains that are black listed.</summary>
    public string ExcludeDomains {
      get {
        return this.excludeDomains;
      }

      set {
        this.excludeDomains = value;
        this.excludeDomainsSetExplicitly = true;
      }
    }
    #endregion

    #region ValueValidatorAttribute overrides
    /// <summary>Creates the <see cref="DutchPhoneNumberValidator"/> described by the configuration object.</summary>
    /// <param name="targetType">The type of object that will be validated by the validator.</param>
    /// <returns>The created Validator.</returns>
    protected override Validator DoCreateValidator(Type targetType) {
      EmailValidator validator = new EmailValidator(this.Name, this.MessageTemplate, this.Tag, this.Negated) { 
        Category = this.Category
      };

      if(this.allowComments.HasValue) {
        validator.AllowComments = this.AllowComments;
      }

      if(this.allowIPAddresses.HasValue) {
        validator.AllowIPAddresses = this.AllowIPAddresses;
      }

      if(this.includeDomainsSetExplicitly) {
        validator.IncludeDomains = this.IncludeDomains;
      }

      if(this.excludeDomainsSetExplicitly) {
        validator.ExcludeDomains = this.ExcludeDomains;
      }

      return validator;
    }
    #endregion
  }
}
