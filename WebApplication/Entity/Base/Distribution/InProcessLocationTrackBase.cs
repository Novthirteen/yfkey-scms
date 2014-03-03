using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Distribution
{
    [Serializable]
    public abstract class InProcessLocationTrackBase : EntityBase
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
		private com.Sconit.Entity.Distribution.InProcessLocation _ipProcessLocation;
		public com.Sconit.Entity.Distribution.InProcessLocation IpProcessLocation
		{
			get
			{
				return _ipProcessLocation;
			}
			set
			{
				_ipProcessLocation = value;
			}
		}
		private Int32 _operation;
		public Int32 Operation
		{
			get
			{
				return _operation;
			}
			set
			{
				_operation = value;
			}
		}
		private string _activity;
		public string Activity
		{
			get
			{
				return _activity;
			}
			set
			{
				_activity = value;
			}
		}
		private com.Sconit.Entity.MasterData.WorkCenter _workCenter;
		public com.Sconit.Entity.MasterData.WorkCenter WorkCenter
		{
			get
			{
				return _workCenter;
			}
			set
			{
				_workCenter = value;
			}
		}
		private DateTime? _activeDate;
		public DateTime? ActiveDate
		{
			get
			{
				return _activeDate;
			}
			set
			{
				_activeDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _activeUser;
		public com.Sconit.Entity.MasterData.User ActiveUser
		{
			get
			{
				return _activeUser;
			}
			set
			{
				_activeUser = value;
			}
		}
		private string _remark;
		public string Remark
		{
			get
			{
				return _remark;
			}
			set
			{
				_remark = value;
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
            InProcessLocationTrackBase another = obj as InProcessLocationTrackBase;

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
