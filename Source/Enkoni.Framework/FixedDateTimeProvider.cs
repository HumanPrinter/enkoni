using System;

namespace Enkoni.Framework {
  /// <summary>A provider that can be used to retrieve the current date and time using a fixed date and time.</summary>
  public class FixedDateTimeProvider : DateTimeProvider {
    #region Private fields
    /// <summary>The fixed date and time that is being used.</summary>
    private DateTime innerClock;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="FixedDateTimeProvider"/> class using a fixed date and time.</summary>
    /// <param name="fixedValue">The date and time that must be used by this provider.</param>
    public FixedDateTimeProvider(DateTime fixedValue) {
      this.innerClock = fixedValue;
    }
    #endregion

    #region Properties
    /// <summary>Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the local time.</summary>
    public override DateTime Now {
      get { return this.innerClock.ToLocalTime(); }
    }

    /// <summary>Gets the current date.</summary>
    public override DateTime Today {
      get { return this.innerClock.Date; }
    }

    /// <summary>Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).</summary>
    public override DateTime UtcNow {
      get { return this.innerClock.ToUniversalTime(); }
    }
    #endregion
  }
}
