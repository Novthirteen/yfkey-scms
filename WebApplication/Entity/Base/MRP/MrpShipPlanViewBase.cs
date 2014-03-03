using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class MrpShipPlanViewBase : EntityBase
    {
        #region O/R Mapping Properties
		
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
		private Decimal _uC;
        public Decimal UnitCount
		{
			get
			{
				return _uC;
			}
			set
			{
				_uC = value;
			}
		}
        private string _loc;
        public string Location
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
		private DateTime _effDate;
        public DateTime EffectiveDate
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
        public string ItemDescription { get; set; }
        public string ItemReference { get; set; }
        
        #endregion

		public override int GetHashCode()
        {
            if (Flow != null && Item != null && Uom != null && UnitCount != null && StartTime != null && WindowTime != null && EffectiveDate != null)
            {
                return Flow.GetHashCode() ^ Item.GetHashCode() ^ Uom.GetHashCode() ^ UnitCount.GetHashCode() ^ StartTime.GetHashCode() ^ WindowTime.GetHashCode() ^ EffectiveDate.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            MrpShipPlanViewBase another = obj as MrpShipPlanViewBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Flow == another.Flow) && (this.Item == another.Item) && (this.Uom == another.Uom) && (this.UnitCount == another.UnitCount) && (this.StartTime == another.StartTime) && (this.WindowTime == another.WindowTime) && (this.EffectiveDate == another.EffectiveDate);
            }
        } 
    }
	
}
