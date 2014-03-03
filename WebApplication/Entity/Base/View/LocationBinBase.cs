using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class LocationBinBase : EntityBase
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
		private string _region;
		public string Region
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
		private string _location;
		public string Location
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
		private string _locationName;
		public string LocationName
		{
			get
			{
				return _locationName;
			}
			set
			{
				_locationName = value;
			}
		}
		private string _area;
		public string Area
		{
			get
			{
				return _area;
			}
			set
			{
				_area = value;
			}
		}
		private string _areaDescription;
		public string AreaDescription
		{
			get
			{
				return _areaDescription;
			}
			set
			{
				_areaDescription = value;
			}
		}
		private string _bin;
		public string Bin
		{
			get
			{
				return _bin;
			}
			set
			{
				_bin = value;
			}
		}
		private string _binDescription;
		public string BinDescription
		{
			get
			{
				return _binDescription;
			}
			set
			{
				_binDescription = value;
			}
		}
        private Int32 _itemCount;
        public Int32 ItemCount
        {
            get
            {
                return _itemCount;
            }
            set
            {
                _itemCount = value;
            }
        }
        private Int32 _huCount;
        public Int32 HuCount
        {
            get
            {
                return _huCount;
            }
            set
            {
                _huCount = value;
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
            LocationBinBase another = obj as LocationBinBase;

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
