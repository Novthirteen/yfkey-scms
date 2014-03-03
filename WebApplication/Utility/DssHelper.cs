using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.Dss;

namespace com.Sconit.Utility
{
    public static class DssHelper
    {
        public static string GetEventValue(string eventCode)
        {
            if (eventCode == BusinessConstants.DSS_EVENT_CODE_CREATE ||
                eventCode == BusinessConstants.DSS_EVENT_CODE_UPDATE)
            {
                return "1";
            }
            else if (eventCode == BusinessConstants.DSS_EVENT_CODE_DELETE)
            {
                return "0";
            }
            else
            {
                throw new TechnicalException("Undefined value");
            }
        }

        public static string FormatDate(DateTime date, string externalSystemCode)
        {
            if (externalSystemCode == BusinessConstants.DSS_SYSTEM_CODE_QAD)
            {
                return date.ToString("MM") + "/" + date.ToString("dd") + "/" + date.ToString("yy");//Example:10/05/11
            }

            return date.ToString("yyyy-MM-dd");
        }

        public static DateTime GetDate(string timeStr, string externalSystemCode)
        {
            if (externalSystemCode == BusinessConstants.DSS_SYSTEM_CODE_QAD)
            {
                //Example:"06/18/10" to "2010-06-18"
                string[] timeArray = timeStr.Split('/');
                return Convert.ToDateTime("20" + timeArray[2] + "-" + timeArray[0] + "-" + timeArray[1]);
            }

            return DateTime.Now;
        }

        public static DateTime GetDate(string date, string time, string externalSystemCode)
        {
            if (externalSystemCode == BusinessConstants.DSS_SYSTEM_CODE_MES_YFK)
            {
                //Example:"07/07/2010","14:28:28" to "2010-07-07 14:28:28"
                string[] dateStr = date.Split('/');
                return Convert.ToDateTime("20" + dateStr[2] + "-" + dateStr[0] + "-" + dateStr[1] + " " + time);
            }

            return DateTime.Now;
        }

        public static string GetBillKeyCode(DssExportHistory dssExportHistory, string billNo)
        {
            if (dssExportHistory.DssOutboundControl.ExternalSystem.Code == BusinessConstants.DSS_SYSTEM_CODE_QAD)
            {
                string prefix = billNo.Substring(0, 1);
                if (dssExportHistory.DssOutboundControl.ExternalSystem.Prefix2 != null && dssExportHistory.DssOutboundControl.ExternalSystem.Prefix2.Trim() != string.Empty)
                    prefix = dssExportHistory.DssOutboundControl.ExternalSystem.Prefix2;

                string keyCode = prefix + billNo.Remove(0, billNo.Length - 7);
                return keyCode;
            }

            return billNo;
        }

        public static void FormatDeleteData(string[] data, string externalSystemCode)
        {
            if (data != null)
            {
                for (int i = 0; i < data.Length; i++)
                {
                    data[i] = FormatDeleteDataString(data[i], externalSystemCode);
                }
            }
        }

        public static string FormatDeleteDataString(string str, string externalSystemCode)
        {
            if (externalSystemCode == BusinessConstants.DSS_SYSTEM_CODE_QAD)
            {
                if (str != null && str.Length > 0)
                    return str.Trim('"');
            }

            return str;
        }
    }
}
