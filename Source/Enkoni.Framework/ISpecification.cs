using System;
using System.Linq.Expressions;

namespace Enkoni.Framework {
  /// <summary>This interface describes the public API of the classes that make up the specification subsystem that is implemented using the 
  /// Specification Pattern.</summary>
  /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
  public interface ISpecification<T> {
    #region Events
    /// <summary>Occurs when the maximum number of records has changed.</summary>
    event EventHandler<EventArgs<int>> MaximumResultsUpdated;

    /// <summary>Occurs when the sorting rules have changed.</summary>
    event EventHandler<SortSpecificationsEventArgs<T>> SortRulesUpdated;
    #endregion

    #region Properties
    /// <summary>Gets the maximum number of results that must be returned by the specification.</summary>
    int MaximumResults { get; }

    /// <summary>Gets the sorting rules.</summary>
    SortSpecifications<T> SortRules { get; }
    #endregion

    #region Methods
    /// <summary>Creates an 'And' specification that can be used to combine two specifications and compare them using the '&amp;&amp;' operation.
    /// </summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    ISpecification<T> And(ISpecification<T> specification);

    /// <summary>Creates an 'Or' specification that can be used to combine two specifications and compare them using the '||' operation.</summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    ISpecification<T> Or(ISpecification<T> specification);

    /// <summary>Sets the maximum number of records that must be retrieved using the specification.</summary>
    /// <param name="maximum">The maximum number. A value of '-1' means 'retrieve all'.</param>
    void SetMaximumResults(int maximum);

    /// <summary>Specifies the way the sequence must be sorted. It used a default sort order of ascending.</summary>
    /// <typeparam name="TKey">The type of object that must be used to perform the sorting.</typeparam>
    /// <param name="keySelector">The expression that points to the field that must be used to perform the sorting.</param>
    /// <returns>The specification with the sorting rules.</returns>
    ISpecification<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector);

    /// <summary>Specifies the way the sequence must be sorted.</summary>
    /// <typeparam name="TKey">The type of object that must be used to perform the sorting.</typeparam>
    /// <param name="keySelector">The expression that points to the field that must be used to perform the sorting.</param>
    /// <param name="direction">The direction that the ordering must take place in.</param>
    /// <returns>The specification with the sorting rules.</returns>
    ISpecification<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector, SortOrder direction);

    /// <summary>Specifies the way the sequence must be sorted. It used a default sort order of descending.</summary>
    /// <typeparam name="TKey">The type of object that must be used to perform the sorting.</typeparam>
    /// <param name="keySelector">The expression that points to the field that must be used to perform the sorting.</param>
    /// <returns>The specification with the sorting rules.</returns>
    ISpecification<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector);

    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into an expression that can 
    /// be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    Expression<Func<T, bool>> Visit(ISpecificationVisitor<T> visitor);
    #endregion
  }
}
