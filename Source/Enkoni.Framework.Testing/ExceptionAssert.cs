using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Enkoni.Framework.Testing {
  /// <summary>Can be used to check if an action throws an exception.</summary>
  public static class ExceptionAssert {
    /// <summary>Checks if the exception of type <typeparamref name="TException"/> was thrown.</summary>
    /// <typeparam name="TException">The type of the exception that should be thrown.</typeparam>
    /// <param name="action">The action that should throw an exception of type <typeparamref name="TException"/>.</param>
    /// <exception cref="Exception">An exception that does not inherit from <typeparamref name="TException"/></exception>
    public static void Throws<TException>(Action action) where TException : Exception {
      Throws<TException>(action, false);
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
    /// <param name="allowDervivedTypes">Specifies wether derived types of the exception are allowed.</param>
    /// <exception cref="Exception">An exception that does not inherit from <typeparamref name="TException"/></exception>
    public static void Throws<TException>(Action action, bool allowDervivedTypes) where TException : Exception {
      Throws<TException>(action, $"Exception of type {typeof(TException).Name} was not thrown.", allowDervivedTypes);
    }

    /// <summary>Checks if the exception of type <typeparamref name="TException"/> was thrown.</summary>
    /// <typeparam name="TException">The type of the exception that should be thrown.</typeparam>
    /// <param name="action">The action that should throw an exception of type <typeparamref name="TException"/>.</param>
    /// <param name="message">The message that should be used when no exception was thrown.</param>
    /// <param name="allowDervivedTypes">Specifies wether derived types of the exception are allowed.</param>
    /// <exception cref="Exception">An exception that does not inherit from <typeparamref name="TException"/></exception>
    public static void Throws<TException>(Action action, string message, bool allowDervivedTypes) where TException : Exception {
      Guard.ArgumentIsNotNull(action, nameof(action));
      Guard.ArgumentIsNotNullOrEmpty(message, nameof(message), "Value cannot be empty.");

      try {
        action();
        Assert.Fail(message);
      }
      catch (TException exception) {
        Type type = exception.GetType();
        if (!allowDervivedTypes && type != typeof(TException)) {
          Assert.Fail($"Exception of type {typeof(TException).Name} was not thrown an exception of type {type.Name} was thrown.");
        }
      }
      catch (Exception) {
        throw;
      }
    }
  }
}
