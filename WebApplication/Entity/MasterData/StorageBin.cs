using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class StorageBin : StorageBinBase
    {
        #region Non O/R Mapping Properties

        public bool IsBlank { get; set; }

        #endregion
    }
}