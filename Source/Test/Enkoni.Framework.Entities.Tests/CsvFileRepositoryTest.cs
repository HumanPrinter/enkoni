using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="CsvFileRepository{TEntity}"/> class.</summary>
  [TestClass]
  public class CsvFileRepositoryTest : FileRepositoryTest {
    #region Retrieve test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve01")]
    public void CsvFileRepository_FindAll_NoSpecification_AllAvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve01\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindAll(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Retrieve02")]
    public void CsvFileRepository_FindAll_NoSpecificationEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve02\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindAll_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve03")]
    public void CsvFileRepository_FindAll_WithSpecification_OnlyMatchingRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve03\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindAllWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Retrieve04")]
    public void CsvFileRepository_FindAll_WithSpecificationEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve04\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindAllWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve05")]
    public void CsvFileRepository_FindSingle_WithSpecification_OnlyMatchingRecordIsReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve05\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindSingleWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Retrieve06")]
    public void CsvFileRepository_FindSingle_WithSpecificationEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve06\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindSingleWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method .</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve07")]
    public void CsvFileRepository_FindFirst_WithSpecification_FirstMatchingRecordIsReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve07\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindFirstWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Retrieve08")]
    public void CsvFileRepository_FindFirst_WithSpecificationEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve08\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindFirstWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\Retrieve09")]
    public void CsvFileRepository_FindAll_SpecificationWithMaxResultsLessThanAvailable_MaxResultsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve09\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, false);
      this.RetrieveLessThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\Retrieve10")]
    public void CsvFileRepository_FindAll_SpecificationWithMaxResultsExactlyAvailable_AvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve10\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, false);
      this.RetrieveExactlyAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a maximum 
    /// number of results.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Retrieve11")]
    public void CsvFileRepository_FindAll_SpecificationWithMaxResultsExactlyAvailableEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve11\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.RetrieveExactlyAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\Retrieve12")]
    public void CsvFileRepository_FindAll_SpecificationWithMaxResultsMoreThenAvailable_AvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve12\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, false);
      this.RetrieveMoreThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a maximum 
    /// number of results.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Retrieve13")]
    public void CsvFileRepository_FindAll_SpecificationWithMaxResultsMoreThenAvailableAndEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve13\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.RetrieveMoreThenAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using type for which a custom mapping is 
    /// available.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\Retrieve14")]
    public void CsvFileRepository_FindAll_CustomMappedType_AvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve14\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, false);
      this.RetrieveTypesWithCustomMapping(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve15")]
    public void CsvFileRepository_FindFirstWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve15\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindFirstWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve16")]
    public void CsvFileRepository_FindFirstWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve16\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindFirstWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve17")]
    public void CsvFileRepository_FindSingleWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve17\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindSingleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve18")]
    public void CsvFileRepository_FindSingleWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve18\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindSingleWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve19")]
    public void CsvFileRepository_FindAllWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve19\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.FindAllWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Retrieve20")]
    public void CsvFileRepository_FindAllWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Retrieve20\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindAllWithCloning(sourceInfo);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a specific ordering 
    /// specification.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\Sorting01")]
    public void CsvFileRepository_SpecificationWithOrderBy_ItemsAreCorrectlyOrdered() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Sorting01\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, false);
      this.OrderBy(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a specific
    /// ordering specification.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Sorting02")]
    public void CsvFileRepository_SpecificationWithOrderByEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Sorting02\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.OrderBy_EmptySource(sourceInfo);
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage01")]
    public void CsvFileRepository_Add_ItemIsStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage01\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Add(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage02")]
    public void CsvFileRepository_AddCustomMappedType_ItemIsStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage02\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddCustomMappedType(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage03")]
    public void CsvFileRepository_Update_ItemIsUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage03\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Update(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage04")]
    public void CsvFileRepository_UpdateCustomMappedType_ItemIsUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage04\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateCustomMappedType(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage05")]
    public void CsvFileRepository_Delete_ItemIsDeleted() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage05\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Delete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage06")]
    public void CsvFileRepository_AddMultiple_ItemsAreAdded() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage06\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage07")]
    public void CsvFileRepository_AddMultipleCustomMappedTypes_ItemsAreAdded() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage07\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddMultipleCustomMappedTypes_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage08")]
    public void CsvFileRepository_DeleteMultiple_ItemsAreDeleted() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage08\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.DeleteMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage09")]
    public void CsvFileRepository_DeleteMultipleDeleteEntityTwice_OperationIsRolledBack() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage09\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.DeleteMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage10")]
    public void CsvFileRepository_UpdateMultiple_ItemsAreUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage10\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage11")]
    public void CsvFileRepository_UpdateMultipleCustomMappedTypes_ItemsAreUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo = 
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage11\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultipleCustomMappedTypes_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage12")]
    public void CsvFileRepository_UpdateMultipleUpdateUnaddedEntity_OperationIsRolledBack() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage12\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage13")]
    public void CsvFileRepository_AddAndReset_AdditionIsUndone() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage13\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Add_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved updates to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage14")]
    public void CsvFileRepository_UpdateAndReset_UpdateIsUndone() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage14\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Update_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved deletions from the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage15")]
    public void CsvFileRepository_DeleteAndReset_DeletionIsUndone() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage15\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Delete_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage16")]
    public void CsvFileRepository_AddWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage16\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, false);
      this.AddWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage17")]
    public void CsvFileRepository_AddWithCloning_CopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage17\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage18")]
    public void CsvFileRepository_AddDeletedItemWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage18\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, false);
      this.AddDeletedItemWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage19")]
    public void CsvFileRepository_AddDeletedItemWithCloning_CopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage19\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddDeletedItemWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage20")]
    public void CsvFileRepository_AddMultipleWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage20\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, false);
      this.AddMultipleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage21")]
    public void CsvFileRepository_AddMultipleWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage21\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddMultipleWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage22")]
    public void CsvFileRepository_UpdateWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage22\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, false);
      this.UpdateWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage23")]
    public void CsvFileRepository_UpdateWithCloning_CopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage23\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage24")]
    public void CsvFileRepository_UpdateMultipleWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage24\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, false);
      this.UpdateMultipleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Storage25")]
    public void CsvFileRepository_UpdateMultipleWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Storage25\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultipleWithCloning(sourceInfo);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\CombinedStorage01")]
    public void CsvFileRepository_AddAndUpdate_ItemsAreStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\CombinedStorage01\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddUpdate(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\CombinedStorage02")]
    public void CsvFileRepository_AddUpdateAndDelete_ItemsAreStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\CombinedStorage02\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddUpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\CombinedStorage03")]
    public void CsvFileRepository_UpdateAndDelete_ItemsAreStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\CombinedStorage03\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\CombinedStorage04")]
    public void CsvFileRepository_DeleteAndAdd_ItemsAreStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\CombinedStorage04\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.DeleteAdd(sourceInfo);
    }
    #endregion

    #region Execute test-case contracts
    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Execute01")]
    public void CsvFileRepository_ExecuteWithDefaultSpecification_NullIsReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Execute01\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, false);
      this.ExecuteDefaultSpecification(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Execute02")]
    [ExpectedException(typeof(NotSupportedException))]
    public void CsvFileRepository_ExecuteWithBusinessRule_ExceptionIsThrown() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Execute02\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, false);
      this.ExecuteBusinessRule(sourceInfo);
    }
    #endregion

    #region Read test-cases
    /// <summary>Tests the functionality of the <see cref="CsvFileRepository{T}.ReadAllRecordsFromFile(FileInfo,DataSourceInfo)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Read01")]
    public void CsvFileRepository_ReadFile_AllItemsAreRetrieved() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Read01\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.ReadFile(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="CsvFileRepository{T}.ReadAllRecordsFromFile(FileInfo,DataSourceInfo)"/> method when 
    /// reading an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Read02")]
    public void CsvFileRepository_ReadEmptyFile_NoItemsAreRetrieved() {
      /* Create the repositiry */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Read02\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.ReadEmptyFile(sourceInfo);
    }
    #endregion

    #region Write test-cases
    /// <summary>Tests the functionality of the <see cref="CsvFileRepository{T}.WriteAllRecordsToFile(FileInfo,DataSourceInfo,IEnumerable{T})"/>
    /// method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Write01")]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\Write01")]
    public void CsvFileRepository_WriteFile_ResultFileMatchesReferenceFile() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Write01\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.WriteFile(sourceInfo, @"CsvFileRepositoryTest\Write01\ReposTest_DataSourceFile.csv");
    }

    /// <summary>Tests the functionality of the <see cref="CsvFileRepository{T}.WriteAllRecordsToFile(FileInfo,DataSourceInfo,IEnumerable{T})"/> 
    /// method when writing an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\Write02")]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\Write02")]
    public void CsvFileRepository_WriteEmptyFile_ResultFileMatchesReferenceFile() {
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Write02\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, false);
      this.WriteEmptyFile(sourceInfo, @"CsvFileRepositoryTest\Write02\ReposTest_EmptyInputFile.csv");
    }
    #endregion

    #region Implementation of RepositoryTest
    /// <summary>Creates a new repository using the specified <see cref="DataSourceInfo"/>.</summary>
    /// <typeparam name="T">The type of entity that must be handled by the repository.</typeparam>
    /// <param name="sourceInfo">The data source information that will be used to create a new repository.</param>
    /// <returns>The created repository.</returns>
    protected override Repository<T> CreateRepository<T>(DataSourceInfo sourceInfo) {
      return new CsvFileRepository<T>(sourceInfo);
    }
    #endregion
  }
}
