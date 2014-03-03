using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class PickListResult : PickListResultBase
    {
        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 
        public string LocationCode { get; set; }

        public string ItemCode { get; set; }

        public string ItemDescription { get; set; }

        public string UomCode { get; set; }

        public decimal UnitCount { get; set; }

        public string Status { get; set; }

        public string PickListNo { get; set; }

        public string HuId { get; set; }

        public string OrderNo { get; set; }

        public string StorageBinCode { get; set; }

        public string LotNo { get; set; }
        #endregion
    }
}