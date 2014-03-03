using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class PickListBase : EntityBase
    {
        #region O/R Mapping Properties

        private string _pickListNo;
        public string PickListNo
        {
            get
            {
                return _pickListNo;
            }
            set
            {
                _pickListNo = value;
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
        private string _pickBy;
        public string PickBy
        {
            get
            {
                return _pickBy;
            }
            set
            {
                _pickBy = value;
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

        private IList<PickListDetail> _pickListDetails;
        public IList<PickListDetail> PickListDetails
        {
            get
            {
                return _pickListDetails;
            }
            set
            {
                _pickListDetails = value;
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
        public Boolean IsReceiptScanHu { get; set; }
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
        private com.Sconit.Entity.MasterData.User _startUser;
        public com.Sconit.Entity.MasterData.User StartUser
        {
            get
            {
                return _startUser;
            }
            set
            {
                _startUser = value;
            }
        }
        public Boolean IsPrinted { get; set; }
        public DateTime WindowTime { get; set; }
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
        #endregion

        public override int GetHashCode()
        {
            if (PickListNo != null)
            {
                return PickListNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            PickListBase another = obj as PickListBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.PickListNo == another.PickListNo);
            }
        }
    }

}
