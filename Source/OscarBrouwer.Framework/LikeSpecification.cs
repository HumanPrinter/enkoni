//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="LikeSpecification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines a specificationtype that matches based on a string-pattern.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace OscarBrouwer.Framework {
  /// <summary>This class defines a specification type that matches using a string-pattern.</summary>
  /// <typeparam name="T">The type of object to which the specification applies.</typeparam>
  internal class LikeSpecification<T> : Specification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="LikeSpecification{T}"/> class.</summary>
    /// <param name="field">The field that must match the pattern.</param>
    /// <param name="pattern">The search-pattern.</param>
    internal LikeSpecification(Expression<Func<T, string>> field, string pattern) {
      this.Field = field;
      this.Pattern = pattern;
    }
    #endregion

    #region Public properties
    /// <summary>Gets the field that must match the pattern.</summary>
    public Expression<Func<T, string>> Field { get; private set; }

    /// <summary>Gets the search-pattern.</summary>
    public string Pattern { get; private set; }
    #endregion

    #region Specification-overrides
    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into
    /// an expression that can be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    protected override Expression<Func<T, bool>> VisitCore(ISpecificationVisitor<T> visitor) {
      return visitor.CreateLikeExpression(this.Field, this.Pattern);
    }
    #endregion
  }
}
