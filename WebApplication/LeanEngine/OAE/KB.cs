using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Entity;
using LeanEngine.Utility;

namespace LeanEngine.OAE
{
    /// <summary>
    /// Kanban
    /// </summary>
    public class KB : OAEBase
    {
        public KB(List<Plans> Plans, List<InvBalance> InvBalances, List<DemandChain> DemandChains)
            : base(Plans, InvBalances, DemandChains)
        {
        }

        protected override decimal GetReqQty(ItemFlow itemFlow)
        {
            string item = itemFlow.Item;
            DateTime? winTime = itemFlow.Flow.WindowTime;
            decimal maxInv = itemFlow.MaxInv;
            decimal safeInv = itemFlow.SafeInv;

            #region Demand
            OrderTracer demand = this.GetDemand_OrderTracer(itemFlow);
            itemFlow.AddOrderTracer(demand);
            #endregion

            foreach (var loc in itemFlow.DemandSources)
            {
                #region OnhandInv
                OrderTracer onhandInv = this.GetOnhandInv_OrderTracer(loc, item);
                itemFlow.AddOrderTracer(onhandInv);
                #endregion

                #region InspectInv
                OrderTracer inspectInv = this.GetInspectInv_OrderTracer(loc, item);
                itemFlow.AddOrderTracer(inspectInv);
                #endregion

                #region OrderRct
                var orderRcts = this.GetOrderRct(loc, item, null, winTime);
                itemFlow.AddOrderTracer(orderRcts);
                #endregion
            }

            decimal reqQty = 0;
            bool isReq = this.CheckReq(itemFlow);
            if (isReq)
            {
                demand.Qty = maxInv;//Actual demand
                reqQty = this.GetReqQty(itemFlow.OrderTracers);
            }

            return reqQty;
        }

        protected virtual bool CheckReq(ItemFlow itemFlow)
        {
            bool isReq = false;
            DateTime? orderTime = itemFlow.Flow.OrderTime;
            decimal reqQty = this.GetReqQty(itemFlow.OrderTracers);

            if (reqQty > 0)
            {
                //Emergency
                isReq = true;
                itemFlow.IsEmergency = true;
            }
            else if (!orderTime.HasValue || orderTime.Value <= DateTime.Now)
            {
                //Normal
                isReq = true;
            }

            return isReq;
        }
    }
}
