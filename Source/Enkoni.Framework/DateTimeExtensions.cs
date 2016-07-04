using System;
using System.Globalization;

namespace Enkoni.Framework {
  /// <summary>This class contains extension-methods for the <see cref="DateTime"/> type.</summary>
  public static class DateTimeExtensions {
    /// <summary>Determines the week number of the given <see cref="DateTime"/> value using the ISO 8601 specification.</summary>
    /// <param name="source">The date time of which the week number must be determined.</param>
    /// <returns>The determined week number.</returns>
    public static int GetWeekNumber(this DateTime source) {
      /* This implementation is inspired on the article written by Shawn Steele which is available on
       * http://blogs.msdn.com/b/shawnste/archive/2006/01/24/517178.aspx */
      /* This implementation is slightly more complicated than simply calling GetWeekOfYear() on Calendar, because the default implementation is not
       * entirely ISO 8601 compliant (even though the documentation says it is). */

      /* Since this method calculates the weeknumber in accordance with the ISO 8601 specification, which is culture independant, the calendar of the
       * invariant culture is used. */
      Calendar calendar = CultureInfo.InvariantCulture.Calendar;

      /* Get the day of the week */
      DayOfWeek weekDay = calendar.GetDayOfWeek(source);

      /* Make sure the date points to the Thursday (or some day later in the same week) to make sure the weeknumber calculation succeeds. */
      if(weekDay >= DayOfWeek.Monday && weekDay <= DayOfWeek.Wednesday) {
        /* Since the DateTime parameter isn't passed by reference, there is no harm in overwritting the parameters value. */
        source = source.AddDays(3);
      }

      /* Now the GetWeekOfYear method can be used and it will return the correct value in accordance with the ISO 8601 specification. */
      return calendar.GetWeekOfYear(source, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }

    /// <summary>Determines whether or not a <see cref="DateTime"/> value is between to specified <see cref="DateTime"/> boundaries.</summary>
    /// <param name="source">The date time that must be tested.</param>
    /// <param name="lowerLimit">The lower bound (exclusive) of the equation.</param>
    /// <param name="upperLimit">The upper bound (exclusive) of the equation.</param>
    /// <returns><see langword="true"/> if <paramref name="source"/> is greater than <paramref name="lowerLimit"/> and smaller than <paramref name="upperLimit"/>;
    /// otherwise, <see langword="false"/>.</returns>
    public static bool Between(this DateTime source, DateTime? lowerLimit, DateTime? upperLimit) {
      return (lowerLimit.HasValue ? source > lowerLimit : true) && (upperLimit.HasValue ? source < upperLimit : true);
    }

    /// <summary>Determines whether or not a <see cref="DateTime"/> value is between to specified <see cref="DateTime"/> boundaries.</summary>
    /// <param name="source">The date time that must be tested.</param>
    /// <param name="lowerLimit">The lower bound (exclusive) of the equation.</param>
    /// <param name="upperLimit">The upper bound (exclusive) of the equation.</param>
    /// <returns><see langword="true"/> if <paramref name="source"/> is greater than <paramref name="lowerLimit"/> and smaller than <paramref name="upperLimit"/>;
    /// otherwise, <see langword="false"/>.</returns>
    public static bool Between(this DateTime? source, DateTime? lowerLimit, DateTime? upperLimit) {
      if(source.HasValue) {
        return Between(source.Value, lowerLimit, upperLimit);
      }
      else {
        return !lowerLimit.HasValue;
      }
    }
  }
}
