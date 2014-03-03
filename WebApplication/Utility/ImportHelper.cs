using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace com.Sconit.Utility
{
    public static class ImportHelper
    {
        /// <summary>
        /// 跳过N行
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="count"></param>
        public static void JumpRows(IEnumerator rows, int count)
        {
            for (int i = 0; i < count; i++)
            {
                rows.MoveNext();
            }
        }
    }
}
