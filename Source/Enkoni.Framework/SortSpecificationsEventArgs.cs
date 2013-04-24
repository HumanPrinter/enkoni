//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="SortSpecificationsEventArgs.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2013. All rights reserved.
// </copyright>
// <summary>
//     Holds a specific EventArgs class that is used bny the specificationssystem.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;

namespace Enkoni.Framework {
  /// <summary>Represents a generic type of <see cref="EventArgs"/> that holds a single value.</summary>
  /// <typeparam name="T">The type of the value that is passed with the event.</typeparam>
  public class SortSpecificationsEventArgs<T> : EventArgs<SortSpecifications<T>> {
    #region Public constructors
    /// <summary>Initializes a new instance of the <see cref="SortSpecificationsEventArgs{T}"/> class.</summary>
    /// <param name="eventValue">The value that must be passed to the eventhandler.</param>
    public SortSpecificationsEventArgs(SortSpecifications<T> eventValue)
      : base(eventValue) {
    }
    #endregion
  }
}
