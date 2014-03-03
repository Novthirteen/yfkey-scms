using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportationBillDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportationBillDetail(TransportationBillDetail entity);

        TransportationBillDetail LoadTransportationBillDetail(Int32 id);
  
        IList<TransportationBillDetail> GetAllTransportationBillDetail();
  
        void UpdateTransportationBillDetail(TransportationBillDetail entity);
        
        void DeleteTransportationBillDetail(Int32 id);
    
        void DeleteTransportationBillDetail(TransportationBillDetail entity);
    
        void DeleteTransportationBillDetail(IList<Int32> pkList);
    
        void DeleteTransportationBillDetail(IList<TransportationBillDetail> entityList);    
        
        TransportationBillDetail LoadTransportationBillDetail(String bill, Int32 tActingBill);
    
        void DeleteTransportationBillDetail(String bill, Int32 tActingBill);
        #endregion Method Created By CodeSmith
    }
}
