using System;
using System.Collections;
using System.Collections.Generic;
using com.Sconit.Entity.Transportation;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class AddressBase : EntityBase
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
        private Party _party;
        [System.Xml.Serialization.XmlElement(Type = typeof(Supplier)), System.Xml.Serialization.XmlElement(Type = typeof(Region)), System.Xml.Serialization.XmlElement(Type = typeof(Customer)), System.Xml.Serialization.XmlElement(Type = typeof(Carrier))]
        public Party Party
		{
			get
			{
                return _party;
			}
			set
			{
                _party = value;
			}
		}
		private Int32 _sequence;
		public Int32 Sequence
		{
			get
			{
				return _sequence;
			}
			set
			{
				_sequence = value;
			}
		}
		private Boolean _isPrimary;
		public Boolean IsPrimary
		{
			get
			{
				return _isPrimary;
			}
			set
			{
				_isPrimary = value;
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
		private string _postalCode;
		public string PostalCode
		{
			get
			{
				return _postalCode;
			}
			set
			{
				_postalCode = value;
			}
		}
		private string _postalCodeExtention;
		public string PostalCodeExtention
		{
			get
			{
				return _postalCodeExtention;
			}
			set
			{
				_postalCodeExtention = value;
			}
		}
		private string _telephoneNumber;
		public string TelephoneNumber
		{
			get
			{
				return _telephoneNumber;
			}
			set
			{
				_telephoneNumber = value;
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
		private string _contactPersonName;
		public string ContactPersonName
		{
			get
			{
				return _contactPersonName;
			}
			set
			{
				_contactPersonName = value;
			}
		}
		private string _fax;
		public string Fax
		{
			get
			{
				return _fax;
			}
			set
			{
				_fax = value;
			}
		}
		private string _email;
		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				_email = value;
			}
		}
		private string _webSite;
		public string WebSite
		{
			get
			{
				return _webSite;
			}
			set
			{
				_webSite = value;
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
            AddressBase another = obj as AddressBase;

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
