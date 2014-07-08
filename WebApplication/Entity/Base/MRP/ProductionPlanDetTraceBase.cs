using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class ProductionPlanDetTraceBase : EntityBase
    {
        //Id, ProductionPlanId, UUID, Flow, Item, Bom, ReqDate, ReqQty, RateQty, 
        //ScrapPct, Uom, UnitQty, CreateDate, CreateUser
        public Int32 Id { get; set; }
        public Int32 ProductionPlanId { get; set; }
        public string UUID { get; set; }
        public string Flow { get; set; }
        public string Item { get; set; }
        public string Bom { get; set; }
        public DateTime ReqDate { get; set; }
        public decimal ReqQty { get; set; }
        public decimal RateQty { get; set; }
        public decimal ScrapPct { get; set; }
        public string Uom { get; set; }
        public decimal UnitQty { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        

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
            ProductionPlanDetTraceBase another = obj as ProductionPlanDetTraceBase;

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
