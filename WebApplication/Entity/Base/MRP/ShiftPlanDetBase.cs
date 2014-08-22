using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class ShiftPlanDetBase : EntityBase
    {
        //Id, MstrId, ProdLine, Item, ItemDesc, Qty, OrderQty, Uom, UnitQty, PlanDate, Shift, CreateDate, CreateUser, LastModifyDate, LastModifyUser
        public Int32 Id { get; set; }
        public Int32 MstrId { get; set; }
        public string ProdLine { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public decimal Qty { get; set; }
        public string Uom { get; set; }
        public decimal UnitCount { get; set; }
        public DateTime PlanDate { get; set; }
        public string Shift { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        
        

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
            ShiftPlanDetBase another = obj as ShiftPlanDetBase;

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
