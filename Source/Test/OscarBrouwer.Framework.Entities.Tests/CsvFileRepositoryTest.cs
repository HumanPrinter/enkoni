//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="CsvFileRepositoryTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of the CsvRepository class.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OscarBrouwer.Framework.Serialization;

namespace OscarBrouwer.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="CsvFileRepository{TEntity}"/> class.</summary>
  [TestClass]
  public class CsvFileRepositoryTest {
    /// <summary>Tests the functionality of the <see cref="Repository{TEntity}.FindAll()"/> method using the 
    /// <see cref="CsvFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public void TestCase1_FindAll() {
      CsvFileSourceInfo sourceInfo = new CsvFileSourceInfo(new FileInfo("ReposTest_InputFile.csv"), true, Encoding.UTF8);
      CsvFileRepository<TestDummy> repository = new CsvFileRepository<TestDummy>(sourceInfo);

      IEnumerable<TestDummy> results = repository.FindAll();

      Assert.IsNotNull(results);
      Assert.AreEqual(6, results.Count());
      for(int index = 0; index < 6; ++index) {
        Assert.AreEqual(index + 1, results.ElementAt(index).RecordId);
      }
    }

    /// <summary>Tests the functionality of the <see cref="Repository{TEntity}.FindAll()"/> method using the 
    /// <see cref="CsvFileRepository{TEntity}"/> implementation based on an empty file.</summary>
    [TestMethod]
    public void TestCase2_FindAll_EmptyFile() {
      CsvFileSourceInfo sourceInfo = new CsvFileSourceInfo(new FileInfo("ReposTest_EmptyInputFile.csv"), true, Encoding.UTF8);
      CsvFileRepository<TestDummy> repository = new CsvFileRepository<TestDummy>(sourceInfo);

      IEnumerable<TestDummy> results = repository.FindAll();

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{TEntity}.FindAll(ISpecification{T})"/> method 
    /// using the <see cref="CsvFileRepository{TEntity}"/> implementation.</summary>
    [TestMethod]
    public void TestCase3_FindAllWithExpression() {
      CsvFileSourceInfo sourceInfo = new CsvFileSourceInfo(new FileInfo("ReposTest_InputFile.csv"), true, Encoding.UTF8);
      CsvFileRepository<TestDummy> repository = new CsvFileRepository<TestDummy>(sourceInfo);

      Func<TestDummy, bool> query = td => td.NumericValue < 3 || td.NumericValue > 5;
      IEnumerable<TestDummy> results = repository.FindAll(Specification.Lambda(query));

      Assert.IsNotNull(results);
      Assert.AreEqual(3, results.Count());
      Assert.AreEqual(2, results.ElementAt(0).RecordId);
      Assert.AreEqual(3, results.ElementAt(1).RecordId);
      Assert.AreEqual(5, results.ElementAt(2).RecordId);

      query = td => td.NumericValue > 500;
      results = repository.FindAll(Specification.Lambda(query));

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{TEntity}.FindAll(ISpecification{T})"/> method 
    /// using the <see cref="CsvFileRepository{TEntity}"/> implementation based on an empty file.</summary>
    [TestMethod]
    public void TestCase4_FindAllWithExpression_EmptyFile() {
      CsvFileSourceInfo sourceInfo = new CsvFileSourceInfo(new FileInfo("ReposTest_EmptyInputFile.csv"), true, Encoding.UTF8);
      CsvFileRepository<TestDummy> repository = new CsvFileRepository<TestDummy>(sourceInfo);

      Func<TestDummy, bool> query = td => td.NumericValue < 3 || td.NumericValue > 5;
      IEnumerable<TestDummy> results = repository.FindAll(Specification.Lambda(query));

      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }

    #region Private helper classes
    /// <summary>A helper class to support the testcases.</summary>
    [CsvRecord(IgnoreHeaderOnRead = true, WriteHeader = true, CultureName = "en-US")]
    private class TestDummy : IEntity<TestDummy> {
      /// <summary>Gets or sets a unique record ID.</summary>
      public int RecordId { get; set; }

      /// <summary>Gets or sets a text value.</summary>
      [CsvColumn(0)]
      public string TextValue { get; set; }

      /// <summary>Gets or sets a numeric value.</summary>
      [CsvColumn(1)]
      public int NumericValue { get; set; }

      /// <summary>Gets or sets an optional boolean value.</summary>
      [CsvColumn(2)]
      public bool BooleanValue { get; set; }

      /// <summary>Copies the values from <paramref name="source"/> to this instance.</summary>
      /// <param name="source">The object that contains the correct values.</param>
      public void CopyFrom(TestDummy source) {
        this.RecordId = source.RecordId;
        this.TextValue = source.TextValue;
        this.NumericValue = source.NumericValue;
        this.BooleanValue = source.BooleanValue;
      }
    }
    #endregion
  }
}
