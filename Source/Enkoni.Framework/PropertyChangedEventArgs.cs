using System.ComponentModel;

namespace Enkoni.Framework {
  /// <summary>Represents a generic type of <see cref="PropertyChangedEventArgs"/> that holds both the old and new value of a changed property.</summary>
  /// <typeparam name="T">The type of the value that is passed with the event.</typeparam>
  public class PropertyChangedEventArgs<T> : PropertyChangedEventArgs {
    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="PropertyChangedEventArgs{T}"/> class.</summary>
    /// <param name="propertyName">The name of the property that changed.</param>
    /// <param name="oldValue">The old (previous) value of the property.</param>
    /// <param name="newValue">The new (current) value of the property.</param>
    public PropertyChangedEventArgs(string propertyName, T oldValue, T newValue)
        : base(propertyName) {
      this.OldValue = oldValue;
      this.NewValue = newValue;
    }
    #endregion

    #region Properties
    /// <summary>Gets the old (previous) value of the property.</summary>
    public T OldValue { get; private set; }

    /// <summary>Gets the new (current) value of the property.</summary>
    public T NewValue { get; private set; }
    #endregion
  }
}
