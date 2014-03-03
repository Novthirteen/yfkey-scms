using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class UserPreferenceBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private com.Sconit.Entity.MasterData.User _user;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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
		private string _value;
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}
        
        #endregion

		public override int GetHashCode()
        {
			if (User != null && Code != null)
            {
                return User.GetHashCode() ^ Code.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            UserPreferenceBase another = obj as UserPreferenceBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.User == another.User) && (this.Code == another.Code);
            }
        } 
    }
	
}
