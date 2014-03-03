using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class InspectItem : EntityBase
    {
        #region Non O/R Mapping Properties

        public decimal InspectQty {get;set;}

        public bool IsBlank { get; set; }

        public Item Item { get; set; }

        public Item FinishGoods { get; set; }

        public string DefectClassification { get; set; }

        public string DefectFactor { get; set; }

        #endregion
    }
}