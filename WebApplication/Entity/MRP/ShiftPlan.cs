using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    public partial class ShiftPlan
    {
        public double QtyA { get; set; }
        public double QtyB { get; set; }
        public double QtyC { get; set; }

        public int IdA { get; set; }
        public int IdB { get; set; }
        public int IdC { get; set; }

        public string MemoA { get; set; }
        public string MemoB { get; set; }
        public string MemoC { get; set; }

        public decimal? OrderLotSize { get; set; }
        //public double OrderLotSizeB { get; set; }
        //public double OrderLotSizeC { get; set; }
    }
}