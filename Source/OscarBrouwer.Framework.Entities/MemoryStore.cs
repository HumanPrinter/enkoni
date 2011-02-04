//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryStore.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Describes the basic API of a class that holds entities in memory.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Threading;

namespace Enkoni.Framework.Entities {
  /// <summary>This abstract class defines the API of a class that holds a collection of objects in memory.</summary>
  /// <typeparam name="T">The type of object that is stored.</typeparam>
  public abstract class MemoryStore<T> where T : class {
    #region Static variables
    /// <summary>A lock that controls access to the storage.</summary>
    private static ReaderWriterLockSlim storageLock = new ReaderWriterLockSlim();
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="MemoryStore{T}"/> class.</summary>
    protected MemoryStore() {
    }
    #endregion

    #region Properties
    /// <summary>Gets the storage that holds the saved instances.</summary>
    public abstract IList<T> Storage { get; }
    #endregion

    #region Public methods
    /// <summary>Enters the readlock to synchronize read-access to the <see cref="M:Storage"/>.</summary>
    public void EnterReadLock() {
      this.EnterReadLockCore();
    }

    /// <summary>Exits the readlock to synchronize read-access to the <see cref="M:Storage"/>.</summary>
    public void ExitReadLock() {
      this.ExitReadLockCore();
    }

    /// <summary>Enters the writelock to synchronize write-access to the <see cref="M:Storage"/>.</summary>
    public void EnterWriteLock() {
      this.EnterWriteLockCore();
    }

    /// <summary>Exists the writelock to synchronize write-access to the <see cref="M:Storage"/>.</summary>
    public void ExitWriteLock() {
      this.ExitWriteLockCore();
    }
    #endregion

    #region Protected extensibility methods
    /// <summary>Enters the readlock to synchronize read-access to the <see cref="M:Storage"/>.</summary>
    protected virtual void EnterReadLockCore() {
      storageLock.EnterReadLock();
    }

    /// <summary>Exits the writelock to synchronize write-access to the <see cref="M:Storage"/>.</summary>
    protected virtual void ExitReadLockCore() {
      if(storageLock.IsReadLockHeld) {
        storageLock.ExitReadLock();
      }
    }

    /// <summary>Enters the writelock to synchronize write-access to the <see cref="M:Storage"/>.</summary>
    protected virtual void EnterWriteLockCore() {
      storageLock.EnterWriteLock();
    }

    /// <summary>Exists the writelock to synchronize write-access to the <see cref="M:Storage"/>.</summary>
    protected virtual void ExitWriteLockCore() {
      if(storageLock.IsWriteLockHeld) {
        storageLock.ExitWriteLock();
      }
    }
    #endregion
  }
}
