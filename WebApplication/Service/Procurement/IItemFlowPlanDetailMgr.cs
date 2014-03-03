using System;
using System.Collections.Generic;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement
{
    public interface IItemFlowPlanDetailMgr : IItemFlowPlanDetailBaseMgr
    {
        #region Customized Methods

        IList<ItemFlowPlanDetail> GetActiveItemFlowPlanDetailListSort(int itemFlowPlanId);

        void GenerateItemFlowPlanDetail(ItemFlowPlan parentItemFlowPlan, ItemFlowPlan itemFlowPlan, decimal qtyPer);

        void GenerateItemFlowPlanDetail(ItemFlowPlan parentItemFlowPlan, ItemFlowPlan itemFlowPlan, decimal qtyPer, bool isRecover);

        IList<ItemFlowPlanDetail> GetItemFlowPlanDetailView(ItemFlowPlan itemFlowPlan, bool computePlanQty);

        IList<ItemFlowPlanDetail> GetItemFlowPlanDetailView(ItemFlowPlan itemFlowPlan, IList<ItemFlowPlanDetail> itemFlowPlanDetailList, bool computePlanQty);

        void UpdateItemFlowPlanDetailPlanQty(int Id, decimal planQty);

        IList<ItemFlowPlanDetail> GetItemFlowPlanDetailRangeOrderByReqDate(ItemFlowPlan itemFlowPlan, string timePeriodType, List<DateTime> dateList, bool autoPlan);

        ItemFlowPlanDetail GetItemFlowPlanDetail(int itemFlowPlanId, string timePeriodType, DateTime reqDate);

        void SaveItemFlowPlanDetail(ItemFlowPlanDetail itemFlowPlanDetail);

        #endregion Customized Methods
    }
}
