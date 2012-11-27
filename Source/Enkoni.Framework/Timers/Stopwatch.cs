//---------------------------------------------------------------------------------------------------------------------------------------------------
// <copyright file="Stopwatch.cs" company="Oscar Brouwer">
//     Copyright (c) Oscar Brouwer 2012. All rights reserved.
// </copyright>
// <summary>
//     Defines a class that adds support for lap time recording to the default .Net stopwatch.
// </summary>
//---------------------------------------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using NetStopwatch = System.Diagnostics.Stopwatch;

namespace Enkoni.Framework.Timers {
  /// <summary>This class is a wrapper around the <see cref="NetStopwatch"/> class and adds support for recording lap times.</summary>
  public class Stopwatch {
    #region Instance variables
    /// <summary>The instance that does the actual work.</summary>
    private NetStopwatch internalStopwatch = new NetStopwatch();

    /// <summary>Holds the elapsed time of each lap.</summary>
    private List<TimeSpan> lapTimes = new List<TimeSpan>();
    #endregion

    #region Constructor
    /// <summary>Initializes a new instance of the <see cref="Stopwatch"/> class.</summary>
    public Stopwatch() {
    }
    #endregion

    #region Properties
    /// <summary>Gets the lap times of each completed lap.</summary>
    public ReadOnlyCollection<TimeSpan> LapTimes {
      get { return new ReadOnlyCollection<TimeSpan>(this.lapTimes); }
    }

    /// <summary>Gets the elapsed time of the current lap.</summary>
    public TimeSpan CurrentLapTime {
      get { return this.internalStopwatch.Elapsed; }
    }
    #endregion

    #region Public methods
    /// <summary>Starts or resumes the stopwatch.</summary>
    public void Start() {
      this.internalStopwatch.Start();
    }

    /// <summary>Stops or pauses the stopwatch.</summary>
    public void Stop() {
      this.internalStopwatch.Stop();
    }

    /// <summary>Resets the stopwatch and clears the laptimes.</summary>
    public void Reset() {
      this.internalStopwatch.Reset();
      this.lapTimes.Clear();
    }

    /// <summary>Saves the elapsed time of the current lap and starts recording the lap time of the next lap.</summary>
    /// <returns>The lap time of the last completed lap.</returns>
    public TimeSpan NewLap() {
      TimeSpan lapTime = this.internalStopwatch.Elapsed;
      this.internalStopwatch.Reset();
      this.internalStopwatch.Start();
      this.lapTimes.Add(lapTime);
      return lapTime;
    }
    #endregion
  }
}
