//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ISortSpecification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines the basic sortspecification API.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Enkoni.Framework {
  /// <summary>Defines a type that is able to sort a collection using the specificationsystem.</summary>
  /// <typeparam name="T">The type of object that must be sorted.</typeparam>
  internal interface ISortSpecification<T> {
    /// <summary>Sorts the elements of a sequence.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    IOrderedQueryable<T> OrderBy(IQueryable<T> query);

    /// <summary>Sorts the elements of a sequence.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    IOrderedEnumerable<T> OrderBy(IEnumerable<T> query);

    /// <summary>Performs a subsequent ordering of the elements in a sequence.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    IOrderedQueryable<T> ThenBy(IOrderedQueryable<T> query);

    /// <summary>Performs a subsequent ordering of the elements in a sequence.</summary>
    /// <param name="query">The sequence that must be sorted.</param>
    /// <returns>The sorted sequence.</returns>
    IOrderedEnumerable<T> ThenBy(IOrderedEnumerable<T> query);
  }
}
