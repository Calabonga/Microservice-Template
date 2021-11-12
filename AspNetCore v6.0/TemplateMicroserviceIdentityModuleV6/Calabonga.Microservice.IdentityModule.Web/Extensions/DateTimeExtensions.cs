using System;
using System.Collections.Generic;

namespace $safeprojectname$.Extensions;

/// <summary>
/// DateTime extensions
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// Each day enumeration
    /// </summary>
    /// <param name="from"></param>
    /// <param name="thru"></param>
    /// <returns></returns>
    private static IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
    {
        for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
        {
            yield return day;
        }
    }

    /// <summary>
    /// Each month enumeration
    /// </summary>
    /// <param name="from"></param>
    /// <param name="thru"></param>
    /// <returns></returns>
    private static IEnumerable<DateTime> EachMonth(DateTime from, DateTime thru)
    {
        for (var month = from.Date; month.Date <= thru.Date || month.Month == thru.Month; month = month.AddMonths(1))
        {
            yield return month;
        }
    }

    /// <summary>
    /// Enumerates all days in period
    /// </summary>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <returns></returns>
    public static IEnumerable<DateTime> EachDayTo(this DateTime dateFrom, DateTime dateTo) => EachDay(dateFrom, dateTo);

    /// <summary>
    /// Enumerates all months in period
    /// </summary>
    /// <param name="dateFrom"></param>
    /// <param name="dateTo"></param>
    /// <returns></returns>
    public static IEnumerable<DateTime> EachMonthTo(this DateTime dateFrom, DateTime dateTo) => EachMonth(dateFrom, dateTo);


    /// <summary>
    /// Total Month count between two dates
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public static int TotalMonths(this DateTime start, DateTime end) => (start.Year * 12 + start.Month) - (end.Year * 12 + end.Month);
}