using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ItemBase : EntityBase
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
		private string _desc1;
		public string Desc1
		{
			get
			{
				return _desc1;
			}
			set
			{
				_desc1 = value;
			}
		}
		private string _desc2;
		public string Desc2
		{
			get
			{
				return _desc2;
			}
			set
			{
				_desc2 = value;
			}
		}
		private com.Sconit.Entity.MasterData.Uom _uom;
		public com.Sconit.Entity.MasterData.Uom Uom
		{
			get
			{
				return _uom;
			}
			set
			{
				_uom = value;
			}
		}
		private Decimal _unitCount;
		public Decimal UnitCount
		{
			get
			{
				return _unitCount;
			}
			set
			{
				_unitCount = value;
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
		private string _imageUrl;
		public string ImageUrl
		{
			get
			{
				return _imageUrl;
			}
			set
			{
				_imageUrl = value;
			}
		}
		private com.Sconit.Entity.MasterData.Bom _bom;
		public com.Sconit.Entity.MasterData.Bom Bom
		{
			get
			{
				return _bom;
			}
			set
			{
				_bom = value;
			}
		}
		private com.Sconit.Entity.MasterData.Routing _routing;
		public com.Sconit.Entity.MasterData.Routing Routing
		{
			get
			{
				return _routing;
			}
			set
			{
				_routing = value;
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
		private string _memo;
		public string Memo
		{
			get
			{
				return _memo;
			}
			set
			{
				_memo = value;
			}
		}
        private string _category;
        public string Category
        {
            get
            {
                return _category;
            }
            set
            {
                _category = value;
            }
        }
        private string _plant;
        public string Plant
        {
            get
            {
                return _plant;
            }
            set
            {
                _plant = value;
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
        [System.Xml.Serialization.XmlIgnoreAttribute()]
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
        private string _backFlushMethod;
        public string BackFlushMethod
        {
            get
            {
                return _backFlushMethod;
            }
            set
            {
                _backFlushMethod = value;
            }
        }

        public string DefaultSupplier { get; set; }
        private Boolean _transferFlag;
        public Boolean TransferFlag
        {
            get
            {
                return _transferFlag;
            }
            set
            {
                _transferFlag = value;
            }
        }
        private Boolean _isMes;
        public Boolean IsMes
        {
            get
            {
                return _isMes;
            }
            set
            {
                _isMes = value;
            }
        }

        public Int32 LeadTime { get; set; }
        public Int32 SafeStock { get; set; }
        public Int32 MaxStock { get; set; }
        public Boolean IsMRP { get; set; }
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
            ItemBase another = obj as ItemBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return string.Equals(this.Code, another.Code, StringComparison.OrdinalIgnoreCase); 
            }
        } 
    }
	
}
