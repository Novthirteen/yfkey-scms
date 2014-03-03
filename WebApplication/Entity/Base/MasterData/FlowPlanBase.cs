using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class FlowPlanBase : EntityBase
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
		private com.Sconit.Entity.MasterData.FlowDetail _flowDetail;
		public com.Sconit.Entity.MasterData.FlowDetail FlowDetail
		{
			get
			{
				return _flowDetail;
			}
			set
			{
				_flowDetail = value;
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
            FlowPlanBase another = obj as FlowPlanBase;

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
