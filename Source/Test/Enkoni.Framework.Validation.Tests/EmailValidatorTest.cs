using System;

using Enkoni.Framework.Validation.Validators;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntLib = Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Validation.Tests {
  /// <summary>Tests the functionality of the <see cref="EmailValidator"/> and <see cref="EmailValidatorAttribute"/> classes.
  /// </summary>
  [TestClass]
  [DeploymentItem("Enkoni.Framework.Validation.Tests.Database.dacpac")]
  public class EmailValidatorTest {
    #region Properties
    /// <summary>Gets or sets the context that gives access to the input data for the test cases.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region TestCases
    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "BasicEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Basic() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "BasicEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Basic_AllowComments() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComments = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool isValid = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      bool expected = isValid && !containsComments;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!isValid || containsComments, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "BasicEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Basic_AllowIPAddresses() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsIpAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !containsIpAddress;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "BasicEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Basic_RequireTopLevelDomain() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsTopLevelDomain = Convert.ToBoolean(this.TestContext.DataRow["ContainsTopLevelDomain"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && containsTopLevelDomain;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ExtendedEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Extended() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ExtendedEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Extended_AllowComments() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Extended, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComment = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !containsComment;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Extended, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ExtendedEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Extended_AllowIPAddresses() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsIpAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !containsIpAddress;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ExtendedEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Extended_RequireTopLevelDomain() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsTopLevelDomain = Convert.ToBoolean(this.TestContext.DataRow["ContainsTopLevelDomain"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && containsTopLevelDomain;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "CompleteEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Complete() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "CompleteEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Complete_AllowComments() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Complete, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComment = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !containsComment;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Complete, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "CompleteEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Complete_AllowIPAddresses() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsIpAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !containsIpAddress;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod, TestCategory("LocalOnly")]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "CompleteEmail", DataAccessMethod.Sequential)]
    public void EmailValidator_Complete_RequireTopLevelDomain() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsTopLevelDomain = Convert.ToBoolean(this.TestContext.DataRow["ContainsTopLevelDomain"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && containsTopLevelDomain;
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new EmailValidator("message {0}", "tag", true) { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    public void EmailValidator_IncludeDomains() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = "*good.o?g;righthere.com", ExcludeDomains = null };
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
    public void EmailValidator_ExcludeDomains() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = "*evil.o?g;nowhere.com" };
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
    public void EmailValidator_IncludeExcludeDomains() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = "all.*.good.org", ExcludeDomains = "all.evil.good.org" };
      string input = "user@all.foobar.good.org";
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.IsTrue(results.IsValid);

      input = "user@all.evil.good.org";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);

      testSubject = new EmailValidator("message {0}", "tag", false) { AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = "all.good.org", ExcludeDomains = "all.good.org" };
      input = "user@all.good.org";
      results = testSubject.Validate(input);
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidator"/> class.</summary>
    [TestMethod]
    public void EmailValidator_IPFilter() {
      EmailValidator testSubject = new EmailValidator("message {0}", "tag", false) { AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = "192.168.10.23;10.12.*", ExcludeDomains = "36.45.12.63;10.12.9.*" };
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
    public void EmailValidator_Configuration() {
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
    public void EmailValidator_Configuration_NamedValidator() {
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
    public void EmailValidator_Attribute_AllDefaults() {
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
    public void EmailValidator_Attribute_CustomCategory() {
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
    public void EmailValidator_Attribute_IncludeDomains() {
      TestDummy_IncludeDomains dummy = new TestDummy_IncludeDomains { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@baddomain.org";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void EmailValidator_Attribute_ExcludeDomains() {
      TestDummy_ExcludeDomains dummy = new TestDummy_ExcludeDomains { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@baddomain.org";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void EmailValidator_Attribute_IncludeExcludeDomains() {
      TestDummy_IncludeExcludeDomains dummy = new TestDummy_IncludeExcludeDomains { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@betterdomain.com";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void EmailValidator_Attribute_AllowComments() {
      TestDummy_AllowComments dummy = new TestDummy_AllowComments { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@betterdomain.com(#1st comment)";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void EmailValidator_Attribute_AllowIPAddresses() {
      TestDummy_AllowIPAddresses dummy = new TestDummy_AllowIPAddresses { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@[125.243.78.96]";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void EmailValidator_Attribute_RequireTopLevelDomain() {
      TestDummy_RequireTopLevelDomain dummy = new TestDummy_RequireTopLevelDomain { MailAddress = "user@gooddomain.org" };
      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.MailAddress = "user@[125.243.78.96]";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user@test";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.MailAddress = "user@test.tld";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }

    /// <summary>Tests the functionality of the <see cref="EmailValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void EmailValidator_Attribute_NamedValidator() {
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
    public class TestDummy_RequireTopLevelDomain {
      /// <summary>Gets or sets a mail address.</summary>
      [EmailValidator(RequireTopLevelDomain = true, Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid e-mail address ('{0}').")]
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
