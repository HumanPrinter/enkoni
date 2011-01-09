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
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules",
      "SA1628:DocumentationTextMustBeginWithACapitalLetter",
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

    #region ICollection<T> extension methods
    /// <summary>Adds an overload for the ICollection-method 'Remove(T)' which lets the user define a comparer that must be 
    /// used.</summary>
    /// <typeparam name="T">The type of element that is stored in the collection.</typeparam>
    /// <param name="source">An <see cref="ICollection{T}"/> to remove the item from.</param>
    /// <param name="item">The item that must be removed.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the collection.</param>
    /// <returns><see langword="true"/> if item was successfully removed from the <see cref="ICollection{T}"/>; otherwise, 
    /// <see langword="false"/>. This method also returns false if item is not found in the original 
    /// <see cref="ICollection{T}"/>.</returns>
    /// <exception cref="ArgumentNullException">One or more parameters are null.</exception>
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1628:DocumentationTextMustBeginWithACapitalLetter",
      Justification = "The keywords 'true' and 'false' start with a lowercase letter")]
    public static bool Remove<T>(this ICollection<T> source, T item, IEqualityComparer<T> comparer) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      if(comparer == null) {
        throw new ArgumentNullException("comparer");
      }

      if(source.Any(t => comparer.Equals(t, item))) {
        return source.Remove(source.First(t => comparer.Equals(t, item)));
      }
      else {
        return false;
      }
    }
    #endregion

    #region IList<T> extension methods
    /// <summary>Adds an overload for the IList-method 'IndexOf(T)' which lets the user define a comparer to look for the 
    /// desired item.</summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="IList{T}"/> that must be searched.</param>
    /// <param name="item">The item that must be removed from the list.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
    public static int IndexOf<T>(this IList<T> source, T item, IEqualityComparer<T> comparer) {
      if(comparer == null) {
        throw new ArgumentNullException("comparer");
      }

      var itemIndexes = source.Select((t, i) => new { Item = t, Index = i });
      if(itemIndexes.Any(a => comparer.Equals(a.Item, item))) {
        return itemIndexes.First(a => comparer.Equals(a.Item, item)).Index;
      }
      else {
        return -1;
      }
    }
    #endregion

    #region List<T> extension methods
    /// <summary>Adds an overload for the List-method 'IndexOf(T, int)' which lets the user define a comparer to look for 
    /// the desired item.</summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="List{T}"/> that must be searched.</param>
    /// <param name="item">The object to locate in the <see cref="List{T}"/>. The value can be <see langword="null"/> for 
    /// reference types.</param>
    /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The zero-based index of the first occurrence of item within the range of elements in the 
    /// <see cref="List{T}"/> that extends from index to the last element, if found; otherwise, –1.</returns>
    /// <exception cref="ArgumentNullException">Parameter <paramref name="comparer"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the
    /// <see cref="List{T}"/>.</exception>
    public static int IndexOf<T>(this List<T> source, T item, int index, IEqualityComparer<T> comparer) {
      if(comparer == null) {
        throw new ArgumentNullException("comparer");
      }

      if(index < 0 || (index > 0 && index >= source.Count())) {
        throw new ArgumentOutOfRangeException("index", index, "Index is out of range");
      }

      var itemIndexes = source.Skip(index).Select((t, i) => new { Item = t, Index = i });
      if(itemIndexes.Any(a => comparer.Equals(a.Item, item))) {
        return itemIndexes.First(a => comparer.Equals(a.Item, item)).Index + index;
      }
      else {
        return -1;
      }
    }

    /// <summary>Adds an overload for the List-method 'IndexOf(T, int, int)' which lets the user define a comparer to look 
    /// for the desired item.</summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="List{T}"/> that must be searched.</param>
    /// <param name="item">The object to locate in the <see cref="List{T}"/>. The value can be <see langword="null"/> for 
    /// reference types.</param>
    /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
    /// <param name="count">The number of elements in the section to search.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The zero-based index of the first occurrence of item within the range of elements in the 
    /// <see cref="List{T}"/> that starts at index and contains count number of elements, if found; otherwise, –1.</returns>
    /// <exception cref="ArgumentNullException">Parameter <paramref name="comparer"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the
    /// <see cref="List{T}"/>.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> and <paramref name="count"/> do not specify a 
    /// valid section in the <see cref="List{T}"/>.</exception>
    public static int IndexOf<T>(this List<T> source, T item, int index, int count, IEqualityComparer<T> comparer) {
      if(comparer == null) {
        throw new ArgumentNullException("comparer");
      }

      if(index < 0 || (index > 0 && index >= source.Count())) {
        throw new ArgumentOutOfRangeException("index", index, "Index is out of range");
      }

      if(count < 0) {
        throw new ArgumentOutOfRangeException("count", count, "Count cannot be less than zero.");
      }

      if(index + count >= source.Count()) {
        throw new ArgumentOutOfRangeException("count", count, "Collection does not contain enough items.");
      }

      var itemIndexes = source.Skip(index).Take(count).Select((t, i) => new { Item = t, Index = i });
      if(itemIndexes.Any(a => comparer.Equals(a.Item, item))) {
        return itemIndexes.First(a => comparer.Equals(a.Item, item)).Index + index;
      }
      else {
        return -1;
      }
    }
    #endregion
  }
}
