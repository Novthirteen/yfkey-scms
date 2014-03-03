using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public abstract class DssExportHistoryDetailBase : EntityBase
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
		private Int32 _mstrId;
		public Int32 MstrId
		{
			get
			{
				return _mstrId;
			}
			set
			{
				_mstrId = value;
			}
        }
        private string _keyCode;
        public string KeyCode
        {
            get
            {
                return _keyCode;
            }
            set
            {
                _keyCode = value;
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
		private string _refLocation;
		public string RefLocation
		{
			get
			{
				return _refLocation;
			}
			set
			{
				_refLocation = value;
			}
		}
		private DateTime? _effDate;
		public DateTime? EffDate
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
		private string _comments;
		public string Comments
		{
			get
			{
				return _comments;
			}
			set
			{
				_comments = value;
			}
		}
		private string _defStr1;
		public string DefStr1
		{
			get
			{
				return _defStr1;
			}
			set
			{
				_defStr1 = value;
			}
		}
		private string _defStr2;
		public string DefStr2
		{
			get
			{
				return _defStr2;
			}
			set
			{
				_defStr2 = value;
			}
		}
		private string _defStr3;
		public string DefStr3
		{
			get
			{
				return _defStr3;
			}
			set
			{
				_defStr3 = value;
			}
		}
		private string _defStr4;
		public string DefStr4
		{
			get
			{
				return _defStr4;
			}
			set
			{
				_defStr4 = value;
			}
		}
		private string _defStr5;
		public string DefStr5
		{
			get
			{
				return _defStr5;
			}
			set
			{
				_defStr5 = value;
			}
		}
		private string _undefStr1;
		public string UndefStr1
		{
			get
			{
				return _undefStr1;
			}
			set
			{
				_undefStr1 = value;
			}
		}
		private string _undefStr2;
		public string UndefStr2
		{
			get
			{
				return _undefStr2;
			}
			set
			{
				_undefStr2 = value;
			}
		}
		private string _undefStr3;
		public string UndefStr3
		{
			get
			{
				return _undefStr3;
			}
			set
			{
				_undefStr3 = value;
			}
		}
		private string _undefStr4;
		public string UndefStr4
		{
			get
			{
				return _undefStr4;
			}
			set
			{
				_undefStr4 = value;
			}
		}
		private string _undefStr5;
		public string UndefStr5
		{
			get
			{
				return _undefStr5;
			}
			set
			{
				_undefStr5 = value;
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
            DssExportHistoryDetailBase another = obj as DssExportHistoryDetailBase;

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
