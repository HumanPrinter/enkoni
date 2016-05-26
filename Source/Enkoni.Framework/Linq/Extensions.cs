using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using Enkoni.Framework.Collections;

namespace Enkoni.Framework.Linq {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Extensions {
    #region IEnumerable<T> extension methods
    /// <summary>Adds an overload for the Linq-method 'SingleOrDefault' which lets the user define the default value that must be returned when the 
    /// standard 'SingleOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The single element of the input sequence that satisfies the condition, or 
    /// <paramref name="defaultValue"/> if no such element is found.</returns>
    public static T SingleOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source));
      Guard.ArgumentIsNotNull(predicate, nameof(predicate));

      T result = source.Where(predicate).DefaultIfEmpty(defaultValue).Single();
      return result;
    }

    /// <summary>Adds an overload for the Linq-method 'ElementAtOrDefault' which lets the user define the default value that must be returned when 
    /// the standard 'ElementAtOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns><paramref name="defaultValue"/> if the index is outside the bounds of the source sequence; otherwise, the element at the specified 
    /// position in the source sequence.</returns>
    public static T ElementAtOrDefault<T>(this IEnumerable<T> source, int index, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source));
      Guard.ArgumentIsGreaterOrEqualThan(0, index, nameof(index), "The index must be a positive integer");

      T result = source.ElementAtOrDefault(index);

      if(object.Equals(result, default(T))) {
        return defaultValue;
      }
      else {
        return result;
      }
    }

    /// <summary>Adds an overload for the Linq-method 'FirstOrDefault' which lets the user define the default value that must be returned when the 
    /// standard 'FirstOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The first element of the input sequence, or <paramref name="defaultValue"/> if no such element is found.</returns>
    public static T FirstOrDefault<T>(this IEnumerable<T> source, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      T result = source.DefaultIfEmpty(defaultValue).First();
      return result;
    }

    /// <summary>Adds an overload for the Linq-method 'FirstOrDefault' which lets the user define the default value that must be returned when the 
    /// standard 'FirstOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The first element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is 
    /// found.</returns>
    public static T FirstOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source));
      Guard.ArgumentIsNotNull(predicate, nameof(predicate));

      T result = source.Where(predicate).DefaultIfEmpty(defaultValue).First();
      return result;
    }

    /// <summary>Adds an overload for the Linq-method 'LastOrDefault' which lets the user define the default value that must be returned when the 
    /// standard 'LastOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The last element of the input sequence, or <paramref name="defaultValue"/> if no such element is found.</returns>
    public static T LastOrDefault<T>(this IEnumerable<T> source, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      T result = source.DefaultIfEmpty(defaultValue).Last();
      return result;
    }

    /// <summary>Adds an overload for the Linq-method 'LastOrDefault' which lets the user define the default value that must be returned when the 
    /// standard 'LastOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The last element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is found.
    /// </returns>
    public static T LastOrDefault<T>(this IEnumerable<T> source, Func<T, bool> predicate, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source));
      Guard.ArgumentIsNotNull(predicate, nameof(predicate));

      T result = source.Where(predicate).DefaultIfEmpty(defaultValue).Last();
      return result;
    }

    /// <summary>Performs an operation on each member in <paramref name="source"/>.</summary>
    /// <typeparam name="T">The type of element that is stored in the enumerable.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{T}"/> to perform the operations on.</param>
    /// <param name="action">The operation that must be performed for each item in the enumerable.</param>
    /// <exception cref="ArgumentNullException">One or more of the parameters are null.</exception>
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action) {
      Guard.ArgumentIsNotNull(source, nameof(source));
      Guard.ArgumentIsNotNull(action, nameof(action));

      foreach(T item in source) {
        action(item);
      }
    }

    /// <summary>Sorts the sequence according to the sort specifications.</summary>
    /// <typeparam name="T">The type of object that must be sorted.</typeparam>
    /// <param name="source">The sequence that must be sorted.</param>
    /// <param name="sortSpecifications">The specifications for the sorting.</param>
    /// <returns>The sorted sequence.</returns>
    public static IEnumerable<T> OrderBy<T>(this IEnumerable<T> source, SortSpecifications<T> sortSpecifications) {
      Guard.ArgumentIsNotNull(source, nameof(source), "The IEnumerable instance is mandatory.");

      if(sortSpecifications == null) {
        return source;
      }
      else {
        return sortSpecifications.Sort(source);
      }
    }

    /// <summary>Creates a new <see cref="IEqualityComparer{T}"/> using the signature of <paramref name="source"/>. The 
    /// comparer will compare two instances of type <typeparamref name="T"/> by evaluating field <paramref name="field"/> of
    /// each instance of <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The type of objects that must be compared.</typeparam>
    /// <typeparam name="TField">The type of the field that must be used for the comparison.</typeparam>
    /// <param name="source">The collection whose signature will be used to create the equality comparer. The collection does 
    /// not actually have to contain any items.</param>
    /// <param name="field">The function that gives access to the field that must be used in the comparison.</param>
    /// <returns>An <see cref="IEqualityComparer{T}"/> that compares objects of type <typeparamref name="T"/>.</returns>
    public static IEqualityComparer<T> CreateEqualityComparer<T, TField>(this IEnumerable<T> source, Func<T, TField> field) {
      Guard.ArgumentIsNotNull(field, nameof(field));

      return new LambdaEqualityComparer<T, TField>(field);
    }

    /// <summary>Returns distinct elements from a sequence by using a <see cref="LambdaEqualityComparer{T,TField}"/> that 
    /// compares values using the specified <paramref name="field"/>.</summary>
    /// <param name="source">The sequence to remove duplicate elements from.</param>
    /// <param name="field">The function that gives access to the field that must be used in the comparison.</param>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <typeparam name="TField">The type of the field that must be used for the comparison.</typeparam>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains distinct elements from the source sequence.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="source"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="field"/> is <see langword="null"/>.</exception>
    public static IEnumerable<T> Distinct<T, TField>(this IEnumerable<T> source, Func<T, TField> field) {
      Guard.ArgumentIsNotNull(source, nameof(source), "The IEnumerable instance is mandatory.");
      Guard.ArgumentIsNotNull(field, nameof(field));
      
      return source.Distinct(source.CreateEqualityComparer(field));
    }

    /// <summary>Partitions a collection into groups based on a key value. This methods differs from the default <see cref="Enumerable.GroupBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/> 
    /// method in the sense that it will create groups of elements with the same key, only if those elements are adjacent.</summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{TSource}"/> whose elements to partition.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IGrouping{TKey, TSource}"/> where each <see cref="IGrouping{TKey,TSource}"/> object contains a sequence of objects and a key.</returns>
    public static IEnumerable<IGrouping<TKey, TSource>> Partition<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) {
      return Partition(source, keySelector, EqualityComparer<TKey>.Default);
    }

    /// <summary>Partitions a collection into groups based on a key value. This methods differs from the default <see cref="Enumerable.GroupBy{TSource, TKey}(IEnumerable{TSource}, Func{TSource, TKey})"/> 
    /// method in the sense that it will create groups of elements with the same key, only if those elements are adjacent.</summary>
    /// <typeparam name="TKey">The type of the key returned by <paramref name="keySelector"/>.</typeparam>
    /// <typeparam name="TSource">The type of the elements of <paramref name="source"/>.</typeparam>
    /// <param name="source">An <see cref="IEnumerable{TSource}"/> whose elements to partition.</param>
    /// <param name="keySelector">A function to extract the key for each element.</param>
    /// <param name="comparer">An <see cref="IEqualityComparer{T}"/> to compare keys.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="IGrouping{TKey, TSource}"/> where each <see cref="IGrouping{TKey,TSource}"/> object contains a sequence of objects and a key.</returns>
    public static IEnumerable<IGrouping<TKey, TSource>> Partition<TKey, TSource>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer) {
      Guard.ArgumentIsNotNull(source, nameof(source), "The IEnumerable instance is mandatory.");
      Guard.ArgumentIsNotNull(keySelector, nameof(keySelector));
      Guard.ArgumentIsNotNull(comparer, nameof(comparer));
      
      List<TSource> sourceList = source.ToList();
      if(sourceList.Count <= 1) {
        return source.GroupBy(keySelector);
      }

      var indexedCollection = source.Select((item, index) => new { Index = index, Item = item }).ToList();
      IEnumerable<IGrouping<TKey, TSource>> result = Enumerable.Empty<IGrouping<TKey, TSource>>();

      int startOfNextPart = 0;
      while(startOfNextPart < sourceList.Count) {
        List<TSource> partition = indexedCollection
            .Skip(startOfNextPart)
            .TakeWhile(indexedItem => indexedItem.Index - startOfNextPart == 0 || comparer.Equals(keySelector(indexedItem.Item), keySelector(sourceList.ElementAt(indexedItem.Index - 1))))
            .Select(a => a.Item)
            .ToList();
        startOfNextPart += partition.Count;
        result = result.Concat(partition.GroupBy(keySelector));
      }

      return result;
    }
    #endregion

    #region IQueryable<T> extension methods
    /// <summary>Adds an overload for the Linq-method 'SingleOrDefault' which lets the user define the default value that must be returned when the 
    /// standard 'SingleOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IQueryable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The single element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is 
    /// found.</returns>
    public static T SingleOrDefault<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source), "The IQueryable instance is mandatory.");

      IQueryable<T> queryResult = source.Where(predicate);
      if(queryResult.Any()) {
        return queryResult.Single();
      }
      else {
        return defaultValue;
      }
    }

    /// <summary>Adds an overload for the Linq-method 'FirstOrDefault' which lets the user define the default value that must be returned when the 
    /// standard 'FirstOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IQueryable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The first element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is 
    /// found.</returns>
    public static T FirstOrDefault<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source), "The IQueryable instance is mandatory.");

      IQueryable<T> queryResult = source.Where(predicate);
      if(queryResult.Any()) {
        return queryResult.First();
      }
      else {
        return defaultValue;
      }
    }

    /// <summary>Adds an overload for the Linq-method 'LastOrDefault' which lets the user define the default value that must be returned when the 
    /// standard 'LastOrDefault' operation yields no results.</summary>
    /// <typeparam name="T">The type of the elements of source.</typeparam>
    /// <param name="source">An <see cref="IQueryable{T}"/> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <param name="defaultValue">The default value that must be used.</param>
    /// <returns>The last element of the input sequence that satisfies the condition, or <paramref name="defaultValue"/> if no such element is found.
    /// </returns>
    public static T LastOrDefault<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, T defaultValue) {
      Guard.ArgumentIsNotNull(source, nameof(source), "The IQueryable instance is mandatory.");

      IQueryable<T> queryResult = source.Where(predicate);
      if(queryResult.Any()) {
        return queryResult.Last();
      }
      else {
        return defaultValue;
      }
    }

    /// <summary>Sorts the sequence according to the sort specifications.</summary>
    /// <typeparam name="T">The type of object that must be sorted.</typeparam>
    /// <param name="source">The sequence that must be sorted.</param>
    /// <param name="sortSpecifications">The specifications for the sorting.</param>
    /// <returns>The sorted sequence.</returns>
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, SortSpecifications<T> sortSpecifications) {
      Guard.ArgumentIsNotNull(source, nameof(source), "The IQueryable instance is mandatory.");

      if(sortSpecifications == null) {
        return source;
      }
      else {
        return sortSpecifications.Sort(source);
      }
    }
    #endregion

    #region Expression<Func<T,bool>> extension methods
    /// <summary>Creates an <see cref="Expression{TDelegate}"/> that inverts the result of <paramref name="source"/>.</summary>
    /// <typeparam name="T">The type that is used as input for the expression.</typeparam>
    /// <param name="source">An <see cref="Expression{TDelegate}"/> whose result must be inverted.</param>
    /// <returns>An expression that will invert the result of <paramref name="source"/>.</returns>
    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> source) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      return Expression.Lambda<Func<T, bool>>(Expression.Not(source.Body), source.Parameters[0]);
    }
    #endregion
  }
}
