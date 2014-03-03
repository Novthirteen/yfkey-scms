using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class RepackDetailBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Repack _repack;
		public com.Sconit.Entity.MasterData.Repack Repack
		{
			get
			{
				return _repack;
			}
			set
			{
				_repack = value;
			}
		}
		private com.Sconit.Entity.MasterData.LocationLotDetail _locationLotDetail;
		public com.Sconit.Entity.MasterData.LocationLotDetail LocationLotDetail
		{
			get
			{
				return _locationLotDetail;
			}
			set
			{
				_locationLotDetail = value;
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
		private string _iOType;
		public string IOType
		{
			get
			{
				return _iOType;
			}
			set
			{
				_iOType = value;
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
            RepackDetailBase another = obj as RepackDetailBase;

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
