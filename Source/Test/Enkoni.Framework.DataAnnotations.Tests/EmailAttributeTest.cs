using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Enkoni.Framework.DataAnnotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.DataAnnotations.Tests {
  /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.
  /// </summary>
  [TestClass]
  [DeploymentItem("Enkoni.Framework.DataAnnotations.Tests.Database.dacpac")]
  public class EmailAttributeTest {
    #region Properties
    /// <summary>Gets or sets the context that gives access to the input data for the test cases.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region TestCases
    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "BasicEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Basic() {
      TestDummy_Basic_OverrideAll dummy = new TestDummy_Basic_OverrideAll { MailAddress = this.TestContext.DataRow["MailAddress"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.MailAddress);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.MailAddress);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.MailAddress);
        Assert.AreEqual("The property MailAddress is not a valid e-mail address.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "BasicEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Basic_AllowComments() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComments = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool isValid = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      bool expected = isValid && !containsComments;
      
      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "BasicEmail", DataAccessMethod.Sequential)]

    public void EmailAttribute_Basic_AllowIPAddresses() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };
      
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsIpAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !containsIpAddress;

      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "BasicEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Basic_RequireTopLevelDomain() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsTopLevelDomain = Convert.ToBoolean(this.TestContext.DataRow["ContainsTopLevelDomain"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && containsTopLevelDomain;

      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ExtendedEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Extended() {
      TestDummy_Extended_OverrideAll dummy = new TestDummy_Extended_OverrideAll { MailAddress = this.TestContext.DataRow["MailAddress"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.MailAddress);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.MailAddress);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.MailAddress);
        Assert.AreEqual("The property MailAddress is not a valid e-mail address.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ExtendedEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Extended_AllowComments() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Extended, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };

      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComments = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool isValid = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      bool expected = isValid && !containsComments;

      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ExtendedEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Extended_AllowIPAddresses() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };

      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsIpAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !containsIpAddress;

      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "ExtendedEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Extended_RequireTopLevelDomain() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsTopLevelDomain = Convert.ToBoolean(this.TestContext.DataRow["ContainsTopLevelDomain"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && containsTopLevelDomain;

      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "CompleteEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Complete() {
      TestDummy_Complete_OverrideAll dummy = new TestDummy_Complete_OverrideAll { MailAddress = this.TestContext.DataRow["MailAddress"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.MailAddress);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.MailAddress);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.MailAddress);
        Assert.AreEqual("The property MailAddress is not a valid e-mail address.", validationResults[0].ErrorMessage);
      }
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "CompleteEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Complete_AllowComments() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Complete, AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };

      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsComments = Convert.ToBoolean(this.TestContext.DataRow["ContainsComment"]);
      bool isValid = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      bool expected = isValid && !containsComments;

      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "CompleteEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Complete_AllowIPAddresses() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null };

      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsIpAddress = Convert.ToBoolean(this.TestContext.DataRow["ContainsIPAddress"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && !containsIpAddress;

      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    //[TestMethod]
    //[DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "CompleteEmail", DataAccessMethod.Sequential)]
    public void EmailAttribute_Complete_RequireTopLevelDomain() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = true, IncludeDomains = null, ExcludeDomains = null };
      string input = this.TestContext.DataRow["MailAddress"].ToString();
      bool containsTopLevelDomain = Convert.ToBoolean(this.TestContext.DataRow["ContainsTopLevelDomain"]);
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]) && containsTopLevelDomain;

      bool result = testSubject.IsValid(input);
      Assert.AreEqual(expected, result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    [TestMethod]
    public void EmailAttribute_IncludeDomains() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = "*good.o?g;righthere.com", ExcludeDomains = null };
      string input = "local@good.orrg";
      bool result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "local@good.org";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "local@sub.good.org";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "local@righthere.com";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "local@sub.righthere.com";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    [TestMethod]
    public void EmailAttribute_ExcludeDomains() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = "*evil.o?g;nowhere.com" };
      string input = "local@devil.orrg";
      bool result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "local@devil.org";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "local@sub.evil.org";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "local@nowhere.com";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "local@sub.nowhere.com";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    [TestMethod]
    public void EmailAttribute_IncludeExcludeDomains() {
      EmailAttribute testSubject = new EmailAttribute { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = "all.*.good.org", ExcludeDomains = "all.evil.good.org" };
      string input = "user@all.foobar.good.org";
      bool result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "user@all.evil.good.org";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      testSubject = new EmailAttribute { Category = EmailCategory.Basic, AllowComments = false, AllowIPAddresses = false, RequireTopLevelDomain = false, IncludeDomains = "all.good.org", ExcludeDomains = "all.good.org" };
      input = "user@all.good.org";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    [TestMethod]
    public void EmailAttribute_IPFilter() {
      EmailAttribute testSubject = new EmailAttribute { AllowComments = false, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = "192.168.10.23;10.12.*", ExcludeDomains = "36.45.12.63;10.12.9.*" };
      string input = "user@[17.45.26.95]";
      bool result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "user@[192.168.10.23]";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "user@[36.45.12.63]";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);

      input = "user@[10.12.45.125]";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result);

      input = "user@[10.12.9.152]";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    [TestMethod]
    public void EmailAttribute_Configuration() {
      EmailAttribute testSubject = new EmailAttribute();
      string input = "user@somewhere.co.uk";
      bool result = testSubject.IsValid(input);
      Assert.IsTrue(result, input);

      input = "user@sub.baddomain.org";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result, input);

      input = "user@[12.124.63.5]";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result, input);

      input = "user(my comment)@somewhere.co.uk";
      result = testSubject.IsValid(input);
      Assert.IsFalse(result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    [TestMethod]
    public void EmailAttribute_Configuration_NamedValidator() {
      EmailAttribute testSubject = new EmailAttribute("Custom Validator");
      string input = "user@somewhere.co.uk";
      bool result = testSubject.IsValid(input);
      Assert.IsFalse(result, input);

      input = "user@[127.251.42.88]";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result, input);

      input = "user(my comment)@[127.251.42.88]";
      result = testSubject.IsValid(input);
      Assert.IsTrue(result, input);
    }

    /// <summary>Tests the functionality of the <see cref="EmailAttribute"/> class.</summary>
    [TestMethod]
    public void EmailAttribute_Attribute_AllDefaults() {
      TestDummy_AllDefault dummy = new TestDummy_AllDefault { MailAddress = "user@somewhere.co.uk" };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);
      Assert.IsTrue(result);
      
      dummy.MailAddress = "user@sub.baddomain.org";
      result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);
      Assert.IsFalse(result);

      validationResults.Clear();
      dummy.MailAddress = "user@[12.124.63.5]";
      result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);
      Assert.IsFalse(result);

      validationResults.Clear();
      dummy.MailAddress = "user(my comment)@somewhere.co.uk";
      result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);
      Assert.IsFalse(result);

      validationResults.Clear();
      dummy.MailAddress = "user!name@somewhere.co.uk";
      result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);
      Assert.IsFalse(result);
    }
    #endregion

    #region Inner test classes
    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Basic_OverrideAll {
      /// <summary>Gets or sets a mail address.</summary>
      [Email(Category = EmailCategory.Basic, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null, ErrorMessage = "The property {0} is not a valid e-mail address.")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Extended_OverrideAll {
      /// <summary>Gets or sets a mail address.</summary>
      [Email(Category = EmailCategory.Extended, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null, ErrorMessage = "The property {0} is not a valid e-mail address.")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_Complete_OverrideAll {
      /// <summary>Gets or sets a mail address.</summary>
      [Email(Category = EmailCategory.Complete, AllowComments = true, AllowIPAddresses = true, RequireTopLevelDomain = false, IncludeDomains = null, ExcludeDomains = null, ErrorMessage = "The property {0} is not a valid e-mail address.")]
      public string MailAddress { get; set; }
    }

    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy_AllDefault {
      /// <summary>Gets or sets a mail address.</summary>
      [Email(ErrorMessage = "The property {0} is not a valid e-mail address.")]
      public string MailAddress { get; set; }
    }
    #endregion
  }
}
