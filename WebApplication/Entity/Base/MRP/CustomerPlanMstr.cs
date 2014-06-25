using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public partial class CustomerPlanMstr : EntityBase
    {
        #region O/R Mapping Properties
        public string PlanNo { get; set; }
        public string Flow { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        public int Version { get; set; }
        
        #endregion

        public override int GetHashCode()
        {
            if (!string.IsNullOrEmpty(PlanNo))
            {
                return PlanNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            CustomerPlanMstr another = obj as CustomerPlanMstr;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.PlanNo == another.PlanNo);
            }
        }
    }

}
