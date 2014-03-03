using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Distribution
{
    [Serializable]
    public abstract class CustomerRollingPlanBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Flow _flow;
		public com.Sconit.Entity.MasterData.Flow Flow
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
		private string _externalOrderNo;
		public string ExternalOrderNo
		{
			get
			{
				return _externalOrderNo;
			}
			set
			{
				_externalOrderNo = value;
			}
		}
		private DateTime? _releaseDate;
		public DateTime? ReleaseDate
		{
			get
			{
				return _releaseDate;
			}
			set
			{
				_releaseDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.FileUpload _fileUpload;
		public com.Sconit.Entity.MasterData.FileUpload FileUpload
		{
			get
			{
				return _fileUpload;
			}
			set
			{
				_fileUpload = value;
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
		private DateTime? _lastModifyDate;
		public DateTime? LastModifyDate
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
		private Int32? _lastModifyUser;
		public Int32? LastModifyUser
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
		private string _activeStatus;
		public string ActiveStatus
		{
			get
			{
				return _activeStatus;
			}
			set
			{
				_activeStatus = value;
			}
		}
		private IList<CustomerRollingPlan> _customerRollingPlanDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<CustomerRollingPlan> CustomerRollingPlanDetails
        {
            get
            {
                return _customerRollingPlanDetails;
            }
            set
            {
                _customerRollingPlanDetails = value;
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
            CustomerRollingPlanBase another = obj as CustomerRollingPlanBase;

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
