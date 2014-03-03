using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public class TransportationActBill : TransportationActBillBase
    {
        #region Non O/R Mapping Properties
        
        private decimal _currentBillQty;
        public decimal CurrentBillQty
        {
            get
            {
                return this._currentBillQty;
            }
            set
            {
                this._currentBillQty = value;
            }
        }

        private decimal _currentBillAmount;
        public decimal CurrentBillAmount
        {
            get
            {
                return this._currentBillAmount;
            }
            set
            {
                this._currentBillAmount = value;
            }
        }

        private decimal _currentDiscount;
        public decimal CurrentDiscount
        {
            get
            {
                return this._currentDiscount;
            }
            set
            {
                this._currentDiscount = value;
            }
        }

        #endregion
    }
}