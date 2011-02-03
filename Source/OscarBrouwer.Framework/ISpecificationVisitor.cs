//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="ISpecificationVisitor.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines the basic specification-visitor API.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;

namespace OscarBrouwer.Framework {
  /// <summary>This interface describes the public API of a visitor class that can be used to visit a specification using
  /// the visitor Design Pattern.</summary>
  /// <typeparam name="T">The type of object that is ultimatilly selected using the specification.</typeparam>
  public interface ISpecificationVisitor<T> {
    /// <summary>Creates a lambda-expression using the specified expression. Typically, this method simply returns the 
    /// parameter.</summary>
    /// <param name="expression">The expression that was originally passed to the specification.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> CreateLambdaExpression(Expression<Func<T, bool>> expression);

    /// <summary>Creates a NOT-expression using the specified specification.</summary>
    /// <param name="specification">The specification whose result must be inverted.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> CreateNotExpression(ISpecification<T> specification);

    /// <summary>Creates a LIKE-expression using the specified field and searchpattern.</summary>
    /// <param name="field">The field of type <c>T</c> that must match the pattern.</param>
    /// <param name="pattern">The pattern to which the field must apply. The pattern may contain a '*' and '?' wildcard.
    /// </param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> CreateLikeExpression(Expression<Func<T, string>> field, string pattern);

    /// <summary>Creates an AND-expression using the two specified specifications.</summary>
    /// <param name="leftOperand">The left operand of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> CreateAndExpression(ISpecification<T> leftOperand, ISpecification<T> rightOperand);

    /// <summary>Creates an OR-expression using the two specified specifications.</summary>
    /// <param name="leftOperand">The left operand of the combination.</param>
    /// <param name="rightOperand">The right operand of the combination.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> CreateOrExpression(ISpecification<T> leftOperand, ISpecification<T> rightOperand);

    /// <summary>Creates an expression using the custom specification. This method is executed when a 
    /// specification-type is used that is not part of the default specification system.</summary>
    /// <param name="specification">The custom specification.</param>
    /// <returns>The created expression.</returns>
    Expression<Func<T, bool>> CreateCustomExpression(ISpecification<T> specification);
  }
}
