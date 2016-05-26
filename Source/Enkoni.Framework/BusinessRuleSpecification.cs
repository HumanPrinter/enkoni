using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Enkoni.Framework {
  /// <summary>This class implements a specific Specification type that holds information about a special business rule.</summary>
  /// <typeparam name="T">The type of object to which the specification applies.</typeparam>
  public class BusinessRuleSpecification<T> : Specification<T> {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="BusinessRuleSpecification{T}"/> class.</summary>
    /// <param name="ruleName">The name of the rule that must be executed.</param>
    /// <param name="arguments">The arguments that must be used by the business rule.</param>
    internal BusinessRuleSpecification(string ruleName, params object[] arguments) {
      Guard.ArgumentIsNotNullOrEmpty(ruleName, nameof(ruleName), "Specify a valid rule name");
      
      this.RuleName = ruleName;
      this.RuleArguments = new ReadOnlyCollection<object>(arguments);
    }
    #endregion

    #region Public properties
    /// <summary>Gets the name of the business rule that must be executed.</summary>
    public string RuleName { get; private set; }

    /// <summary>Gets any arguments that were passed and may be required to execute the business rule.</summary>
    public ReadOnlyCollection<object> RuleArguments { get; private set; }
    #endregion

    #region Specification-overrides
    /// <summary>Visits the specification and lets <paramref name="visitor"/> convert the contents of the specification into an expression that can 
    /// be used to perform the actual filtering/selection.</summary>
    /// <param name="visitor">The instance that will perform the conversion.</param>
    /// <returns>The expression that was created using this specification.</returns>
    protected override Expression<Func<T, bool>> VisitCore(ISpecificationVisitor<T> visitor) {
      string message = "A business rule cannot be converted to a lambda-expression. " +
        "This exception is normally caused by a fault in the Repository-class.";
      throw new InvalidOperationException(message);
    }
    #endregion
  }
}
