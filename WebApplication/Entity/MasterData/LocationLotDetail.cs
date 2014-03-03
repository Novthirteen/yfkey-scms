using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class LocationLotDetail : LocationLotDetailBase
    {
        #region Non O/R Mapping Properties

        private com.Sconit.Entity.MasterData.StorageBin _newStorageBin;
        public com.Sconit.Entity.MasterData.StorageBin NewStorageBin
        {
            get
            {
                return _newStorageBin;
            }
            set
            {
                _newStorageBin = value;
            }
        }

        public decimal CurrentInspectQty { get; set; }

        public Location InspectQualifyLocation { get; set; }

        #endregion
    }
}