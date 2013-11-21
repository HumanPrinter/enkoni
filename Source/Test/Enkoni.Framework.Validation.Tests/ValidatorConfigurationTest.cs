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
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidator);

      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidator.AllowCountryCallingCode);
      Assert.IsTrue(validatorsSection.DutchPhoneNumberValidator.AllowCarrierPreselect);

      Assert.AreEqual(143, validatorsSection.DutchPhoneNumberValidator.AreaCodes.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidator.AreaCodes["058"]);
      Assert.AreEqual("058", validatorsSection.DutchPhoneNumberValidator.AreaCodes["058"].AreaCode);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidator.AreaCodes["0123"]);
      Assert.AreEqual("0123", validatorsSection.DutchPhoneNumberValidator.AreaCodes["0123"].AreaCode);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class using values that should easily pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase02_ReadPhoneNumberValidatorConfiguration_ClearAreaCodes() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase02") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidator);

      Assert.IsTrue(validatorsSection.DutchPhoneNumberValidator.AllowCountryCallingCode);
      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidator.AllowCarrierPreselect);

      Assert.AreEqual(1, validatorsSection.DutchPhoneNumberValidator.AreaCodes.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidator.AreaCodes["0123"]);
      Assert.AreEqual("0123", validatorsSection.DutchPhoneNumberValidator.AreaCodes["0123"].AreaCode);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class using values that should easily pass the validation.
    /// </summary>
    [TestMethod]
    public void TestCase03_ReadPhoneNumberValidatorConfiguration_RemoveAreaCode() {
      Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
      ValidatorsSection validatorsSection = configuration.GetSection("Enkoni.Validators.TestCase03") as ValidatorsSection;

      Assert.IsNotNull(validatorsSection);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidator);

      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidator.AllowCountryCallingCode);
      Assert.IsFalse(validatorsSection.DutchPhoneNumberValidator.AllowCarrierPreselect);

      Assert.AreEqual(141, validatorsSection.DutchPhoneNumberValidator.AreaCodes.Count);
      Assert.IsNotNull(validatorsSection.DutchPhoneNumberValidator.AreaCodes["010"]);
      Assert.AreEqual("010", validatorsSection.DutchPhoneNumberValidator.AreaCodes["010"].AreaCode);
      Assert.IsNull(validatorsSection.DutchPhoneNumberValidator.AreaCodes["058"]);
    }
    #endregion
  }
}
