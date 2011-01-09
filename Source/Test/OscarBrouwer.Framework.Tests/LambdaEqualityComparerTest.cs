//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaEqualityComparerTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the LambdaEqualityComparer-class.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OscarBrouwer.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the 
  /// <see cref="LambdaEqualityComparer{T,TField}"/> class.</summary>
  [TestClass]
  public class LambdaEqualityComparerTest {
    /// <summary>Tests the functionality of the <see cref="LambdaEqualityComparer{T,TField}"/> class when comparing to 
    /// objects based on a first-level property.</summary>
    [TestMethod]
    public void TestCase1_DirectPropertyAccess() {
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.TextValue);

      /* Situation 1: TextValue is equal, numeric value is not equal */
      TestDummy dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      TestDummy dummyB = new TestDummy { TextValue = "Hello World", NumericValue = 6 };
      bool result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);

      /* Situation 2: TextValue is not equal, numeric value is not equal */
      dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      dummyB = new TestDummy { TextValue = "hello world", NumericValue = 6 };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 3: First TextValue is null, numeric value is not equal */
      dummyA = new TestDummy { TextValue = null, NumericValue = 5 };
      dummyB = new TestDummy { TextValue = "hello world", NumericValue = 6 };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 4: Second TextValue is null, numeric value is not equal */
      dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      dummyB = new TestDummy { TextValue = null, NumericValue = 6 };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 5: Both TextValues are null, numeric value is not equal */
      dummyA = new TestDummy { TextValue = null, NumericValue = 5 };
      dummyB = new TestDummy { TextValue = null, NumericValue = 6 };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="LambdaEqualityComparer{T,TField}"/> class when comparing to 
    /// objects based on a second-level property.</summary>
    [TestMethod]
    public void TestCase2_TreePropertyAccess() {
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.Leaf.TextValue);

      /* Situation 1: TextValue is equal, leaf.TextValue is equal */
      TestDummy dummyA = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "Goodmorning" } };
      TestDummy dummyB = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "Goodmorning" } };
      bool result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);

      /* Situation 2: TextValue is equal, leaf.TextValue is not equal */
      dummyA = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "Goodmorning" } };
      dummyB = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "goodMorning" } };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 3: TextValue is equal, first leaf.TextValue is null */
      dummyA = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = null } };
      dummyB = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "Goodmorning" } };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 4: TextValue is equal, second leaf.TextValue is null */
      dummyA = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "Goodmorning" } };
      dummyB = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = null } };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 5: TextValue is equal, both leaf.TextValues are null */
      dummyA = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = null } };
      dummyB = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = null } };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="LambdaEqualityComparer{T,TField}"/> class when comparing to 
    /// objects based on a first-level method.</summary>
    [TestMethod]
    public void TestCase3_MethodAccess() {
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.RetrieveTestValue());

      /* Situation 1: TextValue is equal, therefore result of RetrieveTestValue is equal */
      TestDummy dummyA = new TestDummy { TextValue = "Hello World" };
      TestDummy dummyB = new TestDummy { TextValue = "Hello World" };
      bool result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);

      /* Situation 2: TextValue is not equal, therefore result of RetrieveTestValue is not equal */
      dummyA = new TestDummy { TextValue = "Hello World" };
      dummyB = new TestDummy { TextValue = "hello world" };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="LambdaEqualityComparer{T,TField}"/> class when comparing to 
    /// objects based on a second-level method.</summary>
    [TestMethod]
    public void TestCase4_TreeMethodAccess() {
      LambdaEqualityComparer<TestDummy, string> comparer = new LambdaEqualityComparer<TestDummy, string>(td => td.Leaf.RetrieveTestValue());

      /* Situation 1: leaf.TextValue is equal, therefore result of leaf.RetrieveTestValue is equal */
      TestDummy dummyA = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "Goodmorning" } };
      TestDummy dummyB = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "Goodmorning" } };
      bool result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);

      /* Situation 2: leaf.TextValue is not equal, therefore result of leaf.RetrieveTestValue is not equal */
      dummyA = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "Goodmorning" } };
      dummyB = new TestDummy { TextValue = "Hello World", Leaf = new SubTestDummy { TextValue = "goodMorning" } };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);
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

      /// <summary>Return the value of the TexValue property.</summary>
      /// <returns>The value of the TextValue property.</returns>
      public string RetrieveTestValue() {
        return this.TextValue;
      }
    }
    #endregion
  }
}
