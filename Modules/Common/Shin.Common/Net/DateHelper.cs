// ------------------------------------------------------------------------
// Shin开发平台
// 版 本：V1.0
// 版 权：Shin
// 作 者：Shin
// 邮 箱：shin_l@126.com
// ------------------------------------------------------------------------

namespace Shin.Common;

/// <summary>
/// 计算精度枚举
/// </summary>
[SuppressSniffer]
public enum EnumDateType
{
    /// <summary>
    /// 年份数
    /// </summary>
    Year,

    /// <summary>
    /// 月份数
    /// </summary>
    Month,

    /// <summary>
    /// 天数
    /// </summary>
    Day,

    /// <summary>
    /// 小时
    /// </summary>
    Hour,

    /// <summary>
    /// 分钟
    /// </summary>
    Minute,

    /// <summary>
    /// 秒
    /// </summary>
    Second
}

/// <summary>
/// 季度枚举
/// </summary>
[SuppressSniffer]
public enum QuartersType
{
    /// <summary>
    /// 不限
    /// </summary>
    [Description("不限")]
    None,

    /// <summary>
    /// 一季度
    /// </summary>
    [Description("一季度")]
    First,

    /// <summary>
    /// 二季度
    /// </summary>
    [Description("二季度")]
    Second,

    /// <summary>
    /// 三季度
    /// </summary>
    [Description("三季度")]
    Third,

    /// <summary>
    /// 四季度
    /// </summary>
    [Description("四季度")]
    Fourth
}

/// <summary>
/// 月份周数枚举
/// </summary>
[SuppressSniffer]
public enum WeekOfMonthType
{
    /// <summary>
    /// 第一周
    /// </summary>
    [Description("第一周")]
    First,

    /// <summary>
    /// 第二周
    /// </summary>
    [Description("第二周")]
    Second,

    /// <summary>
    /// 第三周
    /// </summary>
    [Description("第三周")]
    Third,

    /// <summary>
    /// 第四周
    /// </summary>
    [Description("第四周")]
    Fourth,

    /// <summary>
    /// 第五周
    /// </summary>
    [Description("第五周")]
    Fifth
}

/// <summary>
/// 日期计算类
/// </summary>
public class DateHelper
{
    /// <summary>
    /// 获取当月1号的日期
    /// </summary>
    public static DateTime ThisMonthFirstDay
    {
        get
        {
            int day = DateTime.Now.Day;
            int dif = 1 - day;
            return GetAdd(DateTime.Now, dif, EnumDateType.Day).ParseToDateTime("yyyy-MM-dd 00:00:00");
        }
    }

    /// <summary>
    /// 获取当月最后一天的日期.
    /// </summary>
    public static DateTime ThisMonthLastDay
    {
        get
        {
            DateTime nextMonth = DateTime.Now.AddMonths(1).ParseToDateTime("yyyy-MM-01 00:00:00");
            return nextMonth.AddSeconds(-1);
        }
    }

    /// <summary>
    /// 获取date1减去date2后，指定类型的差
    /// </summary>
    /// <param name="date1">日期参数</param>
    /// <param name="date2">日期参数</param>
    /// <param name="doType">计算类型</param>
    /// <returns>指定类型的计算结果</returns>
    public static int GetSubtract(DateTime date1, DateTime date2, EnumDateType doType)
    {
        TimeSpan ts1 = new TimeSpan(date1.Ticks);
        TimeSpan ts2 = new TimeSpan(date2.Ticks);
        TimeSpan ts = ts1.Subtract(ts2);

        int result;
        switch (doType)
        {
            case EnumDateType.Year:

                result = date1.Year - date2.Year;

                break;

            case EnumDateType.Month:

                result = (date1.Year - date2.Year) * 12 + (date1.Month - date2.Month);

                break;

            case EnumDateType.Day:

                result = ts.Days;

                break;

            case EnumDateType.Hour:

                result = ts.Hours;

                break;

            case EnumDateType.Minute:

                result = ts.Minutes;

                break;

            case EnumDateType.Second:

                result = ts.Seconds;

                break;

            default:

                result = -1;

                break;
        }

        return result;
    }

    /// <summary>
    /// 日期增加指定类型的值，返回一个新的日期
    /// </summary>
    /// <param name="date">原日期</param>
    /// <param name="AddNum">值</param>
    /// <param name="doType">计算类型</param>
    /// <returns>计算后的新日期</returns>
    public static DateTime GetAdd(DateTime date, int AddNum, EnumDateType doType)
    {
        DateTime newDate;

        switch (doType)
        {
            case EnumDateType.Year:

                newDate = date.AddYears(AddNum);

                break;

            case EnumDateType.Month:

                newDate = date.AddMonths(AddNum);

                break;

            case EnumDateType.Day:

                newDate = date.AddDays(AddNum);

                break;

            case EnumDateType.Hour:

                newDate = date.AddHours(AddNum);

                break;

            case EnumDateType.Minute:

                newDate = date.AddMinutes(AddNum);

                break;

            case EnumDateType.Second:

                newDate = date.AddSeconds(AddNum);

                break;

            default:

                newDate = date;

                break;
        }

        return newDate;
    }

