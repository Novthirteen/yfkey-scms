using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class MrpReceivePlanBase : EntityBase
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
		private string _itemReference;
		public string ItemReference
		{
			get
			{
				return _itemReference;
			}
			set
			{
				_itemReference = value;
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
		private DateTime _receiveTime;
		public DateTime ReceiveTime
		{
			get
			{
				return _receiveTime;
			}
			set
			{
				_receiveTime = value;
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
		private string _sourceType;
		public string SourceType
		{
			get
			{
				return _sourceType;
			}
			set
			{
				_sourceType = value;
			}
		}
		private string _sourceDateType;
		public string SourceDateType
		{
			get
			{
				return _sourceDateType;
			}
			set
			{
				_sourceDateType = value;
			}
		}
		private string _sourceId;
		public string SourceId
		{
			get
			{
				return _sourceId;
			}
			set
			{
				_sourceId = value;
			}
		}
		private Boolean _isExpire;
		public Boolean IsExpire
		{
			get
			{
				return _isExpire;
			}
			set
			{
				_isExpire = value;
			}
		}
		private DateTime? _expireStartTime;
		public DateTime? ExpireStartTime
		{
			get
			{
				return _expireStartTime;
			}
			set
			{
				_expireStartTime = value;
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
        public decimal SourceUnitQty { get; set; }
        public string SourceItemCode { get; set; }
        public string SourceItemDescription { get; set; }
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
            MrpReceivePlanBase another = obj as MrpReceivePlanBase;

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
