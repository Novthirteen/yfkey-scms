using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Dss;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Dss
{
    public interface IDssOutboundControlBaseDao
    {
        #region Method Created By CodeSmith

        void CreateDssOutboundControl(DssOutboundControl entity);

        DssOutboundControl LoadDssOutboundControl(Int32 id);
  
        IList<DssOutboundControl> GetAllDssOutboundControl();
  
        IList<DssOutboundControl> GetAllDssOutboundControl(bool includeInactive);
  
        void UpdateDssOutboundControl(DssOutboundControl entity);
        
        void DeleteDssOutboundControl(Int32 id);
    
        void DeleteDssOutboundControl(DssOutboundControl entity);
    
        void DeleteDssOutboundControl(IList<Int32> pkList);
    
        void DeleteDssOutboundControl(IList<DssOutboundControl> entityList);    
        #endregion Method Created By CodeSmith
    }
}
