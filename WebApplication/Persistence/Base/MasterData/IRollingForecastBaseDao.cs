using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IRollingForecastBaseDao
    {
        #region Method Created By CodeSmith

        void CreateRollingForecast(RollingForecast entity);

        RollingForecast LoadRollingForecast(Int32 id);
  
        IList<RollingForecast> GetAllRollingForecast();
  
        void UpdateRollingForecast(RollingForecast entity);
        
        void DeleteRollingForecast(Int32 id);
    
        void DeleteRollingForecast(RollingForecast entity);
    
        void DeleteRollingForecast(IList<Int32> pkList);
    
        void DeleteRollingForecast(IList<RollingForecast> entityList);    
        #endregion Method Created By CodeSmith
    }
}
