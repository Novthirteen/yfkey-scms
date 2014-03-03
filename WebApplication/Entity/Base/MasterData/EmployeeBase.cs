using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class EmployeeBase : EntityBase
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
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
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
		private string _department;
		public string Department
		{
			get
			{
				return _department;
			}
			set
			{
				_department = value;
			}
		}
		private string _workGroup;
		public string WorkGroup
		{
			get
			{
				return _workGroup;
			}
			set
			{
				_workGroup = value;
			}
		}
		private string _post;
		public string Post
		{
			get
			{
				return _post;
			}
			set
			{
				_post = value;
			}
		}
		private string _memo;
		public string Memo
		{
			get
			{
				return _memo;
			}
			set
			{
				_memo = value;
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
            EmployeeBase another = obj as EmployeeBase;

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
