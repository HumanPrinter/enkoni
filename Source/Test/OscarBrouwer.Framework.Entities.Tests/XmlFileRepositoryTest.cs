//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlFileRepositoryTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the XmlRepository class.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.IO;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace OscarBrouwer.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="XmlFileRepository{TEntity}"/> class.</summary>
  [TestClass]
  public class XmlFileRepositoryTest {
    #region Retrieve test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\TestCase01")]
    public void TestCase01_FindAll() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase01\ReposTest_InputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

      IEnumerable<TestDummy> results = repository.FindAll();

      Assert.IsNotNull(results);
      Assert.AreEqual(6, results.Count());
      for(int index = 0; index < 6; ++index) {
        Assert.AreEqual(index + 1, results.ElementAt(index).RecordId);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\TestCase02")]
    public void TestCase02_FindAll_EmptyFile() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase02\ReposTest_EmptyInputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

      IEnumerable<TestDummy> results = repository.FindAll();

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method 
    /// using the <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\TestCase03")]
    public void TestCase03_FindAllWithExpression() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase03\ReposTest_InputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method 
    /// using the <see cref="XmlFileRepository{TEntity}"/> implementation based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\TestCase04")]
    public void TestCase04_FindAllWithExpression_EmptyFile() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase04\ReposTest_EmptyInputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue < 3 || td.NumericValue > 5);
      spec = spec.And(Specification.Not((TestDummy td) => !td.BooleanValue));
      IEnumerable<TestDummy> results = repository.FindAll(spec);

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method 
    /// using the <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\TestCase05")]
    public void TestCase05_FindSingleWithExpression() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase05\ReposTest_InputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method 
    /// using the <see cref="XmlFileRepository{TEntity}"/> implementation based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\TestCase06")]
    public void TestCase06_FindSingleWithExpression_EmptyFile() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase06\ReposTest_EmptyInputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 7);
      TestDummy result = repository.FindSingle(spec);

      Assert.IsNull(result);
      
      TestDummy defaultDummy = new TestDummy { RecordId = -25 };
      result = repository.FindSingle(spec, defaultDummy);

      Assert.IsNotNull(result);
      Assert.AreSame(defaultDummy, result);
      Assert.AreEqual(-25, result.RecordId);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method 
    /// using the <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_InputFile.xml", @"XmlFileRepositoryTest\TestCase07")]
    public void TestCase07_FindFirstWithExpression() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase07\ReposTest_InputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method 
    /// using the <see cref="XmlFileRepository{TEntity}"/> implementation based on an empty file.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\TestCase08")]
    public void TestCase08_FindFirstWithExpression_EmptyFile() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase08\ReposTest_EmptyInputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\TestCase09")]
    public void TestCase09_RetrieveLessThenAvailable() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase09\ReposTest_SortingFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\TestCase10")]
    public void TestCase10_RetrieveExactlyAvailable() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase10\ReposTest_SortingFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\TestCase11")]
    public void TestCase11_RetrieveExactlyAvailable_EmptyFile() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase11\ReposTest_EmptyInputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(0);

      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\TestCase12")]
    public void TestCase12_RetrieveMoreThenAvailable() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase12\ReposTest_SortingFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\TestCase13")]
    public void TestCase13_RetrieveMoreThenAvailable_EmptyFile() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase13\ReposTest_EmptyInputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

      /* Set maximumresults on top-most specification */
      ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
      ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
      specB.SetMaximumResults(4);
      IEnumerable<TestDummy> results = repository.FindAll(specB);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_SortingFile.xml", @"XmlFileRepositoryTest\TestCase14")]
    public void TestCase14_OrderBy() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase14\ReposTest_SortingFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_EmptyInputFile.xml", @"XmlFileRepositoryTest\TestCase15")]
    public void TestCase15_OrderBy_EmptyFile() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase15\ReposTest_EmptyInputFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
    /// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\TestCase16")]
    public void TestCase16_Add() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase16\ReposTest_DataSourceFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNull(retrievedDummy);

      /* Add a new entity followed by saving changes and re-creating the repository */
      newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
      addedDummy = repository.AddEntity(newDummy);

      repository.SaveChanges();

      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowX", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\TestCase17")]
    public void TestCase17_Update() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase17\ReposTest_DataSourceFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
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

      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(originalDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\TestCase18")]
    public void TestCase18_Delete() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase18\ReposTest_DataSourceFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

      /* Delete an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 3));
      repository.DeleteEntity(originalDummy);

      TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNull(retrievedDummy);

      /* Re-create the repository and try to retrieve previous deleted entity */
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(originalDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("\"Row1\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(3, retrievedDummy.NumericValue);
      Assert.AreEqual(false, retrievedDummy.BooleanValue);

      /* Delete an entity followed by saving changes and re-creating the repository */
      repository.DeleteEntity(originalDummy);

      repository.SaveChanges();

      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue == false));

      Assert.IsNull(retrievedDummy);
    }
    #endregion

    #region Combined storage test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\TestCase19")]
    public void TestCase19_AddUpdate() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase19\ReposTest_DataSourceFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
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
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
      Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
      Assert.AreEqual(12, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\TestCase20")]
    public void TestCase20_AddUpdateDelete() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase20\ReposTest_DataSourceFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNull(retrievedDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\TestCase21")]
    public void TestCase21_UpdateDelete() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase21\ReposTest_DataSourceFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

      ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.BooleanValue);

      /* Retrieve and then update and delete an entity without saving changes or re-creating the repository */
      TestDummy originalDummy = repository.FindSingle(spec);
      originalDummy.TextValue = "RowX";
      TestDummy updatedDummy = repository.UpdateEntity(originalDummy);
      repository.DeleteEntity(updatedDummy);
      
      TestDummy retrievedDummy = repository.FindSingle(spec);
      
      Assert.IsNull(retrievedDummy);

      /* Re-create the repository and try to retrieve previous updated entity */
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
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
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNull(retrievedDummy);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}"/> when doing multiple storage-actions using the 
    /// <see cref="XmlFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    [DeploymentItem(@"Test\OscarBrouwer.Framework.Entities.Tests\TestData\ReposTest_DataSourceFile.xml", @"XmlFileRepositoryTest\TestCase22")]
    public void TestCase22_DeleteAdd() {
      DataSourceInfo sourceInfo = new FileSourceInfo(new FileInfo(@"XmlFileRepositoryTest\TestCase22\ReposTest_DataSourceFile.xml"), true, 3000);
      Repository<TestDummy> repository = new XmlFileRepository<TestDummy>(sourceInfo);

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
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
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
      repository = new XmlFileRepository<TestDummy>(sourceInfo);
      retrievedDummy = repository.FindSingle(spec);

      Assert.IsNotNull(retrievedDummy);
      Assert.IsNotNull(retrievedDummy);
      Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
      Assert.AreEqual(4, retrievedDummy.NumericValue);
      Assert.AreEqual(true, retrievedDummy.BooleanValue);
    }
    #endregion
  }
}
