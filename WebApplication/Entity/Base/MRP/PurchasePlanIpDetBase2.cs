using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class PurchasePlanIpDetBase2 : EntityBase
    {
        //Id, PurchasePlanId, IpNo, Flow, Item, StartTime, WindowTime, Plant, Qty, CreateDate, CreateUser
        public Int32 Id { get; set; }
        public Int32 PurchasePlanId { get; set; }
        public string IpNo { get; set; }
        public string Flow { get; set; }
        public string Item { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime WindowTime { get; set; }
        public string Plant { get; set; }
        public decimal Qty { get; set; }
        public string CreateUser { get; set; }
        public DateTime CreateDate { get; set; }
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
            PurchasePlanIpDetBase2 another = obj as PurchasePlanIpDetBase2;

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
