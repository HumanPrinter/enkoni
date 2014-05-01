using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>This class contains some all-purpose extension-methods.</summary>
  public static class Extensions {
    #region Event extensions
    /// <summary>Fires an event in a sequential way. An event handler needs to finish before the next event handler will be called. This method will 
    /// return when all the event handlers have finished. <br/>
    /// This method will automatically check if there are any event handlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the event handlers.</param>
    public static void Fire(this PropertyChangedEventHandler handler, object sender, PropertyChangedEventArgs e) {
      UnsafeFire(handler, sender, e);
    }

    /// <summary>Fires an event in a parallel way. This method will return when all the event handlers have finished.<br/>
    /// This method will automatically check if there are any event handlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the event handlers.</param>
    public static void FireInParallel(this PropertyChangedEventHandler handler, object sender, PropertyChangedEventArgs e) {
      UnsafeFireInParallel(handler, sender, e);
    }

    /// <summary>Fires an event in an asynchronous way. When this method returns, the event handlers may still be running.<br/>
    /// This method will automatically check if there are any event handlers subscribed to <paramref name="handler"/> and it will automatically 
    /// propagate the call the the appropriate thread.</summary>
    /// <param name="handler">The multicast delegate that must be executed.</param>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any metadata that must be passed to the event handlers.</param>
    public static void FireAsync(this PropertyChangedEventHandler handler, object sender, PropertyChangedEventArgs e) {
      UnsafeFireAsync(handler, sender, e);
    }
    #endregion

    #region Helper methods
    /// <summary>Invokes a delegate. If required, the call is transferred to the appropriate thread.</summary>
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
      Action<Delegate, object[]> asyncFire = InvokeDelegate;

      /* Invoke the subscribers */
      foreach(Delegate sink in delegates) {
        IAsyncResult asyncResult = asyncFire.BeginInvoke(sink, args, null, null);
        calls.Add(asyncResult.AsyncWaitHandle);
      }

      /* Wait untill all the subscribers are finished */
      WaitHandle[] handles = calls.ToArray();
      WaitHandle.WaitAll(handles);

      Array.ForEach(handles, handle => handle.Close());
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
      Action<Delegate, object[]> asyncFire = InvokeDelegate;
      AsyncCallback cleanUp = asyncResult => asyncResult.AsyncWaitHandle.Close();

      /* Invoke the subscribers */
      foreach(Delegate sink in delegates) {
        asyncFire.BeginInvoke(sink, args, cleanUp, null);
      }
    }
    #endregion
  }
}