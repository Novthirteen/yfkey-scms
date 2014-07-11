using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class ProductionPlanMstr : ProductionPlanMstrBase
    {
        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 

        public string Item { get; set; }
        public string Bom { get; set; }
        public string EffDateFormat { get { return this.EffDate.ToShortDateString(); } }
        public string Msg { get; set; }

        #endregion
    }
}