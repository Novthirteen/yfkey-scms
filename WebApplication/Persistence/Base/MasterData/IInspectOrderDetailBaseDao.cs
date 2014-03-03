using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IInspectOrderDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateInspectOrderDetail(InspectOrderDetail entity);

        InspectOrderDetail LoadInspectOrderDetail(Int32 id);
  
        IList<InspectOrderDetail> GetAllInspectOrderDetail();
  
        void UpdateInspectOrderDetail(InspectOrderDetail entity);
        
        void DeleteInspectOrderDetail(Int32 id);
    
        void DeleteInspectOrderDetail(InspectOrderDetail entity);
    
        void DeleteInspectOrderDetail(IList<Int32> pkList);
    
        void DeleteInspectOrderDetail(IList<InspectOrderDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
