using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class Flow : FlowBase
    {
        #region Non O/R Mapping Properties

        public void AddFlowDetail(FlowDetail flowDetail)
        {
            if (this.FlowDetails == null)
            {
                this.FlowDetails = new List<FlowDetail>();
            }

            this.FlowDetails.Add(flowDetail);
        }

        #endregion
    }
}