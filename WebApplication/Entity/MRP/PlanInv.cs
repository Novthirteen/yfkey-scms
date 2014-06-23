using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    public class PlanInv
    {
        public string ItemCode { get; set; }
        public Double SafeStock { get; set; }
        public Double MaxStock { get; set; }
        public Double InvQty { get; set; }
        public Double InQty { get; set; }
        public Double FgBreakQty { get; set; }
        public Double RecQty { get; set; }
    }
}