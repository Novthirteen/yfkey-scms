using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public abstract class CustomerScheduleBase : EntityBase
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
        private string _referenceScheduleNo;
        public string ReferenceScheduleNo
        {
            get
            {
                return _referenceScheduleNo;
            }
            set
            {
                _referenceScheduleNo = value;
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
        private string _lastModifyUser;
        public string LastModifyUser
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

        public string ReleaseUser { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public IList<CustomerScheduleDetail> CustomerScheduleDetails { get; set; }

        public string Type { get; set; }
        public Int32 Version { get; set; }
        public string ShipFlow { get; set; }
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
            CustomerScheduleBase another = obj as CustomerScheduleBase;

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
