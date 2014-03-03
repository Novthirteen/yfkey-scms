using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class MrpShipPlanBase : EntityBase
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
        private string _flowType;
        public string FlowType
		{
			get
			{
                return _flowType;
			}
			set
			{
                _flowType = value;
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
        private decimal _qty;
        public decimal Qty
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
        public string Bom { get; set; }
        public string Routing { get; set; }
        public string Uom { get; set; }
        public string BaseUom { get; set; }
        public decimal UnitCount { get; set; }
        public decimal UnitQty { get; set; }
        public decimal SourceUnitQty { get; set; }
        public string ItemDescription { get; set; }
        public string ItemReference { get; set; }
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
            MrpShipPlanBase another = obj as MrpShipPlanBase;

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
