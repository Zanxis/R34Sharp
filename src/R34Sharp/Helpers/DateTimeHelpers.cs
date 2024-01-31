using System;
using System.Globalization;

namespace R34Sharp.Helpers
{
    internal static class DateTimeHelpers
    {
        internal static DateTime R34Parse(string datetimeString, string format)
        {
            DateTimeOffset date = DateTimeOffset.ParseExact(datetimeString, format, CultureInfo.InvariantCulture);
            return date.DateTime;
        }
    }
}
