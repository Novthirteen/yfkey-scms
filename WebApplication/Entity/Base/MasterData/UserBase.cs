using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class UserBase : EntityBase
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
		private string _password;
		public string Password
		{
			get
			{
				return _password;
			}
			set
			{
				_password = value;
			}
		}
		private string _firstName;
        public string FirstName
		{
			get
			{
                return _firstName;
			}
			set
			{
                _firstName = value;
			}
		}
        private string _lastName;
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                _lastName = value;
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
		private string _gender;
		public string Gender
		{
			get
			{
				return _gender;
			}
			set
			{
				_gender = value;
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
		private string _mobliePhone;
		public string MobliePhone
		{
			get
			{
				return _mobliePhone;
			}
			set
			{
				_mobliePhone = value;
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
        private DateTime _lastModifyDate;
        public DateTime LastModifyDate
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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
        private IList<UserPreference> _UserPreferences;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<UserPreference> UserPreferences
        {
            get
            {
                return _UserPreferences;
            }
            set
            {
                _UserPreferences = value;
            }
        }
        private IList<UserPermission> _UserPermissions;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<UserPermission> UserPermissions
        {
            get
            {
                return _UserPermissions;
            }
            set
            {
                _UserPermissions = value;
            }
        }
        private IList<Role> _Roles;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<Role> Roles
        {
            get
            {
                return _Roles;
            }
            set
            {
                _Roles = value;
            }
        }

        /// <summary>
        /// 需要使用强密码
        /// </summary>
        public Boolean IsEnforcePolicy { get; set; }
        /// <summary>
        /// 需要修改密码
        /// </summary>
        public Boolean IsEnforceExpiration { get; set; }
        #endregion

		public override int GetHashCode()
        {
			if (Code != string.Empty && Code != null)
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
            UserBase another = obj as UserBase;

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
