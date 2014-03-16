using System.Configuration;

using Enkoni.Framework.DataAnnotations;
using Enkoni.Framework.DataAnnotations.Configuration;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.DataAnnotations.Tests {
  /// <summary>Tests the functionality of the validation configuration classes.</summary>
  [TestClass]
  public class ValidationConfigurationTest {
    #region ValidationSection TestCases
    /// <summary>Tests the functionality of the <see cref="ValidationSection"/> class.</summary>
    [TestMethod]
    public void TestCase01_ValidationSection() {
      ValidationSection validationSection = ConfigurationManager.GetSection("Enkoni.DataAnnotations") as ValidationSection;

      Assert.IsNotNull(validationSection);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators);
      Assert.AreEqual(2, validationSection.DutchPhoneNumberValidators.Count);
      Assert.IsNotNull(validationSection.EmailValidators);
      Assert.AreEqual(2, validationSection.EmailValidators.Count);
    }
    #endregion

    #region DutchPhoneNumberValidationConfiguration TestCases
    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidationConfigElement"/> class.</summary>
    [TestMethod]
    public void TestCase02_ReadDutchPhoneNumberValidationConfiguration_Default() {
      ValidationSection validationSection = ConfigurationManager.GetSection("Enkoni.DataAnnotations.TestCase02") as ValidationSection;

      Assert.IsNotNull(validationSection);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators);
      Assert.AreEqual(1, validationSection.DutchPhoneNumberValidators.Count);

      Assert.IsFalse(validationSection.DutchPhoneNumberValidators[string.Empty].AllowCountryCallingCode);
      Assert.IsTrue(validationSection.DutchPhoneNumberValidators[string.Empty].AllowCarrierPreselect);

      Assert.AreEqual(143, validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes.Count);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["058"]);
      Assert.AreEqual("058", validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["058"].AreaCode);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["0123"]);
      Assert.AreEqual("0123", validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["0123"].AreaCode);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidationConfigElement"/> class.</summary>
    [TestMethod]
    public void TestCase03_ReadDutchPhoneNumberValidationConfiguration_ClearAreaCodes() {
      ValidationSection validationSection = ConfigurationManager.GetSection("Enkoni.DataAnnotations.TestCase03") as ValidationSection;

      Assert.IsNotNull(validationSection);
      Assert.AreEqual(1, validationSection.DutchPhoneNumberValidators.Count);

      Assert.IsTrue(validationSection.DutchPhoneNumberValidators[string.Empty].AllowCountryCallingCode);
      Assert.IsFalse(validationSection.DutchPhoneNumberValidators[string.Empty].AllowCarrierPreselect);

      Assert.AreEqual(1, validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes.Count);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["0123"]);
      Assert.AreEqual("0123", validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["0123"].AreaCode);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidationConfigElement"/> class.</summary>
    [TestMethod]
    public void TestCase04_ReadDutchPhoneNumberValidationConfiguration_RemoveAreaCode() {
      ValidationSection validationSection = ConfigurationManager.GetSection("Enkoni.DataAnnotations.TestCase04") as ValidationSection;

      Assert.IsNotNull(validationSection);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators[string.Empty]);

      Assert.IsFalse(validationSection.DutchPhoneNumberValidators[string.Empty].AllowCountryCallingCode);
      Assert.IsFalse(validationSection.DutchPhoneNumberValidators[string.Empty].AllowCarrierPreselect);

      Assert.AreEqual(141, validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes.Count);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["010"]);
      Assert.AreEqual("010", validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["010"].AreaCode);
      Assert.IsNull(validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["058"]);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidationConfigElement"/> class.</summary>
    [TestMethod]
    public void TestCase05_ReadDutchPhoneNumberValidationConfiguration_MultipleValidators() {
      ValidationSection validationSection = ConfigurationManager.GetSection("Enkoni.DataAnnotations.TestCase05") as ValidationSection;

      Assert.IsNotNull(validationSection);
      Assert.AreEqual(2, validationSection.DutchPhoneNumberValidators.Count);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators[string.Empty]);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators["TestValidator"]);

      Assert.IsFalse(validationSection.DutchPhoneNumberValidators[string.Empty].AllowCountryCallingCode);
      Assert.IsFalse(validationSection.DutchPhoneNumberValidators[string.Empty].AllowCarrierPreselect);
      Assert.IsTrue(validationSection.DutchPhoneNumberValidators["TestValidator"].AllowCountryCallingCode);
      Assert.IsTrue(validationSection.DutchPhoneNumberValidators["TestValidator"].AllowCarrierPreselect);

      Assert.AreEqual(141, validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes.Count);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["010"]);
      Assert.AreEqual("010", validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["010"].AreaCode);
      Assert.IsNull(validationSection.DutchPhoneNumberValidators[string.Empty].AreaCodes["058"]);

      Assert.AreEqual(1, validationSection.DutchPhoneNumberValidators["TestValidator"].AreaCodes.Count);
      Assert.IsNotNull(validationSection.DutchPhoneNumberValidators["TestValidator"].AreaCodes["0123"]);
      Assert.AreEqual("0123", validationSection.DutchPhoneNumberValidators["TestValidator"].AreaCodes["0123"].AreaCode);
    }
    #endregion

    #region EmailValidationConfiguration TestCases
    /// <summary>Tests the functionality of the <see cref="EmailValidationConfigElement"/> class.</summary>
    [TestMethod]
    public void TestCase06_ReadEmailValidationConfiguration_Default() {
      ValidationSection validationSection = ConfigurationManager.GetSection("Enkoni.DataAnnotations.TestCase06") as ValidationSection;

      Assert.IsNotNull(validationSection);
      Assert.IsNotNull(validationSection.EmailValidators);
      Assert.AreEqual(2, validationSection.EmailValidators.Count);

      Assert.IsFalse(validationSection.EmailValidators[string.Empty].AllowComments);
      Assert.IsFalse(validationSection.EmailValidators[string.Empty].AllowIPAddresses);
      Assert.IsFalse(validationSection.EmailValidators[string.Empty].RequireTopLevelDomain);

      Assert.AreEqual(0, validationSection.EmailValidators[string.Empty].IncludeDomains.Count);
      Assert.AreEqual(0, validationSection.EmailValidators[string.Empty].ExcludeDomains.Count);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidationConfigElement"/> class.</summary>
    [TestMethod]
    public void TestCase07_ReadEmailValidationConfiguration_IncludeExcludeDomains() {
      ValidationSection validationSection = ConfigurationManager.GetSection("Enkoni.DataAnnotations.TestCase07") as ValidationSection;

      Assert.IsNotNull(validationSection);
      Assert.AreEqual(1, validationSection.EmailValidators.Count);

      Assert.IsFalse(validationSection.EmailValidators[string.Empty].AllowComments);
      Assert.IsTrue(validationSection.EmailValidators[string.Empty].AllowIPAddresses);
      Assert.IsFalse(validationSection.EmailValidators[string.Empty].RequireTopLevelDomain);

      Assert.AreEqual(2, validationSection.EmailValidators[string.Empty].IncludeDomains.Count);
      Assert.IsNotNull(validationSection.EmailValidators[string.Empty].IncludeDomains["microsoft.com"]);
      Assert.AreEqual("microsoft.com", validationSection.EmailValidators[string.Empty].IncludeDomains["microsoft.com"].Pattern);
      Assert.AreEqual(2, validationSection.EmailValidators[string.Empty].ExcludeDomains.Count);
      Assert.IsNotNull(validationSection.EmailValidators[string.Empty].ExcludeDomains["*yahoo.com"]);
      Assert.AreEqual("*yahoo.com", validationSection.EmailValidators[string.Empty].ExcludeDomains["*yahoo.com"].Pattern);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidationConfigElement"/> class.</summary>
    [TestMethod]
    public void TestCase08_ReadEmailValidationConfiguration_MultipleValidators() {
      ValidationSection validationSection = ConfigurationManager.GetSection("Enkoni.DataAnnotations.TestCase08") as ValidationSection;

      Assert.IsNotNull(validationSection);
      Assert.AreEqual(2, validationSection.EmailValidators.Count);
      Assert.IsNotNull(validationSection.EmailValidators[string.Empty]);
      Assert.IsNotNull(validationSection.EmailValidators["TestValidator"]);

      Assert.IsFalse(validationSection.EmailValidators[string.Empty].AllowComments);
      Assert.IsFalse(validationSection.EmailValidators[string.Empty].AllowIPAddresses);
      Assert.IsFalse(validationSection.EmailValidators[string.Empty].RequireTopLevelDomain);
      Assert.IsTrue(validationSection.EmailValidators["TestValidator"].AllowComments);
      Assert.IsTrue(validationSection.EmailValidators["TestValidator"].AllowIPAddresses);
      Assert.IsTrue(validationSection.EmailValidators["TestValidator"].RequireTopLevelDomain);

      Assert.AreEqual(1, validationSection.EmailValidators[string.Empty].IncludeDomains.Count);
      Assert.AreEqual(0, validationSection.EmailValidators[string.Empty].ExcludeDomains.Count);
      Assert.IsNotNull(validationSection.EmailValidators[string.Empty].IncludeDomains["gmail.com"]);
      Assert.AreEqual("gmail.com", validationSection.EmailValidators[string.Empty].IncludeDomains["gmail.com"].Pattern);
      Assert.IsNull(validationSection.EmailValidators[string.Empty].IncludeDomains["yahoo.com"]);

      Assert.AreEqual(0, validationSection.EmailValidators["TestValidator"].IncludeDomains.Count);
      Assert.AreEqual(1, validationSection.EmailValidators["TestValidator"].ExcludeDomains.Count);
      Assert.IsNotNull(validationSection.EmailValidators["TestValidator"].ExcludeDomains["yahoo.com"]);
      Assert.AreEqual("yahoo.com", validationSection.EmailValidators["TestValidator"].ExcludeDomains["yahoo.com"].Pattern);
    }
    #endregion
  }
}
