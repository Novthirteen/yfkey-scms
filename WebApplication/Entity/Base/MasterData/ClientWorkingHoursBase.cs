using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ClientWorkingHoursBase : EntityBase
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
		private com.Sconit.Entity.MasterData.ClientOrderHead _clientOrderHead;
		public com.Sconit.Entity.MasterData.ClientOrderHead ClientOrderHead
		{
			get
			{
				return _clientOrderHead;
			}
			set
			{
				_clientOrderHead = value;
			}
		}
		private string _orderNo;
		public string OrderNo
		{
			get
			{
				return _orderNo;
			}
			set
			{
				_orderNo = value;
			}
		}
		private string _employee;
		public string Employee
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
            ClientWorkingHoursBase another = obj as ClientWorkingHoursBase;

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
