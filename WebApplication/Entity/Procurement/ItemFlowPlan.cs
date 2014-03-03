using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    [Serializable]
    public class ItemFlowPlan : ItemFlowPlanBase
    {
        #region Non O/R Mapping Properties

        public void AddItemFlowPlanDetail(ItemFlowPlanDetail itemFlowPlanDetail)
        {
            if (this.ItemFlowPlanDetails == null)
            {
                this.ItemFlowPlanDetails = new List<ItemFlowPlanDetail>();
            }

            this.ItemFlowPlanDetails.Add(itemFlowPlanDetail);
        }

        public void AddRangeItemFlowPlanDetail(IList<ItemFlowPlanDetail> itemFlowPlanDetailList)
        {
            if (itemFlowPlanDetailList != null && itemFlowPlanDetailList.Count > 0)
            {
                foreach (ItemFlowPlanDetail itemFlowPlanDetail in itemFlowPlanDetailList)
                {
                    this.AddItemFlowPlanDetail(itemFlowPlanDetail);
                }
            }
        }

        private Decimal _startPAB;
        public Decimal StartPAB
        {
            get
            {
                return _startPAB;
            }
            set
            {
                _startPAB = value;
            }
        }

        #endregion
    }
}