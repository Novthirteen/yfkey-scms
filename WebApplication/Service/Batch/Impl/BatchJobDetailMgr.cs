using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.Batch;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Batch.Impl
{
    [Transactional]
    public class BatchJobDetailMgr : BatchJobDetailBaseMgr, IBatchJobDetailMgr
    {
        public BatchJobDetailMgr(IBatchJobDetailDao entityDao)
            : base(entityDao)
        {
        }

        #region Customized Methods

        //TODO: Add other methods here.

        #endregion Customized Methods
    }
}