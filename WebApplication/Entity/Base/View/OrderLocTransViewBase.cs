using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class OrderLocTransViewBase : EntityBase
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
		private DateTime _startTime;
		public DateTime StartTime
		{
			get
			{
				return _startTime;
			}
			set
			{
				_startTime = value;
			}
		}
		private DateTime _windowTime;
		public DateTime WindowTime
		{
			get
			{
				return _windowTime;
			}
			set
			{
				_windowTime = value;
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
		private string _itemCode;
		public string ItemCode
		{
			get
			{
				return _itemCode;
			}
			set
			{
				_itemCode = value;
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
		private Decimal _reqQty;
		public Decimal ReqQty
		{
			get
			{
				return _reqQty;
			}
			set
			{
				_reqQty = value;
			}
		}
		private Decimal _orderQty;
		public Decimal OrderQty
		{
			get
			{
				return _orderQty;
			}
			set
			{
				_orderQty = value;
			}
		}
		private Decimal _shipQty;
		public Decimal ShipQty
		{
			get
			{
				return _shipQty;
			}
			set
			{
				_shipQty = value;
			}
		}
		private Decimal _recQty;
		public Decimal RecQty
		{
			get
			{
				return _recQty;
			}
			set
			{
				_recQty = value;
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
		private string _iOType;
		public string IOType
		{
			get
			{
				return _iOType;
			}
			set
			{
				_iOType = value;
			}
		}
		private Decimal _unitQty;
		public Decimal UnitQty
		{
			get
			{
				return _unitQty;
			}
			set
			{
				_unitQty = value;
			}
		}
		private Decimal _planQty;
		public Decimal PlanQty
		{
			get
			{
				return _planQty;
			}
			set
			{
				_planQty = value;
			}
		}
		private Decimal _accumQty;
		public Decimal AccumQty
		{
			get
			{
				return _accumQty;
			}
			set
			{
				_accumQty = value;
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
            OrderLocTransViewBase another = obj as OrderLocTransViewBase;

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
