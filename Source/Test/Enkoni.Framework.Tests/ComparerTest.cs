//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ComparerTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the Comparer-class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="Comparer{T}"/> class.</summary>
  [TestClass]
  public class ComparerTest {
    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a first-level property.</summary>
    [TestMethod]
    public void TestCase01_FirstLevelPropertyAccess() {
      Comparer<TestDummy> ascendingComparer = new Comparer<TestDummy>("TextValue");
      Comparer<TestDummy> descendingComparer = new Comparer<TestDummy>("TextValue", SortOrder.Descending);

      /* Situation 1: TextValue is equal */
      TestDummy dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      TestDummy dummyB = new TestDummy { TextValue = "Hello World", NumericValue = 6 };
      int result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);

      /* Situation 2: First TextValue is less than second TextValue */
      dummyA = new TestDummy { TextValue = "abc", NumericValue = 5 };
      dummyB = new TestDummy { TextValue = "def", NumericValue = 6 };
      result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);

      /* Situation 3: First TextValue is greater than second TextValue */
      dummyA = new TestDummy { TextValue = "def", NumericValue = 5 };
      dummyB = new TestDummy { TextValue = "abc", NumericValue = 6 };
      result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a second-level property.</summary>
    [TestMethod]
    public void TestCase02_SecondLevelPropertyAccess() {
      Comparer<TestDummy> ascendingComparer = new Comparer<TestDummy>("Leaf.TextValue");
      Comparer<TestDummy> descendingComparer = new Comparer<TestDummy>("Leaf.TextValue", SortOrder.Descending);

      /* Situation 1: TextValue is equal */
      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } };
      int result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);

      /* Situation 2: First TextValue is less than second TextValue */
      dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "abc" } };
      dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "def" } };
      result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);

      /* Situation 3: First TextValue is greater than second TextValue */
      dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "def" } };
      dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "abc" } };
      result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a third-level property.</summary>
    [TestMethod]
    public void TestCase03_ThirdLevelPropertyAccess() {
      Comparer<TestDummy> ascendingComparer = new Comparer<TestDummy>("Leaf.Leaf.TextValue");
      Comparer<TestDummy> descendingComparer = new Comparer<TestDummy>("Leaf.Leaf.TextValue", SortOrder.Descending);

      /* Situation 1: TextValue is equal */
      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } } };
      int result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);

      /* Situation 2: First TextValue is less than second TextValue */
      dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "abc" } } };
      dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "def" } } };
      result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);

      /* Situation 3: First TextValue is greater than second TextValue */
      dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "def" } } };
      dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "abc" } } };
      result = ascendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
      result = descendingComparer.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }

    #region Private helper classes
    /// <summary>A basic dummy class to support the testcases.</summary>
    private class TestDummy {
      /// <summary>Gets or sets a text value.</summary>
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      public int NumericValue { get; set; }

      /// <summary>Gets or sets a reference value.</summary>
      public SubTestDummy Leaf { get; set; }

      /// <summary>Return the value of the TexValue property.</summary>
      /// <returns>The value of the TextValue property.</returns>
      public string RetrieveTestValue() {
        return this.TextValue;
      }
    }

    /// <summary>A basic dummy class to support the testcases.</summary>
    private class SubTestDummy {
      /// <summary>Gets or sets a text value.</summary>
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      public int NumericValue { get; set; }

      /// <summary>Gets or sets a reference value.</summary>
      public SubTestDummy Leaf { get; set; }

      /// <summary>Return the value of the TexValue property.</summary>
      /// <returns>The value of the TextValue property.</returns>
      public string RetrieveTestValue() {
        return this.TextValue;
      }
    }
    #endregion
  }
}
