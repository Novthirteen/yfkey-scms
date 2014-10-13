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

        public decimal InitStock { get; set; }

        public decimal SafeStock { get; set; }

        public decimal InTransitQty { get; set; }

        public string Logs { get; set; }

        public decimal MaxStock { get; set; }

        public string OrderDets { get; set; }

        public decimal InspectQty { get; set; }

        public string Status { get; set; }

        public decimal? InventoryCountDown { get; set; }
        
        #endregion
    }
}