using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NPOI.HSSF.Record.Formula.Functions;
using System.Reflection;

namespace com.Sconit.Utility
{
    public static class LogHelper
    {
        public static void LogEntityField(log4net.ILog log, object obj)
        {
            PropertyInfo[] propertyInfoAry = obj.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfoAry)
            {
                log.Debug(obj.GetType() + "." + propertyInfo.Name + " : " + propertyInfo.GetValue(obj, null));
            }
        }
    }
}
