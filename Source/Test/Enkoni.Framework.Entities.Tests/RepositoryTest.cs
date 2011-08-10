//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="RepositoryTest.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Contains testcases that test the functionality of any Repository decendant.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Entities.Tests {
	/// <summary>Tests the functionality of a specific <see cref="Repository{TEntity}"/> class. By using this test class, any repository can be tested
	/// in a consistent way.</summary>
	public abstract class RepositoryTest {
		#region Retrieve test-case contracts
		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
		public abstract void TestCase01_FindAll();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method based on an empty file.</summary>
		public abstract void TestCase02_FindAll_EmptySource();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method.</summary>
		public abstract void TestCase03_FindAllWithExpression();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method based on an empty file.</summary>
		public abstract void TestCase04_FindAllWithExpression_EmptySource();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
		public abstract void TestCase05_FindSingleWithExpression();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method based on an empty file.</summary>
		public abstract void TestCase06_FindSingleWithExpression_EmptySource();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method .</summary>
		public abstract void TestCase07_FindFirstWithExpression();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method based on an empty file.</summary>
		public abstract void TestCase08_FindFirstWithExpression_EmptySource();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
		/// </summary>
		public abstract void TestCase09_RetrieveLessThenAvailable();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
		/// </summary>
		public abstract void TestCase10_RetrieveExactlyAvailable();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a maximum 
		/// number of results.</summary>
		public abstract void TestCase11_RetrieveExactlyAvailable_EmptySource();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
		/// </summary>
		public abstract void TestCase12_RetrieveMoreThenAvailable();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a maximum 
		/// number of results.</summary>
		public abstract void TestCase13_RetrieveMoreThenAvailable_EmptySource();
		#endregion

		#region Sorting test-case contracts
		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a specific ordering 
		/// specification.</summary>
		public abstract void TestCase14_OrderBy();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty file and a specific
		/// ordering specification.</summary>
		public abstract void TestCase15_OrderBy_EmptySource();
		#endregion

		#region Storage test-case contracts
		/// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
		public abstract void TestCase16_Add();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
		public abstract void TestCase17_Update();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method.</summary>
		public abstract void TestCase18_Delete();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
		public abstract void TestCase19_AddMultiple_NormalUse();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method.</summary>
		public abstract void TestCase20_DeleteMultiple_NormalUse();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method when it should throw an 
		/// exception and rollback the operation.</summary>
		public abstract void TestCase21_DeleteMultiple_Exceptions();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
		public abstract void TestCase22_UpdateMultiple_NormalUse();

		/// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an 
		/// exception and rollback the operation.</summary>
		public abstract void TestCase23_UpdateMultiple_Exceptions();
		#endregion

		#region Combined storage test-case contracts
		/// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
		public abstract void TestCase24_AddUpdate();

		/// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
		public abstract void TestCase25_AddUpdateDelete();

		/// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
		public abstract void TestCase26_UpdateDelete();

		/// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
		public abstract void TestCase27_DeleteAdd();
		#endregion

		#region Retrieve test-cases
		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void FindAll(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve all entities */
			IEnumerable<TestDummy> results = repository.FindAll();

			/* Check the reslts */
			Assert.IsNotNull(results);
			Assert.AreEqual(6, results.Count());
			for(int index = 0; index < 6; ++index) {
				Assert.AreEqual(index + 1, results.ElementAt(index).RecordId);
			}
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll()"/> method based on an empty data source.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void FindAll_EmptySource(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve all entities */
			IEnumerable<TestDummy> results = repository.FindAll();

			/* Check the reslts */
			Assert.IsNotNull(results);
			Assert.AreEqual(0, results.Count());
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void FindAllWithExpression(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Build a specification that will produce results */
			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue < 3 || td.NumericValue > 5);
			spec = spec.And(Specification.Not((TestDummy td) => td.BooleanValue == false));

			/* Retrieve the entities */
			IEnumerable<TestDummy> results = repository.FindAll(spec);

			/* Check the results */
			Assert.IsNotNull(results);
			Assert.AreEqual(3, results.Count());
			Assert.AreEqual(3, results.ElementAt(0).RecordId);
			Assert.AreEqual(4, results.ElementAt(1).RecordId);
			Assert.AreEqual(6, results.ElementAt(2).RecordId);

			/* Build a specification that will not produce results */
			spec = Specification.Lambda((TestDummy td) => td.NumericValue > 500);
			spec = spec.Or(Specification.Lambda((TestDummy td) => td.TextValue.Contains("7")));
			results = repository.FindAll(spec);

			/* Check the results */
			Assert.IsNotNull(results);
			Assert.AreEqual(0, results.Count());
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method based on an empty data source.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void FindAllWithExpression_EmptySource(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Build a specisifaction */
			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue < 3 || td.NumericValue > 5);
			spec = spec.And(Specification.Not((TestDummy td) => !td.BooleanValue));

			/* Retrieve the entities */
			IEnumerable<TestDummy> results = repository.FindAll(spec);

			/* Check the results */
			Assert.IsNotNull(results);
			Assert.AreEqual(0, results.Count());
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void FindSingleWithExpression(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Build a specification that will produce a result */
			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 7);

			/* Retrieve the entity */
			TestDummy result = repository.FindSingle(spec);

			/* Check the result */
			Assert.IsNotNull(result);
			Assert.AreEqual(3, result.RecordId);

			/* Build a specification that will not produce a result */
			spec = Specification.Lambda((TestDummy td) => td.NumericValue == 500);

			/* Retrieve the entity */
			result = repository.FindSingle(spec);

			/* Check the result */
			Assert.IsNull(result);

			/* Retrieve the entity (or a default) */
			TestDummy defaultDummy = new TestDummy { RecordId = -25 };
			result = repository.FindSingle(spec, defaultDummy);

			/* Check the result */
			Assert.IsNotNull(result);
			Assert.AreSame(defaultDummy, result);
			Assert.AreEqual(-25, result.RecordId);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindSingle(ISpecification{T})"/> method based on an empty data source.
		/// </summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void FindSingleWithExpression_EmptySource(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Build a specification */
			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 7);

			/* Retrieve the entity */
			TestDummy result = repository.FindSingle(spec);

			/* Check the results */
			Assert.IsNull(result);

			/* Retrieve the entity (or a default) */
			TestDummy defaultDummy = new TestDummy { RecordId = -25 };
			result = repository.FindSingle(spec, defaultDummy);

			/* Check the result */
			Assert.IsNotNull(result);
			Assert.AreSame(defaultDummy, result);
			Assert.AreEqual(-25, result.RecordId);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method .</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void FindFirstWithExpression(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Build a specification that will produce a result */
			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 3);

			/* Retrieve the entity */
			TestDummy result = repository.FindFirst(spec);

			/* Check the result */
			Assert.IsNotNull(result);
			Assert.AreEqual(1, result.RecordId);

			/* Build a specification that will not produce a result */
			spec = Specification.Lambda((TestDummy td) => td.NumericValue == 500);

			/* Retrieve the entity */
			result = repository.FindFirst(spec);

			/* Check the result */
			Assert.IsNull(result);

			/* Retrieve the entity (or a default) */
			TestDummy defaultDummy = new TestDummy { RecordId = -25 };
			result = repository.FindFirst(spec, defaultDummy);

			/* Check the result */
			Assert.IsNotNull(result);
			Assert.AreSame(defaultDummy, result);
			Assert.AreEqual(-25, result.RecordId);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindFirst(ISpecification{T})"/> method based on an empty data source.
		/// </summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void FindFirstWithExpression_EmptySource(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Build a specification */
			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.NumericValue == 7);

			/* Retrieve the entity */
			TestDummy result = repository.FindFirst(spec);

			/* Check the result */
			Assert.IsNull(result);

			/* Retrieve the entity (or a default) */
			TestDummy defaultDummy = new TestDummy { RecordId = -25 };
			result = repository.FindFirst(spec, defaultDummy);

			/* Check the result */
			Assert.IsNotNull(result);
			Assert.AreSame(defaultDummy, result);
			Assert.AreEqual(-25, result.RecordId);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
		/// </summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void RetrieveLessThenAvailable(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Set maximumresults on top-most specification */
			ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
			specB.SetMaximumResults(4);

			/* Retrieve the entities */
			IEnumerable<TestDummy> results = repository.FindAll(specB);

			/* Check the results */
			Assert.AreEqual(4, results.Count());
			Assert.AreEqual("aabcdef", results.First().TextValue, false);
			Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
			Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
			Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);

			/* Set maximumresults on bottom-most specification before combining */
			specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			specA.SetMaximumResults(4);
			specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));

			/* Retrieve the entities */
			results = repository.FindAll(specB);

			/* Check the results */
			Assert.AreEqual(4, results.Count());
			Assert.AreEqual("aabcdef", results.First().TextValue, false);
			Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
			Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
			Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);

			/* Set maximumresults on bottom-most specification after combining */
			specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
			specA.SetMaximumResults(4);

			/* Retrieve the entities */
			results = repository.FindAll(specB);

			/* Check the results */
			Assert.AreEqual(4, results.Count());
			Assert.AreEqual("aabcdef", results.First().TextValue, false);
			Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
			Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
			Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);

			/* Set maximumresults to zero */
			specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
			specA.SetMaximumResults(0);

			/* Retrieve the entities */
			results = repository.FindAll(specB);

			/* Check the results */
			Assert.AreEqual(0, results.Count());
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
		/// </summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void RetrieveExactlyAvailable(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Set maximumresults on top-most specification */
			ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
			specB.SetMaximumResults(6);

			/* Retrieve the results */
			IEnumerable<TestDummy> results = repository.FindAll(specB);

			/* Check the results */
			Assert.AreEqual(6, results.Count());
			Assert.AreEqual("aabcdef", results.First().TextValue, false);
			Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
			Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
			Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);
			Assert.AreEqual("acdefgh", results.Skip(4).First().TextValue, false);
			Assert.AreEqual("acfghij", results.Skip(5).First().TextValue, false);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty data source and a 
		/// maximum number of results.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void RetrieveExactlyAvailable_EmptySource(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Set maximumresults on top-most specification */
			ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
			specB.SetMaximumResults(0);

			/* Retrieve the entities */
			IEnumerable<TestDummy> results = repository.FindAll(specB);

			/* Check the results */
			Assert.AreEqual(0, results.Count());
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a maximum number of results.
		/// </summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void RetrieveMoreThenAvailable(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Set maximumresults on top-most specification */
			ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
			specB.SetMaximumResults(10);

			/* Retrieve the entities */
			IEnumerable<TestDummy> results = repository.FindAll(specB);

			/* Check the results */
			Assert.AreEqual(6, results.Count());
			Assert.AreEqual("aabcdef", results.First().TextValue, false);
			Assert.AreEqual("abcdefg", results.Skip(1).First().TextValue, false);
			Assert.AreEqual("aadefgh", results.Skip(2).First().TextValue, false);
			Assert.AreEqual("abefghi", results.Skip(3).First().TextValue, false);
			Assert.AreEqual("acdefgh", results.Skip(4).First().TextValue, false);
			Assert.AreEqual("acfghij", results.Skip(5).First().TextValue, false);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty data source and a 
		/// maximum number of results.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void RetrieveMoreThenAvailable_EmptySource(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Set maximumresults on top-most specification */
			ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			ISpecification<TestDummy> specB = specA.And(Specification.Lambda((TestDummy td) => td.TextValue.Length == 7));
			specB.SetMaximumResults(4);

			/* Retrieve the entities */
			IEnumerable<TestDummy> results = repository.FindAll(specB);

			/* Check the results */
			Assert.AreEqual(0, results.Count());
		}
		#endregion

		#region Sorting test-cases
		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using a specific ordering 
		/// specification.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void OrderBy(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Build the specification using ascending sorting */
			ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			specA.OrderBy((TestDummy td) => td.NumericValue, SortOrder.Ascending);

			/* Retrieve the entities */
			IEnumerable<TestDummy> results = repository.FindAll(specA);

			/* Check the results */
			Assert.AreEqual(6, results.Count());
			Assert.AreEqual("acfghij", results.First().TextValue, false);
			Assert.AreEqual("abefghi", results.Skip(1).First().TextValue, false);
			Assert.AreEqual("aabcdef", results.Skip(2).First().TextValue, false);
			Assert.AreEqual("abcdefg", results.Skip(3).First().TextValue, false);
			Assert.AreEqual("acdefgh", results.Skip(4).First().TextValue, false);
			Assert.AreEqual("aadefgh", results.Skip(5).First().TextValue, false);

			/* Build the specification using descending sorting */
			specA.OrderBy((TestDummy td) => td.TextValue, SortOrder.Descending);

			/* Retrieve the entities */
			results = repository.FindAll(specA);

			/* Check the results */
			Assert.AreEqual(6, results.Count());
			Assert.AreEqual("acfghij", results.First().TextValue, false);
			Assert.AreEqual("abefghi", results.Skip(1).First().TextValue, false);
			Assert.AreEqual("abcdefg", results.Skip(2).First().TextValue, false);
			Assert.AreEqual("aabcdef", results.Skip(3).First().TextValue, false);
			Assert.AreEqual("acdefgh", results.Skip(4).First().TextValue, false);
			Assert.AreEqual("aadefgh", results.Skip(5).First().TextValue, false);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.FindAll(ISpecification{T})"/> method using an empty data source and a 
		/// specific ordering specification.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void OrderBy_EmptySource(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Build the specification using ascending sorting */
			ISpecification<TestDummy> specA = Specification.Lambda((TestDummy td) => td.TextValue.StartsWith("a"));
			specA.OrderBy((TestDummy td) => td.NumericValue, SortOrder.Ascending);

			/* Retrieve the entities */
			IEnumerable<TestDummy> results = repository.FindAll(specA);

			/* Check the results */
			Assert.AreEqual(0, results.Count());

			/* Build the specification using descending sorting */
			specA.OrderBy((TestDummy td) => td.TextValue, SortOrder.Descending);

			/* Retrieve the entities */
			results = repository.FindAll(specA);

			/* Check the results */
			Assert.AreEqual(0, results.Count());
		}
		#endregion

		#region Storage test-cases
		/// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntity(T)"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void Add(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Add a new entity without saving changes or re-creating the repository */
			TestDummy newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
			TestDummy addedDummy = repository.AddEntity(newDummy);

			/* Check the result */
			Assert.IsNotNull(addedDummy);
			Assert.IsTrue(addedDummy.RecordId <= 0);

			/* Retrieve the unsaved entity */
			Func<TestDummy, string> field = td => td.TextValue;
			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.TextValue == "RowX");
			TestDummy retrievedDummy = repository.FindSingle(spec);

			/* Check the result */
			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
			Assert.AreEqual("RowX", retrievedDummy.TextValue, false);
			Assert.AreEqual(12, retrievedDummy.NumericValue);
			Assert.AreEqual(true, retrievedDummy.BooleanValue);

			/* Re-create the repository and try to retrieve previous added entity */
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			retrievedDummy = repository.FindSingle(spec);

			/* Check the result */
			Assert.IsNull(retrievedDummy);

			/* Add a new entity followed by saving changes and re-creating the repository */
			newDummy = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
			addedDummy = repository.AddEntity(newDummy);

			repository.SaveChanges();

			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve the entity */
			retrievedDummy = repository.FindSingle(spec);

			/* Check the result */
			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
			Assert.AreEqual("RowX", retrievedDummy.TextValue, false);
			Assert.AreEqual(12, retrievedDummy.NumericValue);
			Assert.AreEqual(true, retrievedDummy.BooleanValue);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntity(T)"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void Update(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Update an entity without saving changes or re-creating the repository */
			TestDummy originalDummy = repository.FindFirst(Specification.Lambda((TestDummy td) => td.TextValue == "\"Row1\""));
			originalDummy.TextValue = "RowY";
			TestDummy updatedDummy = repository.UpdateEntity(originalDummy);

			/* Check the result */
			Assert.IsNotNull(updatedDummy);
			Assert.IsTrue(updatedDummy.RecordId > 0);

			/* Retrieve the updated entity */
			TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

			/* Check the result */
			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual(updatedDummy.RecordId, updatedDummy.RecordId);
			Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
			Assert.AreEqual(3, retrievedDummy.NumericValue);
			Assert.AreEqual(false, retrievedDummy.BooleanValue);

			/* Re-create the repository and try to retrieve previous updated entity */
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

			/* Check the result */
			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual(originalDummy.RecordId, updatedDummy.RecordId);
			Assert.AreEqual("\"Row1\"", retrievedDummy.TextValue, false);
			Assert.AreEqual(3, retrievedDummy.NumericValue);
			Assert.AreEqual(false, retrievedDummy.BooleanValue);

			/* Update an entity followed by saving changes and re-creating the repository */
			originalDummy.TextValue = "RowY";
			updatedDummy = repository.UpdateEntity(originalDummy);

			repository.SaveChanges();

			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve the entity */
			retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

			/* Check the result */
			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual(originalDummy.RecordId, retrievedDummy.RecordId);
			Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
			Assert.AreEqual(3, retrievedDummy.NumericValue);
			Assert.AreEqual(false, retrievedDummy.BooleanValue);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntity(T)"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void Delete(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Delete an entity without saving changes or re-creating the repository */
			TestDummy originalDummy = repository.FindFirst(Specification.Lambda((TestDummy td) => td.NumericValue == 3));
			repository.DeleteEntity(originalDummy);

			/* Retrieve the deleted entity */
			TestDummy retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

			/* Check the result */
			Assert.IsNull(retrievedDummy);

			/* Re-create the repository and try to retrieve previous deleted entity */
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 1));

			/* Check the result */
			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual(originalDummy.RecordId, retrievedDummy.RecordId);
			Assert.AreEqual("\"Row1\"", retrievedDummy.TextValue, false);
			Assert.AreEqual(3, retrievedDummy.NumericValue);
			Assert.AreEqual(false, retrievedDummy.BooleanValue);

			/* Delete an entity followed by saving changes and re-creating the repository */
			repository.DeleteEntity(originalDummy);

			repository.SaveChanges();

			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve the entity */
			retrievedDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.BooleanValue == false));

			/* Check the result */
			Assert.IsNull(retrievedDummy);
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.AddEntities(IEnumerable{T})"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void AddMultiple_NormalUse(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/*************************************************************************************************/
			/* SCENARIO 1: Add three completely new entities to the repository                               */
			/*************************************************************************************************/
			/* Create three new entities */
			TestDummy newDummyA = new TestDummy { TextValue = "RowX", NumericValue = 12, BooleanValue = true };
			TestDummy newDummyB = new TestDummy { TextValue = "RowY", NumericValue = 13, BooleanValue = true };
			TestDummy newDummyC = new TestDummy { TextValue = "RowZ", NumericValue = 14, BooleanValue = true };
			TestDummy[] newDummies = new TestDummy[] { newDummyA, newDummyB, newDummyC };

			/* Add the entities */
			IEnumerable<TestDummy> addedDummies = repository.AddEntities(newDummies);

			/* Check the results */
			Assert.IsNotNull(addedDummies);
			Assert.AreEqual(3, addedDummies.Count());
			Assert.IsTrue(addedDummies.All(td => td.RecordId <= 0));

			/* Retrieve all the entities */
			IEnumerable<TestDummy> retrievedDummies = repository.FindAll();

			/* Check the results */
			Assert.AreEqual(5, retrievedDummies.Count());
			Assert.AreEqual(2, retrievedDummies.Where(td => td.RecordId > 0).Count());
			Assert.AreEqual(3, retrievedDummies.Where(td => td.RecordId <= 0).Count());

			/* Save the changes/additions and recreate the repository */
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve all the entities */
			retrievedDummies = repository.FindAll();

			/* Check the results */
			Assert.AreEqual(5, retrievedDummies.Count());
			Assert.IsTrue(retrievedDummies.All(td => td.RecordId > 0));

			/****************************************************************************************************/
			/* SCENARIO 2: Add three ne entities to the repository of which one was deleted (but not yet saved) */
			/****************************************************************************************************/
			/* Retrieve and delete an entity */
			TestDummy obsoleteDummy = repository.FindSingle(Specification.Lambda((TestDummy td) => td.RecordId == 3));
			repository.DeleteEntity(obsoleteDummy);

			/* Create two new entites */
			newDummyA = new TestDummy { TextValue = "RowV", NumericValue = 15, BooleanValue = true };
			newDummyB = new TestDummy { TextValue = "RowW", NumericValue = 16, BooleanValue = true };
			newDummies = new TestDummy[] { newDummyA, obsoleteDummy, newDummyB };

			/* Add the dummies (including the deleted one). The deletion shouldbe made undone */
			addedDummies = repository.AddEntities(newDummies);

			/* Check the results */
			Assert.IsNotNull(addedDummies);
			Assert.AreEqual(3, addedDummies.Count());
			Assert.AreEqual(2, addedDummies.Where(td => td.RecordId <= 0).Count());
			Assert.AreEqual(1, addedDummies.Where(td => td.RecordId > 0).Count());

			/* Retrieve all the entities */
			retrievedDummies = repository.FindAll();

			/* Check the results */
			Assert.AreEqual(7, retrievedDummies.Count());
			Assert.AreEqual(5, retrievedDummies.Where(td => td.RecordId > 0).Count());
			Assert.AreEqual(2, retrievedDummies.Where(td => td.RecordId <= 0).Count());

			/* Save the changes/additions and recreate the repository */
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve all the entities */
			retrievedDummies = repository.FindAll();

			/* Check the results */
			Assert.AreEqual(7, retrievedDummies.Count());
			Assert.IsTrue(retrievedDummies.All(td => td.RecordId > 0));
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void DeleteMultiple_NormalUse(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Add some additional entities */
			TestDummy newDummyA = new TestDummy { TextValue = "Row3", NumericValue = 3, BooleanValue = true };
			TestDummy newDummyB = new TestDummy { TextValue = "Row4", NumericValue = 3, BooleanValue = true };
			TestDummy newDummyC = new TestDummy { TextValue = "Row5", NumericValue = 3, BooleanValue = true };
			TestDummy[] newDummies = { newDummyA, newDummyB, newDummyC };
			repository.AddEntities(newDummies);
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			/* The repository now contains 5 entities */

			/*************************************************************************************************/
			/* SCENARIO 1: Remove three existing entities                                                    */
			/*************************************************************************************************/
			/* Retrieve all the entities and select the entities that will be deleted */
			IEnumerable<TestDummy> retrievedDummies = repository.FindAll();
			TestDummy obsoleteDummyA = retrievedDummies.ElementAt(3);
			TestDummy obsoleteDummyB = retrievedDummies.ElementAt(4);
			TestDummy obsoleteDummyC = retrievedDummies.ElementAt(1);
			TestDummy[] obsoleteDummies = { obsoleteDummyA, obsoleteDummyB, obsoleteDummyC };

			/* Delete the entities */
			repository.DeleteEntities(obsoleteDummies);

			/* Retrieve the remaining entities (5 - 3 = 2) */
			retrievedDummies = repository.FindAll();

			/* Check the results */
			Assert.AreEqual(2, retrievedDummies.Count());
			Assert.IsTrue(retrievedDummies.All(td => td.RecordId > 0));

			/* Save the changes and recreate the repository */
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve all the entities */
			retrievedDummies = repository.FindAll();

			/* Check the results */
			Assert.AreEqual(2, retrievedDummies.Count());
			Assert.IsTrue(retrievedDummies.All(td => td.RecordId > 0));

			/* Reset the testcase by adding the removed entities again */
			repository.AddEntities(obsoleteDummies);
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/*************************************************************************************************/
			/* SCENARIO 2: Remove three entities of which one is added (but not yet saved)                   */
			/*************************************************************************************************/
			/* Retrieve all the entities and add a new one */
			retrievedDummies = repository.FindAll();
			TestDummy newDummyD = new TestDummy { TextValue = "RowX", NumericValue = 4, BooleanValue = true };
			repository.AddEntity(newDummyD);

			/* Delete the three entities (including the new entity) */
			obsoleteDummies = new TestDummy[] { retrievedDummies.ElementAt(2), newDummyD, retrievedDummies.ElementAt(3) };
			repository.DeleteEntities(obsoleteDummies);

			/* Retrieve the entities (5 + 1 - 3 = 3 */
			retrievedDummies = repository.FindAll();

			/* Check the results */
			Assert.AreEqual(3, retrievedDummies.Count());
			Assert.IsTrue(retrievedDummies.All(td => td.RecordId > 0));

			/* Save the changes and recreate the repository */
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve all the entities */
			retrievedDummies = repository.FindAll();

			/* Check the results */
			Assert.AreEqual(3, retrievedDummies.Count());
			Assert.IsTrue(retrievedDummies.All(td => td.RecordId > 0));
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.DeleteEntities(IEnumerable{T})"/> method when it should throw an 
		/// exception and rollback the operation.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void DeleteMultiple_Exceptions(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Add some additional entities */
			TestDummy newDummyA = new TestDummy { TextValue = "Row3", NumericValue = 3, BooleanValue = true };
			TestDummy newDummyB = new TestDummy { TextValue = "Row4", NumericValue = 3, BooleanValue = true };
			TestDummy newDummyC = new TestDummy { TextValue = "Row5", NumericValue = 3, BooleanValue = true };
			TestDummy[] newDummies = { newDummyA, newDummyB, newDummyC };
			repository.AddEntities(newDummies);
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			/* The repository now contains 5 entities */

			/*************************************************************************************************/
			/* SCENARIO 1: Remove four existing entities of which one is already deleted                     */
			/*************************************************************************************************/
			/* Retrieve all the entities and select the entities that will be deleted */
			IEnumerable<TestDummy> retrievedDummies = repository.FindAll();
			TestDummy obsoleteDummyA = retrievedDummies.ElementAt(3);
			TestDummy obsoleteDummyB = retrievedDummies.ElementAt(4);
			TestDummy obsoleteDummyC = retrievedDummies.ElementAt(1);

			/* Delete a single entity */
			repository.DeleteEntity(obsoleteDummyA);
			/* The repository now contains 4 entities */

			/* Delete three entities of which one is already deleted */
			TestDummy[] obsoleteDummies = { obsoleteDummyA, obsoleteDummyB, obsoleteDummyC };

			try {
				repository.DeleteEntities(obsoleteDummies);
				/* The operation is expected to fail */
				Assert.Fail("'DeleteEntities' did not throw the expected InvalidOperationException");
			}
			catch(InvalidOperationException) {
				/* Retrieve all the entities */
				retrievedDummies = repository.FindAll();

				/* Check the results */
				Assert.AreEqual(4, retrievedDummies.Count());
			}

			/* Reset the testcase by adding the removed entity again */
			repository.AddEntity(obsoleteDummyA);
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			retrievedDummies = repository.FindAll();
			/* The repository now contains 5 entities */

			/*************************************************************************************************/
			/* SCENARIO 2: Delete three entities of which one has not yet been added                         */
			/*************************************************************************************************/
			/* Create a new entity */
			TestDummy newDummyD = new TestDummy { TextValue = "RowY", NumericValue = 5, BooleanValue = true, RecordId = -1 };

			/* Remove the entities (including a completely new entity */
			obsoleteDummies = new TestDummy[] { retrievedDummies.ElementAt(2), newDummyD, retrievedDummies.ElementAt(4) };
			try {
				repository.DeleteEntities(obsoleteDummies);
				/* The operation is expected to fail */
				Assert.Fail("'DeleteEntities' did not throw the expected InvalidOperationException");
			}
			catch(InvalidOperationException) {
				/* Retrieve all the entities */
				retrievedDummies = repository.FindAll();

				/* Check the results */
				Assert.AreEqual(5, retrievedDummies.Count());
			}
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void UpdateMultiple_NormalUse(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);
			/* Retrieve all the entities to force the repository to read the sourcefile */

			/* Add some additional entities */
			TestDummy newDummyA = new TestDummy { TextValue = "Row3", NumericValue = 3, BooleanValue = true };
			TestDummy newDummyB = new TestDummy { TextValue = "Row4", NumericValue = 3, BooleanValue = true };
			TestDummy newDummyC = new TestDummy { TextValue = "Row5", NumericValue = 3, BooleanValue = true };
			TestDummy[] newDummies = new TestDummy[] { newDummyA, newDummyB, newDummyC };
			repository.AddEntities(newDummies);
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			/* The repository now contains 5 entities */

			/*************************************************************************************************/
			/* SCENARIO 1: Update three entities that are already in the repository                          */
			/*************************************************************************************************/
			/* Retrieve and change three entities */
			IEnumerable<TestDummy> retrievedDummies = repository.FindAll();
			TestDummy updateDummyA = retrievedDummies.ElementAt(3);
			TestDummy updateDummyB = retrievedDummies.ElementAt(4);
			TestDummy updateDummyC = retrievedDummies.ElementAt(1);
			updateDummyA.TextValue = "RowX";
			updateDummyB.TextValue = "RowY";
			updateDummyC.TextValue = "RowZ";
			TestDummy[] updateDummies = new TestDummy[] { updateDummyA, updateDummyB, updateDummyC };

			/* Update the entities */
			IEnumerable<TestDummy> updatedDummies = repository.UpdateEntities(updateDummies);

			/* Check: Make sure the returned value is not null */
			Assert.IsNotNull(updatedDummies);
			/* Check: Make sure the returned value contains three items */
			Assert.AreEqual(3, updatedDummies.Count());
			/* Check: Make sure the returned values are the same as the updated entities */
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == updateDummyA.RecordId));
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == updateDummyB.RecordId));
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == updateDummyC.RecordId));
			Assert.AreEqual("RowX", updatedDummies.Single(td => td.RecordId == updateDummyA.RecordId).TextValue, false);
			Assert.AreEqual("RowY", updatedDummies.Single(td => td.RecordId == updateDummyB.RecordId).TextValue, false);
			Assert.AreEqual("RowZ", updatedDummies.Single(td => td.RecordId == updateDummyC.RecordId).TextValue, false);

			/* Save the changes and recreate the repository */
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve all the entities */
			retrievedDummies = repository.FindAll();

			/* Check: Make sure the updates were saved correctly */
			Assert.AreEqual(5, retrievedDummies.Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowX", StringComparison.Ordinal)).Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowY", StringComparison.Ordinal)).Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowZ", StringComparison.Ordinal)).Count());

			/*************************************************************************************************/
			/* SCENARIO 2: Update three entities of which one was already updated                            */
			/*************************************************************************************************/
			/* Retrieve and change three entities */
			updateDummyA = retrievedDummies.ElementAt(3);
			updateDummyB = retrievedDummies.ElementAt(4);
			updateDummyC = retrievedDummies.ElementAt(1);
			updateDummyA.TextValue = "RowT";
			updateDummyB.TextValue = "RowV";
			updateDummyC.TextValue = "RowW";

			/* Make the first (single) update */
			updateDummyA = repository.UpdateEntity(updateDummyA);

			/* Make another change to the entity */
			updateDummyA.TextValue = "RowU";
			updateDummies = new TestDummy[] { updateDummyA, updateDummyB, updateDummyC };

			/* Update the entities */
			updatedDummies = repository.UpdateEntities(updateDummies);

			/* Check: Make sure the returned value is not null */
			Assert.IsNotNull(updatedDummies);
			/* Check: Make sure the returned value contains three items */
			Assert.AreEqual(3, updatedDummies.Count());
			/* Check: Make sure the returned values are the same as the updated entities */
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == updateDummyA.RecordId));
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == updateDummyB.RecordId));
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == updateDummyC.RecordId));
			Assert.IsNull(updatedDummies.SingleOrDefault(td => td.TextValue.Equals("RowT", StringComparison.Ordinal)));
			Assert.AreEqual("RowU", updatedDummies.Single(td => td.RecordId == updateDummyA.RecordId).TextValue, false);
			Assert.AreEqual("RowV", updatedDummies.Single(td => td.RecordId == updateDummyB.RecordId).TextValue, false);
			Assert.AreEqual("RowW", updatedDummies.Single(td => td.RecordId == updateDummyC.RecordId).TextValue, false);

			/* Save the changes and recreate the repository */
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Retrieve all the entities */
			retrievedDummies = repository.FindAll();

			/* Check: Make sure the updates were saved correctly */
			Assert.AreEqual(5, retrievedDummies.Count());
			Assert.AreEqual(0, retrievedDummies.Where(td => td.TextValue.Equals("RowT", StringComparison.Ordinal)).Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowU", StringComparison.Ordinal)).Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowV", StringComparison.Ordinal)).Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowW", StringComparison.Ordinal)).Count());

			/*************************************************************************************************/
			/* SCENARIO 3: Update three entities of which one was added but not yet saved                    */
			/*************************************************************************************************/
			/* Retrieve and change three entities */
			updateDummyA = retrievedDummies.ElementAt(3);
			updateDummyB = retrievedDummies.ElementAt(2);
			TestDummy newDummyD = new TestDummy { TextValue = "RowM", NumericValue = 2, BooleanValue = true };
			updateDummyA.TextValue = "RowB";
			updateDummyB.TextValue = "RowC";

			newDummyD = repository.AddEntity(newDummyD);
			/* The repository now contains 6 entities */

			newDummyD.TextValue = "RowN";
			updateDummies = new TestDummy[] { updateDummyA, newDummyD, updateDummyB };

			/* Update the entities */
			updatedDummies = repository.UpdateEntities(updateDummies);

			/* Check: Make sure the returned value is not null */
			Assert.IsNotNull(updatedDummies);
			/* Check: Make sure the returned value contains three items */
			Assert.AreEqual(3, updatedDummies.Count());
			/* Check: Make sure the returned values are the same as the updated entities */
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == updateDummyA.RecordId));
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == updateDummyB.RecordId));
			Assert.IsNotNull(updatedDummies.SingleOrDefault(td => td.RecordId == newDummyD.RecordId));
			Assert.IsNull(updatedDummies.SingleOrDefault(td => td.TextValue.Equals("RowM", StringComparison.Ordinal)));
			Assert.AreEqual("RowB", updatedDummies.Single(td => td.RecordId == updateDummyA.RecordId).TextValue, false);
			Assert.AreEqual("RowC", updatedDummies.Single(td => td.RecordId == updateDummyB.RecordId).TextValue, false);
			Assert.AreEqual("RowN", updatedDummies.Single(td => td.RecordId == newDummyD.RecordId).TextValue, false);

			/* Save the changes */
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);

			retrievedDummies = repository.FindAll();

			/* Check: Make sure the updates were saved correctly */
			Assert.AreEqual(6, retrievedDummies.Count());
			Assert.AreEqual(0, retrievedDummies.Where(td => td.TextValue.Equals("RowM", StringComparison.Ordinal)).Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowB", StringComparison.Ordinal)).Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowC", StringComparison.Ordinal)).Count());
			Assert.AreEqual(1, retrievedDummies.Where(td => td.TextValue.Equals("RowN", StringComparison.Ordinal)).Count());
		}

		/// <summary>Tests the functionality of the <see cref="Repository{T}.UpdateEntities(IEnumerable{T})"/> method when it should throw an 
		/// exception and rollback the operation.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void UpdateMultiple_Exceptions(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			/* Add some additional entities */
			TestDummy newDummyA = new TestDummy { TextValue = "Row3", NumericValue = 3, BooleanValue = true };
			TestDummy newDummyB = new TestDummy { TextValue = "Row4", NumericValue = 3, BooleanValue = true };
			TestDummy newDummyC = new TestDummy { TextValue = "Row5", NumericValue = 3, BooleanValue = true };
			TestDummy[] newDummies = new TestDummy[] { newDummyA, newDummyB, newDummyC };
			repository.AddEntities(newDummies);
			repository.SaveChanges();
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			/* The repository now contains 5 entities */

			/*************************************************************************************************/
			/* SCENARIO 1: Update three entities of which one has RecordId 0                                 */
			/*************************************************************************************************/
			/* Retrieve and change three entities */
			IEnumerable<TestDummy> retrievedDummies = repository.FindAll();
			TestDummy updateDummyA = retrievedDummies.ElementAt(3);
			TestDummy updateDummyB = retrievedDummies.ElementAt(4);
			TestDummy updateDummyC = retrievedDummies.ElementAt(1);
			TestDummy newDummyD = new TestDummy();
			newDummyD.CopyFrom(updateDummyC);
			updateDummyA.TextValue = "RowS";
			updateDummyB.TextValue = "RowT";
			newDummyD.RecordId = 0;

			TestDummy[] updateDummies = new TestDummy[] { updateDummyA, updateDummyB, newDummyD };

			/* Update the entities */
			try {
				repository.UpdateEntities(updateDummies);
				Assert.Fail("'UpdateEntities did not throw the expected InvalidOperationException'");
			}
			catch(InvalidOperationException) {
				retrievedDummies = repository.FindAll();
				/* Check: Make sure the rollback succeeded */
				Assert.AreEqual(5, retrievedDummies.Count());
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowS", StringComparison.Ordinal)));
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowT", StringComparison.Ordinal)));
			}

			/*************************************************************************************************/
			/* SCENARIO 2: Update three entities of which one has RecordId <0 but was not yet added          */
			/*************************************************************************************************/
			/* Retrieve and change three entities */
			updateDummyA = retrievedDummies.ElementAt(3);
			updateDummyB = retrievedDummies.ElementAt(4);
			newDummyD = new TestDummy { TextValue = "RowA", NumericValue = 1, BooleanValue = true, RecordId = -1 };
			updateDummyA.TextValue = "RowB";
			updateDummyB.TextValue = "RowC";

			updateDummies = new TestDummy[] { updateDummyA, updateDummyB, newDummyD };

			/* Update the entities */
			try {
				repository.UpdateEntities(updateDummies);
				Assert.Fail("'UpdateEntities did not throw the expected InvalidOperationException'");
			}
			catch(InvalidOperationException) {
				retrievedDummies = repository.FindAll();
				/* Check: Make sure the rollback succeeded */
				Assert.AreEqual(5, retrievedDummies.Count());
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowA", StringComparison.Ordinal)));
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowB", StringComparison.Ordinal)));
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowC", StringComparison.Ordinal)));
			}

			/*************************************************************************************************/
			/* SCENARIO 3: Update three entities of which one has RecordId >0 but is already deleted         */
			/*************************************************************************************************/
			/* Retrieve and change three entities */
			updateDummyA = retrievedDummies.ElementAt(3);
			updateDummyB = retrievedDummies.ElementAt(4);
			TestDummy obsoleteDummyE = retrievedDummies.ElementAt(1);
			updateDummyA.TextValue = "RowB";
			updateDummyB.TextValue = "RowC";

			/* Delete the entity */
			repository.DeleteEntity(obsoleteDummyE);

			updateDummies = new TestDummy[] { obsoleteDummyE, updateDummyA, updateDummyB };

			/* Update the entities */
			try {
				repository.UpdateEntities(updateDummies);
				Assert.Fail("'UpdateEntities did not throw the expected InvalidOperationException'");
			}
			catch(InvalidOperationException) {
				retrievedDummies = repository.FindAll();
				/* Check: Make sure the rollback succeeded */
				Assert.AreEqual(4, retrievedDummies.Count());
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowB", StringComparison.Ordinal)));
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowC", StringComparison.Ordinal)));
			}

			/*************************************************************************************************/
			/* SCENARIO 6: Update three entities of which one has RecordId >0 but is does not exist          */
			/*************************************************************************************************/
			/* Retrieve and change three entities */
			updateDummyA = retrievedDummies.ElementAt(3);
			updateDummyB = retrievedDummies.ElementAt(2);
			TestDummy unknownDummyF = new TestDummy { TextValue = "RowA", NumericValue = 2, BooleanValue = true, RecordId = 8 };
			updateDummyA.TextValue = "RowB";
			updateDummyB.TextValue = "RowC";

			updateDummies = new TestDummy[] { updateDummyA, unknownDummyF, updateDummyB };

			/* Update the entities */
			try {
				repository.UpdateEntities(updateDummies);
				Assert.Fail("'UpdateEntities did not throw the expected InvalidOperationException'");
			}
			catch(InvalidOperationException) {
				retrievedDummies = repository.FindAll();
				/* Check: Make sure the rollback succeeded */
				Assert.AreEqual(4, retrievedDummies.Count());
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowA", StringComparison.Ordinal)));
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowB", StringComparison.Ordinal)));
				Assert.IsNull(retrievedDummies.SingleOrDefault(td => td.TextValue.Equals("RowC", StringComparison.Ordinal)));
			}
		}
		#endregion

		#region Combined storage test-cases
		/// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void AddUpdate(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

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
			repository = this.CreateRepository<TestDummy>(sourceInfo);
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
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			retrievedDummy = repository.FindSingle(spec);

			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual(addedDummy.RecordId, retrievedDummy.RecordId);
			Assert.AreEqual("RowY", retrievedDummy.TextValue, false);
			Assert.AreEqual(12, retrievedDummy.NumericValue);
			Assert.AreEqual(true, retrievedDummy.BooleanValue);
		}

		/// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void AddUpdateDelete(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

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
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			retrievedDummy = repository.FindSingle(spec);

			Assert.IsNull(retrievedDummy);
		}

		/// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void UpdateDelete(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.BooleanValue);
			/* Retrieve and then update and delete an entity without saving changes or re-creating the repository */
			TestDummy originalDummy = repository.FindSingle(spec);
			originalDummy.TextValue = "RowX";
			TestDummy updatedDummy = repository.UpdateEntity(originalDummy);
			repository.DeleteEntity(updatedDummy);

			TestDummy retrievedDummy = repository.FindSingle(spec);

			Assert.IsNull(retrievedDummy);

			/* Re-create the repository and try to retrieve previous updated entity */
			repository = this.CreateRepository<TestDummy>(sourceInfo);
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
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			retrievedDummy = repository.FindSingle(spec);

			Assert.IsNull(retrievedDummy);
		}

		/// <summary>Tests the functionality of the <see cref="FileRepository{T}"/> when doing multiple storage-actions.</summary>
		/// <param name="sourceInfo">The source info that is used to create the repository.</param>
		protected void DeleteAdd(DataSourceInfo sourceInfo) {
			/* Create the repository */
			Repository<TestDummy> repository = this.CreateRepository<TestDummy>(sourceInfo);

			ISpecification<TestDummy> spec = Specification.Lambda((TestDummy td) => td.BooleanValue);
			/* Delete and then add an entity without saving changes or re-creating the repository */
			TestDummy originalDummy = repository.FindSingle(spec);
			repository.DeleteEntity(originalDummy);

			TestDummy newDummy = new TestDummy { TextValue = "\"Row2\"", NumericValue = 4, BooleanValue = true };
			TestDummy addedDummy = repository.AddEntity(newDummy);

			Assert.IsNotNull(addedDummy);
			Assert.IsTrue(addedDummy.RecordId <= 0);

			TestDummy retrievedDummy = repository.FindSingle(spec);

			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
			Assert.AreEqual(4, retrievedDummy.NumericValue);
			Assert.AreEqual(true, retrievedDummy.BooleanValue);

			/* Re-create the repository and try to retrieve previous updated entity */
			repository = this.CreateRepository<TestDummy>(sourceInfo);
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
			repository = this.CreateRepository<TestDummy>(sourceInfo);
			retrievedDummy = repository.FindSingle(spec);

			Assert.IsNotNull(retrievedDummy);
			Assert.IsNotNull(retrievedDummy);
			Assert.AreEqual("\"Row2\"", retrievedDummy.TextValue, false);
			Assert.AreEqual(4, retrievedDummy.NumericValue);
			Assert.AreEqual(true, retrievedDummy.BooleanValue);
		}
		#endregion

		#region Protected abstract methods
		/// <summary>Creates a new repository using the specified <see cref="DataSourceInfo"/>.</summary>
		/// <typeparam name="T">The type of entity that must be handled by the repository.</typeparam>
		/// <param name="sourceInfo">The data source information that will be used to create a new repository.</param>
		/// <returns>The created repository.</returns>
		protected abstract Repository<T> CreateRepository<T>(DataSourceInfo sourceInfo) where T : class, IEntity<T>, new();
		#endregion
	}
}
