using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Batch;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Batch
{
    public interface IBatchRunLogBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateBatchRunLog(BatchRunLog entity);

        BatchRunLog LoadBatchRunLog(Int32 id);

        IList<BatchRunLog> GetAllBatchRunLog();
    
        void UpdateBatchRunLog(BatchRunLog entity);

        void DeleteBatchRunLog(Int32 id);
    
        void DeleteBatchRunLog(BatchRunLog entity);
    
        void DeleteBatchRunLog(IList<Int32> pkList);
    
        void DeleteBatchRunLog(IList<BatchRunLog> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
