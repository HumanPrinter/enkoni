//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CollectionExtensionTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the extension methods for the ICollection and IList-interfaces and
//     the List class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the <see cref="ICollection{T}"/> and 
  /// <see cref="IList{T}"/> interfaces and the <see cref="List{T}"/> class.</summary>
  [TestClass]
  public class CollectionExtensionTest {
    /// <summary>Tests the functionality of the <see cref="Extensions.Remove{T}(ICollection{T}, T, IEqualityComparer{T})"/> extension method.
    /// </summary>
    [TestMethod]
    public void TestCase01_ICollection_Remove() {
      ICollection<TestDummy> collection = new List<TestDummy>();

      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      /* The default Remove-implementation will not be able to remove the object because it is a different reference */
      bool result = collection.Remove(selectionDummy);
      Assert.IsFalse(result);
      Assert.AreEqual(5, collection.Count);

      /* The extension method will be able to remove the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      result = collection.Remove(selectionDummy, comparer);

      Assert.IsTrue(result);
      Assert.AreEqual(4, collection.Count);
      Assert.IsTrue(collection.Contains(dummyA));
      Assert.IsFalse(collection.Contains(dummyB));
      Assert.IsTrue(collection.Contains(dummyC));
      Assert.IsTrue(collection.Contains(dummyD));
      Assert.IsTrue(collection.Contains(dummyE));
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.IndexOf{T}(IList{T}, T, IEqualityComparer{T})"/> extension method.</summary>
    [TestMethod]
    public void TestCase02_IList_IndexOf() {
      IList<TestDummy> collection = new List<TestDummy>();

      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      /* The default IndexOf-implementation will not be able to find the object because it is a different reference */
      int result = collection.IndexOf(selectionDummy);
      Assert.AreEqual(-1, result);

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      result = collection.IndexOf(selectionDummy, comparer);
      Assert.AreEqual(1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.IndexOf{T}(List{T}, T, int, IEqualityComparer{T})"/> extension method.
    /// </summary>
    [TestMethod]
    public void TestCase03_List_IndexOfWithStartIndex() {
      List<TestDummy> collection = new List<TestDummy>();

      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      /* The default IndexOf-implementation will not be able to find the object because it is a different reference regardless of the startindex */
      int result = collection.IndexOf(selectionDummy, 3);
      Assert.AreEqual(-1, result);
      result = collection.IndexOf(selectionDummy, 1);
      Assert.AreEqual(-1, result);

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      result = collection.IndexOf(selectionDummy, 3, comparer);
      Assert.AreEqual(-1, result);
      result = collection.IndexOf(selectionDummy, 1, comparer);
      Assert.AreEqual(1, result);
    }

    /// <summary>Tests the functionality of the <see cref="Extensions.IndexOf{T}(List{T}, T, int, int, IEqualityComparer{T})"/> extension method.
    /// </summary>
    [TestMethod]
    public void TestCase04_List_IndexOfWithStartIndexAndCount() {
      List<TestDummy> collection = new List<TestDummy>();

      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      TestDummy selectionDummy = new TestDummy { TextValue = "DummyB" };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      /* The default IndexOf-implementation will not be able to find the object because it is a different reference regardless of the startindex */
      int result = collection.IndexOf(selectionDummy, 3, 2);
      Assert.AreEqual(-1, result);
      result = collection.IndexOf(selectionDummy, 1, 3);
      Assert.AreEqual(-1, result);

      /* The extension method will be able to find the object because it uses a custom comparer */
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);
      result = collection.IndexOf(selectionDummy, 3, 2, comparer);
      Assert.AreEqual(-1, result);
      result = collection.IndexOf(selectionDummy, 1, 3, comparer);
      Assert.AreEqual(1, result);
    }

    #region Private helper classes
    /// <summary>A basic dummy class to support the testcases.</summary>
    private class TestDummy {
      /// <summary>Gets or sets a text value.</summary>
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      public int NumericValue { get; set; }

      /// <summary>Return the value of the TexValue property.</summary>
      /// <returns>The value of the TextValue property.</returns>
      public string RetrieveTestValue() {
        return this.TextValue;
      }
    }
    #endregion
  }
}
