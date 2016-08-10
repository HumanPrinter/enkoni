using System;
using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing {
  /// <summary>Verifies true/false propositions associated with comparable types in unit tests.</summary>
  public static class ComparableAssert {
    /// <summary>Verifies that the specified value is lower than the threshold.</summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value that is expected to be lower than the threshold.</param>
    public static void IsLowerThan<T>(T threshold, T value) where T : IComparable<T> {
      if(value.CompareTo(threshold) >= 0) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified value is lower than the threshold.</summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value that is expected to be lower than the threshold.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsLowerThan<T>(T threshold, T value, string message) where T : IComparable<T> {
      if(value.CompareTo(threshold) >= 0) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified value is lower than or equal to the threshold.</summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value that is expected to be lower than or equal to the threshold.</param>
    public static void IsLowerThanOrEqualTo<T>(T threshold, T value) where T : IComparable<T> {
      if(value.CompareTo(threshold) > 0) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified value is lower than or equal to the threshold.</summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value that is expected to be lower than or equal to the threshold.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsLowerThanOrEqualTo<T>(T threshold, T value, string message) where T : IComparable<T> {
      if(value.CompareTo(threshold) > 0) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified value is greater than the threshold.</summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value that is expected to be greater than the threshold.</param>
    public static void IsGreaterThan<T>(T threshold, T value) where T : IComparable<T> {
      if(value.CompareTo(threshold) <= 0) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified value is greater than the threshold.</summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value that is expected to be greater than the threshold.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsGreaterThan<T>(T threshold, T value, string message) where T : IComparable<T> {
      if(value.CompareTo(threshold) <= 0) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified value is greater than or equal to the threshold.</summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value that is expected to be greater than or equal to the threshold.</param>
    public static void IsGreaterThanOrEqualTo<T>(T threshold, T value) where T : IComparable<T> {
      if(value.CompareTo(threshold) < 0) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified value is greater than or equal to the threshold.</summary>
    /// <typeparam name="T">The type of objects to compare.</typeparam>
    /// <param name="threshold">The threshold value.</param>
    /// <param name="value">The value that is expected to be greater than or equal to the threshold.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsGreaterThanOrEqualTo<T>(T threshold, T value, string message) where T : IComparable<T> {
      if(value.CompareTo(threshold) < 0) {
        throw new AssertFailedException(message);
      }
    }
  }
}
