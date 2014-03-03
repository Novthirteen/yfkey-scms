using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class VehicleBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _code;
		public string Code
		{
			get
			{
				return _code;
			}
			set
			{
				_code = value;
			}
		}
		private Boolean _isActive;
		public Boolean IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}
		private com.Sconit.Entity.Transportation.Carrier _carrier;
		public com.Sconit.Entity.Transportation.Carrier Carrier
		{
			get
			{
				return _carrier;
			}
			set
			{
				_carrier = value;
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
		private string _driver;
		public string Driver
		{
			get
			{
				return _driver;
			}
			set
			{
				_driver = value;
			}
		}
		private string _phone;
		public string Phone
		{
			get
			{
				return _phone;
			}
			set
			{
				_phone = value;
			}
		}
		private string _mobilePhone;
        public string MobilePhone
		{
			get
			{
                return _mobilePhone;
			}
			set
			{
                _mobilePhone = value;
			}
		}
        
        #endregion

		public override int GetHashCode()
        {
			if (Code != null)
            {
                return Code.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            VehicleBase another = obj as VehicleBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Code == another.Code);
            }
        } 
    }
	
}
