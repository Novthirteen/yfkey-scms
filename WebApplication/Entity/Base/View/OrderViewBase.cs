using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class OrderViewBase : EntityBase
    {
        #region O/R Mapping Properties

        private DateTime _date;
        public DateTime Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
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
		private string _pickListNo;
		public string PickListNo
		{
			get
			{
				return _pickListNo;
			}
			set
			{
				_pickListNo = value;
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
		private string _orderViewType;
        public string OrderViewType
		{
			get
			{
                return _orderViewType;
			}
			set
			{
                _orderViewType = value;
			}
		}
		private string _status;
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}
		private com.Sconit.Entity.MasterData.Party _partyTo;
		public com.Sconit.Entity.MasterData.Party PartyTo
		{
			get
			{
				return _partyTo;
			}
			set
			{
				_partyTo = value;
			}
		}
		private DateTime _createDate;
		public DateTime CreateDate
		{
			get
			{
				return _createDate;
			}
			set
			{
				_createDate = value;
			}
		}
		private DateTime? _startDate;
		public DateTime? StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _startUser;
		public com.Sconit.Entity.MasterData.User StartUser
		{
			get
			{
				return _startUser;
			}
			set
			{
				_startUser = value;
			}
		}
		private DateTime? _completeDate;
		public DateTime? CompleteDate
		{
			get
			{
				return _completeDate;
			}
			set
			{
				_completeDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _completeUser;
		public com.Sconit.Entity.MasterData.User CompleteUser
		{
			get
			{
				return _completeUser;
			}
			set
			{
				_completeUser = value;
			}
		}
        
        #endregion

		public override int GetHashCode()
        {
			if (OrderNo != null)
            {
                return OrderNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            OrderViewBase another = obj as OrderViewBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.OrderNo == another.OrderNo);
            }
        } 
    }
	
}
