using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportPriceListBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportPriceList(TransportPriceList entity);

        TransportPriceList LoadTransportPriceList(String code);
  
        IList<TransportPriceList> GetAllTransportPriceList();
  
        IList<TransportPriceList> GetAllTransportPriceList(bool includeInactive);
  
        void UpdateTransportPriceList(TransportPriceList entity);
        
        void DeleteTransportPriceList(String code);
    
        void DeleteTransportPriceList(TransportPriceList entity);
    
        void DeleteTransportPriceList(IList<String> pkList);
    
        void DeleteTransportPriceList(IList<TransportPriceList> entityList);    
        #endregion Method Created By CodeSmith
    }
}
