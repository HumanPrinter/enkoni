//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="NotSpecification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines a Specification class that uses an inverted lambda-expression to define the desired specification.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;

namespace OscarBrouwer.Framework {
  /// <summary>This class implements a specific Specification-type that uses a lambda expression to define the desired 
  /// specification. The expression must not be true in order for it to pass this specification.</summary>
  /// <typeparam name="T">The type of object to which the specification applies.</typeparam>
  internal class NotSpecification<T> : LambdaSpecification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="NotSpecification{T}"/> class that will always return
    /// <see langword="false"/>.</summary>
    internal NotSpecification()
      : base(new Func<T, bool>(t => false)) {
    }

    /// <summary>Initializes a new instance of the <see cref="NotSpecification{T}"/> class.</summary>
    /// <param name="function">The function that equals the search pattern that is implemented by the type.</param>
    internal NotSpecification(Func<T, bool> function)
      : base(t => !function(t)) {
    }
    #endregion
  }
}
