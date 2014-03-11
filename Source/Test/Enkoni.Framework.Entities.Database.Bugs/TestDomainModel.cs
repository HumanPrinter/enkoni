using System;

namespace Enkoni.Framework.Entities.Database.Bugs {
  /// <summary>Defines a domain model that is used in the test cases.</summary>
  public class TestDomainModel : DomainModel {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="TestDomainModel"/> class.</summary>
    /// <param name="dataSourceInfo">The datasource information that must be used to access the database.</param>
    public TestDomainModel(DataSourceInfo dataSourceInfo)
      : base() {
      this.DataSourceInfo = dataSourceInfo;
    }
    #endregion

    #region Properties
    /// <summary>Gets the datasource info that has information about the datasource.</summary>
    public DataSourceInfo DataSourceInfo { get; private set; }
    #endregion

    #region DomainModel extensions
    /// <summary>Determines if the underlying persistency already exists.</summary>
    /// <returns><see langword="true"/> if the persistency exists; <see langword="false"/> otherwise.</returns>
    protected override bool PersistencyExistsCore() {
      throw new NotImplementedException();
    }

    /// <summary>Creates the underlying persistency.</summary>
    protected override void CreatePersistencyCore() {
      throw new NotImplementedException();
    }

    /// <summary>Clears the underlying persistency.</summary>
    protected override void ClearPersistencyCore() {
      throw new NotImplementedException();
    }

    /// <summary>Returns the subdomain of a specific type that can be used to perform CRUD-operations on that type.</summary>
    /// <typeparam name="T">The type for which the subdomain must be returned.</typeparam>
    /// <returns>The appropriate SubDomainModel.</returns>
    protected override SubDomainModel<T> GetSubDomainCore<T>() {
      if(typeof(T) == typeof(TestDummy)) {
        return new TestSubDomainModel(this) as SubDomainModel<T>;
      }
      else {
        throw new InvalidTypeParameterException(string.Format("Type {0} is not supported", typeof(T)));
      }
    }
    #endregion
  }
}
