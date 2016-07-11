using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Enkoni.Framework {
    /// <summary>Contains methods that can be used for parameter checking.</summary>
  public static class Guard {
    #region Constants

    /// <summary>The collection of invalid path characters.</summary>
    private static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();

    #endregion

    /// <summary>Throws <see cref="ArgumentNullException"/> if the given argument is <see langword="null"/>.</summary>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    /// <exception cref="ArgumentNullException"> If tested value if <see langword="null"/>.</exception>
    public static void ArgumentIsNotNull([ValidatedNotNull]object argumentValue, string argumentName) {
      ArgumentIsNotNull(argumentValue, argumentName, null);
    }

    /// <summary>Throws <see cref="ArgumentNullException"/> if the given argument is <see langword="null"/>.</summary>
    /// <param name="argumentValue">Argument value to test.</param>
    /// <param name="argumentName">Name of the argument being tested.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentNullException"> If tested value if <see langword="null"/>.</exception>
    /// <remarks>If <paramref name="message" /> equals <see langword="null"/> or an empty string, the message will not be passed to the exception.</remarks>
    public static void ArgumentIsNotNull([ValidatedNotNull]object argumentValue, string argumentName, string message) {
      if(argumentValue == null) {
        ThrowArgumentNullException(argumentName, message);
      }
    }

    /// <summary>Throws an exception if the tested string argument is <see langword="null"/> or the empty string.</summary>
    /// <param name="argumentValue">Argument value to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="argumentValue"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if the string is empty.</exception>
    public static void ArgumentIsNotNullOrEmpty([ValidatedNotNull]string argumentValue, string argumentName) {
      if(argumentValue == null) {
        ThrowArgumentNullException(argumentName, null);
        return;
      }

      if(argumentValue.Length == 0) {
        ThrowArgumentException(argumentName, "Value cannot be empty.");
      }
    }

    /// <summary>Throws an exception if the tested string argument is <see langword="null"/> or the empty string.</summary>
    /// <param name="argumentValue">Argument value to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="argumentValue"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if the string is empty.</exception>
    public static void ArgumentIsNotNullOrEmpty([ValidatedNotNull]string argumentValue, string argumentName, string message) {
      if(argumentValue == null) {
        ThrowArgumentNullException(argumentName, message);
        return;
      }

      if(argumentValue.Length == 0) {
        ThrowArgumentException(argumentName, message);
      }
    }

    /// <summary>Throws an exception if the tested collection is <see langword="null"/> or contains no elements.</summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    /// <param name="argumentValue">Argument value to check.</param>
    /// <param name="argumentName">Name of argument being checked.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="argumentValue"/> is <see langword="null"/>.</exception>
    /// <exception cref="ArgumentException">Thrown if the string is empty.</exception>
    public static void ArgumentIsNotNullOrEmpty<T>([ValidatedNotNull]IEnumerable<T> argumentValue, string argumentName, string message) {
      if(argumentValue == null) {
        ThrowArgumentNullException(argumentName, message);
      }

      if(argumentValue.Count() == 0) {
        ThrowArgumentException(argumentName, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is less than <paramref name="lowerValue"/>.</summary>
    /// <typeparam name="T">A type that implements <see cref="IComparable"/>.</typeparam>
    /// <param name="lowerValue">The lower value accepted as valid.</param>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">Validation error.</exception>
    /// <remarks>This method will not test for <see langword="null"/> values. When <paramref name="argumentValue"/> equals <see langword="null"/>, this method will break.
    /// To test for <see langword="null"/> values, use the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</remarks>
    public static void ArgumentIsGreaterOrEqualThan<T>(T lowerValue, T argumentValue, string argumentName, string message) where T : struct, IComparable {
      if(argumentValue.CompareTo((T)lowerValue) < 0) {
        ThrowArgumentOutOfRangeException(argumentName, argumentValue, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is less than or equal to <paramref name="lowerValue"/>.</summary>
    /// <typeparam name="T">A type that implements <see cref="IComparable"/>.</typeparam>
    /// <param name="lowerValue">The lower value accepted as valid.</param>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">Validation error.</exception>
    /// <remarks>This method will not test for <see langword="null"/> values. When <paramref name="argumentValue"/> equals <see langword="null"/>, this method will break.
    /// To test for <see langword="null"/> values, use the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</remarks>
    public static void ArgumentIsGreaterThan<T>(T lowerValue, T argumentValue, string argumentName, string message) where T : struct, IComparable {
      if(argumentValue.CompareTo((T)lowerValue) <= 0) {
        ThrowArgumentOutOfRangeException(argumentName, argumentValue, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is greater than <paramref name="higherValue"/>.</summary>
    /// <typeparam name="T">A type that implements <see cref="IComparable"/>.</typeparam>
    /// <param name="higherValue">The higher value accepted as valid.</param>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">Validation error.</exception>
    /// <remarks>This method will not test for <see langword="null"/> values. When <paramref name="argumentValue"/> equals <see langword="null"/>, this method will break.
    /// To test for <see langword="null"/> values, use the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</remarks>
    public static void ArgumentIsLowerOrEqualThan<T>(T higherValue, T argumentValue, string argumentName, string message) where T : struct, IComparable {
      if(argumentValue.CompareTo((T)higherValue) > 0) {
        ThrowArgumentOutOfRangeException(argumentName, argumentValue, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is greater than or equal to <paramref name="higherValue"/>.</summary>
    /// <typeparam name="T">A type that implements <see cref="IComparable"/>.</typeparam>
    /// <param name="higherValue">The higher value accepted as valid.</param>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">Validation error.</exception>
    /// <remarks>This method will not test for <see langword="null"/> values. When <paramref name="argumentValue"/> equals <see langword="null"/>, this method will break.
    /// To test for <see langword="null"/> values, use the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</remarks>
    public static void ArgumentIsLowerThan<T>(T higherValue, T argumentValue, string argumentName, string message) where T : struct, IComparable {
      if(argumentValue.CompareTo((T)higherValue) >= 0) {
        ThrowArgumentOutOfRangeException(argumentName, argumentValue, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is less than <paramref name="lowerValue"/> or greater than <paramref name="higherValue"/>.</summary>
    /// <typeparam name="T">A type that implements <see cref="IComparable"/>.</typeparam>
    /// <param name="lowerValue">The lower value accepted as valid.</param>
    /// <param name="higherValue">The higher value accepted as valid.</param>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentOutOfRangeException">Validation error.</exception>
    /// <remarks>This method will not test for <see langword="null"/> values. When <paramref name="argumentValue"/> equals <see langword="null"/>, this method will break.
    /// To test for <see langword="null"/> values, use the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</remarks>
    public static void ArgumentIsBetween<T>(T lowerValue, T higherValue, T argumentValue, string argumentName, string message) where T : struct, IComparable {
      if(argumentValue.CompareTo((T)lowerValue) < 0) {
        ThrowArgumentOutOfRangeException(argumentName, argumentValue, message);
      }

      if(argumentValue.CompareTo((T)higherValue) > 0) {
        ThrowArgumentOutOfRangeException(argumentName, argumentValue, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is not a member of enum type <paramref name="enumType"/>.</summary>
    /// <param name="enumType">The type of enum.</param>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentException">Validation error.</exception>
    public static void ArgumentIsValidEnum(Type enumType, object argumentValue, string argumentName, string message) {
      Guard.ArgumentIsNotNull(enumType, nameof(enumType));
      if(!Enum.IsDefined(enumType, argumentValue))
      {
        ThrowArgumentException(argumentName, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is not of type <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The type that is expected.</typeparam>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentException">Validation error.</exception>
    public static void ArgumentIsOfType<T>(object argumentValue, string argumentName, string message) {
      ArgumentIsOfType<T>(false, argumentValue, argumentName, message);
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is not of type <typeparamref name="T"/> or a subtype of <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The type that is expected.</typeparam>
    /// <param name="allowDerivedTypes">Indicates if derived types are also allowed.</param>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentException">Validation error.</exception>
    /// <remarks>This method will not test for <see langword="null"/> values. When <paramref name="argumentValue"/> equals <see langword="null"/>, this method will break.
    /// To test for <see langword="null"/> values, use the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</remarks>
    public static void ArgumentIsOfType<T>(bool allowDerivedTypes, object argumentValue, string argumentName, string message) {
      if(allowDerivedTypes) {
        if(!(argumentValue is T)) {
          ThrowArgumentException(argumentName, message);
        }
      }
      else if(argumentValue.GetType() != typeof(T)) {
        ThrowArgumentException(argumentName, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is of type <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The type that is expected.</typeparam>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentException">Validation error.</exception>
    public static void ArgumentIsNotOfType<T>(object argumentValue, string argumentName, string message) {
      ArgumentIsNotOfType<T>(false, argumentValue, argumentName, message);
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> is of type <typeparamref name="T"/> or a subtype of <typeparamref name="T"/>.</summary>
    /// <typeparam name="T">The type that is not expected.</typeparam>
    /// <param name="disallowDerivedTypes">Indicates if derived types are also not allowed.</param>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentException">Validation error.</exception>
    /// <remarks>This method will not test for <see langword="null"/> values. When <paramref name="argumentValue"/> equals <see langword="null"/>, this method will break.
    /// To test for <see langword="null"/> values, use the <see cref="Guard.ArgumentIsNotNull(object, string)"/> method.</remarks>
    public static void ArgumentIsNotOfType<T>(bool disallowDerivedTypes, object argumentValue, string argumentName, string message) {
      if(disallowDerivedTypes) {
        if(Extensions.Implements(argumentValue.GetType(), typeof(T))) {
          ThrowArgumentException(argumentName, message);
        }
      }
      else if(argumentValue is T) {
        ThrowArgumentException(argumentName, message);
      }
    }

    /// <summary>Throws an exception if <paramref name="argumentValue"/> does not denote a valid path.</summary>
    /// <param name="argumentValue">The argument value to test.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="message">The message that will be passed to the exception.</param>
    /// <exception cref="ArgumentException">Validation error.</exception>
    public static void ArgumentIsValidPath(string argumentValue, string argumentName, string message) {
      if(argumentValue.Intersect(InvalidPathChars).Count() > 0) {
        ThrowArgumentException(argumentName, message);
      }
    }

    #region Private static helper methods

    /// <summary>Throws an <see cref="ArgumentNullException"/>.</summary>
    /// <param name="argumentName">The name of the argument whose value is <see langword="null"/>.</param>
    /// <param name="message">The message for the exception.</param>
    private static void ThrowArgumentNullException(string argumentName, string message) {
      if(string.IsNullOrEmpty(message)) {
        throw new ArgumentNullException(argumentName);
      }
      else {
        throw new ArgumentNullException(argumentName, message);
      }
    }

    /// <summary>Throws an <see cref="ArgumentException"/>.</summary>
    /// <param name="argumentName">The name of the argument whose value is invalid.</param>
    /// <param name="message">The message for the exception.</param>
    private static void ThrowArgumentException(string argumentName, string message) {
      throw new ArgumentException(message, argumentName);
    }

    /// <summary>Throws an <see cref="ArgumentOutOfRangeException"/>.</summary>
    /// <param name="argumentName">The name of the argument whose value is invalid.</param>
    /// <param name="argumentValue">The value of <paramref name="argumentName"/>.</param>
    /// <param name="message">The message for the exception.</param>
    private static void ThrowArgumentOutOfRangeException(string argumentName, object argumentValue, string message) {
      throw new ArgumentOutOfRangeException(argumentName, argumentValue, message);
    }

    #endregion
  }
}
