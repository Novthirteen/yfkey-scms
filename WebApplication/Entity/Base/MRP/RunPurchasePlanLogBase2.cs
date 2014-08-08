using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class RunPurchasePlanLogBase2 : EntityBase
    {
        //<!--Id, BatchNo, Lvl, Item, Uom, Qty, PlanDate, Bom, Msg, CreateDate, CreateUser-->
        #region O/R Mapping Properties

        public Int32 Id { get; set; }
        public Int32 BatchNo { get; set; }
        public string Lvl { get; set; }
        public string Item { get; set; }
        public string Uom { get; set; }
        public decimal Qty { get; set; }
        public DateTime PlanDate { get; set; }
        public string Bom { get; set; }
        public string Msg { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        #endregion

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
            RunPurchasePlanLogBase2 another = obj as RunPurchasePlanLogBase2;

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
