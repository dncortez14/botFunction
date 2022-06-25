using System;

namespace botFunction.Extensions
{
    public static class StringExtensions
    {
        public static double? ToDouble(this string value)
        {
            double d;
            return double.TryParse(value, out d) ? d : null;
        }

        public static DateTime? ToDatetime(this string value)
        {
            DateTime d;
            return DateTime.TryParse(value, out d) ? d : null;
        }

        public static long? ToLong(this string value)
        {
            long d;
            return long.TryParse(value, out d) ? d : null;
        }
    }
}
