using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class ShipPlanIpDetBase : EntityBase
    {
        //Id, ShipPlanId, IpNo, Flow, Item, StartTime, WindowTime, Plant, Qty
        public Int32 Id { get; set; }
        public Int32 ShipPlanId { get; set; }
        public string IpNo { get; set; }
        public string Flow { get; set; }
        public string Item { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime WindowTime { get; set; }
        public string Plant { get; set; }
        public decimal Qty { get; set; }
        
        

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
            ShipPlanIpDetBase another = obj as ShipPlanIpDetBase;

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
