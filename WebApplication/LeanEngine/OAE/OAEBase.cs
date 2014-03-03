// **************************************************************************
// This is OAE(Order Automation Engine), a component of the Lean Engine.
// It can reduce much more work for the planner and offering the suggestions 
// of When to order? Order what? Who can supply? How many/much to purchase?
// Author: Deng Xuyao.  Date: 2010-07.
// **************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Entity;
using LeanEngine.Utility;

namespace LeanEngine.OAE
{
    public abstract class OAEBase : IOAE
    {
        #region Variables
        protected List<Plans> Plans;
        protected List<InvBalance> InvBalances;
        protected List<DemandChain> DemandChains;
        #endregion

        #region Constructor
        public OAEBase(List<Plans> Plans, List<InvBalance> InvBalances, List<DemandChain> DemandChains)
        {
            this.Plans = Plans;
            this.InvBalances = InvBalances;
            this.DemandChains = DemandChains;
        }
        #endregion

        #region Public Methods
        public virtual void ProcessTime(Flow flow)
        {
            if (flow == null || flow.FlowStrategy == null)
                throw new BusinessException("Key value is empty");

            int weekInterval = flow.FlowStrategy.WeekInterval;

            if (!flow.WindowTime.HasValue)
                this.SetDefaultWindowTime(flow);

            string[][] windowTimes = this.CreateWindowTimes(flow.FlowStrategy);
            if (windowTimes == null)
                return;

            #region If restart after pausing for a long time
            if (!flow.WindowTime.HasValue || flow.WindowTime.Value.AddHours(-flow.FlowStrategy.EmLeadTime) < DateTime.Now)
            {
                flow.WindowTime = this.GetNextWindowTime(DateTime.Now, windowTimes, weekInterval);
                flow.OrderTime = this.GetNextOrderTime(flow.WindowTime, flow.FlowStrategy.LeadTime);
                flow.NextWindowTime = flow.WindowTime;
                flow.NextOrderTime = flow.OrderTime;
                flow.IsUpdateWindowTime = true;
            }
            #endregion

            if (!flow.OrderTime.HasValue || flow.OrderTime <= DateTime.Now)
            {
                flow.NextWindowTime = this.GetNextWindowTime(flow.WindowTime.Value, windowTimes, weekInterval);
                flow.NextOrderTime = this.GetNextOrderTime(flow.NextWindowTime, flow.FlowStrategy.LeadTime);
                flow.IsUpdateWindowTime = true;
            }
        }

        public virtual void ProcessReqQty(ItemFlow itemFlow)
        {
            if (itemFlow == null)
                throw new BusinessException("Key value is empty");

            if (itemFlow.OrderTracers == null)
                itemFlow.OrderTracers = new List<OrderTracer>();

            string loc = itemFlow.LocTo;
            string item = itemFlow.Item;
            string flowCode = itemFlow.Flow.Code;
            itemFlow.ReqQty = 0;
            itemFlow.IsEmergency = false;

            #region DemandSources
            if (itemFlow.DemandSources == null)
                itemFlow.DemandSources = new List<string>();

            if (!itemFlow.DemandSources.Contains(loc) && loc != null)
                itemFlow.DemandSources.Add(loc);

            if (itemFlow.DemandSources.Count == 0)
                return;

            itemFlow.DemandSources = itemFlow.DemandSources.Distinct().ToList();
            #endregion

            itemFlow.ReqQty = this.GetReqQty(itemFlow);
        }
        #endregion

        #region Virtual Methods
        protected virtual void SetDefaultWindowTime(Flow flow)
        {
            if (flow == null || flow.FlowStrategy == null)
                throw new BusinessException("Key value is empty");

            flow.WindowTime = DateTime.Now.AddHours(flow.FlowStrategy.LeadTime);
        }

