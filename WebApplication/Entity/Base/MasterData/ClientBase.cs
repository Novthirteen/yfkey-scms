using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ClientBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _clientId;
		public string ClientId
		{
			get
			{
				return _clientId;
			}
			set
			{
				_clientId = value;
			}
		}
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
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
			if (ClientId != null)
            {
                return ClientId.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            ClientBase another = obj as ClientBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.ClientId == another.ClientId);
            }
        } 
    }
	
}
