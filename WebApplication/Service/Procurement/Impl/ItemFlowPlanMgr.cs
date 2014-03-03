using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.Procurement;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class ItemFlowPlanMgr : ItemFlowPlanBaseMgr, IItemFlowPlanMgr
    {
        private ICriteriaMgr CriteriaMgr;
        private IItemFlowPlanDetailMgr ItemFlowPlanDetailMgr;
        private IItemFlowPlanTrackMgr ItemFlowPlanTrackMgr;
        private ISupplyChainMgr SupplyChainMgr;
        private ILocationDetailMgr LocDetMgr;
        private ISupplierMgr SupplierMgr;
        private IRegionMgr RegionMgr;
        private ICustomerMgr CustomerMgr;
        private IBomDetailMgr BomDetailMgr;
        public ItemFlowPlanMgr(
            IItemFlowPlanDao entityDao,
            ICriteriaMgr CriteriaMgr,
            IItemFlowPlanDetailMgr ItemFlowPlanDetailMgr,
            IItemFlowPlanTrackMgr ItemFlowPlanTrackMgr,
            ISupplyChainMgr SupplyChainMgr,
            ILocationDetailMgr LocDetMgr,
            ISupplierMgr SupplierMgr,
            IRegionMgr RegionMgr,
            ICustomerMgr CustomerMgr,
            IBomDetailMgr BomDetailMgr)
            : base(entityDao)
        {
            this.CriteriaMgr = CriteriaMgr;
            this.ItemFlowPlanDetailMgr = ItemFlowPlanDetailMgr;
            this.ItemFlowPlanTrackMgr = ItemFlowPlanTrackMgr;
            this.SupplyChainMgr = SupplyChainMgr;
            this.LocDetMgr = LocDetMgr;
            this.SupplierMgr = SupplierMgr;
            this.RegionMgr = RegionMgr;
            this.CustomerMgr = CustomerMgr;
            this.BomDetailMgr = BomDetailMgr;
        }

        #region Public Methods

        [Transaction(TransactionMode.Requires)]
        public void GeneratePlanning()
        {

        }

        [Transaction(TransactionMode.Requires)]
        public void GeneratePlanning(Flow flow)
        {
        }

        [Transaction(TransactionMode.Requires)]
        public void GeneratePlanning(Flow flow, FlowDetail flowDetail)
        {
        }

        [Transaction(TransactionMode.Requires)]
        public void GeneratePlanning(IList<ItemFlowPlan> itemFlowPlans)
        {
            IList<ItemFlowPlan> itemFlowPlanList = new List<ItemFlowPlan>();
            foreach (ItemFlowPlan itemFlowPlan in itemFlowPlans)
            {
                if (itemFlowPlan.PlanType != BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
                {
                    continue;
                }

                SupplyChain supplyChain = SupplyChainMgr.GenerateSupplyChain(itemFlowPlan.Flow, itemFlowPlan.FlowDetail);
                if (supplyChain != null && supplyChain.SupplyChainDetails != null && supplyChain.SupplyChainDetails.Count > 0)
                {
                    itemFlowPlan.ItemFlowPlanDetails = ItemFlowPlanDetailMgr.GetItemFlowPlanDetailView(itemFlowPlan, true);
                    itemFlowPlanList.Add(itemFlowPlan);

                    IList<SupplyChainDetail> supplyChainDetailList = supplyChain.SupplyChainDetails;
                    this.RunPlanning(itemFlowPlanList, supplyChainDetailList, itemFlowPlan, supplyChainDetailList[0].Id);
                }
            }

            foreach (ItemFlowPlan saveItemFlowPlan in itemFlowPlanList)
            {
                if (saveItemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS || saveItemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
                {
                    this.SaveItemFlowPlan(saveItemFlowPlan);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        public void ImportItemFlowPlan(IList<ItemFlowPlan> itemFlowPlans)
        {
            if (itemFlowPlans != null && itemFlowPlans.Count > 0)
            {
                foreach (ItemFlowPlan itemFlowPlan in itemFlowPlans)
                {
                    this.ClearOldItemFlowPlan(itemFlowPlan.FlowDetail.Item.Code, "", "");
                    this.CreateItemFlowPlan(itemFlowPlan);

                    #region ItemFlowPlanDetails
                    if (itemFlowPlan.ItemFlowPlanDetails != null && itemFlowPlan.ItemFlowPlanDetails.Count > 0)
                    {
                        foreach (ItemFlowPlanDetail itemFlowPlanDetail in itemFlowPlan.ItemFlowPlanDetails)
                        {
                            ItemFlowPlanDetailMgr.CreateItemFlowPlanDetail(itemFlowPlanDetail);

                            #region ItemFlowPlanTracks
                            if (itemFlowPlanDetail.ItemFlowPlanTracks != null && itemFlowPlanDetail.ItemFlowPlanTracks.Count > 0)
                            {
                                foreach (ItemFlowPlanTrack itemFlowPlanTrack in itemFlowPlanDetail.ItemFlowPlanTracks)
                                {
                                    ItemFlowPlanTrackMgr.CreateItemFlowPlanTrack(itemFlowPlanTrack);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
            }

            this.ReleaseItemFlowPlan(itemFlowPlans);
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleaseItemFlowPlan(IList<ItemFlowPlan> itemFlowPlans)
        {
            if (itemFlowPlans != null && itemFlowPlans.Count > 0)
            {
                foreach (ItemFlowPlan itemFlowPlan in itemFlowPlans)
                {
                    this.CloseOldItemFlowPlan(itemFlowPlan);

                    this.UpdateItemFlowPlan(itemFlowPlan);
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemFlowPlanDetail> GetPlanSchedule(string party, string status, DateTime startDate, DateTime? endDate, string flowCode, string itemCode, string planType)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ItemFlowPlan));
            criteria.CreateAlias("Flow", "f");
            criteria.CreateAlias("FlowDetail", "fd");
            if (flowCode != null && flowCode.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Flow.Code", flowCode));
            if (itemCode != null && itemCode.Trim() != string.Empty)
                criteria.Add(Expression.Eq("fd.Item.Code", itemCode));
            criteria.Add(Expression.Eq("Status", status));
            criteria.Add(Expression.Eq("PlanType", planType));
            if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
            {
                criteria.Add(Expression.Eq("f.PartyFrom.Code", party));
            }
            else
            {
                criteria.Add(Expression.Eq("f.PartyTo.Code", party));
            }
            criteria.AddOrder(Order.Asc("fd.Item.Code"));

            IList<ItemFlowPlan> itemFlowPlanList = CriteriaMgr.FindAll<ItemFlowPlan>(criteria);

            IList<ItemFlowPlanDetail> itemFlowPlanDetailList = new List<ItemFlowPlanDetail>();
            if (itemFlowPlanList != null && itemFlowPlanList.Count > 0)
            {
                foreach (ItemFlowPlan itemFlowPlan in itemFlowPlanList)
                {
                    IList<ItemFlowPlanDetail> detailViewList = ItemFlowPlanDetailMgr.GetItemFlowPlanDetailView(itemFlowPlan, false);
                    if (detailViewList != null && detailViewList.Count > 0)
                    {
                        foreach (ItemFlowPlanDetail itemFlowPlanDetail in detailViewList)
                        {
                            itemFlowPlanDetailList.Add(itemFlowPlanDetail);
                        }
                    }
                }
            }

            return itemFlowPlanDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<Flow> GetFlowByPartyAndPlanType(string party, string planType)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(Flow));
            if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
            {
                criteria.Add(Expression.Eq("PartyFrom.Code", party));
            }
            else
            {
                criteria.Add(Expression.Eq("PartyTo.Code", party));
                if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS)
                {
                    criteria.Add(Expression.Eq("Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));
                }
            }

            return CriteriaMgr.FindAll<Flow>(criteria);
        }



        //new method
        [Transaction(TransactionMode.Requires)]
        public DataTable FillDataTableByItemFlowPlan(List<DateTime> dateList, IList<ItemFlowPlan> preIFPList, string planType, string timePeriodType)
        {
            return this.FillDataTableByItemFlowPlan(dateList, preIFPList, planType, timePeriodType, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public DataTable FillDataTableByItemFlowPlan(List<DateTime> dateList, IList<ItemFlowPlan> preIFPList, string planType, string timePeriodType, bool autoPlan)
        {
            DataTable dt = new DataTable();
            if (dateList == null || dateList.Count == 0)
                return dt;
            if (preIFPList == null || preIFPList.Count == 0)
                return dt;

            int dynColIndex = 0;
            #region Add Columns
            //Key Columns
            dt.Columns.Add("ItemFlowPlanId", typeof(int));
            dt.Columns.Add("TimePeriodType", typeof(string));
            dt.Columns.Add("Type", typeof(string));

            //Static Columns
            dt.Columns.Add("StaCol_0", typeof(string));
            dt.Columns.Add("StaCol_1", typeof(string));
            dt.Columns.Add("StaCol_2", typeof(string));
            dt.Columns.Add("StaCol_3", typeof(string));
            dt.Columns.Add("StaCol_4", typeof(string));

            //Dynamic Columns
            foreach (DateTime date in dateList)
            {
                dt.Columns.Add("DynCol_" + dynColIndex.ToString(), typeof(decimal));
                dynColIndex++;
            }
            #endregion

            if (preIFPList != null && preIFPList.Count > 0)
            {
                string flowCode = string.Empty;
                foreach (ItemFlowPlan ifp in preIFPList)
                {
                    if (flowCode != ifp.Flow.Code)
                    {
                        //Group Line
                        DataRow dr = dt.NewRow();
                        dr["Type"] = string.Empty;
                        dr["StaCol_0"] = ifp.Flow.Code;
                        dr["StaCol_1"] = ifp.Flow.Description;
                        dt.Rows.Add(dr);

                        flowCode = ifp.Flow.Code;
                    }

                    ifp.StartPAB = this.GetStartPAB(ifp, dateList[0]);
                    IList<ItemFlowPlanDetail> ifpdList = ItemFlowPlanDetailMgr.GetItemFlowPlanDetailRangeOrderByReqDate(ifp, timePeriodType, dateList, autoPlan);

                    if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
                    {
                        DataRow dr = dt.NewRow();

                        dr["ItemFlowPlanId"] = ifp.Id;
                        dr["TimePeriodType"] = timePeriodType;
                        dr["Type"] = BusinessConstants.PLAN_VIEW_TYPE_PLAN;

                        dr["StaCol_0"] = ifp.FlowDetail.Item.Code;
                        dr["StaCol_1"] = ifp.FlowDetail.Item.Description;

                        this.FillDynamicColumns(dr, 1, ifpdList);

                        dr["StaCol_2"] = "";//todo,客户零件号
                        dr["StaCol_3"] = ifp.FlowDetail.Item.Uom.Code;
                        dt.Rows.Add(dr);
                    }
                    else
                    {
                        int rows = 3;
                        for (int i = 0; i < rows; i++)
                        {
                            DataRow dr = dt.NewRow();

                            dr["ItemFlowPlanId"] = ifp.Id;
                            dr["TimePeriodType"] = timePeriodType;

                            if (i == 0)
                            {
                                //Demand
                                dr["Type"] = BusinessConstants.PLAN_VIEW_TYPE_DEMAND;
                                dr["StaCol_0"] = ifp.FlowDetail.Item.Code;
                                dr["StaCol_1"] = ifp.FlowDetail.Item.Description;
                                //dr["StaCol_2"]

                                this.FillDynamicColumns(dr, i, ifpdList);
                            }
                            else if (i == 1)
                            {
                                decimal maxStock = ifp.FlowDetail.MaxStock == null ? 0 : (decimal)ifp.FlowDetail.MaxStock;
                                //Plan
                                dr["Type"] = BusinessConstants.PLAN_VIEW_TYPE_PLAN;
                                dr["StaCol_0"] = ifp.Flow.Code;
                                dr["StaCol_1"] = maxStock.ToString("0.##");
                                //dr["StaCol_2"]

                                this.FillDynamicColumns(dr, i, ifpdList);
                            }
                            //else if (i == 2)
                            //{
                            //    decimal maxStock = ifp.FlowDetail.MaxStock == null ? 0 : (decimal)ifp.FlowDetail.MaxStock;
                            //    //Order
                            //    dr["Type"] = BusinessConstants.PLAN_VIEW_TYPE_ORDER;
                            //    dr["StaCol_0"] = ifp.FlowDetail.Item.Uom.Code;
                            //    dr["StaCol_1"] = maxStock.ToString("0.##");
                            //    //dr["StaCol_2"]

                            //    this.FillDynamicColumns(dr, i, ifpdList);
                            //}
                            else
                            {
                                decimal safeStock = ifp.FlowDetail.SafeStock == null ? 0 : (decimal)ifp.FlowDetail.SafeStock;
                                //PAB
                                dr["Type"] = BusinessConstants.PLAN_VIEW_TYPE_PAB;
                                dr["StaCol_0"] = ifp.FlowDetail.Item.Uom.Code;
                                dr["StaCol_1"] = safeStock.ToString("0.##");
                                dr["StaCol_2"] = ifp.StartPAB.ToString("0.###");

                                this.FillDynamicColumns(dr, i, ifpdList);
                            }

                            dt.Rows.Add(dr);
                        }
                    }
                }
            }

            return dt;
        }

        [Transaction(TransactionMode.Requires)]
        public IList<ItemFlowPlan> GetPreItemFlowPlan(string planType, string timePeriodType, DateTime startTime, DateTime endTime, string party, string flow, string item, string userCode)
        {
            IList<ItemFlowPlan> itemFlowPlanList = new List<ItemFlowPlan>();

            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowDetail));
            criteria.CreateAlias("Flow", "f");
            if (flow != null && flow.Trim() != string.Empty)
                criteria.Add(Expression.Eq("f.Code", flow));
            if (item != null && item.Trim() != string.Empty)
                criteria.Add(Expression.Eq("Item.Code", item));
            if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
                criteria.Add(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_DISTRIBUTION));
            else if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS)
                criteria.Add(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));
            else if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
                criteria.Add(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PROCUREMENT));
            criteria.AddOrder(Order.Asc("Flow.Code"));
            criteria.AddOrder(Order.Asc("Item.Code"));

            IList<FlowDetail> flowDetailList = CriteriaMgr.FindAll<FlowDetail>(criteria);
            if (flowDetailList != null && flowDetailList.Count > 0)
            {
                foreach (FlowDetail fd in flowDetailList)
                {
                    ItemFlowPlan ifp = this.GetItemFlowPlan(fd.Flow.Code, fd.Id, planType);
                    if (ifp == null)
                    {
                        ifp = new ItemFlowPlan();
                        ifp.Flow = fd.Flow;
                        ifp.FlowDetail = fd;
                        ifp.PlanType = planType;

                        this.CreateItemFlowPlan(ifp);
                    }

                    itemFlowPlanList.Add(ifp);
                }
            }

            return itemFlowPlanList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public ItemFlowPlan GetItemFlowPlan(string flow, int flowDetailId, string planType)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ItemFlowPlan));
            criteria.Add(Expression.Eq("Flow.Code", flow));
            criteria.Add(Expression.Eq("FlowDetail.Id", flowDetailId));
            if (planType != null && planType.Trim() != string.Empty)
                criteria.Add(Expression.Eq("PlanType", planType));
            IList<ItemFlowPlan> itemFlowPlanList = CriteriaMgr.FindAll<ItemFlowPlan>(criteria);

            if (itemFlowPlanList.Count > 0)
                return itemFlowPlanList[0];
            else
                return null;
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleaseItemFlowPlanDetail(int itemFlowPlanId, string timePeriodType, DateTime reqDate)
        {
            ItemFlowPlanDetail ifpd = ItemFlowPlanDetailMgr.GetItemFlowPlanDetail(itemFlowPlanId, timePeriodType, reqDate);
            if (ifpd != null)
                this.ReleaseItemFlowPlanDetail(ifpd);
        }

        [Transaction(TransactionMode.Requires)]
        public void ReleaseItemFlowPlanDetail(ItemFlowPlanDetail itemFlowPlanDetail)
        {
            if (itemFlowPlanDetail.PlanQty < 0)
                return;

            DateTime reqDate = itemFlowPlanDetail.ReqDate;
            double leadTime = Convert.ToDouble(itemFlowPlanDetail.ItemFlowPlan.Flow.LeadTime == null ? 0 : (decimal)itemFlowPlanDetail.ItemFlowPlan.Flow.LeadTime);
            if (itemFlowPlanDetail.TimePeriodType == BusinessConstants.CODE_MASTER_TIME_PERIOD_TYPE_VALUE_DAY)
            {
                //to be refactored
                if (leadTime > 12)
                    reqDate = reqDate.AddHours(-1 * leadTime);
            }

            ItemFlowPlanTrackMgr.ClearOldRelation(null, itemFlowPlanDetail, null);
            if (itemFlowPlanDetail.ItemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
            {
                ItemFlowPlanDetail ifpd = this.GetSupplyFlow(itemFlowPlanDetail.ItemFlowPlan.FlowDetail.Item.Code, itemFlowPlanDetail.TimePeriodType, reqDate);
                if (ifpd != null)
                    this.SaveRelation(ifpd, itemFlowPlanDetail, 1);
            }
            else if (itemFlowPlanDetail.ItemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS)
            {
                string bomCode = itemFlowPlanDetail.ItemFlowPlan.FlowDetail.Item.Bom == null ? itemFlowPlanDetail.ItemFlowPlan.FlowDetail.Item.Code : itemFlowPlanDetail.ItemFlowPlan.FlowDetail.Item.Bom.Code;
                IList<BomDetail> bomdetList = BomDetailMgr.GetFlatBomDetail(bomCode, reqDate);
                if (bomdetList != null && bomdetList.Count > 0)
                {
                    foreach (BomDetail bd in bomdetList)
                    {
                        decimal rateQty = (1 + bd.ScrapPercentage) * bd.RateQty;
                        ItemFlowPlanDetail ifpd = this.GetSupplyFlow(bd.Item.Code, itemFlowPlanDetail.TimePeriodType, reqDate);
                        if (ifpd != null)
                            this.SaveRelation(ifpd, itemFlowPlanDetail, rateQty);
                    }
                }
            }
        }

        #endregion

        #region Private Methods

        [Transaction(TransactionMode.Requires)]
        private void CloseOldItemFlowPlan(ItemFlowPlan itemFlowPlan)
        {
            this.CloseOldItemFlowPlan(itemFlowPlan, "");
        }

        [Transaction(TransactionMode.Requires)]
        private void CloseOldItemFlowPlan(ItemFlowPlan itemFlowPlan, string status)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ItemFlowPlan));
            criteria.Add(Expression.Eq("Status", status));
            criteria.Add(Expression.Eq("PlanType", itemFlowPlan.PlanType));
            criteria.Add(Expression.Eq("Flow.Code", itemFlowPlan.Flow.Code));
            criteria.Add(Expression.Eq("FlowDetail.Id", itemFlowPlan.FlowDetail.Id));
            IList<ItemFlowPlan> itemFlowPlanList = CriteriaMgr.FindAll<ItemFlowPlan>(criteria);

            if (itemFlowPlanList != null && itemFlowPlanList.Count > 0)
            {
                foreach (ItemFlowPlan ifp in itemFlowPlanList)
                {
                    this.UpdateItemFlowPlan(ifp);
                }
            }
        }

        [Transaction(TransactionMode.Requires)]
        private void ClearOldItemFlowPlan(string item, string loc, string status)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ItemFlowPlan));
            criteria.CreateAlias("FlowDetail", "fd");
            criteria.CreateAlias("fd.Item", "item");
            criteria.Add(Expression.Eq("item.Code", item));
            criteria.Add(Expression.Eq("Location.Code", loc));
            criteria.Add(Expression.Eq("Status", status));
            IList<ItemFlowPlan> itemFlowPlanList = CriteriaMgr.FindAll<ItemFlowPlan>(criteria);

            if (itemFlowPlanList != null && itemFlowPlanList.Count > 0)
            {
                foreach (ItemFlowPlan itemFlowPlan in itemFlowPlanList)
                {
                    if (itemFlowPlan.ItemFlowPlanDetails != null && itemFlowPlan.ItemFlowPlanDetails.Count > 0)
                    {
                        foreach (ItemFlowPlanDetail itemFlowPlanDetail in itemFlowPlan.ItemFlowPlanDetails)
                        {
                            if (itemFlowPlanDetail.ItemFlowPlanTracks != null && itemFlowPlanDetail.ItemFlowPlanTracks.Count > 0)
                            {
                                foreach (ItemFlowPlanTrack itemFlowPlanTrack in itemFlowPlanDetail.ItemFlowPlanTracks)
                                {
                                    ItemFlowPlanTrackMgr.DeleteItemFlowPlanTrack(itemFlowPlanTrack);
                                }
                            }

                            ItemFlowPlanDetailMgr.DeleteItemFlowPlanDetail(itemFlowPlanDetail);
                        }
                    }

                    this.DeleteItemFlowPlan(itemFlowPlan);
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        private void RunPlanning(IList<ItemFlowPlan> itemFlowPlanList, IList<SupplyChainDetail> supplyChainDetailList, ItemFlowPlan parentItemFlowPlan, int parentSupplyChainDetailId)
        {
            IList<SupplyChainDetail> scdList = this.GetSuppliers(supplyChainDetailList, parentSupplyChainDetailId);
            if (scdList.Count > 0)
            {
                foreach (SupplyChainDetail supplyChainDetail in scdList)
                {
                    bool isEnd = false;
                    string planType = string.Empty;
                    if (supplyChainDetail.Flow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
                    {
                        isEnd = true;
                        planType = BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS;
                    }
                    else if (this.CheckSupplyChainDetailEnd(supplyChainDetail, supplyChainDetailList))
                    {
                        isEnd = true;
                        planType = BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP;
                    }

                    int index = -1;
                    foreach (ItemFlowPlan ifp in itemFlowPlanList)
                    {
                        if (ifp.Flow.Code == supplyChainDetail.Flow.Code && ifp.FlowDetail.Id == supplyChainDetail.FlowDetail.Id)
                        {
                            index = itemFlowPlanList.IndexOf(ifp);
                            break;
                        }
                    }

                    ItemFlowPlan itemFlowPlan = new ItemFlowPlan();
                    IList<ItemFlowPlanDetail> itemFlowPlanDetailList = new List<ItemFlowPlanDetail>();
                    if (index < 0)
                    {
                        itemFlowPlan.Flow = supplyChainDetail.Flow;
                        itemFlowPlan.FlowDetail = supplyChainDetail.FlowDetail;
                        if (isEnd)
                        {
                            itemFlowPlan.PlanType = planType;
                            if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS)
                            {
                                //itemFlowPlan.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_CREATE;
                            }
                            else if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
                            {
                                //itemFlowPlan.Status = BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT;
                            }
                        }

                        ItemFlowPlanDetailMgr.GenerateItemFlowPlanDetail(parentItemFlowPlan, itemFlowPlan, supplyChainDetail.QuantityPer);
                    }
                    else
                    {
                        //叠加需求
                        itemFlowPlan = itemFlowPlanList[index];
                        itemFlowPlanList.RemoveAt(index);

                        ItemFlowPlanDetailMgr.GenerateItemFlowPlanDetail(parentItemFlowPlan, itemFlowPlan, supplyChainDetail.QuantityPer, true);
                    }

                    if (itemFlowPlan.ItemFlowPlanDetails != null && itemFlowPlan.ItemFlowPlanDetails.Count > 0)
                    {
                        itemFlowPlanList.Add(itemFlowPlan);
                    }

                    if (isEnd)
                    {
                        //end
                        continue;
                    }
                    else
                    {
                        this.RunPlanning(itemFlowPlanList, supplyChainDetailList, itemFlowPlan, supplyChainDetail.Id);
                    }
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<SupplyChainDetail> GetSuppliers(IList<SupplyChainDetail> supplyChainDetailList, int parentSupplyChainDetailId)
        {
            IList<SupplyChainDetail> scdList = new List<SupplyChainDetail>();
            if (supplyChainDetailList != null && supplyChainDetailList.Count > 0)
            {
                foreach (SupplyChainDetail supplyChainDetail in supplyChainDetailList)
                {
                    if (supplyChainDetail.ParentId == parentSupplyChainDetailId)
                    {
                        scdList.Add(supplyChainDetail);
                    }
                }
            }

            //multi suppliers, bidding, simple mode
            IList<SupplyChainDetail> winBidList = new List<SupplyChainDetail>();
            if (scdList != null && scdList.Count > 0)
            {
                foreach (SupplyChainDetail scd in scdList)
                {
                    if (this.CheckSupplyChainDetailItemExist(scd, winBidList))
                    {
                        continue;
                    }

                    IList<SupplyChainDetail> bidList = new List<SupplyChainDetail>();
                    bidList.Add(scd);
                    foreach (SupplyChainDetail scd2 in scdList)
                    {
                        if (scdList.IndexOf(scd2) > scdList.IndexOf(scd))
                        {
                            if (scd2.FlowDetail.Item.Code.ToLower() == scd.FlowDetail.Item.Code.ToLower())
                            {
                                bidList.Add(scd2);
                            }
                        }
                    }

                    if (bidList.Count > 1)
                    {
                        winBidList.Add(this.GetWinBidSupplyChainDetail(bidList));
                    }
                    else
                    {
                        winBidList.Add(bidList[0]);
                    }
                }
            }

            return winBidList;
        }

        private SupplyChainDetail GetWinBidSupplyChainDetail(IList<SupplyChainDetail> bidList)
        {
            //生产优先
            foreach (SupplyChainDetail scd1 in bidList)
            {
                if (scd1.Flow.Type == BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION)
                {
                    return scd1;
                }
            }

            //最短供货优先
            foreach (SupplyChainDetail scd2 in bidList)
            {
                if (scd2.FlowDetail.Flow.Code.ToLower() == scd2.Flow.Code.ToLower())
                {
                    if (scd2.FlowDetail.DefaultLocationFrom == null && scd2.Flow.PartyFrom.Type == BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER)
                    {
                        return scd2;
                    }
                }
                else
                {
                    if (scd2.Flow.LocationFrom == null && scd2.Flow.PartyFrom.Type == BusinessConstants.CODE_MASTER_PARTY_TYPE_VALUE_SUPPLIER)
                    {
                        return scd2;
                    }
                }
            }

            //自动选第一个
            return bidList[0];
        }

        private bool CheckSupplyChainDetailItemExist(SupplyChainDetail supplyChainDetail, IList<SupplyChainDetail> supplyChainDetailList)
        {
            if (supplyChainDetailList != null && supplyChainDetailList.Count > 0)
            {
                foreach (SupplyChainDetail scd in supplyChainDetailList)
                {
                    if (scd.FlowDetail.Item.Code.ToLower() == supplyChainDetail.FlowDetail.Item.Code.ToLower())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool CheckSupplyChainDetailEnd(SupplyChainDetail supplyChainDetail, IList<SupplyChainDetail> supplyChainDetailList)
        {
            if (supplyChainDetailList != null && supplyChainDetailList.Count > 0)
            {
                foreach (SupplyChainDetail scd in supplyChainDetailList)
                {
                    if (scd.ParentId == supplyChainDetail.Id)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        [Transaction(TransactionMode.Requires)]
        private void SaveItemFlowPlan(ItemFlowPlan itemFlowPlan)
        {
            //Clear Old
            this.ClearOldItemFlowPlan(itemFlowPlan.FlowDetail.Item.Code, "", "");

            this.CreateItemFlowPlan(itemFlowPlan);

            //Create ItemFlowPlanDetails
            if (itemFlowPlan.ItemFlowPlanDetails != null && itemFlowPlan.ItemFlowPlanDetails.Count > 0)
            {
                foreach (ItemFlowPlanDetail itemFlowPlanDetail in itemFlowPlan.ItemFlowPlanDetails)
                {
                    ItemFlowPlanDetailMgr.CreateItemFlowPlanDetail(itemFlowPlanDetail);

                    //Create ItemFlowPlanTracks
                    if (itemFlowPlanDetail.ItemFlowPlanTracks != null && itemFlowPlanDetail.ItemFlowPlanTracks.Count > 0)
                    {
                        foreach (ItemFlowPlanTrack itemFlowPlanTrack in itemFlowPlanDetail.ItemFlowPlanTracks)
                        {
                            ItemFlowPlanTrackMgr.CreateItemFlowPlanTrack(itemFlowPlanTrack);
                        }
                    }
                }
            }
        }


        //new method
        [Transaction(TransactionMode.Unspecified)]
        private void FillDynamicColumns(DataRow dr, int i, IList<ItemFlowPlanDetail> ifpdList)
        {
            decimal totalDmd = 0;
            decimal totalPlan = 0;
            //decimal totalOrderQty = 0;
            int j = 0;
            foreach (ItemFlowPlanDetail ifpd in ifpdList)
            {
                string colName = "DynCol_" + j.ToString();
                j++;
                if (i == 0)
                {
                    //Demand
                    dr[colName] = ifpd.GrossDemand;
                    totalDmd += ifpd.GrossDemand;
                    dr["StaCol_2"] = totalDmd.ToString("0.###");
                }
                else if (i == 1)
                {
                    //Plan
                    dr[colName] = ifpd.PlanQty;
                    totalPlan += ifpd.PlanQty;
                    dr["StaCol_2"] = totalPlan.ToString("0.###");
                }
                //else if (i == 2)
                //{
                //    //Order
                //    dr[colName] = ifpd.OrderRemainQty;
                //    totalOrderQty += ifpd.OrderRemainQty;
                //    dr["StaCol_2"] = totalOrderQty.ToString("0.###");
                //}
                else
                {
                    //PAB
                    dr[colName] = ifpd.PAB;
                }
            }
        }

        [Transaction(TransactionMode.Unspecified)]
        private DataRow FillPlanIndexColumn(DataRow dr, List<DateTime> dateList, string timePeriodType, int ItemFlowPlanId)
        {
            foreach (DateTime date in dateList)
            {
                int ID = 0;
                ItemFlowPlanDetail ifpd = ItemFlowPlanDetailMgr.GetItemFlowPlanDetail(ItemFlowPlanId, timePeriodType, date);
                if (ifpd != null)
                    ID = ifpd.Id;

                string colName = date.ToString();
                dr[colName] = ID;
            }

            return dr;
        }

        public decimal GetStartPAB(ItemFlowPlan ifp, DateTime date)
        {
            decimal startPAB = 0;
            string loc = string.Empty;
            if (ifp.Flow.Code == ifp.FlowDetail.Flow.Code)
            {
                loc = ifp.FlowDetail.DefaultLocationTo == null ? string.Empty : ifp.FlowDetail.DefaultLocationTo.Code;
            }
            else
            {
                //Reference flow
                loc = ifp.Flow.LocationTo == null ? string.Empty : ifp.Flow.LocationTo.Code;
            }

            LocationDetail locDet = LocDetMgr.FindLocationDetail(loc, ifp.FlowDetail.Item.Code, date);
            if (locDet != null)
                startPAB = locDet.Qty;

            return startPAB;
        }

        private List<string> GetPartyList(string planType, string userCode)
        {
            List<string> partyList = new List<string>();
            if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
            {
                IList<Customer> custList = CustomerMgr.GetCustomer(userCode);
                if (custList != null && custList.Count > 0)
                {
                    foreach (Customer cust in custList)
                    {
                        partyList.Add(cust.Code);
                    }
                }
            }
            else if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS)
            {
                IList<Region> regionList = RegionMgr.GetRegion(userCode);
                if (regionList != null && regionList.Count > 0)
                {
                    foreach (Region region in regionList)
                    {
                        partyList.Add(region.Code);
                    }
                }
            }
            else if (planType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
            {
                IList<Supplier> supList = SupplierMgr.GetSupplier(userCode);
                if (supList != null && supList.Count > 0)
                {
                    foreach (Supplier sup in supList)
                    {
                        partyList.Add(sup.Code);
                    }
                }
            }
            return partyList;
        }

        [Transaction(TransactionMode.Unspecified)]
        private FlowDetail GetProdFlowForMPS(string itemCode, string loc)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(FlowDetail));
            criteria.CreateAlias("Flow", "f");
            criteria.Add(Expression.Eq("Item.Code", itemCode));
            criteria.Add(Expression.Eq("f.Type", BusinessConstants.CODE_MASTER_FLOW_TYPE_VALUE_PRODUCTION));

            IList<FlowDetail> flowDetailList = CriteriaMgr.FindAll<FlowDetail>(criteria);

            //todo,bid
            if (flowDetailList != null && flowDetailList.Count > 0)
                return flowDetailList[0];
            else
                return null;
        }

        [Transaction(TransactionMode.Requires)]
        private ItemFlowPlanDetail GetSupplyFlow(string item, string timePeriodType, DateTime reqDate)
        {
            IList<ItemFlowPlan> ifpList = this.GetPreItemFlowPlan(BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS, timePeriodType, DateTime.Now, DateTime.Now, null, null, item, null);
            if (ifpList.Count == 0)
                ifpList = this.GetPreItemFlowPlan(BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP, timePeriodType, DateTime.Now, DateTime.Now, null, null, item, null);

            //todo,bid
            ItemFlowPlanDetail ifpd = null;
            if (ifpList.Count > 0)
            {
                ItemFlowPlan ifp = ifpList[0];
                ifpd = ItemFlowPlanDetailMgr.GetItemFlowPlanDetail(ifp.Id, timePeriodType, reqDate);
                if (ifpd == null)
                {
                    ifpd = new ItemFlowPlanDetail();
                    ifpd.ItemFlowPlan = ifp;
                    ifpd.TimePeriodType = timePeriodType;
                    ifpd.ReqDate = reqDate;
                    ifpd.PlanQty = 0;
                    ItemFlowPlanDetailMgr.CreateItemFlowPlanDetail(ifpd);
                }
            }
            return ifpd;
        }

        [Transaction(TransactionMode.Requires)]
        public void SaveRelation(ItemFlowPlanDetail ifpd, ItemFlowPlanDetail parIfpd, decimal rate)
        {
            ItemFlowPlanTrack ifpt = new ItemFlowPlanTrack();
            ifpt.ItemFlowPlanDetail = ifpd;
            ifpt.ReferencePlanDetail = parIfpd;
            ifpt.Rate = rate;
            ItemFlowPlanTrackMgr.CreateItemFlowPlanTrack(ifpt);
        }

        #endregion

    }
}