//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorsSection.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains a custom configuration section used to configure the Enkoni validators.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Configuration;

namespace Enkoni.Framework.Validation.Validators.Configuration {
  /// <summary>Defines the configuration section that can be used to configure the Enkoni validators.</summary>
  public class ValidatorsSection : ConfigurationSection {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="ValidatorsSection"/> class.</summary>
    public ValidatorsSection() {
    }
    #endregion

    #region Properties
    /// <summary>Gets the configuration for the <see cref="DutchPhoneNumberValidator"/>.</summary>
    [ConfigurationProperty("DutchPhoneNumberValidator")]
    public DutchPhoneNumberValidatorConfigElement DutchPhoneNumberValidator {
      get {
        return (DutchPhoneNumberValidatorConfigElement)this["DutchPhoneNumberValidator"];
      }
    }
    #endregion
  }
}
