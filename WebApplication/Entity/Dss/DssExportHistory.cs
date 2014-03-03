using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public class DssExportHistory : DssExportHistoryBase
    {
        #region Non O/R Mapping Properties

        public int OriginalId { get; set; }

        public IList<DssExportHistoryDetail> DssExportHistoryDetails { get; set; }

        public int OrderDetailId { get; set; }

        public override bool Equals(object obj)
        {
            DssExportHistory another = obj as DssExportHistory;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.OriginalId == another.OriginalId);
            }
        }
        #endregion
    }
}