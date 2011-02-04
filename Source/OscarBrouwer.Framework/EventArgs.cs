//--------------------------------------------------------------------------------------------------------------------------
// <copyright file="EventArgs.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2011. All rights reserved.
// </copyright>
// <summary>
//     Holds a generic EventArgs class.
// </summary>
//--------------------------------------------------------------------------------------------------------------------------

using System;

namespace Enkoni.Framework {
  /// <summary>Represents a generic type of <see cref="EventArgs"/> that holds a single value.</summary>
  /// <typeparam name="T">The type of the value that is passed with the event.</typeparam>
  public class EventArgs<T> : EventArgs {
    #region Public constructors
    /// <summary>Initializes a new instance of the <see cref="EventArgs{T}"/> class.</summary>
    /// <param name="eventValue">The value that must be passed to the eventhandler.</param>
    public EventArgs(T eventValue) {
      this.EventValue = eventValue;
    }
    #endregion

    #region Public properties
    /// <summary>Gets the value that is passed to the eventhandler.</summary>
    public T EventValue { get; private set; }
    #endregion
  }
}
