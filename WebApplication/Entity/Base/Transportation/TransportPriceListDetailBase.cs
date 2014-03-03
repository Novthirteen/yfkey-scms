using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportPriceListDetailBase : EntityBase
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
        private com.Sconit.Entity.Transportation.TransportPriceList _transportPriceList;
        public com.Sconit.Entity.Transportation.TransportPriceList TransportPriceList
		{
			get
			{
                return _transportPriceList;
			}
			set
			{
                _transportPriceList = value;
			}
		}
		private DateTime _startDate;
		public DateTime StartDate
		{
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
			}
		}
		private DateTime? _endDate;
        public DateTime? EndDate
		{
			get
			{
                return _endDate;
			}
			set
			{
                _endDate = value;
			}
		}
        private com.Sconit.Entity.Transportation.TransportationAddress _shipFrom;
        public com.Sconit.Entity.Transportation.TransportationAddress ShipFrom
		{
			get
			{
				return _shipFrom;
			}
			set
			{
				_shipFrom = value;
			}
		}
        private com.Sconit.Entity.Transportation.TransportationAddress _shipTo;
        public com.Sconit.Entity.Transportation.TransportationAddress ShipTo
		{
			get
			{
				return _shipTo;
			}
			set
			{
				_shipTo = value;
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
		private Decimal _unitPrice;
		public Decimal UnitPrice
		{
			get
			{
				return _unitPrice;
			}
			set
			{
				_unitPrice = value;
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
        private Boolean _isProvisionalEstimate;
        public Boolean IsProvisionalEstimate
		{
			get
			{
                return _isProvisionalEstimate;
			}
			set
			{
                _isProvisionalEstimate = value;
			}
		}
		private string _pricingMethod;
		public string PricingMethod
		{
			get
			{
				return _pricingMethod;
			}
			set
			{
				_pricingMethod = value;
			}
		}
        private string _billingMethod;
        public string BillingMethod
        {
            get
            {
                return _billingMethod;
            }
            set
            {
                _billingMethod = value;
            }
        }
		private string _vehicleType;
		public string VehicleType
		{
			get
			{
				return _vehicleType;
			}
			set
			{
				_vehicleType = value;
			}
		}
		private Decimal _minVolume;
		public Decimal MinVolume
		{
			get
			{
				return _minVolume;
			}
			set
			{
				_minVolume = value;
			}
		}
		private Decimal _serviceCharge;
		public Decimal ServiceCharge
		{
			get
			{
				return _serviceCharge;
			}
			set
			{
				_serviceCharge = value;
			}
		}
        private string _roundUpOption;
        public string RoundUpOption
		{
			get
			{
                return _roundUpOption;
			}
			set
			{
                _roundUpOption = value;
			}
		}
		private string _type;
		public string Type
		{
			get
			{
				return _type;
			}
			set
			{
				_type = value;
			}
		}
		private string _remark;
		public string Remark
		{
			get
			{
				return _remark;
			}
			set
			{
				_remark = value;
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
            TransportPriceListDetailBase another = obj as TransportPriceListDetailBase;

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
