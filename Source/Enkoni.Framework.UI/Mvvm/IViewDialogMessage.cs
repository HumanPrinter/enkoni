//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="IViewDialogMessage.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines an interface that can be used to show a dialog using a specific viewmodel.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Interface to access the generic <see cref="ViewDialogMessage{T}"/> class in a non generic way.</summary>
  public interface IViewDialogMessage : IMessage {
    /// <summary>Gets or sets the ViewModel source for the View.</summary>
    object ViewModel { get; set; }

    /// <summary>Gets or sets the callback called when a View is closed.</summary>
    Action<bool?> Callback { get; set; }
  }
}
