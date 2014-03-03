using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeanEngine.Utility
{
    public static class Utilities
    {
        /// <summary>
        /// String comparer,ignore case
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool StringEq(string a, string b)
        {
            return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        }
    }
}
