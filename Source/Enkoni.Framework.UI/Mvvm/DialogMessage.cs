//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="DialogMessage.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a default message that is used to show a dialog.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Windows;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Default message for showing dialog messages.</summary>
  [Serializable]
  public class DialogMessage : Message {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="DialogMessage"/> class.</summary>
    public DialogMessage()
      : this(null) {
    }

    /// <summary>Initializes a new instance of the <see cref="DialogMessage"/> class.</summary>
    /// <param name="sender">The sender.</param>
    public DialogMessage(object sender)
      : base(sender) {
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the caption for the message box.</summary>
    public string Caption { get; set; }

    /// <summary>Gets or sets the message to show.</summary>
    public string Message { get; set; }

    /// <summary>Gets or sets the buttons displayed by the message box.</summary>
    public MessageBoxButton Buttons { get; set; }

    /// <summary>Gets or sets which result is the default in the message box.</summary>
    public MessageBoxResult DefaultResult { get; set; }

    /// <summary>Gets or sets a callback method that should be executed to deliver the result of the message box to the object that sent the message.
    /// </summary>
    public Action<MessageBoxResult> Callback { get; set; }

#if !SILVERLIGHT
    /// <summary>Gets or sets the icon for the message box.</summary>
    public MessageBoxImage Icon { get; set; }
#endif
    #endregion
  }
}
