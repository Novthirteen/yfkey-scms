using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class CodeMasterBase : EntityBase
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
		private string _value;
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}
		private Int32 _seq;
		public Int32 Seq
		{
			get
			{
				return _seq;
			}
			set
			{
				_seq = value;
			}
		}
		private Boolean _isDefault;
		public Boolean IsDefault
		{
			get
			{
				return _isDefault;
			}
			set
			{
				_isDefault = value;
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
        
        #endregion

		public override int GetHashCode()
        {
			if (Code != null && Value != null)
            {
                return Code.GetHashCode() ^ Value.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            CodeMasterBase another = obj as CodeMasterBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Code == another.Code) && (this.Value == another.Value);
            }
        } 
    }
	
}
