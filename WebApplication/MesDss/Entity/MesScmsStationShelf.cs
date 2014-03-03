using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class MesScmsStationShelf : EntityBase
    {
        public string ShelfNo { get; set; }
        public Int32 Flag { get; set; }
        public Int32 Qty { get; set; }
        public string LineName { get; set; }
        public string StationName { get; set; }

        public override int GetHashCode()
        {
            if (ShelfNo != null)
            {
                return ShelfNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            MesScmsStationShelf another = obj as MesScmsStationShelf;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.ShelfNo == another.ShelfNo);
            }
        }
    }

}
