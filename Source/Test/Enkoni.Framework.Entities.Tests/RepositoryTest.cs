using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Rhino.Mocks;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of a specific <see cref="Repository{TEntity}"/> class. By using this test class, any repository can be tested
  /// in a consistent way.</summary>
  [TestClass]
  public class RepositoryTest {
    #region Null-check test-cases
    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindAllWithNullSpecification_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      IEnumerable<TestDummy> result = repository.FindAll((ISpecification<TestDummy>)null);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindAllWithNullExpression_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      IEnumerable<TestDummy> result = repository.FindAll((Expression<Func<TestDummy, bool>>)null);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindSingleWithNullSpecification_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      TestDummy result = repository.FindSingle((ISpecification<TestDummy>)null);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindSingleWithNullExpression_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      TestDummy result = repository.FindSingle((Expression<Func<TestDummy, bool>>)null);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindSingleWithDefaultAndNullSpecification_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      TestDummy result = repository.FindSingle((ISpecification<TestDummy>)null, new TestDummy());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindSingleWithDefaultAndNullExpression_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      TestDummy result = repository.FindSingle((Expression<Func<TestDummy, bool>>)null, new TestDummy());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindFirstWithNullSpecification_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      TestDummy result = repository.FindFirst((ISpecification<TestDummy>)null);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindFirstWithNullExpression_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      TestDummy result = repository.FindFirst((Expression<Func<TestDummy, bool>>)null);
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindFirstWithDefaultAndNullSpecification_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      TestDummy result = repository.FindFirst((ISpecification<TestDummy>)null, new TestDummy());
    }

    /// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void Repository_FindFirstWithDefaultAndNullExpression_ExceptionIsThrown() {
      Repository<TestDummy> repository = MockRepository.GenerateMock<Repository<TestDummy>>();
      TestDummy result = repository.FindFirst((Expression<Func<TestDummy, bool>>)null, new TestDummy());
    }
    #endregion
  }
}
