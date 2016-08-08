using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing {
  /// <summary>Verifies true/false propositions associated with strings in unit tests.</summary>
  public static class StringAssert {
    /// <summary>Verifies that the specified string equals an empty string.</summary>
    /// <param name="value">The string that is expected to match an empty string.</param>
    public static void IsEmpty(string value) {
      if(!string.Empty.Equals(value)) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified string equals an empty string.</summary>
    /// <param name="value">The string that is expected to match an empty string.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsEmpty(string value, string message) {
      if(!string.Empty.Equals(value)) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified string does not equal an empty string.</summary>
    /// <param name="value">The string that is expected not to match an empty string.</param>
    public static void IsNotEmpty(string value) {
      if(string.Empty.Equals(value)) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified string does not equal an empty string.</summary>
    /// <param name="value">The string that is expected not to match an empty string.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsNotEmpty(string value, string message) {
      if(string.Empty.Equals(value)) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified string equals <see langword="null"/> or an empty string.</summary>
    /// <param name="value">The string that is expected to be <see langword="null"/> or to match an empty string.</param>
    public static void IsNullOrEmpty(string value) {
      if(!string.IsNullOrEmpty(value)) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified string equals <see langword="null"/> or an empty string.</summary>
    /// <param name="value">The string that is expected to be <see langword="null"/> or to match an empty string.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsNullOrEmpty(string value, string message) {
      if(!string.IsNullOrEmpty(value)) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified string does not equal <see langword="null"/> or an empty string.</summary>
    /// <param name="value">The string that is expected not to be <see langword="null"/> or to match an empty string.</param>
    public static void IsNotNullOrEmpty(string value) {
      if(string.IsNullOrEmpty(value)) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified string does not equal <see langword="null"/> or an empty string.</summary>
    /// <param name="value">The string that is expected not to be <see langword="null"/> or to match an empty string.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsNotNullOrEmpty(string value, string message) {
      if(string.IsNullOrEmpty(value)) {
        throw new AssertFailedException(message);
      }
    }
  }
}
