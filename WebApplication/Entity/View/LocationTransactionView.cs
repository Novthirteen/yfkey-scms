using System;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public class LocationTransactionView : LocationTransactionViewBase
    {
        #region Non O/R Mapping Properties

        public Region Region { get; set; }

        #endregion
    }
}