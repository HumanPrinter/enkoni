using System;
using System.Collections.Generic;

using Enkoni.Framework.Testing;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="MemoryRepository{TEntity}"/> class in combination with the 
  /// <see cref="HttpSessionMemoryStore{T}"/> class.</summary>
  [TestClass]
  public class HttpSessionMemoryRepositoryTest : RepositoryTestBase {
    #region Test-case setup
    /// <summary>Initialzes the testcases by simulating an HTTP session.</summary>
    [TestInitialize]
    public void SetupTestCases() {
      HttpContextHelper.SetHttpContextWithSimulatedRequest("localhost", "TestOrderService");
    }
    #endregion

    #region FindAll test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_NoSpecification_AllAvailableRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindAllWithoutSpecification_AllRecordsAreReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_NoSpecificationEmptySource_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindAllWithoutSpecificationAndWithEmptySource_NoRecordsAreReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionRepository_FindAll_WithMatchingSpecification_OnlyMatchingRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindAllWithSpecification_WithResults(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_WithNotMatchingSpecification_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindAllWithSpecification_WithoutResults(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_WithSpecificationEmptySource_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindAllWithSpecification_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_SpecificationWithMaxResultsLessThanAvailable_MaxResultsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveLessThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_SpecificationWithMaxResultsExactlyAvailable_AvailableRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveExactlyAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_SpecificationWithMaxResultsExactlyAvailableEmptySource_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveExactlyAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_SpecificationWithMaxResultsMoreThenAvailable_AvailableRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveMoreThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_SpecificationWithMaxResultsMoreThenAvailableEmptySource_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.RetrieveMoreThenAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using type for which a custom mapping is 
    /// available.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_CustomMappedType_AvailableRecordsAreReturned() {
      MemoryStore<CustomMappedTestDummy> store = new HttpSessionMemoryStore<CustomMappedTestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<CustomMappedTestDummy>(store, true);
      this.RetrieveTypesWithCustomMapping(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAllWithoutCloning_NoCopiesAreCreated() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindAllWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAllWithCloning_CopiesAreCreated() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindAllWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_WithMatchingExpression_OnlyMatchingRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindAllWithExpression_WithResults(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindAll_WithNotMatchingExpression_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindAllWithExpression_WithoutResults(sourceInfo);
    }
    #endregion

    #region FindSingle test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingle_WithMatchingSpecification_OnlyMatchingRecordIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindSingleWithMatchingSpecification_OnlyMatchingRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingle_WithNotMatchingSpecification_NoRecordIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindSingleWithNotMatchingSpecification_NoRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingle_WithNotMatchingSpecificationAndDefault_DefaultIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindSingleWithNotMatchingSpecificationAndDefault_DefaultIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingle_WithSpecificationEmptySource_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindSingleWithSpecification_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingle_WithMatchingExpression_OnlyMatchingRecordIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindSingleWithMatchingExpression_OnlyMatchingRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingle_WithNotMatchingExpression_NoRecordIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindSingleWithNotMatchingExpression_NoRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingle_WithNotMatchingExpressionAndDefault_DefaultIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindSingleWithNotMatchingExpressionAndDefault_DefaultIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingleWithoutCloning_NoCopiesAreCreated() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindSingleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindSingleWithCloning_CopiesAreCreated() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindSingleWithCloning(sourceInfo);
    }
    #endregion

    #region FindFirst test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirst_WithMatchingSpecification_FirstMatchingRecordIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithMatchingSpecification_FirstMatchingRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirst_WithNotMatchingSpecification_NoRecordIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithNotMatchingSpecification_NoRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirst_WithNotMatchingSpecificationAndDefault_DefaultIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithNotMatchingSpecificationAndDefault_DefaultIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirst_WithSpecificationEmptySource_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithSpecification_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirstWithoutCloning_NoCopiesAreCreated() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.FindFirstWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirstWithCloning_CopiesAreCreated() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirst_WithMatchingExpression_FirstMatchingRecordIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithMatchingExpression_FirstMatchingRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirst_WithNotMatchingExpression_NoRecordIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithNotMatchingExpression_NoRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_FindFirst_WithNotMatchingExpressionAndDefault_DefaultIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareInputTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.FindFirstWithNotMatchingExpressionAndDefault_DefaultIsReturned(sourceInfo);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_SpecificationWithOrderBy_ItemsAreCorrectlyOrdered() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareSortingTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.OrderBy(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with an empty <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_SpecificationWithOrderByEmptySource_NoRecordsAreReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.OrderBy_EmptySource(sourceInfo);
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_Add_ItemIsStored() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Add(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddCustomMappedType_ItemIsStored() {
      MemoryStore<CustomMappedTestDummy> store = new HttpSessionMemoryStore<CustomMappedTestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<CustomMappedTestDummy>(store, true);
      this.AddCustomMappedType(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_Update_ItemIsUpdated() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Update(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateCustomMappedType_ItemIsUpdated() {
      MemoryStore<CustomMappedTestDummy> store = new HttpSessionMemoryStore<CustomMappedTestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<CustomMappedTestDummy>(store, true);
      this.UpdateCustomMappedType(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method using the <see cref="MemoryRepository{TEntity}"/> 
    /// implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_Delete_ItemIsDeleted() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Delete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddMultiple_ItemsAreAdded() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddMultipleCustomMappedTypes_ItemsAreAdded() {
      /* Create the repository */
      MemoryStore<CustomMappedTestDummy> store = new HttpSessionMemoryStore<CustomMappedTestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<CustomMappedTestDummy>(store, true);
      this.AddMultipleCustomMappedTypes_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_DeleteMultiple_ItemsAreDeleted() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.DeleteMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_DeleteMultipleDeleteEntityTwice_OperationIsRolledBack() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.DeleteMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateMultiple_ItemsAreUpdated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.UpdateMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateMultipleCustomMappedTypes_ItemsAreUpdated() {
      /* Create the repository */
      HttpSessionMemoryStore<CustomMappedTestDummy> store = new HttpSessionMemoryStore<CustomMappedTestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<CustomMappedTestDummy>(store, true);
      this.UpdateMultipleCustomMappedTypes_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateMultipleUpdateUnaddedEntity_OperationIsRolledBack() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.UpdateMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddAndReset_AdditionIsUndone() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Add_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved updates to the repository.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateAndReset_UpdateIsUndone() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Update_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved deletions from the repository.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_DeleteAndReset_DeletionIsUndone() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.Delete_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.AddWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddWithCloning_CopyIsCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddDeletedItemWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.AddDeletedItemWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddDeletedItemWithCloning_CopyIsCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddDeletedItemWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddMultipleWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.AddMultipleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddMultipleWithCloning_CopiesAreCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddMultipleWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.UpdateWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateWithCloning_CopyIsCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.UpdateWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateMultipleWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, false);
      this.UpdateMultipleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateMultipleWithCloning_CopiesAreCreated() {
      /* Create the repository */
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.UpdateMultipleWithCloning(sourceInfo);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddAndUpdate_ItemsAreStored() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddUpdate(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_AddUpdateAndDelete_ItemsAreStored() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.AddUpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_UpdateAndDelete_ItemsAreStored() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.UpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_DeleteAndAdd_ItemsAreStored() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.DeleteAdd(sourceInfo);
    }
    #endregion

    #region Execute test-case contracts
    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void HttpSessionMemoryRepository_ExecuteWithDefaultSpecification_NullIsReturned() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.ExecuteDefaultSpecification(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(NotSupportedException))]
    public void HttpSessionMemoryRepository_ExecuteWithBusinessRule_ExceptionIsThrown() {
      MemoryStore<TestDummy> store = new HttpSessionMemoryStore<TestDummy>();
      PrepareStorageTests(store);

      DataSourceInfo sourceInfo = new MemorySourceInfo<TestDummy>(store, true);
      this.ExecuteBusinessRule(sourceInfo);
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

    /// <summary>Prepares the tests by clearing the memorystore.</summary>
    /// <param name="memorystore">The store that must be prepared.</param>
    private static void PrepareTests(MemoryStore<CustomMappedTestDummy> memorystore) {
      memorystore.Storage.Clear();
    }

    /// <summary>Prepares the retrieve tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareInputTests(MemoryStore<TestDummy> memorystore) {
      memorystore.EnterWriteLock();
      try {
        PrepareTests(memorystore);
        memorystore.Storage.Add(new TestDummy { RecordId = 1, TextValue = "\"Row1\"", NumericValue = 3, BooleanValue = false });
        memorystore.Storage.Add(new TestDummy { RecordId = 2, TextValue = "\"Row2\"", NumericValue = 3, BooleanValue = true });
        memorystore.Storage.Add(new TestDummy { RecordId = 3, TextValue = "\"Row3\"", NumericValue = 7, BooleanValue = true });
        memorystore.Storage.Add(new TestDummy { RecordId = 4, TextValue = "\"Row4\"", NumericValue = 2, BooleanValue = true });
        memorystore.Storage.Add(new TestDummy { RecordId = 5, TextValue = "\"Row5\"", NumericValue = 5, BooleanValue = false });
        memorystore.Storage.Add(new TestDummy { RecordId = 6, TextValue = "\"Row6\"", NumericValue = 1, BooleanValue = true });
      }
      finally {
        memorystore.ExitWriteLock();
      }
    }

    /// <summary>Prepares the retrieve tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareInputTests(MemoryStore<CustomMappedTestDummy> memorystore) {
      memorystore.EnterWriteLock();
      try {
        PrepareTests(memorystore);
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 1, TextValue = "\"Row1\"", NumericValue = 3, BooleanValue = false });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 2, TextValue = "\"Row2\"", NumericValue = 3, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 3, TextValue = "\"Row3\"", NumericValue = 7, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 4, TextValue = "\"Row4\"", NumericValue = 2, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 5, TextValue = "\"Row5\"", NumericValue = 5, BooleanValue = false });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 6, TextValue = "\"Row6\"", NumericValue = 1, BooleanValue = true });
      }
      finally {
        memorystore.ExitWriteLock();
      }
    }

    /// <summary>Prepares the sorting tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareSortingTests(MemoryStore<TestDummy> memorystore) {
      memorystore.EnterWriteLock();
      try {
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
      finally {
        memorystore.ExitWriteLock();
      }
    }

    /// <summary>Prepares the sorting tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareSortingTests(MemoryStore<CustomMappedTestDummy> memorystore) {
      memorystore.EnterWriteLock();
      try {
        PrepareTests(memorystore);
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 1, TextValue = "aabcdef", NumericValue = 3, BooleanValue = false });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 2, TextValue = "abcdefg", NumericValue = 3, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 3, TextValue = "aadefgh", NumericValue = 7, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 4, TextValue = "abefghi", NumericValue = 2, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 5, TextValue = "bbcdefg", NumericValue = 5, BooleanValue = false });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 6, TextValue = "bbdefgh", NumericValue = 1, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 7, TextValue = "bbefghi", NumericValue = 1, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 8, TextValue = "bbfghij", NumericValue = 1, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 9, TextValue = "acdefgh", NumericValue = 5, BooleanValue = false });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 10, TextValue = "ccefghi", NumericValue = 1, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 11, TextValue = "acfghij", NumericValue = 1, BooleanValue = true });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 12, TextValue = "ccghijk", NumericValue = 1, BooleanValue = true });
      }
      finally {
        memorystore.ExitWriteLock();
      }
    }

    /// <summary>Prepares the (combined) storage tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareStorageTests(MemoryStore<TestDummy> memorystore) {
      memorystore.EnterWriteLock();
      try {
        PrepareTests(memorystore);
        memorystore.Storage.Add(new TestDummy { RecordId = 1, TextValue = "\"Row1\"", NumericValue = 3, BooleanValue = false });
        memorystore.Storage.Add(new TestDummy { RecordId = 2, TextValue = "\"Row2\"", NumericValue = 3, BooleanValue = true });
      }
      finally {
        memorystore.ExitWriteLock();
      }
    }

    /// <summary>Prepares the (combined) storage tests by filling the memorystore with preconfigured testdata.</summary>
    /// <param name="memorystore">The store in which the data must be stored.</param>
    private static void PrepareStorageTests(MemoryStore<CustomMappedTestDummy> memorystore) {
      memorystore.EnterWriteLock();
      try {
        PrepareTests(memorystore);
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 1, TextValue = "\"Row1\"", NumericValue = 3, BooleanValue = false });
        memorystore.Storage.Add(new CustomMappedTestDummy { RecordId = 2, TextValue = "\"Row2\"", NumericValue = 3, BooleanValue = true });
      }
      finally {
        memorystore.ExitWriteLock();
      }
    }
    #endregion
  }
}
