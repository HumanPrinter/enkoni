//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="LinqExtensionTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the extension methods for the IEnumerable interface.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

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
