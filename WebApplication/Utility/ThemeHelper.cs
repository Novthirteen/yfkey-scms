using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using System.Collections;

namespace com.Sconit.Utility
{
    public static class ThemeHelper
    {
        public static string GetRandomDate()
        {
            DateTime dateTimeMin = Convert.ToDateTime("2009-7-31");
            DateTime dateTimeMax = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            return GetRandomDate(dateTimeMin, dateTimeMax);
        }
        public static string GetRandomDate(DateTime dateTimeMin, DateTime dateTimeMax)
        {
            TimeSpan ts = dateTimeMax - dateTimeMin;
            Random r = new Random();
            int t1 = r.Next(1, (int)ts.TotalDays + 1);
            DateTime newDT = dateTimeMin.Add(new TimeSpan(t1, 0, 0, 0));
            return newDT.ToString("yyyy-MM-dd");
        }
    }
}
