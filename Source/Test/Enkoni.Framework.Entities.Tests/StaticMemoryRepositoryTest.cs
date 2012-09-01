//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="StaticMemoryRepositoryTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the MemoryRepository class using the StaticMemoryStore.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="MemoryRepository{TEntity}"/> class in combination with the 
  /// <see cref="StaticMemoryStore{T}"/> class.</summary>
  [TestClass]
  public class StaticMemoryRepositoryTest : RepositoryTest {
    #region Retrieve test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase01_FindAll() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindAll(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase02_FindAll_EmptySource() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindAll_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase03_FindAllWithExpression() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindAllWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase04_FindAllWithExpression_EmptySource() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindAllWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase05_FindSingleWithExpression() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindSingleWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase06_FindSingleWithExpression_EmptySource() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindSingleWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase07_FindFirstWithExpression() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase08_FindFirstWithExpression_EmptySource() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public override void TestCase09_RetrieveLessThenAvailable() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveLessThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public override void TestCase10_RetrieveExactlyAvailable() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveExactlyAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase11_RetrieveExactlyAvailable_EmptySource() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveExactlyAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public override void TestCase12_RetrieveMoreThenAvailable() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveMoreThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase13_RetrieveMoreThenAvailable_EmptySource() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveMoreThenAvailable_EmptySource(sourceInfo);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase14_OrderBy() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.OrderBy(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase15_OrderBy_EmptySource() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.OrderBy_EmptySource(sourceInfo);
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase16_Add() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Add(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase17_Update() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Update(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase18_Delete() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Delete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public override void TestCase19_AddMultiple_NormalUse() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public override void TestCase20_DeleteMultiple_NormalUse() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.DeleteMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    public override void TestCase21_DeleteMultiple_Exceptions() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.DeleteMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public override void TestCase22_UpdateMultiple_NormalUse() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.UpdateMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    public override void TestCase23_UpdateMultiple_Exceptions() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.UpdateMultiple_Exceptions(sourceInfo);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase24_AddUpdate() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddUpdate(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase25_AddUpdateDelete() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddUpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase26_UpdateDelete() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.UpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public override void TestCase27_DeleteAdd() {
      MemoryStore<TestDummy> store = new StaticMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.DeleteAdd(sourceInfo);
    }
    #endregion

    #region Implementation of RepositoryTest
    /// <summary>Creates a new repository using the specified <see cref="DataSourceInfo"/>.</summary>
    /// <typeparam name="T">The type of entity that must be handled by the repository.</typeparam>
    /// <param name="sourceInfo">The data source information that will be used to create a new repository.</param>
    /// <returns>The created repository.</returns>
    protected override Repository<T> CreateRepository<T>(DataSourceInfo sourceInfo) {
      return new MemoryRepository<T>(sourceInfo);
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
