using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing.Tests {
  /// <summary>Tests the functionality of the <see cref="CollectionAssert"/> class.</summary>
  [TestClass]
  public class CollectionAssertTest {
    /// <summary>Tests the functionality of the <see cref="CollectionAssert.IsEmpty(System.Collections.ICollection)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_IsEmpty_WithEmptyCollection_NoExceptionIsThrown() {
      List<int> input = new List<int>();
      CollectionAssert.IsEmpty(input);
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.IsEmpty(System.Collections.ICollection)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_IsEmpty_WithNonEmptyCollection_ExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      try {
        CollectionAssert.IsEmpty(input);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.IsEmpty(System.Collections.ICollection, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_IsEmptyWithMessage_WithEmptyCollection_NoExceptionIsThrown() {
      List<int> input = new List<int>();
      CollectionAssert.IsEmpty(input, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.IsEmpty(System.Collections.ICollection, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_IsEmptyWithMessage_WithNonCollection_ExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      try {
        CollectionAssert.IsEmpty(input, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.IsNotEmpty(System.Collections.ICollection)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_IsNotEmpty_WithEmptyCollection_ExceptionIsThrown() {
      List<int> input = new List<int>();
      try {
        CollectionAssert.IsNotEmpty(input);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.IsNotEmpty(System.Collections.ICollection)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_IsNotEmpty_WithNonEmptyCollection_NoExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      CollectionAssert.IsNotEmpty(input);
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.IsNotEmpty(System.Collections.ICollection, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_IsNotEmptyWithMessage_WithEmptyCollection_ExceptionIsThrown() {
      List<int> input = new List<int>();
      try {
        CollectionAssert.IsNotEmpty(input, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.IsNotEmpty(System.Collections.ICollection, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_IsNotEmptyWithMessage_WithNonEmptyCollection_NoExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      CollectionAssert.IsNotEmpty(input, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesHaveSize(System.Collections.ICollection, int)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesHaveSize_WithMatchingCollection_NoExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      CollectionAssert.DoesHaveSize(input, 3);
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesHaveSize(System.Collections.ICollection, int)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesHaveSize_WithTooSmallCollection_ExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      try {
        CollectionAssert.DoesHaveSize(input, 4);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesHaveSize(System.Collections.ICollection, int)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesHaveSize_WithTooLargeCollection_ExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      try {
        CollectionAssert.DoesHaveSize(input, 2);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesHaveSize(System.Collections.ICollection, int, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesHaveSizeWithMessage_WithMatchingCollection_NoExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      CollectionAssert.DoesHaveSize(input, 3, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesHaveSize(System.Collections.ICollection, int, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesHaveSizeWithMessage_WithTooSmallCollection_ExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      try {
        CollectionAssert.DoesHaveSize(input, 4, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesHaveSize(System.Collections.ICollection, int, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesHaveSizeWithMessage_WithTooLargeCollection_ExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      try {
        CollectionAssert.DoesHaveSize(input, 2, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesNotHaveSize(System.Collections.ICollection, int)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesNotHaveSize_WithMatchingCollection_ExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      try {
        CollectionAssert.DoesNotHaveSize(input, 3);
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException) {
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesNotHaveSize(System.Collections.ICollection, int)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesNotHaveSize_WithTooSmallCollection_NoExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      CollectionAssert.DoesNotHaveSize(input, 4);
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesNotHaveSize(System.Collections.ICollection, int)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesNotHaveSize_WithTooLargeCollection_NoExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      CollectionAssert.DoesNotHaveSize(input, 2);
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesNotHaveSize(System.Collections.ICollection, int, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesNotHaveSizeWithMessage_WithMatchingCollection_ExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      try {
        CollectionAssert.DoesNotHaveSize(input, 3, "AssertMessage");
        Assert.Fail("AssertFailedException was expected");
      }
      catch(AssertFailedException ex) {
        Assert.AreEqual("AssertMessage", ex.Message);
      }
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesNotHaveSize(System.Collections.ICollection, int, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesNotHaveSizeWithMessage_WithTooSmallCollection_NoExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      CollectionAssert.DoesNotHaveSize(input, 4, "AssertMessage");
    }

    /// <summary>Tests the functionality of the <see cref="CollectionAssert.DoesNotHaveSize(System.Collections.ICollection, int, string)"/> method.</summary>
    [TestMethod]
    public void CollectionAssert_DoesNotHaveSizeWithMessage_WithTooLargeCollection_NoExceptionIsThrown() {
      List<int> input = new List<int> { 42, 43, 44 };
      CollectionAssert.DoesNotHaveSize(input, 2, "AssertMessage");
    }
  }
}
