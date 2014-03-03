// **************************************************************************************
// This is a solution for SCM(Supply Chain Management). It contains
// SCM(Supply Chain Model)/OAE(Order Automation Engine)/MPS(Master Production Schedule)/
// MRP(Material Requirements Planning)/APS(Advanced Planning and Scheduling) components.
// Author: Deng Xuyao.  Date: 2010-07.
// **************************************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Entity;
using LeanEngine.Utility;
using LeanEngine.OAE;
using System.Reflection;
using LeanEngine.SCM;

namespace LeanEngine
{
    /// <summary>
    /// Lean Engine
    /// </summary>
    public class Engine : IEngine, ISupplyChainMgr
    {
        private KB kb;
        private JIT jit;
        private ODP odp;
        private ISupplyChainMgr TheSupplyChainMgr = new SupplyChainMgr();

        public List<SupplyChain> BuildSupplyChain(string itemCode, List<ItemFlow> ItemFlows)
        {
            return TheSupplyChainMgr.BuildSupplyChain(itemCode, ItemFlows);
        }

        public IOAE GetProcessor(Enumerators.Strategy strategy)
        {
            Dictionary<Enumerators.Strategy, IOAE> dic = new Dictionary<Enumerators.Strategy, IOAE>();
            dic.Add(Enumerators.Strategy.KB, kb);
            dic.Add(Enumerators.Strategy.JIT, jit);
            dic.Add(Enumerators.Strategy.ODP, odp);

            if (dic.ContainsKey(strategy))
            {
                return dic[strategy];
            }
            return null;
        }

        public void TellMeDemands(EngineContainer container)
        {
            if (container == null || container.ItemFlows == null || container.ItemFlows.Count == 0)
                return;

            #region Initialize
            kb = new KB(container.Plans, container.InvBalances, container.DemandChains);
            jit = new JIT(container.Plans, container.InvBalances, container.DemandChains);
            odp = new ODP(container.Plans, container.InvBalances, container.DemandChains);
            #endregion

            #region Container param
            List<ItemFlow> ItemFlows = container.ItemFlows;
            List<Plans> Plans = container.Plans;
            List<InvBalance> InvBalances = container.InvBalances;
            List<DemandChain> DemandChains = container.DemandChains;
            #endregion

            #region Process time
            List<Flow> flows = ItemFlows.Select(s => s.Flow).Distinct().ToList<Flow>();
            this.ProcessTime(flows);
            #endregion

            #region Process ReqQty
            foreach (var itemFlow in ItemFlows)
            {
                this.DataValidCheck(itemFlow);
                this.SetFlowProperty(itemFlow, flows);

                this.ProcessReqQty(itemFlow);
            }
            #endregion
        }

        public List<Orders> DemandToOrders(List<ItemFlow> itemFlows)
        {
            #region ProcessOrderQty
            var query = from i in itemFlows
                        where i.ReqQty > 0
                        group i by new { i.LocTo, i.Item } into g
                        select new { g.Key, list = g.ToList(), Count = g.Count() };

            if (query != null && query.Count() > 0)
            {
                foreach (var g in query)
                {
                    if (g.Count == 1)
                    {
                        ItemFlow itemFlow = itemFlows.Single(i => i.Equals(g.list[0]));
                        this.ProcessOrderQty(itemFlow);
                    }
                    else
                    {
                        //to be testing
                        ItemFlow winBidItemFlow = this.BidItemFlow(g.list);
                        ItemFlow itemFlow = itemFlows.SingleOrDefault(i => i.Equals(winBidItemFlow));
                        this.ProcessOrderQty(itemFlow);
                    }
                }
            }
            #endregion

            List<Orders> result = new List<Orders>();
            #region Emergency Orders
            List<Orders> emOrders = this.BuildOrders(itemFlows, true);
            result = result.Concat(emOrders).ToList();
            #endregion

            #region Normal Orders
            List<Orders> nmlOrders = this.BuildOrders(itemFlows, false);
            result = result.Concat(nmlOrders).ToList();
            #endregion

            #region Filter
            result = result.Where(r => (r.IsEmergency && r.ItemFlows.Count > 0)
                || (!r.IsEmergency && (r.Flow.IsUpdateWindowTime || r.ItemFlows.Count > 0))).ToList();
            #endregion

            return result;
        }

        #region Data check
        private void DataValidCheck(ItemFlow itemFlow)
        {
            if (itemFlow.Flow == null)
            {
                throw new BusinessException("Flow is key infomation, it can't be empty!");
            }
        }
        private void DataValidCheck(Flow flow)
        {
            if (flow.FlowStrategy == null)
            {
                throw new BusinessException("FlowStrategy is key infomation, just tell me the strategy!");
            }
        }
        #endregion

