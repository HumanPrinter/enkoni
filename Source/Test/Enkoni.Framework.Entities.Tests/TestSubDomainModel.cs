using System;
using System.Collections.Generic;
using System.Linq;

namespace Enkoni.Framework.Entities.Tests {
  /// <summary>This specific databaserepository is used by the testcases to perform tests that cannot be performed on the default 
  /// <see cref="DatabaseRepository{TEntity}"/> class.</summary>
  public class TestSubDomainModel : SubDomainModel<TestDummy> {
    #region Instance variables
    /// <summary>The repository that is used to access the backing store.</summary>
    private Repository<TestDummy> repository;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestSubDomainModel"/> class.</summary>
    /// <param name="parentDomainModel">The parent model that gives access to the other subdomains.</param>
    public TestSubDomainModel(TestDomainModel parentDomainModel)
      : base(parentDomainModel) {
      this.repository = new TestDatabaseRepository(parentDomainModel.DataSourceInfo);
    }
    #endregion

    #region SubDomainModel extensions
    /// <summary>Creates an empty instance of type <see cref="Environment"/>.</summary>
    /// <returns>The created instance.</returns>
    protected override TestDummy CreateEmptyEntityCore() {
      return this.repository.CreateEntity();
    }

    /// <summary>Adds the entity to the domain.</summary>
    /// <param name="entity">The entity that must be added.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TestDummy AddEntityCore(TestDummy entity) {
      entity = this.repository.AddEntity(entity);
      this.repository.SaveChanges();
      return entity;
    }

    /// <summary>Updates the entity in the domain.</summary>
    /// <param name="entity">The entity that must is updated.</param>
    /// <returns>The entity with the most recent values.</returns>
    protected override TestDummy UpdateEntityCore(TestDummy entity) {
      entity = this.repository.UpdateEntity(entity);
      this.repository.SaveChanges();
      return entity;
    }

    /// <summary>Removes the entity from the domain.</summary>
    /// <param name="entity">The entity that must be removed.</param>
    protected override void DeleteEntityCore(TestDummy entity) {
      this.repository.DeleteEntity(entity);
      this.repository.SaveChanges();
    }

    /// <summary>Finds one entities that matches the specified specification.</summary>
    /// <param name="specification">The specification that describes the query that must be performed.</param>
    /// <returns>The found entity or <see langword="null"/> if there was no result.</returns>
    protected override TestDummy FindEntityCore(ISpecification<TestDummy> specification) {
      return this.repository.FindSingle(specification);
    }

    /// <summary>Finds all the entities that match the specified specification.</summary>
    /// <param name="specification">The specification that describes the query that must be performed.</param>
    /// <returns>The found entities or an empty list if there were no results.</returns>
    protected override IList<TestDummy> FindEntitiesCore(ISpecification<TestDummy> specification) {
      return this.repository.FindAll(specification).ToList();
    }
    #endregion
  }
}
