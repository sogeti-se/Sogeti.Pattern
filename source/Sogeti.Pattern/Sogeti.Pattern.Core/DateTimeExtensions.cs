using System;
using System.Globalization;

namespace Sogeti.Pattern
{
    /// <summary>
    ///   Extension methods for <see cref="DateTime" /> .
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        ///   Returns the first day of the week that the specified date is in using the current culture.
        /// </summary>
        public static DateTime GetFirstDateOfWeek(this DateTime date)
        {
            return GetFirstDateOfWeek(date, CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///   Returns the first day of the week that the specified date date is in.
        /// </summary>
        /// <param name="date"> Any date </param>
        /// <param name="cultureInfo"> Culture used when finding firs day of week </param>
        public static DateTime GetFirstDateOfWeek(this DateTime date, CultureInfo cultureInfo)
        {
            if (cultureInfo == null) throw new ArgumentNullException("cultureInfo");
            var firstDay = cultureInfo.DateTimeFormat.FirstDayOfWeek;
            var firstDateInWeek = date.Date;
            while (firstDateInWeek.DayOfWeek != firstDay)
                firstDateInWeek = firstDateInWeek.AddDays(-1);

            return firstDateInWeek;
        }
    }
}