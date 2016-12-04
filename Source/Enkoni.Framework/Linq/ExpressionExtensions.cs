using System;
using System.Linq.Expressions;

namespace Enkoni.Framework.Linq {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class ExpressionExtensions {
    /// <summary>Creates an <see cref="Expression{TDelegate}"/> that inverts the result of <paramref name="source"/>.</summary>
    /// <typeparam name="T">The type that is used as input for the expression.</typeparam>
    /// <param name="source">An <see cref="Expression{TDelegate}"/> whose result must be inverted.</param>
    /// <returns>An expression that will invert the result of <paramref name="source"/>.</returns>
    public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> source) {
      Guard.ArgumentIsNotNull(source, nameof(source));

      return Expression.Lambda<Func<T, bool>>(Expression.Not(source.Body), source.Parameters[0]);
    }
  }
}
