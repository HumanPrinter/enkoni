using System.Collections;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing {
  /// <summary>Verifies true/false propositions associated with collections in unit tests.</summary>
  public static class CollectionAssert {
    /// <summary>Verifies that the specified collection is empty.</summary>
    /// <param name="value">The collection that is expected to be empty.</param>
    public static void IsEmpty(ICollection value) {
      if(value.Count != 0) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified collection is empty.</summary>
    /// <param name="value">The collection that is expected to be empty.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsEmpty(ICollection value, string message) {
      if(value.Count != 0) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified collection is not empty.</summary>
    /// <param name="value">The collection that is expected not to be empty.</param>
    public static void IsNotEmpty(ICollection value) {
      if(value.Count == 0) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified collection is not empty.</summary>
    /// <param name="value">The collection that is expected not to be empty.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void IsNotEmpty(ICollection value, string message) {
      if(value.Count == 0) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified collection contains <paramref name="expectedSize" /> elements.</summary>
    /// <param name="value">The collection that is expected to have <paramref name="expectedSize" /> elements.</param>
    /// <param name="expectedSize">The number of elements <paramref name="value"/> is expected to have.</param>
    public static void DoesHaveSize(ICollection value, int expectedSize) {
      if(value.Count != expectedSize) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified collection contains <paramref name="expectedSize" /> elements.</summary>
    /// <param name="value">The collection that is expected to have <paramref name="expectedSize" /> elements.</param>
    /// <param name="expectedSize">The number of elements <paramref name="value"/> is expected to have.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void DoesHaveSize(ICollection value, int expectedSize, string message) {
      if(value.Count != expectedSize) {
        throw new AssertFailedException(message);
      }
    }

    /// <summary>Verifies that the specified collection does not contain <paramref name="unexpectedSize" /> elements.</summary>
    /// <param name="value">The collection that is expected not to have <paramref name="unexpectedSize" /> elements.</param>
    /// <param name="unexpectedSize">The number of elements <paramref name="value"/> is not expected to have.</param>
    public static void DoesNotHaveSize(ICollection value, int unexpectedSize) {
      if(value.Count == unexpectedSize) {
        throw new AssertFailedException();
      }
    }

    /// <summary>Verifies that the specified collection does not contain <paramref name="unexpectedSize" /> elements.</summary>
    /// <param name="value">The collection that is expected not to have <paramref name="unexpectedSize" /> elements.</param>
    /// <param name="unexpectedSize">The number of elements <paramref name="value"/> is not expected to have.</param>
    /// <param name="message">A message to display if the assertion fails. This message can be seen in the unit test results.</param>
    public static void DoesNotHaveSize(ICollection value, int unexpectedSize, string message) {
      if(value.Count == unexpectedSize) {
        throw new AssertFailedException(message);
      }
    }
  }
}