    /// <summary>
    /// 补全日期格式
    /// </summary>
    /// <param name="DateStr">字符串日期</param>
    /// <returns>已补全的字符串日期(可转换成DateTime)</returns>
    public static string GetDateByZZBDate(string DateStr)
    {
        string str = string.Empty;
        if (DateStr.Equals("0000") || string.IsNullOrEmpty(DateStr))
        {
            return string.Empty;
        }
        if (DateStr.Length == 8)
        {
            str = DateStr;
        }
        else if (DateStr.Length == 6)
        {
            str = DateStr + "01";
        }
        else if (DateStr.Length == 4)
        {
            str = DateStr + "0101";
        }
        else
        {
            str = string.Empty;
        }
        if ((str.Length == 8) && ((Convert.ToInt32(str) < Convert.ToInt32("19000101")) || (Convert.ToInt32(str) > Convert.ToInt32("20500101"))))
        {
            str = string.Empty;
        }
        return str;
    }

    public static int GetMonthDifference(DateTime startDate, DateTime endDate)
    {
        int yearsDifference = endDate.Year - startDate.Year;
        int monthsDifference = endDate.Month - startDate.Month;

        // 如果开始日期的年份比结束日期晚，则减去1年和12个月
        if (startDate.Year > endDate.Year)
        {
            yearsDifference--;
            monthsDifference += 12;
        }

        // 计算天数差值并转换为月份
        int daysDifference = endDate.Day > startDate.Day ?
                             endDate.Day - startDate.Day :
                             DateTime.DaysInMonth(startDate.Year, startDate.Month) - startDate.Day + endDate.Day;

        int monthDifference = yearsDifference * 12 + monthsDifference + (daysDifference >= DateTime.DaysInMonth(endDate.Year, endDate.Month) / 2 ? 1 : 0);

        return monthDifference;
    }

    /// <summary>
    /// 计算月份精确时间差
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static decimal CalculateMonthDifference(DateTime start, DateTime end)
    {
        TimeSpan duration = end - start;
        double totalMonths = duration.TotalDays / 30; // 大致月份差

        // 如果需要精确的月份差，可以使用下面的方法
        // 这需要考虑闰年和月份天数的不同
        int months = (end.Year - start.Year) * 12 + end.Month - start.Month;
        double fraction = (duration.Days + duration.TotalHours / 24) / duration.TotalDays;
        double preciseMonths = months + fraction;

        return Convert.ToDecimal(preciseMonths);
    }

    /// <summary>
    /// 判断两个时间段是否有重叠
    /// </summary>
    /// <param name="startTime1">时间段1的开始时间</param>
    /// <param name="endTime1">时间段1的结束时间</param>
    /// <param name="startTime2">时间段2的开始时间</param>
    /// <param name="endTime2">时间段2的结束时间</param>
    /// <returns></returns>
    public static bool IsOverlap(DateTime startTime1, DateTime endTime1, DateTime startTime2, DateTime endTime2)
    {
        return !(endTime1 <= startTime2 || startTime1 >= endTime2);
        //根据德摩根定律，等效为：endTime1 >= startTime2 && startTime1 <= endTime2
    }

    /// <summary>
    /// 获取季度的日期范围
    /// </summary>
    /// <param name="year">年份</param>
    /// <param name="_quarter">季度类型</param>
    /// <param name="endDate">季度结束日期</param>
    /// <returns>季度开始日期</returns>
    public static DateTime? GetQuartersDate(string year, QuartersType _quarter, out DateTime? endDate)
    {
        DateTime? startDate = null;
        endDate = null;

        if (string.IsNullOrEmpty(year))
            return null;

        switch (_quarter)
        {
            case QuartersType.First:

                startDate = new DateTime(year.ParseToInt(), 1, 1, 0, 0, 0);
                endDate = new DateTime(year.ParseToInt(), 3, 31, 23, 59, 59);

                break;

            case QuartersType.Second:

                startDate = new DateTime(year.ParseToInt(), 4, 1, 0, 0, 0);
                endDate = new DateTime(year.ParseToInt(), 6, 30, 23, 59, 59);

                break;

            case QuartersType.Third:

                startDate = new DateTime(year.ParseToInt(), 7, 1, 0, 0, 0);
                endDate = new DateTime(year.ParseToInt(), 9, 30, 23, 59, 59);

                break;

            case QuartersType.Fourth:

                startDate = new DateTime(year.ParseToInt(), 10, 1, 0, 0, 0);
                endDate = new DateTime(year.ParseToInt(), 12, 31, 23, 59, 59);

                break;

            default:
                return null;
        }

        return startDate;
    }

