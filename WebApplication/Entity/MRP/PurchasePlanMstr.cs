using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class PurchasePlanMstr : PurchasePlanMstrBase
    {
        #region Non O/R Mapping Properties

        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string Msg { get; set; }
        public string PlanDate { get; set; }
        public string Bom { get; set; }
        public decimal? Qty { get; set; }
        public string EffDateFormat { get { return this.EffDate.ToShortDateString(); } }
        #endregion
    }
}