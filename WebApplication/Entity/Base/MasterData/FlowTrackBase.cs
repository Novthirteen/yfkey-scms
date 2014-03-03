using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class FlowTrackBase : EntityBase
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
		private string _code;
		public string Code
		{
			get
			{
				return _code;
			}
			set
			{
				_code = value;
			}
		}
        private string _description;
		public string Description
		{
			get
			{
                return _description;
			}
			set
			{
                _description = value;
			}
		}
		private Boolean _isActive;
		public Boolean IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}
		private string _type;
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
        private string _referenceFlow;
        public string ReferenceFlow
		{
			get
			{
                return _referenceFlow;
			}
			set
			{
                _referenceFlow = value;
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
		private string _currency;
		public string Currency
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
		private string _routing;
		public string Routing
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
		private string _returnRouting;
		public string ReturnRouting
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
		private string _flowStrategy;
		public string FlowStrategy
		{
			get
			{
				return _flowStrategy;
			}
			set
			{
				_flowStrategy = value;
			}
		}
		private string _lotGroup;
		public string LotGroup
		{
			get
			{
				return _lotGroup;
			}
			set
			{
				_lotGroup = value;
			}
		}
		private Decimal? _leadTime;
		public Decimal? LeadTime
		{
			get
			{
				return _leadTime;
			}
			set
			{
				_leadTime = value;
			}
		}
		private Decimal? _emTime;
		public Decimal? EmTime
		{
			get
			{
				return _emTime;
			}
			set
			{
				_emTime = value;
			}
		}
		private Decimal? _maxCirTime;
		public Decimal? MaxCirTime
		{
			get
			{
				return _maxCirTime;
			}
			set
			{
				_maxCirTime = value;
			}
		}
		private string _winTime1;
		public string WinTime1
		{
			get
			{
				return _winTime1;
			}
			set
			{
				_winTime1 = value;
			}
		}
		private string _winTime2;
		public string WinTime2
		{
			get
			{
				return _winTime2;
			}
			set
			{
				_winTime2 = value;
			}
		}
		private string _winTime3;
		public string WinTime3
		{
			get
			{
				return _winTime3;
			}
			set
			{
				_winTime3 = value;
			}
		}
		private string _winTime4;
		public string WinTime4
		{
			get
			{
				return _winTime4;
			}
			set
			{
				_winTime4 = value;
			}
		}
		private string _winTime5;
		public string WinTime5
		{
			get
			{
				return _winTime5;
			}
			set
			{
				_winTime5 = value;
			}
		}
		private string _winTime6;
		public string WinTime6
		{
			get
			{
				return _winTime6;
			}
			set
			{
				_winTime6 = value;
			}
		}
		private string _winTime7;
		public string WinTime7
		{
			get
			{
				return _winTime7;
			}
			set
			{
				_winTime7 = value;
			}
		}
		private DateTime? _nextOrderTime;
		public DateTime? NextOrderTime
		{
			get
			{
				return _nextOrderTime;
			}
			set
			{
				_nextOrderTime = value;
			}
		}
		private DateTime? _nextWinTime;
		public DateTime? NextWinTime
		{
			get
			{
				return _nextWinTime;
			}
			set
			{
				_nextWinTime = value;
			}
		}
		private Int32? _weekInterval;
		public Int32? WeekInterval
		{
			get
			{
				return _weekInterval;
			}
			set
			{
				_weekInterval = value;
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
		private Boolean _isAutoRelease;
		public Boolean IsAutoRelease
		{
			get
			{
				return _isAutoRelease;
			}
			set
			{
				_isAutoRelease = value;
			}
		}
		private Boolean _isAutoStart;
		public Boolean IsAutoStart
		{
			get
			{
				return _isAutoStart;
			}
			set
			{
				_isAutoStart = value;
			}
		}
		private Boolean _isAutoShip;
		public Boolean IsAutoShip
		{
			get
			{
				return _isAutoShip;
			}
			set
			{
				_isAutoShip = value;
			}
		}
		private Boolean _isAutoReceive;
		public Boolean IsAutoReceive
		{
			get
			{
				return _isAutoReceive;
			}
			set
			{
				_isAutoReceive = value;
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
        private Boolean _isListDetail;
        public Boolean IsListDetail
		{
			get
			{
                return _isListDetail;
			}
			set
			{
                _isListDetail = value;
			}
		}
		private Boolean _isShowPrice;
		public Boolean IsShowPrice
		{
			get
			{
				return _isShowPrice;
			}
			set
			{
				_isShowPrice = value;
			}
		}
        private string _checkDetailOption;
        public string CheckDetailOption
		{
			get
			{
                return _checkDetailOption;
			}
			set
			{
                _checkDetailOption = value;
			}
		}
		private Decimal? _startLatency;
		public Decimal? StartLatency
		{
			get
			{
				return _startLatency;
			}
			set
			{
				_startLatency = value;
			}
		}
		private Decimal? _completeLatency;
		public Decimal? CompleteLatency
		{
			get
			{
				return _completeLatency;
			}
			set
			{
				_completeLatency = value;
			}
		}
		private Boolean _needPrintOrder;
		public Boolean NeedPrintOrder
		{
			get
			{
				return _needPrintOrder;
			}
			set
			{
				_needPrintOrder = value;
			}
		}
		private Boolean _needPrintAsn;
		public Boolean NeedPrintAsn
		{
			get
			{
				return _needPrintAsn;
			}
			set
			{
				_needPrintAsn = value;
			}
		}
        private Boolean _needPrintReceipt;
        public Boolean NeedPrintReceipt
		{
			get
			{
                return _needPrintReceipt;
			}
			set
			{
                _needPrintReceipt = value;
			}
		}
        private string _goodsReceiptGapTo;
        public string GoodsReceiptGapTo
		{
			get
			{
                return _goodsReceiptGapTo;
			}
			set
			{
                _goodsReceiptGapTo = value;
			}
		}
		private Boolean _allowExceed;
		public Boolean AllowExceed
		{
			get
			{
				return _allowExceed;
			}
			set
			{
				_allowExceed = value;
			}
		}
        private Boolean _fulfillUnitCount;
        public Boolean FulfillUnitCount
		{
			get
			{
                return _fulfillUnitCount;
			}
			set
			{
                _fulfillUnitCount = value;
			}
		}
        private string _receiptTemplate;
        public string ReceiptTemplate
		{
			get
			{
                return _receiptTemplate;
			}
			set
			{
                _receiptTemplate = value;
			}
		}
		private string _orderTemplate;
		public string OrderTemplate
		{
			get
			{
				return _orderTemplate;
			}
			set
			{
				_orderTemplate = value;
			}
		}
		private string _asnTemplate;
		public string AsnTemplate
		{
			get
			{
				return _asnTemplate;
			}
			set
			{
				_asnTemplate = value;
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
		private string _carrierBillAddress;
		public string CarrierBillAddress
		{
			get
			{
                return _carrierBillAddress;
			}
			set
			{
                _carrierBillAddress = value;
			}
		}
		private Boolean? _allowCreateDetail;
		public Boolean? AllowCreateDetail
		{
			get
			{
				return _allowCreateDetail;
			}
			set
			{
				_allowCreateDetail = value;
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
    

        private Boolean _isShipScanHu;
        public Boolean IsShipScanHu
		{
			get
			{
                return _isShipScanHu;
			}
			set
			{
                _isShipScanHu = value;
			}
		}
        private Boolean _isReceiptScanHu;
        public Boolean IsReceiptScanHu
		{
			get
			{
                return _isReceiptScanHu;
			}
			set
			{
                _isReceiptScanHu = value;
			}
		}
        private string _createHuOption;
        public string CreateHuOption
		{
			get
			{
                return _createHuOption;
			}
			set
			{
                _createHuOption = value;
			}
		}
		private Boolean _autoPrintHu;
		public Boolean AutoPrintHu
		{
			get
			{
				return _autoPrintHu;
			}
			set
			{
				_autoPrintHu = value;
			}
		}
		private Boolean _isOddCreateHu;
		public Boolean IsOddCreateHu
		{
			get
			{
				return _isOddCreateHu;
			}
			set
			{
				_isOddCreateHu = value;
			}
		}
        private Boolean _isAutoCreatePickList;
        public Boolean IsAutoCreatePickList
		{
			get
			{
                return _isAutoCreatePickList;
			}
			set
			{
                _isAutoCreatePickList = value;
			}
		}
        private Boolean _needInspection;
        public Boolean NeedInspection
		{
			get
			{
                return _needInspection;
			}
			set
			{
                _needInspection = value;
			}
		}
        private Boolean _isGoodsReceiveFIFO;
        public Boolean IsGoodsReceiveFIFO
		{
			get
			{
                return _isGoodsReceiveFIFO;
			}
			set
			{
                _isGoodsReceiveFIFO = value;
			}
		}
		private string _antiResolveHu;
        public string AntiResolveHu
		{
			get
			{
				return _antiResolveHu;
			}
			set
			{
				_antiResolveHu = value;
			}
		}
		private Int32? _maxOnlineQty;
		public Int32? MaxOnlineQty
		{
			get
			{
				return _maxOnlineQty;
			}
			set
			{
				_maxOnlineQty = value;
			}
		}
		private string _huTemplate;
		public string HuTemplate
		{
			get
			{
				return _huTemplate;
			}
			set
			{
				_huTemplate = value;
			}
		}
		private Boolean _allowRepeatlyExceed;
		public Boolean AllowRepeatlyExceed
		{
			get
			{
				return _allowRepeatlyExceed;
			}
			set
			{
				_allowRepeatlyExceed = value;
			}
		}
		private Boolean _isPickFromBin;
		public Boolean IsPickFromBin
		{
			get
			{
				return _isPickFromBin;
			}
			set
			{
				_isPickFromBin = value;
			}
		}
		private Boolean _isShipByOrder;
		public Boolean IsShipByOrder
		{
			get
			{
				return _isShipByOrder;
			}
			set
			{
				_isShipByOrder = value;
			}
		}
		private Boolean _isAsnUniqueReceipt;
		public Boolean IsAsnUniqueReceipt
		{
			get
			{
				return _isAsnUniqueReceipt;
			}
			set
			{
				_isAsnUniqueReceipt = value;
			}
		}
		private Int32? _version;
		public Int32? Version
		{
			get
			{
				return _version;
			}
			set
			{
				_version = value;
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
            FlowTrackBase another = obj as FlowTrackBase;

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
