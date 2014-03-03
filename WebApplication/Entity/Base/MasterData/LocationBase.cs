using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class LocationBase : EntityBase
    {
        #region O/R Mapping Properties
		private Boolean _allowNegativeInventory;
		public Boolean AllowNegativeInventory
		{
			get
			{
				return _allowNegativeInventory;
			}
			set
			{
				_allowNegativeInventory = value;
			}
		}
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
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		private com.Sconit.Entity.MasterData.Region _region;
		public com.Sconit.Entity.MasterData.Region Region
		{
			get
			{
				return _region;
			}
			set
			{
				_region = value;
			}
		}
		
		private Decimal? _volume;
        public Decimal? Volume
		{
			get
			{
				return _volume;
			}
			set
			{
				_volume = value;
			}
		}

        private Boolean _enableAdvWM;
        public Boolean EnableAdvWM
        {
            get
            {
                return _enableAdvWM;
            }
            set
            {
                _enableAdvWM = value;
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

        private Boolean _isSettleConsignment;
        public Boolean IsSettleConsignment
        {
            get
            {
                return _isSettleConsignment;
            }
            set
            {
                _isSettleConsignment = value;
            }
        }
        public Boolean IsMRP { get; set; }
        public Boolean IsAutoConfirm { get; set; }
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
            LocationBase another = obj as LocationBase;

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
