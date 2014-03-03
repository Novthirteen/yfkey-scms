using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class BillDetailBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Bill _bill;
		public com.Sconit.Entity.MasterData.Bill Bill
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
        private com.Sconit.Entity.MasterData.ActingBill _actingBill;
        public com.Sconit.Entity.MasterData.ActingBill ActingBill
		{
			get
			{
                return _actingBill;
			}
			set
			{
                _actingBill = value;
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
        private Decimal _orderAmount;
        public Decimal OrderAmount
		{
			get
			{
                return _orderAmount;
			}
			set
			{
                _orderAmount = value;
			}
		}
        private Decimal? _discount;
        public Decimal? Discount
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

        public String LocationFrom { get; set; }
        public String IpNo { get; set; }
        public String ReferenceItemCode { get; set; }
        #endregion

		public override int GetHashCode()
        {
			if (Id != 0)
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
            BillDetailBase another = obj as BillDetailBase;

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
