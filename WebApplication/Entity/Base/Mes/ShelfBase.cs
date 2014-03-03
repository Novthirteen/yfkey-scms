using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Mes
{
    [Serializable]
    public abstract class ShelfBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Flow _productLine;
		public com.Sconit.Entity.MasterData.Flow ProductLine
		{
			get
			{
				return _productLine;
			}
			set
			{
				_productLine = value;
			}
		}
		private string _tagNo;
		public string TagNo
		{
			get
			{
				return _tagNo;
			}
			set
			{
				_tagNo = value;
			}
		}
		private Int32 _capacity;
		public Int32 Capacity
		{
			get
			{
				return _capacity;
			}
			set
			{
				_capacity = value;
			}
		}
		private Int32 _currentCartons;
		public Int32 CurrentCartons
		{
			get
			{
				return _currentCartons;
			}
			set
			{
				_currentCartons = value;
			}
		}
		private string _originalCartonNo;
		public string OriginalCartonNo
		{
			get
			{
				return _originalCartonNo;
			}
			set
			{
				_originalCartonNo = value;
			}
		}

        private Boolean _isActive;
        public Boolean IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }
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
            ShelfBase another = obj as ShelfBase;

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
