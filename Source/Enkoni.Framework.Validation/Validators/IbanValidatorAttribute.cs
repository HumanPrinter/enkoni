//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="IbanValidatorAttribute.cs" company="Oscar Brouwer">
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
  /// <summary>Attribute to specify IBAN account number validation on a property, method or field.</summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true,
    Inherited = false)]
  public sealed class IbanValidatorAttribute : ValueValidatorAttribute {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="IbanValidatorAttribute"/> class.</summary>
    public IbanValidatorAttribute() {
    }
    #endregion

    #region ValueValidatorAttribute overrides
    /// <summary>Creates the <see cref="DutchPhoneNumberValidator"/> described by the configuration object.</summary>
    /// <param name="targetType">The type of object that will be validated by the validator.</param>
    /// <returns>The created Validator.</returns>
    protected override Validator DoCreateValidator(Type targetType) {
      IbanValidator validator = new IbanValidator(this.MessageTemplate, this.Tag, this.Negated);
      return validator;
    }
    #endregion
  }
}
