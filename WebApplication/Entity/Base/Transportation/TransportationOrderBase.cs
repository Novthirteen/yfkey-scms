using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportationOrderBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _orderNo;
		public string OrderNo
		{
			get
			{
				return _orderNo;
			}
			set
			{
				_orderNo = value;
			}
		}
		private com.Sconit.Entity.Transportation.TransportationRoute _transportationRoute;
		public com.Sconit.Entity.Transportation.TransportationRoute TransportationRoute
		{
			get
			{
				return _transportationRoute;
			}
			set
			{
				_transportationRoute = value;
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
		private DateTime? _startDate;
		public DateTime? StartDate
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
        private com.Sconit.Entity.MasterData.User _startUser;
        public com.Sconit.Entity.MasterData.User StartUser
		{
			get
			{
				return _startUser;
			}
			set
			{
				_startUser = value;
			}
		}
		private DateTime? _completeDate;
		public DateTime? CompleteDate
		{
			get
			{
				return _completeDate;
			}
			set
			{
				_completeDate = value;
			}
		}
        private com.Sconit.Entity.MasterData.User _completeUser;
        public com.Sconit.Entity.MasterData.User CompleteUser
		{
			get
			{
				return _completeUser;
			}
			set
			{
				_completeUser = value;
			}
		}
		private DateTime? _closeDate;
		public DateTime? CloseDate
		{
			get
			{
				return _closeDate;
			}
			set
			{
				_closeDate = value;
			}
		}
        private com.Sconit.Entity.MasterData.User _closeUser;
        public com.Sconit.Entity.MasterData.User CloseUser
		{
			get
			{
				return _closeUser;
			}
			set
			{
				_closeUser = value;
			}
		}
		private DateTime? _cancelDate;
		public DateTime? CancelDate
		{
			get
			{
				return _cancelDate;
			}
			set
			{
				_cancelDate = value;
			}
		}
        private com.Sconit.Entity.MasterData.User _cancelUser;
        public com.Sconit.Entity.MasterData.User CancelUser
		{
			get
			{
				return _cancelUser;
			}
			set
			{
				_cancelUser = value;
			}
		}
		private Boolean _isValuated;
		public Boolean IsValuated
		{
			get
			{
				return _isValuated;
			}
			set
			{
				_isValuated = value;
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
		private String _vehicle;
        public String Vehicle
		{
			get
			{
				return _vehicle;
			}
			set
			{
				_vehicle = value;
			}
		}
		private com.Sconit.Entity.Transportation.Carrier _carrier;
		public com.Sconit.Entity.Transportation.Carrier Carrier
		{
			get
			{
				return _carrier;
			}
			set
			{
				_carrier = value;
			}
		}
		private Int32 _pallentCount;
		public Int32 PallentCount
		{
			get
			{
				return _pallentCount;
			}
			set
			{
				_pallentCount = value;
			}
		}
		private Int32 _referencePallentCount;
		public Int32 ReferencePallentCount
		{
			get
			{
				return _referencePallentCount;
			}
			set
			{
				_referencePallentCount = value;
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
        private string _vehicleDriver;
        public string VehicleDriver
        {
            get
            {
                return _vehicleDriver;
            }
            set
            {
                _vehicleDriver = value;
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
        private com.Sconit.Entity.Transportation.Expense _expense;
        public com.Sconit.Entity.Transportation.Expense Expense
		{
			get
			{
				return _expense;
			}
			set
			{
				_expense = value;
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

        private IList<TransportationOrderDetail> _orderDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<TransportationOrderDetail> OrderDetails
        {
            get
            {
                return _orderDetails;
            }
            set
            {
                _orderDetails = value;
            }
        }

        private com.Sconit.Entity.MasterData.BillAddress _carrierBillAddress;
        public com.Sconit.Entity.MasterData.BillAddress CarrierBillAddress
        {
            get
            {
                return _carrierBillAddress;
            }
            set
            {
                _carrierBillAddress = value;
            }
        }

        private DateTime _transportMethod;
        public DateTime TransportMethod
        {
            get
            {
                return _transportMethod;
            }
            set
            {
                _transportMethod = value;
            }
        }
        #endregion

		public override int GetHashCode()
        {
			if (OrderNo != null)
            {
                return OrderNo.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            TransportationOrderBase another = obj as TransportationOrderBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.OrderNo == another.OrderNo);
            }
        } 
    }
	
}
