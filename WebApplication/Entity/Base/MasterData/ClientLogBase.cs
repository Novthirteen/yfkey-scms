using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ClientLogBase : EntityBase
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
		private string _operation;
		public string Operation
		{
			get
			{
				return _operation;
			}
			set
			{
				_operation = value;
			}
		}
		private string _result;
		public string Result
		{
			get
			{
				return _result;
			}
			set
			{
				_result = value;
			}
		}
		private string _message;
		public string Message
		{
			get
			{
				return _message;
			}
			set
			{
				_message = value;
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
            ClientLogBase another = obj as ClientLogBase;

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
