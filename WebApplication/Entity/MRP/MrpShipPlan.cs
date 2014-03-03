using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class MrpShipPlan : MrpShipPlanBase
    {
        #region Non O/R Mapping Properties
        public IList<int> FlowDetailIdList { get; set; }

        public void AddFlowDetailId(int id)
        {
            if (FlowDetailIdList == null)
            {
                FlowDetailIdList = new List<int>();
            }
            FlowDetailIdList.Add(id);
        }
        #endregion
    }
}