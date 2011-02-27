//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Specification.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Defines the standard Specification class.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Enkoni.Framework {
  /// <summary>This class contains static members that would normally be part of the <see cref="Specification{T}"/> class, but since that class is 
  /// generic the static members are placed in this non-generic counterpart to avoid possible confussion about the use of the methods.</summary>
  public static class Specification {
    #region Public static method
    /// <summary>Creates a specification that will return all available objects.</summary>
    /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
    /// <returns>A specification that will return all available objects.</returns>
    public static ISpecification<T> All<T>() {
      return new LambdaSpecification<T>(t => true);
    }

    /// <summary>Creates a specification that will return none of the available objects.</summary>
    /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
    /// <returns>A specification that will return none of the available objects.</returns>
    public static ISpecification<T> None<T>() {
      return new NotSpecification<T>(t => true);
    }

    /// <summary>Creates a specification that will return the objects that match the specified expression.</summary>
    /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
    /// <param name="expression">The expression that acts as a filter.</param>
    /// <returns>A specification that will only return the objects that match the expression.</returns>
    public static ISpecification<T> Lambda<T>(Expression<Func<T, bool>> expression) {
      return new LambdaSpecification<T>(expression);
    }

    /// <summary>Creates a specification that will return the objects for which the specified field matches the specified pattern. The pattern 
    /// supports two types of wildcards. The '*' wildcard matches any character (zero or more times) and the '?' wildcard matches exactly one 
    /// character.</summary>
    /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
    /// <param name="field">The field that must match the expression.</param>
    /// <param name="pattern">The search-pattern.</param>
    /// <returns>A specification that will return only the objects for which the field matches the search-pattern.</returns>
    public static ISpecification<T> Like<T>(Expression<Func<T, string>> field, string pattern) {
      return new LikeSpecification<T>(field, pattern);
    }

    /// <summary>Creates a specification that will return the objects that do not match the specified expression.</summary>
    /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
    /// <param name="expression">The expression that acts as a filter.</param>
    /// <returns>A specification that will only return the objects that do not match the expression.</returns>
    public static ISpecification<T> Not<T>(Expression<Func<T, bool>> expression) {
      return new NotSpecification<T>(expression);
    }

    /// <summary>Creates a specification that will return the objects that do not match the specified expression.</summary>
    /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
    /// <param name="specification">The specification that acts as a filter.</param>
    /// <returns>A specification that will only return the objects that do not match the specification.</returns>
    public static ISpecification<T> Not<T>(ISpecification<T> specification) {
      return new NotSpecification<T>(specification);
    }

    /// <summary>Creates a specification that holds information about a businessrule that must be executed.</summary>
    /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
    /// <param name="ruleName">The name of the rule that must be executed.</param>
    /// <param name="ruleArguments">The arguments that must be used by the businessrule.</param>
    /// <returns>A specification that holds information about a businessrule.</returns>
    public static ISpecification<T> BusinessRule<T>(string ruleName, params object[] ruleArguments) {
      return new BusinessRuleSpecification<T>(ruleName, ruleArguments);
    }
    #endregion
  }

  /// <summary>This class contains the basic functionality for any specification-class.</summary>
  /// <typeparam name="T">The type of object that is ultimately selected by the specification.</typeparam>
  [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
      Justification = "Since the static class is merely a container for the static members of the non-static class, they can be in the same file")]
  public abstract class Specification<T> : ISpecification<T> {
    #region Private event-delegates
    /// <summary>The delegate that holds the references to the various eventhandlers. Normally, there will be at most one handler.</summary>
    private EventHandler<EventArgs<int>> maxResultsUpdated;

    /// <summary>Indicates if there is a change-event pending.</summary>
    private bool maxResultsChangePending;

    /// <summary>The delegate that holds the references to the various eventhandlers. Normally, there will be at most one handler.</summary>
    private EventHandler<SortSpecificationsEventArgs<T>> sortRulesUpdated;

    /// <summary>Indicates if there is a change-event pending.</summary>
    private bool sortRulesChangePending;
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="Specification{T}"/> class.</summary>
    protected Specification() {
      this.MaximumResults = -1;
    }
    #endregion   

    #region Public events
    /// <summary>Occurs when the maximum number of records has changed.</summary>
    public event EventHandler<EventArgs<int>> MaximumResultsUpdated {
      add { 
        this.maxResultsUpdated += value;
        if(this.maxResultsChangePending) {
          this.maxResultsUpdated(this, new EventArgs<int>(this.MaximumResults));
          this.maxResultsChangePending = false;
        }
      }

      remove { 
        this.maxResultsUpdated -= value; 
      }
    }

    /// <summary>Occurs when the sortrules have been updated.</summary>
    public event EventHandler<SortSpecificationsEventArgs<T>> SortRulesUpdated {
      add {
        this.sortRulesUpdated += value;
        if(this.sortRulesChangePending) {
          this.sortRulesUpdated(this, new SortSpecificationsEventArgs<T>(this.SortRules));
          this.SortRules.Clear();
          this.sortRulesChangePending = false;
        }
      }

      remove {
        this.sortRulesUpdated -= value;
      }
    }
    #endregion

    #region Public properties
    /// <summary>Gets the maximum number of records that must be retrieved using this specification.</summary>
    public int MaximumResults { get; private set; }

    /// <summary>Gets the sorting rules that are specified.</summary>
    public SortSpecifications<T> SortRules { get; private set; }
    #endregion

    #region Public methods
    /// <summary>Sets the maximum number of records that must be retrieved using the specification.</summary>
    /// <param name="maximum">The maximum number. A value of '-1' means 'retrieve all'.</param>
    public void SetMaximumResults(int maximum) {
      if(this.maxResultsUpdated != null) {
        this.maxResultsUpdated(this, new EventArgs<int>(maximum));
      }
      else {
        this.MaximumResults = maximum;
        this.maxResultsChangePending = true;
      }
    }

    /// <summary>Specifies the way the sequence must be sorted. It used a default sortorder of ascending.</summary>
    /// <typeparam name="TKey">The type of object that must be used to perform the sorting.</typeparam>
    /// <param name="keySelector">The expression that points to the field that must be used to perform the sorting.</param>
    /// <returns>The specification with the sortingrules.</returns>
    public ISpecification<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector) {
      return this.OrderBy(keySelector, SortOrder.Ascending);
    }

    /// <summary>Specifies the way the sequence must be sorted. It used a default sortorder of descending.</summary>
    /// <typeparam name="TKey">The type of object that must be used to perform the sorting.</typeparam>
    /// <param name="keySelector">The expression that points to the field that must be used to perform the sorting.</param>
    /// <returns>The specification with the sortingrules.</returns>
    public ISpecification<T> OrderByDescending<TKey>(Expression<Func<T, TKey>> keySelector) {
      return this.OrderBy(keySelector, SortOrder.Descending);
    }

    /// <summary>Specifies the way the sequence must be sorted.</summary>
    /// <typeparam name="TKey">The type of object that must be used to perform the sorting.</typeparam>
    /// <param name="keySelector">The expression that points to the field that must be used to perform the sorting.</param>
    /// <param name="direction">The direction that the ordering must take place in.</param>
    /// <returns>The specification with the sortingrules.</returns>
    public ISpecification<T> OrderBy<TKey>(Expression<Func<T, TKey>> keySelector, SortOrder direction) {
      SortSpecification<T, TKey> spec = new SortSpecification<T, TKey>(keySelector, direction);
      SortSpecifications<T> sortSpecifications = new SortSpecifications<T>();
      sortSpecifications.Add(spec);
      return this.OrderBy(sortSpecifications);
    }

    /// <summary>Specifies the way the sequence must be sorted.</summary>
    /// <param name="sortSpecifications">The specifications that define the way the sequence must be sorted.</param>
    /// <returns>The specification with the sortingrules.</returns>
    public ISpecification<T> OrderBy(SortSpecifications<T> sortSpecifications) {
      if(this.sortRulesUpdated != null) {
        this.sortRulesUpdated(this, new SortSpecificationsEventArgs<T>(sortSpecifications));
      }
      else {
        if(this.SortRules == null) {
          this.SortRules = new SortSpecifications<T>();
        }

        this.SortRules.AddRange(sortSpecifications);
        this.sortRulesChangePending = true;
      }

      return this;
    }

    /// <summary>Creates an 'And' specification that can be used to combine two specifications and compare them using the '&amp;&amp;' operation.
    /// </summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    public virtual ISpecification<T> And(ISpecification<T> specification) {
      if(specification is BusinessRuleSpecification<T>) {
        throw new InvalidOperationException("A BusinessRuleSpecification cannot be combined with other specifications.");
      }

      return new AndSpecification<T>(this, specification);
    }

    /// <summary>Creates an 'Or' specification that can be used to combine two specifications and compare them using the '||' operation.</summary>
    /// <param name="specification">The specification that must be combined.</param>
    /// <returns>The combined specification.</returns>
    public virtual ISpecification<T> Or(ISpecification<T> specification) {
      if(specification is BusinessRuleSpecification<T>) {
        throw new InvalidOperationException("A BusinessRuleSpecification cannot be combined with other specifications.");
      }

      return new OrSpecification<T>(this, specification);
    }

    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into an expression that can 
    /// be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    /// <exception cref="ArgumentNullException">Parameter is <see langword="null"/>.</exception>
    public Expression<Func<T, bool>> Visit(ISpecificationVisitor<T> visitor) {
      if(visitor == null) {
        throw new ArgumentNullException("visitor", "The visitor-parameter is mandatory");
      }

      return this.VisitCore(visitor);
    }
    #endregion

    #region Protected eventhandlers
    /// <summary>Handles the occurence of a changed maximum for the number of records that must be retrieved.</summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="args">Some additional information regarding the event.</param>
    protected void HandleMaximumResultsUpdated(object sender, EventArgs<int> args) {
      if(args == null) {
        throw new ArgumentNullException("args");
      }

      this.SetMaximumResults(args.EventValue);
    }

    /// <summary>Handles the occurence of a changed set of sorting rules.</summary>
    /// <param name="sender">The object that raised the event.</param>
    /// <param name="args">Some additional information regarding the event.</param>
    protected void HandleOrderByRulesUpdated(object sender, SortSpecificationsEventArgs<T> args) {
      if(args == null) {
        throw new ArgumentNullException("args");
      }

      this.OrderBy(args.EventValue);
    }
    #endregion

    #region Extendibility methods
    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into an expression that can 
    /// be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    protected abstract Expression<Func<T, bool>> VisitCore(ISpecificationVisitor<T> visitor);
    #endregion
  }
}
