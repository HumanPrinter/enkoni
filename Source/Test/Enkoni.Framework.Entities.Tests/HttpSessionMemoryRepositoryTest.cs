//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpSessionMemoryRepositoryTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the MemoryRepository class using the HttpSessionMemoryStore.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

using Enkoni.Framework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="MemoryRepository{TEntity}"/> class in combination with the 
  /// <see cref="HttpSessionMemoryStore{T}"/> class.</summary>
  [TestClass]
  public class HttpSessionMemoryRepositoryTest {
    #region Test-case setup
    /// <summary>Initialzes the testcases by simulating an HTTP session.</summary>
    [TestInitialize]
    public void SetupTestCases() {
      HttpContextHelper.SetHttpContextWithSimulatedRequest("localhost", "TestOrderService");
    }
    #endregion

    #region Retrieve test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase01_FindAll() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      IEnumerable<TestDummy> results = repository.FindAll();

      Assert.IsNotNull(results);
      Assert.AreEqual(6, results.Count());
      for(int index = 0; index < 6; ++index) {
        Assert.AreEqual(index + 1, results.ElementAt(index).RecordId);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase02_FindAll_EmptyFile() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      IEnumerable<TestDummy> results = repository.FindAll();

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase03_FindAllWithExpression() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue < 3 || td.NumericValue > 5);
      spec = spec.And(Specification.Not((TestDummy td) => !td.BooleanValue));
      IEnumerable<TestDummy> results = repository.FindAll(spec);

      Assert.IsNotNull(results);
      Assert.AreEqual(3, results.Count());
      Assert.AreEqual(3, results.ElementAt(0).RecordId);
      Assert.AreEqual(4, results.ElementAt(1).RecordId);
      Assert.AreEqual(6, results.ElementAt(2).RecordId);

      spec = Specification.Lambda((TestDummy td) => td.NumericValue > 500);
      spec = spec.Or(Specification.Like((TestDummy td) => td.TextValue, "R*7"));
      results = repository.FindAll(spec);

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase04_FindAllWithExpression_EmptyFile() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue < 3 || td.NumericValue > 5);
      spec = spec.And(Specification.Not((TestDummy td) => !td.BooleanValue));

      IEnumerable<TestDummy> results = repository.FindAll(spec);

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase05_FindSingleWithExpression() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 7);
      TestDummy result = repository.FindSingle(spec);

      Assert.IsNotNull(result);
      Assert.AreEqual(3, result.RecordId);
      
      spec = Specification.Lambda((TestDummy td) => td.NumericValue == 500);
      result = repository.FindSingle(spec);

      Assert.IsNull(result);

      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindSingle(spec, defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase06_FindSingleWithExpression_EmptyFile() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 7);
      TestDummy result = repository.FindSingle(spec);

      Assert.IsNull(result);
      
      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindSingle(spec, defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase07_FindFirstWithExpression() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 3);
      TestDummy result = repository.FindFirst(spec);

      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.RecordId);

      spec = Specification.Lambda((TestDummy td) => td.NumericValue == 500);
      result = repository.FindFirst(spec);

      Assert.IsNull(result);

      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindFirst(spec, defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase08_FindFirstWithExpression_EmptyFile() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 7);
      TestDummy result = repository.FindFirst(spec);

      Assert.IsNull(result);

      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindFirst(spec, defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public void TestCase09_RetrieveLessThenAvailable() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(4);
      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(4, results.Count());
      Assert.AreEqual("aabcdef", results.First().TextValue, false);
      Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
      Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
      Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);

      /* Set maximumresults on bottom-most specification before combining */
      specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      specA.SetMaximumResults(4);
      specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));

      results = repository.FindAll(specB);
      Assert.AreEqual(4, results.Count());
      Assert.AreEqual("aabcdef", results.First().TextValue, false);
      Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
      Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
      Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);

      /* Set maximumresults on bottom-most specification after combining */
      specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specA.SetMaximumResults(4);

      results = repository.FindAll(specB);
      Assert.AreEqual(4, results.Count());
      Assert.AreEqual("aabcdef", results.First().TextValue, false);
      Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
      Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
      Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);

      /* Set maximumresults to zero */
      specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specA.SetMaximumResults(0);

      results = repository.FindAll(specB);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public void TestCase10_RetrieveExactlyAvailable() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(6);
      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(6, results.Count());
      Assert.AreEqual("aabcdef", results.First().TextValue, false);
      Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
      Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
      Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);
      Assert.AreEqual("acdefgh", results.Skip(4).First().TextValue, false);
      Assert.AreEqual("acfghij", results.Skip(5).First().TextValue, false);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase11_RetrieveExactlyAvailable_EmptyFile() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(0);

      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public void TestCase12_RetrieveMoreThenAvailable() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(10);
      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(6, results.Count());
      Assert.AreEqual("aabcdef", results.First().TextValue, false);
      Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
      Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
      Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);
      Assert.AreEqual("acdefgh", results.Skip(4).First().TextValue, false);
      Assert.AreEqual("acfghij", results.Skip(5).First().TextValue, false);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase13_RetrieveMoreThenAvailable_EmptyFile() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(4);
      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase14_OrderBy() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      specA.OrderBy((TestDummy td) => td.NumericValue, SortOrder.Ascending);

      IEnumerable<TestDummy> results = repository.FindAll(specA);
      Assert.AreEqual(6, results.Count());
      Assert.AreEqual("acfghij", results.First().TextValue, false);
      Assert.AreEqual("abefghi", results.Skip(1).First().TextValue, false);
      Assert.AreEqual("aabcdef", results.Skip(2).First().TextValue, false);
      Assert.AreEqual("abcdefg", results.Skip(3).First().TextValue, false);
      Assert.AreEqual("acdefgh", results.Skip(4).First().TextValue, false);
      Assert.AreEqual("aadefgh", results.Skip(5).First().TextValue, false);

      specA.OrderBy((TestDummy td) => td.TextValue, SortOrder.Descending);
      results = repository.FindAll(specA);

      Assert.AreEqual(6, results.Count());
      Assert.AreEqual("acfghij", results.First().TextValue, false);
      Assert.AreEqual("abefghi", results.Skip(1).First().TextValue, false);
      Assert.AreEqual("abcdefg", results.Skip(2).First().TextValue, false);
      Assert.AreEqual("aabcdef", results.Skip(3).First().TextValue, false);
      Assert.AreEqual("acdefgh", results.Skip(4).First().TextValue, false);
      Assert.AreEqual("aadefgh", results.Skip(5).First().TextValue, false);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase15_OrderBy_EmptyFile() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      specA.OrderBy((TestDummy td) => td.NumericValue, SortOrder.Ascending);

      IEnumerable<TestDummy> results = repository.FindAll(specA);
      Assert.AreEqual(0, results.Count());

      specA.OrderBy((TestDummy td) => td.TextValue, SortOrder.Descending);
      results = repository.FindAll(specA);

      Assert.AreEqual(0, results.Count());
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase16_Add() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Add a new entity without saving changes or re-creating the repository */
      TestDummy newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      TestDummy addedDummy = repository.AddEntity(newDummy);

      Assert.IsNotNull(addedDummy);
      Assert.IsTrue(addedDummy.RecordId < 0);

      ISpecification<TestDummy> spec = Specification.Like((TestDummy td) => td.TextValue, "Ro?X");
      TestDummy retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowX", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Re-create the repository and try to retrieve previous added entity */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNull(retrievedDummy);

      /* Add a new entity followed by saving changes and re-creating the repository */
      newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);

      repository.SaveChanges();

      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowX", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase17_Update() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);
      
      /* Update an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 3));
      originalDummy.TextValue = "RowY";
      TestDummy updatedDummy = repository.UpdateEntity(originalDummy);

      Assert.IsNotNull(updatedDummy);
      Assert.IsTrue(updatedDummy.RecordId > 0);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(updatedDummy.RecordId, updatedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);

      /* Re-create the repository and try to retrieve previous updated entity */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(originalDummy.RecordId, updatedDummy.RecordId);
      Assert.AreEqual("\"Row1\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);

      /* Update an entity followed by saving changes and re-creating the repository */
      originalDummy.TextValue = "RowY";
      updatedDummy = repository.UpdateEntity(originalDummy);

      repository.SaveChanges();

      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(originalDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase18_Delete() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Delete an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 3));
      repository.DeleteEntity(originalDummy);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNull(retrievedDummy);

      /* Re-create the repository and try to retrieve previous deleted entity */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(originalDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("\"Row1\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);

      /* Delete an entity followed by saving changes and re-creating the repository */
      repository.DeleteEntity(originalDummy);

      repository.SaveChanges();

      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue == false));

      Assert.IsNull(retrievedDummy);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase19_AddUpdate() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Add and then update an entity without saving changes or re-creating the repository */
      TestDummy newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      TestDummy addedDummy = repository.AddEntity(newDummy);
      addedDummy.TextValue = "RowY";
      TestDummy updatedDummy = repository.UpdateEntity(addedDummy);
      Assert.IsNotNull(updatedDummy);
      Assert.AreEqual(addedDummy.RecordId, updatedDummy.RecordId);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 12);
      TestDummy retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(updatedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Re-create the repository and try to retrieve previous updated entity */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNull(retrievedDummy);

      /* Add and update an entity followed by saving changes and re-creating the repository */
      newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);
      addedDummy.TextValue = "RowY";
      updatedDummy = repository.UpdateEntity(addedDummy);

      repository.SaveChanges();

      /* First retrieve without re-creating the repository */
      retrievedDummy = repository.FindSingle(spec);
      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Then retrieve after re-creating the repository */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase20_AddUpdateDelete() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      /* Add and then update and then delete an entity without saving changes or re-creating the repository */
      TestDummy newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      TestDummy addedDummy = repository.AddEntity(newDummy);
      addedDummy.TextValue = "RowY";
      TestDummy updatedDummy = repository.UpdateEntity(addedDummy);
      repository.DeleteEntity(updatedDummy);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 12);
      TestDummy retrievedDummy = repository.FindSingle(spec);

      Assert.IsNull(retrievedDummy);
      
      /* Add, update and delete an entity followed by saving changes and re-creating the repository */
      newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);
      addedDummy.TextValue = "RowY";
      updatedDummy = repository.UpdateEntity(addedDummy);
      repository.DeleteEntity(updatedDummy);

      repository.SaveChanges();

      /* First retrieve without re-creating the repository */
      retrievedDummy = repository.FindSingle(spec);
      Assert.IsNull(retrievedDummy);
      
      /* Then retrieve after re-creating the repository */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNull(retrievedDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase21_UpdateDelete() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.BooleanValue);
      /* Retrieve and then update and delete an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindSingle(spec);
      originalDummy.TextValue = "RowX";
      TestDummy updatedDummy = repository.UpdateEntity(originalDummy);
      repository.DeleteEntity(updatedDummy);
      
      TestDummy retrievedDummy = repository.FindSingle(spec);
      
      Assert.IsNull(retrievedDummy);

      /* Re-create the repository and try to retrieve previous updated entity */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Update and delete an entity followed by saving changes and re-creating the repository */
      originalDummy = repository.FindSingle(spec);
      originalDummy.TextValue = "RowX";
      updatedDummy = repository.UpdateEntity(originalDummy);
      repository.DeleteEntity(updatedDummy);

      repository.SaveChanges();

      /* First retrieve without re-creating the repository */
      retrievedDummy = repository.FindSingle(spec);
      Assert.IsNull(retrievedDummy);
      
      /* Then retrieve after re-creating the repository */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNull(retrievedDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void TestCase22_DeleteAdd() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store);
      Repository<TestDummy> repository = new MemoryRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.BooleanValue);

      /* Delete and then add an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindSingle(spec);
      repository.DeleteEntity(originalDummy);

      TestDummy newDummy = new TestDummy { TextValue = "\"Row2\"", NumericValue = 4, BooleanValue = true };
      TestDummy addedDummy = repository.AddEntity(newDummy);

      Assert.IsNotNull(addedDummy);
      Assert.IsTrue(addedDummy.RecordId < 0);

      TestDummy retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(4, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Re-create the repository and try to retrieve previous updated entity */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Delete and add an entity followed by saving changes and re-creating the repository */
      originalDummy = repository.FindSingle(spec);
      repository.DeleteEntity(originalDummy);
      newDummy = new TestDummy { TextValue = "\"Row2\"", NumericValue = 4, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);

      repository.SaveChanges();

      /* First retrieve without re-creating the repository */
      retrievedDummy = repository.FindSingle(spec);
      Assert.IsNotNull(retrievedDummy);
      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(4, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Then retrieve after re-creating the repository */
      repository = new MemoryRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(4, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }
    #endregion

    #region Testsetup methods
    /// <summary>Prepares the tests by clearing the memorystore.</summary>
    /// <param name="memorystore">The store that must be prepared.</param>
    private static void PrepareTests(MemoryStore<TestDummy> memorystore) {
      memorystore.Storage.Clear();
    }

    /// <summary>Prepares the retrieve tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareInputTests(MemoryStore<TestDummy> memorystore) {
      PrepareTests(memorystore);
      memorystore.Storage.Add(new TestDummy { RecordId = 1, TextValue = "\"Row1\"", NumericValue = 3, BooleanValue = false });
      memorystore.Storage.Add(new TestDummy { RecordId = 2, TextValue = "\"Row2\"", NumericValue = 3, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 3, TextValue = "\"Row3\"", NumericValue = 7, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 4, TextValue = "\"Row4\"", NumericValue = 2, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 5, TextValue = "\"Row5\"", NumericValue = 5, BooleanValue = false });
      memorystore.Storage.Add(new TestDummy { RecordId = 6, TextValue = "\"Row6\"", NumericValue = 1, BooleanValue = true });
    }

    /// <summary>Prepares the sorting tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareSortingTests(MemoryStore<TestDummy> memorystore) {
      PrepareTests(memorystore);
      memorystore.Storage.Add(new TestDummy { RecordId = 1, TextValue = "aabcdef", NumericValue = 3, BooleanValue = false });
      memorystore.Storage.Add(new TestDummy { RecordId = 2, TextValue = "abcdefg", NumericValue = 3, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 3, TextValue = "aadefgh", NumericValue = 7, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 4, TextValue = "abefghi", NumericValue = 2, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 5, TextValue = "bbcdefg", NumericValue = 5, BooleanValue = false });
      memorystore.Storage.Add(new TestDummy { RecordId = 6, TextValue = "bbdefgh", NumericValue = 1, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 7, TextValue = "bbefghi", NumericValue = 1, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 8, TextValue = "bbfghij", NumericValue = 1, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 9, TextValue = "acdefgh", NumericValue = 5, BooleanValue = false });
      memorystore.Storage.Add(new TestDummy { RecordId = 10, TextValue = "ccefghi", NumericValue = 1, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 11, TextValue = "acfghij", NumericValue = 1, BooleanValue = true });
      memorystore.Storage.Add(new TestDummy { RecordId = 12, TextValue = "ccghijk", NumericValue = 1, BooleanValue = true });
    }

    /// <summary>Prepares the (combined) storage tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareStorageTests(MemoryStore<TestDummy> memorystore) {
      PrepareTests(memorystore);
      memorystore.Storage.Add(new TestDummy { RecordId = 1, TextValue = "\"Row1\"", NumericValue = 3, BooleanValue = false });
      memorystore.Storage.Add(new TestDummy { RecordId = 2, TextValue = "\"Row2\"", NumericValue = 3, BooleanValue = true });
    }
    #endregion
  }
}
