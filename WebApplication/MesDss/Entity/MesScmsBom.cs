using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class MesScmsBom : EntityBase
    {
        public string Bom { get; set; }
        public Int32 Flag { get; set; }
        public string ItemCode { get; set; }
        public string TagNo { get; set; }
        public string ProductLine { get; set; }
        public string Operation { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        public Decimal Qty { get; set; }

        public override int GetHashCode()
        {
            if (Bom != null && ItemCode != null && ProductLine != null && TagNo != null)
            {
                return this.Bom.GetHashCode() ^ this.ItemCode.GetHashCode() ^ this.ProductLine.GetHashCode() ^ this.TagNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            MesScmsBom another = obj as MesScmsBom;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Bom == another.Bom && this.ItemCode == another.ItemCode && this.ProductLine ==another.ProductLine && this.TagNo == another.TagNo);
            }
        }
    }

}
