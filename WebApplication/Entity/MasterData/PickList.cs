using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class PickList : PickListBase
    {
        #region Non O/R Mapping Properties

        public void AddPickListDetail(PickListDetail pickListDetail)
        {
            if (this.PickListDetails == null)
            {
                this.PickListDetails = new List<PickListDetail>();
            }

            this.PickListDetails.Add(pickListDetail);
        }

        public void RemovePickListDetail(PickListDetail pickListDetail)
        {
            if (this.PickListDetails != null)
            {
                this.PickListDetails.Remove(pickListDetail);
            }
        }

        #endregion
    }
}