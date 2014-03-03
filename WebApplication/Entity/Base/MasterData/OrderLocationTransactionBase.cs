using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class OrderLocationTransactionBase : EntityBase
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
        private BomDetail _bomDetail;
        public BomDetail BomDetail
        {
            get
            {
                return _bomDetail;
            }
            set
            {
                _bomDetail = value;
            }
        }
        private Boolean _isAssemble;
        public Boolean IsAssemble
        {
            get
            {
                return _isAssemble;
            }
            set
            {
                _isAssemble = value;
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
		private Int32 _operation;
        public Int32 Operation
		{
			get
			{
				return _operation;
			}
			set
			{
				_operation = value;
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
		private string _transactionType;
		public string TransactionType
		{
			get
			{
				return _transactionType;
			}
			set
			{
				_transactionType = value;
			}
		}
		private Decimal _unitQty;
		public Decimal UnitQty
		{
			get
			{
				return _unitQty;
			}
			set
			{
				_unitQty = value;
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
		private Decimal? _accumulateQty;
		public Decimal? AccumulateQty
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
		private Decimal? _accumulateRejectQty;
		public Decimal? AccumulateRejectQty
		{
			get
			{
				return _accumulateRejectQty;
			}
			set
			{
				_accumulateRejectQty = value;
			}
		}
        private Decimal? _accumulateScrapQty;
        public Decimal? AccumulateScrapQty
        {
            get
            {
                return _accumulateScrapQty;
            }
            set
            {
                _accumulateScrapQty = value;
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
		private com.Sconit.Entity.MasterData.Location _rejectLocation;
		public com.Sconit.Entity.MasterData.Location RejectLocation
		{
			get
			{
				return _rejectLocation;
			}
			set
			{
				_rejectLocation = value;
			}
		}
        private Boolean _IsShipScanHu;
        public Boolean IsShipScanHu
        {
            get
            {
                return _IsShipScanHu;
            }
            set
            {
                _IsShipScanHu = value;
            }
        }
		private Int32? _huLotSize;
		public Int32? HuLotSize
		{
			get
			{
				return _huLotSize;
			}
			set
			{
				_huLotSize = value;
			}
		}
		private Boolean _needPrint;
		public Boolean NeedPrint
		{
			get
			{
				return _needPrint;
			}
			set
			{
				_needPrint = value;
			}
		}
        private String _backFlushMethod;
        public String BackFlushMethod
        {
            get
            {
                return _backFlushMethod;
            }
            set
            {
                _backFlushMethod = value;
            }
        }
        private String _itemVersion;
        public String ItemVersion
        {
            get
            {
                return _itemVersion;
            }
            set
            {
                _itemVersion = value;
            }
        }
        private String _tagNo;
        public String TagNo
        {
            get
            {
                return _tagNo;
            }
            set
            {
                _tagNo = value;
            }
        }
        private String _shelf;
        public String Shelf
        {
            get
            {
                return _shelf;
            }
            set
            {
                _shelf = value;
            }
        }
        private Int32 _cartons;
        public Int32 Cartons
        {
            get
            {
                return _cartons;
            }
            set
            {
                _cartons = value;
            }
        }
        //private Decimal? _plannedBackFlushQty;
        //public Decimal? PlannedBackFlushQty
        //{
        //    get
        //    {
        //        return _plannedBackFlushQty;
        //    }
        //    set
        //    {
        //        _plannedBackFlushQty = value;
        //    }
        //}
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
            OrderLocationTransactionBase another = obj as OrderLocationTransactionBase;

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
