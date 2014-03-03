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

        #endregion
    }
}