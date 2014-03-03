using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Batch;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Batch
{
    public interface IBatchTriggerBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBatchTrigger(BatchTrigger entity);

        BatchTrigger LoadBatchTrigger(Int32 id);
  
        IList<BatchTrigger> GetAllBatchTrigger();
  
        void UpdateBatchTrigger(BatchTrigger entity);
        
        void DeleteBatchTrigger(Int32 id);
    
        void DeleteBatchTrigger(BatchTrigger entity);
    
        void DeleteBatchTrigger(IList<Int32> pkList);
    
        void DeleteBatchTrigger(IList<BatchTrigger> entityList);    
        #endregion Method Created By CodeSmith
    }
}
