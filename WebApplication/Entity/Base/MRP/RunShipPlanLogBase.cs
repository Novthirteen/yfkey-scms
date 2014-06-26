using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class RunShipPlanLogBase : EntityBase
    {
        #region O/R Mapping Properties

        public Int32 Id { get; set; }
        public Int32 BatchNo { get; set; }
        public DateTime EffDate { get; set; }
        public string Lvl { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string RefItem { get; set; }
        public decimal Qty { get; set; }
        public decimal UnitQty { get; set; }
        public decimal MinusQty { get; set; }
        public decimal EndQty { get; set; }
        public string LocFrom { get; set; }
        public string LocTo { get; set; }
        public string Flow { get; set; }
        public string SourceType { get; set; }
        public string SourceDateType { get; set; }
        public string SourceId { get; set; }
        public string InvType { get; set; }
        public string InvLocation { get; set; }
        public decimal InvQty { get; set; }
        public decimal InvSafeQty { get; set; }
        public DateTime? InvEffDate { get; set; }
        public decimal EndInvQty { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? WindowTime { get; set; }
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
            RunShipPlanLogBase another = obj as RunShipPlanLogBase;

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
