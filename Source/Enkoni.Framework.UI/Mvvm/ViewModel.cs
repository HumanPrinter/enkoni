//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="ViewModel.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a base class for all view models.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Windows;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Base class for all ViewModel classes implementing the <see cref="INotifyPropertyChanged"/> interface.</summary>
  [Serializable]
  public abstract class ViewModel : INotifyPropertyChanged, IDataErrorInfo {
    #region Instance variables
    /// <summary>Gets a value indicating whether the control is in design mode (running in Blend or Visual Studio).</summary>
#if SILVERLIGHT
    public static readonly bool IsInDesignMode = DesignerProperties.IsInDesignTool;
#else
    public static readonly bool IsInDesignMode = (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue;
#endif

    /// <summary>The messenger that manages the messages.</summary>
    private IMessenger messenger;

    /// <summary>The registered validation rules.</summary>
    private Dictionary<string, List<Func<string>>> validationRules = new Dictionary<string, List<Func<string>>>();

    /// <summary>The multicast delegate that is invoked when a property changes.</summary>
    private PropertyChangedEventHandler propertyChangedHandler;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="ViewModel"/> class.</summary>
    protected ViewModel() {
    }
    #endregion

    #region Events
    /// <summary>Occurs when a property value changes.</summary>
    public event PropertyChangedEventHandler PropertyChanged {
      add { this.propertyChangedHandler += value; }
      remove { this.propertyChangedHandler -= value; }
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets an instance of a <see cref="IMessenger" /> used to send messages to other objects. If <see langword="null"/>, this 
    /// class will attempt to broadcast using the Messenger's default instance.</summary>
    public IMessenger Messenger {
      get { return this.messenger ?? Enkoni.Framework.UI.Mvvm.Messenger.Default; }
      set { this.messenger = value; }
    }

    /// <summary>Gets an error message indicating what is wrong with this object.</summary>
    public string Error {
      get {
        IEnumerable<string> errorMessages = this.validationRules.Keys.Select(property => this[property])
          .Where(error => !string.IsNullOrEmpty(error));

        return string.Join(Environment.NewLine, errorMessages);
      }
    }

    /// <summary>Gets the error message for the property with the given name.</summary>
    /// <param name="property">The name of the property whose error message to get.</param>
    /// <returns>The error messages for the specified property.</returns>
    public string this[string property] {
      get {
        List<Func<string>> rules;
        if(this.validationRules.TryGetValue(property, out rules)) {
          return string.Join(Environment.NewLine, rules.Select(rule => rule()).Where(error => !string.IsNullOrEmpty(error)));
        }

        return string.Empty;
      }
    }
    #endregion

    #region Public methods
    /// <summary>Determines whether the specified property contains a valid value.</summary>
    /// <typeparam name="T">The type of property.</typeparam>
    /// <param name="property">The property that must be evaluated.</param>
    /// <returns><see langword="true"/> if the specified property is valid; otherwise, <see langword="false"/>.</returns>
    public bool IsPropertyValid<T>(Expression<Func<T, object>> property) {
      if(property == null) {
        throw new ArgumentNullException("property");
      }

      MemberExpression expression = property.Body as MemberExpression;
      if(expression == null) {
        throw new ArgumentException("The property must contain a MemberExpression", "property");
      }

      if(!(expression.Member is PropertyInfo)) {
        throw new ArgumentException("The expression must refer to a property", "property");
      }

      return string.IsNullOrEmpty(((IDataErrorInfo)this)[expression.Member.Name]);
    }

    /// <summary>Determines whether each property is valid.</summary>
    /// <returns><see langword="true"/> if each property is valid; otherwise, <see langword="false"/>.</returns>
    public bool IsEachPropertyValid() {
      return this.validationRules.Keys.All(property => string.IsNullOrEmpty(((IDataErrorInfo)this)[property]));
    }
    #endregion

    #region Protected methods
    /// <summary>Adds a validation rule for a property.</summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="property">The property that is validated by the rule.</param>
    /// <param name="rule">The rule. (The parameter added to the function is the name of the property).</param>
    protected void AddValidationRule<T>(Expression<Func<T>> property, Func<string> rule) {
      if(property == null) {
        throw new ArgumentNullException("property");
      }

      MemberExpression expression = property.Body as MemberExpression;
      if(expression == null) {
        throw new ArgumentException("The property must contain a MemberExpression", "property");
      }

      if(!(expression.Member is PropertyInfo)) {
        throw new ArgumentException("The expression must refer to a property", "property");
      }

      List<Func<string>> rules;
      if(!this.validationRules.TryGetValue(expression.Member.Name, out rules)) {
        lock(this.validationRules) {
          if(!this.validationRules.TryGetValue(expression.Member.Name, out rules)) {
            rules = new List<Func<string>>();
            this.validationRules.Add(expression.Member.Name, rules);
          }
        }
      }

      rules.Add(rule);
    }

    /// <summary>Removes a validation rule from a property.</summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="property">The property that is validated by the rule.</param>
    /// <param name="rule">The rule that must be removed.</param>
    protected void RemoveValidationRule<T>(Expression<Func<T>> property, Func<string> rule) {
      if(property == null) {
        throw new ArgumentNullException("property");
      }

      MemberExpression expression = property.Body as MemberExpression;
      if(expression == null) {
        throw new ArgumentException("The property must contain a MemberExpression", "property");
      }

      if(!(expression.Member is PropertyInfo)) {
        throw new ArgumentException("The expression must refer to a property", "property");
      }

      lock(this.validationRules) {
        List<Func<string>> rules;
        if(this.validationRules.TryGetValue(expression.Member.Name, out rules)) {
          rules.Remove(rule);
        }
      }
    }

    /// <summary>Determines whether the property is valid or not.</summary>
    /// <typeparam name="T">The type of property.</typeparam>
    /// <param name="property">The property that is checked.</param>
    /// <returns><see langword="true"/> if the specified property is valid; otherwise <see langword="false"/>.</returns>
    protected bool IsPropertyValid<T>(Expression<Func<T>> property) {
      if(property == null) {
        throw new ArgumentNullException("property");
      }

      MemberExpression expression = property.Body as MemberExpression;
      if(expression == null) {
        throw new ArgumentException("The property must contain a MemberExpression", "property");
      }

      if(!(expression.Member is PropertyInfo)) {
        throw new ArgumentException("The expression must refer to a property", "property");
      }

      return string.IsNullOrEmpty(((IDataErrorInfo)this)[expression.Member.Name]);
    }

    /// <summary>Raises the PropertyChanged event.</summary>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <param name="property">The property expression.</param>
    protected void RaisePropertyChanged<T>(Expression<Func<T>> property) {
      if(property == null) {
        throw new ArgumentNullException("property");
      }

      MemberExpression expression = property.Body as MemberExpression;
      if(expression == null) {
        throw new ArgumentException("The property must contain a MemberExpression", "property");
      }

      if(!(expression.Member is PropertyInfo)) {
        throw new ArgumentException("The expression must refer to a property", "property");
      }

      this.RaisePropertyChanged(expression.Member.Name);
    }

    /// <summary>Raises the PropertyChanged event and sends a PropertyChanged message.</summary>
    /// <typeparam name="T">The type of the property that changed.</typeparam>
    /// <param name="property">The property expression.</param>
    /// <param name="oldValue">The old value.</param>
    /// <param name="newValue">The new value.</param>
    protected void RaisePropertyChanged<T>(Expression<Func<T>> property, T oldValue, T newValue) {
      if(property == null) {
        throw new ArgumentNullException("property");
      }

      MemberExpression expression = property.Body as MemberExpression;
      if(expression == null) {
        throw new ArgumentException("The property must contain a MemberExpression", "property");
      }

      if(!(expression.Member is PropertyInfo)) {
        throw new ArgumentException("The expression must refer to a property", "property");
      }

      string name = expression.Member.Name;
      this.RaisePropertyChanged(name);
      this.SendPropertyChangedMessage(name, oldValue, newValue);
    }

    /// <summary>Raises the PropertyChanged event.</summary>
    /// <param name="property">Name of the property.</param>
    protected void RaisePropertyChanged(string property) {
      this.VerifyProperty(property);

      this.propertyChangedHandler.Fire(this, new PropertyChangedEventArgs(property));
    }

    /// <summary>Raises the PropertyChanged event with string.Empty. (Indicates all properties on the object have changed).</summary>
    protected void RaiseAllPropertiesChanged() {
      this.propertyChangedHandler.Fire(this, new PropertyChangedEventArgs(string.Empty));
    }

    /// <summary>Sends a <see cref="PropertyChangedMessage{T}"/> using either the instance of the Messenger that was passed to this class 
    /// (if available) or the Messenger's default instance.</summary>
    /// <typeparam name="T">The type of the property that changed.</typeparam>
    /// <param name="property">The name of the property that changed.</param>
    /// <param name="oldValue">The value of the property before it changed.</param>
    /// <param name="newValue">The value of the property after it changed.</param>
    protected void SendPropertyChangedMessage<T>(Expression<Func<T>> property, T oldValue, T newValue) {
      if(property == null) {
        throw new ArgumentNullException("property");
      }

      MemberExpression expression = property.Body as MemberExpression;

      if(expression != null && (expression.Member is PropertyInfo)) {
        this.SendPropertyChangedMessage(expression.Member.Name, oldValue, newValue);
      }
    }

    /// <summary>Sends a <see cref="PropertyChangedMessage{T}"/> using either the instance of the Messenger that was passed to this class 
    /// (if available) or the Messenger's default instance.</summary>
    /// <typeparam name="T">The type of the property that changed.</typeparam>
    /// <param name="property">The name of the property that changed.</param>
    /// <param name="oldValue">The value of the property before it changed.</param>
    /// <param name="newValue">The value of the property after it changed.</param>
    protected void SendPropertyChangedMessage<T>(string property, T oldValue, T newValue) {
      if(this.Messenger == null) {
        return;
      }

      this.VerifyProperty(property);
      this.Messenger.Send(new PropertyChangedMessage<T>(this, property, oldValue, newValue));
    }
    #endregion

    #region Private methods
    /// <summary>Warns the developer if this object does not have a public property with the specified name. This method does not exist in a 
    /// Release build.</summary>
    /// <param name="property">The name of property.</param>
    [Conditional("DEBUG")]
    [DebuggerStepThrough]
    private void VerifyProperty(string property) {
      /* Verify that the property name matches a real, public, instance property on this object. */
      if(TypeDescriptor.GetProperties(this)[property] != null) {
        return;
      }

      throw new ArgumentException("Property not found", property);
    }
    #endregion
  }
}
