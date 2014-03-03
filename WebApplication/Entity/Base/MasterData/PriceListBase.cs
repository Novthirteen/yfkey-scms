using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class PriceListBase : EntityBase
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
        private com.Sconit.Entity.MasterData.Party _party;
        public com.Sconit.Entity.MasterData.Party Party
        {
            get
            {
                return _party;
            }
            set
            {
                _party = value;
            }
        }
        private IList<PriceListDetail> _priceListDetails;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<PriceListDetail> PriceListDetails
        {
            get
            {
                return _priceListDetails;
            }
            set
            {
                _priceListDetails = value;
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
            PriceListBase another = obj as PriceListBase;

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
