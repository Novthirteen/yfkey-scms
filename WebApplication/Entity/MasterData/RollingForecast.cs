using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class RollingForecast : RollingForecastBase
    {
        #region Non O/R Mapping Properties

        private decimal[] _dateQty;
        public decimal[] DateQty
        {
            get
            {
                return _dateQty;
            }
            set
            {
                _dateQty = value;
            }
        }
        #endregion
    }
}