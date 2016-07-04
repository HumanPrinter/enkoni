using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

using Enkoni.Framework.Collections;

namespace Enkoni.Framework {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Extensions {
    #region Private static variables

    /// <summary>Caches types and there derived types for performance reasons.</summary>
    private static Dictionary<Type, Type[]> typeCache = new Dictionary<Type, Type[]>();

    #endregion

    #region Double extensions

    /// <summary>Returns a value indicating whether both values represent the same value.</summary>
    /// <param name="source">The first value to compare.</param>
    /// <param name="obj">The second value to compare.</param>
    /// <param name="comparisonFactor">The factor that must be taken into account. If <paramref name="compareOption"/> is set to
    /// <see cref="DoubleCompareOption.Margin"/>, the comparison factor will be treated as an absolute margin. If <paramref name="compareOption"/> is
    /// set to <see cref="DoubleCompareOption.SignificantDigits"/> the comparison factor will be treated as the number of digits that must be
    /// examined will comparing. Note that the comparison factor in that case will be truncated to an integer.</param>
    /// <param name="compareOption">Defines the method that must be used to compare the double values.</param>
    /// <returns><see langword="true"/> if the two values are equal; otherwise, <see langword="false"/>.</returns>
    public static bool Equals(this double source, double obj, double comparisonFactor, DoubleCompareOption compareOption) {
      return new DoubleEqualityComparer(comparisonFactor, compareOption).Equals(source, obj);
    }

    /// <summary>Compares the two values and returns an integer that indicates whether the first value is less than, equal to, or greater than the
    /// second value.</summary>
    /// <param name="source">The first value to compare.</param>
    /// <param name="value">The second value to compare.</param>
    /// <param name="comparisonFactor">The factor that must be taken into account. If <paramref name="compareOption"/> is set to
    /// <see cref="DoubleCompareOption.Margin"/>, the comparison factor will be treated as an absolute margin. If <paramref name="compareOption"/> is
    /// set to <see cref="DoubleCompareOption.SignificantDigits"/> the comparison factor will be treated as the number of digits that must be
    /// examined will comparing. Note that the comparison factor in that case will be truncated to an integer.</param>
    /// <param name="compareOption">Defines the method that must be used to compare the double values.</param>
    /// <returns>A signed number indicating the relative values of the two numbers. <br />
    /// Return Value Description <br/>
    /// Less than zero: <paramref name="source"/> is less than <paramref name="value"/> -or- <paramref name="source"/> is not a number
    /// (<see cref="double.NaN"/>) and value is a number.<br/>
    /// Zero: <paramref name="source"/> is equal to <paramref name="value"/> -or- Both <paramref name="source"/> and <paramref name="value"/> are
    /// not a number (<see cref="double.NaN"/>), <see cref="double.PositiveInfinity"/>, or <see cref="double.NegativeInfinity"/>.<br/>
    /// Greater than zero: <paramref name="source"/> is greater than <paramref name="value"/> -or- <paramref name="source"/> is a number and
    /// <paramref name="value"/> is not a number (<see cref="double.NaN"/>).</returns>
    public static int CompareTo(this double source, double value, double comparisonFactor, DoubleCompareOption compareOption) {
      return new DoubleComparer(comparisonFactor, compareOption).Compare(source, value);
    }

    #endregion

    #region Type extensions

    /// <summary>Determines if the specified type actually a nullable type.</summary>
    /// <param name="source">The type that is investigated.</param>
    /// <returns><see langword="true"/> is <paramref name="source"/> denotes a nullable type, <see langword="false"/>
    /// otherwise.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
    public static bool IsNullable(this Type source) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      return source.IsGenericType && source.GetGenericTypeDefinition() == typeof(Nullable<>);
    }

