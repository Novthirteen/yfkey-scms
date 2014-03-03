using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class NamedQueryBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private com.Sconit.Entity.MasterData.User _user;
		public com.Sconit.Entity.MasterData.User User
		{
			get
			{
				return _user;
			}
			set
			{
				_user = value;
			}
		}
		private string _queryName;
		public string QueryName
		{
			get
			{
				return _queryName;
			}
			set
			{
				_queryName = value;
			}
		}
        private string _userControlPath;
        public string UserControlPath
        {
            get
            {
                return _userControlPath;
            }
            set
            {
                _userControlPath = value;
            }
        }
		private string _moduleParameter;
		public string ModuleParameter
		{
			get
			{
				return _moduleParameter;
			}
			set
			{
				_moduleParameter = value;
			}
		}
		private string _actionParameter;
		public string ActionParameter
		{
			get
			{
				return _actionParameter;
			}
			set
			{
				_actionParameter = value;
			}
		}
        
        #endregion

		public override int GetHashCode()
        {
			if (User != null && QueryName != null)
            {
                return User.GetHashCode() ^ QueryName.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            NamedQueryBase another = obj as NamedQueryBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.User == another.User) && (this.QueryName == another.QueryName);
            }
        } 
    }
	
}
