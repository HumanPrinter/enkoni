//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ReferenceEqualityComparer.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Implements a comparer-class that simply performs a ReferenceEquals to determine if two objects are equal.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Enkoni.Framework.Collections {
  /// <summary>This class compares types based on their reference.</summary>
  /// <typeparam name="T">The type of object that must be compared.</typeparam>
  public class ReferenceEqualityComparer<T> : IEqualityComparer<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="ReferenceEqualityComparer{T}"/> class.</summary>
    public ReferenceEqualityComparer() {
    }
    #endregion

    #region IEqualityComparer<T> Members
    /// <summary>Determines whether the specified objects are equal.</summary>
    /// <param name="x">The first object of type T to compare.</param>
    /// <param name="y">The second object of type T to compare.</param>
    /// <returns><see langword="true"/> if the specified objects are equal; otherwise, <see langword="false"/>.</returns>
    public bool Equals(T x, T y) {
      if(x == null && y == null) {
        return true;
      }
      else if(x == null || y == null) {
        return false;
      }

      return object.ReferenceEquals(x, y);
    }

    /// <summary>Returns a hash code for the specified object.</summary>
    /// <param name="obj">The <see cref="Object"/> for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified object.</returns>
    /// <exception cref="ArgumentNullException">The type of obj is a reference type and obj is null.</exception>
    public int GetHashCode(T obj) {
      if(obj == null) {
        throw new ArgumentNullException("obj");
      }
      else {
        return obj.GetHashCode();
      }
    }
    #endregion
  }
}
