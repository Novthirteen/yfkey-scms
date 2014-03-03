using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class PickListDetail : PickListDetailBase
    {
        #region Non O/R Mapping Properties

        public void AddPickListResult(PickListResult pickListResult)
        {
            if (this.PickListResults == null)
            {
                this.PickListResults = new List<PickListResult>();
            }

            this.PickListResults.Add(pickListResult);
        }

        public void RemovePickListResult(PickListResult pickListResult)
        {
            if (this.PickListResults != null)
            {
                this.PickListResults.Remove(pickListResult);
            }
        }

        #endregion
    }
}