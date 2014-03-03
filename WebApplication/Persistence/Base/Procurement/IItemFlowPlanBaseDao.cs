using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Procurement;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Procurement
{
    public interface IItemFlowPlanBaseDao
    {
        #region Method Created By CodeSmith

        void CreateItemFlowPlan(ItemFlowPlan entity);

        ItemFlowPlan LoadItemFlowPlan(Int32 id);
  
        IList<ItemFlowPlan> GetAllItemFlowPlan();
  
        void UpdateItemFlowPlan(ItemFlowPlan entity);
        
        void DeleteItemFlowPlan(Int32 id);
    
        void DeleteItemFlowPlan(ItemFlowPlan entity);
    
        void DeleteItemFlowPlan(IList<Int32> pkList);
    
        void DeleteItemFlowPlan(IList<ItemFlowPlan> entityList);    
        #endregion Method Created By CodeSmith
    }
}
