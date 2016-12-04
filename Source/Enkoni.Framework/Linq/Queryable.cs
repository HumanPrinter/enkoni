using System;
using System.Linq;
using System.Linq.Expressions;

namespace Enkoni.Framework.Linq {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Queryable {
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
  }
}
