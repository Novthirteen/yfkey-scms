using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MRP;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MRP
{
    public interface IMrpShipPlanBaseDao
    {
        #region Method Created By CodeSmith

        void CreateMrpShipPlan(MrpShipPlan entity);

        MrpShipPlan LoadMrpShipPlan(Int32 id);
  
        IList<MrpShipPlan> GetAllMrpShipPlan();
  
        void UpdateMrpShipPlan(MrpShipPlan entity);
        
        void DeleteMrpShipPlan(Int32 id);
    
        void DeleteMrpShipPlan(MrpShipPlan entity);
    
        void DeleteMrpShipPlan(IList<Int32> pkList);
    
        void DeleteMrpShipPlan(IList<MrpShipPlan> entityList);    
        #endregion Method Created By CodeSmith
    }
}
