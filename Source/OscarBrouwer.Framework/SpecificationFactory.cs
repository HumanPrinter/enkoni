//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="SpecificationFactory.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Holds a class that is capable of creating Specification-instances.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Text.RegularExpressions;

namespace OscarBrouwer.Framework {
  /// <summary>This factory provides the functionality to create single specifications.</summary>
  /// <typeparam name="T">The type to which the specification applies.</typeparam>
  public class SpecificationFactory<T> {
    /// <summary>Creates an empty specification that will always return <see langword="true"/>.</summary>
    /// <returns>The created specification.</returns>
    public virtual ISpecification<T> Empty() {
      return new Specification<T>();
    }

    /// <summary>Creates an 'Equals' specification that can be used to test if the field of an object equals the 
    /// specified value.</summary>
    /// <typeparam name="TField">The type of the field that must be tested.</typeparam>
    /// <param name="field">The field that must be tested.</param>
    /// <param name="value">The value to test for.</param>
    /// <returns>The created specification.</returns>
    public virtual ISpecification<T> Equals<TField>(Func<T, TField> field, TField value) {
      if(typeof(TField) == typeof(string)) {
        Func<T, string> tempFunc = field as Func<T, string>;
        object tempValue = value;

        return new Specification<T>(new Func<T, bool>(t => tempFunc(t).Equals((string)tempValue, StringComparison.OrdinalIgnoreCase)));
      }
      else {
        return new Specification<T>(new Func<T, bool>(t => field(t).Equals(value)));
      }
    }

    /// <summary>Creates a 'Like' specification that can be used to test if the field of an object matches the 
    /// specified pattern.</summary>
    /// <param name="field">The field that must be tested.</param>
    /// <param name="pattern">The pattern to test for.</param>
    /// <returns>The created specification.</returns>
    public virtual ISpecification<T> Like(Func<T, string> field, string pattern) {
      if(string.IsNullOrEmpty(pattern)) {
        throw new ArgumentException("The pattern cannot be null or empty", "pattern");
      }

      /* We allow '*' to indicate any character and '?' to indicate one character  */
      pattern = pattern.Replace("*", ".*");
      pattern = pattern.Replace("?", ".?");
      return new Specification<T>(new Func<T, bool>(t => new Regex(pattern).IsMatch(field(t))));
    }
  }
}
