using System;

namespace Enkoni.Framework {
  /// <summary>A provider that can be used to retrieve the current date and time in a way that can be influenced using dependency injection.</summary>
  public class DateTimeProvider {
    #region Constructors

    /// <summary>Initializes a new instance of the <see cref="DateTimeProvider"/> class.</summary>
    public DateTimeProvider() {
    }

    #endregion

    #region Properties

    /// <summary>Gets the largest possible value of <see cref="DateTime"/>.</summary>
    public virtual DateTime MaxValue {
      get { return DateTime.MaxValue; }
    }

    /// <summary>Gets the smallest possible value of <see cref="DateTime"/>.</summary>
    public virtual DateTime MinValue {
      get { return DateTime.MinValue; }
    }

    /// <summary>Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the local time.</summary>
    public virtual DateTime Now {
      get { return DateTime.Now; }
    }

    /// <summary>Gets the current date.</summary>
    public virtual DateTime Today {
      get { return DateTime.Today; }
    }

    /// <summary>Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).</summary>
    public virtual DateTime UtcNow {
      get { return DateTime.UtcNow; }
    }

    #endregion
  }
}
