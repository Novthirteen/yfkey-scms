using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IRoutingDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateRoutingDetail(RoutingDetail entity);

        RoutingDetail LoadRoutingDetail(Int32 id);
  
        IList<RoutingDetail> GetAllRoutingDetail();
  
        void UpdateRoutingDetail(RoutingDetail entity);
        
        void DeleteRoutingDetail(Int32 id);
    
        void DeleteRoutingDetail(RoutingDetail entity);
    
        void DeleteRoutingDetail(IList<Int32> pkList);
    
        void DeleteRoutingDetail(IList<RoutingDetail> entityList);    
        
        RoutingDetail LoadRoutingDetail(com.Sconit.Entity.MasterData.Routing routing, Int32 operation, String reference);
    
        void DeleteRoutingDetail(String routingCode, Int32 operation, String reference);
        
        RoutingDetail LoadRoutingDetail(String routingCode, Int32 operation, String reference);
        #endregion Method Created By CodeSmith
    }
}
