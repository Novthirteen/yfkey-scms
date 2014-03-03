using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportationOrderDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportationOrderDetail(TransportationOrderDetail entity);

        TransportationOrderDetail LoadTransportationOrderDetail(Int32 id);
  
        IList<TransportationOrderDetail> GetAllTransportationOrderDetail();
  
        void UpdateTransportationOrderDetail(TransportationOrderDetail entity);
        
        void DeleteTransportationOrderDetail(Int32 id);
    
        void DeleteTransportationOrderDetail(TransportationOrderDetail entity);
    
        void DeleteTransportationOrderDetail(IList<Int32> pkList);
    
        void DeleteTransportationOrderDetail(IList<TransportationOrderDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
