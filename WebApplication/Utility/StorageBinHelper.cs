using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Utility
{
    public static class StorageBinHelper
    {
        public static bool IsStorageBinEqual(StorageBin storageBin1, StorageBin storageBin2)
        {
            if (storageBin1 == null && storageBin2 == null)
            {
                return true;
            }

            if (storageBin1 == null && storageBin2 != null)
            {
                return false;
            }

            if (storageBin1 != null && storageBin2 == null)
            {
                return false;
            }

            if (storageBin1.Code == storageBin2.Code)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
