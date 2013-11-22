//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorConfigurationTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the validator configuration classes.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Configuration;

using Enkoni.Framework.Validation.Validators;
using Enkoni.Framework.Validation.Validators.Configuration;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Validation.Tests {
  /// <summary>Tests the functionality of the validator configuration classes.</summary>
  [TestClass]
  public class ValidatorConfigurationTest {
    #region TestCases
    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class using values that should easily pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase01_ReadPhoneNumberValidatorConfiguration_Default() {
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

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class using values that should easily pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase02_ReadPhoneNumberValidatorConfiguration_ClearAreaCodes() {
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

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class using values that should easily pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase03_ReadPhoneNumberValidatorConfiguration_RemoveAreaCode() {
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

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class using values that should easily pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase04_ReadPhoneNumberValidatorConfiguration_MultipleValidators() {
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
  }
}
