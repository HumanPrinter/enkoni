//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailValidatorTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the EmailValidator class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

using Enkoni.Framework.Validation.Validators;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntLib = Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Validation.Tests {
  /// <summary>Tests the functionality of the <see cref="EmailValidator"/> and <see cref="EmailValidatorAttribute"/> classes.
  /// </summary>
  [TestClass]
  public class EmailValidatorTest {
    #region Properties
    /// <summary>Gets or sets the context that gives access to the input data for the test cases.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region TestCases
    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    [DeploymentItem(@"..\..\TestData\TestData.mdf", @"EmailValidatorTest\TestCase01")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|EmailValidatorTest\TestCase01\TestData.mdf;Integrated Security=True;Connect Timeout=30", "BasicEmail", DataAccessMethod.Sequential)]
    public void TestCase01_Basic() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComment = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool containsIPAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = true, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    [DeploymentItem(@"..\..\TestData\TestData.mdf", @"EmailValidatorTest\TestCase02")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|EmailValidatorTest\TestCase02\TestData.mdf;Integrated Security=True;Connect Timeout=30", "ExtendedEmail", DataAccessMethod.Sequential)]
    public void TestCase02_Extended() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComment = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool containsIPAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = true, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    [DeploymentItem(@"..\..\TestData\TestData.mdf", @"EmailValidatorTest\TestCase03")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|EmailValidatorTest\TestCase03\TestData.mdf;Integrated Security=True;Connect Timeout=30", "CompleteEmail", DataAccessMethod.Sequential)]
    public void TestCase03_Complete() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComment = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool containsIPAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = true, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    public void TestCase04_IncludeDomains() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = false, IncludeDomains = "*good.o?g;righthere.com", ExcludeDomains = null };
      string input = "local@good.orrg";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "local@good.org";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "local@sub.good.org";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "local@righthere.com";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "local@sub.righthere.com";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    public void TestCase05_ExcludeDomains() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = false, IncludeDomains = null, ExcludeDomains = "*evil.o?g;nowhere.com" };
      string input = "local@devil.orrg";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "local@devil.org";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "local@sub.evil.org";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "local@nowhere.com";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "local@sub.nowhere.com";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    public void TestCase06_IncludeExcludeDomains() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { AllowComments = false, AllowIPAddresses = true, IncludeDomains = "all.*.good.org", ExcludeDomains = "all.evil.good.org" };
      string input = "user@all.foobar.good.org";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "user@all.evil.good.org";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      testSubject = new EmailValidator("message {0}", "tag", false) { AllowComments = false, AllowIPAddresses = true, IncludeDomains = "all.good.org", ExcludeDomains = "all.good.org" };
      input = "user@all.good.org";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    public void TestCase07_IPFilter() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { AllowComments = false, AllowIPAddresses= true, IncludeDomains = "192.168.10.23;10.12.*", ExcludeDomains = "36.45.12.63;10.12.9.*" };
      string input = "user@[17.45.26.95]";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "user@[192.168.10.23]";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "user@[36.45.12.63]";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      input = "user@[10.12.45.125]";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "user@[10.12.9.152]";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    public void TestCase08_Configuration() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false);
      string input = "user@somewhere.co.uk";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid, input);

      input = "user@sub.baddomain.org";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid, input);

      input = "user@[12.124.63.5]";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid, input);

      input = "user(my comment)@somewhere.co.uk";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    public void TestCase09_Configuration_NamedValidator() {
      EmailValidator testSubject = new EmailValidator("Custom Validator", "message {0}", "tag", false);
      string input = "user@somewhere.co.uk";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid, input);

      input = "user@[127.251.42.88]";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid, input);

      input = "user(my comment)@[127.251.42.88]";
      results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase10_Attribute_AllDefaults() {
      TestDummy_AllDefault dummy = new TestDummy_AllDefault { MailAddress = "user@somewhere.co.uk" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@sub.baddomain.org";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user@[12.124.63.5]";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user(my comment)@somewhere.co.uk";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user!name@somewhere.co.uk";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase11_Attribute_CustomCategory() {
      TestDummy_CustomCategory dummy = new TestDummy_CustomCategory { MailAddress = "user@somewhere.co.uk" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@sub.baddomain.org";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user@[12.124.63.5]";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user(my comment)@somewhere.co.uk";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user!name@somewhere.co.uk";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase12_Attribute_IncludeDomains() {
      TestDummy_IncludeDomains dummy = new TestDummy_IncludeDomains { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@baddomain.org";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase13_Attribute_ExcludeDomains() {
      TestDummy_ExcludeDomains dummy = new TestDummy_ExcludeDomains { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@baddomain.org";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase14_Attribute_IncludeExcludeDomains() {
      TestDummy_IncludeExcludeDomains dummy = new TestDummy_IncludeExcludeDomains { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@betterdomain.com";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase15_Attribute_AllowComments() {
      TestDummy_AllowComments dummy = new TestDummy_AllowComments { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@betterdomain.com(#1st comment)";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase16_Attribute_AllowIPAddresses() {
      TestDummy_AllowIPAddresses dummy = new TestDummy_AllowIPAddresses { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@[125.243.78.96]";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void TestCase17_Attribute_NamedValidator() {
      TestDummy_NamedValidator dummy = new TestDummy_NamedValidator { MailAddress = "user@domain.com" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user@[127.251.123.42]";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }
    #endregion

    #region Inner test classes
    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_AllDefault {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_CustomCategory {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(Category = EmailCategory.Extended, Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_IncludeDomains {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(IncludeDomains = "gooddomain.org;betterdomain.com", Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_ExcludeDomains {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(ExcludeDomains = "baddomain.org;evildomain.com", Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_IncludeExcludeDomains {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(IncludeDomains = "gooddomain.org;betterdomain.com", ExcludeDomains = "baddomain.org;betterdomain.com", Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_AllowComments {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(AllowComments = true, Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_AllowIPAddresses {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(AllowIPAddresses = true, Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_NamedValidator {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(Name = "Custom Validator", Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
      public string MailAddress { get; set; }
    }
    #endregion
  }
}
