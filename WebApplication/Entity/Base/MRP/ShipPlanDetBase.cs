using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class ShipPlanDetBase : EntityBase
    {
        //Id, ShipPlanId, Flow, Item, ItemDesc, RefItemCode, ShipQty, Uom, BaseUom, UnitQty, UC, LocFrom, LocTo,
        //StartTime, WindowTime, CreateDate, CreateUser, LastModifyDate, LastModifyUser, Version
        public Int32 Id { get; set; }
        public Int32 ShipPlanId { get; set; }
        public string Flow { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string RefItemCode { get; set; }
        public decimal ShipQty { get; set; }
        public string Uom { get; set; }
        public string BaseUom { get; set; }
        public decimal UnitQty { get; set; }
        public decimal UnitCount { get; set; }
        public string LocFrom { get; set; }
        public string LocTo { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime WindowTime { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        public Int32 Version { get; set; }
        public decimal OrgShipQty { get; set; }
        public decimal ReqQty { get; set; }
        public string UUID { get; set; }
        public decimal OrderQty { get; set; }
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
            ShipPlanDetBase another = obj as ShipPlanDetBase;

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
