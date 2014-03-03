using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class RepackDetail : RepackDetailBase
    {
        #region Non O/R Mapping Properties

        public Hu Hu { get; set; }

        public string StorageBinCode { get; set; }

        public string itemCode { get; set; }

        #endregion
    }
}