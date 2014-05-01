using System;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Message for showing a View.</summary>
  /// <typeparam name="T">The type of view model that must be used by the view.</typeparam>
  [Serializable]
  public class ViewDialogMessage<T> : Message, IViewDialogMessage where T : class/*ViewModel*/ {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="ViewDialogMessage{T}"/> class.</summary>
    public ViewDialogMessage()
      : this(null) {
    }

    /// <summary>Initializes a new instance of the <see cref="ViewDialogMessage{T}"/> class.</summary>
    /// <param name="sender">The sender.</param>
    public ViewDialogMessage(object sender)
      : base(sender) {
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the ViewModel source for the View.</summary>
    public T ViewModel { get; set; }

    /// <summary>Gets or sets the ViewModel source for the View.</summary>
    object IViewDialogMessage.ViewModel {
      get { return this.ViewModel; }
      set { this.ViewModel = value as T; }
    }

    /// <summary>Gets or sets the callback called when a ViewModelDialog is closed.</summary>
    public Action<bool?> Callback { get; set; }
    #endregion
  }
}
