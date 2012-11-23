//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DoubleEqualityComparer.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Holds a comparer-class that is capable of comparing two doubles.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace Enkoni.Framework {
  /// <summary>This class compares two <see cref="Double"/> values a more mathematically accepted approach.</summary>
  public class DoubleEqualityComparer : IEqualityComparer<double> {
    #region Instance variables
    /// <summary>The option that determines the compare method.</summary>
    private DoubleCompareOption compareOption;

    /// <summary>The factor that is used in the comparison.</summary>
    private double compareFactor;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DoubleEqualityComparer"/> class.</summary>
    /// <param name="comparisonFactor">The factor that must be taken into account. If <paramref name="compareOption"/> is set to 
    /// <see cref="DoubleCompareOption.Margin"/>, the comparison factor will be treated as an absolute margin. If <paramref name="compareOption"/> is
    /// set to <see cref="DoubleCompareOption.SignificantDigits"/> the comparison factor will be treated as the number of digits that must be 
    /// examined will comparing. Note that the comparison factor in that case will be truncated to an integer.</param>
    /// <param name="compareOption">Defines the method that must be used to compare the double values.</param>
    public DoubleEqualityComparer(double comparisonFactor, DoubleCompareOption compareOption) {
      this.compareOption = compareOption;
      this.compareFactor = comparisonFactor;
    }
    #endregion

    #region IEqualityComparer<T> Members
    /// <summary>Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.</summary>
    /// <param name="x">The first object to compare.</param>
    /// <param name="y">The second object to compare.</param>
    /// <returns>If <b>x</b> is smaller than <b>y</b>, a value less than zero is returned. If <b>x</b> equals <b>y</b>, zero is returned. If <b>x</b> 
    /// is greater than <b>y</b>, a value greater than zero is returned.</returns>
    public bool Equals(double x, double y) {
      if(double.IsNaN(x) && double.IsNaN(y)) {
        return true;
      }

      if(double.IsNaN(x) || double.IsNaN(y)) {
        return false;
      }

      if(double.IsPositiveInfinity(x) && double.IsPositiveInfinity(y)) {
        return true;
      }

      if(double.IsPositiveInfinity(x) || double.IsPositiveInfinity(y)) {
        return false;
      }

      if(double.IsNegativeInfinity(x) && double.IsNegativeInfinity(y)) {
        return true;
      }

      if(double.IsNegativeInfinity(x) || double.IsNegativeInfinity(y)) {
        return false;
      }

      if(x == y) {
        return true;
      }

      switch(this.compareOption) {
        case DoubleCompareOption.Margin:
          return EqualsByMargin(x, y, this.compareFactor);
        case DoubleCompareOption.SignificantDigits:
          return EqualsBySignificantDigits(x, y, (int)this.compareFactor);
        default:
          return x.Equals(y);
      }
    }

    /// <summary>Returns a hash code for the specified object.</summary>
    /// <param name="obj">The <see cref="Object"/> for which a hash code is to be returned.</param>
    /// <returns>A hash code for the specified object.</returns>
    public int GetHashCode(double obj) {
      return obj.GetHashCode();
    }
    #endregion

    #region Private helper methods
    /// <summary>Compares two doubles by looking if the difference is within the specified margin. If so, the two values are considered equal.
    /// </summary>
    /// <param name="x">The left operand.</param>
    /// <param name="y">The right operand.</param>
    /// <param name="margin">The margin that must be taken into account.</param>
    /// <returns><see langword="true"/> if the difference between the two numbers is within the margin; otherwise, <see langword="false"/>.</returns>
    private static bool EqualsByMargin(double x, double y, double margin) {
      double maxOperand = Math.Max(x, y);
      double minOperand = Math.Min(x, y);

      double difference = maxOperand - minOperand;
      return difference <= margin;
    }

    /// <summary>Compares two doubles by looking only at the significant digits.</summary>
    /// <param name="x">The left operand.</param>
    /// <param name="y">The right operand.</param>
    /// <param name="digits">The siginificant digits that must be taken into account.</param>
    /// <returns><see langword="true"/> if the two numbers are equal upto their significant digits; otherwise, <see langword="false"/>.</returns>
    private static bool EqualsBySignificantDigits(double x, double y, int digits) {
      int roundedX = (int)Math.Round((x * Math.Pow(10, digits)));
      int roundedY = (int)Math.Round((y * Math.Pow(10, digits)));

      return roundedX == roundedY;
    }
    #endregion
  }
}
