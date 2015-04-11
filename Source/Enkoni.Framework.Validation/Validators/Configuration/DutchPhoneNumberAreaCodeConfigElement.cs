using System.Configuration;

namespace Enkoni.Framework.Validation.Validators.Configuration {
  /// <summary>Defines the configuration element that holds a single area code that should either be added or removed from the list of area codes
  /// that must be validated by the <see cref="DutchPhoneNumberValidator"/>.</summary>
  public class DutchPhoneNumberAreaCodeConfigElement : ConfigurationElement {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberAreaCodeConfigElement"/> class.</summary>
    public DutchPhoneNumberAreaCodeConfigElement() {
    }

    /// <summary>Initializes a new instance of the <see cref="DutchPhoneNumberAreaCodeConfigElement"/> class using the specified area code.</summary>
    /// <param name="areaCode">The area code that must be added to the list.</param>
    public DutchPhoneNumberAreaCodeConfigElement(string areaCode) {
      this.AreaCode = areaCode;
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the area code that is set through this element.</summary>
    [ConfigurationProperty("areaCode", IsKey = true, IsRequired = true, DefaultValue = "000")]
    [RegexStringValidator(@"^\d{1,4}$")]
    public string AreaCode {
      get { return (string)this["areaCode"]; }
      set { this["areaCode"] = value; }
    }
    #endregion
  }
}