        protected virtual decimal GetOnhandInv(string loc, string item)
        {
            return InvBalances.Where(i => Utilities.StringEq(loc, i.Loc) && Utilities.StringEq(item, i.Item)).Sum(i => i.Qty);
        }
        protected virtual decimal GetInvBalance(string loc, string item, Enumerators.InvType invType)
        {
            return InvBalances.Where(i =>
                Utilities.StringEq(loc, i.Loc) &&
                Utilities.StringEq(item, i.Item) &&
                invType == i.InvType)
                .Sum(i => i.Qty);
        }

        protected virtual decimal GetRctPlanQty(string loc, string item, DateTime? time)
        {
            if (Plans == null)
                return 0;
            else
                return Plans.Where(p =>
                        p.Item == item &&
                        p.Loc == loc &&
                        (!time.HasValue || p.ReqTime <= time.Value) &&//less equal
                        p.IRType == Enumerators.IRType.RCT &&
                        p.PlanType == Enumerators.PlanType.Plans).Sum(p => p.Qty);
        }

        protected virtual decimal GetIssPlanQty(string loc, string item, DateTime? time)
        {
            if (Plans == null)
                return 0;
            else
                return Plans.Where(p =>
                        p.Item == item &&
                        p.Loc == loc &&
                        (!time.HasValue || p.ReqTime < time.Value) &&//less than
                        p.IRType == Enumerators.IRType.ISS &&
                        p.PlanType == Enumerators.PlanType.Plans).Sum(p => p.Qty);
        }

        protected virtual decimal GetRctOrderQty(string loc, string item, DateTime? time)
        {
            if (Plans == null)
                return 0;
            else
                return Plans.Where(p =>
                        p.Item == item &&
                        p.Loc == loc &&
                        (!time.HasValue || p.ReqTime <= time.Value) &&//less equal
                        p.IRType == Enumerators.IRType.RCT &&
                        p.PlanType == Enumerators.PlanType.Orders).Sum(p => p.Qty);
        }

        protected virtual List<OrderTracer> GetOrderRct(string loc, string item, DateTime? startTime, DateTime? endTime)
        {
            if (Plans == null || Plans.Count == 0)
                return null;

            var query = from p in Plans
                        where Utilities.StringEq(p.Item, item) && Utilities.StringEq(p.Loc, loc) &&
                        (!startTime.HasValue || p.ReqTime > startTime.Value) &&//greater than
                        (!endTime.HasValue || p.ReqTime <= endTime.Value) &&//less equal
                        p.IRType == Enumerators.IRType.RCT &&
                        p.PlanType == Enumerators.PlanType.Orders
                        select new OrderTracer
                        {
                            TracerType = Enumerators.TracerType.OrderRct,
                            Code = p.OrderNo,
                            ReqTime = p.ReqTime,
                            Item = p.Item,
                            OrderedQty = p.OrderedQty,
                            FinishedQty = p.FinishedQty,
                            Qty = p.Qty,
                            RefOrderLocTransId = p.ID
                        };

            return query.ToList();
        }

        protected virtual decimal GetIssOrderQty(string loc, string item, DateTime? time)
        {
            if (Plans == null)
                return 0;
            else
                return Plans.Where(p =>
                        p.Item == item &&
                        p.Loc == loc &&
                        (!time.HasValue || p.ReqTime < time.Value) &&//less than
                        p.IRType == Enumerators.IRType.ISS &&
                        p.PlanType == Enumerators.PlanType.Orders).Sum(p => p.Qty);
        }

        protected virtual List<OrderTracer> GetOrderIss(string loc, string item, DateTime? startTime, DateTime? endTime)
        {
            return this.GetOrderIss(loc, item, startTime, endTime, Enumerators.TracerType.OrderIss);
        }
        protected virtual List<OrderTracer> GetOrderIss(string loc, string item, DateTime? startTime, DateTime? endTime, Enumerators.TracerType tracerType)
        {
            if (Plans == null || Plans.Count == 0)
                return null;

            var query = from p in Plans
                        where Utilities.StringEq(p.Item, item) && Utilities.StringEq(p.Loc, loc) &&
                        (!startTime.HasValue || p.ReqTime >= startTime.Value) &&//greater equal
                        (!endTime.HasValue || p.ReqTime < endTime.Value) &&//less than
                        p.IRType == Enumerators.IRType.ISS &&
                        p.PlanType == Enumerators.PlanType.Orders
                        select new OrderTracer
                        {
                            TracerType = tracerType,
                            Code = p.OrderNo,
                            ReqTime = p.ReqTime,
                            Item = p.Item,
                            OrderedQty = p.OrderedQty,
                            FinishedQty = p.FinishedQty,
                            Qty = p.Qty,
                            RefOrderLocTransId = p.ID
                        };

            return query.ToList();
        }

