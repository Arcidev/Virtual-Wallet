using System;

namespace Shared.Formatters
{
    public static class DateTimeFormatter
    {
        public static string ToShortDate(DateTime dateTime)
        {
            return dateTime.ToString("dd.MM.yyyy");
        }
    }
}
