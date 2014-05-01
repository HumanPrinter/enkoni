using System;
using System.Threading;

using Microsoft.Win32;

namespace Enkoni.Framework.Timers {
  /// <summary>Implements a class that is capable of triggering an event at a specific time of day.</summary>
  public class AlarmClock : IDisposable {
    #region Constants
    /// <summary>Defines the number of milliseconds for each 24-hour period.</summary>
    private const int NumberOfMillisecondsPerDay = 86400000;
    #endregion

    #region Instance variables
    /// <summary>The timer that does the actual work.</summary>
    private Timer timer;

    /// <summary>Indicates if the timer is currently active.</summary>
    private bool isActive;

    /// <summary>The handler that will be executed when the alarm time has been reached.</summary>
    private EventHandler<EventArgs<object>> onAlarmHandler;

    /// <summary>The local time at which the alarm must go off.</summary>
    private TimeSpan alarmTime;

    /// <summary>Indicates if the alarm must go off each time the alarm time has been reached, or just once.</summary>
    private bool repeat;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="AlarmClock"/> class.</summary>
    public AlarmClock()
      : this(null) {
    }

    /// <summary>Initializes a new instance of the <see cref="AlarmClock"/> class.</summary>
    /// <param name="state">An object containing information to be used by the callback method, or <see langword="null"/>.</param>
    public AlarmClock(object state) {
      this.timer = new Timer(this.SettOffAlarm, state, Timeout.Infinite, Timeout.Infinite);
      SystemEvents.TimeChanged += this.AdjustTimer;
    }

    /// <summary>Initializes a new instance of the <see cref="AlarmClock"/> class.</summary>
    /// <param name="alarmTime">The time of day at which the alarm must go off. The time must be in the local time zone.</param>
    public AlarmClock(TimeSpan alarmTime)
      : this(alarmTime, null) {
    }

    /// <summary>Initializes a new instance of the <see cref="AlarmClock"/> class.</summary>
    /// <param name="alarmTime">The time of day at which the alarm must go off. The time must be in the local time zone.</param>
    /// <param name="state">An object containing information to be used by the callback method, or <see langword="null"/>.</param>
    public AlarmClock(TimeSpan alarmTime, object state)
      : this(state) {
      this.alarmTime = alarmTime;
    }
    #endregion

    #region Events
    /// <summary>Occurs when the alarm time has been reached.</summary>
    public event EventHandler<EventArgs<object>> OnAlarm {
      add { this.onAlarmHandler += value; }
      remove { this.onAlarmHandler -= value; }
    }
    #endregion

    #region Properties
    /// <summary>Gets or sets the alarm time for the alarm clock in the local time zone.</summary>
    public TimeSpan AlarmTime {
      get {
        return this.alarmTime;
      }

      set {
        this.alarmTime = value;
        this.AdjustTimer(this, null);
      }
    }

    /// <summary>Gets or sets a value indicating whether the Alarm Clock must set off an alarm each time the alarm time is reached or just once.
    /// </summary>
    public bool Repeat {
      get {
        return this.repeat;
      }

      set {
        this.repeat = value;
        this.AdjustTimer(this, null);
      }
    }
    #endregion

    #region Public methods
    /// <summary>Starts the alarm clock.</summary>
    /// <exception cref="InvalidOperationException">When this instance is already disposed.</exception>
    public void Start() {
      if(this.timer == null) {
        throw new InvalidOperationException("This instance has already been disposed.");
      }

      if(this.isActive) {
        /* If the timer is already running, there is nothing to do here */
        return;
      }

      /* Check if the alarm time will happen today of tomorrow for the first time. */
      int period = this.Repeat ? NumberOfMillisecondsPerDay : Timeout.Infinite;
      DateTime alarmDate;
      if(this.AlarmTime > DateTime.Now.TimeOfDay) {
        alarmDate = DateTime.Today.AddTicks(this.AlarmTime.Ticks);
      }
      else {
        alarmDate = DateTime.Today.AddDays(1).AddTicks(this.AlarmTime.Ticks);
      }

      this.timer.Change((int)(alarmDate - DateTime.Now).TotalMilliseconds, period);
      this.isActive = true;
    }

    /// <summary>Stops the alarm clock.</summary>
    /// <exception cref="InvalidOperationException">When this instance is already disposed.</exception>
    public void Stop() {
      if(this.timer == null) {
        throw new InvalidOperationException("This instance has already been disposed.");
      }

      this.timer.Change(Timeout.Infinite, Timeout.Infinite);
      this.isActive = false;
    }
    #endregion

    #region IDisposable implementation
    /// <summary>Disposes any resources held by this instance.</summary>
    public void Dispose() {
      this.DisposeManagedResources();
    }

    /// <summary>Disposes any resources held by this instance.</summary>
    protected virtual void DisposeManagedResources() {
      SystemEvents.TimeChanged -= this.AdjustTimer;

      if(this.timer != null) {
        this.timer.Dispose();
        this.timer = null;
      }
    }
    #endregion

    #region Private methods
    /// <summary>Adjusts the internal timer after the system time has changed or the properties of this class were changed.</summary>
    /// <param name="sender">The object that triggered the event.</param>
    /// <param name="e">Any arguments that may have been passed with the event.</param>
    private void AdjustTimer(object sender, EventArgs e) {
      this.Stop();
      this.Start();
    }

    /// <summary>Sets off the alarm when the timer has elapsed.</summary>
    /// <param name="state">An optional state object that may have been passed in the constructor.</param>
    private void SettOffAlarm(object state) {
      this.onAlarmHandler.FireAsync(this, new EventArgs<object>(state));

      if(!this.Repeat) {
        this.isActive = false;
      }
    }
    #endregion
  }
}
