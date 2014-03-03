using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class CycleCountResult : CycleCountResultBase
    {
        #region Non O/R Mapping Properties

        public Location Location { get; set; } 
        public int Cartons { get; set; }
        public decimal ShortageQty { get; set; }
        public decimal ProfitQty { get; set; }
        public decimal EqualQty { get; set; }
        #endregion
    }
}