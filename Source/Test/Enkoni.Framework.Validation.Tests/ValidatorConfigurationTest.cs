using System.Configuration;

using Enkoni.Framework.Validation.Validators;
using Enkoni.Framework.Validation.Validators.Configuration;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Validation.Tests {
  /// <summary>Tests the functionality of the validator configuration classes.</summary>
  [TestClass]
  public class ValidatorConfigurationTest {
    #region ValidatorsSection TestCases
    /// <summary>Tests the functionality of the <see cref="ValidatorsSection"/> class.</summary>
    [TestMethod]
    public void ValidatorConfiguration_ValidatorsSection() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators);
      Assert.AreEqual(2, validatorsSection.DutchPhoneNumberValidators.Count);
      Assert.IsNotNull(validatorsSection.EmailValidators);
      Assert.AreEqual(2, validatorsSection.EmailValidators.Count);
    }
    #endregion

    #region DutchPhoneNumberValidatorConfiguration TestCases
    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorConfigElement"/> class.</summary>
    [TestMethod]
    public void ValidatorConfiguration_ReadDutchPhoneNumberValidatorConfiguration_Default() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase01") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators);
      Assert.AreEqual(1, validatorsSection.DutchPhoneNumberValidators.Count);

      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidators[string.Empty].AllowCountryCallingCode);
      Assert.IsTrue(validatorsSection.DutchPhoneNumberValidators[string.Empty].AllowCarrierPreselect);

      Assert.AreEqual(143, validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["058"]);
      Assert.AreEqual("058", validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["058"].AreaCode);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["0123"]);
      Assert.AreEqual("0123", validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["0123"].AreaCode);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorConfigElement"/> class.</summary>
    [TestMethod]
    public void ValidatorConfiguration_ReadDutchPhoneNumberValidatorConfiguration_ClearAreaCodes() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase02") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.AreEqual(1, validatorsSection.DutchPhoneNumberValidators.Count);

      Assert.IsTrue(validatorsSection.DutchPhoneNumberValidators[string.Empty].AllowCountryCallingCode);
      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidators[string.Empty].AllowCarrierPreselect);

      Assert.AreEqual(1, validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["0123"]);
      Assert.AreEqual("0123", validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["0123"].AreaCode);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorConfigElement"/> class.</summary>
    [TestMethod]
    public void ValidatorConfiguration_ReadDutchPhoneNumberValidatorConfiguration_RemoveAreaCode() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase03") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators[string.Empty]);

      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidators[string.Empty].AllowCountryCallingCode);
      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidators[string.Empty].AllowCarrierPreselect);

      Assert.AreEqual(141, validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["010"]);
      Assert.AreEqual("010", validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["010"].AreaCode);
      Assert.IsNull(validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["058"]);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorConfigElement"/> class.</summary>
    [TestMethod]
    public void ValidatorConfiguration_ReadDutchPhoneNumberValidatorConfiguration_MultipleValidators() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase04") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.AreEqual(2, validatorsSection.DutchPhoneNumberValidators.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators[string.Empty]);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators["TestValidator"]);

      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidators[string.Empty].AllowCountryCallingCode);
      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidators[string.Empty].AllowCarrierPreselect);
      Assert.IsTrue(validatorsSection.DutchPhoneNumberValidators["TestValidator"].AllowCountryCallingCode);
      Assert.IsTrue(validatorsSection.DutchPhoneNumberValidators["TestValidator"].AllowCarrierPreselect);

      Assert.AreEqual(141, validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["010"]);
      Assert.AreEqual("010", validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["010"].AreaCode);
      Assert.IsNull(validatorsSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["058"]);

      Assert.AreEqual(1, validatorsSection.DutchPhoneNumberValidators["TestValidator"].AreaCodes.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidators["TestValidator"].AreaCodes["0123"]);
      Assert.AreEqual("0123", validatorsSection.DutchPhoneNumberValidators["TestValidator"].AreaCodes["0123"].AreaCode);
    }
    #endregion

    #region EmailValidatorConfiguration TestCases
    /// <summary>Tests the functionality of the <see cref="EmailValidatorConfigElement"/> class.</summary>
    [TestMethod]
    public void ValidatorConfiguration_ReadEmailValidatorConfiguration_Default() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase05") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.IsNotNull(validatorsSection.EmailValidators);
      Assert.AreEqual(2, validatorsSection.EmailValidators.Count);

      Assert.IsFalse(validatorsSection.EmailValidators[string.Empty].AllowComments);
      Assert.IsFalse(validatorsSection.EmailValidators[string.Empty].AllowIPAddresses);
      Assert.IsFalse(validatorsSection.EmailValidators[string.Empty].RequireTopLevelDomain);

      Assert.AreEqual(0, validatorsSection.EmailValidators[string.Empty].IncludeDomains.Count);
      Assert.AreEqual(0, validatorsSection.EmailValidators[string.Empty].ExcludeDomains.Count);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorConfigElement"/> class.</summary>
    [TestMethod]
    public void ValidatorConfiguration_ReadEmailValidatorConfiguration_IncludeExcludeDomains() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase06") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.AreEqual(1, validatorsSection.EmailValidators.Count);

      Assert.IsFalse(validatorsSection.EmailValidators[string.Empty].AllowComments);
      Assert.IsTrue(validatorsSection.EmailValidators[string.Empty].AllowIPAddresses);
      Assert.IsFalse(validatorsSection.EmailValidators[string.Empty].RequireTopLevelDomain);

      Assert.AreEqual(2, validatorsSection.EmailValidators[string.Empty].IncludeDomains.Count);
      Assert.IsNotNull(validatorsSection.EmailValidators[string.Empty].IncludeDomains["microsoft.com"]);
      Assert.AreEqual("microsoft.com", validatorsSection.EmailValidators[string.Empty].IncludeDomains["microsoft.com"].Pattern);
      Assert.AreEqual(2, validatorsSection.EmailValidators[string.Empty].ExcludeDomains.Count);
      Assert.IsNotNull(validatorsSection.EmailValidators[string.Empty].ExcludeDomains["*yahoo.com"]);
      Assert.AreEqual("*yahoo.com", validatorsSection.EmailValidators[string.Empty].ExcludeDomains["*yahoo.com"].Pattern);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorConfigElement"/> class.</summary>
    [TestMethod]
    public void ValidatorConfiguration_ReadEmailValidatorConfiguration_MultipleValidators() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase07") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.AreEqual(2, validatorsSection.EmailValidators.Count);
      Assert.IsNotNull(validatorsSection.EmailValidators[string.Empty]);
      Assert.IsNotNull(validatorsSection.EmailValidators["TestValidator"]);

      Assert.IsFalse(validatorsSection.EmailValidators[string.Empty].AllowComments);
      Assert.IsFalse(validatorsSection.EmailValidators[string.Empty].AllowIPAddresses);
      Assert.IsFalse(validatorsSection.EmailValidators[string.Empty].RequireTopLevelDomain);
      Assert.IsTrue(validatorsSection.EmailValidators["TestValidator"].AllowComments);
      Assert.IsTrue(validatorsSection.EmailValidators["TestValidator"].AllowIPAddresses);
      Assert.IsTrue(validatorsSection.EmailValidators["TestValidator"].RequireTopLevelDomain);

      Assert.AreEqual(1, validatorsSection.EmailValidators[string.Empty].IncludeDomains.Count);
      Assert.AreEqual(0, validatorsSection.EmailValidators[string.Empty].ExcludeDomains.Count);
      Assert.IsNotNull(validatorsSection.EmailValidators[string.Empty].IncludeDomains["gmail.com"]);
      Assert.AreEqual("gmail.com", validatorsSection.EmailValidators[string.Empty].IncludeDomains["gmail.com"].Pattern);
      Assert.IsNull(validatorsSection.EmailValidators[string.Empty].IncludeDomains["yahoo.com"]);

      Assert.AreEqual(0, validatorsSection.EmailValidators["TestValidator"].IncludeDomains.Count);
      Assert.AreEqual(1, validatorsSection.EmailValidators["TestValidator"].ExcludeDomains.Count);
      Assert.IsNotNull(validatorsSection.EmailValidators["TestValidator"].ExcludeDomains["yahoo.com"]);
      Assert.AreEqual("yahoo.com", validatorsSection.EmailValidators["TestValidator"].ExcludeDomains["yahoo.com"].Pattern);
    }
    #endregion
  }
}
