using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Utility
{
    public static class TimerHelper
    {
        public static int GetInterval(string intervalType, int interval)
        {
            return GetInterval(ConvertToIntervalType(intervalType), interval);
        }

        public static int GetInterval(IntervalType intervalType, int interval)
        {
            int result = 0;
            switch (intervalType)
            {
                case IntervalType.Days:
                    result = interval * 24 * 60 * 60 * 1000;
                    break;
                case IntervalType.Hours:
                    result = interval * 60 * 60 * 1000;
                    break;
                case IntervalType.Minutes:
                    result = interval * 60 * 1000;
                    break;
                case IntervalType.Seconds:
                    result = interval * 1000;
                    break;
                case IntervalType.Milliseconds:
                    result = interval;
                    break;
            }

            return result;
        }

        private static IntervalType ConvertToIntervalType(string intrevalTypeString)
        {
            IntervalType result = IntervalType.Milliseconds;
            if (string.Compare(intrevalTypeString, IntervalType.Days.ToString(), true) == 0)
            {
                result = IntervalType.Days;
            }

            if (string.Compare(intrevalTypeString, IntervalType.Hours.ToString(), true) == 0)
            {
                result = IntervalType.Hours;
            }

            if (string.Compare(intrevalTypeString, IntervalType.Minutes.ToString(), true) == 0)
            {
                result = IntervalType.Minutes;
            }

            if (string.Compare(intrevalTypeString, IntervalType.Seconds.ToString(), true) == 0)
            {
                result = IntervalType.Seconds;
            }

            if (string.Compare(intrevalTypeString, IntervalType.Milliseconds.ToString(), true) == 0)
            {
                result = IntervalType.Milliseconds;
            }

            return result;
        }
    }

    public enum IntervalType
    {
        Days = 100001,
        Hours,
        Minutes,
        Seconds,
        Milliseconds
    }
}