    /// <summary>Returns the actual type of <paramref name="source"/>. If <paramref name="source"/> denotes a nullable type,
    /// the underlying type is returned. Otherwise, <paramref name="source"/> is returned.</summary>
    /// <param name="source">The type that is investigated.</param>
    /// <returns>The underlying type if <paramref name="source"/> is nullable or <paramref name="source"/> if it is not
    /// nullable.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is null.</exception>
    public static Type ActualType(this Type source) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      if(source.IsNullable()) {
        NullableConverter converter = new NullableConverter(source);
        return converter.UnderlyingType;
      }
      else {
        return source;
      }
    }

    /// <summary>Gets all the base classes extended or inherited by the current <see cref="Type"/>.</summary>
    /// <param name="source">The <see cref="Type"/> whose base classes must be retrieved.</param>
    /// <returns>The base classes that are extended or inherited by the current <see cref="Type"/>.</returns>
    public static Type[] GetBaseClasses(this Type source) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      List<Type> baseTypes = new List<Type>();

      Type baseType = source.BaseType;
      while(baseType != null) {
        if(source.IsGenericTypeDefinition && baseType.IsGenericType) {
          baseType = baseType.GetGenericTypeDefinition();
        }

        baseTypes.Add(baseType);
        baseType = baseType.BaseType;
      }

      return baseTypes.ToArray();
    }

    /// <summary>Gets all the base types (base classes and interfaces) extended, implemented or inherited by the current <see cref="Type"/>.</summary>
    /// <param name="source">The <see cref="Type"/> whose base types must be retrieved.</param>
    /// <returns>The base types that are extended, implemented or inherited by the current <see cref="Type"/>.</returns>
    public static Type[] GetBaseTypes(this Type source) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      Type[] interfaces = source.GetInterfaces();
      Type[] baseClasses = source.GetBaseClasses();
      if(interfaces.Length == 0) {
        return baseClasses;
      }

      if(baseClasses.Length == 0) {
        return interfaces;
      }

      Type[] baseTypes = new Type[interfaces.Length + baseClasses.Length];
      Array.Copy(interfaces, baseTypes, interfaces.Length);
      Array.Copy(baseClasses, 0, baseTypes, interfaces.Length, baseClasses.Length);

      return baseTypes;
    }

    /// <summary>Determines if the source implements the specified base type.</summary>
    /// <param name="source">The instance that must be evaluated.</param>
    /// <param name="baseType">The base type that must be implemented.</param>
    /// <returns><see langword="true"/> if <paramref name="source"/> implements <paramref name="baseType"/>;
    /// <see langword="false"/> otherwise.</returns>
    public static bool Implements(this Type source, Type baseType) {
      Guard.ArgumentIsNotNull(source, nameof(source));
      Guard.ArgumentIsNotNull(baseType, nameof(baseType));

      return source.Equals(baseType) || source.GetBaseTypes().Contains(baseType, new TypeEqualityComparer());
    }

    /// <summary>Gets the types that derive from <paramref name="source"/> and are available in the loaded assemblies. If this type has been evaluated
    /// before, the previous results are returned regardless of any assembly load or unload event.</summary>
    /// <param name="source">The instance that must be evaluated.</param>
    /// <returns>The detected types that derive from <paramref name="source"/>.</returns>
    public static Type[] GetDerivedTypes(this Type source) {
      return GetDerivedTypes(source, false);
    }

    /// <summary>Gets the types that derive from <paramref name="source"/> and are available in the loaded assemblies.</summary>
    /// <param name="source">The instance that must be evaluated.</param>
    /// <param name="forceRescan">Forces to ignore the previous scan-results and rescan the loaded assemblies.</param>
    /// <returns>The detected types that derive from <paramref name="source"/>.</returns>
    public static Type[] GetDerivedTypes(this Type source, bool forceRescan) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      return Find(source, forceRescan);
    }

    #endregion

    #region Helper methods

    /// <summary>Gets the derived types from the cache for the specified type.</summary>
    /// <param name="parent">The parent type.</param>
    /// <param name="forceRescan">Indicates whether or not to ignore the cache.</param>
    /// <returns>The found types.</returns>
    private static Type[] Find(Type parent, bool forceRescan) {
      if(forceRescan) {
        lock(typeCache) {
          if(typeCache.ContainsKey(parent)) {
            typeCache.Remove(parent);
          }
        }
      }

      Type[] result;
      if(typeCache.TryGetValue(parent, out result)) {
        return result;
      }

      List<Type> list = new List<Type>();
      foreach(Assembly assembly in AppDomain.CurrentDomain.GetAssemblies()) {
        try {
          if(parent.IsInterface) {
            list.AddRange(assembly.GetTypes().Where(t => !t.Equals(parent) && parent.IsAssignableFrom(t)));
          }
          else if(parent.IsGenericParameter) {
            Type[] contrains = parent.GetGenericParameterConstraints();
            if(contrains.Length > 0) {
              foreach(Type type in assembly.GetTypes()) {
                bool add = true;
                foreach(Type item in contrains) {
                  if(!item.IsAssignableFrom(type)) {
                    add = false;
                    break;
                  }
                }

                if(add) {
                  list.Add(type);
                }
              }
            }
          }
          else {
            list.AddRange(assembly.GetTypes().Where(t => t.IsSubclassOf(parent)));
          }
        }
        catch(ReflectionTypeLoadException) {
          /* Ignore this assembly and move on */
        }
      }

      lock(typeCache) {
        if(!typeCache.TryGetValue(parent, out result)) {
          result = list.ToArray();
          typeCache.Add(parent, result);
        }
      }

      return result;
    }

    #endregion

    #region Private classes

    /// <summary>Provides the functionality to compare two <see cref="Type"/> instances.</summary>
    private class TypeEqualityComparer : IEqualityComparer<Type> {
      #region Constructors

      /// <summary>Initializes a new instance of the <see cref="TypeEqualityComparer"/> class.</summary>
      public TypeEqualityComparer() {
      }

      #endregion

      #region IEqualityComparer implementation

      /// <summary>Determines if the specified instances are equal.</summary>
      /// <param name="x">The left operand.</param>
      /// <param name="y">The right operand.</param>
      /// <returns><see langword="true"/> if both parameters are equal; <see langword="false"/> otherwise.</returns>
      public bool Equals(Type x, Type y) {
        if(x == null && y == null) {
          return true;
        }

        if(x == null || y == null) {
          return false;
        }

        if(ReferenceEquals(x, y)) {
          return true;
        }

        if(x == y) {
          return true;
        }

        return x.GUID == y.GUID && x.Assembly == y.Assembly && x.Name == y.Name && x.Namespace == y.Namespace;
      }

      /// <summary>Returns the hash code for the given instance.</summary>
      /// <param name="obj">The instance whose hash code must be returned.</param>
      /// <returns>The hash code of <paramref name="obj"/> or zero if <paramref name="obj"/> is
      /// <see langword="null"/>.</returns>
      public int GetHashCode(Type obj) {
        return obj == null ? 0 : obj.GetHashCode();
      }

      #endregion
    }

    #endregion
  }
}
