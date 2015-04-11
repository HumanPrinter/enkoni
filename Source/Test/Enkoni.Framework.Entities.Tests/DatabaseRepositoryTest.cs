using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="DatabaseRepository{TEntity}"/> class.</summary>
  [TestClass]
  [DeploymentItem(@"amd64\", @"amd64\")]
  [DeploymentItem(@"x86\", @"x86\")]
  [DeploymentItem("System.Data.SqlServerCe.dll")]
  [DeploymentItem("EntityFramework.SqlServer.dll")]
  [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
  public class DatabaseRepositoryTest : RepositoryTestBase {
    #region FindAll test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll01")]
    public void DatabaseRepository_FindAll_NoSpecification_AllAvailableRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll01\", TestCategory.Retrieve, false);
      this.FindAllWithoutSpecification_AllRecordsAreReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll02")]
    public void DatabaseRepository_FindAll_NoSpecificationEmptySource_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll02\", TestCategory.RetrieveEmpty, false);
      this.FindAllWithoutSpecificationAndWithEmptySource_NoRecordsAreReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll03")]
    public void DatabaseRepository_FindAll_WithMatchingSpecification_OnlyMatchingRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll03\", TestCategory.Retrieve, false);
      this.FindAllWithSpecification_WithResults(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="MemoryRepository{TEntity}"/> implementation in combination with a <see cref="HttpSessionMemoryStore{T}"/>.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll04")]
    public void DatabaseRepository_FindAll_WithNotMatchingSpecification_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll04\", TestCategory.Retrieve, false);
      this.FindAllWithSpecification_WithoutResults(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll05")]
    public void DatabaseRepository_FindAll_WithSpecificationEmptySource_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll05\", TestCategory.RetrieveEmpty, false);
      this.FindAllWithSpecification_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll06")]
    public void DatabaseRepository_FindAll_SpecificationWithMaxResultsLessThanAvailable_MaxResultsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll06\", TestCategory.Sorting, false);
      this.RetrieveLessThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll07")]
    public void DatabaseRepository_FindAll_SpecificationWithMaxResultsExactlyAvailable_AvailableRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll07\", TestCategory.Sorting, false);
      this.RetrieveExactlyAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll08")]
    public void DatabaseRepository_FindAll_SpecificationWithMaxResultsExactlyAvailableEmptySource_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll08\", TestCategory.RetrieveEmpty, false);
      this.RetrieveExactlyAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll09")]
    public void DatabaseRepository_FindAll_SpecificationWithMaxResultsMoreThenAvailable_AvailableRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll09\", TestCategory.Sorting, false);
      this.RetrieveMoreThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll10")]
    public void DatabaseRepository_FindAll_SpecificationWithMaxResultsMoreThenAvailableEmptySource_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll10\", TestCategory.RetrieveEmpty, false);
      this.RetrieveMoreThenAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using type for which a custom mapping is 
    /// available.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll11")]
    public void DatabaseRepository_FindAll_CustomMappedType_AvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll11\", TestCategory.Sorting, false);
      this.RetrieveTypesWithCustomMapping(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll12")]
    public void HttpSessionMemoryRepository_FindAllWithoutCloning_NoCopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll12\", TestCategory.Retrieve, false);
      this.FindAllWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll13")]
    public void HttpSessionMemoryRepository_FindAllWithCloning_CopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll13\", TestCategory.Retrieve, true);
      this.FindAllWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll14")]
    public void DatabaseRepository_FindAll_WithMatchingExpression_OnlyMatchingRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll14\", TestCategory.Retrieve, true);
      this.FindAllWithExpression_WithResults(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindAll15")]
    public void DatabaseRepository_FindAll_WithNotMatchingExpression_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindAll15\", TestCategory.Retrieve, true);
      this.FindAllWithExpression_WithoutResults(sourceInfo);
    }
    #endregion

    #region FindSingle test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle01")]
    public void DatabaseRepository_FindSingle_WithMatchingSpecification_OnlyMatchingRecordIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle01\", TestCategory.Retrieve, false);
      this.FindSingleWithMatchingSpecification_OnlyMatchingRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle02")]
    public void DatabaseRepository_FindSingle_WithNotMatchingSpecification_NoRecordIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle02\", TestCategory.Retrieve, false);
      this.FindSingleWithNotMatchingSpecification_NoRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle03")]
    public void DatabaseRepository_FindSingle_WithNotMatchingSpecificationAndDefault_DefaultIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle03\", TestCategory.Retrieve, false);
      this.FindSingleWithNotMatchingSpecificationAndDefault_DefaultIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle04")]
    public void DatabaseRepository_FindSingle_WithSpecificationEmptySource_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle04\", TestCategory.RetrieveEmpty, false);
      this.FindSingleWithSpecification_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle05")]
    public void DatabaseRepository_FindSingle_WithMatchingExpression_OnlyMatchingRecordIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle05\", TestCategory.Retrieve, false);
      this.FindSingleWithMatchingExpression_OnlyMatchingRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle06")]
    public void DatabaseRepository_FindSingle_WithNotMatchingExpression_NoRecordIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle06\", TestCategory.Retrieve, false);
      this.FindSingleWithNotMatchingExpression_NoRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle07")]
    public void DatabaseRepository_FindSingle_WithNotMatchingExpressionAndDefault_DefaultIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle07\", TestCategory.Retrieve, false);
      this.FindSingleWithNotMatchingExpressionAndDefault_DefaultIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle08")]
    public void HttpSessionMemoryRepository_FindSingleWithoutCloning_NoCopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle08\", TestCategory.Retrieve, false);
      this.FindSingleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindSingle09")]
    public void HttpSessionMemoryRepository_FindSingleWithCloning_CopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindSingle09\", TestCategory.Retrieve, true);
      this.FindSingleWithCloning(sourceInfo);
    }
    #endregion

    #region FindFirst test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst01")]
    public void DatabaseRepository_FindFirst_WithMatchingSpecification_FirstMatchingRecordIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst01\", TestCategory.Retrieve, false);
      this.FindFirstWithMatchingSpecification_FirstMatchingRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst02")]
    public void DatabaseRepository_FindFirst_WithNotMatchingSpecification_NoRecordIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst02\", TestCategory.Retrieve, false);
      this.FindFirstWithNotMatchingSpecification_NoRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst03")]
    public void DatabaseRepository_FindFirst_WithNotMatchingSpecificationAndDefault_DefaultIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst03\", TestCategory.Retrieve, false);
      this.FindFirstWithNotMatchingSpecificationAndDefault_DefaultIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst04")]
    public void DatabaseRepository_FindFirst_WithSpecificationEmptySource_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst04\", TestCategory.RetrieveEmpty, false);
      this.FindFirstWithSpecification_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst05")]
    public void DatabaseRepository_FindFirstWithoutCloning_NoCopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst05\", TestCategory.Retrieve, false);
      this.FindFirstWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst06")]
    public void HttpSessionMemoryRepository_FindFirstWithCloning_CopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst06\", TestCategory.Retrieve, true);
      this.FindFirstWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst07")]
    public void DatabaseRepository_FindFirst_WithMatchingExpression_FirstMatchingRecordIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst07\", TestCategory.Retrieve, false);
      this.FindFirstWithMatchingExpression_FirstMatchingRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst08")]
    public void DatabaseRepository_FindFirst_WithNotMatchingExpression_NoRecordIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst08\", TestCategory.Retrieve, false);
      this.FindFirstWithNotMatchingExpression_NoRecordIsReturned(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\FindFirst09")]
    public void DatabaseRepository_FindFirst_WithNotMatchingExpressionAndDefault_DefaultIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\FindFirst09\", TestCategory.Retrieve, false);
      this.FindFirstWithNotMatchingExpressionAndDefault_DefaultIsReturned(sourceInfo);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Sorting01")]
    public void DatabaseRepository_SpecificationWithOrderBy_ItemsAreCorrectlyOrdered() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Sorting01\", TestCategory.Sorting, false);
      this.OrderBy(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Sorting02")]
    public void DatabaseRepository_SpecificationWithOrderByEmptySource_NoRecordsAreReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Sorting02\", TestCategory.RetrieveEmpty, false);
      this.OrderBy_EmptySource(sourceInfo);
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage01")]
    public void DatabaseRepository_Add_ItemIsStored() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage01\", TestCategory.Storage, true);
      this.Add(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage02")]
    public void DatabaseRepository_AddCustomMappedType_ItemIsStored() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage02\", TestCategory.Storage, true);
      this.AddCustomMappedType(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage03")]
    public void DatabaseRepository_Update_ItemIsUpdated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage03\", TestCategory.Storage, true);
      this.Update(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage04")]
    public void DatabaseRepository_UpdateCustomMappedType_ItemIsUpdated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage04\", TestCategory.Storage, true);
      this.UpdateCustomMappedType(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage05")]
    public void DatabaseRepository_Delete_ItemIsDeleted() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage05\", TestCategory.Storage, true);
      this.Delete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage06")]
    public void DatabaseRepository_AddMultiple_ItemsAreAdded() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage06\", TestCategory.Storage, true);
      this.AddMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage07")]
    public void DatabaseRepository_AddMultipleCustomMappedTypes_ItemsAreAdded() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage07\", TestCategory.Storage, true);
      this.AddMultipleCustomMappedTypes_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage08")]
    public void DatabaseRepository_DeleteMultiple_ItemsAreDeleted() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage08\", TestCategory.Storage, true);
      this.DeleteMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method when is should throw an exeption using 
    /// the <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage09")]
    public void DatabaseRepository_DeleteMultipleDeleteEntityTwice_OperationIsRolledBack() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage09\", TestCategory.Storage, true);
      this.DeleteMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage10")]
    public void DatabaseRepository_UpdateMultiple_ItemsAreUpdated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage10\", TestCategory.Storage, true);
      this.UpdateMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage11")]
    public void DatabaseRepository_UpdateMultipleCustomMappedTypes_ItemsAreUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage11\", TestCategory.Storage, true);
      this.UpdateMultipleCustomMappedTypes_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an exception
    /// using the <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage12")]
    public void DatabaseRepository_UpdateMultipleUpdateUnaddedEntity_OperationIsRolledBack() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage12\", TestCategory.Storage, true);
      this.UpdateMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage13")]
    public void DatabaseRepository_AddAndReset_AdditionIsUndone() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage13\", TestCategory.Storage, true);
      this.Add_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage14")]
    public void DatabaseRepository_UpdateAndReset_UpdateIsUndone() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage14\", TestCategory.Storage, true);
      this.Update_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage15")]
    public void DatabaseRepository_DeleteAndReset_DeletionIsUndone() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage15\", TestCategory.Storage, true);
      this.Delete_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage16")]
    public void DatabaseRepository_AddWithoutCloning_NoCopyIsCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage16\", TestCategory.Storage, false);
      this.AddWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage17")]
    public void DatabaseRepository_AddWithCloning_CopyIsCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage17\", TestCategory.Storage, true);
      this.AddWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage18")]
    public void DatabaseRepository_AddDeletedItemWithoutCloning_NoCopyIsCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage18\", TestCategory.Storage, false);
      this.AddDeletedItemWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage19")]
    public void DatabaseRepository_AddDeletedItemWithCloning_CopyIsCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage19\", TestCategory.Storage, true);
      this.AddDeletedItemWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage20")]
    public void DatabaseRepository_AddMultipleWithoutCloning_NoCopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage20\", TestCategory.Storage, false);
      this.AddMultipleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage21")]
    public void DatabaseRepository_AddMultipleWithCloning_CopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage21\", TestCategory.Storage, true);
      this.AddMultipleWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage22")]
    public void DatabaseRepository_UpdateWithoutCloning_NoCopyIsCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage22\", TestCategory.Storage, false);
      this.UpdateWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage23")]
    public void DatabaseRepository_UpdateWithCloning_CopyIsCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage23\", TestCategory.Storage, true);
      this.UpdateWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage24")]
    public void DatabaseRepository_UpdateMultipleWithoutCloning_NoCopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage24\", TestCategory.Storage, false);
      this.UpdateMultipleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Storage25")]
    public void DatabaseRepository_UpdateMultipleWithCloning_CopiesAreCreated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Storage25\", TestCategory.Storage, true); 
      this.UpdateMultipleWithCloning(sourceInfo);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\CombinedStorage01")]
    public void DatabaseRepository_AddAndUpdate_ItemsAreStored() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\CombinedStorage01\", TestCategory.Storage, true);
      this.AddUpdate(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\CombinedStorage02")]
    public void DatabaseRepository_AddUpdateAndDelete_ItemsAreStored() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\CombinedStorage02\", TestCategory.Storage, true);
      this.AddUpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\CombinedStorage03")]
    public void DatabaseRepository_UpdateAndDelete_ItemsAreStored() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\CombinedStorage03\", TestCategory.Storage, true);
      this.UpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\CombinedStorage04")]
    public void DatabaseRepository_DeleteAndAdd_ItemsAreStored() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\CombinedStorage04\", TestCategory.Storage, true);
      this.DeleteAdd(sourceInfo);
    }
    #endregion

    #region Execute test-case contracts
    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Execute01")]
    public void DatabaseRepository_ExecuteWithDefaultSpecification_NullIsReturned() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Execute01\", TestCategory.Storage, false);
      this.ExecuteDefaultSpecification(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Execute02")]
    [ExpectedException(typeof(NotSupportedException))]
    public void DatabaseRepository_ExecuteWithBusinessRule_ExceptionIsThrown() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Execute02\", TestCategory.Storage, false);
      this.ExecuteBusinessRule(sourceInfo);
    }
    #endregion

    #region Custom query test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when executing a business rule that retrieves a single result using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Custom01")]
    public void DatabaseRepository_ExecuteBusinessRuleWithSingleResult_ResultIsRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Custom01\", TestCategory.Storage, false);
      Repository<TestDummy> repository = new TestDatabaseRepository(sourceInfo);

      /* Add some ectra records to te database */
      TestDummy itemA = new TestDummy { BooleanValue = true, NumericValue = 6, TextValue = "NothingA" };
      TestDummy itemB = new TestDummy { BooleanValue = true, NumericValue = 6, TextValue = "NothingB" };
      TestDummy itemC = new TestDummy { BooleanValue = true, NumericValue = 7, TextValue = "Hit" };
      TestDummy itemD = new TestDummy { BooleanValue = true, NumericValue = 6, TextValue = "NothingD" };

      repository.AddEntity(itemA);
      repository.AddEntity(itemB);
      repository.AddEntity(itemC);
      repository.AddEntity(itemD);

      repository.SaveChanges();

      /* Create the specification */
      ISpecification<TestDummy> selectSpec = Specification.BusinessRule<TestDummy>("CustomQuery_SingleResult", "Hit");
      Assert.IsNotNull(selectSpec);

      /* Execute the query */
      TestDummy result = repository.FindFirst(selectSpec);
      Assert.IsNotNull(result);
      Assert.AreEqual("Hit", result.TextValue, false);
      Assert.AreEqual(7, result.NumericValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when executing a business rule that retrieves multiple results using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\Custom02")]
    public void DatabaseRepository_ExecuteBusinessRuleWithMultipleResults_ResultsAreRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\Custom02\", TestCategory.Storage, false);
      Repository<TestDummy> repository = new TestDatabaseRepository(sourceInfo);

      /* Add some ectra records to te database */
      TestDummy itemA = new TestDummy { BooleanValue = true, NumericValue = 6, TextValue = "NothingA" };
      TestDummy itemB = new TestDummy { BooleanValue = true, NumericValue = 6, TextValue = "NothingB" };
      TestDummy itemC = new TestDummy { BooleanValue = true, NumericValue = 7, TextValue = "Hit" };
      TestDummy itemD = new TestDummy { BooleanValue = true, NumericValue = 6, TextValue = "NothingD" };

      repository.AddEntity(itemA);
      repository.AddEntity(itemB);
      repository.AddEntity(itemC);
      repository.AddEntity(itemD);

      repository.SaveChanges();

      /* Create the specification */
      ISpecification<TestDummy> selectSpec = Specification.BusinessRule<TestDummy>("CustomQuery_MultipleResults", 3, 2);
      Assert.IsNotNull(selectSpec);

      /* Execute the query */
      IEnumerable<TestDummy> result = repository.FindAll(selectSpec);
      Assert.IsNotNull(result);
      Assert.AreEqual(3, result.Count());
      Assert.IsNotNull(result.SingleOrDefault(td => td.TextValue.Equals("NothingA", StringComparison.Ordinal)));
      Assert.IsNotNull(result.SingleOrDefault(td => td.TextValue.Equals("NothingB", StringComparison.Ordinal)));
      Assert.IsNotNull(result.SingleOrDefault(td => td.TextValue.Equals("NothingD", StringComparison.Ordinal)));
    }
    #endregion

    #region Retrieve graph operations
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(string)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\RetrieveGraph01")]
    public void DatabaseRepository_RetrieveGraphAll_SubentitiesAreRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\RetrieveGraph01\", TestCategory.Retrieve, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      IEnumerable<TestParentDummy> result = repository.FindAll("Children");
      Assert.AreEqual(6, result.Count());
      Assert.IsNotNull(result.ElementAt(0).Children);
      Assert.AreEqual(3, result.ElementAt(0).Children.Count);
      Assert.IsNotNull(result.ElementAt(1).Children);
      Assert.AreEqual(1, result.ElementAt(1).Children.Count);
      Assert.IsNotNull(result.ElementAt(2).Children);
      Assert.AreEqual(2, result.ElementAt(2).Children.Count);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(string)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\RetrieveGraph02")]
    public void DatabaseRepository_RetrieveGraphAllUsingSpecification_SubentitiesAreRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\RetrieveGraph02\", TestCategory.Retrieve, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      ISpecification<TestParentDummy> spec = Specification.Lambda<TestParentDummy>(td => td.BooleanValue).OrderBy(td => td.RecordId);
      spec.Include("Children");
      IEnumerable<TestParentDummy> result = repository.FindAll(spec);
      Assert.AreEqual(4, result.Count());
      Assert.IsNotNull(result.ElementAt(0).Children);
      Assert.AreEqual(1, result.ElementAt(0).Children.Count);
      Assert.IsNotNull(result.ElementAt(1).Children);
      Assert.AreEqual(2, result.ElementAt(1).Children.Count);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(string)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\RetrieveGraph03")]
    public void DatabaseRepository_RetrieveGraphAllUsingExpression_SubentitiesAreRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\RetrieveGraph03\", TestCategory.Retrieve, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      IEnumerable<TestParentDummy> result = repository.FindAll(td => td.BooleanValue, "Children");
      Assert.AreEqual(4, result.Count());
      Assert.IsNotNull(result.ElementAt(0).Children);
      Assert.AreEqual(1, result.ElementAt(0).Children.Count);
      Assert.IsNotNull(result.ElementAt(1).Children);
      Assert.AreEqual(2, result.ElementAt(1).Children.Count);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\RetrieveGraph04")]
    public void DatabaseRepository_RetrieveGraphFirstUsingSpecification_SubentitiesAreRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\RetrieveGraph04\", TestCategory.Retrieve, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      ISpecification<TestParentDummy> spec = Specification.Lambda<TestParentDummy>(td => td.BooleanValue).OrderBy(td => td.RecordId);
      spec.Include("Children");
      TestParentDummy result = repository.FindFirst(spec);
      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Children);
      Assert.AreEqual(1, result.Children.Count);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\RetrieveGraph05")]
    public void DatabaseRepository_RetrieveGraphFirstUsingExpression_SubentitiesAreRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\RetrieveGraph05\", TestCategory.Retrieve, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy result = repository.FindFirst(td => td.BooleanValue, "Children");
      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Children);
      Assert.AreEqual(1, result.Children.Count);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\RetrieveGraph06")]
    public void DatabaseRepository_RetrieveGraphSingleUsingSpecification_SubentitiesAreRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\RetrieveGraph06\", TestCategory.Retrieve, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      ISpecification<TestParentDummy> spec = Specification.Lambda<TestParentDummy>(td => td.RecordId == 1);
      spec.Include("Children");
      TestParentDummy result = repository.FindSingle(spec);
      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Children);
      Assert.AreEqual(3, result.Children.Count);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\RetrieveGraph07")]
    public void DatabaseRepository_RetrieveGraphSingleUsingExpression_SubentitiesAreRetrieved() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\RetrieveGraph07\", TestCategory.Retrieve, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy result = repository.FindFirst(td => td.RecordId == 1, "Children");
      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Children);
      Assert.AreEqual(3, result.Children.Count);
    }
    #endregion

    #region Save graph operations
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\SaveGraph01")]
    public void DatabaseRepository_AddGraphEntireGraph_SubentitiesAreAdded() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\SaveGraph01\", TestCategory.Storage, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy newDummy = new TestParentDummy {
        TextValue = "Row X",
        NumericValue = 42,
        BooleanValue = true,
        Children = new List<TestSubDummy> { 
          new TestSubDummy { TextValue = "Subrow X_1" }, 
          new TestSubDummy { TextValue = "Subrow X_2" } 
        }
      };

      TestParentDummy addedDummy = repository.AddEntity(newDummy);
      repository.SaveChanges();

      repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy result = repository.FindSingle(td => td.TextValue == "Row X", "Children");
      Assert.IsNotNull(result);
      Assert.IsNotNull(result.Children);
      Assert.AreEqual(2, result.Children.Count);
      Assert.IsTrue(result.Children.ElementAt(0).RecordId > 0);
      Assert.IsTrue(result.Children.ElementAt(1).RecordId > 0);
      Assert.AreEqual(result.RecordId, result.Children.ElementAt(0).ParentId);
      Assert.AreEqual(result.RecordId, result.Children.ElementAt(1).ParentId);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\SaveGraph02")]
    public void DatabaseRepository_DeleteGraphEntireGraph_SubentitiesAreDeleted() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\SaveGraph02\", TestCategory.Storage, false);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy obsoleteDummy = repository.FindSingle(td => td.RecordId == 1, "Children");
      Assert.AreEqual(3, obsoleteDummy.Children.Count);

      repository.DeleteEntity(obsoleteDummy);
      repository.SaveChanges();

      repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy result = repository.FindSingle(td => td.RecordId == 1, "Children");
      Assert.IsNull(result);

      List<TestSubDummy> subDummies = (sourceInfo as DatabaseSourceInfo).DbContext.Set<TestSubDummy>().ToList();
      Assert.AreEqual(0, subDummies.Count);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\SaveGraph03")]
    public void DatabaseRepository_UpdateGraphEntireGraph_SubentitiesAreUpdated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\SaveGraph03\", TestCategory.Storage, false, true);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy targetDummy = repository.FindSingle(td => td.RecordId == 1, "Children");
      Assert.AreEqual(3, targetDummy.Children.Count);

      targetDummy.NumericValue = 999;
      targetDummy.Children.ElementAt(1).TextValue = "New value";
      
      repository.UpdateEntity(targetDummy);
      repository.SaveChanges();

      repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy result = repository.FindSingle(td => td.RecordId == 1, "Children");
      Assert.IsNotNull(result);
      Assert.AreEqual(3, result.Children.Count);
      Assert.AreEqual(999, result.NumericValue);
      Assert.AreEqual("New value", result.Children.ElementAt(1).TextValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\SaveGraph04")]
    public void DatabaseRepository_UpdateGraphUpdateChild_SubentityIsUpdated() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\SaveGraph04\", TestCategory.Storage, false, true);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy targetDummy = repository.FindSingle(td => td.RecordId == 1, "Children");
      Assert.AreEqual(3, targetDummy.Children.Count);

      targetDummy.Children.ElementAt(1).TextValue = "New value";

      repository.UpdateEntity(targetDummy);
      repository.SaveChanges();

      repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy result = repository.FindSingle(td => td.RecordId == 1, "Children");
      Assert.IsNotNull(result);
      Assert.AreEqual(3, result.Children.Count);
      Assert.AreEqual("New value", result.Children.ElementAt(1).TextValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\SaveGraph05")]
    public void DatabaseRepository_UpdateGraphAddChild_SubentityIsAdded() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\SaveGraph05\", TestCategory.Storage, false, true);
      Repository<TestParentDummy> repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy targetDummy = repository.FindSingle(td => td.RecordId == 1, "Children");
      Assert.AreEqual(3, targetDummy.Children.Count);

      targetDummy.Children.Add(new TestSubDummy { TextValue = "New value" });

      repository.UpdateEntity(targetDummy);
      repository.SaveChanges();

      repository = new DatabaseRepository<TestParentDummy>(sourceInfo);

      TestParentDummy result = repository.FindSingle(td => td.RecordId == 1, "Children");
      Assert.IsNotNull(result);
      Assert.AreEqual(4, result.Children.Count);
      Assert.AreEqual("New value", result.Children.ElementAt(3).TextValue);
      Assert.AreEqual(result.RecordId, result.Children.ElementAt(3).ParentId);
    }
    #endregion

    #region Implementation of RepositoryTest
    /// <summary>Creates a new repository using the specified <see cref="DataSourceInfo"/>.</summary>
    /// <typeparam name="T">The type of entity that must be handled by the repository.</typeparam>
    /// <param name="sourceInfo">The data source information that will be used to create a new repository.</param>
    /// <returns>The created repository.</returns>
    protected override Repository<T> CreateRepository<T>(DataSourceInfo sourceInfo) {
      /* Create a new DataSourceInfo, to make sure that the repository uses a fresh context and connection to the database */
      bool cloneDataSourceItems = sourceInfo.CloneDataSourceItems;
      DbContext context = null;
      DatabaseSourceInfo databaseSourceInfo = sourceInfo as DatabaseSourceInfo;
      if(databaseSourceInfo != null && databaseSourceInfo.IsDbContextSpecified()) {
        context = new DatabaseRepositoryTestContext(databaseSourceInfo.DbContext.Database.Connection.ConnectionString);
      }
      else {
        context = new DatabaseRepositoryTestContext();
      }

      DataSourceInfo newSourceInfo = new DatabaseSourceInfo(context, cloneDataSourceItems);
      return new DatabaseRepository<T>(newSourceInfo);
    }
    #endregion

    #region Private helper methods
    /// <summary>Constructs a new instance of the <see cref="DataSourceInfo"/> class that can be used in a test case.</summary>
    /// <param name="databaseSubPath">The sub path relative to the execution path where the database will be created.</param>
    /// <param name="testCategory">The category of the test.</param>
    /// <param name="cloneDataSourceItems">Indicates if the retrieved items must be cloned before returning them.</param>
    /// <param name="saveGraph">Indicates if the entire graph shuld be saved when saving an entity.</param>
    /// <returns>The constructed source info.</returns>
    private static DataSourceInfo ConstructDataSourceInfo(string databaseSubPath, TestCategory testCategory, bool cloneDataSourceItems, bool saveGraph = false) {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, databaseSubPath, "Enkoni.Framework.Entities.Tests.DatabaseRepositoryTestContext.sdf");

      Database.SetInitializer(new DatabaseRepositoryInitializer(testCategory));

      DbContext context = new DatabaseRepositoryTestContext("Data Source=\"" + databaseBasePath + "\"");
      context.Database.Initialize(true);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, cloneDataSourceItems, saveGraph);
      return sourceInfo;
    }
    #endregion
  }
}
