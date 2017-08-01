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
    public void ExceptionAssert_Throws_ActionIsNull_ArgumentNullExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(null);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ExceptionAssert_ThrowsWithMessage_ActionIsNull_ArgumentNullExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(null, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ExceptionAssert_ThrowsWithMessage_MessageIsNull_ArgumentNullExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(() => { }, null);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ExceptionAssert_ThrowsWithMessage_MessageIsEmpty_ArgumentExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(() => { }, string.Empty);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ExceptionAssert_ThrowsWithMessageAndBool_ActionIsNull_ArgumentNullExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(null, "AssertMessage", false);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void ExceptionAssert_ThrowsWithMessageAndBool_MessageIsNull_ArgumentNullExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(() => { }, null, false);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void ExceptionAssert_ThrowsWithMessageAndBool_MessageIsEmpty_ArgumentExceptionIsThrown() {
      ExceptionAssert.Throws<InvalidOperationException>(() => { }, string.Empty, false);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_Throws_ExpectedExceptionIsThrown_AssertDoesNotFail() {
      ExceptionAssert.Throws<InvalidOperationException>(() => {
        throw new InvalidOperationException();
      });
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_Throws_OtherThanExpectedExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidOperationException>(() => {
          throw new NotFiniteNumberException();
        });
      }
      catch(AssertFailedException ex) {
        Assert.IsInstanceOfType(ex.InnerException, typeof(NotFiniteNumberException));
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_Throws_NoExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidOperationException>(() => { });
        Assert.Fail("No exception was thrown");
      }
      catch (AssertFailedException ex) when (ex.Message.EndsWith("No exception was thrown")) {
        throw;
      }
      catch (AssertFailedException ex) {
        Assert.IsNull(ex.InnerException);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_Throws_DerivedExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<ArgumentException>(() => {
          throw new ArgumentNullException();
        });
      }
      catch(AssertFailedException ex) {
        Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessage_ExpectedExceptionIsThrown_AssertDoesNotFail() {
      ExceptionAssert.Throws<InvalidOperationException>(() => {
        throw new InvalidOperationException();
      }, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessage_OtherThanExpectedExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidOperationException>(() => {
          throw new NotFiniteNumberException();
        }, "AssertMessage");
      }
      catch(AssertFailedException ex) {
        Assert.IsInstanceOfType(ex.InnerException, typeof(NotFiniteNumberException));
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessage_NoExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidOperationException>(() => { }, "AssertMessage");
      }
      catch(AssertFailedException ex) {
        Assert.IsNull(ex.InnerException);
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessage_DerivedExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<ArgumentException>(() => {
          throw new ArgumentNullException();
        }, "AssertMessage");
      }
      catch(AssertFailedException ex) {
        Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessageAndBool_DoNotAllowDerivedTypes_ExpectedExceptionIsThrown_AssertDoesNotFail() {
      ExceptionAssert.Throws<InvalidOperationException>(() => {
        throw new InvalidOperationException();
      }, "AssertMessage", false);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessageAndBool_DoNotAllowDerivedTypes_OtherThanExpectedExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidOperationException>(() => {
          throw new NotFiniteNumberException();
        }, "AssertMessage", false);
      }
      catch(AssertFailedException ex) {
        Assert.IsInstanceOfType(ex.InnerException, typeof(NotFiniteNumberException));
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessageAndBool_DoNotAllowDerivedTypes_NoExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidOperationException>(() => { }, "AssertMessage", false);
      }
      catch(AssertFailedException ex) {
        Assert.IsNull(ex.InnerException);
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessageAndBool_DoNotAllowDerivedTypes_DerivedExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<ArgumentException>(() => {
          throw new ArgumentNullException();
        }, "AssertMessage", false);
      }
      catch(AssertFailedException ex) {
        Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessageAndBool_AllowDerivedTypes_ExpectedExceptionIsThrown_AssertDoesNotFail() {
      ExceptionAssert.Throws<InvalidOperationException>(() => {
        throw new InvalidOperationException();
      }, "AssertMessage", true);
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessageAndBool_AllowDerivedTypes_OtherThanExpectedExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidOperationException>(() => {
          throw new NotFiniteNumberException();
        }, "AssertMessage", true);
      }
      catch(AssertFailedException ex) {
        Assert.IsInstanceOfType(ex.InnerException, typeof(NotFiniteNumberException));
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessageAndBool_AllowDerivedTypes_NoExceptionIsThrown_AssertFails() {
      try {
        ExceptionAssert.Throws<InvalidOperationException>(() => { }, "AssertMessage", true);
      }
      catch(AssertFailedException ex) {
        Assert.IsNull(ex.InnerException);
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Throws{TException}(Action, string, bool)"/> method.</summary>
    [TestMethod]
    public void ExceptionAssert_ThrowsWithMessageAndBool_AllowDerivedTypes_DerivedExceptionIsThrown_AssertDoesNotFail() {
      ExceptionAssert.Throws<ArgumentException>(() => {
        throw new ArgumentNullException();
      }, "AssertMessage", true);
    }
  }
}
