using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class MesScmsRepairMaterial : EntityBase
    {
        public Int32 Id { get; set; }
        public string RejectNo { get; set; }
        public string ItemCode { get; set; }
        public string ProductLine { get; set; }
        public string OrderNo { get; set; }
        public Int32 Type { get; set; }
        public DateTime CreateDate { get; set; }
        public Int32 Qty { get; set; }
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
            MesScmsRepairMaterial another = obj as MesScmsRepairMaterial;

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
