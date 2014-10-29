using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public class LocationLotDetailView : LocationLotDetailViewBase
    {
        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 
        public string ItemCode { get; set; }
        public string ItemDesc { get; set; }
        public string Uom { get; set; }
        public string LocCode { get; set; }
        public string LocName { get; set; }
        public string BinCode { get; set; }
        #endregion
    }
}