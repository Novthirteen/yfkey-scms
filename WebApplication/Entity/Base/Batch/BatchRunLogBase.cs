using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Batch
{
    [Serializable]
    public abstract class BatchRunLogBase : EntityBase
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
		private com.Sconit.Entity.Batch.BatchTrigger _batchTrigger;
		public com.Sconit.Entity.Batch.BatchTrigger BatchTrigger
		{
			get
			{
				return _batchTrigger;
			}
			set
			{
				_batchTrigger = value;
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
		private DateTime? _endTime;
		public DateTime? EndTime
		{
			get
			{
				return _endTime;
			}
			set
			{
				_endTime = value;
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
		private string _message;
		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
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
            BatchRunLogBase another = obj as BatchRunLogBase;

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
