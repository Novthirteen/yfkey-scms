using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity;

namespace com.Sconit.Utility
{
    public static class RepackHelper
    {
        public static string GetRepackLabel(string repackType)
        {
             return GetRepackLabel(repackType, false);
        }

        public static string GetRepackLabel(string repackType, bool withComma)
        {
            string repackLabel = string.Empty;
            if (repackType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_REPACK)
            {
                repackLabel = "${MasterData.Inventory.Repack.RepackNo.Repack}";
            }
            else if (repackType == BusinessConstants.CODE_MASTER_REPACK_TYPE_VALUE_DEVANNING)
            {
                repackLabel = "${MasterData.Inventory.Repack.RepackNo.Devanning}";
            }
            if (withComma)
            {
                repackLabel = repackLabel + ":";
            }
            return repackLabel;
        }
    }
}
