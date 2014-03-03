using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class PickListResultBase : EntityBase
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
		private com.Sconit.Entity.MasterData.PickListDetail _pickListDetail;
		public com.Sconit.Entity.MasterData.PickListDetail PickListDetail
		{
			get
			{
				return _pickListDetail;
			}
			set
			{
				_pickListDetail = value;
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
            PickListResultBase another = obj as PickListResultBase;

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
