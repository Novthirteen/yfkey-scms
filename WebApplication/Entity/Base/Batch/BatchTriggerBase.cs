using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Batch
{
    [Serializable]
    public abstract class BatchTriggerBase : EntityBase
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
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}
		private com.Sconit.Entity.Batch.BatchJobDetail _batchJobDetail;
		public com.Sconit.Entity.Batch.BatchJobDetail BatchJobDetail
		{
			get
			{
				return _batchJobDetail;
			}
			set
			{
				_batchJobDetail = value;
			}
		}
		private DateTime? _nextFireTime;
		public DateTime? NextFireTime
		{
			get
			{
				return _nextFireTime;
			}
			set
			{
				_nextFireTime = value;
			}
		}
		private DateTime? _previousFireTime;
		public DateTime? PreviousFireTime
		{
			get
			{
				return _previousFireTime;
			}
			set
			{
				_previousFireTime = value;
			}
		}
		private Int32 _repeatCount;
		public Int32 RepeatCount
		{
			get
			{
				return _repeatCount;
			}
			set
			{
				_repeatCount = value;
			}
		}
		private Int32 _interval;
		public Int32 Interval
		{
			get
			{
				return _interval;
			}
			set
			{
				_interval = value;
			}
		}
		private string _intervalType;
		public string IntervalType
		{
			get
			{
				return _intervalType;
			}
			set
			{
				_intervalType = value;
			}
		}
		private Int64 _timesTriggered;
		public Int64 TimesTriggered
		{
			get
			{
				return _timesTriggered;
			}
			set
			{
				_timesTriggered = value;
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
            BatchTriggerBase another = obj as BatchTriggerBase;

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
