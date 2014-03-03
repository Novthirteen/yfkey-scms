using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class WorkingHoursBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Receipt _receipt;
		public com.Sconit.Entity.MasterData.Receipt Receipt
		{
			get
			{
				return _receipt;
			}
			set
			{
				_receipt = value;
			}
		}
		private com.Sconit.Entity.MasterData.Employee _employee;
		public com.Sconit.Entity.MasterData.Employee Employee
		{
			get
			{
				return _employee;
			}
			set
			{
				_employee = value;
			}
		}
		private Decimal _hours;
		public Decimal Hours
		{
			get
			{
				return _hours;
			}
			set
			{
				_hours = value;
			}
		}
		private DateTime? _lastModifyDate;
		public DateTime? LastModifyDate
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
            WorkingHoursBase another = obj as WorkingHoursBase;

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
