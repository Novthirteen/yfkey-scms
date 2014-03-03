using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class BillTransactionBase : EntityBase
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
		private string _item;
		public string Item
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
		private string _itemDescription;
		public string ItemDescription
		{
			get
			{
				return _itemDescription;
			}
			set
			{
				_itemDescription = value;
			}
		}
		private string _uom;
		public string Uom
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
		private string _billAddress;
		public string BillAddress
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
		private string _billAddressDescription;
		public string BillAddressDescription
		{
			get
			{
				return _billAddressDescription;
			}
			set
			{
				_billAddressDescription = value;
			}
		}
		private string _location;
		public string Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
			}
		}
		private string _locationName;
		public string LocationName
		{
			get
			{
				return _locationName;
			}
			set
			{
				_locationName = value;
			}
		}
		private string _huId;
		public string HuId
		{
			get
			{
				return _huId;
			}
			set
			{
				_huId = value;
			}
		}
		private string _lotNo;
		public string LotNo
		{
			get
			{
				return _lotNo;
			}
			set
			{
				_lotNo = value;
			}
		}
		private Int32 _batchNo;
        public Int32 BatchNo
		{
			get
			{
				return _batchNo;
			}
			set
			{
				_batchNo = value;
			}
		}
		private Decimal _qty;
		public Decimal Qty
		{
			get
			{
				return _qty;
			}
			set
			{
				_qty = value;
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
        private Int32 _plannedBill;
        public Int32 PlannedBill
        {
            get
            {
                return _plannedBill;
            }
            set
            {
                _plannedBill = value;
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
		private string _createUser;
		public string CreateUser
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
        private Int32 _actingBill;
        public Int32 ActingBill
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
       
        public String Party { get; set; }
        public String PartyName { get; set; }
        public String LocationFrom { get; set; }
        public String IpNo { get; set; }
        public String ReferenceItemCode { get; set; }
        public Int32 BillDetail { get; set; }
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
            BillTransactionBase another = obj as BillTransactionBase;

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
