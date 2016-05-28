using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.DataAnnotations.Tests {
  /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
  [TestClass]
  [DeploymentItem("Enkoni.Framework.DataAnnotations.Tests.Database.dacpac")]
  public class DutchPhoneNumberAttributeTest {
    #region Properties
    /// <summary>Gets or sets the context that gives access to the input data for the test cases.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region TestCases
    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Regular() {
      TestDummy_Regular_OverrideAll dummy = new TestDummy_Regular_OverrideAll { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Regular_IncludeAreaCodes() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, IncludeAreaCodes = "012;013" };
      string input = "+31122151740";
      bool result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "16420031122151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "0031582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Regular_ExcludeAreaCodes() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, ExcludeAreaCodes = "058;020", IncludeAreaCodes = null };
      string input = "+31502151740";
      bool result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "+31582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "164231502151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "0031582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Regular_NoCountryCode() {
      TestDummy_Regular_NoCountryCode dummy = new TestDummy_Regular_NoCountryCode { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Regular_NoCountryCode_IncludeAreaCodes() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Regular, IncludeAreaCodes = "012;013", AllowCountryCallingCode = false };
      string input = "+31122151740";
      bool result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "0122151740";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "0031582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "0582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Regular_NoCountryCode_ExcludeAreaCodes() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Regular, ExcludeAreaCodes = "058;020", AllowCountryCallingCode = false, IncludeAreaCodes = null };
      string input = "+31502151740";
      bool result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "0502151740";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "0031582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "0582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Regular_WithCarrierPreselect() {
      TestDummy_Regular_WithCarrierPreselect dummy = new TestDummy_Regular_WithCarrierPreselect { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Regular_IncludeAreaCodes_WithCarrierPreselect() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, AllowCarrierPreselect = true, IncludeAreaCodes = "012;013" };
      string input = "+31122151740";
      bool result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "16420031122151740";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "16420031582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Regular_ExcludeAreaCodes_WithCarrierPreselect() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, AllowCarrierPreselect = true, ExcludeAreaCodes = "058;020", IncludeAreaCodes = null };
      string input = "+31502151740";
      bool result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "16420031502151740";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "16420031582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Regular_NoCountryCode_WithCarrierPreselect() {
      TestDummy_Regular_NoCountryCode_WithCarrierPreselect dummy = new TestDummy_Regular_NoCountryCode_WithCarrierPreselect { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Regular_NoCountryCode_IncludeAreaCodes_WithCarrierPreselect() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Regular, IncludeAreaCodes = "012;013", AllowCountryCallingCode = false, AllowCarrierPreselect = true };
      string input = "+31122151740";
      bool result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "16420122151740";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "105880031582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "109420582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Regular_NoCountryCode_ExcludeAreaCodes_WithCarrierPreselect() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Regular, ExcludeAreaCodes = "058;020", AllowCountryCallingCode = false, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      string input = "+31502151740";
      bool result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "16000031582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "16990582151740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Mobile() {
      TestDummy_Mobile_OverrideAll dummy = new TestDummy_Mobile_OverrideAll { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Mobile_NoCountryCode() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);
      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Mobile_WithCarrierPreselect() {
      TestDummy_Mobile_WithCarrierPreselect dummy = new TestDummy_Mobile_WithCarrierPreselect { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Mobile_NoCountryCode_WithCarrierPreselect() {
      TestDummy_Mobile_NoCountryCode_WithCarrierPreselect dummy = new TestDummy_Mobile_NoCountryCode_WithCarrierPreselect { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();
      
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Emergency_Valid() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Emergency };
      bool result = testSubject.IsValid("112");
      Assert.IsTrue(result);

      result = testSubject.IsValid("144");
      Assert.IsTrue(result);

      result = testSubject.IsValid("116123");
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Emergency_Invalid() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute { Categories = PhoneNumberCategories.Emergency };
      bool result = testSubject.IsValid("113");
      Assert.IsFalse(result);

      result = testSubject.IsValid("1a4");
      Assert.IsFalse(result);

      result = testSubject.IsValid("11613");
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ServicePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Service() {
      TestDummy_Service_OverrideAll dummy = new TestDummy_Service_OverrideAll { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "OtherPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Other() {
      TestDummy_Other_OverrideAll dummy = new TestDummy_Other_OverrideAll { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "OtherPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Other_WithCarrierPreselect() {
      TestDummy_Other_WithCarrierPreselect dummy = new TestDummy_Other_WithCarrierPreselect { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "DefaultPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberAttribute_Default() {
      TestDummy_Default_OverrideAll dummy = new TestDummy_Default_OverrideAll { PhoneNumber = this.TestContext.DataRow["PhoneNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.PhoneNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.PhoneNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.PhoneNumber);
        Assert.AreEqual("The property PhoneNumber is not a valid Dutch phone number.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Configuration() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute();
      string input = "+31582141740";
      bool result = testSubject.IsValid(input);
      Assert.IsTrue(result, input);

      input = "1688582141740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result, input);

      input = "+31202141740";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result, input);

      input = "+31519214174";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Configuration_NamedValidator() {
      DutchPhoneNumberAttribute testSubject = new DutchPhoneNumberAttribute("Custom Validator");
      string input = "+31582141740";
      bool result = testSubject.IsValid(input);
      Assert.IsFalse(result, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberAttribute_Attribute_AllDefaults() {
      TestDummy_AllDefault dummy = new TestDummy_AllDefault { PhoneNumber = "0582151740" };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);
      Assert.IsTrue(result);

      dummy.PhoneNumber = "102360582151740";
      result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);
      Assert.IsFalse(result);

      dummy.PhoneNumber = "08001254";
      result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);
      Assert.IsFalse(result);
    }
    #endregion

    #region Inner test classes
    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Regular_OverrideAll {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Regular, AllowCarrierPreselect = false, AllowCountryCallingCode = true, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Regular_NoCountryCode {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Regular, AllowCarrierPreselect = false, AllowCountryCallingCode = false, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Regular_WithCarrierPreselect {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Regular, AllowCarrierPreselect = true, AllowCountryCallingCode = true, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Regular_NoCountryCode_WithCarrierPreselect {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Regular, AllowCarrierPreselect = true, AllowCountryCallingCode = false, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Mobile_OverrideAll {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Mobile, AllowCarrierPreselect = false, AllowCountryCallingCode = true, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Mobile_WithCarrierPreselect {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Mobile, AllowCarrierPreselect = true, AllowCountryCallingCode = true, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Mobile_NoCountryCode_WithCarrierPreselect {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Mobile, AllowCarrierPreselect = true, AllowCountryCallingCode = false, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Service_OverrideAll {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Service, AllowCarrierPreselect = false, AllowCountryCallingCode = true, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Other_OverrideAll {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Other, AllowCarrierPreselect = false, AllowCountryCallingCode = true, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Other_WithCarrierPreselect {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Other, AllowCarrierPreselect = true, AllowCountryCallingCode = true, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Default_OverrideAll {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber(Categories = PhoneNumberCategories.Default, AllowCarrierPreselect = false, AllowCountryCallingCode = true, IncludeAreaCodes = null, ErrorMessage = "The property {0} is not a valid Dutch phone number.")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_AllDefault {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumber]
      public string PhoneNumber { get; set; }
    }
    #endregion
  }
}
