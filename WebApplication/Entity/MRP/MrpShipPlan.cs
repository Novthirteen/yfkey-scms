using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class MrpShipPlan : MrpShipPlanBase
    {
        #region Non O/R Mapping Properties
        public string RefFlows;
        #endregion
    }
}