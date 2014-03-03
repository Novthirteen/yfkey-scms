using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class LocationTransactionViewBase : EntityBase
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
		public string ExtOrderNo
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
		public string RefOrderNo
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
		private string _recNo;
		public string RecNo
		{
			get
			{
				return _recNo;
			}
			set
			{
				_recNo = value;
			}
		}
		private Int32? _billTransId;
		public Int32? BillTransId
		{
			get
			{
				return _billTransId;
			}
			set
			{
				_billTransId = value;
			}
		}
		private string _transType;
		public string TransType
		{
			get
			{
				return _transType;
			}
			set
			{
				_transType = value;
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
		public string ShipFromAddr
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
		public string ShipToAddr
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


        /// <summary>
        /// djin 2012-08-02 Ä¿±ê¿âÎ»
        /// </summary>
        private string _refLoc;
        public string RefLoc { get { return _refLoc; } set { _refLoc = value; } }


        private string _loc;
        public string Loc
		{
			get
			{
                return _loc;
			}
			set
			{
                _loc = value;
			}
		}
        private string _locname;
        public string LocName
        {
            get
            {
                return _locname;
            }
            set
            {
                _locname = value;
            }
        }
		private string _locIOReason;
		public string LocIOReason
		{
			get
			{
				return _locIOReason;
			}
			set
			{
				_locIOReason = value;
			}
		}
		private string _locIOReasonDescription;
		public string LocIOReasonDescription
		{
			get
			{
				return _locIOReasonDescription;
			}
			set
			{
				_locIOReasonDescription = value;
			}
		}
        private DateTime _effDate;
        public DateTime EffDate
		{
			get
			{
                return _effDate;
			}
			set
			{
                _effDate = value;
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
            LocationTransactionViewBase another = obj as LocationTransactionViewBase;

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