        protected virtual decimal GetAvailableInvQty(string loc, string item, DateTime? time)
        {
            decimal rctOrderQty = this.GetRctOrderQty(loc, item, time);
            decimal rctPlanQty = this.GetRctPlanQty(loc, item, time);
            decimal issOrderQty = this.GetIssOrderQty(loc, item, time);
            decimal issPlanQty = this.GetIssPlanQty(loc, item, time);
            decimal onhandInv = this.GetOnhandInv(loc, item);

            decimal rctQty = Math.Max(rctOrderQty, rctPlanQty);
            decimal issQty = Math.Max(issOrderQty, issPlanQty);

            return onhandInv + rctQty - issQty;
        }

        //protected virtual DateTime? GetRelativeTime(DateTime? time, DemandChain demandChain)
        //{
        //    double relativeLeadTime = demandChain.RelativeLeadTime;

        //    if (time.HasValue)
        //        time = time.Value.AddHours(relativeLeadTime);

        //    return time;
        //}

        //protected virtual decimal GetRelativeQty(decimal qty, DemandChain demandChain)
        //{
        //    decimal relativeQtyPer = demandChain.RelativeQtyPer;
        //    return qty * relativeQtyPer;
        //}

        protected virtual decimal GetReqQty(List<OrderTracer> list)
        {
            decimal reqQty = 0;
            if (list != null && list.Count > 0)
            {
                decimal totalQty = list.Sum(l => l.Qty);
                decimal issQty = list.Where(l =>
                    l.TracerType == Enumerators.TracerType.Demand ||
                    l.TracerType == Enumerators.TracerType.OrderIss)
                    .Sum(l => l.Qty);

                decimal rctQty = totalQty - issQty;

                reqQty = issQty > rctQty ? issQty - rctQty : 0;
            }

            return reqQty;
        }

        #region Order Tracer
        protected virtual OrderTracer GetDemand_OrderTracer(ItemFlow itemFlow)
        {
            OrderTracer orderTracer = new OrderTracer();
            orderTracer.TracerType = Enumerators.TracerType.Demand;
            orderTracer.Code = itemFlow.Flow.Code;
            orderTracer.ReqTime = DateTime.Now;
            orderTracer.Item = itemFlow.Item;
            orderTracer.Qty = itemFlow.SafeInv;

            return orderTracer;
        }

        protected virtual OrderTracer GetOnhandInv_OrderTracer(string loc, string item)
        {
            OrderTracer orderTracer = new OrderTracer();
            orderTracer.TracerType = Enumerators.TracerType.OnhandInv;
            orderTracer.Code = loc;
            orderTracer.ReqTime = DateTime.Now;
            orderTracer.Item = item;
            orderTracer.Qty = this.GetInvBalance(loc, item, Enumerators.InvType.Normal);

            return orderTracer;
        }

        protected virtual OrderTracer GetInspectInv_OrderTracer(string loc, string item)
        {
            OrderTracer orderTracer = new OrderTracer();
            orderTracer.TracerType = Enumerators.TracerType.InspectInv;
            orderTracer.Code = loc;
            orderTracer.ReqTime = DateTime.Now;
            orderTracer.Item = item;
            orderTracer.Qty = this.GetInvBalance(loc, item, Enumerators.InvType.Inspect);

            return orderTracer;
        }
        #endregion
        #endregion

        #region Abstract Methods
        protected abstract decimal GetReqQty(ItemFlow itemFlow);
        #endregion

