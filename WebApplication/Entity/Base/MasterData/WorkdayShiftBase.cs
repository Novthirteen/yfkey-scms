using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class WorkdayShiftBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Workday _workday;
		public com.Sconit.Entity.MasterData.Workday Workday
		{
			get
			{
				return _workday;
			}
			set
			{
				_workday = value;
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
            WorkdayShiftBase another = obj as WorkdayShiftBase;

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
