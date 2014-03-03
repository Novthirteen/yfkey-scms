using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ClientOrderHeadBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _id;
		public string Id
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
		private com.Sconit.Entity.MasterData.Client _client;
		public com.Sconit.Entity.MasterData.Client Client
		{
			get
			{
				return _client;
			}
			set
			{
				_client = value;
			}
		}
		private string _orderNo;
		public string OrderNo
		{
			get
			{
				return _orderNo;
			}
			set
			{
				_orderNo = value;
			}
		}
		private string _userCode;
		public string UserCode
		{
			get
			{
				return _userCode;
			}
			set
			{
				_userCode = value;
			}
		}
		private string _orderType;
		public string OrderType
		{
			get
			{
				return _orderType;
			}
			set
			{
				_orderType = value;
			}
		}
		private string _flow;
		public string Flow
		{
			get
			{
				return _flow;
			}
			set
			{
				_flow = value;
			}
		}
		private string _synStatus;
		public string SynStatus
		{
			get
			{
				return _synStatus;
			}
			set
			{
				_synStatus = value;
			}
		}
        private DateTime _synTime;
        public DateTime SynTime
        {
            get
            {
                return _synTime;
            }
            set
            {
                _synTime = value;
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
            ClientOrderHeadBase another = obj as ClientOrderHeadBase;

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
