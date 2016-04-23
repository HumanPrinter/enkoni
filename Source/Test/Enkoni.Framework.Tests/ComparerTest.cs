using Enkoni.Framework.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="Comparer{T}"/> class.</summary>
  [TestClass]
  public class ComparerTest {
    #region Compare - FirstLevel Property Access
    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a first-level property.</summary>
    [TestMethod]
    public void Compare_FirstLevelPropertyAscending_LeftEqualToRight_ZeroIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("TextValue");

      TestDummy dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      TestDummy dummyB = new TestDummy { TextValue = "Hello World", NumericValue = 6 };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a first-level property.</summary>
    [TestMethod]
    public void Compare_FirstLevelPropertyAscending_LeftLowerThanRight_NegativeNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("TextValue");

      TestDummy dummyA = new TestDummy { TextValue = "abc", NumericValue = 5 };
      TestDummy dummyB = new TestDummy { TextValue = "def", NumericValue = 6 };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a first-level property.</summary>
    [TestMethod]
    public void Compare_FirstLevelPropertyAscending_LeftGreaterThanRight_PositiveNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("TextValue");

      TestDummy dummyA = new TestDummy { TextValue = "def", NumericValue = 5 };
      TestDummy dummyB = new TestDummy { TextValue = "abc", NumericValue = 6 };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a first-level property.</summary>
    [TestMethod]
    public void Compare_FirstLevelPropertyDescending_LeftEqualToRight_ZeroIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("TextValue", SortOrder.Descending);

      TestDummy dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      TestDummy dummyB = new TestDummy { TextValue = "Hello World", NumericValue = 6 };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a first-level property.</summary>
    [TestMethod]
    public void Compare_FirstLevelPropertyDescending_LeftLowerThanRight_PositiveNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("TextValue", SortOrder.Descending);

      TestDummy dummyA = new TestDummy { TextValue = "abc", NumericValue = 5 };
      TestDummy dummyB = new TestDummy { TextValue = "def", NumericValue = 6 };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a first-level property.</summary>
    [TestMethod]
    public void Compare_FirstLevelPropertyDescending_LeftGreaterThanRight_NegativeNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("TextValue", SortOrder.Descending);

      TestDummy dummyA = new TestDummy { TextValue = "def", NumericValue = 5 };
      TestDummy dummyB = new TestDummy { TextValue = "abc", NumericValue = 6 };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }
    #endregion

    #region Compare - SecondLevel Property Access
    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a second-level property.</summary>
    [TestMethod]
    public void Compare_SecondLevelPropertyAscending_LeftEqualToRight_ZeroIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.TextValue");

      /* Situation 1: TextValue is equal */
      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a second-level property.</summary>
    [TestMethod]
    public void Compare_SecondLevelPropertyAscending_LeftLowerThanRight_NegativeNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.TextValue");

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "abc" } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "def" } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a second-level property.</summary>
    [TestMethod]
    public void Compare_SecondLevelPropertyAscending_LeftGreaterThanRight_PositiveNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.TextValue");

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "def" } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "abc" } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a second-level property.</summary>
    [TestMethod]
    public void Compare_SecondLevelPropertyDescending_LeftEqualToRight_ZeroIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.TextValue", SortOrder.Descending);

      /* Situation 1: TextValue is equal */
      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a second-level property.</summary>
    [TestMethod]
    public void Compare_SecondLevelPropertyDescending_LeftLowerThanRight_PositiveNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.TextValue", SortOrder.Descending);

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "abc" } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "def" } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a second-level property.</summary>
    [TestMethod]
    public void Compare_SecondLevelPropertyDescending_LeftGreaterThanRight_NegativeNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.TextValue", SortOrder.Descending);

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { TextValue = "def" } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { TextValue = "abc" } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }
    #endregion

    #region Compare - ThirdLevel Property Access
    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a third-level property.</summary>
    [TestMethod]
    public void Compare_ThirdLevelPropertyAscending_LeftEqualToRight_ZeroIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.Leaf.TextValue");

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a third-level property.</summary>
    [TestMethod]
    public void Compare_ThirdLevelPropertyAscending_LeftLowerThanRight_NegativeNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.Leaf.TextValue");

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "abc" } } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "def" } } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a third-level property.</summary>
    [TestMethod]
    public void Compare_ThirdLevelPropertyAscending_LeftGreaterThanRight_PositiveNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.Leaf.TextValue");

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "def" } } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "abc" } } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a third-level property.</summary>
    [TestMethod]
    public void Compare_ThirdLevelPropertyDescending_LeftEqualToRight_ZeroIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.Leaf.TextValue", SortOrder.Descending);

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "Hello World" } } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreEqual(0, result);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a third-level property.</summary>
    [TestMethod]
    public void Compare_ThirdLevelPropertyDescending_LeftLowerThanRight_PostiveNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.Leaf.TextValue", SortOrder.Descending);

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "abc" } } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "def" } } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result > 0);
    }

    /// <summary>Tests the functionality of the <see cref="Comparer{T}"/> class when comparing to objects based on a third-level property.</summary>
    [TestMethod]
    public void Compare_ThirdLevelPropertyDescending_LeftGreaterThanRight_NegativeNumberIsReturned() {
      Comparer<TestDummy> testSubject = new Comparer<TestDummy>("Leaf.Leaf.TextValue", SortOrder.Descending);

      TestDummy dummyA = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "def" } } };
      TestDummy dummyB = new TestDummy { Leaf = new SubTestDummy { Leaf = new SubTestDummy { TextValue = "abc" } } };
      int result = testSubject.Compare(dummyA, dummyB);
      Assert.AreNotEqual(0, result);
      Assert.IsTrue(result < 0);
    }
    #endregion

    #region Private helper classes
    /// <summary>A basic dummy class to support the testcases.</summary>
    private class TestDummy {
      /// <summary>Gets or sets a text value.</summary>
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      public int NumericValue { get; set; }

      /// <summary>Gets or sets a reference value.</summary>
      public SubTestDummy Leaf { get; set; }

      /// <summary>Return the value of the TextValue property.</summary>
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

      /// <summary>Return the value of the TextValue property.</summary>
      /// <returns>The value of the TextValue property.</returns>
      public string RetrieveTestValue() {
        return this.TextValue;
      }
    }
    #endregion
  }
}
