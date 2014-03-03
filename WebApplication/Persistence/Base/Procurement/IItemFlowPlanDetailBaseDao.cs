using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Procurement;
//TODO: Add other using statements here.

namespace com.Sconit.Persistence.Procurement
{
    public interface IItemFlowPlanDetailBaseDao
    {
        #region Method Created By CodeSmith

        void CreateItemFlowPlanDetail(ItemFlowPlanDetail entity);

        ItemFlowPlanDetail LoadItemFlowPlanDetail(Int32 id);
  
        IList<ItemFlowPlanDetail> GetAllItemFlowPlanDetail();
  
        void UpdateItemFlowPlanDetail(ItemFlowPlanDetail entity);
        
        void DeleteItemFlowPlanDetail(Int32 id);
    
        void DeleteItemFlowPlanDetail(ItemFlowPlanDetail entity);
    
        void DeleteItemFlowPlanDetail(IList<Int32> pkList);
    
        void DeleteItemFlowPlanDetail(IList<ItemFlowPlanDetail> entityList);    
        #endregion Method Created By CodeSmith
    }
}
