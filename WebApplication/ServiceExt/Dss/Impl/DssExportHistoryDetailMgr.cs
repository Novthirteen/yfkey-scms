using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Dss;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Dss.Impl
{
    [Transactional]
    public class DssExportHistoryDetailMgr : DssExportHistoryDetailBaseMgr, IDssExportHistoryDetailMgr
    {
        public DssExportHistoryDetailMgr(IDssExportHistoryDetailDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}