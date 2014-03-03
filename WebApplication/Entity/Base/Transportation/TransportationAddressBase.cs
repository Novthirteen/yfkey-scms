using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportationAddressBase : EntityBase
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
		private string _country;
		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				_country = value;
			}
		}
		private string _province;
		public string Province
		{
			get
			{
				return _province;
			}
			set
			{
				_province = value;
			}
		}
		private string _city;
		public string City
		{
			get
			{
				return _city;
			}
			set
			{
				_city = value;
			}
		}
		private string _district;
		public string District
		{
			get
			{
				return _district;
			}
			set
			{
				_district = value;
			}
		}
		private string _address;
		public string Address
		{
			get
			{
				return _address;
			}
			set
			{
				_address = value;
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
            TransportationAddressBase another = obj as TransportationAddressBase;

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
