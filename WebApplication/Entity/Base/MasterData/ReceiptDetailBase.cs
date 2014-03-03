using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class ReceiptDetailBase : EntityBase
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
		private com.Sconit.Entity.MasterData.Receipt _receipt;
		public com.Sconit.Entity.MasterData.Receipt Receipt
		{
			get
			{
				return _receipt;
			}
			set
			{
				_receipt = value;
			}
		}
		private com.Sconit.Entity.MasterData.OrderLocationTransaction _orderLocationTransaction;
		public com.Sconit.Entity.MasterData.OrderLocationTransaction OrderLocationTransaction
		{
			get
			{
				return _orderLocationTransaction;
			}
			set
			{
				_orderLocationTransaction = value;
			}
		}
        //private com.Sconit.Entity.MasterData.Uom _uom;
        //public com.Sconit.Entity.MasterData.Uom Uom
        //{
        //    get
        //    {
        //        return _uom;
        //    }
        //    set
        //    {
        //        _uom = value;
        //    }
        //}
        private String _huId;
        public String HuId
        {
            get
            {
                return _huId;
            }
            set
            {
                _huId = value;
            }
        }
        private String _lotNo;
        public String LotNo
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
		private Decimal? _rejectedQty;
		public Decimal? RejectedQty
		{
			get
			{
				return _rejectedQty;
			}
			set
			{
				_rejectedQty = value;
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
        private Boolean _isConsignment;
        public Boolean IsConsignment
        {
            get
            {
                return _isConsignment;
            }
            set
            {
                _isConsignment = value;
            }
        }
        private Int32? _plannedBill;
        public Int32? PlannedBill
        {
            get
            {
                return _plannedBill;
            }
            set
            {
                _plannedBill = value;
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
            ReceiptDetailBase another = obj as ReceiptDetailBase;

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
