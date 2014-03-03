using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Distribution
{
    [Serializable]
    public abstract class CustomerRollingPlanDetailBase : EntityBase
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
		private com.Sconit.Entity.Distribution.CustomerRollingPlan _customerRollingPlan;
		public com.Sconit.Entity.Distribution.CustomerRollingPlan CustomerRollingPlan
		{
			get
			{
				return _customerRollingPlan;
			}
			set
			{
				_customerRollingPlan = value;
			}
		}
		private DateTime _scheduleDate;
		public DateTime ScheduleDate
		{
			get
			{
				return _scheduleDate;
			}
			set
			{
				_scheduleDate = value;
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
		private Decimal? _qty;
		public Decimal? Qty
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
            CustomerRollingPlanDetailBase another = obj as CustomerRollingPlanDetailBase;

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
