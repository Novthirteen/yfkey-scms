using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportationAddressBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportationAddress(TransportationAddress entity);

        TransportationAddress LoadTransportationAddress(Int32 id);
  
        IList<TransportationAddress> GetAllTransportationAddress();
  
        void UpdateTransportationAddress(TransportationAddress entity);
        
        void DeleteTransportationAddress(Int32 id);
    
        void DeleteTransportationAddress(TransportationAddress entity);
    
        void DeleteTransportationAddress(IList<Int32> pkList);
    
        void DeleteTransportationAddress(IList<TransportationAddress> entityList);    
        #endregion Method Created By CodeSmith
    }
}
