using System.Collections.Generic;
using System.IO;
using System.Linq;

using Enkoni.Framework.Tests;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="FileRepository{TEntity}"/> class. Since this class contains most of the functionality, it is 
  /// sufficient to test this class thoroughly and just a more global approach for the implementing classes (unless they contain non-standard 
  /// overloads. These tests use the <see cref="CsvFileRepository{TEntity}"/> for the concrete implementation since it relies more on custom code then the 
  /// <see cref="XmlFileRepository{TEntity}"/> which can only result in a more ridgid test of that same custom code..</summary>
  public abstract class FileRepositoryTest : RepositoryTest {
    #region Read test-cases
    /// <summary>Tests the functionality of the <see cref="FileRepository{T}.ReadAllRecordsFromFile(FileInfo,DataSourceInfo)"/> method.</summary>
    /// <param name="sourceInfo">The source info that is used to create the repository.</param>
    protected void ReadFile(DataSourceInfo sourceInfo) {
      /* Create the repository */
      Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

      /* Retrieve all entities. This forces the repository to read the file */
      IEnumerable<TestDummy> results = repository.FindAll();

      /* Check if the contents were read correctly */
      Assert.IsNotNull(results);
      Assert.AreEqual(6, results.Count());
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}.ReadAllRecordsFromFile(FileInfo,DataSourceInfo)"/> method when 
    /// reading an empty file.</summary>
    /// <param name="sourceInfo">The source info that is used to create the repository.</param>
    protected void ReadEmptyFile(DataSourceInfo sourceInfo) {
      /* Create the repository */
      Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

      /* Retrieve all entities. This forces the repository to read the file */
      IEnumerable<TestDummy> results = repository.FindAll();

      /* Check if the contents were read correctly */
      Assert.IsNotNull(results);
      Assert.AreEqual(0, results.Count());
    }
    #endregion

    #region Write test-cases
    /// <summary>Tests the functionality of the <see cref="FileRepository{T}.WriteAllRecordsToFile(FileInfo,DataSourceInfo,IEnumerable{T})"/>
    /// method.</summary>
    /// <param name="sourceInfo">The source info that is used to create the repository.</param>
    /// <param name="templatePath">The path to the file that serves as a template to check the output of the repository.</param>
    protected void WriteFile(DataSourceInfo sourceInfo, string templatePath) {
      /* Create the repository */
      Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

      /* Retrieve all entities. This forces the repository to read the file */
      IEnumerable<TestDummy> results = repository.FindAll();
      repository.DeleteEntities(results.Skip(2).Take(4));

      repository.SaveChanges();

      TestCaseHelper.CheckTestResult(templatePath, FileSourceInfo.SelectSourceFileInfo(sourceInfo).FullName);
    }

    /// <summary>Tests the functionality of the <see cref="FileRepository{T}.WriteAllRecordsToFile(FileInfo,DataSourceInfo,IEnumerable{T})"/> 
    /// method when writing an empty file.</summary>
    /// <param name="sourceInfo">The source info that is used to create the repository.</param>
    /// <param name="templatePath">The path to the file that serves as a template to check the output of the repository.</param>
    protected void WriteEmptyFile(DataSourceInfo sourceInfo, string templatePath) {
      Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

      /* Retrieve all entities. This forces the repository to read the file */
      IEnumerable<TestDummy> results = repository.FindAll();
      repository.DeleteEntities(results);

      repository.SaveChanges();

      TestCaseHelper.CheckTestResult(templatePath, FileSourceInfo.SelectSourceFileInfo(sourceInfo).FullName);
    }
    #endregion
  }
}
