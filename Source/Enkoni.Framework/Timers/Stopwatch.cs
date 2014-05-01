using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using NetStopwatch = System.Diagnostics.Stopwatch;

namespace Enkoni.Framework.Timers {
  /// <summary>This class is a wrapper around the <see cref="NetStopwatch"/> class and adds support for recording lap times.</summary>
  public class Stopwatch {
    #region Instance variables
    /// <summary>The instance that does the actual work.</summary>
    private NetStopwatch internalStopwatch = new NetStopwatch();

    /// <summary>Holds the elapsed time of each lap.</summary>
    private List<TimeSpan> lapTimes = new List<TimeSpan>();

    /// <summary>Holds the current state of the stopwatch.</summary>
    private WorkflowState state = WorkflowState.Init;
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
      get {
        if(this.state == WorkflowState.Stopped) {
          return this.lapTimes.Last();
        }
        else {
          return this.internalStopwatch.Elapsed;
        }
      }
    }
    #endregion

    #region Public methods
    /// <summary>Starts or resumes the stopwatch. If the stopwatch was paused before, the current lap will be resumed; otherwise, a new lap is 
    /// started.</summary>
    public void Start() {
      this.internalStopwatch.Start();
      this.state = WorkflowState.Started;
    }

    /// <summary>Stops the stopwatch and saves the elapsed time of the current lap. Restarting the stopwatch will start a new lap.</summary>
    public void Stop() {
      TimeSpan lapTime = this.internalStopwatch.Elapsed;
      this.internalStopwatch.Stop();
      this.internalStopwatch.Reset();
      this.lapTimes.Add(lapTime);
      this.state = WorkflowState.Stopped;
    }

    /// <summary>Pauses the stopwatch. Restarting the stopwatch will resume the current lap.</summary>
    public void Pause() {
      this.internalStopwatch.Stop();
      this.state = WorkflowState.Paused;
    }

    /// <summary>Resumes the stopwatch.</summary>
    /// <remarks><see cref="Resume()"/> and <see cref="Start()"/> provide the same functionality. The <see cref="Resume()"/> method has been added to 
    /// make working with the <see cref="Stopwatch"/> class more intuitive.</remarks>
    public void Resume() {
      this.internalStopwatch.Start();
      this.state = WorkflowState.Started;
    }

    /// <summary>Resets the stopwatch and clears the lap times.</summary>
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
