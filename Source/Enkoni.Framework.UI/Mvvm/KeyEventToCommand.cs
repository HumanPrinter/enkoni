using System;
using System.Collections.Generic;
using System.Windows.Input;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Derived class from EventToCommand to map key events to a command.</summary>
  public class KeyEventToCommand : EventToCommand {
    #region Instance variables

    /// <summary>The collection of associated keys.</summary>
    private List<Key> associatedKeys = new List<Key>();

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="KeyEventToCommand"/> class.</summary>
    public KeyEventToCommand() {
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets the associated keys.</summary>
    public string AssociatedKeys {
      get {
        return string.Join(" ", this.associatedKeys);
      }

      set {
        this.associatedKeys.Clear();
        if(!string.IsNullOrWhiteSpace(value)) {
          foreach(string item in value.Split(' ')) {
            Key key;
            if(Enum.TryParse(item, true, out key)) {
              this.associatedKeys.Add(key);
            }
          }
        }
      }
    }

    /// <summary>Gets or sets a value indicating whether the <see cref="System.Windows.RoutedEventArgs.Handled"/> property must set to <see langword="true"/> after execution
    /// of the command.</summary>
    public bool MarkEventAsHandled { get; set; }

    #endregion

    #region Private methods

    /// <summary>Executes the trigger.
    /// <para>To access the EventArgs of the fired event, use a <see cref="RelayCommand{EventArgs}"/> and leave the CommandParameter and
    /// CommandParameterValue empty.</para></summary>
    /// <param name="parameter">The EventArgs of the fired event.</param>
    protected override void Invoke(object parameter) {
      KeyEventArgs args = parameter as KeyEventArgs;
      if(args == null) {
        return;
      }

      if(!this.associatedKeys.Contains(args.Key)) {
        return;
      }

      base.Invoke(parameter);

      if(this.MarkEventAsHandled) {
        args.Handled = true;
      }
    }

    #endregion
  }
}
