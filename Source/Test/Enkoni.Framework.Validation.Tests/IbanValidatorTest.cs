using System;

using Enkoni.Framework.Validation.Validators;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using EntLib = Microsoft.Practices.EnterpriseLibrary.Validation;

namespace Enkoni.Framework.Validation.Tests {
  /// <summary>Tests the functionality of the <see cref="IbanValidator"/> and <see cref="IbanValidatorAttribute"/> classes.</summary>
  [TestClass]
  [DeploymentItem("Enkoni.Framework.Validation.Tests.Database.dacpac")]
  public class IbanValidatorTest {
    #region Properties
    /// <summary>Gets or sets the context that gives access to the input data for the test cases.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region TestCases
    /// <summary>Tests the functionality of the <see cref="IbanValidator"/> class.</summary>
    [TestMethod]
    [DataSource("System.Data.SqlClient", TestInitializer.ConnectionString, "IbanAccountNumber", DataAccessMethod.Sequential)]
    public void IbanValidator_Validator() {
      IbanValidator testSubject = new IbanValidator("message {0}", "tag", false);
      string input = this.TestContext.DataRow["AccountNumber"].ToString();
      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      EntLib.ValidationResults results = testSubject.Validate(input);
      Assert.AreEqual(expected, results.IsValid, input);

      testSubject = new IbanValidator("message {0}", "tag", true);
      results = testSubject.Validate(input);
      Assert.AreEqual(!expected, results.IsValid, input);
    }

    /// <summary>Tests the functionality of the <see cref="IbanValidatorAttribute"/> class.</summary>
    [TestMethod]
    public void IbanValidator_Attribute() {
      TestDummy dummy = new TestDummy { AccountNumber = "NL80INGB0007321304" };

      EntLib.ValidationResults results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);

      dummy.AccountNumber = "NL80INgB0007321304";
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsFalse(results.IsValid);

      dummy.AccountNumber = null;
      results = EntLib.Validation.Validate(dummy, "ValidationTest");
      Assert.IsTrue(results.IsValid);
    }
    #endregion

    #region Inner test classes
    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy {
      /// <summary>Gets or sets a phone number.</summary>
      [IbanValidator(Ruleset = "ValidationTest", MessageTemplate = "The property {1} is not a valid IBAN account number ('{0}').")]
      public string AccountNumber { get; set; }
    }
    #endregion
  }
}
