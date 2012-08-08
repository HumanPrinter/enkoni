//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Message.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a default message that can be used with the MVVM pattern.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Default implementation of the <see cref="IMessage"/> interface.</summary>
  [Serializable]
  public class Message : IMessage {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="Message"/> class.</summary>
    public Message()
      : this(null) {
    }

    /// <summary>Initializes a new instance of the <see cref="Message"/> class.</summary>
    /// <param name="sender">The sender.</param>
    public Message(object sender) {
      this.Sender = sender;
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the message's sender.</summary>
    public object Sender { get; protected set; }
    #endregion

    #region Public methods
    /// <summary>Searches for the Window that owns the sender.</summary>
    /// <returns>The window that owns the sender.</returns>
    public Window GetWindow() {
      if(this.Sender == null) {
        return Application.Current.MainWindow;
      }

      foreach(Window window in Application.Current.Windows) {
        if(window.DataContext == this.Sender) {
          return window;
        }
      }

      return Application.Current.MainWindow;
    }
    #endregion
  }
}
