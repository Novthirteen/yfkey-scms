using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IRoutingBaseDao
    {
        #region Method Created By CodeSmith

        void CreateRouting(Routing entity);

        Routing LoadRouting(String code);
  
        IList<Routing> GetAllRouting();
  
        IList<Routing> GetAllRouting(bool includeInactive);
  
        void UpdateRouting(Routing entity);
        
        void DeleteRouting(String code);
    
        void DeleteRouting(Routing entity);
    
        void DeleteRouting(IList<String> pkList);
    
        void DeleteRouting(IList<Routing> entityList);    
        #endregion Method Created By CodeSmith
    }
}
