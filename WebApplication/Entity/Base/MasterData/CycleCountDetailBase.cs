using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class CycleCountDetailBase : EntityBase
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
        public string HuId { get; set; }
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
        public string StorageBin { get; set; }
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
            CycleCountDetailBase another = obj as CycleCountDetailBase;

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
