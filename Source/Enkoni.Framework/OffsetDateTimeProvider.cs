using System;

namespace Enkoni.Framework {
  /// <summary>A provider that can be used to retrieve the current date and time but with a specified offset applied.</summary>
  public class OffsetDateTimeProvider : DateTimeProvider {
    #region Private fields
    /// <summary>The offset that is applied to the current date and time.</summary>
    private TimeSpan offset;
    #endregion

    #region Constructors
    /// <summary>Initializes a new instance of the <see cref="OffsetDateTimeProvider"/> class using a specified offset to the current date and time.</summary>
    /// <param name="offset">A negative of positive offset that will be added to the <see cref="DateTime"/> values that are retrieved through the properties and methods of this class.</param>
    /// <exception cref="ArgumentOutOfRangeException">The sum of the current date and time and <paramref name="offset"/> results in a value that is less than <see cref="DateTime.MinValue"/> or 
    /// greater than <see cref="DateTime.MaxValue"/>.</exception>
    public OffsetDateTimeProvider(TimeSpan offset) {
      if (offset < TimeSpan.Zero && DateTime.Now.Ticks + offset.Ticks < DateTime.MinValue.Ticks) {
        throw new ArgumentOutOfRangeException("offset", offset, "The specified offset would result in a DateTime that cannot be displayed.");
      }
      else if(offset > TimeSpan.Zero && (DateTime.Now.Ticks + offset.Ticks > DateTime.MaxValue.Ticks || DateTime.Now.Ticks + offset.Ticks < offset.Ticks)) {
        /* Either the offset caused the datetime to go beyond the maxvalue or the sum of ticks causes an overflow in which case the maxvalue is definitely passed */
        throw new ArgumentOutOfRangeException("offset", offset, "The specified offset would result in a DateTime that cannot be displayed.");
      }

      this.offset = offset;
    }
    #endregion

    #region Properties
    /// <summary>Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the local time.</summary>
    /// <exception cref="ArgumentOutOfRangeException">The sum of the current date and time and the offset results in a value that is less than <see cref="DateTime.MinValue"/> or 
    /// greater than <see cref="DateTime.MaxValue"/>.</exception>
    public override DateTime Now {
      get { return DateTime.Now + this.offset; }
    }

    /// <summary>Gets the current date.</summary>
    /// <exception cref="ArgumentOutOfRangeException">The sum of the current date and time and the offset results in a value that is less than <see cref="DateTime.MinValue"/> or 
    /// greater than <see cref="DateTime.MaxValue"/>.</exception>
    public override DateTime Today {
      get { return this.Now.Date; }
    }

    /// <summary>Gets a <see cref="DateTime"/> object that is set to the current date and time on this computer, expressed as the Coordinated Universal Time (UTC).</summary>
    /// <exception cref="ArgumentOutOfRangeException">The sum of the current date and time and the offset results in a value that is less than <see cref="DateTime.MinValue"/> or 
    /// greater than <see cref="DateTime.MaxValue"/>.</exception>
    public override DateTime UtcNow {
      get { return DateTime.UtcNow + this.offset; }
    }
    #endregion
  }
}
