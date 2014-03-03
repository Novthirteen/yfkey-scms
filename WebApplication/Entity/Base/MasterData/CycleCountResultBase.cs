using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class CycleCountResultBase : EntityBase
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
		private com.Sconit.Entity.MasterData.CycleCount _cycleCount;
		public com.Sconit.Entity.MasterData.CycleCount CycleCount
		{
			get
			{
				return _cycleCount;
			}
			set
			{
				_cycleCount = value;
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
        //private com.Sconit.Entity.MasterData.Hu _hu;
        //public com.Sconit.Entity.MasterData.Hu Hu
        //{
        //    get
        //    {
        //        return _hu;
        //    }
        //    set
        //    {
        //        _hu = value;
        //    }
        //}
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
		private Decimal _invQty;
		public Decimal InvQty
		{
			get
			{
				return _invQty;
			}
			set
			{
				_invQty = value;
			}
		}
		private Decimal _diffQty;
		public Decimal DiffQty
		{
			get
			{
				return _diffQty;
			}
			set
			{
				_diffQty = value;
			}
		}
		private string _diffReason;
		public string DiffReason
		{
			get
			{
				return _diffReason;
			}
			set
			{
				_diffReason = value;
			}
        }
        //private com.Sconit.Entity.MasterData.StorageBin _storageBin;
        //public com.Sconit.Entity.MasterData.StorageBin StorageBin
        //{
        //    get
        //    {
        //        return _storageBin;
        //    }
        //    set
        //    {
        //        _storageBin = value;
        //    }
        //}
        private com.Sconit.Entity.MasterData.Location _referenceLocation;
        public com.Sconit.Entity.MasterData.Location ReferenceLocation
        {
            get
            {
                return _referenceLocation;
            }
            set
            {
                _referenceLocation = value;
            }
        }

        public string StorageBin { get; set; }
        public string HuId { get; set; }
        public bool? IsProcessed { get; set; }
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
            CycleCountResultBase another = obj as CycleCountResultBase;

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
