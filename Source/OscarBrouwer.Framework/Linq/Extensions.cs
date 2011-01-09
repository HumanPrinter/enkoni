//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds numerous extension-methods that extend the standard .NET Linq capabilities.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace OscarBrouwer.Framework.Linq {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Extensions {
    #region IEnumerable<T> extension methods
    /// <summary>Adds an overload for the Linq-method 'SingleOrDefault' which lets the user define the default value 
    /// that must be returned when the standard 'SingleOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The single element of the input sequence that satisfies the condition, or 
    /// <paramref name="defaultValue"/> if no such element is found.</returns>
    public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
      T result = source.Where(predicate).DefaultIfEmpty(defaultValue).Single();
      return result;
    }

    /// <summary>Adds an overload for the Linq-method 'ElementAtOrDefault' which lets the user define the default value 
    /// that must be returned when the standard 'ElementAtOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns><paramref name="defaultValue"/> if the index is outside the bounds of the source sequence; otherwise, 
    /// the element at the specified position in the source sequence.</returns>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The parametername starts with a lowercase letter")]
    public static T ElementAtOrDefault<T>(this IEnumerable<T> source, int index, T defaultValue) {
      T result = source.ElementAtOrDefault(index);

      if(object.Equals(result, default(T))) {
        return defaultValue;
      }
      else {
        return result;
      }
    }

    /// <summary>Adds an overload for the Linq-method 'FirstOrDefault' which lets the user define the default value 
    /// that must be returned when the standard 'FirstOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The first element of the input sequence, or <paramref name="defaultValue"/> if no such element is 
    /// found.</returns>
    public static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultValue) {
      T result = source.DefaultIfEmpty(defaultValue).First();
      return result;
    }

    /// <summary>Adds an overload for the Linq-method 'FirstOrDefault' which lets the user define the default value 
    /// that must be returned when the standard 'FirstOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The first element of the input sequence that satisfies the condition, or 
    /// <paramref name="defaultValue"/> if no such element is found.</returns>
    public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
      T result = source.Where(predicate).DefaultIfEmpty(defaultValue).First();
      return result;
    }

    /// <summary>Adds an overload for the Linq-method 'LastOrDefault' which lets the user define the default value 
    /// that must be returned when the standard 'LastOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The last element of the input sequence, or <paramref name="defaultValue"/> if no such element is 
    /// found.</returns>
    public static T LastOrDefault<T>(this IEnumerable<T> source, T defaultValue) {
      T result = source.DefaultIfEmpty(defaultValue).Last();
      return result;
    }

    /// <summary>Adds an overload for the Linq-method 'LastOrDefault' which lets the user define the default value 
    /// that must be returned when the standard 'LastOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The last element of the input sequence that satisfies the condition, or 
    /// <paramref name="defaultValue"/> if no such element is found.</returns>
    public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
      T result = source.Where(predicate).DefaultIfEmpty(defaultValue).Last();
      return result;
    }

    /// <summary>Performs an operation on each member in <paramref name="source"/>.</summary>
    /// <typeparam name="T">The type of element that is stored in the enumerable.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to perform the operations on.</param>
    /// <param name="action">The operation that must be performed for each item in the enumerable.</param>
    /// <exception cref="ArgumentNullException">One or more of the parameters are null.</exception>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      if(action == null) {
        throw new ArgumentNullException("action");
      }

      foreach(T item in source) {
        action(item);
      }
    }
    #endregion
  }
}
