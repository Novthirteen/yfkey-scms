using System;
using System.Collections;
using System.Collections.Generic;
using Castle.Services.Transaction;
using com.Sconit.Entity;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Persistence.Procurement;
using com.Sconit.Service.Criteria;
using com.Sconit.Service.MasterData;
using com.Sconit.Utility;
using NHibernate.Expression;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement.Impl
{
    [Transactional]
    public class ItemFlowPlanDetailMgr : ItemFlowPlanDetailBaseMgr, IItemFlowPlanDetailMgr
    {
        private ICriteriaMgr CriteriaMgr;
        private IItemFlowPlanTrackMgr ItemFlowPlanTrackMgr;
        private ILocationDetailMgr LocationDetailMgr;
        private IWorkCalendarMgr WorkCalendarMgr;
        public ItemFlowPlanDetailMgr(
            IItemFlowPlanDetailDao entityDao,
            ICriteriaMgr CriteriaMgr,
            IItemFlowPlanTrackMgr ItemFlowPlanTrackMgr,
            ILocationDetailMgr LocationDetailMgr,
            IWorkCalendarMgr WorkCalendarMgr)
            : base(entityDao)
        {
            this.CriteriaMgr = CriteriaMgr;
            this.ItemFlowPlanTrackMgr = ItemFlowPlanTrackMgr;
            this.LocationDetailMgr = LocationDetailMgr;
            this.WorkCalendarMgr = WorkCalendarMgr;
        }

        #region Public Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemFlowPlanDetail> GetActiveItemFlowPlanDetailListSort(int itemFlowPlanId)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ItemFlowPlanDetail));
            criteria.Add(Expression.Eq("ItemFlowPlan.Id", itemFlowPlanId));
            criteria.Add(Expression.Ge("ReqDate", DateTime.Now.Date));
            criteria.AddOrder(Order.Asc("ReqDate"));

            return CriteriaMgr.FindAll<ItemFlowPlanDetail>(criteria);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void GenerateItemFlowPlanDetail(ItemFlowPlan parentItemFlowPlan, ItemFlowPlan itemFlowPlan, decimal qtyPer)
        {
            this.GenerateItemFlowPlanDetail(parentItemFlowPlan, itemFlowPlan, qtyPer, false);
        }

        [Transaction(TransactionMode.Unspecified)]
        public void GenerateItemFlowPlanDetail(ItemFlowPlan parentItemFlowPlan, ItemFlowPlan itemFlowPlan, decimal qtyPer, bool isAccum)
        {
            IList<ItemFlowPlanDetail> itemFlowPlanDetailList = new List<ItemFlowPlanDetail>();

            itemFlowPlanDetailList = this.GetPreItemFlowPlanDetailByParent(parentItemFlowPlan, itemFlowPlan, qtyPer);
            if (isAccum)
            {
                itemFlowPlanDetailList = this.MergeItemFlowPlanDetail(itemFlowPlanDetailList, itemFlowPlan.ItemFlowPlanDetails);
            }
            itemFlowPlanDetailList = this.GetItemFlowPlanDetailView(itemFlowPlan, itemFlowPlanDetailList, true);

            itemFlowPlan.AddRangeItemFlowPlanDetail(itemFlowPlanDetailList);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemFlowPlanDetail> GetItemFlowPlanDetailView(ItemFlowPlan itemFlowPlan, bool computePlanQty)
        {
            IList<ItemFlowPlanDetail> itemFlowPlanDetailList = this.GetActiveItemFlowPlanDetailListSort(itemFlowPlan.Id);
            return this.GetItemFlowPlanDetailView(itemFlowPlan, itemFlowPlanDetailList, computePlanQty);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemFlowPlanDetail> GetItemFlowPlanDetailView(ItemFlowPlan itemFlowPlan, IList<ItemFlowPlanDetail> itemFlowPlanDetailList, bool computePlanQty)
        {
            if (itemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_DMDSCHEDULE)
            {
                if (itemFlowPlanDetailList != null && itemFlowPlanDetailList.Count > 0)
                {
                    foreach (ItemFlowPlanDetail itemFlowPlanDetail in itemFlowPlanDetailList)
                    {
                        DateTime startWinTime = itemFlowPlanDetail.ReqDate.Date;
                        DateTime endWinTime = this.GetDmdEndTime(itemFlowPlanDetail, itemFlowPlanDetailList);

                        IList<OrderLocationTransaction> orderLocTransList = new List<OrderLocationTransaction>();
                        if (itemFlowPlanDetailList.IndexOf(itemFlowPlanDetail) == itemFlowPlanDetailList.Count - 1)
                        {
                            orderLocTransList = this.GetOpenOrderLocTransInByFlow(itemFlowPlan.Flow.Code, itemFlowPlan.FlowDetail.Item.Code, startWinTime, null);
                        }
                        else
                        {
                            orderLocTransList = this.GetOpenOrderLocTransInByFlow(itemFlowPlan.Flow.Code, itemFlowPlan.FlowDetail.Item.Code, startWinTime, endWinTime);
                        }

                        this.RefreshItemFlowPlanDetail(itemFlowPlanDetail, orderLocTransList);
                    }

                    if (DateTime.Compare(DateTime.Now.Date, itemFlowPlanDetailList[0].ReqDate) < 0)
                    {
                        IList<OrderLocationTransaction> orderLocTransList = this.GetOpenOrderLocTransInByFlow(itemFlowPlan.Flow.Code, itemFlowPlan.FlowDetail.Item.Code, DateTime.Now.Date, itemFlowPlanDetailList[0].ReqDate);
                        IList<ItemFlowPlanDetail> orderDemandList = this.ConvertOrderDmdToItemFlowPlanDetail(itemFlowPlan, orderLocTransList);
                        foreach (ItemFlowPlanDetail ifpd in itemFlowPlanDetailList)
                        {
                            orderDemandList.Add(ifpd);
                        }
                        itemFlowPlanDetailList = orderDemandList;
                    }
                }
                else
                {
                    IList<OrderLocationTransaction> orderLocTransList = this.GetOpenOrderLocTransInByFlow(itemFlowPlan.Flow.Code, itemFlowPlan.FlowDetail.Item.Code, DateTime.Now.Date, null);
                    itemFlowPlanDetailList = this.ConvertOrderDmdToItemFlowPlanDetail(itemFlowPlan, orderLocTransList);
                }
            }
            else if (itemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS)
            {
                decimal PAB = this.GetDmdStartPAB(WorkCalendarMgr.GetDayShiftStart(DateTime.Now, itemFlowPlan.Flow.PartyTo.Code), "", itemFlowPlan.FlowDetail.Item.Code);
                //itemFlowPlan.PAB = PAB;

                DateTime maxDate = DateTime.Now.Date;
                if (itemFlowPlanDetailList != null && itemFlowPlanDetailList.Count > 0)
                {
                    maxDate = itemFlowPlanDetailList[itemFlowPlanDetailList.Count - 1].ReqDate;
                }
                IList<OrderLocationTransaction> orderLocTransList = this.GetOpenOrderLocTransInByFlow(itemFlowPlan.Flow.Code, itemFlowPlan.FlowDetail.Item.Code, DateTime.Now.Date, null);
                if (orderLocTransList != null && orderLocTransList.Count > 0)
                {
                    DateTime maxWinTime = orderLocTransList[orderLocTransList.Count - 1].OrderDetail.OrderHead.WindowTime;
                    maxWinTime = WorkCalendarMgr.GetDayShiftStart(maxWinTime, itemFlowPlan.Flow.PartyTo.Code);
                    if (DateTime.Compare(maxDate, maxWinTime) < 0)
                    {
                        maxDate = maxWinTime;
                    }
                }

                DateTime date = DateTime.Now.Date;
                while (date <= maxDate)
                {
                    ItemFlowPlanDetail itemFlowPlanDetail = new ItemFlowPlanDetail();
                    itemFlowPlanDetail.ItemFlowPlan = itemFlowPlan;
                    itemFlowPlanDetail.GrossDemand = 0;
                    itemFlowPlanDetail.OrderRemainQty = 0;
                    itemFlowPlanDetail.PlanQty = 0;

                    foreach (ItemFlowPlanDetail ifpd in itemFlowPlanDetailList)
                    {
                        if (DateTime.Compare(ifpd.ReqDate, date) == 0)
                        {
                            itemFlowPlanDetail = ifpd;
                            break;
                        }
                    }

                    foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
                    {
                        DateTime winTime = orderLocTrans.OrderDetail.OrderHead.WindowTime;
                        if (DateTime.Compare(date, winTime) <= 0 && DateTime.Compare(date.AddDays(1), winTime) > 0)
                        {
                            ItemFlowPlanTrack itemFlowPlanTrack = new ItemFlowPlanTrack();
                            itemFlowPlanTrack.ItemFlowPlanDetail = itemFlowPlanDetail;
                            itemFlowPlanTrack.OrderLocationTransaction = orderLocTrans;
                            //itemFlowPlanTrack.DemandQty = orderLocTrans.RemainQty;
                            itemFlowPlanDetail.AddItemFlowPlanTrack(itemFlowPlanTrack);
                        }
                    }

                    date = date.AddDays(1);
                }
            }

            itemFlowPlanDetailList = this.PlanningAndScheduling(itemFlowPlanDetailList, itemFlowPlan, computePlanQty);
            return itemFlowPlanDetailList;
        }

        [Transaction(TransactionMode.Requires)]
        public void UpdateItemFlowPlanDetailPlanQty(int Id, decimal planQty)
        {
            ItemFlowPlanDetail itemFlowPlanDetail = this.LoadItemFlowPlanDetail(Id);
            itemFlowPlanDetail.PlanQty = planQty;
            this.UpdateItemFlowPlanDetail(itemFlowPlanDetail);
        }


        //new method
        [Transaction(TransactionMode.Unspecified)]
        public IList<ItemFlowPlanDetail> GetItemFlowPlanDetailRangeOrderByReqDate(ItemFlowPlan itemFlowPlan, string timePeriodType, List<DateTime> dateList, bool autoPlan)
        {
            IList<ItemFlowPlanDetail> ifpdList = new List<ItemFlowPlanDetail>();

            DateTime planStartTime = DateTimeHelper.GetStartTime(timePeriodType, DateTime.Now.Date);
            decimal PAB = itemFlowPlan.StartPAB;
            foreach (DateTime date in dateList)
            {
                ItemFlowPlanDetail ifpd = this.GetItemFlowPlanDetail(itemFlowPlan.Id, timePeriodType, date);
                if (ifpd == null)
                {
                    ifpd = new ItemFlowPlanDetail();
                    ifpd.ItemFlowPlan = itemFlowPlan;
                    ifpd.TimePeriodType = timePeriodType;
                    ifpd.ReqDate = date;
                    ifpd.PlanQty = 0;//todo
                }

                ifpd.GrossDemand = 0;//todo
                IList<ItemFlowPlanTrack> ifptList = ItemFlowPlanTrackMgr.GetItemFlowPlanTrackList(ifpd, null, null);
                if (ifptList != null && ifptList.Count > 0)
                {
                    foreach (ItemFlowPlanTrack ifpt in ifptList)
                    {
                        ifpd.GrossDemand += ifpt.ReferencePlanDetail.PlanQty * ifpt.Rate;
                    }
                }

                ifpd.OrderRemainQty = 0;//todo

                //auto Plan,已有计划不再重排
                if (autoPlan && ifpd.PlanQty == 0)
                {
                    decimal safeInv = ifpd.ItemFlowPlan.FlowDetail.SafeStock.HasValue ? (decimal)ifpd.ItemFlowPlan.FlowDetail.SafeStock : 0;
                    decimal planQty = ifpd.GrossDemand + safeInv - PAB;
                    if (planQty > 0)
                    {
                        if (ifpd.ItemFlowPlan.FlowDetail.BatchSize.HasValue)
                        {
                            decimal batchSize = (decimal)ifpd.ItemFlowPlan.FlowDetail.BatchSize;
                            if (batchSize > 0)
                                planQty = batchSize * Math.Ceiling(planQty / batchSize);
                        }

                        ifpd.PlanQty = planQty;
                    }
                }

                PAB += Math.Max(ifpd.PlanQty, ifpd.OrderRemainQty) - ifpd.GrossDemand;
                ifpd.PAB = PAB;

                ifpdList.Add(ifpd);
            }

            return ifpdList;
        }

        [Transaction(TransactionMode.Unspecified)]
        public ItemFlowPlanDetail GetItemFlowPlanDetail(int itemFlowPlanId, string timePeriodType, DateTime reqDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(ItemFlowPlanDetail));
            criteria.Add(Expression.Eq("ItemFlowPlan.Id", itemFlowPlanId));
            criteria.Add(Expression.Eq("TimePeriodType", timePeriodType));
            criteria.Add(Expression.Eq("ReqDate", reqDate));
            IList<ItemFlowPlanDetail> itemFlowPlanList = CriteriaMgr.FindAll<ItemFlowPlanDetail>(criteria);

            if (itemFlowPlanList.Count > 0)
                return itemFlowPlanList[0];
            else
                return null;
        }

        [Transaction(TransactionMode.Requires)]
        public void SaveItemFlowPlanDetail(ItemFlowPlanDetail itemFlowPlanDetail)
        {
            ItemFlowPlanDetail ifpd = this.GetItemFlowPlanDetail(itemFlowPlanDetail.ItemFlowPlan.Id, itemFlowPlanDetail.TimePeriodType, itemFlowPlanDetail.ReqDate);
            if (ifpd == null)
            {
                this.CreateItemFlowPlanDetail(itemFlowPlanDetail);
            }
            else
            {
                if (ifpd.PlanQty != itemFlowPlanDetail.PlanQty)
                {
                    ifpd.PlanQty = itemFlowPlanDetail.PlanQty;
                    this.UpdateItemFlowPlanDetail(ifpd);
                }
            }
        }

        #endregion Public Methods


        #region Private Methods

        [Transaction(TransactionMode.Unspecified)]
        private IList<OrderLocationTransaction> GetOpenOrderLocTransInByFlow(string flowCode, string itemCode, DateTime? startWinTime, DateTime? endWinTime)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction), "orderLocTrans");
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            criteria.Add(Expression.Eq("orderLocTrans.Item.Code", itemCode));
            criteria.Add(Expression.Eq("orderLocTrans.IOType", BusinessConstants.IO_TYPE_IN));
            criteria.Add(Expression.Eq("oh.Flow", flowCode));
            //WindowTime in [startWinTime,endWinTime)
            if (startWinTime.HasValue)
                criteria.Add(Expression.Ge("oh.WindowTime", (DateTime)startWinTime));
            if (endWinTime.HasValue)
                criteria.Add(Expression.Lt("oh.WindowTime", (DateTime)endWinTime));
            criteria.Add(Expression.Or(Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT),
                Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)));
            criteria.AddOrder(Order.Asc("oh.WindowTime"));

            IList<OrderLocationTransaction> orderLocTransList = CriteriaMgr.FindAll<OrderLocationTransaction>(criteria);
            return orderLocTransList;
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<OrderLocationTransaction> GetOpenOrderLocTrans(string loc, string itemCode, string IOType, DateTime? startTime, DateTime? endTime)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(OrderLocationTransaction), "orderLocTrans");
            criteria.CreateAlias("OrderDetail", "od");
            criteria.CreateAlias("od.OrderHead", "oh");
            criteria.Add(Expression.Eq("orderLocTrans.Item.Code", itemCode));
            criteria.Add(Expression.Eq("orderLocTrans.Location.Code", loc));
            criteria.Add(Expression.Eq("orderLocTrans.IOType", IOType));
            if (IOType == BusinessConstants.IO_TYPE_IN)
            {
                if (startTime.HasValue)
                    criteria.Add(Expression.Gt("oh.WindowTime", (DateTime)startTime));
                if (endTime.HasValue)
                    criteria.Add(Expression.Le("oh.WindowTime", (DateTime)endTime));
            }
            else
            {
                if (startTime.HasValue)
                    criteria.Add(Expression.Ge("oh.StartTime", (DateTime)startTime));
                if (endTime.HasValue)
                    criteria.Add(Expression.Lt("oh.StartTime", (DateTime)endTime));
            }
            criteria.Add(Expression.Or(Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_SUBMIT),
                Expression.Eq("oh.Status", BusinessConstants.CODE_MASTER_STATUS_VALUE_INPROCESS)));
            IList<OrderLocationTransaction> orderLocTransList = CriteriaMgr.FindAll<OrderLocationTransaction>(criteria);

            return orderLocTransList;
        }

        [Transaction(TransactionMode.Requires)]
        private void RefreshItemFlowPlanDetail(ItemFlowPlanDetail itemFlowPlanDetail, IList<OrderLocationTransaction> orderLocTransList)
        {
            decimal inputOrderDmd = 0;
            if (orderLocTransList != null && orderLocTransList.Count > 0)
            {
                foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
                {
                    if (orderLocTrans.RemainQty > 0)
                    {
                        inputOrderDmd += orderLocTrans.RemainQty;

                        ItemFlowPlanTrack itemFlowPlanTrack = new ItemFlowPlanTrack();
                        itemFlowPlanTrack.ItemFlowPlanDetail = itemFlowPlanDetail;
                        itemFlowPlanTrack.OrderLocationTransaction = orderLocTrans;
                        //itemFlowPlanTrack.DemandQty = orderLocTrans.RemainQty;
                        itemFlowPlanDetail.AddItemFlowPlanTrack(itemFlowPlanTrack);
                    }
                }
            }

            itemFlowPlanDetail.OrderRemainQty = inputOrderDmd;
        }

        [Transaction(TransactionMode.Unspecified)]
        private DateTime GetReqDate(ItemFlowPlanDetail parentItemFlowPlanDetail)
        {
            double leadTime = 0;
            if (parentItemFlowPlanDetail.ItemFlowPlan.Flow.LeadTime != null)
            {
                leadTime = Convert.ToDouble((decimal)(parentItemFlowPlanDetail.ItemFlowPlan.Flow.LeadTime));
            }

            DateTime reqDate = parentItemFlowPlanDetail.ReqDate.AddHours(-1 * leadTime);
            reqDate = WorkCalendarMgr.GetWorkTime(reqDate, parentItemFlowPlanDetail.ItemFlowPlan.Flow.PartyFrom.Code, true);

            return reqDate;
        }

        [Transaction(TransactionMode.Unspecified)]
        private DateTime GetDmdEndTime(ItemFlowPlanDetail itemFlowPlanDetail, IList<ItemFlowPlanDetail> itemFlowPlanDetailList)
        {
            DateTime dmdEndTime = DateTime.Now;
            int index = itemFlowPlanDetailList.IndexOf(itemFlowPlanDetail);

            if (index + 1 < itemFlowPlanDetailList.Count)
            {
                dmdEndTime = itemFlowPlanDetailList[index + 1].ReqDate;
            }
            else
            {
                if (index >= 1)
                {
                    TimeSpan ts = itemFlowPlanDetailList[index].ReqDate - itemFlowPlanDetailList[index - 1].ReqDate;
                    dmdEndTime = itemFlowPlanDetailList[index].ReqDate.AddDays(ts.TotalDays);
                }
                else
                {
                    dmdEndTime = itemFlowPlanDetailList[index].ReqDate.AddDays(1);
                }
            }

            return dmdEndTime.Date;
        }

        [Transaction(TransactionMode.Unspecified)]
        private decimal GetOrderDmd(IList<OrderLocationTransaction> orderLocTransList)
        {
            decimal orderDmd = 0;
            foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
            {
                if (orderLocTrans.RemainQty > 0)
                {
                    orderDmd += orderLocTrans.RemainQty;
                }
            }

            return orderDmd;
        }

        [Transaction(TransactionMode.Unspecified)]
        private decimal GetDmdStartPAB(DateTime dmdStartTime, string loc, string itemCode)
        {
            IList<OrderLocationTransaction> inOrderLocTransList = this.GetOpenOrderLocTrans(loc, itemCode, BusinessConstants.IO_TYPE_IN, null, dmdStartTime);
            IList<OrderLocationTransaction> outOrderLocTransList = this.GetOpenOrderLocTrans(loc, itemCode, BusinessConstants.IO_TYPE_OUT, null, dmdStartTime);

            decimal currentInv = LocationDetailMgr.GetCurrentInv(loc, itemCode);
            decimal totalInputQty = this.GetTotalRemainQty(inOrderLocTransList);
            decimal totalOutputQty = this.GetTotalRemainQty(outOrderLocTransList);
            decimal PAB = currentInv + totalInputQty - totalOutputQty;

            return PAB;
        }

        [Transaction(TransactionMode.Unspecified)]
        private decimal GetTotalRemainQty(IList<OrderLocationTransaction> orderLocTransList)
        {
            decimal totalRemainQty = 0;
            foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
            {
                if (orderLocTrans.RemainQty > 0)
                {
                    totalRemainQty += orderLocTrans.RemainQty;
                }
            }

            return totalRemainQty;
        }

        [Transaction(TransactionMode.Unspecified)]
        private decimal GetFinalPlanQty(decimal planQty, FlowDetail flowDetail)
        {
            decimal finalPlanQty = planQty;

            //批量
            decimal batchSize = flowDetail.BatchSize == null ? 0 : (decimal)(flowDetail.BatchSize);
            if (batchSize > 0)
            {
                finalPlanQty = Math.Ceiling(planQty / batchSize) * batchSize;
            }

            return finalPlanQty;
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<ItemFlowPlanDetail> MergeItemFlowPlanDetail(IList<ItemFlowPlanDetail> preItemFlowPlanDetailList, IList<ItemFlowPlanDetail> oldItemFlowPlanDetailList)
        {
            IList<ItemFlowPlanDetail> itemFlowPlanDetailList = new List<ItemFlowPlanDetail>();
            if (oldItemFlowPlanDetailList == null || oldItemFlowPlanDetailList.Count > 0)
            {
                return preItemFlowPlanDetailList;
            }
            if (preItemFlowPlanDetailList == null || preItemFlowPlanDetailList.Count > 0)
            {
                return oldItemFlowPlanDetailList;
            }

            int i = 0;
            foreach (ItemFlowPlanDetail preItemFlowPlanDetail in preItemFlowPlanDetailList)
            {
                while (i < oldItemFlowPlanDetailList.Count)
                {
                    ItemFlowPlanDetail itemFlowPlanDetail = new ItemFlowPlanDetail();
                    if (DateTime.Compare(preItemFlowPlanDetail.ReqDate.Date, oldItemFlowPlanDetailList[i].ReqDate.Date) == 0)
                    {
                        itemFlowPlanDetail = preItemFlowPlanDetail;
                        itemFlowPlanDetail.GrossDemand = preItemFlowPlanDetail.GrossDemand + oldItemFlowPlanDetailList[i].GrossDemand;
                        itemFlowPlanDetail.AddRangeItemFlowPlanTrack(preItemFlowPlanDetail.ItemFlowPlanTracks);
                        itemFlowPlanDetail.AddRangeItemFlowPlanTrack(oldItemFlowPlanDetailList[i].ItemFlowPlanTracks);
                        itemFlowPlanDetailList.Add(itemFlowPlanDetail);

                        i++;
                        continue;
                    }
                    else if (DateTime.Compare(preItemFlowPlanDetail.ReqDate, oldItemFlowPlanDetailList[i].ReqDate) < 0)
                    {
                        itemFlowPlanDetailList.Add(preItemFlowPlanDetail);

                        break;
                    }
                    else
                    {
                        itemFlowPlanDetailList.Add(oldItemFlowPlanDetailList[i]);

                        i++;
                        continue;
                    }
                }
            }

            return itemFlowPlanDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<ItemFlowPlanDetail> ConvertOrderDmdToItemFlowPlanDetail(ItemFlowPlan itemFlowPlan, IList<OrderLocationTransaction> orderLocTransList)
        {
            IList<ItemFlowPlanDetail> itemFlowPlanDetailList = new List<ItemFlowPlanDetail>();
            List<DateTime> dateTimeList = this.CollectDateList(orderLocTransList);
            if (dateTimeList.Count > 0)
            {
                foreach (DateTime date in dateTimeList)
                {
                    ItemFlowPlanDetail itemFlowPlanDetail = new ItemFlowPlanDetail();
                    itemFlowPlanDetail.ItemFlowPlan = itemFlowPlan;
                    itemFlowPlanDetail.ReqDate = date;
                    itemFlowPlanDetail.OrderRemainQty = 0;
                    foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
                    {
                        if (DateTime.Compare(date, orderLocTrans.OrderDetail.OrderHead.WindowTime.Date) == 0)
                        {
                            itemFlowPlanDetail.OrderRemainQty += orderLocTrans.RemainQty;
                            ItemFlowPlanTrack itemFlowPlanTrack = new ItemFlowPlanTrack();
                            itemFlowPlanTrack.ItemFlowPlanDetail = itemFlowPlanDetail;
                            itemFlowPlanTrack.OrderLocationTransaction = orderLocTrans;
                            //itemFlowPlanTrack.DemandQty = orderLocTrans.RemainQty;
                            itemFlowPlanDetail.AddItemFlowPlanTrack(itemFlowPlanTrack);
                        }
                    }

                    if (itemFlowPlanDetail.OrderRemainQty > 0)
                    {
                        itemFlowPlanDetailList.Add(itemFlowPlanDetail);
                    }
                }
            }

            return itemFlowPlanDetailList;
        }

        private List<DateTime> CollectDateList(IList<OrderLocationTransaction> orderLocTransList)
        {
            List<DateTime> dateTimeList = new List<DateTime>();
            foreach (OrderLocationTransaction orderLocTrans in orderLocTransList)
            {
                if (dateTimeList.IndexOf(orderLocTrans.OrderDetail.OrderHead.WindowTime.Date) < 0)
                {
                    dateTimeList.Add(orderLocTrans.OrderDetail.OrderHead.WindowTime.Date);
                }
            }

            return dateTimeList;
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<ItemFlowPlanDetail> PlanningAndScheduling(IList<ItemFlowPlanDetail> preItemFlowPlanDetailList, ItemFlowPlan itemFlowPlan, bool computePlanQty)
        {
            IList<ItemFlowPlanDetail> itemFlowPlanDetailList = new List<ItemFlowPlanDetail>();
            decimal PAB = 0;// itemFlowPlan.PAB;
            decimal safeInv = itemFlowPlan.FlowDetail.SafeStock == null ? 0 : (decimal)(itemFlowPlan.FlowDetail.SafeStock);
            foreach (ItemFlowPlanDetail itemFlowPlanDetail in preItemFlowPlanDetailList)
            {
                //计划量
                decimal planQty = itemFlowPlanDetail.PlanQty;
                if (computePlanQty)
                {
                    planQty = itemFlowPlanDetail.GrossDemand - (PAB + itemFlowPlanDetail.OrderRemainQty) + safeInv;
                    planQty = this.GetFinalPlanQty(planQty, itemFlowPlan.FlowDetail);
                    itemFlowPlanDetail.PlanQty = planQty;
                }

                //区间期末PAB
                PAB = PAB + itemFlowPlanDetail.OrderRemainQty + planQty - itemFlowPlanDetail.GrossDemand;
                itemFlowPlanDetail.PAB = PAB;

                if (planQty > 0)
                {
                    itemFlowPlanDetail.ItemFlowPlan = itemFlowPlan;
                    itemFlowPlanDetailList.Add(itemFlowPlanDetail);
                }
            }

            return itemFlowPlanDetailList;
        }

        [Transaction(TransactionMode.Unspecified)]
        private IList<ItemFlowPlanDetail> GetPreItemFlowPlanDetailByParent(ItemFlowPlan parentItemFlowPlan, ItemFlowPlan itemFlowPlan, decimal qtyPer)
        {
            IList<ItemFlowPlanDetail> itemFlowPlanDetailList = new List<ItemFlowPlanDetail>();
            if (parentItemFlowPlan.ItemFlowPlanDetails != null && parentItemFlowPlan.ItemFlowPlanDetails.Count > 0)
            {
                DateTime dmdStartTime = DateTime.Now;
                DateTime dmdEndTime = DateTime.Now;

                foreach (ItemFlowPlanDetail parentItemFlowPlanDetail in parentItemFlowPlan.ItemFlowPlanDetails)
                {
                    //确定计划区间/需求时界
                    double leadTime = parentItemFlowPlanDetail.ItemFlowPlan.Flow.LeadTime == null ? 0 : Convert.ToDouble((decimal)(parentItemFlowPlanDetail.ItemFlowPlan.Flow.LeadTime));
                    if (parentItemFlowPlan.ItemFlowPlanDetails.IndexOf(parentItemFlowPlanDetail) == 0)
                    {
                        dmdStartTime = parentItemFlowPlanDetail.ReqDate.AddHours(-1 * leadTime);
                        //WorkCalendar
                        if (itemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS || itemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
                        {
                            dmdStartTime = WorkCalendarMgr.GetWorkTime(dmdStartTime, parentItemFlowPlan.Flow.PartyFrom.Code, true);
                        }
                    }
                    dmdEndTime = this.GetDmdEndTime(parentItemFlowPlanDetail, parentItemFlowPlan.ItemFlowPlanDetails).AddHours(-1 * leadTime);

                    //WorkCalendar
                    if (itemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MPS || itemFlowPlan.PlanType == BusinessConstants.CODE_MASTER_PLAN_TYPE_VALUE_MRP)
                    {
                        dmdEndTime = WorkCalendarMgr.GetWorkTime(dmdEndTime, parentItemFlowPlan.Flow.PartyFrom.Code, true);
                    }

                    if (DateTime.Compare(DateTime.Now, dmdStartTime) > 0)
                    {
                        //Rogue Town!
                        dmdStartTime = dmdEndTime;
                        continue;
                    }

                    ItemFlowPlanDetail itemFlowPlanDetail = new ItemFlowPlanDetail();
                    itemFlowPlanDetail.ReqDate = WorkCalendarMgr.GetDayShiftStart(dmdStartTime, itemFlowPlan.Flow.PartyTo.Code);

                    //毛需求
                    itemFlowPlanDetail.GrossDemand = (Math.Max(parentItemFlowPlanDetail.OrderRemainQty, parentItemFlowPlanDetail.PlanQty)) * qtyPer;

                    itemFlowPlanDetailList.Add(itemFlowPlanDetail);

                    //Rogue Town!
                    dmdStartTime = dmdEndTime;
                }
            }

            return itemFlowPlanDetailList;
        }

        #endregion
    }
}