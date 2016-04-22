using System;

using Enkoni.Framework.Validation.Validators;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntLib = Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Validation.Tests {
  /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> and <see cref="DutchPhoneNumberValidatorAttribute"/> classes.
  /// </summary>
  [TestClass]
  [DeploymentItem("Enkoni.Framework.Validation.Tests.Database.dacpac")]
  public class DutchPhoneNumberValidatorTest {
    #region Properties
    /// <summary>Gets or sets the context that gives access to the input data for the test cases.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region TestCases
    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Regular() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Regular_IncludeAreaCodes() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, IncludeAreaCodes = "012;013" };
      string input = "+31122151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "16420031122151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "0031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Regular_ExcludeAreaCodes() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, ExcludeAreaCodes = "058;020", IncludeAreaCodes = null };
      string input = "+31502151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "+31582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "164231502151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "0031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Regular_NoCountryCode() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Regular_NoCountryCode_IncludeAreaCodes() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, IncludeAreaCodes = "012;013", AllowCountryCallingCode = false };
      string input = "+31122151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "0122151740";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "0031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "0582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Regular_NoCountryCode_ExcludeAreaCodes() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, ExcludeAreaCodes = "058;020", AllowCountryCallingCode = false, IncludeAreaCodes = null };
      string input = "+31502151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "0502151740";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "0031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "0582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Regular_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Regular_IncludeAreaCodes_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, AllowCarrierPreselect = true, IncludeAreaCodes = "012;013" };
      string input = "+31122151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "16420031122151740";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "16420031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Regular_ExcludeAreaCodes_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, AllowCarrierPreselect = true, ExcludeAreaCodes = "058;020", IncludeAreaCodes = null };
      string input = "+31502151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "16420031502151740";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "16420031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Regular_NoCountryCode_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = false, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = false, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Regular_NoCountryCode_IncludeAreaCodes_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, IncludeAreaCodes = "012;013", AllowCountryCallingCode = false, AllowCarrierPreselect = true };
      string input = "+31122151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "16420122151740";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "105880031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "109420582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Regular_NoCountryCode_ExcludeAreaCodes_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, ExcludeAreaCodes = "058;020", AllowCountryCallingCode = false, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      string input = "+31502151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "16000031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "16990582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Mobile() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Mobile_NoCountryCode() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Mobile_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = true, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = true, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Mobile_NoCountryCode_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = false, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = false, AllowCarrierPreselect = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Emergency_Valid() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Emergency };
      EntLib.ValidationResults results = testSubject.Validate("112");
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate("144");
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate("116123");
      Assert.IsTrue(results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Emergency };
      results = testSubject.Validate("112");
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate("144");
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate("116123");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Emergency_Invalid() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Emergency };
      EntLib.ValidationResults results = testSubject.Validate("113");
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate("1a4");
      Assert.IsFalse(results.IsValid);

      results = testSubject.Validate("11613");
      Assert.IsFalse(results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Emergency };
      results = testSubject.Validate("113");
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate("1a4");
      Assert.IsTrue(results.IsValid);

      results = testSubject.Validate("11613");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ServicePhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Service() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Service };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Service };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "OtherPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Other() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Other, AllowCountryCallingCode = true };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool containsCarrierPreselect = Convert.ToBoolean(this.TestContext.DataRow["ContainsCarrierPreselect"]);
      bool expected = !containsCarrierPreselect && Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Other, AllowCountryCallingCode = true };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "OtherPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Other_WithCarrierPreselect() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Other, AllowCountryCallingCode = true, AllowCarrierPreselect = true };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Other, AllowCountryCallingCode = true, AllowCarrierPreselect = true };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "DefaultPhoneNumber", DataAccessMethod.Sequential)]
    public void DutchPhoneNumberValidator_Default() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Default, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Default, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Configuration() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false);
      string input = "+31582141740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid, input);

      input = "1688582141740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid, input);

      input = "+31202141740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid, input);

      input = "+31519214174";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Configuration_NamedValidator() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("Custom Validator", "message {0}", "tag", false);
      string input = "+31582141740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Attribute_AllDefaults() {
      TestDummy_AllDefault dummy = new TestDummy_AllDefault { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "102360582151740";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.PhoneNumber = "08001254";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Attribute_CustomCategory() {
      TestDummy_CustomCategory dummy = new TestDummy_CustomCategory { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "0616070302";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Attribute_IncludeAreaCodes() {
      TestDummy_IncludeAreaCodes dummy = new TestDummy_IncludeAreaCodes { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "0502348547";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Attribute_ExcludeAreaCodes() {
      TestDummy_ExcludeAreaCodes dummy = new TestDummy_ExcludeAreaCodes { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "0202348547";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Attribute_AllowCarrierPreselect() {
      TestDummy_AllowCarrierPreselect dummy = new TestDummy_AllowCarrierPreselect { PhoneNumber = "105630582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void DutchPhoneNumberValidator_Attribute_NamedValidator() {
      TestDummy_NamedValidator dummy = new TestDummy_NamedValidator { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }
    #endregion

    #region Inner test classes
    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_AllDefault {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumberValidator(Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid Dutch phone number ('{0}').")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_CustomCategory {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumberValidator(Categories = PhoneNumberCategories.Regular, Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid Dutch phone number ('{0}').")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_IncludeAreaCodes {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumberValidator(IncludeAreaCodes = "010;020;058", Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid Dutch phone number ('{0}').")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_ExcludeAreaCodes {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumberValidator(ExcludeAreaCodes = "020;0519", Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid Dutch phone number ('{0}').")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_AllowCarrierPreselect {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumberValidator(AllowCarrierPreselect = true, Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid Dutch phone number ('{0}').")]
      public string PhoneNumber { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_NamedValidator {
      /// <summary>Gets or sets a phone number.</summary>
      [DutchPhoneNumberValidator(Name = "Custom Validator", Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid Dutch phone number ('{0}').")]
      public string PhoneNumber { get; set; }
    }
    #endregion
  }
}
