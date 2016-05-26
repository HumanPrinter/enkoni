using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>This <see cref="TriggerAction{T}" /> can be used to bind any event on any <see cref="FrameworkElement"/> to an <see cref="ICommand" />.
  /// Typically, this element is used in XAML to connect the attached element to a command located in a ViewModel. This trigger can only be attached
  /// to a <see cref="FrameworkElement"/> or a class deriving from <see cref="FrameworkElement"/>.
  /// <para>To access the EventArgs of the fired event, use a <see cref="RelayCommand{EventArgs}"/> and leave the <see cref="CommandParameter"/> and
  /// <see cref="CommandParameterValue"/> empty.</para></summary>
  public class EventToCommand : TriggerAction<FrameworkElement> {
    #region Dependency properties

    /// <summary>Identifies the <see cref="Command" /> dependency property.</summary>
    public static readonly DependencyProperty CommandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(EventToCommand),
        new PropertyMetadata(null, (s, e) => {
          EventToCommand sender = s as EventToCommand;
          if(sender == null || sender.AssociatedObject == null) {
            return;
          }

          sender.ValidateAssociatedObjectEnabled();
        }));

    /// <summary>Identifies the <see cref="CommandParameter" /> dependency property.</summary>
    public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object),
        typeof(EventToCommand), new PropertyMetadata(null, (s, e) => {
          EventToCommand sender = s as EventToCommand;
          if(sender == null || sender.AssociatedObject == null) {
            return;
          }

          sender.ValidateAssociatedObjectEnabled();
        }));

    /// <summary>Identifies the <see cref="BindCanExecuteToIsEnabled" /> dependency property.</summary>
    public static readonly DependencyProperty BindCanExecuteToIsEnabledProperty = DependencyProperty.Register("MustToggleIsEnabled", typeof(bool),
        typeof(EventToCommand), new PropertyMetadata(false, (s, e) => {
          EventToCommand sender = s as EventToCommand;
          if(sender == null) {
            return;
          }

          if(e.OldValue != null && e.OldValue is ICommand) {
            ((ICommand)e.OldValue).CanExecuteChanged -= sender.OnCommandCanExecuteChanged;
          }

          ICommand command = e.NewValue as ICommand;
          if(command != null) {
            command.CanExecuteChanged += sender.OnCommandCanExecuteChanged;
          }

          sender.ValidateAssociatedObjectEnabled();
        }));
    #endregion

    #region Instance variables

    /// <summary>Indicates if the CanExecute must be evaluated by the object.</summary>
    private bool? bindCanExecuteToIsEnabled;

    /// <summary>The parameter that will be passed to the command.</summary>
    private object commandParameter;

    #endregion

    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="EventToCommand"/> class.</summary>
    public EventToCommand() {
    }

    #endregion

    #region Properties

    /// <summary>Gets or sets the <see cref="ICommand"/> that this trigger is bound to. This is a DependencyProperty.</summary>
    public ICommand Command {
      get {
        return (ICommand)this.GetValue(CommandProperty);
      }

      set {
        this.SetValue(CommandProperty, value);
      }
    }

    /// <summary>Gets or sets an object that will be passed to the <see cref="Command" /> attached to this trigger. This is a DependencyProperty.
    /// </summary>
    public object CommandParameter {
      get {
        return this.GetValue(CommandParameterProperty);
      }

      set {
        this.SetValue(CommandParameterProperty, value);
      }
    }

    /// <summary>Gets or sets an object that will be passed to the <see cref="Command" /> attached to this trigger. This property is here for
    /// compatibility with the Silverlight version. This is NOT a DependencyProperty. For data binding, use the <see cref="CommandParameter" />
    /// property.</summary>
    public object CommandParameterValue {
      get {
        return this.commandParameter ?? this.CommandParameter;
      }

      set {
        this.commandParameter = value;
        this.ValidateAssociatedObjectEnabled();
      }
    }

    /// <summary>Gets or sets a value indicating whether the EventArgs passed to the event handler will be forwarded to the
    /// <see cref="ICommand.Execute"/> method, when the event is fired (if the bound ICommand accepts an argument of type EventArgs).
    /// <para>For example, use a <see cref="RelayCommand{MouseEventArgs}"/> to get the arguments of a MouseMove event.</para></summary>
    public bool PassEventArgsToCommand { get; set; }

    /// <summary>Gets or sets a value indicating whether the attached element must be disabled when the <see cref="Command" /> property's
    /// CanExecuteChanged event fires. If this property is true, and the command's CanExecute method returns false, the element will be disabled. If
    /// this property is false, the element will not be disabled when the command's CanExecute method changes. This is a DependencyProperty.</summary>
    public bool BindCanExecuteToIsEnabled {
      get {
        return (bool)this.GetValue(BindCanExecuteToIsEnabledProperty);
      }

      set {
        this.SetValue(BindCanExecuteToIsEnabledProperty, value);
      }
    }

    /// <summary>Gets or sets a value indicating whether the attached element must be disabled when the <see cref="Command" /> property's
    /// CanExecuteChanged event fires. If this property is true, and the command's CanExecute  method returns false, the element will be disabled.
    /// This property is here for compatibility with the Silverlight version. This is NOT a DependencyProperty. For data binding, use the
    /// <see cref="BindCanExecuteToIsEnabled" /> property.</summary>
    public bool BindCanExecuteToIsEnabledValue {
      get {
        return this.bindCanExecuteToIsEnabled == null ? this.BindCanExecuteToIsEnabled : this.bindCanExecuteToIsEnabled.Value;
      }

      set {
        this.bindCanExecuteToIsEnabled = value;
        this.ValidateAssociatedObjectEnabled();
      }
    }

    #endregion

    #region Public methods

    /// <summary>Provides a simple way to invoke this trigger programmatically without any EventArgs.</summary>
    public void Invoke() {
      this.Invoke(null);
    }

    #endregion

    #region Protected methods

    /// <summary>Called when a FrameworkElement/Control is attached.</summary>
    protected override void OnAttached() {
      base.OnAttached();
      this.ValidateAssociatedObjectEnabled();
    }

    /// <summary>Executes the trigger.
    /// <para>To access the EventArgs of the fired event, use a <see cref="RelayCommand{EventArgs}"/> and leave the <see cref="CommandParameter"/> and
    /// <see cref="CommandParameterValue"/> empty.</para></summary>
    /// <param name="parameter">The EventArgs of the fired event.</param>
    protected override void Invoke(object parameter) {
      if(this.IsAssociatedObjectDisabled()) {
        return;
      }

      ICommand command = this.Command;
      object commandParameterVal = this.CommandParameterValue;

      if(commandParameterVal == null && this.PassEventArgsToCommand) {
        commandParameterVal = parameter;
      }

      if(command != null && command.CanExecute(commandParameterVal)) {
        command.Execute(commandParameterVal);
      }
    }

    #endregion

    #region Private methods

    /// <summary>This method is here for compatibility with the Silverlight version.</summary>
    /// <returns>The FrameworkElement/Control to which this trigger is attached.</returns>
    #if !SILVERLIGHT
    private FrameworkElement GetAssociatedObject() {
      return this.AssociatedObject;
    }
    #else
    private Control GetAssociatedObject() {
      return this.AssociatedObject as Control;
    }
    #endif

    /// <summary>Determines whether the associated object is disabled.</summary>
    /// <returns><see langword="true"/> if the associated object is disabled; otherwise, <see langword="false"/>.</returns>
    private bool IsAssociatedObjectDisabled() {
      /* Use var because of the differences in Silverlight/WPF */
      var element = this.GetAssociatedObject();
      return element != null && !element.IsEnabled;
    }

    /// <summary>Validates whether the associated object needs to be enabled.</summary>
    private void ValidateAssociatedObjectEnabled() {
      /* Use var because of the differences in Silverlight/WPF */
      var element = this.GetAssociatedObject();
      if(element == null) {
        return;
      }

      ICommand command = this.Command;
      if(this.BindCanExecuteToIsEnabledValue && command != null) {
        element.IsEnabled = command.CanExecute(this.CommandParameterValue);
      }
    }

    #endregion

    #region Private Event Handling

    /// <summary>Called when the outcome of the CanExecute changed.</summary>
    /// <param name="sender">The sender.</param>
    /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
    private void OnCommandCanExecuteChanged(object sender, EventArgs e) {
      this.ValidateAssociatedObjectEnabled();
    }

    #endregion
  }
}
