using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportationActBillBase : EntityBase
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
		private string _orderNo;
		public string OrderNo
		{
			get
			{
				return _orderNo;
			}
			set
			{
				_orderNo = value;
			}
		}
		private string _receiptNo;
		public string ReceiptNo
		{
			get
			{
				return _receiptNo;
			}
			set
			{
				_receiptNo = value;
			}
		}
		private string _expenseNo;
		public string ExpenseNo
		{
			get
			{
				return _expenseNo;
			}
			set
			{
				_expenseNo = value;
			}
		}
		private string _externalReceiptNo;
		public string ExternalReceiptNo
		{
			get
			{
				return _externalReceiptNo;
			}
			set
			{
				_externalReceiptNo = value;
			}
		}
		private com.Sconit.Entity.MasterData.BillAddress _billAddress;
		public com.Sconit.Entity.MasterData.BillAddress BillAddress
		{
			get
			{
				return _billAddress;
			}
			set
			{
				_billAddress = value;
			}
		}
		private Decimal _billQty;
		public Decimal BillQty
		{
			get
			{
				return _billQty;
			}
			set
			{
				_billQty = value;
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
		private Decimal _billAmount;
		public Decimal BillAmount
		{
			get
			{
				return _billAmount;
			}
			set
			{
				_billAmount = value;
			}
		}
		private Decimal _billedAmount;
		public Decimal BilledAmount
		{
			get
			{
				return _billedAmount;
			}
			set
			{
				_billedAmount = value;
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
		private com.Sconit.Entity.Transportation.TransportPriceList _priceList;
		public com.Sconit.Entity.Transportation.TransportPriceList PriceList
		{
			get
			{
				return _priceList;
			}
			set
			{
				_priceList = value;
			}
		}
		private com.Sconit.Entity.Transportation.TransportPriceListDetail _priceListDetail;
		public com.Sconit.Entity.Transportation.TransportPriceListDetail PriceListDetail
		{
			get
			{
				return _priceListDetail;
			}
			set
			{
				_priceListDetail = value;
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
		private Boolean _isProvisionalEstimate;
		public Boolean IsProvisionalEstimate
		{
			get
			{
				return _isProvisionalEstimate;
			}
			set
			{
				_isProvisionalEstimate = value;
			}
		}
		private DateTime _createDate;
		public DateTime CreateDate
		{
			get
			{
				return _createDate;
			}
			set
			{
				_createDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _createUser;
		public com.Sconit.Entity.MasterData.User CreateUser
		{
			get
			{
				return _createUser;
			}
			set
			{
				_createUser = value;
			}
		}
		private DateTime _lastModifyDate;
		public DateTime LastModifyDate
		{
			get
			{
				return _lastModifyDate;
			}
			set
			{
				_lastModifyDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _lastModifyUser;
		public com.Sconit.Entity.MasterData.User LastModifyUser
		{
			get
			{
				return _lastModifyUser;
			}
			set
			{
				_lastModifyUser = value;
			}
		}
		private string _status;
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}
        private DateTime _effectiveDate;
		public DateTime EffectiveDate
		{
			get
			{
                return _effectiveDate;
			}
			set
			{
                _effectiveDate = value;
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
		private com.Sconit.Entity.MasterData.Uom _uom;
		public com.Sconit.Entity.MasterData.Uom Uom
		{
			get
			{
				return _uom;
			}
			set
			{
				_uom = value;
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

        private com.Sconit.Entity.MasterData.Item _item;
        public com.Sconit.Entity.MasterData.Item Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }

        private string _pricingMethod;
        public string PricingMethod
        {
            get
            {
                return _pricingMethod;
            }
            set
            {
                _pricingMethod = value;
            }
        }

        private com.Sconit.Entity.Transportation.TransportationAddress _shipFrom;
        public com.Sconit.Entity.Transportation.TransportationAddress ShipFrom
        {
            get
            {
                return _shipFrom;
            }
            set
            {
                _shipFrom = value;
            }
        }
        private com.Sconit.Entity.Transportation.TransportationAddress _shipTo;
        public com.Sconit.Entity.Transportation.TransportationAddress ShipTo
        {
            get
            {
                return _shipTo;
            }
            set
            {
                _shipTo = value;
            }
        }
        private string _vehicleType;
        public string VehicleType
        {
            get
            {
                return _vehicleType;
            }
            set
            {
                _vehicleType = value;
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
            TransportationActBillBase another = obj as TransportationActBillBase;

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
