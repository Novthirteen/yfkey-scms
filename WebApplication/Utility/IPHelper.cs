using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Utility
{
    public static class IPHelper
    {
        public static bool IsInnerIP(string ipAddress)
        {
            bool isInnerIp = false;
            int ipNum = GetIpNum(ipAddress);
            /**   
                私有IP：A类  10.0.0.0-10.255.255.255   
                B类  172.16.0.0-172.31.255.255   
                C类  192.168.0.0-192.168.255.255   
                当然，还有127这个网段是环回地址   
            **/
            int aBegin = GetIpNum("10.0.0.0");
            int aEnd = GetIpNum("10.255.255.255");
            int bBegin = GetIpNum("172.16.0.0");
            int bEnd = GetIpNum("172.31.255.255");
            int cBegin = GetIpNum("192.168.0.0");
            int cEnd = GetIpNum("192.168.255.255");
            isInnerIp = IsInner(ipNum, aBegin, aEnd) || IsInner(ipNum, bBegin, bEnd) || IsInner(ipNum, cBegin, cEnd) || ipAddress.Equals("127.0.0.1");
            return isInnerIp;
        }

        private static int GetIpNum(string ipAddress)
        {
            string[] ip = ipAddress.Split('.');
            int a = int.Parse(ip[0]);
            int b = int.Parse(ip[1]);
            int c = int.Parse(ip[2]);
            int d = int.Parse(ip[3]);

            int ipNum = a * 256 * 256 * 256 + b * 256 * 256 + c * 256 + d;
            return ipNum;
        }

        private static bool IsInner(int userIp, int begin, int end)
        {
            return (userIp >= begin) && (userIp <= end);
        }
    }
}
