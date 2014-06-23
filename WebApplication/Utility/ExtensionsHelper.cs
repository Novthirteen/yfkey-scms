using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using com.Sconit.Entity;

namespace com.Sconit.Utility
{
    public static class ExtensionsHelper
    {
        public static void RemoveAll<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Predicate<KeyValuePair<TKey, TValue>> condition)
        {
            var temp = new List<TKey>();

            foreach (var item in dictionary)
            {
                if (!condition(item))
                    temp.Add(item.Key);
            }

            foreach (var itemKey in temp)
            {
                dictionary.Remove(itemKey);
            }
        }

        public static TValue ValueOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value = default(TValue);
            dictionary.TryGetValue(key, out value);
            return value;
        }

        public static TValue ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value = default(TValue);
            dictionary.TryGetValue(key, out value);
            return value;
        }

        public static DateTime RoundToNearestHour(this DateTime dateTime)
        {
            dateTime += TimeSpan.FromMinutes(30);

            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, dateTime.Kind);
        }


        public static TimeSpan Round(this TimeSpan time, TimeSpan roundingInterval, MidpointRounding roundingType)
        {
            return new TimeSpan(
                Convert.ToInt64(Math.Round(
                    time.Ticks / (decimal)roundingInterval.Ticks,
                    roundingType
                )) * roundingInterval.Ticks
            );
        }

        public static TimeSpan Round(this TimeSpan time, TimeSpan roundingInterval)
        {
            return Round(time, roundingInterval, MidpointRounding.ToEven);
        }

        public static DateTime Round(this DateTime datetime, TimeSpan roundingInterval)
        {
            return new DateTime((datetime - DateTime.MinValue).Round(roundingInterval).Ticks);
        }
    }
}
