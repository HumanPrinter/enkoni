﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.DataAnnotations.Tests {
  /// <summary>Tests the functionality of the <see cref="IbanAttribute"/> class.</summary>
  [TestClass]
  public class IbanAttributeTest {
    #region Properties
    /// <summary>Gets or sets the context that gives access to the input data for the test cases.</summary>
    public TestContext TestContext { get; set; }
    #endregion

    #region TestCases
    /// <summary>Tests the functionality of the <see cref="IbanAttribute"/> class.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\TestData.mdf", @"IbanValidatorTest\TestCase01")]
    [DataSource("System.Data.SqlClient", @"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|IbanValidatorTest\TestCase01\TestData.mdf;Integrated Security=True;Connect Timeout=30", "IbanAccountNumber", DataAccessMethod.Sequential)]
    public void TestCase01_Attribute() {
      TestDummy dummy = new TestDummy { AccountNumber = this.TestContext.DataRow["AccountNumber"].ToString() };
      ValidationContext validationContext = new ValidationContext(dummy, null, null);
      List<ValidationResult> validationResults = new List<ValidationResult>();

      bool expected = Convert.ToBoolean(this.TestContext.DataRow["IsValid"]);
      
      bool result = Validator.TryValidateObject(dummy, validationContext, validationResults, true);

      Assert.AreEqual(expected, result, dummy.AccountNumber);
      if(expected) {
        Assert.AreEqual(0, validationResults.Count, dummy.AccountNumber);
      }
      else {
        Assert.AreEqual(1, validationResults.Count, dummy.AccountNumber);
        Assert.AreEqual("The property AccountNumber is not a valid IBAN account number.", validationResults[0].ErrorMessage);
      }
    }
    #endregion

    #region Inner test classes
    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy {
      /// <summary>Gets or sets a phone number.</summary>
      [Iban(ErrorMessage = "The property {0} is not a valid IBAN account number.")]
      public string AccountNumber { get; set; }
    }
    #endregion
  }
}
