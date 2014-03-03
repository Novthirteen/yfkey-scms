using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class MesScmsCompletedBox : EntityBase
    {
        public string OrderNo { get; set; }
        public string ItemCode { get; set; }
        public string HuId { get; set; }
        public Int32 Qty { get; set; }
        public DateTime CompleteDate { get; set; }
        public Int32 Flag { get; set; }

        public override int GetHashCode()
        {
            if (HuId != null)
            {
                return this.HuId.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            MesScmsCompletedBox another = obj as MesScmsCompletedBox;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.HuId == another.HuId);
            }
        }
    }

}
