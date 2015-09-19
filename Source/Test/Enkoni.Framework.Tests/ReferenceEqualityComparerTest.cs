using System;

using Enkoni.Framework.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the <see cref="ReferenceEqualityComparer{T}"/> class.</summary>
  [TestClass]
  public class ReferenceEqualityComparerTest {
    /// <summary>Tests the functionality of the <see cref="ReferenceEqualityComparer{T}"/> class when comparing two reference-type objects.</summary>
    [TestMethod]
    public void TestCase01_CompareReferenceTypes() {
      ReferenceEqualityComparer<TestDummy> comparer = new ReferenceEqualityComparer<TestDummy>();

      /* Situation 1: Comparing two variables that point to the same instance */
      TestDummy dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      TestDummy dummyB = dummyA;
      bool result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);

      /* Situation 2: Comparing two variables that point to different instances with different contents */
      dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      dummyB = new TestDummy { TextValue = "hello world", NumericValue = 6 };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 3: Comparing two variables that point to different instances with the same content */
      dummyA = new TestDummy { TextValue = "hello world", NumericValue = 5 };
      dummyB = new TestDummy { TextValue = "hello world", NumericValue = 5 };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 4: Comparing an instance with its clone */
      dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      dummyB = dummyA.Clone<TestDummy>();
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 5: Comparing an instance with a null-value */
      dummyA = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      dummyB = null;
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 6: Comparing a null-value with an instance */
      dummyA = null;
      dummyB = new TestDummy { TextValue = "Hello World", NumericValue = 5 };
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 6: Comparing two null-values */
      dummyA = null;
      dummyB = null;
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);
    }

    /// <summary>Tests the functionality of the <see cref="ReferenceEqualityComparer{T}"/> class when comparing two value-type objects.</summary>
    [TestMethod]
    public void TestCase02_CompareValueTypes() {
      ReferenceEqualityComparer<DateTime> comparer = new ReferenceEqualityComparer<DateTime>();

      /* Situation 1: Comparing two variables that point to different instances with different contents */
      DateTime dummyA = new DateTime(2000, 5, 10);
      DateTime dummyB = new DateTime(2000, 6, 9);
      bool result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 2: Comparing two variables that point to different instances with the same content */
      dummyA = new DateTime(2000, 5, 10);
      dummyB = new DateTime(2000, 5, 10);
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);
    }

    /// <summary>Tests the functionality of the <see cref="ReferenceEqualityComparer{T}"/> class when comparing two nullable value-type objects.
    /// </summary>
    [TestMethod]
    public void TestCase03_CompareNullableValueTypes() {
      ReferenceEqualityComparer<DateTime?> comparer = new ReferenceEqualityComparer<DateTime?>();

      /* Situation 1: Comparing two variables that point to the same instance (note: a nullable value-type is still a value-type) */
      DateTime? dummyA = new DateTime?(new DateTime(2000, 5, 10));
      DateTime? dummyB = dummyA;
      bool result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 2: Comparing two variables that point to different instances with different contents */
      dummyA = new DateTime?(new DateTime(2000, 5, 10));
      dummyB = new DateTime?(new DateTime(2000, 6, 9));
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 3: Comparing two variables that point to different instances with the same content */
      dummyA = new DateTime?(new DateTime(2000, 5, 10));
      dummyB = new DateTime?(new DateTime(2000, 5, 10));
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 4: Comparing an instance with its 'clone' */
      DateTime testValue = new DateTime(2000, 5, 10);
      dummyA = new DateTime?(testValue);
      dummyB = new DateTime?(testValue);
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 5: Comparing an instance with a null-value */
      dummyA = new DateTime?(new DateTime(2000, 5, 10));
      dummyB = null;
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 6: Comparing a null-value with an instance */
      dummyA = null;
      dummyB = new DateTime?(new DateTime(2000, 5, 10));
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsFalse(result);

      /* Situation 6: Comparing two null-values */
      dummyA = null;
      dummyB = null;
      result = comparer.Equals(dummyA, dummyB);
      Assert.IsTrue(result);
    }

    #region Private helper classes
    /// <summary>A basic dummy class to support the testcases.</summary>
    private class TestDummy : ICloneable {
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

      /// <summary>Creates and returns a clone of this instance.</summary>
      /// <returns>The copy of this instance.</returns>
      public object Clone() {
        TestDummy clone = this.MemberwiseClone() as TestDummy;
        if(this.Leaf != null) {
          clone.Leaf = this.Leaf.Clone<SubTestDummy>();
        }

        return clone;
      }
    }

    /// <summary>A basic dummy class to support the testcases.</summary>
    private class SubTestDummy : ICloneable {
      /// <summary>Gets or sets a text value.</summary>
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      public int NumericValue { get; set; }

      /// <summary>Return the value of the TextValue property.</summary>
      /// <returns>The value of the TextValue property.</returns>
      public string RetrieveTestValue() {
        return this.TextValue;
      }

      /// <summary>Creates and returns a clone of this instance.</summary>
      /// <returns>A deep-copy of this instance.</returns>
      public object Clone() {
        return this.MemberwiseClone();
      }
    }
    #endregion
  }
}
