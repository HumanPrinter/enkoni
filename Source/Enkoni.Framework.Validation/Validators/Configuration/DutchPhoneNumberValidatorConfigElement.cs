//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DutchPhoneNumberValidatorConfigElement.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains a custom configuration element used to configure the Dutch phone number validator.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Configuration;

using Enkoni.Framework.Validation.Properties;

namespace Enkoni.Framework.Validation.Validators.Configuration {
  /// <summary>Defines the configuration element that can be used to configure the <see cref="DutchPhoneNumberValidator"/>.</summary>
  public class DutchPhoneNumberValidatorConfigElement : ConfigurationElement {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberValidatorConfigElement"/> class.</summary>
    public DutchPhoneNumberValidatorConfigElement() {
      string[] defaultAreaCodes = Resources.AreaCodes_NL.Split(';');
      foreach(string areaCode in defaultAreaCodes) {
        DutchPhoneNumberAreaCodeConfigElement areaElement = new DutchPhoneNumberAreaCodeConfigElement("0" + areaCode);
        this.AreaCodes.Add(areaElement);
      }
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets a value indicating whether country calling codes are allowed by the validator.</summary>
    [ConfigurationProperty("allowCountryCallingCode", IsKey = true, IsRequired = false, DefaultValue = true)]
    public bool AllowCountryCallingCode {
      get { return (bool)this["allowCountryCallingCode"]; }
      set { this["allowCountryCallingCode"] = value; }
    }

    /// <summary>Gets the collection of countries that must be used by the validator.</summary>
    [ConfigurationProperty("areaCodes", IsRequired = false, IsDefaultCollection = false)]
    [ConfigurationCollection(typeof(DutchPhoneNumberAreaCodeCollection), AddItemName = "add", RemoveItemName = "remove", ClearItemsName = "clear")]
    public DutchPhoneNumberAreaCodeCollection AreaCodes { 
      get {
        DutchPhoneNumberAreaCodeCollection areaCodes = (DutchPhoneNumberAreaCodeCollection)this["areaCodes"];
        return areaCodes;
      }
    }
    #endregion
  }
}
