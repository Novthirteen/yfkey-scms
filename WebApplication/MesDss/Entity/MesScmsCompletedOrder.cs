using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class MesScmsCompletedOrder : EntityBase
    {
        public string OrderNo { get; set; }
        public Int32 Flag { get; set; }

        public override int GetHashCode()
        {
            if (OrderNo != null)
            {
                return this.OrderNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            MesScmsCompletedOrder another = obj as MesScmsCompletedOrder;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.OrderNo == another.OrderNo);
            }
        }
    }

}
