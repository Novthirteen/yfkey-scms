using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ShiftDetailBase : EntityBase
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
		private string _shiftTime;
		public string ShiftTime
		{
			get
			{
				return _shiftTime;
			}
			set
			{
				_shiftTime = value;
			}
		}
		private DateTime? _startDate;
		public DateTime? StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
			}
		}
		private DateTime? _endDate;
		public DateTime? EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				_endDate = value;
			}
		}

        private string _daysAdd;
        public string DaysAdd
        {
            get
            {
                return _daysAdd;
            }
            set
            {
                _daysAdd = value;
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
            ShiftDetailBase another = obj as ShiftDetailBase;

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
