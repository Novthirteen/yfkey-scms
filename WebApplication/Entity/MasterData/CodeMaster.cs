using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class CodeMaster : CodeMasterBase
    {
        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 

        #endregion

        public enum TimeUnit
        {
            NA = 0,
            Second = 1,
            Minute = 2,
            Hour = 3,
            Day = 4,
            Week = 5,
            Month = 6,
            Quarter = 7,
            Year = 8
        }

        public enum PlanStatus
        {
            Create = 0,
            Release = 1,
        }
    }
}