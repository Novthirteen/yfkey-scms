using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class Hu : HuBase
    {
        #region Non O/R Mapping Properties

        private decimal _currentShipQty;
        public decimal CurrentShipQty
        {
            get
            {
                return this._currentShipQty;
            }
            set
            {
                this._currentShipQty = value;
            }
        }

        private decimal _currentReceiveQty;
        public decimal CurrentReceiveQty
        {
            get
            {
                return this._currentReceiveQty;
            }
            set
            {
                this._currentReceiveQty = value;
            }
        }

        #endregion
    }
}