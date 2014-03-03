using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class InspectOrderBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _inspectNo;
		public string InspectNo
		{
			get
			{
				return _inspectNo;
			}
			set
			{
				_inspectNo = value;
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
        private Boolean _isDetailHasHu;
        public Boolean IsDetailHasHu
        {
            get
            {
                return _isDetailHasHu;
            }
            set
            {
                _isDetailHasHu = value;
            }
        }
		private com.Sconit.Entity.MasterData.User _createUser;
		public com.Sconit.Entity.MasterData.User CreateUser
		{
			get
			{
				return _createUser;
			}
			set
			{
				_createUser = value;
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
        public Boolean IsPrinted { get; set; }

        public string IpNo { get; set; }

        public string ReceiptNo { get; set; }

        public Boolean IsSeperated { get; set; }

        public string Region { get; set; }

        private IList<InspectOrderDetail> _inspectOrderDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<InspectOrderDetail> InspectOrderDetails
        {
            get
            {
                return _inspectOrderDetails;
            }
            set
            {
                _inspectOrderDetails = value;
            }
        }
        private DateTime _estimateInspectDate;
        public DateTime EstimateInspectDate
        {
            get
            {
                return _estimateInspectDate;
            }
            set
            {
                _estimateInspectDate = value;
            }
        }
        #endregion

		public override int GetHashCode()
        {
			if (InspectNo != null)
            {
                return InspectNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            InspectOrderBase another = obj as InspectOrderBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.InspectNo == another.InspectNo);
            }
        } 
    }
	
}
