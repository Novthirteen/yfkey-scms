using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportationBillDetailBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private Int32 _id;
		public Int32 Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}
        private com.Sconit.Entity.Transportation.TransportationBill _bill;
        public com.Sconit.Entity.Transportation.TransportationBill Bill
		{
			get
			{
				return _bill;
			}
			set
			{
				_bill = value;
			}
		}
        private com.Sconit.Entity.Transportation.TransportationActBill _ActBill;
        public com.Sconit.Entity.Transportation.TransportationActBill ActBill
		{
			get
			{
                return _ActBill;
			}
			set
			{
                _ActBill = value;
			}
		}
		private Decimal _billedQty;
		public Decimal BilledQty
		{
			get
			{
				return _billedQty;
			}
			set
			{
				_billedQty = value;
			}
		}
		private Decimal _unitPrice;
		public Decimal UnitPrice
		{
			get
			{
				return _unitPrice;
			}
			set
			{
				_unitPrice = value;
			}
		}
		private com.Sconit.Entity.MasterData.Currency _currency;
		public com.Sconit.Entity.MasterData.Currency Currency
		{
			get
			{
				return _currency;
			}
			set
			{
				_currency = value;
			}
		}
		private Decimal _discount;
		public Decimal Discount
		{
			get
			{
				return _discount;
			}
			set
			{
				_discount = value;
			}
		}
		private Boolean _isIncludeTax;
		public Boolean IsIncludeTax
		{
			get
			{
				return _isIncludeTax;
			}
			set
			{
				_isIncludeTax = value;
			}
		}
		private string _taxCode;
		public string TaxCode
		{
			get
			{
				return _taxCode;
			}
			set
			{
				_taxCode = value;
			}
		}
		private Decimal _amount;
		public Decimal Amount
		{
			get
			{
				return _amount;
			}
			set
			{
				_amount = value;
			}
        }
        private string _transType;
        public string TransType
        {
            get
            {
                return _transType;
            }
            set
            {
                _transType = value;
            }
        }
        
        #endregion

		public override int GetHashCode()
        {
			if (Id != null)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            TransportationBillDetailBase another = obj as TransportationBillDetailBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id);
            }
        } 
    }
	
}
