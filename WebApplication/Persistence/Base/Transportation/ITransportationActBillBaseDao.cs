using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportationActBillBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportationActBill(TransportationActBill entity);

        TransportationActBill LoadTransportationActBill(Int32 id);
  
        IList<TransportationActBill> GetAllTransportationActBill();
  
        void UpdateTransportationActBill(TransportationActBill entity);
        
        void DeleteTransportationActBill(Int32 id);
    
        void DeleteTransportationActBill(TransportationActBill entity);
    
        void DeleteTransportationActBill(IList<Int32> pkList);
    
        void DeleteTransportationActBill(IList<TransportationActBill> entityList);    
        #endregion Method Created By CodeSmith
    }
}
