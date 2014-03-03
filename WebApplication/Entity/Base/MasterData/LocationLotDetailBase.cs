using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class LocationLotDetailBase : EntityBase
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
        private com.Sconit.Entity.MasterData.StorageBin _storageBin;
        public com.Sconit.Entity.MasterData.StorageBin StorageBin
        {
            get
            {
                return _storageBin;
            }
            set
            {
                _storageBin = value;
            }
        }
		private com.Sconit.Entity.MasterData.Item _item;
		public com.Sconit.Entity.MasterData.Item Item
		{
			get
			{
				return _item;
			}
			set
			{
				_item = value;
			}
		}
        private com.Sconit.Entity.MasterData.Hu _hu;
        public com.Sconit.Entity.MasterData.Hu Hu
        {
            get
            {
                return _hu;
            }
            set
            {
                _hu = value;
            }
        }
		private string _lotNo;
		public string LotNo
		{
			get
			{
				return _lotNo;
			}
			set
			{
				_lotNo = value;
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
		private Decimal _qty;
		public Decimal Qty
		{
			get
			{
				return _qty;
			}
			set
			{
				_qty = value;
			}
		}
        private Boolean _isConsignment;
        public Boolean IsConsignment
        {
            get
            {
                return _isConsignment;
            }
            set
            {
                _isConsignment = value;
            }
        }
        private Int32? _plannedBill;
        public Int32? PlannedBill
        {
            get
            {
                return _plannedBill;
            }
            set
            {
                _plannedBill = value;
            }
        }

        private DateTime? _lastModifyDate = DateTime.Now;
        public DateTime? LastModifyDate 
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

        private String _refLocation;
        public String RefLocation
        {
            get
            {
                return _refLocation;
            }
            set
            {
                _refLocation = value;
            }
        }
        #endregion

		public override int GetHashCode()
        {
			if (Id != 0)
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
            LocationLotDetailBase another = obj as LocationLotDetailBase;

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