        #region Private Methods
        #region Time Processor
        private DateTime? GetNextWindowTime(DateTime? windowTime, string[][] windowTimes, int weekInterval)
        {
            if (!windowTime.HasValue || windowTimes == null)
                return null;

            DateTime nextWindowTime = windowTime.Value;
            int nextWTweekday = (int)windowTime.Value.DayOfWeek;

            bool isGet = false;
            for (int i = 0; i <= 7; i++)
            {
                if (isGet) break;

                int weekday = nextWTweekday + i;
                if (weekday >= 7) weekday = weekday - 7;

                string[] wins = windowTimes[weekday];
                if (wins == null || wins.Length == 0)
                    continue;

                foreach (string s in wins)
                {
                    if (s.Equals(string.Empty)) break;

                    string[] ts = s.Split(":".ToCharArray());
                    TimeSpan tspan = new TimeSpan(Int32.Parse(ts[0]), Int32.Parse(ts[1]), 0);

                    DateTime newTime = windowTime.Value.Date.AddDays(i).Add(tspan);

                    if ((windowTime < DateTime.Now && newTime > DateTime.Now) ||
                        (windowTime >= DateTime.Now && newTime > windowTime))
                    {
                        if (weekInterval > 0)
                        {
                            newTime = newTime.AddDays(weekInterval * 7);
                        }

                        nextWindowTime = newTime;
                        isGet = true;
                        break;
                    }
                }
            }

            return nextWindowTime;
        }

        //创建窗口时间组
        private string[][] CreateWindowTimes(FlowStrategy flowStrategy)
        {
            if (flowStrategy == null)
                return null;

            string[][] windowTimes = new string[7][];

            windowTimes[0] = flowStrategy.SunWinTime;
            windowTimes[1] = flowStrategy.MonWinTime;
            windowTimes[2] = flowStrategy.TueWinTime;
            windowTimes[3] = flowStrategy.WedWinTime;
            windowTimes[4] = flowStrategy.ThuWinTime;
            windowTimes[5] = flowStrategy.FriWinTime;
            windowTimes[6] = flowStrategy.SatWinTime;

            for (int i = 0; i < 7; i++)
            {
                if (windowTimes[i] != null && windowTimes[i].Length > 0)
                {
                    return windowTimes;
                }
            }
            return null;
        }

        private DateTime? GetNextOrderTime(DateTime? windowTime, double leadTime)
        {
            DateTime? orderTime = null;
            if (windowTime.HasValue)
            {
                orderTime = windowTime.Value.AddHours(-leadTime);
                if (orderTime.Value < DateTime.Now)
                    orderTime = DateTime.Now;
            }

            return orderTime;
        }

        private DateTime GetOrderStartTime(DateTime windowTime, double leadTime)
        {
            DateTime orderStartTime = windowTime.AddHours(-leadTime);

            return orderStartTime;
        }
        #endregion

        /// <summary>
        /// 3 Level filter
        /// </summary>
        /// <param name="itemFlow"></param>
        /// <returns></returns>
        //private List<DemandChain> GetDemandChainUnit(ItemFlow itemFlow)
        //{
        //    if (itemFlow == null)
        //        return null;

        //    string flowCode = itemFlow.Flow.Code;
        //    string item = itemFlow.Item;

        //    var q1 = DemandChains.Where(d =>
        //        Utilities.StringEq(d.FlowCode, flowCode)
        //        && Utilities.StringEq(d.Item, item)).ToList();
        //    if (q1.Count > 0)
        //        return q1;

        //    var q2 = DemandChains.Where(d =>
        //        Utilities.StringEq(d.FlowCode, flowCode)
        //        && Utilities.StringEq(d.Item, null)).ToList();

        //    return q2;
        //}

        //private List<DemandChain> GetDemandChain(DemandChain demandChainUnit)
        //{
        //    if (demandChainUnit == null || DemandChains == null)
        //        return null;

        //    var query = DemandChains
        //        .Where(d => Utilities.StringEq(demandChainUnit.Code, d.Code) && !d.Equals(demandChainUnit))
        //        .OrderBy(d => d.BomLevel).ToList();

        //    return query;
        //}
        #endregion
    }
}
