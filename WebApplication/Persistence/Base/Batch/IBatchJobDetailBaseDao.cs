using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Batch;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Batch
{
    public interface IBatchJobDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateBatchJobDetail(BatchJobDetail entity);

        BatchJobDetail LoadBatchJobDetail(Int32 id);
  
        IList<BatchJobDetail> GetAllBatchJobDetail();
  
        void UpdateBatchJobDetail(BatchJobDetail entity);
        
        void DeleteBatchJobDetail(Int32 id);
    
        void DeleteBatchJobDetail(BatchJobDetail entity);
    
        void DeleteBatchJobDetail(IList<Int32> pkList);
    
        void DeleteBatchJobDetail(IList<BatchJobDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
