using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class InspectResultBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private Decimal? _qualifiedQty;
		public Decimal? QualifiedQty
		{
			get
			{
				return _qualifiedQty;
			}
			set
			{
				_qualifiedQty = value;
			}
		}
		private Decimal? _rejectedQty;
		public Decimal? RejectedQty
		{
			get
			{
				return _rejectedQty;
			}
			set
			{
				_rejectedQty = value;
			}
		}
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
		private com.Sconit.Entity.MasterData.InspectOrderDetail _inspectOrderDetail;
		public com.Sconit.Entity.MasterData.InspectOrderDetail InspectOrderDetail
		{
			get
			{
				return _inspectOrderDetail;
			}
			set
			{
				_inspectOrderDetail = value;
			}
		}

        private String _inspectResultNo;
        public String InspectResultNo
        {
            get
            {
                return _inspectResultNo;
            }
            set
            {
                _inspectResultNo = value;
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

        public Int32 PrintCount { get; set; }
        public Boolean IsPrinted { get; set; }
        public String PrintNo { get; set; }

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
            InspectResultBase another = obj as InspectResultBase;

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
