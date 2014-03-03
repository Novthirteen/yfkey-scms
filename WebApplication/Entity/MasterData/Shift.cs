using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class Shift : ShiftBase
    {
        #region Non O/R Mapping Properties

        public string ShiftTime { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string DaysAdd { get; set; }
        #endregion
    }
}