using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class UserPermissionBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Permission _permission;
		public com.Sconit.Entity.MasterData.Permission Permission
		{
			get
			{
				return _permission;
			}
			set
			{
				_permission = value;
			}
		}
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
        
        #endregion

		public override int GetHashCode()
        {
			if (Id != 0)
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
            UserPermissionBase another = obj as UserPermissionBase;

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
