//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeSpanRangeValidatorAttribute.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Contains an attribute for the validation capabilities.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Enkoni.Framework.Validation.Validators {
  /// <summary>Attribute to specify timespan range validation on a property, method or field.</summary>
  [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true,
    Inherited = false)]
  public sealed class TimeSpanRangeValidatorAttribute : ValueValidatorAttribute {
    #region Instance variables
    /// <summary>The lower bound.</summary>
    private TimeSpan lowerBound;

    /// <summary>The type of the lower bound.</summary>
    private RangeBoundaryType lowerBoundType;

    /// <summary>The upper bound.</summary>
    private TimeSpan upperBound;

    /// <summary>The type of the upper bound.</summary>
    private RangeBoundaryType upperBoundType;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidatorAttribute"/> class with lower and upper bounds, and bound types.
    /// </summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <param name="lowerBoundType">The bound type for the lower bound.</param>
    /// <param name="upperBound">The upper bound.</param>
    /// <param name="upperBoundType">The bound type for the upper bound.</param>
    public TimeSpanRangeValidatorAttribute(TimeSpan lowerBound, RangeBoundaryType lowerBoundType, TimeSpan upperBound,
      RangeBoundaryType upperBoundType)
      : base() {
      this.lowerBound = lowerBound;
      this.lowerBoundType = lowerBoundType;
      this.upperBound = upperBound;
      this.upperBoundType = upperBoundType;
    }

    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidatorAttribute"/> class with lower and upper bounds, and bound types.
    /// </summary>
    /// <param name="lowerBound">An ISO8601 formatted string representing the lower bound, like "00:00:00".</param>
    /// <param name="lowerBoundType">The bound type for the lower bound.</param>
    /// <param name="upperBound">An ISO8601 formatted string representing the upper bound, like "00:00:00".</param>
    /// <param name="upperBoundType">The bound type for the upper bound.</param>
    public TimeSpanRangeValidatorAttribute(string lowerBound, RangeBoundaryType lowerBoundType, string upperBound,
      RangeBoundaryType upperBoundType)
      : this(TimeSpan.Parse(lowerBound), lowerBoundType, TimeSpan.Parse(upperBound), upperBoundType) {
    }
    #endregion

    #region Properties
    /// <summary>Gets the lower bound of the range validator.</summary>
    public TimeSpan LowerBound {
      get { return this.lowerBound; }
    }

    /// <summary>Gets the lower bound type of the range validator.</summary>
    public RangeBoundaryType LowerBoundType {
      get { return this.lowerBoundType; }
    }

    /// <summary>Gets the upper bound of the range validator.</summary>
    public TimeSpan UpperBound {
      get { return this.upperBound; }
    }

    /// <summary>Gets the upper bound type of the range validator.</summary>
    public RangeBoundaryType UpperBoundType {
      get { return this.upperBoundType; }
    }
    #endregion

    #region ValueValidatorAttribute overrides
    /// <summary>Creates the <see cref="TimeSpanRangeValidator"/> described by the configuration object.</summary>
    /// <param name="targetType">The type of object that will be validated by the validator.</param>
    /// <returns>The created Validator.</returns>
    protected override Validator DoCreateValidator(Type targetType) {
      return new TimeSpanRangeValidator(this.lowerBound, this.lowerBoundType, this.upperBound, this.upperBoundType,
        this.MessageTemplate, this.Negated);
    }
    #endregion
  }
}
