using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class InspectOrder : InspectOrderBase
    {
        #region Non O/R Mapping Properties

        public void AddInspectOrderDetail(InspectOrderDetail inspectOrderDetail)
        {
            if (this.InspectOrderDetails == null)
            {
                this.InspectOrderDetails = new List<InspectOrderDetail>();
            }
            this.InspectOrderDetails.Add(inspectOrderDetail);
        }

        #endregion
    }
}