using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public class DssExportHistoryDetail : DssExportHistoryDetailBase
    {
        #region Non O/R Mapping Properties

        public int OriginalId { get; set; }

        #endregion
    }
}