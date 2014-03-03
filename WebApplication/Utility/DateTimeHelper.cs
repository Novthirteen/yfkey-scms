using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity;

namespace com.Sconit.Utility
{
    public static class DateTimeHelper
    {
        public static DateTime GetStartTime(string timePeriodType, DateTime startTime)
        {
            DateTime time = startTime;
            //天起始
            if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY || timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_HOUR)
            {
                time = startTime.Date;
            }
            //周起始
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
            {
                int dayOfWeek = (int)(startTime.DayOfWeek);
                double days = -6;
                if (dayOfWeek > 0)
                    days = 1 - (double)dayOfWeek;

                time = startTime.Date.AddDays(days);
            }
            //月起始
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH)
            {
                time = new DateTime(startTime.Date.Year, startTime.Date.Month, 1);
            }
            //季起始
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_QUARTER)
            {
                int month = startTime.Date.Month;
                if (month < 4)
                    time = new DateTime(startTime.Date.Year, 1, 1);
                else if (month < 7)
                    time = new DateTime(startTime.Date.Year, 4, 1);
                else if (month < 10)
                    time = new DateTime(startTime.Date.Year, 7, 1);
                else
                    time = new DateTime(startTime.Date.Year, 10, 1);
            }
            //年起始
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_YEAR)
            {
                time = new DateTime(startTime.Date.Year, 1, 1);
            }

            return time;
        }

        public static DateTime GetEndStartTime(string timePeriodType, DateTime endTime)
        {
            if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_HOUR)
            {
                return endTime.Date.AddDays(1).AddHours(-1);
            }
            else
            {
                return GetStartTime(timePeriodType, endTime);
            }
        }

        public static DateTime GetEndTime(string timePeriodType, DateTime startTime)
        {
            DateTime nextStartTime = startTime;
            //天结束
            if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY || timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_HOUR)
            {
                nextStartTime = startTime.Date.AddDays(1);
            }
            //周结束
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
            {
                nextStartTime = GetStartTime(timePeriodType, startTime).AddDays(7);
            }
            //月结束
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH)
            {
                nextStartTime = GetStartTime(timePeriodType, startTime).AddMonths(1);
            }
            //季结束
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_QUARTER)
            {
                int month = startTime.Date.Month;
                if (month < 4)
                    nextStartTime = new DateTime(startTime.Date.Year, 3, 1);
                else if (month < 7)
                    nextStartTime = new DateTime(startTime.Date.Year, 6, 1);
                else if (month < 10)
                    nextStartTime = new DateTime(startTime.Date.Year, 9, 1);
                else
                    nextStartTime = new DateTime(startTime.Date.Year, 12, 1);

                nextStartTime = nextStartTime.AddMonths(1);
            }
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_YEAR)
            {
                nextStartTime = GetStartTime(timePeriodType, startTime).AddYears(1);
            }

            DateTime time = nextStartTime.AddMinutes(-1);
            return time;
        }

        public static DateTime GetNextStartTime(string timePeriodType, DateTime startTime)
        {
            DateTime nextStartTime = startTime;
            //下一天
            if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
            {
                nextStartTime = startTime.Date.AddDays(1);
            }
            //下一周
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_WEEK)
            {
                nextStartTime = GetStartTime(timePeriodType, startTime).AddDays(7);
            }
            //下一月
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_MONTH)
            {
                nextStartTime = GetStartTime(timePeriodType, startTime).AddMonths(1);
            }
            //下一季
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_QUARTER)
            {
                int month = startTime.Date.Month;
                if (month < 4)
                    nextStartTime = new DateTime(startTime.Date.Year, 3, 1);
                else if (month < 7)
                    nextStartTime = new DateTime(startTime.Date.Year, 6, 1);
                else if (month < 10)
                    nextStartTime = new DateTime(startTime.Date.Year, 9, 1);
                else
                    nextStartTime = new DateTime(startTime.Date.Year, 12, 1);

                nextStartTime = nextStartTime.AddMonths(1);
            }
            //下一年
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_YEAR)
            {
                nextStartTime = GetStartTime(timePeriodType, startTime).AddYears(1);
            }
            //下一小时
            else if (timePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_HOUR)
            {
                nextStartTime = startTime.AddHours(1);
            }

            return nextStartTime;
        }

        public static DateTime GetWeekStart(DateTime date)
        {
            int dayOfWeek = (int)(date.DayOfWeek);
            double days = -6;
            if (dayOfWeek > 0)
                days = 1 - (double)dayOfWeek;

            return date.Date.AddDays(days);
        }

        public static string GetQuarter(DateTime date)
        {
            int month = date.Month;
            if (month < 4)
                return "Q1";
            else if (month < 7)
                return "Q2";
            else if (month < 10)
                return "Q3";
            else
                return "Q4";
        }

        public static int GetQuarterIndex(DateTime date)
        {
            int month = date.Month;
            if (month < 4)
                return 1;
            else if (month < 7)
                return 2;
            else if (month < 10)
                return 3;
            else
                return 4;
        }

        public static int GetWeekIndex(DateTime date)
        {
            DateTime yearStart = new DateTime(date.Date.Year, 1, 1);
            DateTime weekStart = GetWeekStart(date);
            DateTime firstWeekStartDate = GetWeekStart(yearStart);
            int dayOfWeek = (int)yearStart.DayOfWeek;
            if (dayOfWeek != 1)
                firstWeekStartDate = firstWeekStartDate.AddDays(7);

            TimeSpan ts = weekStart - firstWeekStartDate;
            int days = ts.Days;
            int weekIndex = days / 7 + 1;

            return weekIndex;
        }

    }
}
