using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
using System.Linq;
using System.Reflection;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="DatabaseRepository{TEntity}"/> class.</summary>
  [TestClass]
  public class DatabaseRepositoryTest : RepositoryTest {
    #region Retrieve test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase01")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase01_FindAll() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase01\", TestCategory.Retrieve);
      this.FindAll(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase02")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase02_FindAll_EmptySource() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase02\", TestCategory.RetrieveEmpty);
      this.FindAll_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase03")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase03_FindAllWithExpression() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase03\", TestCategory.Retrieve);
      this.FindAllWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase04")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase04_FindAllWithExpression_EmptySource() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase04\", TestCategory.RetrieveEmpty);
      this.FindAllWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase05")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase05_FindSingleWithExpression() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase05\", TestCategory.Retrieve);
      this.FindSingleWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase06")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase06_FindSingleWithExpression_EmptySource() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase06\", TestCategory.RetrieveEmpty);
      this.FindSingleWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase07")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase07_FindFirstWithExpression() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase07\", TestCategory.Retrieve);
      this.FindFirstWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase08")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase08_FindFirstWithExpression_EmptySource() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase08\", TestCategory.RetrieveEmpty);
      this.FindFirstWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase09")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase09_RetrieveLessThenAvailable() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase09\", TestCategory.Sorting);
      this.RetrieveLessThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase10")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase10_RetrieveExactlyAvailable() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase10\", TestCategory.Sorting);
      this.RetrieveExactlyAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase11")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase11_RetrieveExactlyAvailable_EmptySource() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase11\", TestCategory.RetrieveEmpty);
      this.RetrieveExactlyAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase12")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase12_RetrieveMoreThenAvailable() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase12\", TestCategory.Sorting);
      this.RetrieveMoreThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase13")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase13_RetrieveMoreThenAvailable_EmptySource() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase13\", TestCategory.RetrieveEmpty);
      this.RetrieveMoreThenAvailable_EmptySource(sourceInfo);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase14")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase14_OrderBy() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase14\", TestCategory.Sorting);
      this.OrderBy(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase15")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase15_OrderBy_EmptySource() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase15\", TestCategory.RetrieveEmpty);
      this.OrderBy_EmptySource(sourceInfo);
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase16")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase16_Add() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase16\", TestCategory.Storage);
      this.Add(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase17")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase17_Update() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase17\", TestCategory.Storage);
      this.Update(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase18")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase18_Delete() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase18\", TestCategory.Storage);
      this.Delete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase19")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase19_AddMultiple_NormalUse() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase19\", TestCategory.Storage);
      this.AddMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase20")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase20_DeleteMultiple_NormalUse() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase20\", TestCategory.Storage);
      this.DeleteMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method when is should throw an exeption using 
    /// the <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase21")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase21_DeleteMultiple_Exceptions() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase21\", TestCategory.Storage);
      this.DeleteMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase22")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase22_UpdateMultiple_NormalUse() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase22\", TestCategory.Storage);
      this.UpdateMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an exception
    /// using the <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase23")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase23_UpdateMultiple_Exceptions() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase23\", TestCategory.Storage);
      this.UpdateMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase24")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase24_Add_Reset() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase24\", TestCategory.Storage);
      this.Add_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase25")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase25_Update_Reset() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase25\", TestCategory.Storage);
      this.Update_Reset(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Reset(DataSourceInfo)"/> method after unsaved additions to the repository.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase26")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase26_Delete_Reset() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase26\", TestCategory.Storage);
      this.Delete_Reset(sourceInfo);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase27")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase27_AddUpdate() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase27\", TestCategory.Storage);
      this.AddUpdate(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase28")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase28_AddUpdateDelete() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase28\", TestCategory.Storage);
      this.AddUpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase29")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase29_UpdateDelete() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase29\", TestCategory.Storage);
      this.UpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase30")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase30_DeleteAdd() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase30\", TestCategory.Storage);
      this.DeleteAdd(sourceInfo);
    }
    #endregion

    #region Execute test-case contracts
    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase31")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public override void TestCase31_ExecuteDefaultSpecification() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase31\", TestCategory.Storage);
      this.ExecuteDefaultSpecification(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.Execute(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase32")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    [ExpectedException(typeof(NotSupportedException))]
    public override void TestCase32_ExecuteBusinessRule() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase32\", TestCategory.Storage);
      this.ExecuteBusinessRule(sourceInfo);
    }
    #endregion

    #region Custom query test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when executing a business rule that retrieves a single result using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase33")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public void TestCase33_ExecuteBusinessRuleSingleResult() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase33\", TestCategory.Storage);
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
      ISpecification<TestDummy> selectSpec = Specification.BusinessRule<TestDummy>("TestCase31_CustomQuery", "Hit");
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
    [DeploymentItem(@"TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase34")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("EntityFramework.SqlServer.dll")]
    [DeploymentItem("EntityFramework.SqlServerCompact.dll")]
    public void TestCase34_ExecuteBusinessRuleMultipleResults() {
      DataSourceInfo sourceInfo = ConstructDataSourceInfo(@"DatabaseRepositoryTest\TestCase34\", TestCategory.Storage);
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
      ISpecification<TestDummy> selectSpec = Specification.BusinessRule<TestDummy>("TestCase32_CustomQuery", 3, 2);
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
    /// <returns>The constructed source info.</returns>
    private static DataSourceInfo ConstructDataSourceInfo(string databaseSubPath, TestCategory testCategory) {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, databaseSubPath, "Enkoni.Framework.Entities.Tests.DatabaseRepositoryTestContext.sdf");

      Database.SetInitializer(new DatabaseRepositoryInitializer(testCategory));

      DbContext context = new DatabaseRepositoryTestContext("Data Source=\"" + databaseBasePath + "\"");
      context.Database.Initialize(true);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      return sourceInfo;
    }
    #endregion
  }
}
