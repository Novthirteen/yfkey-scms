using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class PlannedBillBase : EntityBase
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
        //private com.Sconit.Entity.MasterData.OrderHead _orderHead;
        //public com.Sconit.Entity.MasterData.OrderHead OrderHead
        //{
        //    get
        //    {
        //        return _orderHead;
        //    }
        //    set
        //    {
        //        _orderHead = value;
        //    }
        //}
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
		private string _transactionType;
		public string TransactionType
		{
			get
			{
				return _transactionType;
			}
			set
			{
				_transactionType = value;
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
		private string _settleTerm;
		public string SettleTerm
		{
			get
			{
				return _settleTerm;
			}
			set
			{
				_settleTerm = value;
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
        private Decimal _unitCount;
        public Decimal UnitCount
        {
            get
            {
                return _unitCount;
            }
            set
            {
                _unitCount = value;
            }
        }
		private Decimal _plannedQty;
		public Decimal PlannedQty
		{
			get
			{
				return _plannedQty;
			}
			set
			{
				_plannedQty = value;
			}
		}
		private Decimal? _actingQty;
		public Decimal? ActingQty
		{
			get
			{
				return _actingQty;
			}
			set
			{
				_actingQty = value;
			}
		}
        private Decimal _unitQty;
        public Decimal UnitQty
        {
            get
            {
                return _unitQty;
            }
            set
            {
                _unitQty = value;
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
		private com.Sconit.Entity.MasterData.PriceList _priceList;
		public com.Sconit.Entity.MasterData.PriceList PriceList
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
        private Decimal _plannedAmount;
        public Decimal PlannedAmount
        {
            get
            {
                return _plannedAmount;
            }
            set
            {
                _plannedAmount = value;
            }
        }
        private Decimal? _actingAmount;
        public Decimal? ActingAmount
        {
            get
            {
                return _actingAmount;
            }
            set
            {
                _actingAmount = value;
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
        private Boolean _isAutoBill;
        public Boolean IsAutoBill
        {
            get
            {
                return _isAutoBill;
            }
            set
            {
                _isAutoBill = value;
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
        public String HuId { get; set; }
        public String LotNo { get; set; }
        public String LocationFrom { get; set; }
        public String IpNo { get; set; }
        public String ReferenceItemCode { get; set; }
        public String PartyFrom { get; set; }
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
            PlannedBillBase another = obj as PlannedBillBase;

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
