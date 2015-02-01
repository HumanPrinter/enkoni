using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="XmlFileRepository{TEntity}"/> class.</summary>
  [TestClass]
  public class XmlFileRepositoryTest : FileRepositoryTest {
    #region Retrieve test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve01")]
    public void XmlFileRepository_FindAll_NoSpecification_AllAvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve01\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindAll(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Retrieve02")]
    public void XmlFileRepository_FindAll_NoSpecificationEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve02\ReposTest_EmptyInputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindAll_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve03")]
    public void XmlFileRepository_FindAll_WithSpecification_OnlyMatchingRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve03\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindAllWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Retrieve04")]
    public void XmlFileRepository_FindAll_WithExpressionEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve04\ReposTest_EmptyInputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindAllWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve05")]
    public void XmlFileRepository_FindSingle_WithExpression_OnlyMatchingRecordIsReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve05\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindSingleWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Retrieve06")]
    public void XmlFileRepository_FindSingle_WithExpressionEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve06\ReposTest_EmptyInputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindSingleWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method .</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve07")]
    public void XmlFileRepository_FindFirst_WithExpression_FirstMatchingRecordIsReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve07\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindFirstWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Retrieve08")]
    public void XmlFileRepository_FindFirst_WithExpressionEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve08\ReposTest_EmptyInputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindFirstWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\Retrieve09")]
    public void XmlFileRepository_FindAll_SpecificationWithMaxResultsLessThanAvailable_MaxResultsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve09\ReposTest_SortingFile.xml"), true, 3000, Encoding.UTF8, false);
      this.RetrieveLessThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\Retrieve10")]
    public void XmlFileRepository_FindAll_SpecificationWithMaxResultsExactlyAvailable_AvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve10\ReposTest_SortingFile.xml"), true, 3000, Encoding.UTF8, false);
      this.RetrieveExactlyAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a maximum 
    /// number of results.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Retrieve11")]
    public void XmlFileRepository_FindAll_SpecificationWithMaxResultsExactlyAvailableAndEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve11\ReposTest_EmptyInputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.RetrieveExactlyAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\Retrieve12")]
    public void XmlFileRepository_FindAll_SpecificationWithMaxResultsMoreThenAvailable_AvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve12\ReposTest_SortingFile.xml"), true, 3000, Encoding.UTF8, false);
      this.RetrieveMoreThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a maximum 
    /// number of results.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Retrieve13")]
    public void XmlFileRepository_FindAll_SpecificationWithMaxResultsMoreThenAvailableEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve13\ReposTest_EmptyInputFile.xml"), true, 3000, Encoding.UTF8, true);
      this.RetrieveMoreThenAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using type for which a custom mapping is 
    /// available.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\Retrieve14")]
    public void XmlFileRepository_FindAll_CustomMappedType_AvailableRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve14\ReposTest_SortingFile.xml"), true, 3000, Encoding.UTF8, false);
      this.RetrieveTypesWithCustomMapping(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve15")]
    public void XmlFileRepository_FindFirstWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve15\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindFirstWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve16")]
    public void XmlFileRepository_FindFirstWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve16\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, true);
      this.FindFirstWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve17")]
    public void XmlFileRepository_FindSingleWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve17\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindSingleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve18")]
    public void XmlFileRepository_FindSingleWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve18\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, true);
      this.FindSingleWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method without cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve19")]
    public void XmlFileRepository_FindAllWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve19\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.FindAllWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method with cloning.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Retrieve20")]
    public void XmlFileRepository_FindAllWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Retrieve20\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, true);
      this.FindAllWithCloning(sourceInfo);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a specific ordering 
    /// specification.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\Sorting01")]
    public void XmlFileRepository_SpecificationWithOrderBy_ItemsAreCorrectlyOrdered() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Sorting01\ReposTest_SortingFile.xml"), true, 3000, Encoding.UTF8, false);
      this.OrderBy(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a specific
    /// ordering specification.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Sorting02")]
    public void XmlFileRepository_SpecificationWithOrderByEmptySource_NoRecordsAreReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Sorting02\ReposTest_EmptyInputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.OrderBy_EmptySource(sourceInfo);
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage01")]
    public void XmlFileRepository_Add_ItemIsStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage01\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.Add(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage02")]
    public void XmlFileRepository_AddCustomMappedType_ItemIsStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage02\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.AddCustomMappedType(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage03")]
    public void XmlFileRepository_Update_ItemIsUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage03\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.Update(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage04")]
    public void XmlFileRepository_UpdateCustomMappedType_ItemIsUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage04\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.UpdateCustomMappedType(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage05")]
    public void XmlFileRepository_Delete_ItemIsDeleted() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage05\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.Delete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage06")]
    public void XmlFileRepository_AddMultiple_ItemsAreAdded() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage06\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.AddMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage07")]
    public void XmlFileRepository_AddMultipleCustomMappedTypes_ItemsAreAdded() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage07\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.AddMultipleCustomMappedTypes_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage08")]
    public void XmlFileRepository_DeleteMultiple_ItemsAreDeleted() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage08\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.DeleteMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage09")]
    public void XmlFileRepository_DeleteMultipleDeleteEntityTwice_OperationIsRolledBack() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage09\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.DeleteMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage10")]
    public void XmlFileRepository_UpdateMultiple_ItemsAreUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage10\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage11")]
    public void XmlFileRepository_UpdateMultipleCustomMappedTypes_ItemsAreUpdated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage11\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultipleCustomMappedTypes_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage12")]
    public void XmlFileRepository_UpdateMultipleUpdateUnaddedEntity_OperationIsRolledBack() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage12\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage13")]
    public void XmlFileRepository_AddAndReset_AdditionIsUndone() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage13\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.Add_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved updates to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage14")]
    public void XmlFileRepository_UpdateAndReset_UpdateIsUndone() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage14\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.Update_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved deletions from the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage15")]
    public void XmlFileRepository_DeleteAndReset_DeletionIsUndone() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage15\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.Delete_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage16")]
    public void XmlFileRepository_AddWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage16\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, false); 
      this.AddWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage17")]
    public void XmlFileRepository_AddWithCloning_CopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage17\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true); 
      this.AddWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage18")]
    public void XmlFileRepository_AddDeletedItemWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage18\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, false);
      this.AddDeletedItemWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage19")]
    public void XmlFileRepository_AddDeletedItemWithCloning_CopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage19\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.AddDeletedItemWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage20")]
    public void XmlFileRepository_AddMultipleWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage20\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, false);
      this.AddMultipleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage21")]
    public void XmlFileRepository_AddMultipleWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage21\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.AddMultipleWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage22")]
    public void XmlFileRepository_UpdateWithoutCloning_NoCopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage22\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, false);
      this.UpdateWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage23")]
    public void XmlFileRepository_UpdateWithCloning_CopyIsCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage23\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.UpdateWithCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage24")]
    public void XmlFileRepository_UpdateMultipleWithoutCloning_NoCopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage24\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, false);
      this.UpdateMultipleWithoutCloning(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Storage25")]
    public void XmlFileRepository_UpdateMultipleWithCloning_CopiesAreCreated() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Storage25\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultipleWithCloning(sourceInfo);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\CombinedStorage01")]
    public void XmlFileRepository_AddAndUpdate_ItemsAreStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\CombinedStorage01\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.AddUpdate(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\CombinedStorage02")]
    public void XmlFileRepository_AddUpdateAndDelete_ItemsAreStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\CombinedStorage02\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.AddUpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\CombinedStorage03")]
    public void XmlFileRepository_UpdateAndDelete_ItemsAreStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\CombinedStorage03\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.UpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\CombinedStorage04")]
    public void XmlFileRepository_DeleteAndAdd_ItemsAreStored() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\CombinedStorage04\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, true);
      this.DeleteAdd(sourceInfo);
    }
    #endregion

    #region Execute test-case contracts
    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"CsvFileRepositoryTest\Execute01")]
    public void XmlFileRepository_ExecuteWithDefaultSpecification_NullIsReturned() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Execute01\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, false);
      this.ExecuteDefaultSpecification(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"CsvFileRepositoryTest\Execute02")]
    [ExpectedException(typeof(NotSupportedException))]
    public void XmlFileRepository_ExecuteWithBusinessRule_ExceptionIsThrown() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\Execute02\ReposTest_DataSourceFile.xml"), true, 3000, Encoding.UTF8, false);
      this.ExecuteBusinessRule(sourceInfo);
    }
    #endregion

    #region Read test-cases
    /// <summary>Tests the functionality of the <see cref="XmlFileRepository{T}.ReadAllRecordsFromFile(FileInfo,DataSourceInfo)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Read01")]
    public void XmlFileRepository_ReadFile_AllItemsAreRetrieved() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Read01\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.ReadFile(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="XmlFileRepository{T}.ReadAllRecordsFromFile(FileInfo,DataSourceInfo)"/> method when 
    /// reading an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Read02")]
    public void XmlFileRepository_ReadEmptyFile_NoItemsAreRetrieved() {
      /* Create the repositiry */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Read02\ReposTest_EmptyInputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.ReadEmptyFile(sourceInfo);
    }
    #endregion

    #region Write test-cases
    /// <summary>Tests the functionality of the <see cref="XmlFileRepository{T}.WriteAllRecordsToFile(FileInfo,DataSourceInfo,IEnumerable{T})"/> 
    /// method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Write01")]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\Write01")]
    public void XmlFileRepository_WriteFile_ResultFileMatchesReferenceFile() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Write01\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.WriteFile(sourceInfo, @"XmlFileRepositoryTest\Write01\ReposTest_DataSourceFile.xml");
    }

    /// <summary>Tests the functionality of the <see cref="XmlFileRepository{T}.WriteAllRecordsToFile(FileInfo,DataSourceInfo,IEnumerable{T})"/> 
    /// method when writing an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\Write02")]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\Write02")]
    public void XmlFileRepository_WriteEmptyFile_ResultFileMatchesReferenceFile() {
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\Write02\ReposTest_InputFile.xml"), true, 3000, Encoding.UTF8, false);
      this.WriteEmptyFile(sourceInfo, @"XmlFileRepositoryTest\Write02\ReposTest_EmptyInputFile.xml");
    }
    #endregion

    #region Implementation of RepositoryTest
    /// <summary>Creates a new repository using the specified <see cref="DataSourceInfo"/>.</summary>
    /// <typeparam name="T">The type of entity that must be handled by the repository.</typeparam>
    /// <param name="sourceInfo">The data source information that will be used to create a new repository.</param>
    /// <returns>The created repository.</returns>
    protected override Repository<T> CreateRepository<T>(DataSourceInfo sourceInfo) {
      return new XmlFileRepository<T>(sourceInfo);
    }
    #endregion
  }
}
