using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class WeeklyPurchasePlanDet : WeeklyPurchasePlanDetBase
    {
        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 

        public Int32 ReleaseNo { get; set; }
        public Int32 BatchNo { get; set; }
        public DateTime EffDate { get; set; }
        public string Status { get; set; }
        public string OrderDets { get; set; }

        #endregion
    }
}