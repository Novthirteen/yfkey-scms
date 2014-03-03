using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface ILocationBaseDao
    {
        #region Method Created By CodeSmith

        void CreateLocation(Location entity);

        Location LoadLocation(String code);
  
        IList<Location> GetAllLocation();
  
        IList<Location> GetAllLocation(bool includeInactive);
  
        void UpdateLocation(Location entity);
        
        void DeleteLocation(String code);
    
        void DeleteLocation(Location entity);
    
        void DeleteLocation(IList<String> pkList);
    
        void DeleteLocation(IList<Location> entityList);    
        #endregion Method Created By CodeSmith
    }
}
