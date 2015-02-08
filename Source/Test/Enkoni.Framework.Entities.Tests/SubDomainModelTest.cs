using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}"/> class.</summary>
  [TestClass]
  public class SubDomainModelTest {
    #region CreateEmptyEntity test-cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.CreateEmptyEntity()"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_CreateEmptyEntity_TemplateMethodIsCalled() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      TestSubDomainModel testSubject = new TestSubDomainModel(parentModel);

      TestDummy result = testSubject.CreateEmptyEntity();
      Assert.IsTrue(testSubject.CreateEmptyEntityCoreWasCalled);
    }
    #endregion

    #region FindEntities test cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntities_NullSpecification_ExceptionIsThrown() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      SubDomainModel<TestDummy> testSubject = MockRepository.GenerateMock<SubDomainModel<TestDummy>>(parentModel);

      IList<TestDummy> result = testSubject.FindEntities((ISpecification<TestDummy>)null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntities_NullExpression_ExceptionIsThrown() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      SubDomainModel<TestDummy> testSubject = MockRepository.GenerateMock<SubDomainModel<TestDummy>>(parentModel);

      IList<TestDummy> result = testSubject.FindEntities((Expression<Func<TestDummy, bool>>)null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntities_Specification_TemplateMethodIsCalled() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      TestSubDomainModel testSubject = new TestSubDomainModel(parentModel);

      ISpecification<TestDummy> spec = Specification.Lambda<TestDummy>(td => td.RecordId > 1);
      IList<TestDummy> result = testSubject.FindEntities(spec);

      Assert.IsTrue(testSubject.FindEntitiesCoreWasCalled);
      Assert.AreSame(spec, testSubject.FindEntitiesCoreParameter);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntities_Expression_TemplateMethodIsCalled() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      TestSubDomainModel testSubject = new TestSubDomainModel(parentModel);
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      IList<TestDummy> result = testSubject.FindEntities(td => td.RecordId > 1);

      Assert.IsTrue(testSubject.FindEntitiesCoreWasCalled);

      Expression<Func<TestDummy, bool>> expressionResult = testSubject.FindEntitiesCoreParameter.Visit(mockedRepository);
      Assert.AreEqual(ExpressionType.Lambda, expressionResult.NodeType);
      Assert.AreEqual(typeof(bool), expressionResult.ReturnType);
    }
    #endregion

    #region FindEntity test cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntity_NullSpecification_ExceptionIsThrown() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      SubDomainModel<TestDummy> testSubject = MockRepository.GenerateMock<SubDomainModel<TestDummy>>(parentModel);

      TestDummy result = testSubject.FindEntity((ISpecification<TestDummy>)null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntity_NullExpression_ExceptionIsThrown() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      SubDomainModel<TestDummy> testSubject = MockRepository.GenerateMock<SubDomainModel<TestDummy>>(parentModel);

      TestDummy result = testSubject.FindEntity((Expression<Func<TestDummy, bool>>)null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntity_Specification_TemplateMethodIsCalled() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      TestSubDomainModel testSubject = new TestSubDomainModel(parentModel);

      ISpecification<TestDummy> spec = Specification.Lambda<TestDummy>(td => td.RecordId > 1);
      TestDummy result = testSubject.FindEntity(spec);

      Assert.IsTrue(testSubject.FindEntityCoreWasCalled);
      Assert.AreSame(spec, testSubject.FindEntityCoreParameter);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntity_Expression_TemplateMethodIsCalled() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      TestSubDomainModel testSubject = new TestSubDomainModel(parentModel);
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      TestDummy result = testSubject.FindEntity(td => td.RecordId > 1);

      Assert.IsTrue(testSubject.FindEntityCoreWasCalled);

      Expression<Func<TestDummy, bool>> expressionResult = testSubject.FindEntityCoreParameter.Visit(mockedRepository);
      Assert.AreEqual(ExpressionType.Lambda, expressionResult.NodeType);
      Assert.AreEqual(typeof(bool), expressionResult.ReturnType);
    }
    #endregion

    #region FindEntityById test-cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntityById(int)"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntityById_TemplateMethodIsCalled() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      TestSubDomainModel testSubject = new TestSubDomainModel(parentModel);
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      TestDummy result = testSubject.FindEntityById(42);

      Assert.IsTrue(testSubject.FindEntityCoreWasCalled);

      Expression<Func<TestDummy, bool>> expressionResult = testSubject.FindEntityCoreParameter.Visit(mockedRepository);
      Assert.AreEqual(ExpressionType.Lambda, expressionResult.NodeType);
      Assert.AreEqual(typeof(bool), expressionResult.ReturnType);

      object container = ((ConstantExpression)((MemberExpression)((BinaryExpression)expressionResult.Body).Right).Expression).Value;
      int constantValue = (int)((FieldInfo)((MemberExpression)((BinaryExpression)expressionResult.Body).Right).Member).GetValue(container);
      Assert.AreEqual(42, constantValue);
    }
    #endregion

    #region ValidateEntity test cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.ValidateEntity(T)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_ValidateEntity_NullEntity_ExceptionIsThrown() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      SubDomainModel<TestDummy> testSubject = MockRepository.GenerateMock<SubDomainModel<TestDummy>>(parentModel);

      ICollection<ValidationResult> result = testSubject.ValidateEntity(null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.ValidateEntity(T)"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_ValidateEntity_ValidEntity_NoValidationResult() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>(parentModel);

      TestDummy entity = new TestDummy { TestNumber = 30 };
      ICollection<ValidationResult> result = testSubject.ValidateEntity(entity);

      Assert.AreEqual(0, result.Count);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.ValidateEntity(T)"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_ValidateEntity_InvalidEntity_ValidationResultIsReturned() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>(parentModel);

      TestDummy entity = new TestDummy { TestNumber = 50 };
      ICollection<ValidationResult> result = testSubject.ValidateEntity(entity);

      Assert.AreEqual(1, result.Count);
    }
    #endregion

    #region AddEntity test cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_AddEntity_NullEntity_ExceptionIsThrown() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      SubDomainModel<TestDummy> testSubject = MockRepository.GenerateMock<SubDomainModel<TestDummy>>(parentModel);

      TestDummy result = testSubject.AddEntity(null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_AddEntity_TemplateMethodIsCalled() {
      DomainModel parentModel = MockRepository.GenerateMock<DomainModel>();
      TestSubDomainModel testSubject = new TestSubDomainModel(parentModel);

      TestDummy entity = new TestDummy { TestNumber = 30 };
      TestDummy result = testSubject.AddEntity(entity);

      Assert.IsTrue(testSubject.AddEntityCoreWasCalled);
      Assert.AreEqual(entity, testSubject.AddEntityCoreParameter);
    }
    #endregion

    #region Private helper classes
    /// <summary>A helper class to support the testcases.</summary>
    public class TestDummy : Entity<TestDummy> {
      /// <summary>A test property.</summary>
      [Range(12, 42)]
      public int TestNumber { get; set; }
    }

    /// <summary>A test implementation of the <see cref="SubDomainModel{T}"/>.</summary>
    public class TestSubDomainModel : SubDomainModel<TestDummy> {
      /// <summary>Initializes a new instance of the <see cref="TestDomainModel"/> class.</summary>
      /// <param name="parent">The parent model.</param>
      public TestSubDomainModel(DomainModel parent) 
        :base(parent) {
      }

      /// <summary>Gets a value indicating whether the CreateEmptyEntityCore-method was called.</summary>
      public bool CreateEmptyEntityCoreWasCalled { get; private set; }

      /// <summary>Gets a value indicating whether the FindEntitiesCore-method was called.</summary>
      public bool FindEntitiesCoreWasCalled { get; private set; }

      /// <summary>Gets the parameter that was passed to the FindEntitiesCore-method.</summary>
      public ISpecification<TestDummy> FindEntitiesCoreParameter { get; private set; }

      /// <summary>Gets a value indicating whether the FindEntityCore-method was called.</summary>
      public bool FindEntityCoreWasCalled { get; private set; }

      /// <summary>Gets the parameter that was passed to the FindEntityCore-method.</summary>
      public ISpecification<TestDummy> FindEntityCoreParameter { get; private set; }

      /// <summary>Gets a value indicating whether the AddEntityCore-method was called.</summary>
      public bool AddEntityCoreWasCalled { get; private set; }

      /// <summary>Gets the parameter that was passed to the AddEntityCore-method.</summary>
      public TestDummy AddEntityCoreParameter { get; private set; }

      /// <summary>Test implementation.</summary>
      /// <returns>No real value.</returns>
      protected override TestDummy CreateEmptyEntityCore() {
 	      this.CreateEmptyEntityCoreWasCalled = true;
        return null;
      }

      /// <summary>Test implementation.</summary>
      /// <param name="specification">The specification.</param>
      /// <returns>No real value.</returns>
      protected override IList<TestDummy> FindEntitiesCore(ISpecification<TestDummy> specification) {
        this.FindEntitiesCoreWasCalled = true;
        this.FindEntitiesCoreParameter = specification;
        return null;
      }

      /// <summary>Test implementation.</summary>
      /// <param name="specification">The specification.</param>
      /// <returns>No real value.</returns>
      protected override TestDummy FindEntityCore(ISpecification<TestDummy> specification) {
        this.FindEntityCoreWasCalled = true;
        this.FindEntityCoreParameter = specification;
        return null;
      }

      /// <summary>Test implementation.</summary>
      /// <param name="entity">The entity.</param>
      /// <returns>No real value.</returns>
      protected override TestDummy AddEntityCore(TestDummy entity) {
        this.AddEntityCoreWasCalled = true;
        this.AddEntityCoreParameter = entity;
        return null;
      }

      protected override TestDummy UpdateEntityCore(TestDummy entity) {
 	      throw new System.NotImplementedException();
      }

      protected override void DeleteEntityCore(TestDummy entity) {
 	      throw new System.NotImplementedException();
      }
    }
    #endregion
  }
}
