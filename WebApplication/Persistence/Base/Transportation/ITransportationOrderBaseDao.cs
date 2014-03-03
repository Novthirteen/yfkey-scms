using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportationOrderBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportationOrder(TransportationOrder entity);

        TransportationOrder LoadTransportationOrder(String orderNo);
  
        IList<TransportationOrder> GetAllTransportationOrder();
  
        void UpdateTransportationOrder(TransportationOrder entity);
        
        void DeleteTransportationOrder(String orderNo);
    
        void DeleteTransportationOrder(TransportationOrder entity);
    
        void DeleteTransportationOrder(IList<String> pkList);
    
        void DeleteTransportationOrder(IList<TransportationOrder> entityList);    
        #endregion Method Created By CodeSmith
    }
}
