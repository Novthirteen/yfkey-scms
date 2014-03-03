using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ReceiptBase : EntityBase
    {
        #region O/R Mapping Properties
		
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
        private User _createUser;
		public User CreateUser
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
        private IList<com.Sconit.Entity.MasterData.ReceiptDetail> _receiptDetails;
        public IList<com.Sconit.Entity.MasterData.ReceiptDetail> ReceiptDetails
        {
            get
            {
                return _receiptDetails;
            }
            set
            {
                _receiptDetails = value;
            }
        }
        private IList<com.Sconit.Entity.Distribution.InProcessLocation> _inProcessLocations;
        public IList<com.Sconit.Entity.Distribution.InProcessLocation> InProcessLocations
        {
            get
            {
                return _inProcessLocations;
            }
            set
            {
                _inProcessLocations = value;
            }
        }
        public String ReceiptTemplate { get; set; }
        public String ReferenceIpNo { get; set; }
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
        private bool _needPrint;
        public bool NeedPrint
        {
            get
            {
                return _needPrint;
            }
            set
            {
                _needPrint = value;
            }
        }
        #endregion

		public override int GetHashCode()
        {
			if (ReceiptNo != null)
            {
                return ReceiptNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ReceiptBase another = obj as ReceiptBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.ReceiptNo == another.ReceiptNo);
            }
        } 
    }
	
}
