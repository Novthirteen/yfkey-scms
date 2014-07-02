using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class ProductionPlanDetBase : EntityBase
    {
        //Id, ProductionPlanId, Item, ItemDesc, RefItemCode, OrgQty, Qty, Uom,
    //StartTime, WindowTime, CreateDate, CreateUser, LastModifyUser, LastModifyDate, Version
        public Int32 Id { get; set; }
        public Int32 ProductionPlanId { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string RefItemCode { get; set; }
        public decimal OrgQty { get; set; }
        public decimal Qty { get; set; }
        public string Uom { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime WindowTime { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        public Int32 Version { get; set; }
        
        

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
