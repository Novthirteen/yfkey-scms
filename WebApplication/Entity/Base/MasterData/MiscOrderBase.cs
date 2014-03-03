using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class MiscOrderBase : EntityBase
    {
        #region O/R Mapping Properties
		
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
        private string _referenceOrderNo;
        public string ReferenceOrderNo
        {
            get
            {
                return _referenceOrderNo;
            }
            set
            {
                _referenceOrderNo = value;
            }
        }
		private string _type;
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
		private com.Sconit.Entity.MasterData.Location _location;
		public com.Sconit.Entity.MasterData.Location Location
		{
			get
			{
				return _location;
			}
			set
			{
				_location = value;
			}
		}
		private string _reason;
		public string Reason
		{
			get
			{
				return _reason;
			}
			set
			{
				_reason = value;
			}
		}
        private string _projectCode;
        public string ProjectCode
        {
            get
            {
                return _projectCode;
            }
            set
            {
                _projectCode = value;
            }
        }
        private SubjectList _subjectList;
        public SubjectList SubjectList
        {
            get
            {
                return _subjectList;
            }
            set
            {
                _subjectList = value;
            }
        }
		private DateTime _effectiveDate;
		public DateTime EffectiveDate
		{
			get
			{
				return _effectiveDate;
			}
			set
			{
				_effectiveDate = value;
			}
		}
		private string _remark;
		public string Remark
		{
			get
			{
				return _remark;
			}
			set
			{
				_remark = value;
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
        private IList<MiscOrderDetail> _orderDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<MiscOrderDetail> MiscOrderDetails
        {
            get
            {
                return _orderDetails;
            }
            set
            {
                _orderDetails = value;
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
            MiscOrderBase another = obj as MiscOrderBase;

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
