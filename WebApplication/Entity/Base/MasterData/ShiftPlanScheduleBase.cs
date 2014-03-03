using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ShiftPlanScheduleBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Shift _shift;
		public com.Sconit.Entity.MasterData.Shift Shift
		{
			get
			{
				return _shift;
			}
			set
			{
				_shift = value;
			}
		}
        private Int32 _sequence;
        public Int32 Sequence
		{
			get
			{
                return _sequence;
			}
			set
			{
                _sequence = value;
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
		private DateTime _lastModifyDate;
		public DateTime LastModifyDate
		{
			get
			{
				return _lastModifyDate;
			}
			set
			{
				_lastModifyDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _lastModifyUser;
		public com.Sconit.Entity.MasterData.User LastModifyUser
		{
			get
			{
				return _lastModifyUser;
			}
			set
			{
				_lastModifyUser = value;
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
            ShiftPlanScheduleBase another = obj as ShiftPlanScheduleBase;

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
