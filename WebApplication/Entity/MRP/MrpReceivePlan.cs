using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class MrpReceivePlan : MrpReceivePlanBase
    {
        #region Non O/R Mapping Properties
        public string RefFlows;
        public bool TryAddRefFlow(string refFlow)
        {
            if (RefFlows == null)
            {
                RefFlows = "->" + refFlow;
                return true;
            }
            else
            {
                if (RefFlows.IndexOf("->" + refFlow) > -1)
                {
                    return false;
                }
                else
                {
                    RefFlows += "->" + refFlow;
                    return true;
                }
            }
        }
        #endregion
    }
}