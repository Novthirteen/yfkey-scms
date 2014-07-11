using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class ShipPlanOpenOrderBase : EntityBase
    {
        //Id, ShipPlanId, UUID, OrderNo, Item, StartTime, WindowTime, OrderQty, ShipQty, RecQty, CreateDate, CreateUser
        public Int32 Id { get; set; }
        public Int32 ShipPlanId { get; set; }
        public string UUID { get; set; }
        public string OrderNo { get; set; }
        public string Item { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime WindowTime { get; set; }
        public DateTime OrgStartTime { get; set; }
        public DateTime OrgWindowTime { get; set; }
        public decimal OrderQty { get; set; }
        public decimal ShipQty { get; set; }
        public decimal RecQty { get; set; }
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
            ShipPlanOpenOrderBase another = obj as ShipPlanOpenOrderBase;

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
