using System.Collections.Generic;

namespace Enkoni.Framework.Entities {
  /// <summary>This class implements the abstract class <see cref="MemoryStore{T}"/> using a simple static collection variable.</summary>
  /// <typeparam name="T">The type of object that is stored.</typeparam>
  public class StaticMemoryStore<T> : MemoryStore<T> where T : class {
    #region Static variables

    /// <summary>The storage itself.</summary>
    private static List<T> storage = new List<T>();

    #endregion

    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="StaticMemoryStore{T}"/> class.</summary>
    public StaticMemoryStore()
      : base() {
    }

    #endregion

    #region Properties

    /// <summary>Gets the storage that holds the saved instances.</summary>
    public override IList<T> Storage {
      get { return storage; }
    }

    #endregion
  }
}
