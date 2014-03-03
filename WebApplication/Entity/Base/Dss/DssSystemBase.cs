using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public abstract class DssSystemBase : EntityBase
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
        private string _description;
        public string Description
		{
			get
			{
                return _description;
			}
			set
			{
                _description = value;
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
        private string _undefinedString1;
        public string UndefinedString1
        {
            get
            {
                return _undefinedString1;
            }
            set
            {
                _undefinedString1 = value;
            }
        }
        private string _undefinedString2;
        public string UndefinedString2
        {
            get
            {
                return _undefinedString2;
            }
            set
            {
                _undefinedString2 = value;
            }
        }
        private string _undefinedString3;
        public string UndefinedString3
        {
            get
            {
                return _undefinedString3;
            }
            set
            {
                _undefinedString3 = value;
            }
        }
        private string _undefinedString4;
        public string UndefinedString4
        {
            get
            {
                return _undefinedString4;
            }
            set
            {
                _undefinedString4 = value;
            }
        }
        private string _undefinedString5;
        public string UndefinedString5
        {
            get
            {
                return _undefinedString5;
            }
            set
            {
                _undefinedString5 = value;
            }
        }
        public string SysAlias { get; set; }
        public string Flag { get; set; }
        public string Prefix1 { get; set; }
        public string Prefix2 { get; set; }
        public string Prefix3 { get; set; }
        public string Prefix4 { get; set; }
        public string Prefix5 { get; set; }

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
            DssSystemBase another = obj as DssSystemBase;

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
