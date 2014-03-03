using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Distribution;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Distribution
{
    public interface IInProcessLocationDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInProcessLocationDetail(InProcessLocationDetail entity);

        InProcessLocationDetail LoadInProcessLocationDetail(Int32 id);
  
        IList<InProcessLocationDetail> GetAllInProcessLocationDetail();
  
        void UpdateInProcessLocationDetail(InProcessLocationDetail entity);
        
        void DeleteInProcessLocationDetail(Int32 id);
    
        void DeleteInProcessLocationDetail(InProcessLocationDetail entity);
    
        void DeleteInProcessLocationDetail(IList<Int32> pkList);
    
        void DeleteInProcessLocationDetail(IList<InProcessLocationDetail> entityList);    
        
        InProcessLocationDetail LoadInProcessLocationDetail(com.Sconit.Entity.Distribution.InProcessLocation inProcessLocation, com.Sconit.Entity.MasterData.OrderLocationTransaction orderLocationTransaction, String lotNo);

        void DeleteInProcessLocationDetail(String inProcessLocationIpNo, Int32 orderLocationTransactionId, String lotNo);

        InProcessLocationDetail LoadInProcessLocationDetail(String inProcessLocationIpNo, Int32 orderLocationTransactionId, String lotNo);
        #endregion Method Created By CodeSmith
    }
}
