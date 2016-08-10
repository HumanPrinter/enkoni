using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing.Tests {
  /// <summary>Tests the functionality of the <see cref="ComparableAssert"/> class.</summary>
  [TestClass]
  public class ComparableAssertTest {
    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThan{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThan_ValueIsLowerThanThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsLowerThan(50, 42);
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThan{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThan_ValueIsEqualToThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsLowerThan(42, 42);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThan{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThan_ValueIsGreaterThanThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsLowerThan(42, 50);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThan_WithMessage_ValueIsLowerThanThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsLowerThan(50, 42, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThan_WithMessage_ValueIsEqualToThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsLowerThan(42, 42, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThan_WithMessage_ValueIsGreaterThanThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsLowerThan(42, 50, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThanOrEqualTo{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThanOrEqualTo_ValueIsLowerThanThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsLowerThanOrEqualTo(50D, 42D);
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThanOrEqualTo{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThanOrEqualTo_ValueIsEqualToThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsLowerThanOrEqualTo(42D, 42D);
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThanOrEqualTo{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThanOrEqualTo_ValueIsGreaterThanThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsLowerThanOrEqualTo(42D, 50D);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThanOrEqualTo{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThanOrEqualTo_WithMessage_ValueIsLowerThanThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsLowerThanOrEqualTo(50M, 42M, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThanOrEqualTo{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThanOrEqualTo_WithMessage_ValueIsEqualToThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsLowerThanOrEqualTo(42M, 42M, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsLowerThanOrEqualTo{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsLowerThanOrEqualTo_WithMessage_ValueIsGreaterThanThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsLowerThanOrEqualTo(42M, 50M, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThan{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThan_ValueIsGreaterThanThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsGreaterThan(42, 50);
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThan{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThan_ValueIsEqualToThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsGreaterThan(42, 42);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThan{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThan_ValueIsLowerThanThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsLowerThan(50, 42);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThan_WithMessage_ValueIsGreaterThanThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsGreaterThan(42, 50, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThan_WithMessage_ValueIsEqualToThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsGreaterThan(42, 42, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThan_WithMessage_ValueIsLowerThanThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsGreaterThan(50, 42, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThanOrEqualTo{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThanOrEqualTo_ValueIsGreaterThanThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsGreaterThanOrEqualTo(42D, 50D);
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThanOrEqualTo{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThanOrEqualTo_ValueIsEqualToThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsGreaterThanOrEqualTo(42D, 42D);
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThanOrEqualTo{T}(T, T)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThanOrEqualTo_ValueIsLowerThanThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsGreaterThanOrEqualTo(50D, 42D);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThanOrEqualTo{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThanOrEqualTo_WithMessage_ValueIsGreaterThanThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsGreaterThanOrEqualTo(42M, 50M, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThanOrEqualTo{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThanOrEqualTo_WithMessage_ValueIsEqualToThreshold_NoExceptionIsThrown() {
      ComparableAssert.IsGreaterThanOrEqualTo(42M, 42M, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="ComparableAssert.IsGreaterThanOrEqualTo{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ComparableAssert_IsGreaterThanOrEqualTo_WithMessage_ValueIsLowerThanThreshold_ExceptionIsThrown() {
      try {
        ComparableAssert.IsGreaterThanOrEqualTo(50M, 42M, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }
  }
}
