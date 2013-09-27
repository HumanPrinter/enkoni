//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="LinqExtensionTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the extension methods for the IEnumerable interface.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using Enkoni.Framework.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the <see cref="IEnumerable{T}"/> interface.
  /// </summary>
  [TestClass]
  public class LinqExtensionTest {
    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Extensions.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    public void TestCase01_SingleOrDefault() {
      List<TestDummy> collection = new List<TestDummy>();

      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));

      result = collection.SingleOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Extensions.ElementAtOrDefault{T}(IEnumerable{T}, int, T)"/> 
    /// extension method.</summary>
    [TestMethod]
    public void TestCase02_ElementAtOrDefault() {
      List<TestDummy> collection = new List<TestDummy>();

      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      TestDummy result = collection.ElementAtOrDefault(5, defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));

      result = collection.ElementAtOrDefault(1, defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Extensions.FirstOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Extensions.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void TestCase03_FirstOrDefault() {
      List<TestDummy> collection = new List<TestDummy>();

      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      TestDummy result = collection.FirstOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));

      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      result = collection.FirstOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));

      result = collection.FirstOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));

      result = collection.FirstOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyA, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Extensions.LastOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Extensions.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void TestCase04_LastOrDefault() {
      List<TestDummy> collection = new List<TestDummy>();

      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      TestDummy result = collection.LastOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));

      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      result = collection.LastOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));

      result = collection.LastOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));

      result = collection.LastOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyE, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Extensions.ForEach{T}(IEnumerable{T}, System.Action{T})"/> extension 
    /// method.</summary>
    [TestMethod]
    public void TestCase05_ForEach() {
      List<TestDummy> collection = new List<TestDummy>();

      TestDummy dummyA = new TestDummy { NumericValue = 1 };
      TestDummy dummyB = new TestDummy { NumericValue = 2 };
      TestDummy dummyC = new TestDummy { NumericValue = 3 };
      TestDummy dummyD = new TestDummy { NumericValue = 4 };
      TestDummy dummyE = new TestDummy { NumericValue = 5 };

      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);

      collection.ForEach(td => td.NumericValue += 1);
      Assert.IsNotNull(collection);
      Assert.AreEqual(5, collection.Count);
      Assert.AreEqual(2, dummyA.NumericValue);
      Assert.AreEqual(3, dummyB.NumericValue);
      Assert.AreEqual(4, dummyC.NumericValue);
      Assert.AreEqual(5, dummyD.NumericValue);
      Assert.AreEqual(6, dummyE.NumericValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Extensions.CreateEqualityComparer{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void TestCase06_CreateEqualityComparer() {
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValue" };
      TestDummy dummyB = new TestDummy { NumericValue = 2, TextValue = "AnotherValue" };
      TestDummy dummyC = new TestDummy { NumericValue = 1, TextValue = "MyValue" };
      TestDummy dummyD = new TestDummy { NumericValue = 4, TextValue = "TestValue" };
      TestDummy dummyE = new TestDummy { NumericValue = 1, TextValue = "TestingValue" };
      var anonymousDummyA = new { OtherTextValue = dummyA.TextValue.Reverse(), Length = dummyA.TextValue.Length };
      var anonymousDummyB = new { OtherTextValue = dummyB.TextValue.Reverse(), Length = dummyB.TextValue.Length };
      var anonymousDummyC = new { OtherTextValue = dummyC.TextValue.Reverse(), Length = dummyC.TextValue.Length };
      var anonymousDummyD = new { OtherTextValue = dummyD.TextValue.Reverse(), Length = dummyD.TextValue.Length };
      var anonymousDummyE = new { OtherTextValue = dummyE.TextValue.Reverse(), Length = dummyE.TextValue.Length };

      /* Test the method using a null-reference as source */
      List<TestDummy> collection = null;
      IEqualityComparer<TestDummy> comparer = collection.CreateEqualityComparer(td => td.NumericValue);

      Assert.IsNotNull(comparer);
      Assert.IsFalse(comparer.Equals(dummyA, dummyB));
      Assert.IsTrue(comparer.Equals(dummyA, dummyC));
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working instance of 
       * 'IEqualityComparer<T>' */

      /* Test the method using an empty collection as source */
      collection = new List<TestDummy>();
      var anonymousCollection = collection.Select(td => new { OtherTextValue = td.TextValue.Reverse(), Length = td.TextValue.Length });
      comparer = collection.CreateEqualityComparer(td => td.NumericValue);
      var anonymousComparer = anonymousCollection.CreateEqualityComparer(a => a.Length);

      Assert.IsNotNull(comparer);
      Assert.IsNotNull(anonymousComparer);
      Assert.IsFalse(comparer.Equals(dummyA, dummyB));
      Assert.IsFalse(anonymousComparer.Equals(anonymousDummyA, anonymousDummyB));
      Assert.IsTrue(comparer.Equals(dummyA, dummyC));
      Assert.IsTrue(anonymousComparer.Equals(anonymousDummyB, anonymousDummyE));

      /* Test the method using a filled collection as source */
      collection.Add(dummyA);
      collection.Add(dummyB);
      collection.Add(dummyC);
      collection.Add(dummyD);
      collection.Add(dummyE);
      anonymousCollection = collection.Select(td => new { OtherTextValue = td.TextValue.Reverse(), Length = td.TextValue.Length });
      comparer = collection.CreateEqualityComparer(td => td.NumericValue);
      anonymousComparer = anonymousCollection.CreateEqualityComparer(a => a.Length);

      Assert.IsNotNull(comparer);
      Assert.IsNotNull(anonymousComparer);
      Assert.IsFalse(comparer.Equals(dummyA, dummyB));
      Assert.IsFalse(anonymousComparer.Equals(anonymousDummyA, anonymousDummyB));
      Assert.IsTrue(comparer.Equals(dummyA, dummyC));
      Assert.IsTrue(anonymousComparer.Equals(anonymousDummyB, anonymousDummyE));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Extensions.Distinct{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void TestCase07_Distinct() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      List<TestDummy> collection = new List<TestDummy>();
      var anonymousCollection = collection.Select(td => new { OtherTextValue = td.TextValue.Reverse(), Length = td.TextValue.Length });
      IEnumerable<TestDummy> distinctCollection = collection.Distinct(td => td.NumericValue);
      var distinctAnonymousCollection = anonymousCollection.Distinct(a => a.Length);

      Assert.IsNotNull(distinctCollection);
      Assert.IsNotNull(distinctAnonymousCollection);
      Assert.AreEqual(0, distinctCollection.Count());
      Assert.AreEqual(0, distinctAnonymousCollection.Count());

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValue" };
      TestDummy dummyB = new TestDummy { NumericValue = 2, TextValue = "AnotherValue" };
      TestDummy dummyC = new TestDummy { NumericValue = 1, TextValue = "MyValue" };
      TestDummy dummyD = new TestDummy { NumericValue = 4, TextValue = "TestValue" };
      TestDummy dummyE = new TestDummy { NumericValue = 1, TextValue = "TestingValue" };
      collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      anonymousCollection = collection.Select(td => new { OtherTextValue = td.TextValue.Reverse(), Length = td.TextValue.Length });
      distinctCollection = collection.Distinct(td => td.NumericValue);
      distinctAnonymousCollection = anonymousCollection.Distinct(a => a.Length);

      Assert.IsNotNull(distinctCollection);
      Assert.IsNotNull(distinctAnonymousCollection);

      Assert.AreEqual(3, distinctCollection.Count());
      Assert.AreEqual(1, distinctCollection.Count(d => d.NumericValue == 1));
      Assert.AreEqual(1, distinctCollection.Count(d => d.NumericValue == 2));
      Assert.AreEqual(1, distinctCollection.Count(d => d.NumericValue == 4));
      Assert.AreEqual(3, distinctAnonymousCollection.Count());
      Assert.AreEqual(1, distinctAnonymousCollection.Count(d => d.Length == 7));
      Assert.AreEqual(1, distinctAnonymousCollection.Count(d => d.Length == 9));
      Assert.AreEqual(1, distinctAnonymousCollection.Count(d => d.Length == 12));
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
