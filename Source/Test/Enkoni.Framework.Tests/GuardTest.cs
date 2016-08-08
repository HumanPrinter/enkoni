using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the string-class.</summary>
  [TestClass]
  public class GuardTest {
    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullWithDefaultMessage_NoNullArgument_DoesNotThrowException() {
      object testArgument = new object();
      Guard.ArgumentIsNotNull(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullWithDefaultMessage_NullArgument_ThrowsArgumentNullException() {
      object testArgument = null;

      try {
        Guard.ArgumentIsNotNull(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentNullException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNull(object, string, string)"/> extension method.</summary>
    [TestMethod]
    public void ArgumentIsNotNull_NoNullArgument_DoesNotThrowException() {
      object testArgument = new object();
      Guard.ArgumentIsNotNull(testArgument, nameof(testArgument), "errorMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNull(object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNull_NullArgument_ThrowsArgumentNullException() {
      object testArgument = null;

      try {
        Guard.ArgumentIsNotNull(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentNullException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmptyWithDefaultMessage_NoNullNoEmptyString_ArgumentDoesNotThrowException() {
      string testArgument = "someInput";
      Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmptyWithDefaultMessage_NullStringArgument_ThrowsArgumentNullException() {
      string testArgument = null;
      try {
        Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentNullException was expected");
      }
      catch(ArgumentNullException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty(string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmptyWithDefaultMessage_EmptyStringArgument_ThrowsArgumentException() {
      string testArgument = string.Empty;
      try {
        Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument cannot be empty" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty(string, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_NoNullNoEmptyString_ArgumentDoesNotThrowException() {
      string testArgument = "someInput";
      Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty(string, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_NullStringArgument_ThrowsArgumentNullException() {
      string testArgument = null;
      try {
        Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentNullException was expected");
      }
      catch(ArgumentNullException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty(string, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_EmptyStringArgument_ThrowsArgumentException() {
      string testArgument = string.Empty;
      try {
        Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty{T}(IEnumerable{T}, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmptyWithDefaultMessage_NoNullNoEmptyCollectionArgument_DoesNotThrowException() {
      IEnumerable<string> testArgument = new List<string> { "someInput" };
      Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty{T}(IEnumerable{T}, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmptyWithDefaultMessage_NullCollectionArgument_ThrowsArgumentNullException() {
      IEnumerable<string> testArgument = null;
      try {
        Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentNullException was expected");
      }
      catch(ArgumentNullException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty{T}(IEnumerable{T}, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmptyWithDefaultMessage_EmptyCollectionArgument_ThrowsArgumentException() {
      IEnumerable<string> testArgument = Enumerable.Empty<string>();
      try {
        Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument cannot be empty" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty{T}(IEnumerable{T}, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_NoNullNoEmptyCollectionArgument_DoesNotThrowException() {
      IEnumerable<string> testArgument = new List<string> { "someInput" };
      Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty{T}(IEnumerable{T}, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_NullCollectionArgument_ThrowsArgumentNullException() {
      IEnumerable<string> testArgument = null;
      try {
        Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentNullException was expected");
      }
      catch(ArgumentNullException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotNullOrEmpty{T}(IEnumerable{T}, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotNullOrEmpty_EmptyCollectionArgument_ThrowsArgumentException() {
      IEnumerable<string> testArgument = Enumerable.Empty<string>();
      try {
        Guard.ArgumentIsNotNullOrEmpty(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterOrEqualThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterOrEqualThanWithDefaultMessage_ArgumentGreaterThanThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsGreaterOrEqualThan(40, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterOrEqualThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterOrEqualThanWithDefaultMessage_ArgumentEqualToThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsGreaterOrEqualThan(42, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterOrEqualThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterOrEqualThanWithDefaultMessage_ArgumentSmallerThanThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsGreaterOrEqualThan(44, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "Argument must be greater than or equal to 44" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterOrEqualThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterOrEqualThan_ArgumentGreaterThanThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsGreaterOrEqualThan(40, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterOrEqualThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterOrEqualThan_ArgumentEqualToThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsGreaterOrEqualThan(42, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterOrEqualThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterOrEqualThan_ArgumentSmallerThanThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsGreaterOrEqualThan(44, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterThanWithDefaultMessage_ArgumentGreaterThanThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsGreaterThan(40, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterThanWithDefaultMessage_ArgumentEqualToThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsGreaterThan(42, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "Argument must be greater than 42" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterThanWithDefaultMessage_ArgumentSmallerThanThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsGreaterOrEqualThan(44, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "Argument must be greater than or equal to 44" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterThan_ArgumentGreaterThanThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsGreaterThan(40, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterThan_ArgumentEqualToThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsGreaterThan(42, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsGreaterThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsGreaterThan_ArgumentSmallerThanThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsGreaterOrEqualThan(44, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerOrEqualThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerOrEqualThanWithDefaultMessage_ArgumentLowerThanThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsLowerOrEqualThan(44, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerOrEqualThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerOrEqualThanWithDefaultMessage_ArgumentEqualToThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsLowerOrEqualThan(42, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerOrEqualThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerOrEqualThanWithDefaultMessage_ArgumentBiggerThanThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsLowerOrEqualThan(40, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "Argument must be lower than or equal to 40" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerOrEqualThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerOrEqualThan_ArgumentLowerThanThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsLowerOrEqualThan(44, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerOrEqualThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerOrEqualThan_ArgumentEqualToThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsLowerOrEqualThan(42, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerOrEqualThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerOrEqualThan_ArgumentBiggerThanThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsLowerOrEqualThan(40, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerThanWithDefaultMessage_ArgumentLowerThanThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsLowerThan(44, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerThanWithDefaultMessage_ArgumentEqualToThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsLowerThan(42, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "Argument must be lower than 42" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerThan{T}(T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerThanWithDefaultMessage_ArgumentBiggerThanThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsLowerThan(40, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "Argument must be lower than 40" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerThan_ArgumentLowerThanThreshold_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsLowerThan(44, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerThan_ArgumentEqualToThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsLowerThan(42, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsLowerThan{T}(T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsLowerThan_ArgumentBiggerThanThreshold_ThrowsArgumentOutOfRangeException() {
      int testArgument = 42;
      try {
        Guard.ArgumentIsLowerThan(40, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetweenWithDefaultMessage_ArgumentBetweenBoundaries_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetweenWithDefaultMessage_ArgumentEqualToLowerBoundary_DoesNotThrowArgumentException() {
      int testArgument = 40;
      Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetweenWithDefaultMessage_ArgumentEqualToUpperBoundary_DoesNotThrowArgumentException() {
      int testArgument = 44;
      Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetweenWithDefaultMessage_ArgumentLowerThanLowerBoundary_ThrowsArgumentOutOfRangeException() {
      int testArgument = 38;
      try {
        Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "Argument must be between 40 and 44" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetweenWithDefaultMessage_ArgumentGreaterThanUpperBoundary_ThrowsArgumentOutOfRangeException() {
      int testArgument = 48;
      try {
        Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "Argument must be between 40 and 44" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetween_ArgumentBetweenBoundaries_DoesNotThrowArgumentException() {
      int testArgument = 42;
      Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetween_ArgumentEqualToLowerBoundary_DoesNotThrowArgumentException() {
      int testArgument = 40;
      Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetween_ArgumentEqualToUpperBoundary_DoesNotThrowArgumentException() {
      int testArgument = 44;
      Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetween_ArgumentLowerThanLowerBoundary_ThrowsArgumentOutOfRangeException() {
      int testArgument = 38;
      try {
        Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsBetween{T}(T, T, T, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsBetween_ArgumentGreaterThanUpperBoundary_ThrowsArgumentOutOfRangeException() {
      int testArgument = 48;
      try {
        Guard.ArgumentIsBetween(40, 44, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentOutOfRangeException was expected");
      }
      catch(ArgumentOutOfRangeException exception) {
        Assert.AreEqual("testArgument", exception.ParamName);
        Assert.AreEqual(testArgument, exception.ActualValue);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidEnum(Type, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidEnumWithDefaultMessage_ArgumentIsValidEnum_DoesNotThrowArgumentException() {
      TestEnum testArgument = TestEnum.EnumValueB;
      Guard.ArgumentIsValidEnum(typeof(TestEnum), testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidEnum(Type, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidEnumWithDefaultMessage_ArgumentAsIntIsValidEnum_DoesNotThrowArgumentException() {
      int testArgument = 2;
      Guard.ArgumentIsValidEnum(typeof(TestEnum), testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidEnum(Type, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidEnumWithDefaultMessage_ArgumentIsNotValidEnum_ThrowsArgumentException() {
      int testArgument = 3;
      try {
        Guard.ArgumentIsValidEnum(typeof(TestEnum), testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument must be a valid enum value" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidEnum(Type, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidEnum_ArgumentIsValidEnum_DoesNotThrowArgumentException() {
      TestEnum testArgument = TestEnum.EnumValueB;
      Guard.ArgumentIsValidEnum(typeof(TestEnum), testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidEnum(Type, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidEnum_ArgumentAsIntIsValidEnum_DoesNotThrowArgumentException() {
      int testArgument = 2;
      Guard.ArgumentIsValidEnum(typeof(TestEnum), testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidEnum(Type, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidEnum_ArgumentIsNotValidEnum_ThrowsArgumentException() {
      int testArgument = 3;
      try {
        Guard.ArgumentIsValidEnum(typeof(TestEnum), testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfTypeWithDefaultMessage_ArgumentIsOfExpectedType_DoesNotThrowArgumentException() {
      object testArgument = new TestDummy();
      Guard.ArgumentIsOfType<TestDummy>(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfTypeWithDefaultMessage_ArgumentIsNotOfExpectedType_ThrowsArgumentException() {
      object testArgument = new OtherTestDummy();
      try {
        Guard.ArgumentIsOfType<TestDummy>(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument is not of the expected type" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfTypeWithDefaultMessage_ArgumentIsSubtypeOfExpectedType_ThrowsArgumentException() {
      object testArgument = new SubTestDummy();
      try {
        Guard.ArgumentIsOfType<TestDummy>(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument is not of the expected type" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfType_ArgumentIsOfExpectedType_DoesNotThrowArgumentException() {
      object testArgument = new TestDummy();
      Guard.ArgumentIsOfType<TestDummy>(testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfType_ArgumentIsNotOfExpectedType_ThrowsArgumentException() {
      object testArgument = new OtherTestDummy();
      try {
        Guard.ArgumentIsOfType<TestDummy>(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfType_ArgumentIsSubtypeOfExpectedType_ThrowsArgumentException() {
      object testArgument = new SubTestDummy();
      try {
        Guard.ArgumentIsOfType<TestDummy>(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(bool, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfTypeWithDefaultMessage_ArgumentIsOfExpectedTypeAllowDerivedTypes_DoesNotThrowArgumentException() {
      object testArgument = new TestDummy();
      Guard.ArgumentIsOfType<TestDummy>(true, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(bool, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfTypeWithDefaultMessage_ArgumentIsSubtypeOfExpectedTypeAllowDerivedTypes_DoesNotThrowArgumentException() {
      object testArgument = new SubTestDummy();
      Guard.ArgumentIsOfType<TestDummy>(true, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(bool, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfTypeWithDefaultMessage_ArgumentIsNotOfExpectedTypeAllowDerivedTypes_ThrowsArgumentException() {
      object testArgument = new OtherTestDummy();
      try {
        Guard.ArgumentIsOfType<TestDummy>(true, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument is not of the expected type" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(bool, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfType_ArgumentIsOfExpectedTypeAllowDerivedTypes_DoesNotThrowArgumentException() {
      object testArgument = new TestDummy();
      Guard.ArgumentIsOfType<TestDummy>(true, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(bool, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfType_ArgumentIsSubtypeOfExpectedTypeAllowDerivedTypes_DoesNotThrowArgumentException() {
      object testArgument = new SubTestDummy();
      Guard.ArgumentIsOfType<TestDummy>(true, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsOfType{T}(bool, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsOfType_ArgumentIsNotOfExpectedTypeAllowDerivedTypes_ThrowsArgumentException() {
      object testArgument = new OtherTestDummy();
      try {
        Guard.ArgumentIsOfType<TestDummy>(true, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfTypeWithDefaultMessage_ArgumentIsNotOfExpectedType_DoesNotThrowArgumentException() {
      object testArgument = new OtherTestDummy();
      Guard.ArgumentIsNotOfType<TestDummy>(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfTypeWithDefaultMessage_ArgumentIsOfExpectedType_ThrowsArgumentException() {
      object testArgument = new TestDummy();
      try {
        Guard.ArgumentIsNotOfType<TestDummy>(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument has an illegal type" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfTypeWithDefaultMessage_ArgumentIsSubtypeOfExpectedType_DoesNotThrowArgumentException() {
      object testArgument = new OtherTestDummy();
      Guard.ArgumentIsNotOfType<TestDummy>(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfType_ArgumentIsNotOfExpectedType_DoesNotThrowArgumentException() {
      object testArgument = new OtherTestDummy();
      Guard.ArgumentIsNotOfType<TestDummy>(testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfType_ArgumentIsOfExpectedType_ThrowsArgumentException() {
      object testArgument = new TestDummy();
      try {
        Guard.ArgumentIsNotOfType<TestDummy>(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfType_ArgumentIsSubtypeOfExpectedType_DoesNotThrowArgumentException() {
      object testArgument = new OtherTestDummy();
      Guard.ArgumentIsNotOfType<TestDummy>(testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(bool, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfTypeWithDefaultMessage_ArgumentIsNotOfExpectedTypeDisallowDerivedTypes_DoesNotThrowArgumentException() {
      object testArgument = new OtherTestDummy();
      Guard.ArgumentIsNotOfType<TestDummy>(true, testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(bool, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfTypeWithDefaultMessage_ArgumentIsOfExpectedTypeDisallowDerivedTypes_ThrowsArgumentException() {
      object testArgument = new TestDummy();
      try {
        Guard.ArgumentIsNotOfType<TestDummy>(true, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument has an illegal type" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(bool, object, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfTypeWithDefaultMessage_ArgumentIsSubtypeOfExpectedTypeDisallowDerivedTypes_ThrowsArgumentException() {
      object testArgument = new SubTestDummy();
      try {
        Guard.ArgumentIsNotOfType<TestDummy>(true, testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument has an illegal type" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(bool, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfType_ArgumentIsNotOfExpectedTypeDisallowDerivedTypes_DoesNotThrowArgumentException() {
      object testArgument = new OtherTestDummy();
      Guard.ArgumentIsNotOfType<TestDummy>(true, testArgument, nameof(testArgument), "ExpectedMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(bool, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfType_ArgumentIsOfExpectedTypeDisallowDerivedTypes_ThrowsArgumentException() {
      object testArgument = new TestDummy();
      try {
        Guard.ArgumentIsNotOfType<TestDummy>(true, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsNotOfType{T}(bool, object, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsNotOfType_ArgumentIsSubtypeOfExpectedTypeDisallowDerivedTypes_ThrowsArgumentException() {
      object testArgument = new SubTestDummy();
      try {
        Guard.ArgumentIsNotOfType<TestDummy>(true, testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPathWithDefaultMessage_ArgumentIsValidAbsoluteFilePath_DoesNotThrowException() {
      string testArgument = "C:\\somewhere\\somethere\\here.txt";
      Guard.ArgumentIsValidPath(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPathWithDefaultMessage_ArgumentIsValidRelativeFilePath_DoesNotThrowException() {
      string testArgument = "..\\somethere\\here.txt";
      Guard.ArgumentIsValidPath(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPathWithDefaultMessage_ArgumentIsValidAbsoluteDirectoryPath_DoesNotThrowException() {
      string testArgument = "C:\\somewhere\\somethere\\here\\";
      Guard.ArgumentIsValidPath(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPathWithDefaultMessage_ArgumentIsValidRelativeDirectoryPath_DoesNotThrowException() {
      string testArgument = "..\\somethere\\here\\";
      Guard.ArgumentIsValidPath(testArgument, nameof(testArgument));
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPathWithDefaultMessage_ArgumentIsInvalidPath_ThrowsArgumentException() {
      try {
        string testArgument = "C:\\somewhere\\s>omethere\\here.txt";
        Guard.ArgumentIsValidPath(testArgument, nameof(testArgument));
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "Argument must be a valid path" + Environment.NewLine);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPath_ArgumentIsValidAbsoluteFilePath_DoesNotThrowException() {
      string testArgument = "C:\\somewhere\\somethere\\here.txt";
      Guard.ArgumentIsValidPath(testArgument, nameof(testArgument), "ErrorMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPath_ArgumentIsValidRelativeFilePath_DoesNotThrowException() {
      string testArgument = "..\\somethere\\here.txt";
      Guard.ArgumentIsValidPath(testArgument, nameof(testArgument), "ErrorMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPath_ArgumentIsValidAbsoluteDirectoryPath_DoesNotThrowException() {
      string testArgument = "C:\\somewhere\\somethere\\here\\";
      Guard.ArgumentIsValidPath(testArgument, nameof(testArgument), "ErrorMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPath_ArgumentIsValidRelativeDirectoryPath_DoesNotThrowException() {
      string testArgument = "..\\somethere\\here\\";
      Guard.ArgumentIsValidPath(testArgument, nameof(testArgument), "ErrorMessage");
    }

    /// <summary>Tests the functionality of the <see cref="Guard.ArgumentIsValidPath(string, string, string)"/> method.</summary>
    [TestMethod]
    public void ArgumentIsValidPath_ArgumentIsInvalidPath_ThrowsArgumentException() {
      try {
        string testArgument = "C:\\somewhere\\s>omethere\\here.txt";
        Guard.ArgumentIsValidPath(testArgument, nameof(testArgument), "ExpectedMessage");
        Assert.Fail("An ArgumentException was expected");
      }
      catch(ArgumentException exception) {
        /* Make sure it not some derived exceptioin type */
        Assert.AreEqual(typeof(ArgumentException), exception.GetType());
        Assert.AreEqual("testArgument", exception.ParamName);
        StringAssert.StartsWith(exception.Message, "ExpectedMessage" + Environment.NewLine);
      }
    }
    #region Private helper types
    private enum TestEnum {
      EnumValueA = 1,
      EnumValueB = 2
    }

    private class TestDummy {
    }

    private class SubTestDummy : TestDummy {
    }

    private class OtherTestDummy {
    }
    #endregion
  }
}
