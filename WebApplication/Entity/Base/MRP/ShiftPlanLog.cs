using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public partial class ShiftPlanLog : EntityBase
    {
        #region O/R Mapping Properties
        public Int32 PlanId { get; set; }//联合主键
        public Int32 PlanVersion { get; set; }//联合主键

        public DateTime PlanDate { get; set; }
        public string Shift { get; set; }
        public string Flow { get; set; }
        public string Item { get; set; }
        public string ItemDescription { get; set; }
        public Double Qty { get; set; }
        public decimal UnitQty { get; set; }
        public string Uom { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        #endregion

        public override int GetHashCode()
        {
            if (PlanId != 0 && PlanVersion != 0)
            {
                return PlanId.GetHashCode()
                    ^ PlanVersion.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ShiftPlanLog another = obj as ShiftPlanLog;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.PlanId == another.PlanId)
                    && (this.PlanVersion == another.PlanVersion);
            }
        }
    }

}
