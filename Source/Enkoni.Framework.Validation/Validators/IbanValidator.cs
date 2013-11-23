//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="IbanValidator.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains a custom validator that validates IBAN account numbers.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

using Enkoni.Framework.Validation.Properties;
using Enkoni.Framework.Validation.RegularExpressions;
using Enkoni.Framework.Validation.Validators.Configuration;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Enkoni.Framework.Validation.Validators {
  /// <summary>Performs validation on <see cref="String"/> instances by checking if they contain a valid IBAN account number.</summary>
  public class IbanValidator : ValueValidator<string> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="IbanValidator"/> class.</summary>
    /// <param name="messageTemplate">The template to use when logging validation results, or null we the default message template is to be used.</param>
    /// <param name="tag">The tag to set when logging validation results, or null.</param>
    /// <param name="negated">Indicates if the validation logic represented by the validator should be negated.</param>
    public IbanValidator(string messageTemplate, string tag, bool negated)
      : base(messageTemplate, tag, negated) {
    }
    #endregion

    #region Properties
    /// <summary>Gets the Default Message Template when the validator is non negated.</summary>
    protected override string DefaultNonNegatedMessageTemplate {
      get { return "Value '{0}' is not a valid IBAN account number"; }
    }

    /// <summary>Gets the Default Message Template when the validator is negated.</summary>
    protected override string DefaultNegatedMessageTemplate {
      get { return "Value '{0}' is a valid IBAN account number"; }
    }
    #endregion

    #region Validator overrides
    /// <summary>Implements the validation logic for the receiver.</summary>
    /// <param name="objectToValidate">The object to validate.</param>
    /// <param name="currentTarget">The object on the behalf of which the validation is performed.</param>
    /// <param name="key">The key that identifies the source of objectToValidate.</param>
    /// <param name="validationResults">The validation results to which the outcome of the validation should be stored.</param>
    protected override void DoValidate(string objectToValidate, object currentTarget, string key, ValidationResults validationResults) {
      /* First, check if the account number has a value. */
      if(string.IsNullOrEmpty(objectToValidate)) {
        if(!this.Negated) {
          this.LogValidationResult(validationResults, this.GetMessage(objectToValidate, key), currentTarget, key);
        }

        return;
      }

      /* Then, check if the account number matches the regular expression */
      Match match = new IbanRegex().Match(objectToValidate);
      if(!match.Success && !this.Negated) {
        this.LogValidationResult(validationResults, this.GetMessage(objectToValidate, key), currentTarget, key);
        return;
      }
      else if(!match.Success && this.Negated) {
        return;
      }

      /* If the account number passes the regex check, continue to check the contents */
      string countryCode = match.Groups["countrycode"].Value;
      int checkDigits = int.Parse(match.Groups["check_digits"].Value, CultureInfo.InvariantCulture);
      string accountId = match.Groups["accountid"].Value;

      BigInteger working = ConvertToNumeric(accountId + countryCode + checkDigits);
      BigInteger remainder = working % 97;
      bool isValid = remainder == 1;

      isValid = this.Negated ? !isValid : isValid;

      if(!isValid) {
        this.LogValidationResult(validationResults, this.GetMessage(objectToValidate, key), currentTarget, key);
      }
    }
    #endregion

    #region Private helper methods
    /// <summary>Converts a string into a numeric value by substituting the 'A' to 'Z' characters whit there numeric equivalents where 'A' equals '10',
    /// 'B' equals '11' etcetera.</summary>
    /// <param name="text">The value that must be converted.</param>
    /// <returns>The converted value.</returns>
    private static BigInteger ConvertToNumeric(string text) {
      char[] chars = text.ToCharArray();
      StringBuilder result = new StringBuilder();
      foreach(char character in chars) {
        if(character >= 48 && character <= 57) {
          result.Append(character - 48);
        }
        else {
          result.Append(character - 55);
        }
      }

      return BigInteger.Parse(result.ToString(), CultureInfo.InvariantCulture);
    }
    #endregion
  }
}
