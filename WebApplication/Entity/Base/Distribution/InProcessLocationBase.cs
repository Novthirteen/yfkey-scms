using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Distribution
{
    [Serializable]
    public abstract class InProcessLocationBase : EntityBase
    {
        #region O/R Mapping Properties
		
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
        private string _orderType;
        public string OrderType
        {
            get
            {
                return _orderType;
            }
            set
            {
                _orderType = value;
            }
        }
        private int? _currentOperation;
        public int? CurrentOperation
		{
			get
			{
                return _currentOperation;
			}
			set
			{
                _currentOperation = value;
			}
		}
        private string _currentActivity;
        public string CurrentActivity
        {
            get
            {
                return _currentActivity;
            }
            set
            {
                _currentActivity = value;
            }
        }
        private com.Sconit.Entity.MasterData.Party _partyFrom;
        public com.Sconit.Entity.MasterData.Party PartyFrom
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
        private com.Sconit.Entity.MasterData.Party _partyTo;
        public com.Sconit.Entity.MasterData.Party PartyTo
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
		private com.Sconit.Entity.MasterData.ShipAddress _shipFrom;
		public com.Sconit.Entity.MasterData.ShipAddress ShipFrom
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
		private com.Sconit.Entity.MasterData.ShipAddress _shipTo;
		public com.Sconit.Entity.MasterData.ShipAddress ShipTo
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
        
        private Int32 _printCount;
        public Int32 PrintCount
        {
            get
            {
                return _printCount;
            }
            set
            {
                _printCount = value;
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
        private com.Sconit.Entity.MasterData.Flow _flow;
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
        private IList<InProcessLocationDetail> _inProcessLocationDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<InProcessLocationDetail> InProcessLocationDetails
        {
            get
            {
                return _inProcessLocationDetails;
            }
            set
            {
                _inProcessLocationDetails = value;
            }
        }
        private IList<InProcessLocationTrack> _inProcessLocationTracks;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<InProcessLocationTrack> InProcessLocationTracks
        {
            get
            {
                return _inProcessLocationTracks;
            }
            set
            {
                _inProcessLocationTracks = value;
            }
        }
        public com.Sconit.Entity.MasterData.Receipt GapReceipt { get; set; }
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
        public Boolean IsReceiptScanHu { get; set; }
        public Boolean IsDetailContainHu { get; set; }
        public Boolean IsAutoReceive { get; set; }
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
        public String GoodsReceiptGapTo { get; set; }
        public String AsnTemplate { get; set; }
        public String ReceiptTemplate { get; set; }
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

        private string _disposition;
        public string Disposition
        {
            get
            {
                return _disposition;
            }
            set
            {
                _disposition = value;
            }
        }
        private bool _isPrinted;
        public bool IsPrinted
        {
            get
            {
                return _isPrinted;
            }
            set
            {
                _isPrinted = value;
            }
        }
        private bool _needPrintAsn;
        public bool NeedPrintAsn
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
        private bool _needPrintReceipt;
        public bool NeedPrintReceipt
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
        private bool _isAsnUniqueReceipt;
        public bool IsAsnUniqueReceipt
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
        private bool _isReferenced;
        public bool IsReferenced
        {
            get
            {
                return _isReferenced;
            }
            set
            {
                _isReferenced = value;
            }
        }
        public DateTime DepartTime { get; set; }
        public DateTime ArriveTime { get; set; }
        #endregion

		public override int GetHashCode()
        {
			if (IpNo != null)
            {
                return IpNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            InProcessLocationBase another = obj as InProcessLocationBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.IpNo == another.IpNo);
            }
        } 
    }
	
}
