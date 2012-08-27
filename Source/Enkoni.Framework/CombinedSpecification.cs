//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="CombinedSpecification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines an abstract specification type that contains to specifications that must be combined.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

namespace Enkoni.Framework {
  /// <summary>This abstract class defines the basic API of a specification-type that combines two specifications.</summary>
  /// <typeparam name="T">The type of object that is ultimatilly selected by the specification.</typeparam>
  public abstract class CombinedSpecification<T> : Specification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="CombinedSpecification{T}"/> class.</summary>
    /// <param name="leftOperand">The left operand of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    protected CombinedSpecification(ISpecification<T> leftOperand, ISpecification<T> rightOperand) {
      if(leftOperand == null) {
        throw new ArgumentNullException("leftOperand");
      }

      if(rightOperand == null) {
        throw new ArgumentNullException("rightOperand");
      }

      leftOperand.MaximumResultsUpdated += this.HandleMaximumResultsUpdated;
      rightOperand.MaximumResultsUpdated += this.HandleMaximumResultsUpdated;

      this.LeftOperand = leftOperand;
      this.RightOperand = rightOperand;
    }
    #endregion

    #region Public properties
    /// <summary>Gets the left operand of the combination.</summary>
    public ISpecification<T> LeftOperand { get; private set; }

    /// <summary>Gets the right operand of the combination.</summary>
    public ISpecification<T> RightOperand { get; private set; }
    #endregion
  }
}
