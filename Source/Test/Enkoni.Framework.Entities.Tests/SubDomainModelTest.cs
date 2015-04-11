using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

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
      TestSubDomainModel testSubject = new TestSubDomainModel();

      TestDummy result = testSubject.CreateEmptyEntity();
      Assert.IsTrue(testSubject.CreateEmptyEntityCoreWasCalled);
    }
    #endregion

    #region FindEntities test cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(string[])"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntities_NullIncludePaths_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      IList<TestDummy> result = testSubject.FindEntities((string[])null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(string[])"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SubDomainModel_FindEntities_EmptyIncludePaths_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      IList<TestDummy> result = testSubject.FindEntities(new string[0]);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(string[])"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SubDomainModel_FindEntities_InvalidIncludePaths_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      IList<TestDummy> result = testSubject.FindEntities(new string[] { "", "PropA.PropB", "dummy" });
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntities_NullSpecification_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      IList<TestDummy> result = testSubject.FindEntities((ISpecification<TestDummy>)null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntities_NullExpression_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      IList<TestDummy> result = testSubject.FindEntities((Expression<Func<TestDummy, bool>>)null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities()"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntities_NoSpecificationNoIncludePaths_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      IList<TestDummy> result = testSubject.FindEntities();

      ISpecification<TestDummy> specificationResult = testSubject.FindEntitiesCoreParameter;
      Expression<Func<TestDummy, bool>> expressionResult = specificationResult.Visit(mockedRepository);
      Assert.IsTrue(testSubject.FindEntitiesCoreWasCalled);

      Assert.AreEqual(ExpressionType.Lambda, expressionResult.NodeType);
      Assert.AreEqual(typeof(bool), expressionResult.ReturnType);

      bool constantValue = (bool)((ConstantExpression)expressionResult.Body).Value;
      Assert.AreEqual(true, constantValue);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities()"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntities_NoSpecificationIncludePaths_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      IList<TestDummy> result = testSubject.FindEntities(new string[] { "PropA.PropB" });

      ISpecification<TestDummy> specificationResult = testSubject.FindEntitiesCoreParameter;
      CollectionAssert.AreEquivalent(new string[] { "PropA.PropB" }, specificationResult.IncludePaths.ToList());

      Expression<Func<TestDummy, bool>> expressionResult = specificationResult.Visit(mockedRepository);
      Assert.IsTrue(testSubject.FindEntitiesCoreWasCalled);

      Assert.AreEqual(ExpressionType.Lambda, expressionResult.NodeType);
      Assert.AreEqual(typeof(bool), expressionResult.ReturnType);

      bool constantValue = (bool)((ConstantExpression)expressionResult.Body).Value;
      Assert.AreEqual(true, constantValue);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntities_Specification_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();

      ISpecification<TestDummy> spec = Specification.Lambda<TestDummy>(td => td.RecordId > 1);
      IList<TestDummy> result = testSubject.FindEntities(spec);

      Assert.IsTrue(testSubject.FindEntitiesCoreWasCalled);
      Assert.AreSame(spec, testSubject.FindEntitiesCoreParameter);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntities(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntities_Expression_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
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
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      TestDummy result = testSubject.FindEntity((ISpecification<TestDummy>)null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntity_NullExpression_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      TestDummy result = testSubject.FindEntity((Expression<Func<TestDummy, bool>>)null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntity_Specification_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();

      ISpecification<TestDummy> spec = Specification.Lambda<TestDummy>(td => td.RecordId > 1);
      TestDummy result = testSubject.FindEntity(spec);

      Assert.IsTrue(testSubject.FindEntityCoreWasCalled);
      Assert.AreSame(spec, testSubject.FindEntityCoreParameter);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntity_Expression_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      TestDummy result = testSubject.FindEntity(td => td.RecordId > 1);

      Assert.IsTrue(testSubject.FindEntityCoreWasCalled);

      Expression<Func<TestDummy, bool>> expressionResult = testSubject.FindEntityCoreParameter.Visit(mockedRepository);
      Assert.AreEqual(ExpressionType.Lambda, expressionResult.NodeType);
      Assert.AreEqual(typeof(bool), expressionResult.ReturnType);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntity_ExpressionWithIncludePaths_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      TestDummy result = testSubject.FindEntity(td => td.RecordId > 1, new string[] { "PropA.PropB", "dummy" });

      Assert.IsTrue(testSubject.FindEntityCoreWasCalled);

      ISpecification<TestDummy> specificationResult = testSubject.FindEntityCoreParameter;
      Assert.AreEqual(2, specificationResult.IncludePaths.Count());
      CollectionAssert.AreEquivalent(new string[] { "dummy", "PropA.PropB" }, specificationResult.IncludePaths.ToList());

      Expression<Func<TestDummy, bool>> expressionResult = specificationResult.Visit(mockedRepository);
      Assert.AreEqual(ExpressionType.Lambda, expressionResult.NodeType);
      Assert.AreEqual(typeof(bool), expressionResult.ReturnType);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntity_ExpressionWithNullIncludePaths_ExceptionIsThrown() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      TestDummy result = testSubject.FindEntity(td => td.RecordId > 1, null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SubDomainModel_FindEntity_ExpressionWithEmptyIncludePaths_ExceptionIsThrown() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      TestDummy result = testSubject.FindEntity(td => td.RecordId > 1, new string[0]);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntity(ISpecification{T})"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SubDomainModel_FindEntity_ExpressionWithInvalidIncludePaths_ExceptionIsThrown() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
      Repository<TestDummy> mockedRepository = MockRepository.GeneratePartialMock<Repository<TestDummy>>();

      TestDummy result = testSubject.FindEntity(td => td.RecordId > 1, new string[] { "PropA.PropB", null, "dummy" });
    }
    #endregion

    #region FindEntityById test-cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntityById(int)"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_FindEntityById_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();
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

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntityById(int, string[])"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_FindEntityByIdWithNullIncludePaths_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();
      
      TestDummy result = testSubject.FindEntityById(42, null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntityById(int, string[])"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SubDomainModel_FindEntityByIdWithEmptyIncludePaths_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      TestDummy result = testSubject.FindEntityById(42, new string[0]);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.FindEntityById(int, string[])"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void SubDomainModel_FindEntityByIdWithInvalidIncludePaths_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      TestDummy result = testSubject.FindEntityById(42, new string[] { "PropA.PropB", null, "dummy" });
    }
    #endregion

    #region ValidateEntity test cases
    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.ValidateEntity(T)"/> method.</summary>
    [TestMethod]
    [ExpectedException(typeof(ArgumentNullException))]
    public void SubDomainModel_ValidateEntity_NullEntity_ExceptionIsThrown() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GenerateMock<SubDomainModel<TestDummy>>();

      ICollection<ValidationResult> result = testSubject.ValidateEntity(null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.ValidateEntity(T)"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_ValidateEntity_ValidEntity_NoValidationResult() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

      TestDummy entity = new TestDummy { TestNumber = 30 };
      ICollection<ValidationResult> result = testSubject.ValidateEntity(entity);

      Assert.AreEqual(0, result.Count);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.ValidateEntity(T)"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_ValidateEntity_InvalidEntity_ValidationResultIsReturned() {
      SubDomainModel<TestDummy> testSubject = MockRepository.GeneratePartialMock<SubDomainModel<TestDummy>>();

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
      SubDomainModel<TestDummy> testSubject = MockRepository.GenerateMock<SubDomainModel<TestDummy>>();

      TestDummy result = testSubject.AddEntity(null);
    }

    /// <summary>Tests the functionality of the <see cref="SubDomainModel{T}.AddEntity(T)"/> method.</summary>
    [TestMethod]
    public void SubDomainModel_AddEntity_TemplateMethodIsCalled() {
      TestSubDomainModel testSubject = new TestSubDomainModel();

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
      /// <summary>Initializes a new instance of the <see cref="TestSubDomainModel"/> class.</summary>
      public TestSubDomainModel() 
        :base() {
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

      /// <summary>Test implementation.</summary>
      /// <param name="entity">The entity.</param>
      /// <returns>No real value.</returns>
      protected override TestDummy UpdateEntityCore(TestDummy entity) {
 	      throw new System.NotImplementedException();
      }

      /// <summary>Test implementation.</summary>
      /// <param name="entity">The entity.</param>
      protected override void DeleteEntityCore(TestDummy entity) {
 	      throw new System.NotImplementedException();
      }
    }
    #endregion
  }
}
