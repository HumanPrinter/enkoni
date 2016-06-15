using System;
using System.Linq.Expressions;

namespace Enkoni.Framework {
  /// <summary>This class contains a specification type that combines two specifications using an AND operation.</summary>
  /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
  internal class AndSpecification<T> : CombinedSpecification<T> {
    #region Constructor

    /// <summary>Initializes a new instance of the <see cref="AndSpecification{T}"/> class.</summary>
    /// <param name="leftOperand">The left operant of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    internal AndSpecification(ISpecification<T> leftOperand, ISpecification<T> rightOperand)
      : base(leftOperand, rightOperand) {
    }

    #endregion

    #region Specification-overrides

    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into an expression that can
    /// be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    protected override Expression<Func<T, bool>> VisitCore(ISpecificationVisitor<T> visitor) {
      return visitor.CreateAndExpression(this.LeftOperand, this.RightOperand);
    }

    #endregion
  }
}
