using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class KPItemBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _pART_CODE;
		public string PART_CODE
		{
			get
			{
				return _pART_CODE;
			}
			set
			{
				_pART_CODE = value;
			}
		}
		private string _pURCHASE_ORDER_ID;
		public string PURCHASE_ORDER_ID
		{
			get
			{
				return _pURCHASE_ORDER_ID;
			}
			set
			{
				_pURCHASE_ORDER_ID = value;
			}
		}
		private string _iNCOMING_ORDER_ID;
		public string INCOMING_ORDER_ID
		{
			get
			{
				return _iNCOMING_ORDER_ID;
			}
			set
			{
				_iNCOMING_ORDER_ID = value;
			}
		}
		private string _sEQ_ID;
		public string SEQ_ID
		{
			get
			{
				return _sEQ_ID;
			}
			set
			{
				_sEQ_ID = value;
			}
		}
		private Double? _iNCOMING_QTY;
		public Double? INCOMING_QTY
		{
			get
			{
				return _iNCOMING_QTY;
			}
			set
			{
				_iNCOMING_QTY = value;
			}
		}
		private Double? _pRICE;
		public Double? PRICE
		{
			get
			{
				return _pRICE;
			}
			set
			{
				_pRICE = value;
			}
		}
		private string _uM;
		public string UM
		{
			get
			{
				return _uM;
			}
			set
			{
				_uM = value;
			}
		}
		private Double? _pRICE1;
		public Double? PRICE1
		{
			get
			{
				return _pRICE1;
			}
			set
			{
				_pRICE1 = value;
			}
		}
		private Decimal? _pRICE2;
		public Decimal? PRICE2
		{
			get
			{
				return _pRICE2;
			}
			set
			{
				_pRICE2 = value;
			}
		}
		private string _pART_NAME;
		public string PART_NAME
		{
			get
			{
				return _pART_NAME;
			}
			set
			{
				_pART_NAME = value;
			}
		}
		private string _dELIVER_ORDER_ID;
		public string DELIVER_ORDER_ID
		{
			get
			{
				return _dELIVER_ORDER_ID;
			}
			set
			{
				_dELIVER_ORDER_ID = value;
			}
		}
		private DateTime? _iNCOMING_DATE;
		public DateTime? INCOMING_DATE
		{
			get
			{
				return _iNCOMING_DATE;
			}
			set
			{
				_iNCOMING_DATE = value;
			}
		}
		private string _lOCATION_ID;
		public string LOCATION_ID
		{
			get
			{
				return _lOCATION_ID;
			}
			set
			{
				_lOCATION_ID = value;
			}
		}
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
		private Decimal _iTEM_SEQ_ID;
		public Decimal ITEM_SEQ_ID
		{
			get
			{
				return _iTEM_SEQ_ID;
			}
			set
			{
				_iTEM_SEQ_ID = value;
			}
		}
        
        #endregion

		public override int GetHashCode()
        {
			if (ITEM_SEQ_ID != null)
            {
                return ITEM_SEQ_ID.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            KPItemBase another = obj as KPItemBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.ITEM_SEQ_ID == another.ITEM_SEQ_ID);
            }
        } 
    }
	
}
