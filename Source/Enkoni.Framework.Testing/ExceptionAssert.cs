using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing {
  /// <summary>Can be used to check if an action throws an exception.</summary>
  public static class ExceptionAssert {
    /// <summary>Checks if the exception of type <typeparamref name="TException"/> was thrown.</summary>
    /// <typeparam name="TException">The type of the exception that should be thrown.</typeparam>
    /// <param name="action">The action that should throw an exception of type <typeparamref name="TException"/>.</param>
    /// <exception cref="Exception">An exception that does not inherit from <typeparamref name="TException"/></exception>
    public static void Throws<TException>(Action action) where TException : Exception {
      Throws<TException>(action, string.Format(CultureInfo.InvariantCulture, "Exception of type {0} was not thrown.", typeof(TException).Name));
    }

    /// <summary>Checks if the exception of type <typeparamref name="TException"/> was thrown.</summary>
    /// <typeparam name="TException">The type of the exception that should be thrown.</typeparam>
    /// <param name="action">The action that should throw an exception of type <typeparamref name="TException"/>.</param>
    /// <param name="message">The message that should be used when no exception was thrown.</param>
    /// <exception cref="Exception">An exception that does not inherit from <typeparamref name="TException"/></exception>
    public static void Throws<TException>(Action action, string message) where TException : Exception {
      Throws<TException>(action, message, false);
    }

    /// <summary>Checks if the exception of type <typeparamref name="TException"/> was thrown.</summary>
    /// <typeparam name="TException">The type of the exception that should be thrown.</typeparam>
    /// <param name="action">The action that should throw an exception of type <typeparamref name="TException"/>.</param>
    /// <param name="message">The message that should be used when no exception was thrown.</param>
    /// <param name="allowDerivedTypes">Specifies whether derived types of the exception are allowed.</param>
    /// <exception cref="Exception">An exception that does not inherit from <typeparamref name="TException"/></exception>
    public static void Throws<TException>(Action action, string message, bool allowDerivedTypes) where TException : Exception {
      Guard.ArgumentIsNotNull(action, nameof(action));
      Guard.ArgumentIsNotNullOrEmpty(message, nameof(message), "Value cannot be empty.");

      try {
        action();
        throw new AssertFailedException(message);
      }
      catch(AssertFailedException) {
      }
      catch(TException exception) {
        if(!allowDerivedTypes && exception.GetType() != typeof(TException)) {
          throw new AssertFailedException(message, exception);
        }
      }
      catch(Exception exception) {
        throw new AssertFailedException(message, exception);
      }
    }
  }
}
