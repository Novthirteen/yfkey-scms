using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class ProductionPlanDet : ProductionPlanDetBase
    {
        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here.

        public int ReleaseNo { get; set; }

        #endregion
    }
}