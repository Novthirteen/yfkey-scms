using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class ScmsBom : EntityBase
    {
        public string Bom { get; set; }
        public Int32 Flag { get; set; }
        public string ItemCode { get; set; }
        public string Version { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        public Decimal Qty { get; set; }

        public override int GetHashCode()
        {
            if (Bom != null && ItemCode != null)
            {
                return Bom.GetHashCode() ^ ItemCode.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ScmsBom another = obj as ScmsBom;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Bom == another.Bom && this.ItemCode == another.ItemCode);
            }
        }
    }

}
