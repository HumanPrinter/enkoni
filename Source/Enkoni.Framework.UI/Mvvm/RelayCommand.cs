using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Input;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the
  /// <see cref="CanExecute"/> method is <see langword="true"/>. This class does not allow you to accept command parameters in the
  /// <see cref="Execute"/> and <see cref="CanExecute"/> callback methods.</summary>
  public class RelayCommand : ICommand {
    #region Instance variables

    /// <summary>The action that is executed by this command.</summary>
    private readonly Action action;

    /// <summary>The function that is used to determine if the command can be executed.</summary>
    private readonly Func<bool> canExecute;

    /// <summary>The handler that is executed when the outcome of the 'CanExecute' function changes.</summary>
    private EventHandler canExecuteChangedHandler;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="RelayCommand"/> class.</summary>
    /// <param name="action">The action that will be executed when the command is invoked.</param>
    /// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
    public RelayCommand(Action action)
      : this(action, null) {
    }

    /// <summary>Initializes a new instance of the <see cref="RelayCommand"/> class.</summary>
    /// <param name="action">The action that will be executed when the command is invoked.</param>
    /// <param name="canExecute">The function that will be used to determine if the command can be executed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
    public RelayCommand(Action action, Func<bool> canExecute) {
      Guard.ArgumentIsNotNull(action, nameof(action));

      this.action = action;
      this.canExecute = canExecute;
    }

    #endregion

    #region Public events

    /// <summary>Occurs when changes occur that affect whether the command should execute.</summary>
    public event EventHandler CanExecuteChanged {
      add { this.canExecuteChangedHandler += value; }
      remove { this.canExecuteChangedHandler -= value; }
    }

    #endregion

    #region Public methods

    /// <summary>Raises the <see cref="CanExecuteChanged" /> event on the UI thread.</summary>
    public void RaiseCanExecuteChanged() {
      EventHandler handler = this.canExecuteChangedHandler;
      if(handler == null) {
        return;
      }

      UIDispatcher.BeginInvoke(() => handler(this, EventArgs.Empty));
    }

    /// <summary>Determines if the command can execute in its current state.</summary>
    /// <param name="parameter">This parameter will always be ignored.</param>
    /// <returns><see langword="true"/> if this command can be executed; otherwise, <see langword="false"/>.</returns>
    [DebuggerStepThrough]
    public bool CanExecute(object parameter) {
      return this.canExecute == null ? true : this.canExecute();
    }

    /// <summary>Executes the action behind this command.</summary>
    /// <param name="parameter">This parameter will always be ignored.</param>
    public void Execute(object parameter) {
      this.action();
    }

    #endregion
  }

  /// <summary>A command whose sole purpose is to relay its functionality to other objects by invoking delegates. The default return value for the
  /// <see cref="CanExecute"/> method is <see langword="true"/>. This class allows you to accept command parameters in the <see cref="Execute"/>
  /// and <see cref="CanExecute"/> callback methods.</summary>
  /// <typeparam name="T">The type of command parameter that is handled by this command.</typeparam>
  [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass",
      Justification = "These are small classes that only differ by type-parameter, therefore they can be in the same file")]
  public class RelayCommand<T> : ICommand {
    #region Instance variables

    /// <summary>The action that is executed by this command.</summary>
    private readonly Action<T> action;

    /// <summary>The function that is used to determine if the command can be executed.</summary>
    private readonly Func<T, bool> canExecute;

    /// <summary>The handler that is executed when the outcome of the 'CanExecute' function changes.</summary>
    private EventHandler canExecuteChangedHandler;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="RelayCommand{T}"/> class.</summary>
    /// <param name="action">The action that will be executed when the command is invoked.</param>
    /// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
    public RelayCommand(Action<T> action)
      : this(action, null) {
    }

    /// <summary>Initializes a new instance of the <see cref="RelayCommand{T}"/> class.</summary>
    /// <param name="action">The action that will be executed when the command is invoked.</param>
    /// <param name="canExecute">The function that will be used to determine if the command can be executed.</param>
    /// <exception cref="ArgumentNullException"><paramref name="action"/> is <see langword="null"/>.</exception>
    public RelayCommand(Action<T> action, Func<T, bool> canExecute) {
      Guard.ArgumentIsNotNull(action, nameof(action));

      this.action = action;
      this.canExecute = canExecute;
    }

    #endregion

    #region Public events

    /// <summary>Occurs when changes occur that affect whether the command should execute.</summary>
    public event EventHandler CanExecuteChanged {
      add { this.canExecuteChangedHandler += value; }
      remove { this.canExecuteChangedHandler -= value; }
    }

    #endregion

    #region Public methods

    /// <summary>Raises the <see cref="CanExecuteChanged" /> event on the UI thread.</summary>
    public void RaiseCanExecuteChanged() {
      EventHandler handler = this.canExecuteChangedHandler;
      if(handler == null) {
        return;
      }

      UIDispatcher.BeginInvoke(() => handler(this, EventArgs.Empty));
    }

    /// <summary>Determines if the command can execute in its current state.</summary>
    /// <param name="parameter">Data used by the command.</param>
    /// <returns><see langword="true"/> if this command can be executed; otherwise, <see langword="false"/>.</returns>
    public bool CanExecute(object parameter) {
      return this.canExecute == null ? true : this.canExecute((T)parameter);
    }

    /// <summary>Executes the action behind this command.</summary>
    /// <param name="parameter">Data used by the command.</param>
    public void Execute(object parameter) {
      this.action((T)parameter);
    }

    #endregion
  }
}
