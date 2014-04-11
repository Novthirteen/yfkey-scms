using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class FlowDetailBase : EntityBase
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

		private com.Sconit.Entity.MasterData.Flow _flow;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
		public com.Sconit.Entity.MasterData.Flow Flow
		{
			get
			{
				return _flow;
			}
			set
			{
				_flow = value;
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
        private com.Sconit.Entity.MasterData.Customer _customer;
        public com.Sconit.Entity.MasterData.Customer Customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
            }
        }
        private string _referenceItemCode;
        public string ReferenceItemCode
        {
            get
            {
                return _referenceItemCode;
            }
            set
            {
                _referenceItemCode = value;
            }
        }
        private string _timeUnit;
        public string TimeUnit
        {
            get
            {
                return _timeUnit;
            }
            set
            {
                _timeUnit = value;
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
		private com.Sconit.Entity.MasterData.Bom _bom;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
		public com.Sconit.Entity.MasterData.Bom Bom
		{
			get
			{
				return _bom;
			}
			set
			{
				_bom = value;
			}
		}
		private Int32 _sequence;
		public Int32 Sequence
		{
			get
			{
				return _sequence;
			}
			set
			{
				_sequence = value;
			}
		}
		private com.Sconit.Entity.MasterData.Location _locationFrom;
		public com.Sconit.Entity.MasterData.Location LocationFrom
		{
			get
			{
				return _locationFrom;
			}
			set
			{
				_locationFrom = value;
			}
		}
		private com.Sconit.Entity.MasterData.Location _locationTo;
		public com.Sconit.Entity.MasterData.Location LocationTo
		{
			get
			{
				return _locationTo;
			}
			set
			{
				_locationTo = value;
			}
		}
		private com.Sconit.Entity.MasterData.BillAddress _billFrom;
		public com.Sconit.Entity.MasterData.BillAddress BillFrom
		{
			get
			{
				return _billFrom;
			}
			set
			{
				_billFrom = value;
			}
		}
		private com.Sconit.Entity.MasterData.BillAddress _billTo;
		public com.Sconit.Entity.MasterData.BillAddress BillTo
		{
			get
			{
				return _billTo;
			}
			set
			{
				_billTo = value;
			}
		}
		private Boolean _isAutoCreate;
		public Boolean IsAutoCreate
		{
			get
			{
				return _isAutoCreate;
			}
			set
			{
				_isAutoCreate = value;
			}
		}
		private Decimal? _safeStock;
		public Decimal? SafeStock
		{
			get
			{
				return _safeStock;
			}
			set
			{
				_safeStock = value;
			}
		}
		private Decimal? _maxStock;
		public Decimal? MaxStock
		{
			get
			{
				return _maxStock;
			}
			set
			{
				_maxStock = value;
			}
		}
		private Decimal? _minLotSize;
		public Decimal? MinLotSize
		{
			get
			{
				return _minLotSize;
			}
			set
			{
				_minLotSize = value;
			}
		}
		private Decimal? _orderLotSize;
		public Decimal? OrderLotSize
		{
			get
			{
				return _orderLotSize;
			}
			set
			{
				_orderLotSize = value;
			}
		}
		private Decimal? _goodsReceiptLotSize;
		public Decimal? GoodsReceiptLotSize
		{
			get
			{
                return _goodsReceiptLotSize;
			}
			set
			{
                _goodsReceiptLotSize = value;
			}
		}
        private Decimal? _batchSize;
        public Decimal? BatchSize
        {
            get
            {
                return _batchSize;
            }
            set
            {
                _batchSize = value;
            }
        }
		private string _roundUpOption;
		public string RoundUpOption
		{
			get
			{
				return _roundUpOption;
			}
			set
			{
				_roundUpOption = value;
			}
		}
        private string _billSettleTerm;
        public string BillSettleTerm
        {
            get
            {
                return _billSettleTerm;
            }
            set
            {
                _billSettleTerm = value;
            }
        }
		private Int32? _huLotSize;
		public Int32? HuLotSize
		{
			get
			{
				return _huLotSize;
			}
			set
			{
				_huLotSize = value;
			}
		}
		private Decimal? _packageVolumn;
		public Decimal? PackageVolumn
		{
			get
			{
				return _packageVolumn;
			}
			set
			{
				_packageVolumn = value;
			}
		}
		private string _packageType;
		public string PackageType
		{
			get
			{
				return _packageType;
			}
			set
			{
				_packageType = value;
			}
		}
		private string _projectDescription;
		public string ProjectDescription
		{
			get
			{
				return _projectDescription;
			}
			set
			{
				_projectDescription = value;
			}
		}
		private string _remark;
		public string Remark
		{
			get
			{
				return _remark;
			}
			set
			{
				_remark = value;
			}
		}
        private com.Sconit.Entity.Procurement.PurchasePriceList _priceListFrom;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public com.Sconit.Entity.Procurement.PurchasePriceList PriceListFrom
        {
            get
            {
                return _priceListFrom;
            }
            set
            {
                _priceListFrom = value;
            }
        }
        private com.Sconit.Entity.Distribution.SalesPriceList _priceListTo;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public com.Sconit.Entity.Distribution.SalesPriceList PriceListTo
        {
            get
            {
                return _priceListTo;
            }
            set
            {
                _priceListTo = value;
            }
        }
        public Boolean NeedInspection { get; set; }
        public String IdMark { get; set; }
        public String BarCodeType { get; set; }
        public String CustomerItemCode { get; set; }
        private DateTime? _startDate;
        public DateTime? StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                _startDate = value;
            }
        }
        private DateTime? _endDate;
        public DateTime? EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                _endDate = value;
            }
        }
        public String OddShipOption { get; set; }
        public Int32 Version { get; set; }
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

        private com.Sconit.Entity.MasterData.Routing _routing;
        public com.Sconit.Entity.MasterData.Routing Routing
        {
            get
            {
                return _routing;
            }
            set
            {
                _routing = value;
            }
        }
        private com.Sconit.Entity.MasterData.Routing _returnRouting;
        public com.Sconit.Entity.MasterData.Routing ReturnRouting
        {
            get
            {
                return _returnRouting;
            }
            set
            {
                _returnRouting = value;
            }
        }
        public Int32 MRPWeight { get; set; }

        #region    Ford EDI Option
        public string ReceivingPlant { get; set; }
        public string ShipFrom { get; set; }
        public string TransModeCode { get; set; }
        public string ConveyanceNumber { get; set; }
        public string CarrierCode { get; set; }
        public string GrossWeight { get; set; }
        public string NetWeight { get; set; }
        public string WeightUom { get; set; }
        public string PackagingCode { get; set; }
        public string LadingQuantity { get; set; }
        public string UnitsPerContainer { get; set; }

        #endregion
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
            FlowDetailBase another = obj as FlowDetailBase;

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
