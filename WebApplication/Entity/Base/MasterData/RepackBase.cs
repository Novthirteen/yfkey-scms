using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class RepackBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _repackNo;
		public string RepackNo
		{
			get
			{
				return _repackNo;
			}
			set
			{
				_repackNo = value;
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

        private IList<RepackDetail> _repackDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<RepackDetail> RepackDetails
        {
            get
            {
                return _repackDetails;
            }
            set
            {
                _repackDetails = value;
            }
        }

        #endregion

		public override int GetHashCode()
        {
			if (RepackNo != null)
            {
                return RepackNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            RepackBase another = obj as RepackBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.RepackNo == another.RepackNo);
            }
        } 
    }
	
}
