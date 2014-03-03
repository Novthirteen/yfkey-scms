using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.MasterData;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.MasterData
{
    public interface IFlowPlanBaseDao
    {
        #region Method Created By CodeSmith

        void CreateFlowPlan(FlowPlan entity);

        FlowPlan LoadFlowPlan(Int32 id);
  
        IList<FlowPlan> GetAllFlowPlan();
  
        void UpdateFlowPlan(FlowPlan entity);
        
        void DeleteFlowPlan(Int32 id);
    
        void DeleteFlowPlan(FlowPlan entity);
    
        void DeleteFlowPlan(IList<Int32> pkList);
    
        void DeleteFlowPlan(IList<FlowPlan> entityList);    
        #endregion Method Created By CodeSmith
    }
}
