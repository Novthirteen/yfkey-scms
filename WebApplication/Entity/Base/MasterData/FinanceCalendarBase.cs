using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class FinanceCalendarBase : EntityBase
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
		private Int32 _financeYear;
		public Int32 FinanceYear
		{
			get
			{
				return _financeYear;
			}
			set
			{
				_financeYear = value;
			}
		}
		private Int32 _financeMonth;
		public Int32 FinanceMonth
		{
			get
			{
				return _financeMonth;
			}
			set
			{
				_financeMonth = value;
			}
		}
		private DateTime _startDate;
		public DateTime StartDate
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
		private DateTime _endDate;
		public DateTime EndDate
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
        private Boolean _isClosed;
        public Boolean IsClosed
        {
            get
            {
                return _isClosed;
            }
            set
            {
                _isClosed = value;
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
            FinanceCalendarBase another = obj as FinanceCalendarBase;

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
