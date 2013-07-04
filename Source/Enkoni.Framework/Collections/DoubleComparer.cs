//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleComparer.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Holds a comparer-class that is capable of comparing two doubles.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Enkoni.Framework.Collections {
  /// <summary>This class compares two <see cref="Double"/> values a more mathematically accepted approach.</summary>
  public class DoubleComparer : IComparer<double> {
    #region Instance variables
    /// <summary>The option that determines the compare method.</summary>
    private DoubleCompareOption compareOption;

    /// <summary>The factor that is used in the comparison.</summary>
    private double compareFactor;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DoubleComparer"/> class.</summary>
    /// <param name="comparisonFactor">The factor that must be taken into account. If <paramref name="compareOption"/> is set to 
    /// <see cref="DoubleCompareOption.Margin"/>, the comparison factor will be treated as an absolute margin. If <paramref name="compareOption"/> is
    /// set to <see cref="DoubleCompareOption.SignificantDigits"/> the comparison factor will be treated as the number of digits that must be 
    /// examined will comparing. Note that the comparison factor in that case will be truncated to an integer.</param>
    /// <param name="compareOption">Defines the method that must be used to compare the double values.</param>
    public DoubleComparer(double comparisonFactor, DoubleCompareOption compareOption) {
      this.compareOption = compareOption;
      this.compareFactor = comparisonFactor;
    }
    #endregion

    #region Methods
    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>A signed number indicating the relative values of the two numbers. <br />
    /// Return Value Description <br/>
    /// Less than zero: <paramref name="x"/> is less than <paramref name="y"/> -or- <paramref name="x"/> is not a number (<see cref="Double.NaN"/>) 
    /// and <paramref name="y"/> is a number.<br/>
    /// Zero: <paramref name="x"/> is equal to <paramref name="y"/> -or- Both <paramref name="x"/> and <paramref name="y"/> are not a number 
    /// (<see cref="Double.NaN"/>), <see cref="Double.PositiveInfinity"/>, or <see cref="Double.NegativeInfinity"/>.<br/>
    /// Greater than zero: <paramref name="x"/> is greater than <paramref name="y"/> -or- <paramref name="x"/> is a number and <paramref name="y"/> is
    /// not a number (<see cref="Double.NaN"/>).</returns>
    public int Compare(double x, double y) {
      /* Follow the same contract as double.CompareTo(double) */
      if(double.IsNaN(x) && !double.IsNaN(y)) {
        return -1;
      }

      if(!double.IsNaN(x) && double.IsNaN(y)) {
        return 1;
      }

      if(double.IsNaN(x) && double.IsNaN(y)) {
        return 0;
      }

      if(double.IsPositiveInfinity(x) && double.IsPositiveInfinity(y)) {
        return 0;
      }

      if(double.IsNegativeInfinity(x) && double.IsNegativeInfinity(y)) {
        return 0;
      }

      /* If both values are exactly the same, no need to look any further */
      if(x == y) {
        return 0;
      }

      switch(this.compareOption) {
        case DoubleCompareOption.Margin:
          return CompareByMargin(x, y, this.compareFactor);
        case DoubleCompareOption.SignificantDigits:
          return CompareBySignificantDigits(x, y, (int)this.compareFactor);
        default:
          /* If all else fails, fall back to the default path */
          return x.CompareTo(y);
      }
    }
    #endregion

    #region Private helper methods
    /// <summary>Compares two doubles by looking if the difference is within the specified margin. If so, the two values are considered equal. 
    /// Otherwise, the values are compared in the traditional way.</summary>
    /// <param name="x">The left operand.</param>
    /// <param name="y">The right operand.</param>
    /// <param name="margin">The margin that must be taken into account.</param>
    /// <returns>Zero if the difference between the two numbers is within the margin, less than zero if the first operand is less than the second 
    /// operand, more than zero if the first operand is more than the second operand.</returns>
    private static int CompareByMargin(double x, double y, double margin) {
      double maxOperand = Math.Max(x, y);
      double minOperand = Math.Min(x, y);

      double difference = maxOperand - minOperand;
      if(difference < Math.Abs(margin)) {
        return 0;
      }

      return x.CompareTo(y);
    }

    /// <summary>Compares two doubles by looking only at the significant digits.</summary>
    /// <param name="x">The left operand.</param>
    /// <param name="y">The right operand.</param>
    /// <param name="digits">The siginificant digits that must be taken into account.</param>
    /// <returns>An integer indicating the difference between the two numbers.</returns>
    private static int CompareBySignificantDigits(double x, double y, int digits) {
      int roundedX = (int)(x * Math.Pow(10, digits));
      int roundedY = (int)(y * Math.Pow(10, digits));

      if(roundedX == roundedY) {
        return 0;
      }

      return x.CompareTo(y);
    }
    #endregion
  }
}
