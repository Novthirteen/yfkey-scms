using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity;

//TODO: Add other using statements here

namespace com.Mes.Dss.Entity
{
    [Serializable]
    public class ScmsWorkOrderNew : EntityBase
    {
        public string OrderNo { get; set; }
        public string ProductLine { get; set; }
        public string IsAdditional { get; set; }
        public string RefOrderNo { get; set; }
        public string Shift { get; set; }
        public string ShiftCode { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime WindowTime { get; set; }
        public string RefItemCode { get; set; }
        public Int32 UC { get; set; }
        public string ItemCode { get; set; }
        public string Version { get; set; }
        public Int32 Qty { get; set; }
        public string Bom { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string LastModifyUser { get; set; }
        public Int32 Flag { get; set; }

        public override int GetHashCode()
        {
            if (OrderNo != null)
            {
                return OrderNo.GetHashCode() ^ ItemCode.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ScmsWorkOrderNew another = obj as ScmsWorkOrderNew;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.OrderNo == another.OrderNo && this.ItemCode == another.ItemCode);
            }
        }
    }

}
