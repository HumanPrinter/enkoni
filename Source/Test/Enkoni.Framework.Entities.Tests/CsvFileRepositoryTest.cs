//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvFileRepositoryTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the CsvRepository class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

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
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\TestCase01")]
    public override void TestCase01_FindAll() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase01\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindAll(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase02")]
    public override void TestCase02_FindAll_EmptySource() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase02\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindAll_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\TestCase03")]
    public override void TestCase03_FindAllWithExpression() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase03\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindAllWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase04")]
    public override void TestCase04_FindAllWithExpression_EmptySource() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase04\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindAllWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\TestCase05")]
    public override void TestCase05_FindSingleWithExpression() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase05\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindSingleWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase06")]
    public override void TestCase06_FindSingleWithExpression_EmptySource() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase06\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindSingleWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method .</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\TestCase07")]
    public override void TestCase07_FindFirstWithExpression() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase07\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindFirstWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase08")]
    public override void TestCase08_FindFirstWithExpression_EmptySource() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase08\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.FindFirstWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\TestCase09")]
    public override void TestCase09_RetrieveLessThenAvailable() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase09\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, true);
      this.RetrieveLessThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\TestCase10")]
    public override void TestCase10_RetrieveExactlyAvailable() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase10\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, true);
      this.RetrieveExactlyAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a maximum 
    /// number of results.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase11")]
    public override void TestCase11_RetrieveExactlyAvailable_EmptySource() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase11\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.RetrieveExactlyAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
    /// </summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\TestCase12")]
    public override void TestCase12_RetrieveMoreThenAvailable() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase12\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, true);
      this.RetrieveMoreThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a maximum 
    /// number of results.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase13")]
    public override void TestCase13_RetrieveMoreThenAvailable_EmptySource() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase13\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.RetrieveMoreThenAvailable_EmptySource(sourceInfo);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a specific ordering 
    /// specification.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_SortingFile.csv", @"CsvFileRepositoryTest\TestCase14")]
    public override void TestCase14_OrderBy() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase14\ReposTest_SortingFile.csv"), true, 3000, Encoding.UTF8, true);
      this.OrderBy(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a specific
    /// ordering specification.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase15")]
    public override void TestCase15_OrderBy_EmptySource() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase15\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.OrderBy_EmptySource(sourceInfo);
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase16")]
    public override void TestCase16_Add() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase16\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Add(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase17")]
    public override void TestCase17_Update() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase17\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Update(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase18")]
    public override void TestCase18_Delete() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase18\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.Delete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase19")]
    public override void TestCase19_AddMultiple_NormalUse() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase19\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase20")]
    public override void TestCase20_DeleteMultiple_NormalUse() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase20\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.DeleteMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase21")]
    public override void TestCase21_DeleteMultiple_Exceptions() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase21\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.DeleteMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase22")]
    public override void TestCase22_UpdateMultiple_NormalUse() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase22\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an 
    /// exception and rollback the operation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase23")]
    public override void TestCase23_UpdateMultiple_Exceptions() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase23\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateMultiple_Exceptions(sourceInfo);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase24")]
    public override void TestCase24_AddUpdate() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase24\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddUpdate(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase25")]
    public override void TestCase25_AddUpdateDelete() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase25\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.AddUpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase26")]
    public override void TestCase26_UpdateDelete() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase26\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.UpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase27")]
    public override void TestCase27_DeleteAdd() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase27\ReposTest_DataSourceFile.csv"), true, 3000, Encoding.UTF8, true);
      this.DeleteAdd(sourceInfo);
    }
    #endregion

    #region Read test-cases
    /// <summary>Tests the functionality of the <see cref="CsvFileRepository{T}.ReadAllRecordsFromFile(FileInfo,DataSourceInfo)"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\TestCase28")]
    public override void TestCase28_ReadFile() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase28\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.ReadFile(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="CsvFileRepository{T}.ReadAllRecordsFromFile(FileInfo,DataSourceInfo)"/> method when 
    /// reading an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase29")]
    public override void TestCase29_ReadEmptyFile() {
      /* Create the repositiry */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase29\ReposTest_EmptyInputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.ReadEmptyFile(sourceInfo);
    }
    #endregion

    #region Write test-cases
    /// <summary>Tests the functionality of the <see cref="CsvFileRepository{T}.WriteAllRecordsToFile(FileInfo,DataSourceInfo,IEnumerable{T})"/>
    /// method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\TestCase30")]
    [DeploymentItem(@"TestData\ReposTest_DataSourceFile.csv", @"CsvFileRepositoryTest\TestCase30")]
    public override void TestCase30_WriteFile() {
      /* Create the repository */
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase30\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.WriteFile(sourceInfo, @"CsvFileRepositoryTest\TestCase30\ReposTest_DataSourceFile.csv");
    }

    /// <summary>Tests the functionality of the <see cref="CsvFileRepository{T}.WriteAllRecordsToFile(FileInfo,DataSourceInfo,IEnumerable{T})"/> 
    /// method when writing an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\ReposTest_InputFile.csv", @"CsvFileRepositoryTest\TestCase31")]
    [DeploymentItem(@"TestData\ReposTest_EmptyInputFile.csv", @"CsvFileRepositoryTest\TestCase31")]
    public override void TestCase31_WriteEmptyFile() {
      DataSourceInfo sourceInfo =
        new FileSourceInfo(new FileInfo(@"CsvFileRepositoryTest\TestCase31\ReposTest_InputFile.csv"), true, 3000, Encoding.UTF8, true);
      this.WriteEmptyFile(sourceInfo, @"CsvFileRepositoryTest\TestCase31\ReposTest_EmptyInputFile.csv");
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
