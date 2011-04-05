//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DatabaseRepositoryTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
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
  public class DatabaseRepositoryTest {
    #region Retrieve test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase01")]
    public void TestCase01_FindAll() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase01");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Retrieve));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);
      
      IEnumerable<TestDummy> results = repository.FindAll();

      Assert.IsNotNull(results);
      Assert.AreEqual(6, results.Count());
      for(int index = 0; index < 6; ++index) {
        Assert.AreEqual(index + 1, results.ElementAt(index).RecordId);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase02")]
    public void TestCase02_FindAll_EmptyFile() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase02");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      IEnumerable<TestDummy> results = repository.FindAll();

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase03")]
    public void TestCase03_FindAllWithExpression() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase03");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Retrieve));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue < 3 || td.NumericValue > 5);
      spec = spec.And(Specification.Not((TestDummy td) => td.BooleanValue == false));
      IEnumerable<TestDummy> results = repository.FindAll(spec);
      
      Assert.IsNotNull(results);
      Assert.AreEqual(3, results.Count());
      Assert.AreEqual(3, results.ElementAt(0).RecordId);
      Assert.AreEqual(4, results.ElementAt(1).RecordId);
      Assert.AreEqual(6, results.ElementAt(2).RecordId);

      spec = Specification.Lambda((TestDummy td) => td.NumericValue > 500);
      results = repository.FindAll(spec);

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase04")]
    public void TestCase04_FindAllWithExpression_EmptyFile() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase04");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue < 3 || td.NumericValue > 5);
      spec = spec.And(Specification.Not((TestDummy td) => !td.BooleanValue));

      IEnumerable<TestDummy> results = repository.FindAll(spec);

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase05")]
    public void TestCase05_FindSingleWithExpression() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase05");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Retrieve));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      TestDummy result = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 7));

      Assert.IsNotNull(result);
      Assert.AreEqual(3, result.RecordId);
      
      result = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 500));

      Assert.IsNull(result);

      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 500), defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase06")]
    public void TestCase06_FindSingleWithExpression_EmptyFile() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase06");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      TestDummy result = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 7));

      Assert.IsNull(result);
      
      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 7), defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase07")]
    public void TestCase07_FindFirstWithExpression() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase07");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Retrieve));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      TestDummy result = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 3));

      Assert.IsNotNull(result);
      Assert.AreEqual(1, result.RecordId);

      result = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 500));
      
      Assert.IsNull(result);

      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 500), defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase08")]
    public void TestCase08_FindFirstWithExpression_EmptyFile() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase08");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      TestDummy result = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 7));

      Assert.IsNull(result);

      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 7), defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }
    #endregion

    #region Sorting test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase09")]
    public void TestCase09_RetrieveLessThenAvailable() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase09");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Sorting));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

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
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase10")]
    public void TestCase10_RetrieveExactlyAvailable() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase10");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Sorting));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

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
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase11")]
    public void TestCase11_RetrieveExactlyAvailable_EmptyFile() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase11");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(0);

      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase12")]
    public void TestCase12_RetrieveMoreThenAvailable() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase12");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Sorting));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

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
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase13")]
    public void TestCase13_RetrieveMoreThenAvailable_EmptyFile() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase13");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(4);
      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase14")]
    public void TestCase14_OrderBy() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase14");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Sorting));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

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
    /// <see cref="DatabaseRepository{TEntity}"/> implementation based on an empty database.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase15")]
    public void TestCase15_OrderBy_EmptyFile() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase15");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.RetrieveEmpty));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

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
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the <see cref="DatabaseRepository{TEntity}"/> 
    /// implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase16")]
    public void TestCase16_Add() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase16");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      /* Add a new entity without saving changes or re-creating the repository */
      TestDummy newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      TestDummy addedDummy = repository.AddEntity(newDummy);

      Assert.IsNotNull(addedDummy);
      Assert.IsTrue(addedDummy.RecordId == 0);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.TextValue == "RowX"));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowX", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Re-create the repository and try to retrieve previous added entity */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.TextValue == "RowX"));

      Assert.IsNull(retrievedDummy);

      /* Add a new entity followed by saving changes and re-creating the repository */
      newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);

      repository.SaveChanges();

      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.TextValue == "RowX"));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowX", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase17")]
    public void TestCase17_Update() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase17");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

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
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(originalDummy.RecordId, updatedDummy.RecordId);
      Assert.AreEqual("\"Row1\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);

      /* Update an entity followed by saving changes and re-creating the repository */
      retrievedDummy.TextValue = "RowY";
      updatedDummy = repository.UpdateEntity(originalDummy);

      repository.SaveChanges();

      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(originalDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase18")]
    public void TestCase18_Delete() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase18");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      /* Delete an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 3));
      repository.DeleteEntity(originalDummy);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNull(retrievedDummy);

      /* Re-create the repository and try to retrieve previous deleted entity */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(originalDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("\"Row1\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);

      /* Delete an entity followed by saving changes and re-creating the repository */
      repository.DeleteEntity(originalDummy);

      repository.SaveChanges();

      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue == false));

      Assert.IsNull(retrievedDummy);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase19")]
    public void TestCase19_AddUpdate() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase19");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      /* Add and then update an entity without saving changes or re-creating the repository */
      TestDummy newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      TestDummy addedDummy = repository.AddEntity(newDummy);
      addedDummy.TextValue = "RowY";
      TestDummy updatedDummy = repository.UpdateEntity(addedDummy);
      Assert.IsNotNull(updatedDummy);
      Assert.AreEqual(addedDummy.RecordId, updatedDummy.RecordId);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 12));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(updatedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Re-create the repository and try to retrieve previous updated entity */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 12));

      Assert.IsNull(retrievedDummy);

      /* Add and update an entity followed by saving changes and re-creating the repository */
      newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);
      addedDummy.TextValue = "RowY";
      updatedDummy = repository.UpdateEntity(addedDummy);

      repository.SaveChanges();

      /* First retrieve without re-creating the repository */
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 12));
      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Then retrieve after re-creating the repository */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 12));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase20")]
    public void TestCase20_AddUpdateDelete() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase20");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      /* Add and then update and then delete an entity without saving changes or re-creating the repository */
      TestDummy newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      TestDummy addedDummy = repository.AddEntity(newDummy);
      addedDummy.TextValue = "RowY";
      TestDummy updatedDummy = repository.UpdateEntity(addedDummy);
      repository.DeleteEntity(updatedDummy);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 12));

      Assert.IsNull(retrievedDummy);
      
      /* Add, update and delete an entity followed by saving changes and re-creating the repository */
      newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);
      addedDummy.TextValue = "RowY";
      updatedDummy = repository.UpdateEntity(addedDummy);
      repository.DeleteEntity(updatedDummy);

      repository.SaveChanges();

      /* First retrieve without re-creating the repository */
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 12));
      Assert.IsNull(retrievedDummy);
      
      /* Then retrieve after re-creating the repository */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.NumericValue == 12));

      Assert.IsNull(retrievedDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase21")]
    public void TestCase21_UpdateDelete() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase21");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      /* Retrieve and then update and delete an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));
      originalDummy.TextValue = "RowX";
      TestDummy updatedDummy = repository.UpdateEntity(originalDummy);
      repository.DeleteEntity(updatedDummy);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));
      
      Assert.IsNull(retrievedDummy);

      /* Re-create the repository and try to retrieve previous updated entity */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Update and delete an entity followed by saving changes and re-creating the repository */
      originalDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));
      originalDummy.TextValue = "RowX";
      updatedDummy = repository.UpdateEntity(originalDummy);
      repository.DeleteEntity(updatedDummy);

      repository.SaveChanges();

      /* First retrieve without re-creating the repository */
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));
      Assert.IsNull(retrievedDummy);
      
      /* Then retrieve after re-creating the repository */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));

      Assert.IsNull(retrievedDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="DatabaseRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\Enkoni.Framework.Entities.Tests\TestData\placeholder.txt", @"DatabaseRepositoryTest\TestCase22")]
    public void TestCase22_DeleteAdd() {
      string dbBasePath = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
      dbBasePath = Path.Combine(dbBasePath, @"DatabaseRepositoryTest\TestCase22");
      Database.DefaultConnectionFactory = new SqlCeConnectionFactory("System.Data.SqlServerCe.4.0", dbBasePath, "");
      Database.SetInitializer(new DatabaseRepositoryInitializer(TestCategory.Storage));

      DataSourceInfo sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      Repository<TestDummy> repository = new DatabaseRepository<TestDummy>(sourceInfo);

      /* Delete and then add an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));
      repository.DeleteEntity(originalDummy);

      TestDummy newDummy = new TestDummy { TextValue = "\"Row2\"", NumericValue = 4, BooleanValue = true };
      TestDummy addedDummy = repository.AddEntity(newDummy);

      Assert.IsNotNull(addedDummy);
      Assert.IsTrue(addedDummy.RecordId == 0);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(4, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Re-create the repository and try to retrieve previous updated entity */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Delete and add an entity followed by saving changes and re-creating the repository */
      originalDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));
      repository.DeleteEntity(originalDummy);
      newDummy = new TestDummy { TextValue = "\"Row2\"", NumericValue = 4, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);

      repository.SaveChanges();

      /* First retrieve without re-creating the repository */
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));
      Assert.IsNotNull(retrievedDummy);
      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(4, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);

      /* Then retrieve after re-creating the repository */
      sourceInfo = new DatabaseSourceInfo(new DatabaseRepositoryTestContext());
      repository = new DatabaseRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue));

      Assert.IsNotNull(retrievedDummy);
      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(4, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }
    #endregion
  }
}
