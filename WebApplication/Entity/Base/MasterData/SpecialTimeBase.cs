using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class SpecialTimeBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private Int32 _iD;
		public Int32 ID
		{
			get
			{
				return _iD;
			}
			set
			{
				_iD = value;
			}
		}
		private com.Sconit.Entity.MasterData.Region _region;
		public com.Sconit.Entity.MasterData.Region Region
		{
			get
			{
				return _region;
			}
			set
			{
				_region = value;
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
		private DateTime _endTime;
		public DateTime EndTime
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
		private string _desc1;
        public string Description
		{
			get
			{
				return _desc1;
			}
			set
			{
				_desc1 = value;
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
        
        #endregion

		public override int GetHashCode()
        {
			if (ID != 0)
            {
                return ID.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            SpecialTimeBase another = obj as SpecialTimeBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.ID == another.ID);
            }
        } 
    }
	
}
