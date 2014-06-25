using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public partial class CustomerPlanDet : EntityBase
    {
        #region O/R Mapping Properties
        public Int32 Id { get; set; }
        public string PlanNo { get; set; }
        public string Type { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string ItemRef { get; set; }
        public string Uom { get; set; }
        public decimal UnitCount { get; set; }
        public decimal Qty { get; set; }
        public string Location { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        public int Version { get; set; }
        #endregion

        public override int GetHashCode()
        {
            if (Id != 0 )
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
            CustomerPlanDet another = obj as CustomerPlanDet;

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
