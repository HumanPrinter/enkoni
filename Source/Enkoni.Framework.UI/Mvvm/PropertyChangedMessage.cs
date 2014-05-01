using System;

namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Defines a message that can be used to inform about a changed property value.</summary>
  /// <typeparam name="T">The type of the property that changed.</typeparam>
  [Serializable]
  public class PropertyChangedMessage<T> : Message, IPropertyChangedMessage {
    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="PropertyChangedMessage{T}"/> class.</summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    public PropertyChangedMessage(string propertyName, T oldValue, T newValue)
      : this(null, propertyName, oldValue, newValue) {
    }

    /// <summary>Initializes a new instance of the <see cref="PropertyChangedMessage{T}"/> class.</summary>
    /// <param name="sender">The sender of the message.</param>
    /// <param name="propertyName">The name of the property that changed.</param>
    /// <param name="oldValue">The old value of the property.</param>
    /// <param name="newValue">The new value of the property.</param>
    public PropertyChangedMessage(object sender, string propertyName, T oldValue, T newValue)
      : base(sender) {
        this.PropertyName = propertyName;
      this.OldValue = oldValue;
      this.NewValue = newValue;
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the name of the property.</summary>
    public string PropertyName { get; protected set; }

    /// <summary>Gets or sets the old value.</summary>
    public T OldValue { get; protected set; }

    /// <summary>Gets the old value.</summary>
    object IPropertyChangedMessage.OldValue {
      get { return this.OldValue; }
    }

    /// <summary>Gets or sets the new value.</summary>
    public T NewValue { get; protected set; }

    /// <summary>Gets the new value.</summary>
    object IPropertyChangedMessage.NewValue {
      get { return this.NewValue; }
    }
    #endregion
  }
}
