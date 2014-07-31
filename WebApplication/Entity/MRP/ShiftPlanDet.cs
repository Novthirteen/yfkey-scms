using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class ShiftPlanDet : ShiftPlanDetBase
    {

        public string Status { get; set; }
        public Int32 Version { get; set; }
        public Int32 ReleaseNo { get; set; }
        public decimal OrderQty { get; set; }
    }
}