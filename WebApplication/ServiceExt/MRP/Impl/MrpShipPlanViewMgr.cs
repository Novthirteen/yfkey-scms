using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Services.Transaction;
using com.Sconit.Persistence.MRP;
using com.Sconit.Entity.MRP;
using NHibernate.Expression;
using com.Sconit.Service.Criteria;
using com.Sconit.Entity;
using System.Linq;
using System.Reflection;
using com.Sconit.Utility;
using com.Sconit.Entity.Exception;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP.Impl
{
    [Transactional]
    public class MrpShipPlanViewMgr : MrpShipPlanViewBaseMgr, IMrpShipPlanViewMgr
    {
        private ICriteriaMgr criteriaMgr;

        public MrpShipPlanViewMgr(IMrpShipPlanViewDao entityDao, ICriteriaMgr criteriaMgr)
            : base(entityDao)
        {
            this.criteriaMgr = criteriaMgr;
        }
        #region Customized Methods

        //[1]added by williamlu@esteering.cn
        //[1]2012/5
        [Transaction(TransactionMode.Requires)]
        public IList<MrpShipPlanView> GetMrpShipPlanViewsBB(string flowCode, string locCode, string itemCode, List<string> itemCodes, DateTime effectiveDate, DateTime? winDate, DateTime? startDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(MrpShipPlanView));
            if (itemCode != null && itemCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Like("Item", itemCode, MatchMode.Start));
            }

            //[2]in参数超过2100报错的解决
            //[2]added by williamlu@esteering.cn
            //[2]2012/05
            int LimitCount = 2000;
            double dLoopCycle = (itemCodes.Count / LimitCount);
            int iLoopCycle = Convert.ToInt32(Math.Ceiling(dLoopCycle));
            iLoopCycle = iLoopCycle == 0 ? 1 : (iLoopCycle + 1);

            string strItem = string.Empty;
            string tempSql = string.Empty;
            for (int i = 0; i < iLoopCycle; i++)
            {
                int rangeIndex = i * LimitCount;
                List<string> tempItemCodes = itemCodes.GetRange(rangeIndex, Math.Min(LimitCount, itemCodes.Count - rangeIndex));
              
                strItem = "";
                for (int j = 0; j < Math.Min(LimitCount, itemCodes.Count - rangeIndex); j++)
                {
                    strItem += "'" + tempItemCodes[j] + "'" + ",";
                }
                strItem = strItem.Substring(0, strItem.Length - 1);

                if (i == 0)
                {
                    tempSql = string.Format("Item in ({0})", strItem);
                }
                else
                {
                    tempSql = "(" + tempSql + string.Format(" or Item in ({0})", strItem) + ")";
                }
            }
            criteria.Add(Expression.Sql(tempSql));
            //[2]added end           

            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Flow", flowCode));
            }
            if (locCode != null && locCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Location", locCode));
            }
            criteria.Add(Expression.Eq("EffectiveDate", effectiveDate));
            if (winDate.HasValue)
            {
                criteria.Add(Expression.Lt("WindowTime", winDate.Value.Date.AddDays(1)));
            }
            if (startDate.HasValue)
            {
                criteria.Add(Expression.Gt("StartTime", startDate.Value.Date));
            }
            return criteriaMgr.FindAll<MrpShipPlanView>(criteria);
        }
        //[1]added end

        [Transaction(TransactionMode.Requires)]
        public IList<MrpShipPlanView> GetMrpShipPlanViews(string flowCode, string locCode, string itemCode, DateTime effectiveDate, DateTime? winDate, DateTime? startDate)
        {
            DetachedCriteria criteria = DetachedCriteria.For(typeof(MrpShipPlanView));
            if (itemCode != null && itemCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Like("Item", itemCode, MatchMode.Start));
            }
            if (flowCode != null && flowCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Flow", flowCode));
            }
            if (locCode != null && locCode.Trim() != string.Empty)
            {
                criteria.Add(Expression.Eq("Location", locCode));
            }
            criteria.Add(Expression.Eq("EffectiveDate", effectiveDate));
            if (winDate.HasValue)
            {
                criteria.Add(Expression.Lt("WindowTime", winDate.Value.Date.AddDays(1)));
            }
            if (startDate.HasValue)
            {
                criteria.Add(Expression.Gt("StartTime", startDate.Value.Date));
            }
            return criteriaMgr.FindAll<MrpShipPlanView>(criteria);
        }

        [Transaction(TransactionMode.Requires)]
        public ScheduleView TransferMrpShipPlanViews2ScheduleView(IList<MrpShipPlanView> mrpShipPlanViews,
            IList<ExpectTransitInventoryView> expectTransitInventoryViews,
            string locOrFlow, string winOrStartTime)
        {
            if (mrpShipPlanViews == null || mrpShipPlanViews.Count == 0)
            {
                return null;
            }
            #region 头
            List<ScheduleHead> scheduleHeads = new List<ScheduleHead>();

            if (locOrFlow == "Flow")
            {
                if (winOrStartTime == "WindowTime")
                {
                    scheduleHeads = (from det in mrpShipPlanViews
                                     group det by new { det.Flow, det.FlowType, det.WindowTime } into result
                                     select new ScheduleHead
                                     {
                                         Flow = result.Key.Flow,
                                         Type = result.Key.FlowType,
                                         DateTo = result.Key.WindowTime
                                     }).ToList();
                }
                else
                {
                    scheduleHeads = (from det in mrpShipPlanViews
                                     group det by new { det.Flow, det.FlowType, det.StartTime } into result
                                     select new ScheduleHead
                                     {
                                         Flow = result.Key.Flow,
                                         Type = result.Key.FlowType,
                                         DateFrom = result.Key.StartTime
                                     }).ToList();
                }
            }
            else if (locOrFlow == "Location")
            {
                if (winOrStartTime == "WindowTime")
                {
                    scheduleHeads = (from det in mrpShipPlanViews
                                     group det by new { det.Location, det.WindowTime } into result
                                     select new ScheduleHead
                                     {
                                         Location = result.Key.Location,
                                         Type = "Location",
                                         DateTo = result.Key.WindowTime,
                                     }).ToList();
                }
                else
                {
                    scheduleHeads = (from det in mrpShipPlanViews
                                     group det by new { det.Location, det.StartTime } into result
                                     select new ScheduleHead
                                     {
                                         Location = result.Key.Location,
                                         Type = "Location",
                                         DateFrom = result.Key.StartTime,
                                     }).ToList();
                }
            }
            else
            {
                throw new TechnicalException(locOrFlow);
            }

            if (winOrStartTime == "WindowTime")
            {
                scheduleHeads = scheduleHeads.OrderBy(c => c.DateTo).Take(41).ToList();
            }
            else
            {
                scheduleHeads = scheduleHeads.OrderBy(c => c.DateFrom).Take(41).ToList();
            }
            #endregion

            #region 明细
            List<ScheduleBody> scheduleBodys =
                (from det in mrpShipPlanViews
                 group det by new { det.Item, det.ItemDescription, det.ItemReference, det.Uom, det.UnitCount } into result
                 select new ScheduleBody
                 {
                     Item = result.Key.Item,
                     ItemDescription = result.Key.ItemDescription,
                     ItemReference = result.Key.ItemReference,
                     Uom = result.Key.Uom,
                     UnitCount = result.Key.UnitCount,
                 }).ToList();
            #endregion

            #region 赋值
            foreach (ScheduleBody scheduleBody in scheduleBodys)
            {
                int i = 0;
                DateTime? lastDate = null;
                ScheduleHead lastScheduleHead = null;

                foreach (ScheduleHead scheduleHead in scheduleHeads)
                {

                    string qty = "Qty" + i.ToString();
                    string actQty = "ActQty" + i.ToString();
                    string requiredQty = "RequiredQty" + i.ToString();

                    decimal rQty = 0;
                    decimal? aQty = 0;

                    PropertyInfo[] scheduleBodyPropertyInfo = typeof(ScheduleBody).GetProperties().OrderBy(c => c.Name).ToArray();
                    if (locOrFlow == "Location")
                    {
                        if (winOrStartTime == "WindowTime")
                        {
                            rQty = (from plan in mrpShipPlanViews
                                    where plan.Location == scheduleHead.Location
                                    && plan.Item == scheduleBody.Item
                                    && plan.WindowTime == scheduleHead.DateTo
                                    select plan.Qty).Sum();

                            aQty = (from inv in expectTransitInventoryViews
                                    where inv.Location == scheduleHead.Location
                                    && inv.Item == scheduleBody.Item
                                    && inv.WindowTime <= scheduleHead.DateTo
                                    && (!lastDate.HasValue || inv.WindowTime > lastDate.Value)
                                    select inv.TransitQty).Sum();

                        }
                        else if (winOrStartTime == "StartTime")
                        {
                            rQty = (from plan in mrpShipPlanViews
                                    where plan.Location == scheduleHead.Location
                                    && plan.Item == scheduleBody.Item
                                    && plan.StartTime == scheduleHead.DateFrom
                                    select plan.Qty).Sum();


                            aQty = (from inv in expectTransitInventoryViews
                                    where inv.Location == scheduleHead.Location
                                    && inv.Item == scheduleBody.Item
                                    && inv.StartTime <= scheduleHead.DateFrom
                                    && (!lastDate.HasValue || inv.StartTime > lastDate.Value)
                                    select inv.TransitQty).Sum();

                        }
                    }
                    else if (locOrFlow == "Flow")
                    {
                        if (winOrStartTime == "WindowTime")
                        {
                            rQty = (from plan in mrpShipPlanViews
                                    where plan.Flow == scheduleHead.Flow
                                    && plan.Item == scheduleBody.Item
                                    && plan.WindowTime == scheduleHead.DateTo
                                    select plan.Qty).Sum();

                            aQty = (from inv in expectTransitInventoryViews
                                    where inv.Flow == scheduleHead.Flow
                                    && inv.Item == scheduleBody.Item
                                    && inv.WindowTime <= scheduleHead.DateTo
                                    && (!lastDate.HasValue || inv.WindowTime > lastDate.Value)
                                    select inv.TransitQty).Sum();


                        }
                        else if (winOrStartTime == "StartTime")
                        {
                            rQty = (from plan in mrpShipPlanViews
                                    where plan.Flow == scheduleHead.Flow
                                    && plan.Item == scheduleBody.Item
                                    && plan.StartTime == scheduleHead.DateFrom
                                    select plan.Qty).Sum();


                            aQty = (from inv in expectTransitInventoryViews
                                    where inv.Flow == scheduleHead.Flow
                                    && inv.Item == scheduleBody.Item
                                    && inv.StartTime <= scheduleHead.DateFrom
                                    && (!lastDate.HasValue || inv.StartTime > lastDate.Value)
                                    select inv.TransitQty).Sum();

                        }
                    }

                    decimal qQty = (rQty - (aQty.HasValue ? aQty.Value : 0)) > 0 ? (rQty - (aQty.HasValue ? aQty.Value : 0)) : 0;
                    foreach (PropertyInfo pi in scheduleBodyPropertyInfo)
                    {
                        if (pi.Name != null && StringHelper.Eq(pi.Name.ToLower(), actQty))
                        {
                            pi.SetValue(scheduleBody, aQty, null);
                            continue;
                        }
                        if ((aQty.HasValue && aQty.Value > 0 || rQty > 0) && pi.Name != null && StringHelper.Eq(pi.Name.ToLower(), qty))
                        {
                            pi.SetValue(scheduleBody, qQty, null);
                            continue;
                        }
                        if (pi.Name != null && StringHelper.Eq(pi.Name.ToLower(), requiredQty))
                        {
                            pi.SetValue(scheduleBody, rQty, null);
                            break;
                        }


                    }
                    i++;
                    if (winOrStartTime == "WindowTime")
                    {
                        lastDate = scheduleHead.DateTo;
                    }
                    else if (winOrStartTime == "StartTime")
                    {
                        lastDate = scheduleHead.DateFrom;
                    }
                    else
                    {
                        throw new TechnicalException(winOrStartTime);
                    }

                    if (lastScheduleHead != null)
                    {
                        scheduleHead.LastDateTo = lastScheduleHead.DateTo;
                        scheduleHead.LastDateFrom = lastScheduleHead.DateFrom;
                    }
                    lastScheduleHead = scheduleHead;
                }


            }

            #endregion

            ScheduleView scheduleView = new ScheduleView();
            scheduleView.ScheduleHeads = scheduleHeads;
            scheduleView.ScheduleBodys = scheduleBodys;
            return scheduleView;
        }

        #endregion Customized Methods
    }
}