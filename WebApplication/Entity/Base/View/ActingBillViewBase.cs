using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class ActingBillViewBase : EntityBase
    {
        #region O/R Mapping Properties

        private String _order;
		public String OrderNo
		{
			get
			{
				return _order;
			}
			set
			{
				_order = value;
			}
		}
		private string _receiptNo;
		public string ReceiptNo
		{
			get
			{
				return _receiptNo;
			}
			set
			{
				_receiptNo = value;
			}
		}
		private string _externalReceiptNo;
		public string ExternalReceiptNo
		{
			get
			{
				return _externalReceiptNo;
			}
			set
			{
				_externalReceiptNo = value;
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
		private DateTime _effectiveDate;
		public DateTime EffectiveDate
		{
			get
			{
				return _effectiveDate;
			}
			set
			{
				_effectiveDate = value;
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
            ActingBillViewBase another = obj as ActingBillViewBase;

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
