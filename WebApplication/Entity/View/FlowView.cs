using System;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public class FlowView : FlowViewBase
    {
        #region Non O/R Mapping Properties

        public LocationDetail LocationDetail { get; set; }

        #endregion
    }
}