using System;
using System.Collections.Generic;
using System.Linq;

using Enkoni.Framework.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Tests {
  /// <summary>This class defines the testcases that test the functionality of the extension methods for the <see cref="IEnumerable{T}"/> interface.
  /// </summary>
  [TestClass]
  public class LinqExtensionTest {
    #region SingleOrDefault test-cases
    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    public void LinqExtensions_SingleOrDefaultOnIEnumerable_MatchingPredicate_MatchingRecordIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      
      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    public void LinqExtensions_SingleOrDefaultOnIEnumerable_NotMatchingPredicate_DefaultIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    public void LinqExtensions_SingleOrDefaultOnIEnumerable_EmptySource_DefaultIsReturned() {
      IEnumerable<TestDummy> collection = new List<TestDummy>();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_SingleOrDefaultOnIEnumerable_NullSource_ExceptionIsThrown() {
      IEnumerable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyG", defaultDummy);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_SingleOrDefaultOnIEnumerable_NullPredicate_ExceptionIsThrown() {
      IEnumerable<TestDummy> collection = new List<TestDummy>();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(null, defaultDummy);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    public void LinqExtensions_SingleOrDefaultOnIQueryable_MatchingPredicate_MatchingRecordIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IQueryable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE }.AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    public void LinqExtensions_SingleOrDefaultOnIQueryable_NotMatchingPredicate_DefaultIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IQueryable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE }.AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    public void LinqExtensions_SingleOrDefaultOnIQueryable_EmptySource_DefaultIsReturned() {
      IQueryable<TestDummy> collection = new List<TestDummy>().AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_SingleOrDefaultOnIQueryable_NullSource_ExceptionIsThrown() {
      IQueryable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(td => td.TextValue == "DummyG", defaultDummy);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.SingleOrDefault{T}(IEnumerable{T}, System.Func{T, bool}, T)"/> extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_SingleOrDefaultOnIQueryable_NullPredicate_ExceptionIsThrown() {
      IQueryable<TestDummy> collection = new List<TestDummy>().AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.SingleOrDefault(null, defaultDummy);
    }
    #endregion

    #region ElementAtOrDefault test-cases
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.ElementAtOrDefault{T}(IEnumerable{T}, int, T)"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_ElementAtOrDefault_ValidIndex_ItemAtIndexIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      
      TestDummy result = collection.ElementAtOrDefault(1, defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.ElementAtOrDefault{T}(IEnumerable{T}, int, T)"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_ElementAtOrDefault_InvalidIndex_DefaultIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.ElementAtOrDefault(5, defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.ElementAtOrDefault{T}(IEnumerable{T}, int, T)"/> 
    /// extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_ElementAtOrDefault_NullSource_ExceptionIsThrown() {
      IEnumerable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.ElementAtOrDefault(5, defaultDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.ElementAtOrDefault{T}(IEnumerable{T}, int, T)"/> 
    /// extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentOutOfRangeException))]
    public void LinqExtensions_ElementAtOrDefault_NegativeIndex_ExceptionIsThrown() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.ElementAtOrDefault(-5, defaultDummy);
    }
    #endregion

    #region FirstOrDefault test-cases
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_FirstOrDefaultOnIEnumerable_NoPredicate_FirstItemIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyA, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_FirstOrDefaultOnIEnumerable_EmptySource_DefaultIsReturned() {
      IEnumerable<TestDummy> collection = new List<TestDummy>();

      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      TestDummy result = collection.FirstOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_FirstOrDefaultOnIEnumerable_MatchingPredicate_MatchingItemIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      
      TestDummy result = collection.FirstOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_FirstOrDefaultOnIEnumerable_NotMatchingPredicate_DefaultIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_FirstOrDefaultOnIEnumerable_NullSource_ExceptionIsTrown() {
      IEnumerable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(td => td.TextValue == "DummyG", defaultDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_FirstOrDefaultOnIEnumerable_NoPredicateNullSource_ExceptionIsTrown() {
      IEnumerable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(defaultDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_FirstOrDefaultOnIEnumerable_NullPredicate_ExceptionIsTrown() {
      IEnumerable<TestDummy> collection = new List<TestDummy>();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(null, defaultDummy);
    }
    
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_FirstOrDefaultOnIQueryable_NoPredicate_FirstItemIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IQueryable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE }.AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyA, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_FirstOrDefaultOnIQueryable_EmptySource_DefaultIsReturned() {
      IQueryable<TestDummy> collection = new List<TestDummy>().AsQueryable();

      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      TestDummy result = collection.FirstOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_FirstOrDefaultOnIQueryable_MatchingPredicate_MatchingItemIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IQueryable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE }.AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_FirstOrDefaultOnIQueryable_NotMatchingPredicate_DefaultIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IQueryable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE }.AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_FirstOrDefaultOnIQueryable_NullSource_ExceptionIsTrown() {
      IQueryable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(td => td.TextValue == "DummyG", defaultDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.FirstOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_FirstOrDefaultOnIQueryable_NullPredicate_ExceptionIsTrown() {
      IQueryable<TestDummy> collection = new List<TestDummy>().AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.FirstOrDefault(null, defaultDummy);
    }
    #endregion

    #region LastOrDefault test-cases
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_LastOrDefaultOnIEnumerable_NoPredicate_FirstItemIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyE, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_LastOrDefaultOnIEnumerable_EmptySource_DefaultIsReturned() {
      IEnumerable<TestDummy> collection = new List<TestDummy>();

      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      TestDummy result = collection.LastOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_LastOrDefaultOnIEnumerable_MatchingPredicate_MatchingItemIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_LastOrDefaultOnIEnumerable_NotMatchingPredicate_DefaultIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_LastOrDefaultOnIEnumerable_NullSource_ExceptionIsTrown() {
      IEnumerable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(td => td.TextValue == "DummyG", defaultDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_LastOrDefaultOnIEnumerable_NoPredicateNullSource_ExceptionIsTrown() {
      IEnumerable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(defaultDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, T)"/> and 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_LastOrDefaultOnIEnumerable_NullPredicate_ExceptionIsTrown() {
      IEnumerable<TestDummy> collection = new List<TestDummy>();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(null, defaultDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_LastOrDefaultOnIQueryable_NoPredicate_FirstItemIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IQueryable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE }.AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyE, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_LastOrDefaultOnIQueryable_EmptySource_DefaultIsReturned() {
      IQueryable<TestDummy> collection = new List<TestDummy>().AsQueryable();

      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };
      TestDummy result = collection.LastOrDefault(defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_LastOrDefaultOnIQueryable_MatchingPredicate_MatchingItemIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IQueryable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE }.AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(td => td.TextValue == "DummyB", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(dummyB, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    public void LinqExtensions_LastOrDefaultOnIQueryable_NotMatchingPredicate_DefaultIsReturned() {
      TestDummy dummyA = new TestDummy { TextValue = "DummyA" };
      TestDummy dummyB = new TestDummy { TextValue = "DummyB" };
      TestDummy dummyC = new TestDummy { TextValue = "DummyC" };
      TestDummy dummyD = new TestDummy { TextValue = "DummyD" };
      TestDummy dummyE = new TestDummy { TextValue = "DummyE" };

      IQueryable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE }.AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(td => td.TextValue == "DummyG", defaultDummy);
      Assert.IsNotNull(result);
      Assert.IsTrue(object.ReferenceEquals(defaultDummy, result));
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_LastOrDefaultOnIQueryable_NullSource_ExceptionIsTrown() {
      IQueryable<TestDummy> collection = null;
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(td => td.TextValue == "DummyG", defaultDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.LastOrDefault{T}(IEnumerable{T}, System.Func{T,bool}, T)"/> 
    /// extension methods.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_LastOrDefaultOnIQueryable_NullPredicate_ExceptionIsTrown() {
      IQueryable<TestDummy> collection = new List<TestDummy>().AsQueryable();
      TestDummy defaultDummy = new TestDummy { TextValue = "DefaultDummy" };

      TestDummy result = collection.LastOrDefault(null, defaultDummy);
    }
    #endregion

    #region ForEach test-cases
    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.ForEach{T}(IEnumerable{T}, System.Action{T})"/> extension 
    /// method.</summary>
    [TestMethod]
    public void LinqExtensions_ForEach_ActionIsExecutedOnAllItems() {
      TestDummy dummyA = new TestDummy { NumericValue = 1 };
      TestDummy dummyB = new TestDummy { NumericValue = 2 };
      TestDummy dummyC = new TestDummy { NumericValue = 3 };
      TestDummy dummyD = new TestDummy { NumericValue = 4 };
      TestDummy dummyE = new TestDummy { NumericValue = 5 };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      
      collection.ForEach(td => td.NumericValue += 1);
      
      Assert.IsNotNull(collection);
      Assert.AreEqual(5, collection.Count());
      Assert.AreEqual(2, dummyA.NumericValue);
      Assert.AreEqual(3, dummyB.NumericValue);
      Assert.AreEqual(4, dummyC.NumericValue);
      Assert.AreEqual(5, dummyD.NumericValue);
      Assert.AreEqual(6, dummyE.NumericValue);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.ForEach{T}(IEnumerable{T}, System.Action{T})"/> extension 
    /// method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_ForEach_NullSource_ExceptionIsThrown() {
      IEnumerable<TestDummy> collection = null;

      collection.ForEach(td => td.NumericValue += 1);
    }

    /// <summary>Tests the functionality of the <see cref="Enkoni.Framework.Linq.Enumerable.ForEach{T}(IEnumerable{T}, System.Action{T})"/> extension 
    /// method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_ForEach_NullAction_ExceptionIsThrown() {
      TestDummy dummyA = new TestDummy { NumericValue = 1 };
      TestDummy dummyB = new TestDummy { NumericValue = 2 };
      TestDummy dummyC = new TestDummy { NumericValue = 3 };
      TestDummy dummyD = new TestDummy { NumericValue = 4 };
      TestDummy dummyE = new TestDummy { NumericValue = 5 };

      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };

      collection.ForEach(null);
    }
    #endregion

    #region CreateEqualityComparer test-cases
    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.CreateEqualityComparer{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_CreateEqualityComparer_ConcreteType_ComparerIsCreated() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working instance of 
       * 'IEqualityComparer<T>' */

      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValue" };
      TestDummy dummyB = new TestDummy { NumericValue = 2, TextValue = "AnotherValue" };
      TestDummy dummyC = new TestDummy { NumericValue = 1, TextValue = "MyValue" };
      
      /* Test the method using a null-reference as source */
      List<TestDummy> collection = null;
      IEqualityComparer<TestDummy> comparer = collection.CreateEqualityComparer(td => td.NumericValue);

      Assert.IsNotNull(comparer);
      Assert.IsFalse(comparer.Equals(dummyA, dummyB));
      Assert.IsTrue(comparer.Equals(dummyA, dummyC));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.CreateEqualityComparer{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_CreateEqualityComparer_AnonymousType_ComparerIsCreated() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working instance of 
       * 'IEqualityComparer<T>' */

      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValue" };
      TestDummy dummyB = new TestDummy { NumericValue = 2, TextValue = "AnotherValue" };
      TestDummy dummyC = new TestDummy { NumericValue = 1, TextValue = "TestingValue" };
      var anonymousDummyA = new { OtherTextValue = dummyA.TextValue.Reverse(), Length = dummyA.TextValue.Length };
      var anonymousDummyB = new { OtherTextValue = dummyB.TextValue.Reverse(), Length = dummyB.TextValue.Length };
      var anonymousDummyC = new { OtherTextValue = dummyC.TextValue.Reverse(), Length = dummyC.TextValue.Length };

      /* Test the method using an empty collection as source */
      List<TestDummy> collection = new List<TestDummy>();
      var anonymousCollection = collection.Select(td => new { OtherTextValue = td.TextValue.Reverse(), Length = td.TextValue.Length });
      var anonymousComparer = anonymousCollection.CreateEqualityComparer(a => a.Length);

      Assert.IsNotNull(anonymousComparer);
      Assert.IsFalse(anonymousComparer.Equals(anonymousDummyA, anonymousDummyB));
      Assert.IsTrue(anonymousComparer.Equals(anonymousDummyB, anonymousDummyC));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.CreateEqualityComparer{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_CreateEqualityComparer_NullAction_ExceptionIsThrown() {
      /* Test the method using a null-reference as source */
      List<TestDummy> collection = null;
      IEqualityComparer<TestDummy> comparer = collection.CreateEqualityComparer((Func<TestDummy, object>)null);
    }
    #endregion

    #region Distinct test-cases
    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Distinct{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Distinct_EmtyCollectionConcreteType_CollectionIsFiltered() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      List<TestDummy> collection = new List<TestDummy>();
      IEnumerable<TestDummy> distinctCollection = collection.Distinct(td => td.NumericValue);
      
      Assert.IsNotNull(distinctCollection);
      Assert.AreEqual(0, distinctCollection.Count());
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Distinct{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Distinct_EmtyCollectionAnonymousType_CollectionIsFiltered() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      List<TestDummy> collection = new List<TestDummy>();
      var anonymousCollection = collection.Select(td => new { OtherTextValue = td.TextValue.Reverse(), Length = td.TextValue.Length });
      var distinctAnonymousCollection = anonymousCollection.Distinct(a => a.Length);

      Assert.IsNotNull(distinctAnonymousCollection);
      Assert.AreEqual(0, distinctAnonymousCollection.Count());
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Distinct{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Distinct_FilledCollectionConcreteType_CollectionIsFiltered() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValue" };
      TestDummy dummyB = new TestDummy { NumericValue = 2, TextValue = "AnotherValue" };
      TestDummy dummyC = new TestDummy { NumericValue = 1, TextValue = "MyValue" };
      TestDummy dummyD = new TestDummy { NumericValue = 4, TextValue = "TestValue" };
      TestDummy dummyE = new TestDummy { NumericValue = 1, TextValue = "TestingValue" };
      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      IEnumerable<TestDummy> distinctCollection = collection.Distinct(td => td.NumericValue);
      
      Assert.IsNotNull(distinctCollection);
      
      Assert.AreEqual(3, distinctCollection.Count());
      Assert.AreEqual(1, distinctCollection.Count(d => d.NumericValue == 1));
      Assert.AreEqual(1, distinctCollection.Count(d => d.NumericValue == 2));
      Assert.AreEqual(1, distinctCollection.Count(d => d.NumericValue == 4));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Distinct{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Distinct_FilledCollectionAnonymousType_CollectionIsFiltered() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValue" };
      TestDummy dummyB = new TestDummy { NumericValue = 2, TextValue = "AnotherValue" };
      TestDummy dummyC = new TestDummy { NumericValue = 1, TextValue = "MyValue" };
      TestDummy dummyD = new TestDummy { NumericValue = 4, TextValue = "TestValue" };
      TestDummy dummyE = new TestDummy { NumericValue = 1, TextValue = "TestingValue" };
      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE };
      var anonymousCollection = collection.Select(td => new { OtherTextValue = td.TextValue.Reverse(), Length = td.TextValue.Length });
      var distinctAnonymousCollection = anonymousCollection.Distinct(a => a.Length);

      Assert.IsNotNull(distinctAnonymousCollection);

      Assert.AreEqual(3, distinctAnonymousCollection.Count());
      Assert.AreEqual(1, distinctAnonymousCollection.Count(d => d.Length == 7));
      Assert.AreEqual(1, distinctAnonymousCollection.Count(d => d.Length == 9));
      Assert.AreEqual(1, distinctAnonymousCollection.Count(d => d.Length == 12));
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Distinct{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_Distinct_NullSource_ExceptionIsThrown() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      IEnumerable<TestDummy> collection = null;
      IEnumerable<TestDummy> distinctCollection = collection.Distinct(td => td.NumericValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Distinct{T,TField}(IEnumerable{T}, System.Func{T,TField})"/> 
    /// extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_Distinct_NullField_ExceptionIsThrown() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      IEnumerable<TestDummy> collection = new List<TestDummy>();
      IEnumerable<TestDummy> distinctCollection = collection.Distinct((Func<TestDummy, object>)null);
    }
    #endregion

    #region Partition test-cases
    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Partition_EmtyCollectionConcreteType_CollectionIsPartitioned() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      List<TestDummy> collection = new List<TestDummy>();
      IEnumerable<IGrouping<int, TestDummy>> partitionedCollection = collection.Partition(td => td.NumericValue);

      Assert.IsNotNull(partitionedCollection);
      Assert.AreEqual(0, partitionedCollection.Count());
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Partition_EmtyCollectionAnonymousType_CollectionIsPartitioned() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      List<TestDummy> collection = new List<TestDummy>();
      var anonymousCollection = collection.Select(td => new { OtherTextValue = td.TextValue.Reverse(), Length = td.TextValue.Length });
      var partitionedAnonymousCollection = anonymousCollection.Partition(a => a.Length);

      Assert.IsNotNull(partitionedAnonymousCollection);
      Assert.AreEqual(0, partitionedAnonymousCollection.Count());
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Partition_CollectionWithOneConcreteTypeInstance_CollectionIsPartitioned() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValue" };
      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA };
      IEnumerable<IGrouping<int, TestDummy>> partitionedCollection = collection.Partition(td => td.NumericValue);

      Assert.IsNotNull(partitionedCollection);

      Assert.AreEqual(1, partitionedCollection.Count());
      Assert.AreEqual(1, partitionedCollection.ElementAt(0).Key);
      Assert.AreEqual(1, partitionedCollection.ElementAt(0).Count());
      Assert.AreEqual(1, partitionedCollection.ElementAt(0).ElementAt(0).NumericValue);
      Assert.AreEqual("TestValue", partitionedCollection.ElementAt(0).ElementAt(0).TextValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Partition_CollectionWithOneAnonymousTypeInstance_CollectionIsPartitioned() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValue" };
      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA };
      var anonymousCollection = collection.Select(td => new { OtherTextValue = new string(td.TextValue.Reverse().ToArray()), Length = td.TextValue.Length });
      var partitionedAnonymousCollection = anonymousCollection.Partition(a => a.Length);

      Assert.IsNotNull(partitionedAnonymousCollection);

      Assert.AreEqual(1, partitionedAnonymousCollection.Count());
      Assert.AreEqual(9, partitionedAnonymousCollection.ElementAt(0).Key);
      Assert.AreEqual(1, partitionedAnonymousCollection.ElementAt(0).Count());
      Assert.AreEqual(9, partitionedAnonymousCollection.ElementAt(0).ElementAt(0).Length);
      Assert.AreEqual("eulaVtseT", partitionedAnonymousCollection.ElementAt(0).ElementAt(0).OtherTextValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Partition_CollectionWithMultipleConcreteTypeInstancesAdjacent_CollectionIsPartitioned() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValueA" };
      TestDummy dummyB = new TestDummy { NumericValue = 1, TextValue = "TestValueB" };
      TestDummy dummyC = new TestDummy { NumericValue = 2, TextValue = "TestValueC" };
      TestDummy dummyD = new TestDummy { NumericValue = 3, TextValue = "TestValueD" };
      TestDummy dummyE = new TestDummy { NumericValue = 3, TextValue = "TestValueE" };
      TestDummy dummyF = new TestDummy { NumericValue = 3, TextValue = "TestValueF" };
      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE, dummyF };
      IEnumerable<IGrouping<int, TestDummy>> partitionedCollection = collection.Partition(td => td.NumericValue);

      Assert.IsNotNull(partitionedCollection);

      Assert.AreEqual(3, partitionedCollection.Count());

      Assert.AreEqual(1, partitionedCollection.ElementAt(0).Key);
      Assert.AreEqual(2, partitionedCollection.ElementAt(1).Key);
      Assert.AreEqual(3, partitionedCollection.ElementAt(2).Key);

      Assert.AreEqual(2, partitionedCollection.ElementAt(0).Count());
      Assert.AreEqual(1, partitionedCollection.ElementAt(1).Count());
      Assert.AreEqual(3, partitionedCollection.ElementAt(2).Count());

      Assert.AreEqual("TestValueA", partitionedCollection.ElementAt(0).ElementAt(0).TextValue);
      Assert.AreEqual("TestValueB", partitionedCollection.ElementAt(0).ElementAt(1).TextValue);
      Assert.AreEqual("TestValueC", partitionedCollection.ElementAt(1).ElementAt(0).TextValue);
      Assert.AreEqual("TestValueD", partitionedCollection.ElementAt(2).ElementAt(0).TextValue);
      Assert.AreEqual("TestValueE", partitionedCollection.ElementAt(2).ElementAt(1).TextValue);
      Assert.AreEqual("TestValueF", partitionedCollection.ElementAt(2).ElementAt(2).TextValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Partition_CollectionWithMultipleAnonymousTypeInstancesAdjacent_CollectionIsPartitioned() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValueA" };
      TestDummy dummyB = new TestDummy { NumericValue = 1, TextValue = "TestValueB" };
      TestDummy dummyC = new TestDummy { NumericValue = 2, TextValue = "TestValue0C" };
      TestDummy dummyD = new TestDummy { NumericValue = 3, TextValue = "TestValue00D" };
      TestDummy dummyE = new TestDummy { NumericValue = 3, TextValue = "TestValue00E" };
      TestDummy dummyF = new TestDummy { NumericValue = 3, TextValue = "TestValue00F" };
      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE, dummyF };
      var anonymousCollection = collection.Select(td => new { OtherTextValue = new string(td.TextValue.Reverse().ToArray()), Length = td.TextValue.Length });
      var partitionedAnonymousCollection = anonymousCollection.Partition(a => a.Length);

      Assert.IsNotNull(partitionedAnonymousCollection);

      Assert.AreEqual(3, partitionedAnonymousCollection.Count());

      Assert.AreEqual(10, partitionedAnonymousCollection.ElementAt(0).Key);
      Assert.AreEqual(11, partitionedAnonymousCollection.ElementAt(1).Key);
      Assert.AreEqual(12, partitionedAnonymousCollection.ElementAt(2).Key);

      Assert.AreEqual(2, partitionedAnonymousCollection.ElementAt(0).Count());
      Assert.AreEqual(1, partitionedAnonymousCollection.ElementAt(1).Count());
      Assert.AreEqual(3, partitionedAnonymousCollection.ElementAt(2).Count());

      Assert.AreEqual("AeulaVtseT", partitionedAnonymousCollection.ElementAt(0).ElementAt(0).OtherTextValue);
      Assert.AreEqual("BeulaVtseT", partitionedAnonymousCollection.ElementAt(0).ElementAt(1).OtherTextValue);
      Assert.AreEqual("C0eulaVtseT", partitionedAnonymousCollection.ElementAt(1).ElementAt(0).OtherTextValue);
      Assert.AreEqual("D00eulaVtseT", partitionedAnonymousCollection.ElementAt(2).ElementAt(0).OtherTextValue);
      Assert.AreEqual("E00eulaVtseT", partitionedAnonymousCollection.ElementAt(2).ElementAt(1).OtherTextValue);
      Assert.AreEqual("F00eulaVtseT", partitionedAnonymousCollection.ElementAt(2).ElementAt(2).OtherTextValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Partition_CollectionWithMultipleConcreteTypeInstancesNotAdjacent_CollectionIsPartitioned() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValueA" };
      TestDummy dummyB = new TestDummy { NumericValue = 3, TextValue = "TestValueB" };
      TestDummy dummyC = new TestDummy { NumericValue = 2, TextValue = "TestValueC" };
      TestDummy dummyD = new TestDummy { NumericValue = 2, TextValue = "TestValueD" };
      TestDummy dummyE = new TestDummy { NumericValue = 3, TextValue = "TestValueE" };
      TestDummy dummyF = new TestDummy { NumericValue = 3, TextValue = "TestValueF" };
      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE, dummyF };
      IEnumerable<IGrouping<int, TestDummy>> partitionedCollection = collection.Partition(td => td.NumericValue);

      Assert.IsNotNull(partitionedCollection);

      Assert.AreEqual(4, partitionedCollection.Count());

      Assert.AreEqual(1, partitionedCollection.ElementAt(0).Key);
      Assert.AreEqual(3, partitionedCollection.ElementAt(1).Key);
      Assert.AreEqual(2, partitionedCollection.ElementAt(2).Key);
      Assert.AreEqual(3, partitionedCollection.ElementAt(3).Key);

      Assert.AreEqual(1, partitionedCollection.ElementAt(0).Count());
      Assert.AreEqual(1, partitionedCollection.ElementAt(1).Count());
      Assert.AreEqual(2, partitionedCollection.ElementAt(2).Count());
      Assert.AreEqual(2, partitionedCollection.ElementAt(3).Count());

      Assert.AreEqual("TestValueA", partitionedCollection.ElementAt(0).ElementAt(0).TextValue);
      Assert.AreEqual("TestValueB", partitionedCollection.ElementAt(1).ElementAt(0).TextValue);
      Assert.AreEqual("TestValueC", partitionedCollection.ElementAt(2).ElementAt(0).TextValue);
      Assert.AreEqual("TestValueD", partitionedCollection.ElementAt(2).ElementAt(1).TextValue);
      Assert.AreEqual("TestValueE", partitionedCollection.ElementAt(3).ElementAt(0).TextValue);
      Assert.AreEqual("TestValueF", partitionedCollection.ElementAt(3).ElementAt(1).TextValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    public void LinqExtensions_Partition_CollectionWithMultipleAnonymousTypeInstancesNotAdjacent_CollectionIsPartitioned() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using a filled collection as source */
      TestDummy dummyA = new TestDummy { NumericValue = 1, TextValue = "TestValueA" };
      TestDummy dummyB = new TestDummy { NumericValue = 1, TextValue = "TestValue00B" };
      TestDummy dummyC = new TestDummy { NumericValue = 2, TextValue = "TestValue0C" };
      TestDummy dummyD = new TestDummy { NumericValue = 3, TextValue = "TestValue0D" };
      TestDummy dummyE = new TestDummy { NumericValue = 3, TextValue = "TestValue00E" };
      TestDummy dummyF = new TestDummy { NumericValue = 3, TextValue = "TestValue00F" };
      IEnumerable<TestDummy> collection = new List<TestDummy> { dummyA, dummyB, dummyC, dummyD, dummyE, dummyF };
      var anonymousCollection = collection.Select(td => new { OtherTextValue = new string(td.TextValue.Reverse().ToArray()), Length = td.TextValue.Length });
      var partitionedAnonymousCollection = anonymousCollection.Partition(a => a.Length);

      Assert.IsNotNull(partitionedAnonymousCollection);

      Assert.AreEqual(4, partitionedAnonymousCollection.Count());

      Assert.AreEqual(10, partitionedAnonymousCollection.ElementAt(0).Key);
      Assert.AreEqual(12, partitionedAnonymousCollection.ElementAt(1).Key);
      Assert.AreEqual(11, partitionedAnonymousCollection.ElementAt(2).Key);
      Assert.AreEqual(12, partitionedAnonymousCollection.ElementAt(3).Key);

      Assert.AreEqual(1, partitionedAnonymousCollection.ElementAt(0).Count());
      Assert.AreEqual(1, partitionedAnonymousCollection.ElementAt(1).Count());
      Assert.AreEqual(2, partitionedAnonymousCollection.ElementAt(2).Count());
      Assert.AreEqual(2, partitionedAnonymousCollection.ElementAt(3).Count());

      Assert.AreEqual("AeulaVtseT", partitionedAnonymousCollection.ElementAt(0).ElementAt(0).OtherTextValue);
      Assert.AreEqual("B00eulaVtseT", partitionedAnonymousCollection.ElementAt(1).ElementAt(0).OtherTextValue);
      Assert.AreEqual("C0eulaVtseT", partitionedAnonymousCollection.ElementAt(2).ElementAt(0).OtherTextValue);
      Assert.AreEqual("D0eulaVtseT", partitionedAnonymousCollection.ElementAt(2).ElementAt(1).OtherTextValue);
      Assert.AreEqual("E00eulaVtseT", partitionedAnonymousCollection.ElementAt(3).ElementAt(0).OtherTextValue);
      Assert.AreEqual("F00eulaVtseT", partitionedAnonymousCollection.ElementAt(3).ElementAt(1).OtherTextValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_Partition_NullSource_ExceptionIsThrown() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      IEnumerable<TestDummy> collection = null;
      IEnumerable<IGrouping<int, TestDummy>> partitionedCollection = collection.Partition(td => td.NumericValue);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_Partition_NullKeySelector_ExceptionIsThrown() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      IEnumerable<TestDummy> collection = new List<TestDummy>();
      IEnumerable<IGrouping<object, TestDummy>> partitionedCollection = collection.Partition((Func<TestDummy, object>)null);
    }

    /// <summary>Tests the functionality of the 
    /// <see cref="Enkoni.Framework.Linq.Enumerable.Partition{TKey,TSource}(IEnumerable{TSource}, System.Func{TSource,TKey})"/> 
    /// extension method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void LinqExtensions_Partition_NullComparer_ExceptionIsThrown() {
      /* Note: This testcase does not fully test the functionality of the created comparer as this is already done by the 
       * 'LambdaEqualityComparerTest' class. It only verifies that the test subject returns a working collection that contains 
       * the expected instances */

      /* Test the method using an empty collection as source */
      IEnumerable<TestDummy> collection = new List<TestDummy>();
      IEnumerable<IGrouping<int, TestDummy>> partitionedCollection = collection.Partition(td => td.NumericValue, null);
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
