using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class MesScmsShelfPart : EntityBase
    {
        public string ShelfNo { get; set; }
        public Int32 Flag { get; set; }
        public string ItemCode { get; set; }
        

        public override int GetHashCode()
        {
            if (ShelfNo != null && ItemCode != null)
            {
                return this.ShelfNo.GetHashCode() ^ this.ItemCode.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            MesScmsShelfPart another = obj as MesScmsShelfPart;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.ShelfNo == another.ShelfNo && this.ItemCode == another.ItemCode);
            }
        }
    }

}
