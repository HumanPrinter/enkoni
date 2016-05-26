using System;
using System.Collections.Generic;

namespace Enkoni.Framework.Collections {
  /// <summary>This class compares types based on a field of the type. By using this class, it is no longer required to create a specific equality 
  /// comparer to compare types using just one field.</summary>
  /// <typeparam name="T">The type of object that must be compared.</typeparam>
  /// <typeparam name="TField">The type of the field of <b>T</b> that must be used in the comparison.</typeparam>
  public class LambdaEqualityComparer<T, TField> : IEqualityComparer<T> {
    #region Private instance variables
    /// <summary>The function that gives access to the field.</summary>
    private Func<T, TField> fieldFunction;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="LambdaEqualityComparer{T, TField}"/> class.</summary>
    /// <param name="field">The function that gives access to the field that must be used in the comparison.</param>
    public LambdaEqualityComparer(Func<T, TField> field) {
      Guard.ArgumentIsNotNull(field, nameof(field));

      this.fieldFunction = field;
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

      TField fieldOfX = this.fieldFunction(x);
      TField fieldOfY = this.fieldFunction(y);

      if(fieldOfX == null && fieldOfY == null) {
        return true;
      }
      else if(fieldOfX == null || fieldOfY == null) {
        return false;
      }
      else {
        return fieldOfX.Equals(fieldOfY);
      }
    }

    /// <summary>Returns a hash code for the specified object.</summary>
    /// <param name="obj">The <see cref="Object"/> for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified object.</returns>
    /// <exception cref="ArgumentNullException">The type of obj is a reference type and obj is null.</exception>
    public int GetHashCode(T obj) {
      Guard.ArgumentIsNotNull(obj, nameof(obj));
      
      TField fieldOfObj = this.fieldFunction(obj);
      if(fieldOfObj == null) {
        return -1;
      }
      else {
        return fieldOfObj.GetHashCode();
      }
    }
    #endregion
  }
}
