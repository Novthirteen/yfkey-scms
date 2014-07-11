using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class PurchasePlanDetTraceBase : EntityBase
    {
        //Id, PurchasePlanId, UUID, ShiftPlanDetId, ShiftPlanMstrId, RefPlanNo, ProdLine, ProdItem, ProdQty, RateQty, 
        //ScrapPct, BomUnitQty, PurchaseUnitQty, BomUom, PurchaseUom, PlanDate, CreateDate, CreateUser-->
        public Int32 Id { get; set; }
        public Int32 PurchasePlanId { get; set; }
        public string UUID { get; set; }
        public Int32 ShiftPlanDetId { get; set; }
        public Int32 ShiftPlanMstrId { get; set; }
        public string RefPlanNo { get; set; }
        public string ProdLine { get; set; }
        public string ProdItem { get; set; }
        public decimal ProdQty { get; set; }
        public decimal RateQty { get; set; }
        public decimal ScrapPct { get; set; }
        public decimal BomUnitQty { get; set; }
        public decimal PurchaseUnitQty { get; set; }
        public string BomUom { get; set; }
        public string PurchaseUom { get; set; }
        public DateTime PlanDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public string Type { get; set; }
        

		public override int GetHashCode()
        {
			if (Id != null)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            PurchasePlanDetTraceBase another = obj as PurchasePlanDetTraceBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id);
            }
        } 
    }
	
}
