using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    [Serializable]
    public abstract class ItemFlowPlanDetailBase : EntityBase
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
		private com.Sconit.Entity.Procurement.ItemFlowPlan _itemFlowPlan;
		public com.Sconit.Entity.Procurement.ItemFlowPlan ItemFlowPlan
		{
			get
			{
				return _itemFlowPlan;
			}
			set
			{
				_itemFlowPlan = value;
			}
        }
        private string _timePeriodType;
        public string TimePeriodType
        {
            get
            {
                return _timePeriodType;
            }
            set
            {
                _timePeriodType = value;
            }
        }
        private DateTime _reqDate;
        public DateTime ReqDate
        {
            get
            {
                return _reqDate;
            }
            set
            {
                _reqDate = value;
            }
        }
        private Decimal _grossDemand;
        public Decimal GrossDemand
        {
            get
            {
                return _grossDemand;
            }
            set
            {
                _grossDemand = value;
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
        private IList<ItemFlowPlanTrack> _itemFlowPlanTracks;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<ItemFlowPlanTrack> ItemFlowPlanTracks
        {
            get
            {
                return _itemFlowPlanTracks;
            }
            set
            {
                _itemFlowPlanTracks = value;
            }
        }
        
        #endregion

		public override int GetHashCode()
        {
			if (Id != 0)
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
            ItemFlowPlanDetailBase another = obj as ItemFlowPlanDetailBase;

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
