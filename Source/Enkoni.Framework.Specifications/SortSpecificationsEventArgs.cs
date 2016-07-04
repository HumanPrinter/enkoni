using System;

namespace Enkoni.Framework {
  /// <summary>Represents a generic type of <see cref="EventArgs"/> that holds a single value.</summary>
  /// <typeparam name="T">The type of the value that is passed with the event.</typeparam>
  public class SortSpecificationsEventArgs<T> : EventArgs<SortSpecifications<T>> {
    #region Public constructors

    /// <summary>Initializes a new instance of the <see cref="SortSpecificationsEventArgs{T}"/> class.</summary>
    /// <param name="eventValue">The value that must be passed to the event handler.</param>
    public SortSpecificationsEventArgs(SortSpecifications<T> eventValue)
      : base(eventValue) {
    }

    #endregion
  }
}
