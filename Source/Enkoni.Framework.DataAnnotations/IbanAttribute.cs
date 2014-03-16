using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;

using Enkoni.Framework.Validation.RegularExpressions;

namespace Enkoni.Framework.DataAnnotations {
  /// <summary>Attribute to specify IBAN account number validation on a property, method or field.</summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true,
    Inherited = false)]
  public sealed class IbanAttribute : ValidationAttribute {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="IbanAttribute"/> class.</summary>
    public IbanAttribute() {
    }

    /// <summary>Initializes a new instance of the <see cref="IbanAttribute"/> class.</summary>
    /// <param name="errorMessage">The error message to associate with a validation control.</param>
    public IbanAttribute(string errorMessage)
      : base(errorMessage) {
    }

    /// <summary>Initializes a new instance of the <see cref="IbanAttribute"/> class.</summary>
    /// <param name="errorMessageAccessor">The function that enables access to validation resources.</param>
    /// <exception cref="ArgumentNullException"><paramref name="errorMessageAccessor"/> is <see langword="null"/>.</exception>
    public IbanAttribute(Func<string> errorMessageAccessor)
      : base(errorMessageAccessor) {
    }
    #endregion

    #region ValidationAttribute overrides
    /// <summary>Determines whether the specified value of the object is valid.</summary>
    /// <param name="value">The value of the object to validate.</param>
    /// <returns><see langword="true"/> if the specified value is valid; otherwise, <see langword="false"/>.</returns>
    public override bool IsValid(object value) {
      string valueToValidate = value as string;
      if(string.IsNullOrWhiteSpace(valueToValidate)) {
        return false;
      }

      /* Then, check if the account number matches the regular expression */
      Match match = new IbanValidatorRegex().Match(valueToValidate);
      if(!match.Success) {
        return false;
      }

      /* If the account number passes the regex check, continue to check the contents */
      string countryCode = match.Groups["countrycode"].Value;
      int checkDigits = int.Parse(match.Groups["check_digits"].Value, CultureInfo.InvariantCulture);
      string accountId = match.Groups["accountid"].Value;

      BigInteger working = ConvertToNumeric(accountId + countryCode + checkDigits);
      BigInteger remainder = working % 97;
      bool isValid = remainder == 1;

      return isValid;
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
