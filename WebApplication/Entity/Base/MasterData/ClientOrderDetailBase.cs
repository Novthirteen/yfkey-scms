using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ClientOrderDetailBase : EntityBase
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
		private Int32? _seq;
		public Int32? Seq
		{
			get
			{
				return _seq;
			}
			set
			{
				_seq = value;
			}
		}
		private com.Sconit.Entity.MasterData.ClientOrderHead _clientOrderHead;
		public com.Sconit.Entity.MasterData.ClientOrderHead ClientOrderHead
		{
			get
			{
				return _clientOrderHead;
			}
			set
			{
				_clientOrderHead = value;
			}
		}
		private string _itemCode;
		public string ItemCode
		{
			get
			{
				return _itemCode;
			}
			set
			{
				_itemCode = value;
			}
		}
		private string _itemDescription;
		public string ItemDescription
		{
			get
			{
				return _itemDescription;
			}
			set
			{
				_itemDescription = value;
			}
		}
		private string _uomCode;
		public string UomCode
		{
			get
			{
				return _uomCode;
			}
			set
			{
				_uomCode = value;
			}
		}
		private string _uomDescription;
		public string UomDescription
		{
			get
			{
				return _uomDescription;
			}
			set
			{
				_uomDescription = value;
			}
		}
		private Decimal? _unitCount;
		public Decimal? UnitCount
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
		private Decimal? _orderedQty;
		public Decimal? OrderedQty
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
		private Decimal? _shippedQty;
		public Decimal? ShippedQty
		{
			get
			{
				return _shippedQty;
			}
			set
			{
				_shippedQty = value;
			}
		}
		private Decimal? _receivedQty;
		public Decimal? ReceivedQty
		{
			get
			{
				return _receivedQty;
			}
			set
			{
				_receivedQty = value;
			}
		}
		private Decimal? _receiveQty;
		public Decimal? ReceiveQty
		{
			get
			{
				return _receiveQty;
			}
			set
			{
				_receiveQty = value;
			}
		}
		private Decimal? _rejectQty;
		public Decimal? RejectQty
		{
			get
			{
				return _rejectQty;
			}
			set
			{
				_rejectQty = value;
			}
		}
		private Decimal? _scrapQty;
		public Decimal? ScrapQty
		{
			get
			{
				return _scrapQty;
			}
			set
			{
				_scrapQty = value;
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
            ClientOrderDetailBase another = obj as ClientOrderDetailBase;

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
