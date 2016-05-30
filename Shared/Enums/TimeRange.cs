
using System;

namespace Shared.Enums
{
    public enum TimeRange
    {
        Week = 1,
        Month = 2,
        Quarter = 3,
        Year = 4,
    }

    public static class TimeRangeMethods
    {

        public static DateTime ToDateSince(this TimeRange tr)
        {
            switch (tr)
            {
                case TimeRange.Week:
                    return DateTime.Now.AddDays(-7).Date;
                case TimeRange.Month:
                    return DateTime.Now.AddMonths(-1).Date;
                case TimeRange.Quarter:
                    return DateTime.Now.AddMonths(-3).Date;
                case TimeRange.Year:
                    return DateTime.Now.AddYears(-1).Date;
                default:
                    return new DateTime();
            }
        }
    }
}

