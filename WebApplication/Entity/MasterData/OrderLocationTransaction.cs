using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class OrderLocationTransaction : OrderLocationTransactionBase
    {
        #region Non O/R Mapping Properties

        private decimal _currentShipQty;
        public decimal CurrentShipQty
        {
            get
            {
                return this._currentShipQty;
            }
            set
            {
                this._currentShipQty = value;
            }
        }

        private decimal _currentReceiveQty;
        public decimal CurrentReceiveQty
        {
            get
            {
                return this._currentReceiveQty;
            }
            set
            {
                this._currentReceiveQty = value;
            }
        }

        private decimal _currentRejectQty;
        public decimal CurrentRejectQty
        {
            get
            {
                return this._currentRejectQty;
            }
            set
            {
                this._currentRejectQty = value;
            }
        }

        private decimal _currentScrapQty;
        public decimal CurrentScrapQty
        {
            get
            {
                return this._currentScrapQty;
            }
            set
            {
                this._currentScrapQty = value;
            }
        }

        //剩余需求量/欠交量
        public decimal RemainQty
        {
            get
            {
                decimal accumulateQty = this.AccumulateQty == null ? 0 : (decimal)this.AccumulateQty;
                return (this.OrderedQty - accumulateQty) > 0 ? (this.OrderedQty - accumulateQty) : 0;
            }
        }

        //发货的时候扫描huID无法绑定，此字段用来暂存
        private string _huId;
        public string HuId
        {
            get
            {
                return _huId;
            }
            set
            {
                this._huId = value;
            }
        }
        private decimal? _huQty;
        public decimal? HuQty
        {
            get
            {
                return _huQty;
            }
            set
            {
                this._huQty = value;
            }
        }
        private string _lotNo;
        public string LotNo
        {
            get
            {
                return _lotNo;
            }
            set
            {
                this._lotNo = value;
            }
        }

        //是否新加明细
        private bool _isBlank = false;
        public bool IsBlank
        {
            get
            {
                return _isBlank;
            }
            set
            {
                this._isBlank = value;
            }
        }
        #endregion
    }
}