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
  /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
  public interface ISpecification<T> {
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

    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into
    /// an expression that can be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    Func<T, bool> Visit(ISpecificationVisitor<T> visitor);
  }
}
