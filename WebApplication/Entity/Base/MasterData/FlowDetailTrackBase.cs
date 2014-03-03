using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class FlowDetailTrackBase : EntityBase
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

        private Int32 _flowDetailId;
        public Int32 FlowDetailId
        {
            get
            {
                return _flowDetailId;
            }
            set
            {
                _flowDetailId = value;
            }
        }

        private string _flow;
        public string Flow
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
        private string _customer;
        public string Customer
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
        private string _bom;
        public string Bom
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
        private string _locationFrom;
        public string LocationFrom
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
        private string _locationTo;
        public string LocationTo
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
        private string _billFrom;
        public string BillFrom
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
        private string _billTo;
        public string BillTo
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
        private string _priceListFrom;
        public string PriceListFrom
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
        private string _priceListTo;
        public string PriceListTo
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
        private string _lastModifyUser;
        public string LastModifyUser
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
        private string _eventCode;
        public string EventCode
        {
            get
            {
                return _eventCode;
            }
            set
            {
                _eventCode = value;
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
            FlowDetailTrackBase another = obj as FlowDetailTrackBase;

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
