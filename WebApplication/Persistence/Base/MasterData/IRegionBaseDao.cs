using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IRegionBaseDao
    {
        #region Method Created By CodeSmith

        void CreateRegion(Region entity);

        Region LoadRegion(String code);
  
        IList<Region> GetAllRegion();
  
        IList<Region> GetAllRegion(bool includeInactive);
  
        void UpdateRegion(Region entity);
        
        void DeleteRegion(String code);
    
        void DeleteRegion(Region entity);
    
        void DeleteRegion(IList<String> pkList);
    
        void DeleteRegion(IList<Region> entityList);    
        #endregion Method Created By CodeSmith
    }
}
