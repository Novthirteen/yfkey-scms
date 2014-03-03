using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class BillAgingViewBase : EntityBase
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
		private com.Sconit.Entity.MasterData.BillAddress _billAddress;
		public com.Sconit.Entity.MasterData.BillAddress BillAddress
		{
			get
			{
				return _billAddress;
			}
			set
			{
				_billAddress = value;
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
		private Decimal? _qty1;
		public Decimal? Qty1
		{
			get
			{
				return _qty1;
			}
			set
			{
				_qty1 = value;
			}
		}
		private Decimal? _qty2;
		public Decimal? Qty2
		{
			get
			{
				return _qty2;
			}
			set
			{
				_qty2 = value;
			}
		}
		private Decimal? _qty3;
		public Decimal? Qty3
		{
			get
			{
				return _qty3;
			}
			set
			{
				_qty3 = value;
			}
		}
		private Decimal? _qty4;
		public Decimal? Qty4
		{
			get
			{
				return _qty4;
			}
			set
			{
				_qty4 = value;
			}
		}
		private Decimal? _qty5;
		public Decimal? Qty5
		{
			get
			{
				return _qty5;
			}
			set
			{
				_qty5 = value;
			}
		}
		private Decimal? _qty6;
		public Decimal? Qty6
		{
			get
			{
				return _qty6;
			}
			set
			{
				_qty6 = value;
			}
		}
		private Decimal? _qty7;
		public Decimal? Qty7
		{
			get
			{
				return _qty7;
			}
			set
			{
				_qty7 = value;
			}
		}
		private Decimal? _qty8;
		public Decimal? Qty8
		{
			get
			{
				return _qty8;
			}
			set
			{
				_qty8 = value;
			}
		}
		private Decimal? _qty9;
		public Decimal? Qty9
		{
			get
			{
				return _qty9;
			}
			set
			{
				_qty9 = value;
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
            BillAgingViewBase another = obj as BillAgingViewBase;

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
