using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class MesScmsStationBox : EntityBase
    {
        public Int32 Id { get; set; }
        public string OrderNo { get; set; }
        public string TagNo { get; set; }
        public string HuId { get; set; }
        public DateTime ScanTime { get; set; }
        public Int32 Flag { get; set; }

        public override int GetHashCode()
        {
            if (Id != null)
            {
                return this.Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            MesScmsStationBox another = obj as MesScmsStationBox;

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
