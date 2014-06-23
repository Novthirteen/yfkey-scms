using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    public class FirmPlan
    {
        public DateTime PlanDate { get; set; }
        public string FlowCode { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string UomCode { get; set; }
        public Double InQty { get; set; }
        public Double OutQty { get; set; }
        public Double EndQty { get; set; }
        public Double Qty { get; set; }
        public Double Uc { get; set; }
        public Double SafeStock { get; set; }
        public Double MaxStock { get; set; }
        public Double InvQty { get; set; }
        public string LocationTo { get; set; }
        public string Supplier { get; set; }
        public Double InProdQty { get; set; }
        
    }
}