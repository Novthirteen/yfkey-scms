using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ActingBillBase : EntityBase
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
        //private com.Sconit.Entity.MasterData.PlannedBill _plannedBill;
        //public com.Sconit.Entity.MasterData.PlannedBill PlannedBill
        //{
        //    get
        //    {
        //        return _plannedBill;
        //    }
        //    set
        //    {
        //        _plannedBill = value;
        //    }
        //}
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
		private com.Sconit.Entity.MasterData.BillAddress _billAddr;
        public com.Sconit.Entity.MasterData.BillAddress BillAddress
		{
			get
			{
				return _billAddr;
			}
			set
			{
				_billAddr = value;
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
        //private Decimal _remainQty;
        //public Decimal RemainQty
        //{
        //    get
        //    {
        //        return _remainQty;
        //    }
        //    set
        //    {
        //        _remainQty = value;
        //    }
        //}
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
        private String _status;
        public String Status
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
            ActingBillBase another = obj as ActingBillBase;

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
