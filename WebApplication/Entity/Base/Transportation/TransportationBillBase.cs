using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportationBillBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _billNo;
		public string BillNo
		{
			get
			{
				return _billNo;
			}
			set
			{
				_billNo = value;
			}
		}
		private string _externalBillNo;
		public string ExternalBillNo
		{
			get
			{
				return _externalBillNo;
			}
			set
			{
				_externalBillNo = value;
			}
		}
		private string _referenceBillNo;
		public string ReferenceBillNo
		{
			get
			{
				return _referenceBillNo;
			}
			set
			{
				_referenceBillNo = value;
			}
		}
		private string _status;
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
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
		private com.Sconit.Entity.MasterData.Currency _currency;
		public com.Sconit.Entity.MasterData.Currency Currency
		{
			get
			{
				return _currency;
			}
			set
			{
				_currency = value;
			}
		}
		private Boolean _isIncludeTax;
		public Boolean IsIncludeTax
		{
			get
			{
				return _isIncludeTax;
			}
			set
			{
				_isIncludeTax = value;
			}
		}
		private string _taxCode;
		public string TaxCode
		{
			get
			{
				return _taxCode;
			}
			set
			{
				_taxCode = value;
			}
		}
		private Decimal? _discount;
		public Decimal? Discount
		{
			get
			{
				return _discount;
			}
			set
			{
				_discount = value;
			}
		}
		private DateTime _createDate;
		public DateTime CreateDate
		{
			get
			{
				return _createDate;
			}
			set
			{
				_createDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _createUser;
		public com.Sconit.Entity.MasterData.User CreateUser
		{
			get
			{
				return _createUser;
			}
			set
			{
				_createUser = value;
			}
		}
		private DateTime _lastModifyDate;
		public DateTime LastModifyDate
		{
			get
			{
				return _lastModifyDate;
			}
			set
			{
				_lastModifyDate = value;
			}
		}
		private com.Sconit.Entity.MasterData.User _lastModifyUser;
		public com.Sconit.Entity.MasterData.User LastModifyUser
		{
			get
			{
				return _lastModifyUser;
			}
			set
			{
				_lastModifyUser = value;
			}
		}
		private string _billType;
		public string BillType
		{
			get
			{
				return _billType;
			}
			set
			{
				_billType = value;
			}
        }
        private IList<TransportationBillDetail> _transportationDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<TransportationBillDetail> TransportationBillDetails
        {
            get
            {
                return _transportationDetails;
            }
            set
            {
                _transportationDetails = value;
            }
        }
        
        #endregion

		public override int GetHashCode()
        {
			if (BillNo != null)
            {
                return BillNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            TransportationBillBase another = obj as TransportationBillBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.BillNo == another.BillNo);
            }
        } 
    }
	
}
