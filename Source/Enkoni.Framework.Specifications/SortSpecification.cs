using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Enkoni.Framework {
  /// <summary>Implements the <see cref="ISortSpecification{T}"/>.</summary>
  /// <typeparam name="T">The type of object that must be sorted.</typeparam>
  /// <typeparam name="TKey">The type of the key that is used to perform the sorting.</typeparam>
  internal class SortSpecification<T, TKey> : ISortSpecification<T> {
    #region Private instance variables

    /// <summary>Defines the direction of the ordering.</summary>
    private SortOrder sortDirection;

    /// <summary>Defines the expression that must be used to perform the sorting.</summary>
    private Expression<Func<T, TKey>> sortExpression;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="SortSpecification{T,TKey}"/> class using the ascending sort order as default sort
    /// direction.</summary>
    /// <param name="sortExpression">The expression that is used for the sorting.</param>
    internal SortSpecification(Expression<Func<T, TKey>> sortExpression)
      : this(sortExpression, SortOrder.Ascending) {
    }

    /// <summary>Initializes a new instance of the <see cref="SortSpecification{T,TKey}"/> class.</summary>
    /// <param name="sortExpression">The expression that is used for the sorting.</param>
    /// <param name="sortDirection">The direction of the sorting.</param>
    internal SortSpecification(Expression<Func<T, TKey>> sortExpression, SortOrder sortDirection) {
      Guard.ArgumentIsNotNull(sortExpression, nameof(sortExpression), "The sort-expression is mandatory.");
      Guard.ArgumentIsValidEnum(typeof(SortOrder), sortDirection, nameof(sortDirection), "The specified sort order is undefined");

      this.sortExpression = sortExpression;
      this.sortDirection = sortDirection;
    }

    #endregion

    #region Methods

    /// <summary>Sorts the elements of a sequence.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    public IOrderedQueryable<T> OrderBy(IQueryable<T> query) {
      Guard.ArgumentIsNotNull(query, nameof(query));

      if(this.sortDirection == SortOrder.Ascending) {
        return query.OrderBy(this.sortExpression);
      }
      else {
        return query.OrderByDescending(this.sortExpression);
      }
    }

    /// <summary>Sorts the elements of a sequence.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    public IOrderedEnumerable<T> OrderBy(IEnumerable<T> query) {
      Guard.ArgumentIsNotNull(query, nameof(query));

      if(this.sortDirection == SortOrder.Ascending) {
        return query.OrderBy(this.sortExpression.Compile());
      }
      else {
        return query.OrderByDescending(this.sortExpression.Compile());
      }
    }

    /// <summary>Performs a subsequent ordering of the elements in a sequence.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    public IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> query) {
      Guard.ArgumentIsNotNull(query, nameof(query));

      if(this.sortDirection == SortOrder.Ascending) {
        return query.ThenBy(this.sortExpression);
      }
      else {
        return query.ThenByDescending(this.sortExpression);
      }
    }

    /// <summary>Performs a subsequent ordering of the elements in a sequence.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    public IOrderedEnumerable<T> ThenBy(IOrderedEnumerable<T> query) {
      Guard.ArgumentIsNotNull(query, nameof(query));

      if(this.sortDirection == SortOrder.Ascending) {
        return query.ThenBy(this.sortExpression.Compile());
      }
      else {
        return query.ThenByDescending(this.sortExpression.Compile());
      }
    }

    #endregion
  }
}
