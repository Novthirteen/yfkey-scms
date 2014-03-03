using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class LocationTransactionBase : EntityBase
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
		private string _extOrderNo;
        public string ExternalOrderNo
		{
			get
			{
				return _extOrderNo;
			}
			set
			{
				_extOrderNo = value;
			}
		}
		private string _refOrderNo;
        public string ReferenceOrderNo
		{
			get
			{
				return _refOrderNo;
			}
			set
			{
				_refOrderNo = value;
			}
		}
		private string _ipNo;
		public string IpNo
		{
			get
			{
				return _ipNo;
			}
			set
			{
				_ipNo = value;
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
		private string _partyFrom;
		public string PartyFrom
		{
			get
			{
				return _partyFrom;
			}
			set
			{
				_partyFrom = value;
			}
		}
		private string _partyFromName;
		public string PartyFromName
		{
			get
			{
				return _partyFromName;
			}
			set
			{
				_partyFromName = value;
			}
		}
		private string _partyTo;
		public string PartyTo
		{
			get
			{
				return _partyTo;
			}
			set
			{
				_partyTo = value;
			}
		}
		private string _partyToName;
		public string PartyToName
		{
			get
			{
				return _partyToName;
			}
			set
			{
				_partyToName = value;
			}
		}
		private string _shipFrom;
		public string ShipFrom
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
		private string _shipFromAddr;
        public string ShipFromAddress
		{
			get
			{
				return _shipFromAddr;
			}
			set
			{
				_shipFromAddr = value;
			}
		}
		private string _shipTo;
		public string ShipTo
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
		private string _shipToAddr;
        public string ShipToAddress
		{
			get
			{
				return _shipToAddr;
			}
			set
			{
				_shipToAddr = value;
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
        private string _reflocation;
        public string RefLocation
        {
            get
            {
                return _reflocation;
            }
            set
            {
                _reflocation = value;
            }
        }
        private string _reflocationName;
        public string RefLocationName
        {
            get
            {
                return _reflocationName;
            }
            set
            {
                _reflocationName = value;
            }
        }
        private string _storageArea;
        public string StorageArea
        {
            get
            {
                return _storageArea;
            }
            set
            {
                _storageArea = value;
            }
        }
        private string _storageAreaDescription;
        public string StorageAreaDescription
        {
            get
            {
                return _storageAreaDescription;
            }
            set
            {
                _storageAreaDescription = value;
            }
        }
        private string _storageBin;
        public string StorageBin
        {
            get
            {
                return _storageBin;
            }
            set
            {
                _storageBin = value;
            }
        }
        private string _storageBinDescription;
        public string StorageBinDescription
        {
            get
            {
                return _storageBinDescription;
            }
            set
            {
                _storageBinDescription = value;
            }
        }
        private string _locInOutReason;
        public string LocInOutReason
        {
            get
            {
                return _locInOutReason;
            }
            set
            {
                _locInOutReason = value;
            }
        }
        private string _locInOutReasonDescription;
        public string LocInOutReasonDescription
        {
            get
            {
                return _locInOutReasonDescription;
            }
            set
            {
                _locInOutReasonDescription = value;
            }
        }
		private string _dockDescription;
		public string DockDescription
		{
			get
			{
				return _dockDescription;
			}
			set
			{
				_dockDescription = value;
			}
		}
		private string _carrier;
		public string Carrier
		{
			get
			{
				return _carrier;
			}
			set
			{
				_carrier = value;
			}
		}
        private string _carrierBill;
        public string CarrierBillCode
        {
            get
            {
                return _carrierBill;
            }
            set
            {
                _carrierBill = value;
            }
        }
		private string _carrierBillAddr;
        public string CarrierBillAddress
		{
			get
			{
				return _carrierBillAddr;
			}
			set
			{
				_carrierBillAddr = value;
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
        private Int32 _orderDetailId;
        public Int32 OrderDetailId
        {
            get
            {
                return _orderDetailId;
            }
            set
            {
                _orderDetailId = value;
            }
        }
        private Int32 _orderLocationTransactionId;
        public Int32 OrderLocationTransactionId
        {
            get
            {
                return _orderLocationTransactionId;
            }
            set
            {
                _orderLocationTransactionId = value;
            }
        }
        private bool _isSubcontract;
        public bool IsSubcontract
        {
            get
            {
                return _isSubcontract;
            }
            set
            {
                _isSubcontract = value;
            }
        }
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
            LocationTransactionBase another = obj as LocationTransactionBase;

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
