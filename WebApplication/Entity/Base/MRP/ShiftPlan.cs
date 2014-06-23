using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public partial class ShiftPlan : EntityBase
    {
        #region O/R Mapping Properties
        public int Id { get; set; }
        public DateTime PlanDate { get; set; }
        public string Shift { get; set; }
        public string Flow { get; set; }
        public string Item { get; set; }
        public string ItemDescription { get; set; }
        public Double Qty { get; set; }
        public string Uom { get; set; }
        public decimal UnitQty { get; set; }
        public Int32 PlanVersion { get; set; }
        public string LastModifyUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string Memo { get; set; }
        
        #endregion

        public override int GetHashCode()
        {
            if (Id != 0)
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
            ShiftPlan another = obj as ShiftPlan;

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
