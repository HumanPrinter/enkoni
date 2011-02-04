//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="SortSpecifications.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds a collection of sortspecifications.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;

namespace Enkoni.Framework {
  /// <summary>This class holds a collection of sortspecifications which can be used to sort a sequence of 
  /// <typeparamref name="T"/>.</summary>
  /// <typeparam name="T">The type of object that must be sorted.</typeparam>
  public class SortSpecifications<T> {
    #region Private instance variables
    /// <summary>The collection of sortspecifications.</summary>
    private List<ISortSpecification<T>> sortingSpecifications;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="SortSpecifications{T}"/> class.</summary>
    public SortSpecifications() {
      this.sortingSpecifications = new List<ISortSpecification<T>>();
    }
    #endregion

    #region Public methods
    /// <summary>Sorts a sequence according to the specifications that are held by this instance.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    public IQueryable<T> Sort(IQueryable<T> query) {
      IOrderedQueryable<T> orderedQuery = this.sortingSpecifications.First().OrderBy(query);
      foreach(ISortSpecification<T> sortSpec in this.sortingSpecifications.Skip(1)) {
        orderedQuery = sortSpec.ThenBy(orderedQuery);
      }

      query = orderedQuery;
      return query;
    }

    /// <summary>Sorts a sequence according to the specifications that are held by this instance.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    public IEnumerable<T> Sort(IEnumerable<T> query) {
      IOrderedEnumerable<T> orderedQuery = this.sortingSpecifications.First().OrderBy(query);
      foreach(ISortSpecification<T> sortSpec in this.sortingSpecifications.Skip(1)) {
        orderedQuery = sortSpec.ThenBy(orderedQuery);
      }

      query = orderedQuery;
      return query;
    }
    #endregion

    #region Internal methods
    /// <summary>Adds a new sortspecification to the collection.</summary>
    /// <param name="sortSpecification">The sortspecification that must be added.</param>
    internal void Add(ISortSpecification<T> sortSpecification) {
      if(sortSpecification == null) {
        throw new ArgumentNullException("sortSpecification", "The ISortSpecification is mandatory");
      }

      this.sortingSpecifications.Add(sortSpecification);
    }

    /// <summary>Adds a new collcetion of sortspecifications to the collection.</summary>
    /// <param name="sortSpecifications">The sortspecifications that must be added.</param>
    internal void AddRange(SortSpecifications<T> sortSpecifications) {
      if(sortSpecifications == null) {
        throw new ArgumentNullException("sortSpecifications", "The SortSpecifications is mandatory");
      }

      this.sortingSpecifications.AddRange(sortSpecifications.sortingSpecifications);
    }

    /// <summary>Removes all the sortspecifications from the collection.</summary>
    internal void Clear() {
      this.sortingSpecifications.Clear();
    }
    #endregion
  }
}
