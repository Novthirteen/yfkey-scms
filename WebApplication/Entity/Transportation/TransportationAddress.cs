using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public class TransportationAddress : TransportationAddressBase
    {
        #region Non O/R Mapping Properties

        public String FullAddress
        {
            get
            {
                return this.Country + this.Province + this.City + this.District + this.Address;
            }
        }

        public String FullAddressAndId
        {
            get
            {
                return this.FullAddress + "[" + this.Id.ToString() + "]";
            }
        }

        public String Empty
        {
            get
            {
                return "";
            }
        }
        #endregion
    }
}