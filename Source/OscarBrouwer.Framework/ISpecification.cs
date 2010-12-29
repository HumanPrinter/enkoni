//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpecification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines the basic specification API.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;

namespace OscarBrouwer.Framework {
  /// <summary>This interface describes the public API of the classes that make up the specification subsystem that is
  /// implemented using the Specification Pattern.</summary>
  /// <typeparam name="T">The type of object to which the specification applies.</typeparam>
  public interface ISpecification<T> {
    /// <summary>Gets the function that equals the search-pattern that is implemented by this type.</summary>
    Func<T, bool> IsSatisfiedBy { get; }

    /// <summary>Creates an 'And' specification that can be used to combine two specifications and compare them using
    /// the '&amp;&amp;' operation.</summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    ISpecification<T> And(ISpecification<T> specification);

    /// <summary>Creates an 'Or' specification that can be used to combine two specifications and compare them using
    /// the '||' operation.</summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    ISpecification<T> Or(ISpecification<T> specification);

    /// <summary>Creates a 'Not' specification that can be used to inverse the result of the specification.</summary>
    /// <returns>The combined specification.</returns>
    ISpecification<T> Not();
  }
}
