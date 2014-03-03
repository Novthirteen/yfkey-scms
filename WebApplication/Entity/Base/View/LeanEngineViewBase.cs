using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class LeanEngineViewBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private Int32 _flowDetId;
		public Int32 FlowDetId
		{
			get
			{
				return _flowDetId;
			}
			set
			{
				_flowDetId = value;
			}
		}
		private string _flow;
		public string Flow
		{
			get
			{
				return _flow;
			}
			set
			{
				_flow = value;
			}
        }
        private Boolean _isAutoCreate;
        public Boolean IsAutoCreate
        {
            get
            {
                return _isAutoCreate;
            }
            set
            {
                _isAutoCreate = value;
            }
        }
		private string _locFrom;
		public string LocFrom
		{
			get
			{
				return _locFrom;
			}
			set
			{
				_locFrom = value;
			}
		}
		private string _locTo;
		public string LocTo
		{
			get
			{
				return _locTo;
			}
			set
			{
				_locTo = value;
			}
		}
		private string _item;
		public string Item
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
		private string _uom;
		public string Uom
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
		private Decimal _uC;
		public Decimal UC
		{
			get
			{
				return _uC;
			}
			set
			{
				_uC = value;
			}
		}
		private Int32? _huLotSize;
		public Int32? HuLotSize
		{
			get
			{
				return _huLotSize;
			}
			set
			{
				_huLotSize = value;
			}
		}
		private string _bom;
		public string Bom
		{
			get
			{
				return _bom;
			}
			set
			{
				_bom = value;
			}
		}
		private Decimal? _safeStock;
		public Decimal? SafeStock
		{
			get
			{
				return _safeStock;
			}
			set
			{
				_safeStock = value;
			}
		}
		private Decimal? _maxStock;
		public Decimal? MaxStock
		{
			get
			{
				return _maxStock;
			}
			set
			{
				_maxStock = value;
			}
		}
		private Decimal? _minLotSize;
		public Decimal? MinLotSize
		{
			get
			{
				return _minLotSize;
			}
			set
			{
				_minLotSize = value;
			}
		}
		private Decimal? _orderLotSize;
		public Decimal? OrderLotSize
		{
			get
			{
				return _orderLotSize;
			}
			set
			{
				_orderLotSize = value;
			}
		}
		private Decimal? _batchSize;
		public Decimal? BatchSize
		{
			get
			{
				return _batchSize;
			}
			set
			{
				_batchSize = value;
			}
		}
		private string _roundUpOpt;
		public string RoundUpOpt
		{
			get
			{
				return _roundUpOpt;
			}
			set
			{
				_roundUpOpt = value;
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
		private string _partyFrom;
		public string PartyFrom
		{
			get
			{
				return _partyFrom;
			}
			set
			{
				_partyFrom = value;
			}
		}
		private string _partyTo;
		public string PartyTo
		{
			get
			{
				return _partyTo;
			}
			set
			{
				_partyTo = value;
			}
		}
		private string _flowStrategy;
		public string FlowStrategy
		{
			get
			{
				return _flowStrategy;
			}
			set
			{
				_flowStrategy = value;
			}
		}
		private Decimal? _leadTime;
		public Decimal? LeadTime
		{
			get
			{
				return _leadTime;
			}
			set
			{
				_leadTime = value;
			}
		}
		private Decimal? _emTime;
		public Decimal? EmTime
		{
			get
			{
				return _emTime;
			}
			set
			{
				_emTime = value;
			}
		}
		private Decimal? _maxCirTime;
		public Decimal? MaxCirTime
		{
			get
			{
				return _maxCirTime;
			}
			set
			{
				_maxCirTime = value;
			}
		}
		private string _winTime1;
		public string WinTime1
		{
			get
			{
				return _winTime1;
			}
			set
			{
				_winTime1 = value;
			}
		}
		private string _winTime2;
		public string WinTime2
		{
			get
			{
				return _winTime2;
			}
			set
			{
				_winTime2 = value;
			}
		}
		private string _winTime3;
		public string WinTime3
		{
			get
			{
				return _winTime3;
			}
			set
			{
				_winTime3 = value;
			}
		}
		private string _winTime4;
		public string WinTime4
		{
			get
			{
				return _winTime4;
			}
			set
			{
				_winTime4 = value;
			}
		}
		private string _winTime5;
		public string WinTime5
		{
			get
			{
				return _winTime5;
			}
			set
			{
				_winTime5 = value;
			}
		}
		private string _winTime6;
		public string WinTime6
		{
			get
			{
				return _winTime6;
			}
			set
			{
				_winTime6 = value;
			}
		}
		private string _winTime7;
		public string WinTime7
		{
			get
			{
				return _winTime7;
			}
			set
			{
				_winTime7 = value;
			}
		}
		private DateTime? _nextOrderTime;
		public DateTime? NextOrderTime
		{
			get
			{
				return _nextOrderTime;
			}
			set
			{
				_nextOrderTime = value;
			}
		}
		private DateTime? _nextWinTime;
		public DateTime? NextWinTime
		{
			get
			{
				return _nextWinTime;
			}
			set
			{
				_nextWinTime = value;
			}
		}
		private Int32? _weekInterval;
		public Int32? WeekInterval
		{
			get
			{
				return _weekInterval;
			}
			set
			{
				_weekInterval = value;
			}
		}
        #endregion

		public override int GetHashCode()
        {
			if (FlowDetId != null)
            {
                return FlowDetId.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            LeanEngineViewBase another = obj as LeanEngineViewBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.FlowDetId == another.FlowDetId);
            }
        } 
    }
	
}
