namespace Enkoni.Framework.UI.Mvvm {
  /// <summary>Interface to access the generic <see cref="PropertyChangedMessage{T}"/> class in a non generic way.</summary>
  public interface IPropertyChangedMessage : IMessage {
    #region Properties

    /// <summary>Gets the name of the property whose value changed.</summary>
    string PropertyName { get; }

    /// <summary>Gets the old value of the property.</summary>
    object OldValue { get; }

    /// <summary>Gets the new value of the property.</summary>
    object NewValue { get; }

    #endregion
  }
}