    /// <summary>
    /// 计算出某年月下各周的日期范围
    /// </summary>
    /// <param name="year">年份</param>
    /// <param name="month">月份</param>
    /// <param name="hasToday">是否包含今天</param>
    /// <returns></returns>
    public static List<DateRange> GetWeekDatesInMonth(int year, int month, bool hasToday = true)
    {
        try
        {
            List<DateRange> range = new List<DateRange>();
            DateTime firstDayOfMonth = new DateTime(year, month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            if (lastDayOfMonth > DateTime.Today)
                lastDayOfMonth = hasToday ? DateTime.Today : DateTime.Today.AddDays(-1);
            while (firstDayOfMonth <= lastDayOfMonth)
            {
                int firstDayOfWeek = firstDayOfMonth.DayOfWeek == DayOfWeek.Sunday
                    ? 7
                    : (int)firstDayOfMonth.DayOfWeek;
                int addDays = 7 - firstDayOfWeek;
                DateTime endDate = firstDayOfMonth.AddDays(addDays);
                if (endDate > lastDayOfMonth)
                    endDate = lastDayOfMonth;
                range.Add(new DateRange { StartDate = firstDayOfMonth.ParseToDateTime("yyyy-MM-dd 00:00:00"), EndDate = endDate.ParseToDateTime("yyyy-MM-dd 23:59:59") });
                firstDayOfMonth = endDate.AddDays(1);
            }

            return range;
        }
        catch (Exception)
        {
            return new List<DateRange>();
        }
    }

    /// <summary>
    /// 获取查询日期范围.
    /// </summary>
    public static DateRange GetDateRange(int? dateType)
    {
        DateRange range = new DateRange();

        if (!dateType.IsNullOrEmpty())
        {
            switch (dateType)
            {
                case 1:     // 今日

                    range.StartDate = DateTime.Now.ParseToDateTime("yyyy-MM-dd 00:00:00");
                    range.EndDate = DateTime.Now.ParseToDateTime("yyyy-MM-dd 23:59:59");

                    break;

                case 2:    //昨日

                    DateTime yesterday = DateTime.Now.AddDays(-1);
                    range.StartDate = yesterday.ParseToDateTime("yyyy-MM-dd 00:00:00");
                    range.EndDate = yesterday.ParseToDateTime("yyyy-MM-dd 23:59:59");

                    break;

                case 3:     // 本周

                    // 1.获取本月中所有周的日期范围
                    List<DateRange> weekRanges = GetWeekDatesInMonth(DateTime.Now.Year, DateTime.Now.Month, true);
                    // 2.得到距离今日最近的这一周的日期
                    range = weekRanges.Last();

                    break;

                case 4:     //上周

                    // 1.获取本月中所有周的日期范围
                    weekRanges = GetWeekDatesInMonth(DateTime.Now.Year, DateTime.Now.Month, true);

                    if (weekRanges.Count == 1)
                    {
                        // 获取上月中所有周的日期范围
                        weekRanges = GetWeekDatesInMonth(DateTime.Now.Year, DateTime.Now.Month - 1, true);
                        // 2.得到距离今日最近的这一周的日期
                        range = weekRanges.Last();
                    }
                    else
                    {
                        range = weekRanges[weekRanges.Count - 2];
                    }

                    break;

                case 5:     // 本月

                    range.StartDate = DateHelper.ThisMonthFirstDay;
                    range.EndDate = DateHelper.ThisMonthLastDay;

                    break;

                case 6:     // 上月

                    DateTime thisMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ParseToDateTime("yyyy-MM-dd 00:00:00");
                    range.StartDate = new DateTime(thisMonth.Year, thisMonth.Month - 1, 1).ParseToDateTime("yyyy-MM-dd 00:00:00");
                    range.EndDate = thisMonth.AddDays(-1).ParseToDateTime("yyyy-MM-dd 23:59:59");

                    break;

                case 7:     // 本年

                    range.StartDate = new DateTime(DateTime.Now.Year, 1, 1).ParseToDateTime("yyyy-MM-dd 00:00:00");
                    range.EndDate = new DateTime(DateTime.Now.Year, 12, 31).ParseToDateTime("yyyy-MM-dd 23:59:59");

                    break;

                    // 日期范围枚举：1.今日；2.昨日；3.本周；4.本月；5.上月；6.本年；.
            }
        }

        return range;
    }

    /// <summary>
    /// 指定时间戳转为时间。
    /// </summary>
    /// <param name="timeStamp">需要被反转的时间戳</param>
    /// <param name="accurateToMilliseconds">是否精确到毫秒</param>
    /// <returns>返回时间戳对应的DateTime</returns>
    public static DateTime TimeStampToDate(long timeStamp, bool accurateToMilliseconds = false)
    {
        if (accurateToMilliseconds)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(timeStamp).LocalDateTime;
        }
        else
        {
            return DateTimeOffset.FromUnixTimeSeconds(timeStamp).LocalDateTime;
        }
    }
}