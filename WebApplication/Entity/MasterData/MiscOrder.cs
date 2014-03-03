using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class MiscOrder : MiscOrderBase
    {
        #region Non O/R Mapping Properties

        public void AddMiscOrderDetail(MiscOrderDetail miscOrderDetail)
        {
            if (this.MiscOrderDetails == null)
            {
                this.MiscOrderDetails = new List<MiscOrderDetail>();
            }
            this.MiscOrderDetails.Add(miscOrderDetail);
        }

        #endregion
    }
}