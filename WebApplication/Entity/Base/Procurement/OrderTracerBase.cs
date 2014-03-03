using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Procurement
{
    [Serializable]
    public abstract class OrderTracerBase : EntityBase
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
		private string _tracerType;
		public string TracerType
		{
			get
			{
				return _tracerType;
			}
			set
			{
				_tracerType = value;
			}
		}
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
		private string _item;
		public string Item
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
		private DateTime _reqTime;
		public DateTime ReqTime
		{
			get
			{
				return _reqTime;
			}
			set
			{
				_reqTime = value;
			}
		}
		private Decimal _orderedQty;
		public Decimal OrderedQty
		{
			get
			{
				return _orderedQty;
			}
			set
			{
				_orderedQty = value;
			}
		}
		private Decimal _accumulateQty;
		public Decimal AccumulateQty
		{
			get
			{
				return _accumulateQty;
			}
			set
			{
				_accumulateQty = value;
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
		private Int32 _refOrderLocTransId;
		public Int32 RefOrderLocTransId
		{
			get
			{
				return _refOrderLocTransId;
			}
			set
			{
				_refOrderLocTransId = value;
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
            OrderTracerBase another = obj as OrderTracerBase;

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
