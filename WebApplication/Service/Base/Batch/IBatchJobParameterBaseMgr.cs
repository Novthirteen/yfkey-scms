using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Batch;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Batch
{
    public interface IBatchJobParameterBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateBatchJobParameter(BatchJobParameter entity);

        BatchJobParameter LoadBatchJobParameter(Int32 id);

        IList<BatchJobParameter> GetAllBatchJobParameter();
    
        void UpdateBatchJobParameter(BatchJobParameter entity);

        void DeleteBatchJobParameter(Int32 id);
    
        void DeleteBatchJobParameter(BatchJobParameter entity);
    
        void DeleteBatchJobParameter(IList<Int32> pkList);
    
        void DeleteBatchJobParameter(IList<BatchJobParameter> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
