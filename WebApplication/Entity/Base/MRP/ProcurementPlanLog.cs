using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public partial class ProcurementPlanLog : EntityBase
    {
        #region O/R Mapping Properties
        public Int32 PlanId { get; set; }//联合主键
        public Int32 PlanVersion { get; set; }//联合主键

        public DateTime PlanDate { get; set; }
        public string Flow { get; set; }
        public string Item { get; set; }
        public string ItemDescription { get; set; }
        public string Uom { get; set; }
        public decimal UnitQty { get; set; }
        public string Location { get; set; }
        public decimal SafeStock { get; set; }
        public decimal MaxStock { get; set; }
        public decimal InvQty { get; set; }
        public decimal InQty { get; set; }
        public decimal OutQty { get; set; }
        public decimal OrderQty { get; set; }
        public decimal FinalQty { get; set; }
        public string LastModifyUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string Supplier { get; set; }
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
            ProcurementPlanLog another = obj as ProcurementPlanLog;

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
