using System;
using System.Collections.Generic;
using System.Linq;

namespace Enkoni.Framework.Collections {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Extensions {
    #region ICollection<T> extension methods

    /// <summary>Removes the first occurrence of a specific object from the <see cref="ICollection{T}"/>.</summary>
    /// <typeparam name="T">The type of element that is stored in the collection.</typeparam>
    /// <param name="source">An <see cref="ICollection{T}"/> to remove the item from.</param>
    /// <param name="item">The item that must be removed.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the collection.</param>
    /// <returns><see langword="true"/> if item was successfully removed from the <see cref="ICollection{T}"/>; otherwise, <see langword="false"/>.
    /// This method also returns false if item is not found in the original <see cref="ICollection{T}"/>.</returns>
    /// <exception cref="ArgumentNullException">One or more parameters are null.</exception>
    public static bool Remove<T>(this ICollection<T> source, T item, IEqualityComparer<T> comparer) {
      Guard.ArgumentIsNotNull(source, nameof(source));
      Guard.ArgumentIsNotNull(comparer, nameof(comparer));

      if(source.Any(t => comparer.Equals(t, item))) {
        return source.Remove(source.First(t => comparer.Equals(t, item)));
      }
      else {
        return false;
      }
    }

    #endregion

    #region IList<T> extension methods

    /// <summary>Adds an overload for the IList-method 'IndexOf(T)' which lets the user define a comparer to look for the desired item.</summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="IList{T}"/> that must be searched.</param>
    /// <param name="item">The item that must be removed from the list.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
    public static int IndexOf<T>(this IList<T> source, T item, IEqualityComparer<T> comparer) {
      Guard.ArgumentIsNotNull(comparer, nameof(comparer));

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

    /// <summary>Adds an overload for the List-method 'IndexOf(T, int)' which lets the user define a comparer to look for the desired item.</summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="List{T}"/> that must be searched.</param>
    /// <param name="item">The object to locate in the <see cref="List{T}"/>. The value can be <see langword="null"/> for reference types.</param>
    /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="List{T}"/> that extends from
    /// index to the last element, if found; otherwise, –1.</returns>
    /// <exception cref="ArgumentNullException">Parameter <paramref name="comparer"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="List{T}"/>.
    /// </exception>
    public static int IndexOf<T>(this List<T> source, T item, int index, IEqualityComparer<T> comparer) {
      Guard.ArgumentIsNotNull(comparer, nameof(comparer));

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

    /// <summary>Adds an overload for the List-method 'IndexOf(T, int, int)' which lets the user define a comparer to look for the desired item.
    /// </summary>
    /// <typeparam name="T">The type of element that is stored in the list.</typeparam>
    /// <param name="source">An <see cref="List{T}"/> that must be searched.</param>
    /// <param name="item">The object to locate in the <see cref="List{T}"/>. The value can be <see langword="null"/> for reference types.</param>
    /// <param name="index">The zero-based starting index of the search. 0 (zero) is valid in an empty list.</param>
    /// <param name="count">The number of elements in the section to search.</param>
    /// <param name="comparer">The comparer that must be used to find the appropriate item in the list.</param>
    /// <returns>The zero-based index of the first occurrence of item within the range of elements in the <see cref="List{T}"/> that starts at index
    /// and contains count number of elements, if found; otherwise, –1.</returns>
    /// <exception cref="ArgumentNullException">Parameter <paramref name="comparer"/> is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> is outside the range of valid indexes for the <see cref="List{T}"/>.
    /// </exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="count"/> is less than 0.</exception>
    /// <exception cref="ArgumentOutOfRangeException"><paramref name="index"/> and <paramref name="count"/> do not specify a valid section in the
    /// <see cref="List{T}"/>.</exception>
    public static int IndexOf<T>(this List<T> source, T item, int index, int count, IEqualityComparer<T> comparer) {
      Guard.ArgumentIsNotNull(comparer, nameof(comparer));
      Guard.ArgumentIsBetween(0, source.Count - 1, index, nameof(index), "Index is out of range");
      Guard.ArgumentIsGreaterOrEqualThan(0, count, nameof(count), "Count cannot be less then zero");

      if(index + count > source.Count()) {
        throw new ArgumentOutOfRangeException(nameof(count), count, "Collection does not contain enough items.");
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
