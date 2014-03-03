using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class InProcessLocationDetailTrackViewBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Flow _flow;
		public com.Sconit.Entity.MasterData.Flow Flow
		{
			get
			{
				return _flow;
			}
			set
			{
				_flow = value;
			}
		}
		private Int32? _currOperation;
        public Int32? CurrentOperation
		{
			get
			{
				return _currOperation;
			}
			set
			{
				_currOperation = value;
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
            InProcessLocationDetailTrackViewBase another = obj as InProcessLocationDetailTrackViewBase;

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
