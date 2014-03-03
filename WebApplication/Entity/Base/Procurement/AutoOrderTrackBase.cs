using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    [Serializable]
    public abstract class AutoOrderTrackBase : EntityBase
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
		private com.Sconit.Entity.MasterData.OrderDetail _orderDetail;
		public com.Sconit.Entity.MasterData.OrderDetail OrderDetail
		{
			get
			{
				return _orderDetail;
			}
			set
			{
				_orderDetail = value;
			}
		}
		private com.Sconit.Entity.MasterData.OrderDetail _refOrderDetail;
		public com.Sconit.Entity.MasterData.OrderDetail RefOrderDetail
		{
			get
			{
				return _refOrderDetail;
			}
			set
			{
				_refOrderDetail = value;
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
		private Decimal _orderQty;
		public Decimal OrderQty
		{
			get
			{
				return _orderQty;
			}
			set
			{
				_orderQty = value;
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
            AutoOrderTrackBase another = obj as AutoOrderTrackBase;

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
