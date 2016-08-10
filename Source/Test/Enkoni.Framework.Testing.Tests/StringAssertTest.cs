using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing.Tests {
  /// <summary>Tests the functionality of the <see cref="StringAssert"/> class.</summary>
  [TestClass]
  public class StringAssertTest {
    /// <summary>Tests the functionality of the <see cref="StringAssert.IsEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsEmpty_WithEmptyString_NoExceptionIsThrown() {
      StringAssert.IsEmpty(string.Empty);
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsEmpty_WithNullString_ExceptionIsThrown() {
      try {
        StringAssert.IsEmpty(null);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsEmpty_WithNonNullNonEmptyString_ExceptionIsThrown() {
      try {
        StringAssert.IsEmpty("some value");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsEmptyWithMessage_WithEmptyString_NoExceptionIsThrown() {
      StringAssert.IsEmpty(string.Empty, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsEmptyWithMessage_WithNullString_ExceptionIsThrown() {
      try {
        StringAssert.IsEmpty(null, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsEmptyWithMessage_WithNonNullNonEmptyString_ExceptionIsThrown() {
      try {
        StringAssert.IsEmpty("some value", "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotEmpty_WithEmptyString_ExceptionIsThrown() {
      try {
        StringAssert.IsNotEmpty(string.Empty);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotEmpty_WithNullString_NoExceptionIsThrown() {
      StringAssert.IsNotEmpty(null);
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotEmpty_WithNonNullNonEmptyString_NoExceptionIsThrown() {
      StringAssert.IsNotEmpty("some value");
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotEmptyWithMessage_WithEmptyString_ExceptionIsThrown() {
      try {
        StringAssert.IsNotEmpty(string.Empty, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotEmptyWithMessage_WithNullString_NoExceptionIsThrown() {
      StringAssert.IsNotEmpty(null, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotEmptyWithMessage_WithNonNullNonEmptyString_NoExceptionIsThrown() {
      StringAssert.IsNotEmpty("some value", "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNullOrEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNullOrEmpty_WithEmptyString_NoExceptionIsThrown() {
      StringAssert.IsNullOrEmpty(string.Empty);
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNullOrEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNullOrEmpty_WithNullString_NoExceptionIsThrown() {
      StringAssert.IsNullOrEmpty(null);
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNullOrEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNullOrEmpty_WithNonNullNonEmptyString_ExceptionIsThrown() {
      try {
        StringAssert.IsNullOrEmpty("some value");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNullOrEmptyWithMessage_WithEmptyString_NoExceptionIsThrown() {
      StringAssert.IsNullOrEmpty(string.Empty, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNullOrEmptyWithMessage_WithNullString_NoExceptionIsThrown() {
      StringAssert.IsNullOrEmpty(null, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNullOrEmptyWithMessage_WithNonNullNonEmptyString_ExceptionIsThrown() {
      try {
        StringAssert.IsNullOrEmpty("some value", "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotNullOrEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotNullOrEmpty_WithEmptyString_ExceptionIsThrown() {
      try {
        StringAssert.IsNotNullOrEmpty(string.Empty);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotNullOrEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotNullOrEmpty_WithNullString_ExceptionIsThrown() {
      try {
        StringAssert.IsNotNullOrEmpty(null);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotNullEmpty(string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotNullOrEmpty_WithNonNullNonEmptyString_NoExceptionIsThrown() {
      StringAssert.IsNotNullOrEmpty("some value");
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotNullOrEmptyWithMessage_WithEmptyString_ExceptionIsThrown() {
      try {
        StringAssert.IsNotNullOrEmpty(string.Empty, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotNullOrEmptyWithMessage_WithNullString_ExceptionIsThrown() {
      try {
        StringAssert.IsNotNullOrEmpty(null, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="StringAssert.IsNotNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void StringAssert_IsNotNullOrEmptyWithMessage_WithNonNullNonEmptyString_NoExceptionIsThrown() {
      StringAssert.IsNotNullOrEmpty("some value", "AssertMessage");
    }
  }
}
