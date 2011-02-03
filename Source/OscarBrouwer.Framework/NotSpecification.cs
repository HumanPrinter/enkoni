//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="NotSpecification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines a Specification class that uses an inverted lambda-expression to define the desired specification.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace OscarBrouwer.Framework {
  /// <summary>This class implements a specific Specification-type that uses a lambda expression to define the desired 
  /// specification. The expression must not be true in order for it to pass this specification.</summary>
  /// <typeparam name="T">The type of object to which the specification applies.</typeparam>
  internal class NotSpecification<T> : Specification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="NotSpecification{T}"/> class.</summary>
    /// <param name="function">The function that equals the search pattern that is implemented by the type.</param>
    internal NotSpecification(Expression<Func<T, bool>> function) {
      this.Specification = OscarBrouwer.Framework.Specification.Lambda(function);
    }

    /// <summary>Initializes a new instance of the <see cref="NotSpecification{T}"/> class.</summary>
    /// <param name="specification">The specification that must not be met.</param>
    internal NotSpecification(ISpecification<T> specification) {
      if(specification == null) {
        throw new ArgumentNullException("specification");
      }

      specification.MaximumResultsUpdated += this.HandleMaximumResultsUpdated;
      this.Specification = specification;
    }
    #endregion

    #region Public properties
    /// <summary>Gets the specification whose result must be negated.</summary>
    public ISpecification<T> Specification { get; private set; }
    #endregion

    #region Specification<T> overrides
    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into
    /// an expression that can be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    protected override Expression<Func<T, bool>> VisitCore(ISpecificationVisitor<T> visitor) {
      return visitor.CreateNotExpression(this.Specification);
    }
    #endregion
  }
}
