using System;

namespace com.Sconit.Entity.MRP
{
    public class ForcastPlan
    {
        public string FlowCode { get; set; }
        public string DateIndexFrom { get; set; }
        public string DateIndexTo { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string UomCode { get; set; }
        public Double Qty { get; set; }
        public Double Uc { get; set; }
        public string LocationTo { get; set; }
        public Double InQty { get; set; }
        public string Supplier { get; set; }
    }

   
}