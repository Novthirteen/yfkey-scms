using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class HuOdd : HuOddBase
    {
        #region Non O/R Mapping Properties

        public decimal CurrentCreateQty { get; set; }

        #endregion
    }
}