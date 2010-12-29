//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="Specification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines the standard Specification class.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;

namespace OscarBrouwer.Framework {
  /// <summary>This class contains the basic functionality for any specification-class.</summary>
  /// <typeparam name="T">The type of object to which the specification applies.</typeparam>
  public class Specification<T> : ISpecification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="Specification{T}"/> class that will always return
    /// <see langword="true"/>.</summary>
    public Specification()
      : this(new Func<T, bool>(t => true)) {
    }

    /// <summary>Initializes a new instance of the <see cref="Specification{T}"/> class.</summary>
    /// <param name="function">The function that equals the search pattern that is implemented by the type.</param>
    public Specification(Func<T, bool> function) {
      this.IsSatisfiedBy = function;
    }
    #endregion

    #region Public properties
    /// <summary>Gets the function that equals the search-pattern that is implemented by this type.</summary>
    public Func<T, bool> IsSatisfiedBy { get; private set; }
    #endregion

    #region Public methods
    /// <summary>Creates an 'And' specification that can be used to combine two specifications and compare them using
    /// the '&amp;&amp;' operation.</summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    public virtual ISpecification<T> And(ISpecification<T> specification) {
      return new Specification<T>(new Func<T, bool>(t => this.IsSatisfiedBy(t) && specification.IsSatisfiedBy(t)));
    }

    /// <summary>Creates an 'Or' specification that can be used to combine two specifications and compare them using
    /// the '||' operation.</summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    public virtual ISpecification<T> Or(ISpecification<T> specification) {
      return new Specification<T>(new Func<T, bool>(t => this.IsSatisfiedBy(t) || specification.IsSatisfiedBy(t)));
    }

    /// <summary>Creates a 'Not' specification that can be used to inverse the result of the specification.</summary>
    /// <returns>The combined specification.</returns>
    public virtual ISpecification<T> Not() {
      return new Specification<T>(new Func<T, bool>(t => !this.IsSatisfiedBy(t)));
    }
    #endregion
  }
}
