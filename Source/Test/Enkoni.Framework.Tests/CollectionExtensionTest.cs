using System.Collections.Generic;

using Enkoni.Framework.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the <see cref="ICollection{T}"/> and 
  /// <see cref="IList{T}"/> interfaces and the <see cref="List{T}"/> class.</summary>
  [TestClass]
  public class CollectionExtensionTest {
    #region Remove test cases
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.Remove{T}(ICollection{T}, T, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void Remove_ComparerWithSingleMatch_MatchedInstanceIsRemovedFromCollection() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      List<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to remove the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      bool result = collection.Remove(selectionDummy, comparer);

      Assert.IsTrue(result);
      Assert.AreEqual(4, collection.Count);
      CollectionAssert.Contains(collection, dummyA);
      CollectionAssert.DoesNotContain(collection, dummyB);
      CollectionAssert.Contains(collection, dummyC);
      CollectionAssert.Contains(collection, dummyD);
      CollectionAssert.Contains(collection, dummyE);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.Remove{T}(ICollection{T}, T, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void Remove_ComparerWithMultipleMatches_FirstMatchedInstanceIsRemovedFromCollection() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB1 = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyB2 = new TestDummy { TextValue = "DummyB" };

      List<TestDummy> collection = new List<TestDummy> { dummyA, dummyB1, dummyC, dummyD, dummyB2 };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to remove the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      bool result = collection.Remove(selectionDummy, comparer);

      Assert.IsTrue(result);
      Assert.AreEqual(4, collection.Count);
      CollectionAssert.Contains(collection, dummyA);
      CollectionAssert.DoesNotContain(collection, dummyB1);
      CollectionAssert.Contains(collection, dummyC);
      CollectionAssert.Contains(collection, dummyD);
      CollectionAssert.Contains(collection, dummyB2);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.Remove{T}(ICollection{T}, T, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void Remove_ComparerWithNoMatch_NoInstanceIsRemovedFromCollection() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      List<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyF" };

      /* The extension method will be able to remove the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      bool result = collection.Remove(selectionDummy, comparer);

      Assert.IsFalse(result);
      Assert.AreEqual(5, collection.Count);
      CollectionAssert.Contains(collection, dummyA);
      CollectionAssert.Contains(collection, dummyB);
      CollectionAssert.Contains(collection, dummyC);
      CollectionAssert.Contains(collection, dummyD);
      CollectionAssert.Contains(collection, dummyE);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.Remove{T}(ICollection{T}, T, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void Remove_EmptyCollection_NoInstanceIsRemovedFromCollection() {
      ICollection<TestDummy> collection = new List<TestDummy>();

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyF" };

      /* The extension method will be able to remove the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      bool result = collection.Remove(selectionDummy, comparer);

      Assert.IsFalse(result);
      Assert.AreEqual(0, collection.Count);
    }
    #endregion

    #region IndexOf test cases
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(IList{T}, T, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOf_ComparerWithSingleMatch_IndexOfMatchedInstanceIsReturned() {
      IList<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, comparer);
      Assert.AreEqual(1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(IList{T}, T, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOf_ComparerWithMultipleMatches_IndexOfFirstMatchedInstanceIsReturned() {
      IList<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyB" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, comparer);
      Assert.AreEqual(1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(IList{T}, T, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOf_ComparerWithNoMatch_NoIndexIsReturned() {
      IList<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyF" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, comparer);
      Assert.AreEqual(-1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(IList{T}, T, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOf_EmptyCollection_NoIndexIsReturned() {
      IList<TestDummy> collection = new List<TestDummy>();

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyF" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, comparer);
      Assert.AreEqual(-1, result);
    }
    #endregion

    #region IndexOf (with start index) test cases
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndex_ComparerWithSingleMatchAfterStartIndex_IndexOfMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyC" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, comparer);
      Assert.AreEqual(2, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndex_ComparerWithSingleMatchAtStartIndex_IndexOfMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, comparer);
      Assert.AreEqual(1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndex_ComparerWithSingleMatchBeforeStartIndex_NoIndexIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 3, comparer);
      Assert.AreEqual(-1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndex_ComparerWithMatchBeforeAndAfterStartIndex_IndexOfMatchedInstanceAfterStartIndexIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyE" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 2, comparer);
      Assert.AreEqual(3, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndex_ComparerWithMultipleMatchesAfterStartIndex_IndexOfFirstMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyC" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyC" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, comparer);
      Assert.AreEqual(2, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndex_ComparerWithNoMatch_NoIndexOfIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyF" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, comparer);
      Assert.AreEqual(-1, result);
    }
    #endregion

    #region IndexOf (with start index and count) test cases
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithSingleMatchAfterStartIndexWithinCount_IndexOfMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" },
        new TestDummy { TextValue = "DummyF" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyC" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, 3, comparer);
      Assert.AreEqual(2, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithSingleMatchAtStartIndexWithinCount_IndexOfMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" },
        new TestDummy { TextValue = "DummyF" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyC" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 2, 3, comparer);
      Assert.AreEqual(2, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithSingleMatchAfterStartIndexAtCount_IndexOfMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" },
        new TestDummy { TextValue = "DummyF" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyD" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, 3, comparer);
      Assert.AreEqual(3, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithSingleMatchAfterStartIndexAfterCount_NoIndexIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" },
        new TestDummy { TextValue = "DummyF" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyE" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, 3, comparer);
      Assert.AreEqual(-1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithSingleMatchBeforeStartIndex_NoIndexIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" },
        new TestDummy { TextValue = "DummyF" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 2, 3, comparer);
      Assert.AreEqual(-1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithMatchBeforeAndAfterStartIndexWithinCount_IndexOfMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyE" },
        new TestDummy { TextValue = "DummyF" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 2, 3, comparer);
      Assert.AreEqual(3, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithMatchAfterStartIndexWithinAndAfterCount_IndexOfMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" },
        new TestDummy { TextValue = "DummyC" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyC" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, 3, comparer);
      Assert.AreEqual(2, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithMultipleMatchesAfterStartIndexWithinCount_IndexOfFirstMatchedInstanceIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyF" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyC" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 1, 5, comparer);
      Assert.AreEqual(2, result);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Collections.Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void IndexOfWithStartIndexAndCount_ComparerWithNoMatch_NoIndexIsReturned() {
      List<TestDummy> collection = new List<TestDummy> {
        new TestDummy { TextValue = "DummyA" },
        new TestDummy { TextValue = "DummyB" },
        new TestDummy { TextValue = "DummyC" },
        new TestDummy { TextValue = "DummyD" },
        new TestDummy { TextValue = "DummyE" },
        new TestDummy { TextValue = "DummyF" }
      };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyG" };

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      int result = collection.IndexOf(selectionDummy, 0, 6, comparer);
      Assert.AreEqual(-1, result);
    }
    #endregion

    #region Private helper classes
    /// <summary>A basic dummy class to support the testcases.</summary>
    private class TestDummy {
      /// <summary>Gets or sets a text value.</summary>
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      public int NumericValue { get; set; }

      /// <summary>Return the value of the TextValue property.</summary>
      /// <returns>The value of the TextValue property.</returns>
      public string RetrieveTestValue() {
        return this.TextValue;
      }
    }
    #endregion
  }
}
