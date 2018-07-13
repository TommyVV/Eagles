using System;
using System.Globalization;

namespace Eagles.Base.Utility
{
    public static class Utility
    {
        public static string FormartDatetime(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss ");
        }

        public static DateTime ConvertToDateTime(this string datetime)
        {
            var newDatetime = DateTime.Parse(datetime, null);
            return newDatetime;
        }
    }
}
