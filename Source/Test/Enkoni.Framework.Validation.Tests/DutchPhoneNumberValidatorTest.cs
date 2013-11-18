//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DutchPhoneNumberValidatorTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the DutchPhoneNumberValidator class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

using Enkoni.Framework.Validation.Validators;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntLib = Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Validation.Tests {
  /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> and <see cref="DutchPhoneNumberValidator"/> classes.</summary>
  [TestClass]
  public class DutchPhoneNumberValidatorTest {
    #region Properties
    /// <summary>Gets or sets the context that gives access to the input data for the test cases.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region TestCases
    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Validation.Tests\TestData\TestData.mdf", @"DutchPhoneNumberValidatorTest\TestCase01")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|DutchPhoneNumberValidatorTest\TestCase01\TestData.mdf;Integrated Security=True;Connect Timeout=30", "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void TestCase01_Regular() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void TestCase02_Regular_IncludeAreaCodes() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, IncludeAreaCodes = "012;013" };
      string input = "+31122151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "0031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void TestCase03_Regular_ExcludeAreaCodes() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = true, ExcludeAreaCodes = "058;020", IncludeAreaCodes = null };
      string input = "+31502151740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "0031582151740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Validation.Tests\TestData\TestData.mdf", @"DutchPhoneNumberValidatorTest\TestCase04")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|DutchPhoneNumberValidatorTest\TestCase04\TestData.mdf;Integrated Security=True;Connect Timeout=30", "RegularPhoneNumber", DataAccessMethod.Sequential)]
    public void TestCase04_Regular_NoCountryCode() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Regular, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void TestCase05_Regular_NoCountryCode_IncludeAreaCodes() {
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
    public void TestCase06_Regular_NoCountryCode_ExcludeAreaCodes() {
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
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Validation.Tests\TestData\TestData.mdf", @"DutchPhoneNumberValidatorTest\TestCase07")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|DutchPhoneNumberValidatorTest\TestCase07\TestData.mdf;Integrated Security=True;Connect Timeout=30", "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void TestCase07_Mobile() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = true, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Validation.Tests\TestData\TestData.mdf", @"DutchPhoneNumberValidatorTest\TestCase08")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|DutchPhoneNumberValidatorTest\TestCase08\TestData.mdf;Integrated Security=True;Connect Timeout=30", "MobilePhoneNumber", DataAccessMethod.Sequential)]
    public void TestCase08_Mobile_NoCountryCode() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !Convert.ToBoolean(this.TestContext.DataRow["ContainsCountryAccessCode"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Mobile, AllowCountryCallingCode = false, IncludeAreaCodes = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    public void TestCase09_Emergency_Valid() {
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
    public void TestCase10_Emergency_Invalid() {
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
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Validation.Tests\TestData\TestData.mdf", @"DutchPhoneNumberValidatorTest\TestCase11")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|DutchPhoneNumberValidatorTest\TestCase11\TestData.mdf;Integrated Security=True;Connect Timeout=30", "ServicePhoneNumber", DataAccessMethod.Sequential)]
    public void TestCase11_Service() {
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
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Validation.Tests\TestData\TestData.mdf", @"DutchPhoneNumberValidatorTest\TestCase12")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|DutchPhoneNumberValidatorTest\TestCase12\TestData.mdf;Integrated Security=True;Connect Timeout=30", "OtherPhoneNumber", DataAccessMethod.Sequential)]
    public void TestCase12_Other() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false) { Categories = PhoneNumberCategories.Other, AllowCountryCallingCode = true };
      string input = this.TestContext.DataRow["PhoneNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid);

      testSubject = new DutchPhoneNumberValidator("message {0}", "tag", true) { Categories = PhoneNumberCategories.Other, AllowCountryCallingCode = true };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidator"/> class.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Validation.Tests\TestData\TestData.mdf", @"DutchPhoneNumberValidatorTest\TestCase13")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|DutchPhoneNumberValidatorTest\TestCase13\TestData.mdf;Integrated Security=True;Connect Timeout=30", "DefaultPhoneNumber", DataAccessMethod.Sequential)]
    public void TestCase13_Default() {
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
    public void TestCase14_Configuration() {
      DutchPhoneNumberValidator testSubject = new DutchPhoneNumberValidator("message {0}", "tag", false);
      string input = "+31582141740";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid, input);

      input = "+31202141740";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid, input);

      input = "+31519214174";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase15_Attribute_AllDefaults() {
      TestDummy_AllDefault dummy = new TestDummy_AllDefault { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_AllDefault>(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "08001254";
      results = EntLib.Validation.Validate<TestDummy_AllDefault>(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase16_Attribute_CustomCategory() {
      TestDummy_CustomCategory dummy = new TestDummy_CustomCategory { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_CustomCategory>(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "0616070302";
      results = EntLib.Validation.Validate<TestDummy_CustomCategory>(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase17_Attribute_CustomCategory() {
      TestDummy_CustomCategory dummy = new TestDummy_CustomCategory { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_CustomCategory>(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "0616070302";
      results = EntLib.Validation.Validate<TestDummy_CustomCategory>(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase18_Attribute_IncludeAreaCodes() {
      TestDummy_IncludeAreaCodes dummy = new TestDummy_IncludeAreaCodes { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_IncludeAreaCodes>(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "0502348547";
      results = EntLib.Validation.Validate<TestDummy_IncludeAreaCodes>(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="DutchPhoneNumberValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase19_Attribute_ExcludeAreaCodes() {
      TestDummy_ExcludeAreaCodes dummy = new TestDummy_ExcludeAreaCodes { PhoneNumber = "0582151740" };

      EntLib.ValidationResults results = EntLib.Validation.Validate<TestDummy_ExcludeAreaCodes>(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.PhoneNumber = "0202348547";
      results = EntLib.Validation.Validate<TestDummy_ExcludeAreaCodes>(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }
    #endregion
  }

  #region Helper classes
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
  #endregion
}
