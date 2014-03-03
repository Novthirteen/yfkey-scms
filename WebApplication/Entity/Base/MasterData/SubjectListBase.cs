using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class SubjectListBase : EntityBase
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
		private string _subjectCode;
		public string SubjectCode
		{
			get
			{
				return _subjectCode;
			}
			set
			{
				_subjectCode = value;
			}
		}
		private string _accountCode;
		public string AccountCode
		{
			get
			{
				return _accountCode;
			}
			set
			{
				_accountCode = value;
			}
		}
		private string _costCenterCode;
		public string CostCenterCode
		{
			get
			{
				return _costCenterCode;
			}
			set
			{
				_costCenterCode = value;
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
		private string _accountType;
		public string AccountType
		{
			get
			{
				return _accountType;
			}
			set
			{
				_accountType = value;
			}
		}
		private string _subjectName;
		public string SubjectName
		{
			get
			{
				return _subjectName;
			}
			set
			{
				_subjectName = value;
			}
		}
		private string _costCenterName;
        public string CostCenterName
		{
			get
			{
                return _costCenterName;
			}
			set
			{
                _costCenterName = value;
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
            SubjectListBase another = obj as SubjectListBase;

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
