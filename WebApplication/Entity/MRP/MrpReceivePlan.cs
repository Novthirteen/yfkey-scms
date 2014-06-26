using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class MrpReceivePlan : MrpReceivePlanBase
    {
        #region Non O/R Mapping Properties

        public IList<int> FlowDetailIdList { get; set; }

        public bool ContainFlowDetailId(int id)
        {
            if (FlowDetailIdList == null)
            {
                return false;
            }
            else
            {
                return FlowDetailIdList.Contains(id);
            }
        }

        public IList<string> RefLocs;
        public bool TryAddRefLoc(string refLoc)
        {
            if(RefLocs == null)
            {
                RefLocs = new List<string>();
            }

            if (RefLocs.Contains(refLoc))
            {
                return false;
            }
            else
            {
                RefLocs.Add(refLoc);
                return true;
            }
        }
        #endregion
    }
}