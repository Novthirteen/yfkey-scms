using System;
using System.Collections.Generic;
using System.Data;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement
{
    public interface IItemFlowPlanMgr : IItemFlowPlanBaseMgr
    {
        #region Customized Methods

        void GeneratePlanning();

        void GeneratePlanning(Flow flow);

        void GeneratePlanning(Flow flow, FlowDetail flowDetail);

        void GeneratePlanning(IList<ItemFlowPlan> itemFlowPlans);

        void ImportItemFlowPlan(IList<ItemFlowPlan> itemFlowPlans);

        void ReleaseItemFlowPlan(IList<ItemFlowPlan> itemFlowPlans);

        //IList<ItemFlowPlan> GetItemFlowPlan(string flowCode, string itemCode, string status, string planType);

        //IList<ItemFlowPlan> GetItemFlowPlan(string flowCode, string itemCode, string status, string planType, bool includeDetailView);

        IList<ItemFlowPlanDetail> GetPlanSchedule(string party, string status, DateTime startDate, DateTime? endDate, string flowCode, string itemCode, string planType);

        IList<Flow> GetFlowByPartyAndPlanType(string party, string planType);

        DataTable FillDataTableByItemFlowPlan(List<DateTime> dateList, IList<ItemFlowPlan> preIFPList, string planType, string timePeriodType);

        DataTable FillDataTableByItemFlowPlan(List<DateTime> dateList, IList<ItemFlowPlan> preIFPList, string planType, string timePeriodType, bool autoPlan);

        IList<ItemFlowPlan> GetPreItemFlowPlan(string planType, string timePeriodType, DateTime startTime, DateTime endTime, string party, string flow, string item, string userCode);

        ItemFlowPlan GetItemFlowPlan(string flow, int flowDetailId, string planType);

        void ReleaseItemFlowPlanDetail(int itemFlowPlanId, string timePeriodType, DateTime reqDate);

        void ReleaseItemFlowPlanDetail(ItemFlowPlanDetail itemFlowPlanDetail);

        #endregion Customized Methods
    }
}
