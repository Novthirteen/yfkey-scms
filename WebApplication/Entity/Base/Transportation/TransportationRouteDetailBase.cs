using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportationRouteDetailBase : EntityBase
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
		private Int32 _sequence;
		public Int32 Sequence
		{
			get
			{
				return _sequence;
			}
			set
			{
				_sequence = value;
			}
		}
        private com.Sconit.Entity.Transportation.TransportationAddress _transportationAddress;
        public com.Sconit.Entity.Transportation.TransportationAddress TransportationAddress
		{
			get
			{
				return _transportationAddress;
			}
			set
			{
				_transportationAddress = value;
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
            TransportationRouteDetailBase another = obj as TransportationRouteDetailBase;

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
