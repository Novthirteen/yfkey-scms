using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Entity;
using LeanEngine.Utility;

namespace LeanEngine.OAE
{
    /// <summary>
    /// Order Point Method
    /// </summary>
    public class ODP : KB
    {
        public ODP(List<Plans> Plans, List<InvBalance> InvBalances, List<DemandChain> DemandChains)
            : base(Plans, InvBalances, DemandChains)
        {
        }

        public override void ProcessTime(Flow flow)
        {
            flow.WindowTime = DateTime.Now.AddHours(flow.FlowStrategy.LeadTime);
        }

        protected override bool CheckReq(ItemFlow itemFlow)
        {
            bool isReq = false;
            DateTime? orderTime = itemFlow.Flow.OrderTime;
            decimal reqQty = this.GetReqQty(itemFlow.OrderTracers);

            if (reqQty > 0)
            {
                isReq = true;
            }

            return isReq;
        }
    }
}
