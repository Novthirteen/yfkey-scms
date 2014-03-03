using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportPriceListDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportPriceListDetail(TransportPriceListDetail entity);

        TransportPriceListDetail LoadTransportPriceListDetail(Int32 id);
  
        IList<TransportPriceListDetail> GetAllTransportPriceListDetail();
  
        void UpdateTransportPriceListDetail(TransportPriceListDetail entity);
        
        void DeleteTransportPriceListDetail(Int32 id);
    
        void DeleteTransportPriceListDetail(TransportPriceListDetail entity);
    
        void DeleteTransportPriceListDetail(IList<Int32> pkList);
    
        void DeleteTransportPriceListDetail(IList<TransportPriceListDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
