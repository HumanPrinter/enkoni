using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing.Tests
{
  /// <summary>Tests the functionality of the <see cref="ExceptionAssert"/> class.</summary>
  [TestClass]
  public class ExceptionAssertTest {
    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Throws_ActionIsNull_ArgumentExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(null);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Throws_MessageIsNull_ArgumentExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(() => { }, null);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void Throws_MessageIsEmpty_ArgumentExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(() => { }, string.Empty);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    public void Throws_InvalidOperationExceptionIsExpectedAndThrown_AssertDoesNotFail() {
      ExceptionAssert.Throws<InvalidOperationException>(() => {
        throw new InvalidOperationException();
      });
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(NotFiniteNumberException))]
    public void Throws_InvalidOperationExceptionIsExpectedButNotFiniteNumberExceptionIsThrown_NotFiniteNumberExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(() => {
        throw new NotFiniteNumberException();
      });
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    public void Throws_ExpectedExceptionIsDerivedFromTheExpectedException_AssertFails() {
      try {
        ExceptionAssert.Throws<ArgumentException>(() => {
          throw new ArgumentNullException();
        }, "aMessage");
      }
      catch (AssertFailedException exception) {
        Assert.AreEqual("Assert.Fail failed. Exception of type ArgumentException was not thrown an exception of type ArgumentNullException was thrown.",exception.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void Throws_ExpectedExceptionIsDerivedFromTheExpectedExceptionAndAllowDervivedTypesIsTrue_AssertDoesNotFail() {
      ExceptionAssert.Throws<ArgumentException>(() => {
        throw new ArgumentNullException();
      }, "aMessage", true);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action)"/> method.</summary>
    [TestMethod]
    public void Throws_NoExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidCastException>(() => { });
      }
      catch (AssertFailedException exception) {
        Assert.AreEqual("Assert.Fail failed. Exception of type InvalidCastException was not thrown.", exception.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    public void Throws_NoExceptionIsThrownAndMessageIsSpecified_AssertFailsWithSpecifiedMessage() {
      try {
        ExceptionAssert.Throws<InvalidCastException>(() => { }, "aMessage");
      }
      catch (AssertFailedException exception) {
        Assert.AreEqual("Assert.Fail failed. aMessage", exception.Message);
      }
    }
  }
}
