//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="OrSpecification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines a Specificationtype that combines two specifications using an OR operation.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;

namespace OscarBrouwer.Framework {
  /// <summary>This class contains a specificationtype that combines two specifications using an OR operation.</summary>
  /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
  internal class OrSpecification<T> : CombinedSpecification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="OrSpecification{T}"/> class.</summary>
    /// <param name="leftOperand">The left operant of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    internal OrSpecification(ISpecification<T> leftOperand, ISpecification<T> rightOperand)
      : base(leftOperand, rightOperand) {
    }
    #endregion

    #region Specification overrides
    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into
    /// an expression that can be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    protected override Func<T, bool> VisitCore(ISpecificationVisitor<T> visitor) {
      return visitor.CreateOrExpression(this.LeftOperand, this.RightOperand);
    }
    #endregion
  }
}
