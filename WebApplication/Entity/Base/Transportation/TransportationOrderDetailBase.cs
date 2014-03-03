using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public abstract class TransportationOrderDetailBase : EntityBase
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
        private com.Sconit.Entity.Transportation.TransportationOrder _transportationOrder;
        public com.Sconit.Entity.Transportation.TransportationOrder TransportationOrder
		{
			get
			{
                return _transportationOrder;
			}
			set
			{
                _transportationOrder = value;
			}
		}
        private com.Sconit.Entity.Distribution.InProcessLocation _inProcessLocation;
        public com.Sconit.Entity.Distribution.InProcessLocation InProcessLocation
		{
			get
			{
                return _inProcessLocation;
			}
			set
			{
                _inProcessLocation = value;
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
            TransportationOrderDetailBase another = obj as TransportationOrderDetailBase;

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
