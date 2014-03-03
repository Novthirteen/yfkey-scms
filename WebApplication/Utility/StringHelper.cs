using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Utility
{
    public static class StringHelper
    {
        /// <summary>
        /// "Code [Description]"
        /// </summary>
        /// <param name="code"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        public static string GetCodeDescriptionString(string code, string description)
        {
            if (code == null || code.Trim() == string.Empty)
                code = string.Empty;
            if (description == null || description.Trim() == string.Empty)
                description = string.Empty;

            if (description == string.Empty)
                return code;
            else
                return code + " [" + description + "]";
        }

        public static string SubStr(string sString, int nLeng)
        {
            int totalLength = 0;
            int currentIndex = 0;
            while (totalLength < nLeng && currentIndex < sString.Length)
            {
                if (sString[currentIndex] < 0 || sString[currentIndex] > 255)
                    totalLength += 2;
                else
                    totalLength++;

                currentIndex++;
            }

            if (currentIndex < sString.Length)
                return sString.Substring(0, currentIndex) + "...";
            else
                return sString.ToString();
        }

        /// <summary>
        /// Sconit Common String Comparer, ignore case, support Null
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool Eq(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }

    }
}
