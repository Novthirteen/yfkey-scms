using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class RunWeeklyMRPLogBase : EntityBase
    {
        //<!--Id, BatchNo, Lvl, Phase, Flow, Item, Msg, CreateDate, CreateUser-->
        #region O/R Mapping Properties

        public Int32 Id { get; set; }
        public Int32 BatchNo { get; set; }
        public string Lvl { get; set; }
        public string Phase { get; set; }
        public string Flow { get; set; }
        public string Item { get; set; }
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
            RunWeeklyMRPLogBase another = obj as RunWeeklyMRPLogBase;

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