        #region Setting properties
        private void SetFlowProperty(ItemFlow itemFlow, List<Flow> flows)
        {
            Flow flow = flows.Single(f => f.Equals(itemFlow.Flow));
            itemFlow.Flow.WindowTime = flow.WindowTime;
            itemFlow.Flow.NextOrderTime = flow.NextOrderTime;
            itemFlow.Flow.NextWindowTime = flow.NextWindowTime;
            itemFlow.Flow.IsUpdateWindowTime = flow.IsUpdateWindowTime;
        }
        #endregion

        #region Goto Processor
        private void ProcessTime(List<Flow> flows)
        {
            if (flows != null && flows.Count > 0)
            {
                foreach (var flow in flows)
                {
                    this.DataValidCheck(flow);
                    IOAE processor = this.GetProcessor(flow.FlowStrategy.Strategy);
                    if (processor != null)
                        processor.ProcessTime(flow);
                }
            }
        }

        private void ProcessReqQty(ItemFlow itemFlow)
        {
            IOAE processor = this.GetProcessor(itemFlow.Flow.FlowStrategy.Strategy);
            if (processor != null)
                processor.ProcessReqQty(itemFlow);
        }

        private void ProcessOrderQty(ItemFlow itemFlow)
        {
            if (itemFlow == null || itemFlow.ReqQty <= 0)
                return;

            decimal orderQty = itemFlow.ReqQty;
            decimal minLotSize = itemFlow.MinLotSize;//Min qty to order
            decimal UC = itemFlow.UC;//Unit container
            Enumerators.RoundUp roundUp = itemFlow.RoundUp;//Round up option
            //decimal orderLotSize = itemFlow.OrderLotSize;//Order lot size, one to many

            //Min lot size to order
            if (minLotSize > 0 && orderQty < minLotSize)
            {
                orderQty = minLotSize;
            }

            //round up
            if (UC > 0)
            {
                if (roundUp == Enumerators.RoundUp.Ceiling)
                {
                    orderQty = Math.Ceiling(orderQty / UC) * UC;
                }
                else if (roundUp == Enumerators.RoundUp.Floor)
                {
                    orderQty = Math.Floor(orderQty / UC) * UC;
                }
            }
            itemFlow.OrderQty = orderQty;

            //Order lot size, only production support
            //if (itemFlow.Flow.FlowType == Enumerators.FlowType.Production && orderLotSize > 0)
            //{
            //    itemFlow.OrderQtyList = this.SplitOrderByLotSize(orderQty, orderLotSize);
            //}
        }

        private List<decimal> SplitOrderByLotSize(decimal orderQty, decimal orderLotSize)
        {
            if (orderQty <= 0 || orderLotSize <= 0)
                return null;

            List<decimal> orderQtyList = new List<decimal>();
            if (orderLotSize > 0)
            {
                int count = (int)Math.Floor(orderQty / orderLotSize);
                for (int i = 0; i < count; i++)
                {
                    orderQtyList.Add(orderLotSize);
                }

                decimal oddQty = orderQty % orderLotSize;
                if (oddQty > 0)
                {
                    orderQtyList.Add(oddQty);
                }
            }

            return orderQtyList;
        }

        private ItemFlow BidItemFlow(List<ItemFlow> itemFlows)
        {
            if (itemFlows == null || itemFlows.Count == 0)
                return null;

            var q1 = itemFlows.Where(i => i.Flow.OrderTime <= DateTime.Now && i.ReqQty > 0).ToList();
            if (q1.Count == 0)
                return null;

            if (q1.Count == 1)
                return q1[0];

            //todo
            return q1[0];
        }
        #endregion

        #region Build Orders
        private List<Orders> BuildOrders(List<ItemFlow> itemFlows, bool isEmergency)
        {
            if (itemFlows == null || itemFlows.Count == 0)
                return null;

            var query =
                from i in itemFlows
                where i.IsEmergency == isEmergency || (!isEmergency && i.Flow.IsUpdateWindowTime)
                group i by i.Flow into g
                select new Orders
                {
                    Flow = g.Key,
                    IsEmergency = isEmergency,
                    ItemFlows = g.Where(i => i.OrderQty > 0 && i.IsEmergency == isEmergency).ToList(),
                    WindowTime = this.GetWindowTime(g.Key, isEmergency),
                    StartTime = this.GetStartTime(g.Key, isEmergency)
                };

            return query.ToList();
        }
        private DateTime GetWindowTime(Flow flow, bool isEmergency)
        {
            if (isEmergency)
                return DateTime.Now.AddHours(flow.FlowStrategy.EmLeadTime);
            else
                return flow.WindowTime.HasValue ? flow.WindowTime.Value : DateTime.Now.AddHours(flow.FlowStrategy.LeadTime);
        }
        private DateTime GetStartTime(Flow flow, bool isEmergency)
        {
            if (isEmergency)
            {
                return DateTime.Now;
            }
            else
            {
                DateTime windowTime = this.GetWindowTime(flow, isEmergency);
                return windowTime.AddHours(-flow.FlowStrategy.LeadTime);
            }
        }
        #endregion
    }
}
