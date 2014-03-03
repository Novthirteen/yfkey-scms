using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement
{
    public interface IItemFlowPlanTrackBaseMgr
    {
        #region Method Created By CodeSmith

        void CreateItemFlowPlanTrack(ItemFlowPlanTrack entity);

        ItemFlowPlanTrack LoadItemFlowPlanTrack(Int32 id);

        IList<ItemFlowPlanTrack> GetAllItemFlowPlanTrack();
    
        void UpdateItemFlowPlanTrack(ItemFlowPlanTrack entity);

        void DeleteItemFlowPlanTrack(Int32 id);
    
        void DeleteItemFlowPlanTrack(ItemFlowPlanTrack entity);
    
        void DeleteItemFlowPlanTrack(IList<Int32> pkList);
    
        void DeleteItemFlowPlanTrack(IList<ItemFlowPlanTrack> entityList);    
    
        #endregion Method Created By CodeSmith
    }
}
