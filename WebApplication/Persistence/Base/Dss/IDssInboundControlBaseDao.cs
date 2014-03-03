using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Dss;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Dss
{
    public interface IDssInboundControlBaseDao
    {
        #region Method Created By CodeSmith

        void CreateDssInboundControl(DssInboundControl entity);

        DssInboundControl LoadDssInboundControl(Int32 id);
  
        IList<DssInboundControl> GetAllDssInboundControl();
  
        void UpdateDssInboundControl(DssInboundControl entity);
        
        void DeleteDssInboundControl(Int32 id);
    
        void DeleteDssInboundControl(DssInboundControl entity);
    
        void DeleteDssInboundControl(IList<Int32> pkList);
    
        void DeleteDssInboundControl(IList<DssInboundControl> entityList);    
        #endregion Method Created By CodeSmith
    }
}
