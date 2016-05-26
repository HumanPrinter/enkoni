using System.Collections.Generic;

namespace Enkoni.Framework.Entities {
  /// <summary>This class implements the abstract class <see cref="MemoryStore{T}"/> using a simple collection variable.</summary>
  /// <typeparam name="T">The type of object that is stored.</typeparam>
  public class InstanceMemoryStore<T> : MemoryStore<T> where T : class {
    #region Instance variables

    /// <summary>The storage itself.</summary>
    private List<T> storage = new List<T>();

    #endregion

    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="InstanceMemoryStore{T}"/> class.</summary>
    public InstanceMemoryStore()
      : base() {
    }

    #endregion

    #region Properties

    /// <summary>Gets the storage that holds the saved instances.</summary>
    public override IList<T> Storage {
      get { return this.storage; }
    }

    #endregion
  }
}
