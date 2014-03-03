using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class PickListDetailBase : EntityBase
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
		private com.Sconit.Entity.MasterData.PickList _pickList;
		public com.Sconit.Entity.MasterData.PickList PickList
		{
			get
			{
				return _pickList;
			}
			set
			{
				_pickList = value;
			}
		}
        private com.Sconit.Entity.MasterData.OrderLocationTransaction _orderLocationTransaction;
        public com.Sconit.Entity.MasterData.OrderLocationTransaction OrderLocationTransaction
		{
			get
			{
                return _orderLocationTransaction;
			}
			set
			{
                _orderLocationTransaction = value;
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
		private com.Sconit.Entity.MasterData.StorageArea _storageArea;
		public com.Sconit.Entity.MasterData.StorageArea StorageArea
		{
			get
			{
				return _storageArea;
			}
			set
			{
				_storageArea = value;
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
		private string _huId;
		public string HuId
		{
			get
			{
				return _huId;
			}
			set
			{
				_huId = value;
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

        private IList<PickListResult> _pickListResults;
        public IList<PickListResult> PickListResults
        {
            get
            {
                return _pickListResults;
            }
            set
            {
                _pickListResults = value;
            }
        }
        public String Memo { get; set; }
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
            PickListDetailBase another = obj as PickListDetailBase;

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
