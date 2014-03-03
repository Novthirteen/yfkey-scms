using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportationRouteBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportationRoute(TransportationRoute entity);

        TransportationRoute LoadTransportationRoute(String code);
  
        IList<TransportationRoute> GetAllTransportationRoute();
  
        IList<TransportationRoute> GetAllTransportationRoute(bool includeInactive);
  
        void UpdateTransportationRoute(TransportationRoute entity);
        
        void DeleteTransportationRoute(String code);
    
        void DeleteTransportationRoute(TransportationRoute entity);
    
        void DeleteTransportationRoute(IList<String> pkList);
    
        void DeleteTransportationRoute(IList<TransportationRoute> entityList);    
        #endregion Method Created By CodeSmith
    }
}
