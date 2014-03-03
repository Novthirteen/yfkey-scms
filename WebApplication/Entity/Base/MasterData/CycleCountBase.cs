using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class CycleCountBase : EntityBase
    {
        #region O/R Mapping Properties
		
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
		private DateTime _effDate;
        public DateTime EffectiveDate
		{
			get
			{
				return _effDate;
			}
			set
			{
				_effDate = value;
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
        private string _createUser;
        public string CreateUser
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
        private string _releaseUser;
        public string ReleaseUser
		{
			get
			{
				return _releaseUser;
			}
			set
			{
				_releaseUser = value;
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
        private string _cancelUser;
        public string CancelUser
		{
			get
			{
				return _cancelUser;
			}
			set
			{
				_cancelUser = value;
			}
		}
		private DateTime? _cancelDate;
		public DateTime? CancelDate
		{
			get
			{
				return _cancelDate;
			}
			set
			{
				_cancelDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _closeUser;
		public com.Sconit.Entity.MasterData.User CloseUser
		{
			get
			{
				return _closeUser;
			}
			set
			{
				_closeUser = value;
			}
		}
		private DateTime? _closeDate;
		public DateTime? CloseDate
		{
			get
			{
				return _closeDate;
			}
			set
			{
				_closeDate = value;
			}
        }
        private IList<CycleCountDetail> _cycleCountDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<CycleCountDetail> CycleCountDetails
        {
            get
            {
                return _cycleCountDetails;
            }
            set
            {
                _cycleCountDetails = value;
            }
        }

        public string Bins { get; set; }
        public string Items { get; set; }
        public bool IsScanHu { get; set; }
        public string StartUser { get; set; }
        public DateTime? StartDate { get; set; }
        public string CompleteUser { get; set; }
        public DateTime? CompleteDate { get; set; }
        public string PhyCntGroupBy { get; set; }
        #endregion

		public override int GetHashCode()
        {
			if (Code != null)
            {
                return Code.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            CycleCountBase another = obj as CycleCountBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Code == another.Code);
            }
        } 
    }
	
}
