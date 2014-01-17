//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeSpanRangeValidator.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Contains an helper class for the validation capabilities.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Enkoni.Framework.Validation.Validators {
  /// <summary>Performs validation on TimeSpan instances by comparing them to the specified boundaries.</summary>
  public class TimeSpanRangeValidator : RangeValidator<TimeSpan> {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidator"/> class with an upper bound constraint.</summary>
    /// <param name="upperBound">The upper bound.</param>
    /// <remarks>No lower bound constraints will be checked by this instance, and the upper bound check will be 
    /// <see cref="RangeBoundaryType.Inclusive"/>.</remarks>
    public TimeSpanRangeValidator(TimeSpan upperBound)
      : base(upperBound) {
    }

    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidator"/> class with an upper bound constraint.</summary>
    /// <param name="upperBound">The upper bound.</param>
    /// <param name="negated">True if the validator must negate the result of the validation.</param>
    /// <remarks>No lower bound constraints will be checked by this instance, and the upper bound check will be 
    /// <see cref="RangeBoundaryType.Inclusive"/>.</remarks>
    public TimeSpanRangeValidator(TimeSpan upperBound, bool negated)
      : base(upperBound, negated) {
    }

    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidator"/> class with lower and upper bound constraints.</summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <param name="upperBound">The upper bound.</param>
    /// <remarks>Both bound checks will be <see cref="RangeBoundaryType.Inclusive"/>.</remarks>
    public TimeSpanRangeValidator(TimeSpan lowerBound, TimeSpan upperBound)
      : base(lowerBound, upperBound) {
    }

    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidator"/> class with lower and upper bound constraints.</summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <param name="upperBound">The upper bound.</param>
    /// <param name="negated">True if the validator must negate the result of the validation.</param>
    /// <remarks>Both bound checks will be <see cref="RangeBoundaryType.Inclusive"/>.</remarks>
    public TimeSpanRangeValidator(TimeSpan lowerBound, TimeSpan upperBound, bool negated)
      : base(lowerBound, upperBound, negated) {
    }

    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidator"/> class with fully specified bound constraints.</summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
    /// <param name="upperBound">The upper bound.</param>
    /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
    public TimeSpanRangeValidator(TimeSpan lowerBound, RangeBoundaryType lowerBoundType, TimeSpan upperBound, RangeBoundaryType upperBoundType)
      : base(lowerBound, lowerBoundType, upperBound, upperBoundType) {
    }

    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidator"/> class with fully specified bound constraints.</summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
    /// <param name="upperBound">The upper bound.</param>
    /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
    /// <param name="negated">True if the validator must negate the result of the validation.</param>
    public TimeSpanRangeValidator(TimeSpan lowerBound, RangeBoundaryType lowerBoundType, TimeSpan upperBound, RangeBoundaryType upperBoundType,
      bool negated)
      : base(lowerBound, lowerBoundType, upperBound, upperBoundType, negated) {
    }

    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidator"/> class with fully specified bound constraints and a message 
    /// template.</summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
    /// <param name="upperBound">The upper bound.</param>
    /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
    /// <param name="messageTemplate">The message template to use when logging results.</param>
    public TimeSpanRangeValidator(TimeSpan lowerBound, RangeBoundaryType lowerBoundType, TimeSpan upperBound, RangeBoundaryType upperBoundType,
      string messageTemplate)
      : base(lowerBound, lowerBoundType, upperBound, upperBoundType, messageTemplate) {
    }

    /// <summary>Initializes a new instance of the <see cref="TimeSpanRangeValidator"/> class with fully specified bound constraints and a message 
    /// template.</summary>
    /// <param name="lowerBound">The lower bound.</param>
    /// <param name="lowerBoundType">The indication of how to perform the lower bound check.</param>
    /// <param name="upperBound">The upper bound.</param>
    /// <param name="upperBoundType">The indication of how to perform the upper bound check.</param>
    /// <param name="messageTemplate">The message template to use when logging results.</param>
    /// <param name="negated">True if the validator must negate the result of the validation.</param>
    public TimeSpanRangeValidator(TimeSpan lowerBound, RangeBoundaryType lowerBoundType, TimeSpan upperBound, RangeBoundaryType upperBoundType,
      string messageTemplate, bool negated)
      : base(lowerBound, lowerBoundType, upperBound, upperBoundType, messageTemplate, negated) {
    }
    #endregion
  }
}
