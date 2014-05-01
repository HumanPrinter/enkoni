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
