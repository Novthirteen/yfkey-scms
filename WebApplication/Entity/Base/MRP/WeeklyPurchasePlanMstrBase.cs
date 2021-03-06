using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class WeeklyPurchasePlanMstrBase : EntityBase
    {
        #region O/R Mapping Properties
        //<!--Id, ReleaseNo, BatchNo, EffDate, Status, CreateDate, CreateUser, LastModifyDate, LastModifyUser, ReleaseDate, ReleaseUser, Version-->
         //
        public Int32 Id { get; set; }
        public Int32 ReleaseNo { get; set; }
        public Int32 BatchNo { get; set; }
        public DateTime EffDate { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUser { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string ReleaseUser { get; set; }
        public Int32 Version { get; set; }
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
            WeeklyPurchasePlanMstrBase another = obj as WeeklyPurchasePlanMstrBase;

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
