using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class ShipPlanDetTraceBase : EntityBase
    {
        //Id, ShipPlanId, UUID, DistributionFlow, Item, ReqDate, ReqQty, CreateDate, CreateUser
        public Int32 Id { get; set; }
        public Int32 ShipPlanId { get; set; }
        public string UUID { get; set; }
        public string DistributionFlow { get; set; }
        public string Item { get; set; }
        public DateTime ReqDate { get; set; }
        public decimal ReqQty { get; set; }
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
            ShipPlanDetTraceBase another = obj as ShipPlanDetTraceBase;

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
