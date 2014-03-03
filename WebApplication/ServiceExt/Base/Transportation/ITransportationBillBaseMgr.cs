using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Transportation
{
    public interface ITransportationBillBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateTransportationBill(TransportationBill entity);

        TransportationBill LoadTransportationBill(String billNo);

        IList<TransportationBill> GetAllTransportationBill();
    
        void UpdateTransportationBill(TransportationBill entity);

        void DeleteTransportationBill(String billNo);
    
        void DeleteTransportationBill(TransportationBill entity);
    
        void DeleteTransportationBill(IList<String> pkList);
    
        void DeleteTransportationBill(IList<TransportationBill> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
