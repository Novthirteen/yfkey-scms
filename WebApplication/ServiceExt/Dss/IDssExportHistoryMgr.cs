using System;
using System.Collections.Generic;
using com.Sconit.Entity.Dss;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss
{
    public interface IDssExportHistoryMgr : IDssExportHistoryBaseMgr
    {
        #region Customized Methods

        void CreateDssExportHistory(IList<DssExportHistory> dssExportHistoryList);

        #endregion Customized Methods
    }
}
