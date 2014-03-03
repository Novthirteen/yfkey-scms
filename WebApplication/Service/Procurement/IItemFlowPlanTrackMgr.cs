using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement
{
    public interface IItemFlowPlanTrackMgr : IItemFlowPlanTrackBaseMgr
    {
        #region Customized Methods

        IList<ItemFlowPlanTrack> GenerateItemFlowPlanTrack(ItemFlowPlanDetail parentItemFlowPlanDetail, ItemFlowPlanDetail itemFlowPlanDetail, decimal qtyPer);

        IList<ItemFlowPlanTrack> GetItemFlowPlanTrackList(ItemFlowPlanDetail mstrIfpd, ItemFlowPlanDetail refIfpd, OrderLocationTransaction orderLocTrans);

        void ClearOldRelation(ItemFlowPlanDetail mstrIfpd, ItemFlowPlanDetail refIfpd, OrderLocationTransaction orderLocTrans);

        #endregion Customized Methods
    }
}
