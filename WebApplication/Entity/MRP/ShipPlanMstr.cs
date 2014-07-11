using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class ShipPlanMstr : ShipPlanMstrBase
    {
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string Msg { get; set; }
        public string Flow { get; set; }
        public string EffDateFormat { get { return this.EffDate.ToShortDateString(); } }
    }

}