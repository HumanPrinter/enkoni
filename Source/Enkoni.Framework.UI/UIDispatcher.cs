//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="UIDispatcher.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a dispatcher that can be used to execute actions on the UI thread.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Threading;

namespace Enkoni.Framework.UI {
  /// <summary>Provides services for managing the queue of work items for the UI thread.</summary>
  public static class UIDispatcher {
    #region Static constants
    /// <summary>The actual UI Dispatcher.</summary>
    public static readonly Dispatcher Dispatcher = Application.Current.Dispatcher;
    #endregion

    #region Public Methods
    /// <summary>Executes an action on the UI thread. If this method is called from the UI thread, the action is executed immediately. If the method 
    /// is called from another thread, the action will be enqueued on the UI thread's dispatcher and executed asynchronously.</summary>
    /// <param name="action">The action that is to be executed.</param>
    public static void BeginInvoke(Action action) {
      if(action == null) {
        return;
      }

      if(Dispatcher.CheckAccess()) {
        action();
      }
      else {
        Dispatcher.BeginInvoke(action);
      }
    }
    #endregion
  }
}
