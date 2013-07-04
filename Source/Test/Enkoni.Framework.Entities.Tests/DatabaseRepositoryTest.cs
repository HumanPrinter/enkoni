//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseRepositoryTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the DatabaseRepository class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

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
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase01")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase01_FindAll() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase01");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Retrieve));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.FindAll(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase02")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase02_FindAll_EmptySource() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase02");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.FindAll_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase03")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase03_FindAllWithExpression() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase03");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Retrieve));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.FindAllWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase04")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase04_FindAllWithExpression_EmptySource() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase04");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.FindAllWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase05")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase05_FindSingleWithExpression() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase05");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Retrieve));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.FindSingleWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase06")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase06_FindSingleWithExpression_EmptySource() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase06");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.FindSingleWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase07")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase07_FindFirstWithExpression() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase07");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Retrieve));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.FindFirstWithExpression(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase08")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase08_FindFirstWithExpression_EmptySource() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase08");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.FindFirstWithExpression_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase09")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase09_RetrieveLessThenAvailable() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase09");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Sorting));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.RetrieveLessThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase10")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase10_RetrieveExactlyAvailable() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase10");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Sorting));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.RetrieveExactlyAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase11")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase11_RetrieveExactlyAvailable_EmptySource() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase11");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.RetrieveExactlyAvailable_EmptySource(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase12")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase12_RetrieveMoreThenAvailable() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase12");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Sorting));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.RetrieveMoreThenAvailable(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase13")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase13_RetrieveMoreThenAvailable_EmptySource() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase13");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.RetrieveMoreThenAvailable_EmptySource(sourceInfo);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase14")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase14_OrderBy() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase14");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Sorting));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.OrderBy(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase15")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase15_OrderBy_EmptySource() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase15");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.OrderBy_EmptySource(sourceInfo);
    }
    #endregion

    #region Storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase16")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase16_Add() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase16");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.Add(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase17")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase17_Update() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase17");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.Update(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase18")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase18_Delete() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase18");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.Delete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase19")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase19_AddMultiple_NormalUse() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase19");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.AddMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase20")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase20_DeleteMultiple_NormalUse() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase20");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.DeleteMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method when is should throw an exeption using 
    /// the <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase21")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase21_DeleteMultiple_Exceptions() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase21");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.DeleteMultiple_Exceptions(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase22")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase22_UpdateMultiple_NormalUse() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase22");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.UpdateMultiple_NormalUse(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an exception
    /// using the <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase23")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase23_UpdateMultiple_Exceptions() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase23");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.UpdateMultiple_Exceptions(sourceInfo);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase24")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase24_AddUpdate() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase24");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.AddUpdate(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase25")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase25_AddUpdateDelete() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase25");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.AddUpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase26")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase26_UpdateDelete() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase26");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.UpdateDelete(sourceInfo);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase27")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public override void TestCase27_DeleteAdd() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase27");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
      this.DeleteAdd(sourceInfo);
    }
    #endregion

    #region Custom query test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when executing a business rule that retrieves a single result using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase28")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public void TestCase28_ExecuteBusinessRuleSingleResult() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase28");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
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
      ISpecification<TestDummy> selectSpec = Specification.BusinessRule<TestDummy>("TestCase28_CustomQuery", "Hit");
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
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase29")]
    [DeploymentItem(@"amd64\", @"amd64\")]
    [DeploymentItem(@"x86\", @"x86\")]
    [DeploymentItem("System.Data.SqlServerCe.dll")]
    [DeploymentItem("System.Data.SqlServerCe.Entity.dll")]
    public void TestCase29_ExecuteBusinessRuleMultipleResults() {
      string databaseBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      databaseBasePath = Path.Combine(databaseBasePath, @"DatabaseRepositoryTest\TestCase29");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", databaseBasePath, string.Empty);
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DbContext context = new DatabaseRepositoryTestContext();
      context.Database.Initialize(false);

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(context, true);
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
      ISpecification<TestDummy> selectSpec = Specification.BusinessRule<TestDummy>("TestCase29_CustomQuery", 3, 2);
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
      DataSourceInfo newSourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext(), true);
      return new DatabaseRepository<T>(newSourceInfo);
    }
    #endregion
  }
}
