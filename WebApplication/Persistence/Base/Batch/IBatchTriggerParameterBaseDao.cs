using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Batch;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Batch
{
    public interface IBatchTriggerParameterBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBatchTriggerParameter(BatchTriggerParameter entity);

        BatchTriggerParameter LoadBatchTriggerParameter(Int32 id);
  
        IList<BatchTriggerParameter> GetAllBatchTriggerParameter();
  
        void UpdateBatchTriggerParameter(BatchTriggerParameter entity);
        
        void DeleteBatchTriggerParameter(Int32 id);
    
        void DeleteBatchTriggerParameter(BatchTriggerParameter entity);
    
        void DeleteBatchTriggerParameter(IList<Int32> pkList);
    
        void DeleteBatchTriggerParameter(IList<BatchTriggerParameter> entityList);    
        #endregion Method Created By CodeSmith
    }
}
