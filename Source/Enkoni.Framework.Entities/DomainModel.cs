namespace Enkoni.Framework.Entities {
  /// <summary>This class defines the public API of a class that represents a domain model.</summary>
  public abstract class DomainModel {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DomainModel"/> class.</summary>
    protected DomainModel() {
    }
    #endregion

    #region Public methods
    /// <summary>Determines if the underlying persistency already exists.</summary>
    /// <returns><see langword="true"/> if the persistency exists; <see langword="false"/> otherwise.</returns>
    public bool PersistencyExists() {
      return this.PersistencyExistsCore();
    }

    /// <summary>Creates the underlying persistency if it does not yet already exist.</summary>
    public void CreatePersistency() {
      if(!this.PersistencyExists()) {
        this.CreatePersistencyCore();
      }
    }

    /// <summary>Clears the underlying persistency.</summary>
    public void ClearPersistency() {
      if(this.PersistencyExists()) {
        this.ClearPersistencyCore();
      }
    }

    /// <summary>Returns the sub domain of a specific type that can be used to perform CRUD-operations on that type.</summary>
    /// <typeparam name="T">The type for which the sub domain must be returned.</typeparam>
    /// <returns>The SubDomain.</returns>
    public SubDomainModel<T> GetSubDomain<T>() where T : IEntity<T> {
      return this.GetSubDomainCore<T>();
    }
    #endregion

    #region Extensibility methods
    /// <summary>Determines if the underlying persistency already exists.</summary>
    /// <returns><see langword="true"/> if the persistency exists; <see langword="false"/> otherwise.</returns>
    protected abstract bool PersistencyExistsCore();

    /// <summary>Creates the underlying persistency.</summary>
    protected abstract void CreatePersistencyCore();

    /// <summary>Clears the underlying persistency.</summary>
    protected abstract void ClearPersistencyCore();

    /// <summary>Returns the sub domain of a specific type that can be used to perform CRUD-operations on that type.</summary>
    /// <typeparam name="T">The type for which the sub domain must be returned.</typeparam>
    /// <returns>The appropriate SubDomainModel.</returns>
    protected abstract SubDomainModel<T> GetSubDomainCore<T>() where T : IEntity<T>;
    #endregion
  }
}
