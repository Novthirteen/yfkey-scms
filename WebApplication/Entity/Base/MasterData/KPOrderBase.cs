using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class KPOrderBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private Decimal _oRDER_ID;
		public Decimal ORDER_ID
		{
			get
			{
				return _oRDER_ID;
			}
			set
			{
				_oRDER_ID = value;
			}
		}
		private string _oRDER_TYPE_ID;
		public string ORDER_TYPE_ID
		{
			get
			{
				return _oRDER_TYPE_ID;
			}
			set
			{
				_oRDER_TYPE_ID = value;
			}
		}
		private string _oRDER_TYPE_NAME;
		public string ORDER_TYPE_NAME
		{
			get
			{
				return _oRDER_TYPE_NAME;
			}
			set
			{
				_oRDER_TYPE_NAME = value;
			}
		}
		private DateTime? _oRDER_PUB_DATE;
		public DateTime? ORDER_PUB_DATE
		{
			get
			{
				return _oRDER_PUB_DATE;
			}
			set
			{
				_oRDER_PUB_DATE = value;
			}
		}
		private string _oRDER_PRINT;
		public string ORDER_PRINT
		{
			get
			{
				return _oRDER_PRINT;
			}
			set
			{
				_oRDER_PRINT = value;
			}
		}
		private DateTime? _pRINT_MODIFY_DATE;
		public DateTime? PRINT_MODIFY_DATE
		{
			get
			{
				return _pRINT_MODIFY_DATE;
			}
			set
			{
				_pRINT_MODIFY_DATE = value;
			}
		}
		private string _qAD_ORDER_ID;
		public string QAD_ORDER_ID
		{
			get
			{
				return _qAD_ORDER_ID;
			}
			set
			{
				_qAD_ORDER_ID = value;
			}
		}
		private string _pARTY_FROM_ID;
		public string PARTY_FROM_ID
		{
			get
			{
				return _pARTY_FROM_ID;
			}
			set
			{
				_pARTY_FROM_ID = value;
			}
		}
		private string _pARTY_TO_ID;
		public string PARTY_TO_ID
		{
			get
			{
				return _pARTY_TO_ID;
			}
			set
			{
				_pARTY_TO_ID = value;
			}
		}
		private DateTime? _oRDER_DATE;
		public DateTime? ORDER_DATE
		{
			get
			{
				return _oRDER_DATE;
			}
			set
			{
				_oRDER_DATE = value;
			}
		}
		private string _sYS_CODE;
		public string SYS_CODE
		{
			get
			{
				return _sYS_CODE;
			}
			set
			{
				_sYS_CODE = value;
			}
		}
		private string _sHIPPER;
		public string SHIPPER
		{
			get
			{
				return _sHIPPER;
			}
			set
			{
				_sHIPPER = value;
			}
		}
		private DateTime? _oRDER_READ_DATE;
		public DateTime? ORDER_READ_DATE
		{
			get
			{
				return _oRDER_READ_DATE;
			}
			set
			{
				_oRDER_READ_DATE = value;
			}
		}
        private Int32 _invoiceCount;
        public Int32 InvoiceCount
        {
            get
            {
                return _invoiceCount;
            }
            set
            {
                _invoiceCount = value;
            }
        }
        private DateTime? _invoiceDate;
        public DateTime? InvoiceDate
        {
            get
            {
                return _invoiceDate;
            }
            set
            {
                _invoiceDate = value;
            }
        }
        private string _invoiceTax;
        public string InvoiceTax
        {
            get
            {
                return _invoiceTax;
            }
            set
            {
                _invoiceTax = value;
            }
        }
        private Decimal _invoiceAmount;
        public Decimal InvoiceAmount
        {
            get
            {
                return _invoiceAmount;
            }
            set
            {
                _invoiceAmount = value;
            }
        }

        private Decimal _invoiceAmountWithoutTax;
        public Decimal InvoiceAmountWithoutTax
        {
            get
            {
                return _invoiceAmountWithoutTax;
            }
            set
            {
                _invoiceAmountWithoutTax = value;
            }
        }

        private string _invoiceNumber;
        public string InvoiceNumber
        {
            get
            {
                return _invoiceNumber;
            }
            set
            {
                _invoiceNumber = value;
            }
        }
        private string _invoiceRemark;
        public string InvoiceRemark
        {
            get
            {
                return _invoiceRemark;
            }
            set
            {
                _invoiceRemark = value;
            }
        }
        private string _invoiceStatus;
        public string InvoiceStatus
        {
            get
            {
                return _invoiceStatus;
            }
            set
            {
                _invoiceStatus = value;
            }
        }

        private IList<KPItem> _kpItems;
        public IList<KPItem> KPItems
        {
            get
            {
                return _kpItems;
            }
            set
            {
                _kpItems = value;
            }
        }
        #endregion

		public override int GetHashCode()
        {
			if (ORDER_ID != null)
            {
                return ORDER_ID.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            KPOrderBase another = obj as KPOrderBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.ORDER_ID == another.ORDER_ID);
            }
        } 
    }
	
}
