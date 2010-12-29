//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2010. All rights reserved.
// </copyright>
// <summary>
//     Defines several all-purpose extension methods.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;

namespace OscarBrouwer.Framework {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Extensions {
    #region Private delegates
    /// <summary>This delegate is used to execute delegates asynchronous.</summary>
    /// <param name="del">The actual delegate that must be executed.</param>
    /// <param name="args">Any arguments that must be passed to delegate <c>del</c>.</param>
    private delegate void AsyncFire(Delegate del, object[] args);
    #endregion

    #region ICloneable extensions
    /// <summary>Returns a strong-typed clone of the instance.</summary>
    /// <typeparam name="T">The actual type of the instance that will be cloned.</typeparam>
    /// <param name="instance">The instance on which the clone-method will be invoked.</param>
    /// <returns>A typed clone of the instance.</returns>
    public static T Clone<T>(this T instance) where T : ICloneable {
      if(instance == null) {
        return default(T);
      }
      else {
        return (T)instance.Clone();
      }
    }
    #endregion

    #region Event extensions
    /// <summary>Fires an event in a serial way. An eventhandler needs to finish before the next eventhandler will be 
    /// called. This method will return when all the eventhandlers have finished. <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void Fire(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFire(handler, sender, e);
    }

    /// <summary>Fires an event in a serial way. An eventhandler needs to finish before the next eventhandler will be 
    /// called. This method will return when all the eventhandlers have finished. <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/>
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <typeparam name="T">The type of eventargs that is passed with the event.</typeparam>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void Fire<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs {
      UnsafeFire(handler, sender, e);
    }

    /// <summary>Fires an event in a parallel way. This method will return when all the eventhandlers have finished. 
    /// <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireInParallel(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFireInParallel(handler, sender, e);
    }

    /// <summary>Fires an event in a parallel way. This method will return when all the eventhandlers have finished. 
    /// <br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <typeparam name="T">The type of eventargs that is passed with the event.</typeparam>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireInParallel<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs {
      UnsafeFireInParallel(handler, sender, e);
    }

    /// <summary>Fires an event in an asynchronous way. When this method returns, the eventhandlers may still be 
    /// running.<br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireAsync(this EventHandler handler, object sender, EventArgs e) {
      UnsafeFireAsync(handler, sender, e);
    }

    /// <summary>Fires an event in an asynchronous way. When this method returns, the eventhandlers may still be 
    /// running.<br/>
    /// This method will automatically check if there are any eventhandlers subscribed to <paramref name="handler"/> 
    /// and it will automatically propagate the call the the appropriate thread.</summary>
    /// <typeparam name="T">The type of eventargs that is passed with the event.</typeparam>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the eventhandlers.</param>
    public static void FireAsync<T>(this EventHandler<T> handler, object sender, T e) where T : EventArgs {
      UnsafeFireAsync(handler, sender, e);
    }
    #endregion

    #region String extensions
    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <returns>The capitalized string.</returns>
    public static string Capitalize(this string source) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      return source.Capitalize(null);
    }

    /// <summary>Capitalizes the first letter of each word assuming that words are separated by a single space.</summary>
    /// <param name="source">The string that must be capitalized.</param>
    /// <param name="culture">A <see cref="CultureInfo"/> object that supplies culture-specific casing rules.</param>
    /// <returns>The capitalized string.</returns>
    public static string Capitalize(this string source, CultureInfo culture) {
      if(source == null) {
        throw new ArgumentNullException("source");
      }

      if(culture == null) {
        culture = CultureInfo.CurrentCulture;
      }

      IEnumerable<string> capitalizedStrings = source.Split(' ')
        .Select(str => new string(new char[] { str.First() }).ToUpper(culture) + /* Capitalize the first character */
        new string(str.Skip(1).ToArray()).ToLower(culture));                     /* Lower the remaining characters */
      return string.Join(" ", capitalizedStrings.ToArray());
    }
    #endregion

    #region Helper methods
    /// <summary>Invokes a delegate. If required, the call is transfered to the appropriate thread.</summary>
    /// <param name="del">The delegate that must be invoked.</param>
    /// <param name="args">The arguments that must be passed to the delegate.</param>
    private static void InvokeDelegate(Delegate del, object[] args) {
      ISynchronizeInvoke synchronizer = del.Target as ISynchronizeInvoke;
      if(synchronizer != null) {
        /* Requires thread affinity */
        if(synchronizer.InvokeRequired) {
          synchronizer.Invoke(del, args);
          return;
        }
      }

      /* Not requiring thread affinity or invoke is not required */
      del.DynamicInvoke(args);
    }

    /// <summary>Invokes the subscribers of the specified delegate in a sequential way.</summary>
    /// <param name="del">The delegate whose subscribers must be invoked.</param>
    /// <param name="args">The arguments that must be passed to the delegate-subscribers.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void UnsafeFire(Delegate del, params object[] args) {
      /* Check if there are any subscribers */
      if(del == null) {
        return;
      }

      /* Get the subscribers and invoke them sequential */
      Delegate[] delegates = del.GetInvocationList();
      foreach(Delegate sink in delegates) {
        InvokeDelegate(sink, args);
      }
    }

    /// <summary>Invokes the subscribers of the specified delegate in a parallel way.</summary>
    /// <param name="del">The delegate whose subscribers must be invoked.</param>
    /// <param name="args">The arguments that must be passed to the delegate-subscribers.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void UnsafeFireInParallel(Delegate del, params object[] args) {
      /* Check if there are any subscribers */
      if(del == null) {
        return;
      }

      /* Get the subscribers and prepare the asynchronous invocations */
      Delegate[] delegates = del.GetInvocationList();
      List<WaitHandle> calls = new List<WaitHandle>(delegates.Length);
      AsyncFire asyncFire = InvokeDelegate;

      /* Invoke the subscribers */
      foreach(Delegate sink in delegates) {
        IAsyncResult asyncResult = asyncFire.BeginInvoke(sink, args, null, null);
        calls.Add(asyncResult.AsyncWaitHandle);
      }

      /* Wait untill all the subscribers are finished */
      WaitHandle[] handles = calls.ToArray();
      WaitHandle.WaitAll(handles);
      Action<WaitHandle> close = delegate(WaitHandle handle) {
        handle.Close();
      };
      Array.ForEach(handles, close);
    }

    /// <summary>Invokes the subscribers of the specified delegate in an asynchronous way.</summary>
    /// <param name="del">The delegate whose subscribers must be invoked.</param>
    /// <param name="args">The arguments that must be passed to the delegate-subscribers.</param>
    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void UnsafeFireAsync(Delegate del, params object[] args) {
      /* Check if there are any subscribers */
      if(del == null) {
        return;
      }

      /* Get the subscribers and prepare the asynchronous invocations */
      Delegate[] delegates = del.GetInvocationList();
      AsyncFire asyncFire = InvokeDelegate;
      AsyncCallback cleanUp = delegate(IAsyncResult asyncResult) {
        asyncResult.AsyncWaitHandle.Close();
      };

      /* Invoke the subscribers */
      foreach(Delegate sink in delegates) {
        asyncFire.BeginInvoke(sink, args, cleanUp, null);
      }
    }

    #endregion
  }
}
