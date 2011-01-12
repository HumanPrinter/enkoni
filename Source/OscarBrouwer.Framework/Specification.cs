//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="Specification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines the standard Specification class.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;

namespace OscarBrouwer.Framework {
  /// <summary>This class contains static members that would normally be part of the <see cref="Specification{T}"/> class,
  /// but since that class is generic the static members are placed in this non-generic counterpart to avoid possible
  /// confussion about the use of the methods.</summary>
  public static class Specification {
    #region Public static method
    /// <summary>Creates a specification that will return all available objects.</summary>
    /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
    /// <returns>A specification that will return all available objects.</returns>
    public static ISpecification<T> All<T>() {
      return new LambdaSpecification<T>();
    }

    /// <summary>Creates a specification that will return none of the available objects.</summary>
    /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
    /// <returns>A specification that will return none of the available objects.</returns>
    public static ISpecification<T> None<T>() {
      return new NotSpecification<T>();
    }

    /// <summary>Creates a specification that will return the objects that match the specified expression.</summary>
    /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
    /// <param name="expression">The expression that acts as a filter.</param>
    /// <returns>A specification that will only return the objects that match the expression.</returns>
    public static ISpecification<T> Lambda<T>(Func<T, bool> expression) {
      return new LambdaSpecification<T>(expression);
    }

    /// <summary>Creates a specification that will return the objects for which the specified field matches the specified 
    /// pattern. The pattern supports two types of wildcards. The '*' wildcard matches any character (zero or more times)
    /// and the '?' wildcard matches exactly one character.</summary>
    /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
    /// <param name="field">The field that must match the expression.</param>
    /// <param name="pattern">The search-pattern.</param>
    /// <returns>A specification that will return only the objects for which the field matches the search-pattern.</returns>
    public static ISpecification<T> Like<T>(Func<T, string> field, string pattern) {
      return new LikeSpecification<T>(field, pattern);
    }

    /// <summary>Creates a specification that will return the objects that do not match the specified expression.</summary>
    /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
    /// <param name="expression">The expression that acts as a filter.</param>
    /// <returns>A specification that will only return the objects that do not match the expression.</returns>
    public static ISpecification<T> Not<T>(Func<T, bool> expression) {
      return new NotSpecification<T>(expression);
    }
    #endregion
  }

  /// <summary>This class contains the basic functionality for any specification-class.</summary>
  /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
  [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
      Justification = "Since the static class is merely a container for the static members of the non-static class, they can be in the same file")]
  public abstract class Specification<T> : ISpecification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="Specification{T}"/> class.</summary>
    protected Specification() {
    }
    #endregion   

    #region Public methods
    /// <summary>Creates an 'And' specification that can be used to combine two specifications and compare them using the
    /// '&amp;&amp;' operation.</summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    public virtual ISpecification<T> And(ISpecification<T> specification) {
      return new AndSpecification<T>(this, specification);
    }

    /// <summary>Creates an 'Or' specification that can be used to combine two specifications and compare them using
    /// the '||' operation.</summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    public virtual ISpecification<T> Or(ISpecification<T> specification) {
      return new OrSpecification<T>(this, specification);
    }

    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into
    /// an expression that can be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    /// <exception cref="ArgumentNullException">Parameter is <see langword="null"/>.</exception>
    public Func<T, bool> Visit(ISpecificationVisitor<T> visitor) {
      if(visitor == null) {
        throw new ArgumentNullException("visitor", "The visitor-parameter is mandatory");
      }

      return this.VisitCore(visitor);
    }
    #endregion

    #region Extendibility methods
    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into
    /// an expression that can be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    protected abstract Func<T, bool> VisitCore(ISpecificationVisitor<T> visitor);
    #endregion
  }
}
