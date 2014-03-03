using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Transportation;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Transportation
{
    public interface ITransportationRouteDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateTransportationRouteDetail(TransportationRouteDetail entity);

        TransportationRouteDetail LoadTransportationRouteDetail(Int32 id);
  
        IList<TransportationRouteDetail> GetAllTransportationRouteDetail();
  
        void UpdateTransportationRouteDetail(TransportationRouteDetail entity);
        
        void DeleteTransportationRouteDetail(Int32 id);
    
        void DeleteTransportationRouteDetail(TransportationRouteDetail entity);
    
        void DeleteTransportationRouteDetail(IList<Int32> pkList);
    
        void DeleteTransportationRouteDetail(IList<TransportationRouteDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
