//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="LambdaSpecification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Defines a Specification class that uses a lambda-expression to define the desired specification.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace Enkoni.Framework {
  /// <summary>This class implements a specific Specification-type that uses a lambda expression to define the desired specification.</summary>
  /// <typeparam name="T">The type of object to which the specification applies.</typeparam>
  internal class LambdaSpecification<T> : Specification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="LambdaSpecification{T}"/> class.</summary>
    /// <param name="function">The function that equals the search pattern that is implemented by the type.</param>
    internal LambdaSpecification(Expression<Func<T, bool>> function) {
      this.Expression = function;
    }
    #endregion

    #region Public properties
    /// <summary>Gets the function that equals the search-pattern that is implemented by this type.</summary>
    public Expression<Func<T, bool>> Expression { get; private set; }
    #endregion

    #region Specification-overrides
    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into an expression that can 
    /// be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    protected override Expression<Func<T, bool>> VisitCore(ISpecificationVisitor<T> visitor) {
      return visitor.CreateLambdaExpression(this.Expression);
    }
    #endregion
  }
}
