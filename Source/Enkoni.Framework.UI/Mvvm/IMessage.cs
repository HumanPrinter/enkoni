using System.Windows;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Interface that defines a message that can be used with the MVVM pattern.</summary>
  public interface IMessage {
    /// <summary>Gets the message's sender.</summary>
    object Sender { get; }

    /// <summary>Searches for the owner window of the message sender.</summary>
    /// <returns>The window that owns the sender.</returns>
    Window GetWindow();
  }
}
