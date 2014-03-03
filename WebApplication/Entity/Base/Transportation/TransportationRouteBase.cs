using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportationRouteBase : EntityBase
    {
        #region O/R Mapping Properties
		
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
		private Boolean _isActive;
		public Boolean IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				_isActive = value;
			}
		}
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
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

        private IList<TransportationRouteDetail> _transportationRouteDetails;
        public IList<TransportationRouteDetail> TransportationRouteDetails
        {
            get
            {
                return _transportationRouteDetails;
            }
            set
            {
                _transportationRouteDetails = value;
            }
        }
        
        #endregion

		public override int GetHashCode()
        {
			if (Code != null)
            {
                return Code.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            TransportationRouteBase another = obj as TransportationRouteBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Code == another.Code);
            }
        } 
    }
	
}
