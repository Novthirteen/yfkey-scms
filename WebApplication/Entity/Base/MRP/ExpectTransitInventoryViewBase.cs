using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class ExpectTransitInventoryViewBase : EntityBase
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
		private DateTime? _startTime;
		public DateTime? StartTime
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
		private DateTime? _windowTime;
		public DateTime? WindowTime
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
		private Decimal? _transitQty;
		public Decimal? TransitQty
		{
			get
			{
				return _transitQty;
			}
			set
			{
				_transitQty = value;
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
            ExpectTransitInventoryBase another = obj as ExpectTransitInventoryBase;

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
