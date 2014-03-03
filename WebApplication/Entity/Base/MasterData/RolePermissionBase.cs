using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class RolePermissionBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Role _role;
		public com.Sconit.Entity.MasterData.Role Role
		{
			get
			{
				return _role;
			}
			set
			{
				_role = value;
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
            RolePermissionBase another = obj as RolePermissionBase;

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
