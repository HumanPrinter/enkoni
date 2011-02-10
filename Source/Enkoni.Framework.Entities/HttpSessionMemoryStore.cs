//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpSessionMemoryStore.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines a class that holds objects in a variable that is stored in a HTTP session.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Web;

namespace Enkoni.Framework.Entities {
  /// <summary>This class implements the abstract class <see cref="MemoryStore{T}"/> using collections that are stored in a HTTP Session.</summary>
  /// <typeparam name="T">The type of object that is stored.</typeparam>
  public class HttpSessionMemoryStore<T> : MemoryStore<T> where T : class {
    #region Static variables
    /// <summary>The key that is used to retrieve the storage-collection from the HttpSession.</summary>
    private const string StorageKey = "HttpSessionMemoryStoreStorage";
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="HttpSessionMemoryStore{T}"/> class.</summary>
    public HttpSessionMemoryStore()
      : base() {
    }
    #endregion

    #region Properties
    /// <summary>Gets the storage that holds the saved instances.</summary>
    public override IList<T> Storage {
      get { return RetrieveStorage(); }
    }
    #endregion

    #region Private static helper methods
    /// <summary>Retrieves the storage from the current session. If there is no storage available yet, a new one is created.</summary>
    /// <returns>The storage that is stored in the session.</returns>
    private static IList<T> RetrieveStorage() {
      if(HttpContext.Current == null || HttpContext.Current.Session == null) {
        throw new InvalidOperationException("No current Http Context or Http Session available");
      }
      else {
        if(HttpContext.Current.Session[StorageKey] == null) {
          HttpContext.Current.Session[StorageKey] = new List<T>();
        }
        else if(!(HttpContext.Current.Session[StorageKey] is List<T>)) {
          throw new InvalidOperationException("The current session contains an unexpected item for the used key");
        }

        return HttpContext.Current.Session[StorageKey] as List<T>;
      }
    }
    #endregion
  }
}
