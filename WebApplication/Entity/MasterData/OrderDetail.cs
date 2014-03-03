using System;
using System.Collections.Generic;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Production;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class OrderDetail : OrderDetailBase
    {
        #region Non O/R Mapping Properties

        public void AddOrderLocationTransaction(OrderLocationTransaction orderLocationTransaction)
        {
            if (this.OrderLocationTransactions == null)
            {
                this.OrderLocationTransactions = new List<OrderLocationTransaction>();
            }

            this.OrderLocationTransactions.Add(orderLocationTransaction);
        }

        public void RemoveOrderLocationTransaction(OrderLocationTransaction orderLocationTransaction)
        {
            if (this.OrderLocationTransactions != null)
            {
                this.OrderLocationTransactions.Remove(orderLocationTransaction);
            }
        }

        public void RemoveOrderLocationTransaction(int position)
        {
            if (this.OrderLocationTransactions != null)
            {
                this.OrderLocationTransactions.RemoveAt(position);
            }
        }

        public Location DefaultLocationFrom
        {
            get
            {
                if (this.LocationFrom == null)
                {
                    if (this.OrderHead != null)
                    {
                        return this.OrderHead.LocationFrom;
                    }
                    return null;
                }
                else
                {
                    return this.LocationFrom;
                }
            }
        }

        public Location DefaultLocationTo
        {
            get
            {
                if (this.LocationTo == null)
                {
                    if (this.OrderHead != null)
                    {
                        return this.OrderHead.LocationTo;
                    }
                    return null;
                }
                else
                {
                    return this.LocationTo;
                }
            }
        }

        public BillAddress DefaultBillFrom
        {
            get
            {
                if (this.BillFrom == null)
                {
                    return this.OrderHead.BillFrom;
                }
                else
                {
                    return this.BillFrom;
                }
            }
        }

        public BillAddress DefaultBillTo
        {
            get
            {
                if (this.BillTo == null)
                {
                    return this.OrderHead.BillTo;
                }
                else
                {
                    return this.BillTo;
                }
            }
        }

        public PurchasePriceList DefaultPriceListFrom
        {
            get
            {
                if (this.PriceListFrom == null)
                {
                    return this.OrderHead.PriceListFrom;
                }
                else
                {
                    return this.PriceListFrom;
                }
            }
        }

        public SalesPriceList DefaultPriceListTo
        {
            get
            {
                if (this.PriceListTo == null)
                {
                    return this.OrderHead.PriceListTo;
                }
                else
                {
                    return this.PriceListTo;
                }
            }
        }

        public string DefaultBillSettleTerm
        {
            get
            {
                if (this.BillSettleTerm == null)
                {
                    return this.OrderHead.BillSettleTerm;
                }
                else
                {
                    return this.BillSettleTerm;
                }
            }
        }

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

        private FlowDetail _flowDetail;
        public FlowDetail FlowDetail
        {
            get
            {
                return this._flowDetail;
            }
            set
            {
                this._flowDetail = value;
            }
        }

        private IList<AutoOrderTrack> _autoOrderTracks;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<AutoOrderTrack> AutoOrderTracks
        {
            get
            {
                return _autoOrderTracks;
            }
            set
            {
                _autoOrderTracks = value;
            }
        }

        public void AddAutoOrderTrack(AutoOrderTrack autoOrderTrack)
        {
            if (this.AutoOrderTracks == null)
            {
                this.AutoOrderTracks = new List<AutoOrderTrack>();
            }

            this.AutoOrderTracks.Add(autoOrderTrack);
        }

        public void AddRangeAutoOrderTrack(IList<AutoOrderTrack> autoOrderTrackList)
        {
            foreach (AutoOrderTrack autoOrderTrack in autoOrderTrackList)
            {
                this.AddAutoOrderTrack(autoOrderTrack);
            }
        }

        public decimal RemainQty
        {
            get
            {
                decimal shippedQty = this.ShippedQty == null ? 0 : (decimal)this.ShippedQty;
                return (this.OrderedQty - shippedQty) > 0 ? (this.OrderedQty - shippedQty) : 0;
            }
        }

        //辅助字段，判断该Detail是否新加的空行
        private Boolean _isBlankDetail = false;
        public Boolean IsBlankDetail
        {
            get
            {
                return _isBlankDetail;
            }
            set
            {
                _isBlankDetail = value;
            }
        }

        //折扣率
        private decimal _discountRateFrom;
        public decimal DiscountRateFrom
        {
            get
            {
                decimal unitPriceFrom = 0;
                if (this.PriceListDetailFrom != null)
                {
                    unitPriceFrom = this.PriceListDetailFrom.UnitPrice;
                }
                decimal discount = this.DiscountFrom == null ? 0 : (decimal)this.DiscountFrom;
                if (this.OrderedQty == 0 || unitPriceFrom == 0)
                {
                    _discountRateFrom = 0;
                }
                else
                {
                    _discountRateFrom = 100 * discount / (this.OrderedQty * unitPriceFrom);
                }
                return this._discountRateFrom;
            }

        }

        private decimal _discountRateTo;
        public decimal DiscountRateTo
        {
            get
            {
                decimal unitPriceTo = 0;
                if (this.PriceListDetailTo != null)
                {
                    unitPriceTo = this.PriceListDetailTo.UnitPrice;
                }
                decimal discount = this.DiscountTo == null ? 0 : (decimal)this.DiscountTo;
                if (this.OrderedQty == 0 || unitPriceTo == 0)
                {
                    _discountRateTo = 0;
                }
                else
                {
                    _discountRateTo = 100 * discount / (this.OrderedQty * unitPriceTo);
                }
                return this._discountRateTo;
            }

        }

        //辅助字段，计算订单明细折扣后的采购价格，不包含头折扣
        public decimal AmountFrom
        {
            get
            {
                decimal unitPriceFrom = 0;
                if (this.PriceListDetailFrom != null)
                {
                    unitPriceFrom = this.PriceListDetailFrom.UnitPrice;
                }
                decimal discountFrom = this.DiscountFrom.HasValue ? this.DiscountFrom.Value : 0;
                return unitPriceFrom * this.OrderedQty - discountFrom;
            }
        }

        //辅助字段，计算订单明细折扣后的销售价格，不包含头折扣
        public decimal AmountTo
        {
            get
            {
                decimal unitPriceTo = 0;
                if (this.PriceListDetailTo != null)
                {
                    unitPriceTo = this.PriceListDetailTo.UnitPrice;
                }
                decimal discountTo = this.DiscountTo.HasValue ? this.DiscountTo.Value : 0;
                return unitPriceTo * this.OrderedQty - discountTo;
            }
        }

        //辅助字段，HuId
        private string _huId;
        public string HuId
        {
            get
            {
                return _huId;
            }
            set
            {
                _huId = value;
            }
        }

        //辅助字段，Hu数量
        private decimal _huQty;
        public decimal HuQty
        {
            get
            {
                return _huQty;
            }
            set
            {
                _huQty = value;
            }
        }
        //辅助字段，是否被扫描
        private Boolean _isScanHu;
        public Boolean IsScanHu
        {
            get
            {
                return _isScanHu;
            }
            set
            {
                _isScanHu = value;
            }
        }

        public decimal RemainShippedQty
        {
            get
            {
                decimal shippedQty = this.ShippedQty.HasValue ? this.ShippedQty.Value : 0;
                return this.OrderedQty > shippedQty ? this.OrderedQty - shippedQty : 0;
            }
        }
        public decimal InTransitQty
        {
            get
            {
                decimal shippedQty = this.ShippedQty.HasValue ? this.ShippedQty.Value : 0;
                decimal receivedQty = this.ReceivedQty.HasValue ? this.ReceivedQty.Value : 0;
                return shippedQty > receivedQty ? shippedQty - receivedQty : 0;
            }
        }
        public decimal RemainReceivedQty
        {
            get
            {
                decimal receivedQty = this.ReceivedQty.HasValue ? this.ReceivedQty.Value : 0;
                return this.OrderedQty > receivedQty ? this.OrderedQty - receivedQty : 0;
            }
        }
        public decimal WrapSize
        {
            get
            {
                if (this.HuLotSize.HasValue && this.HuLotSize.Value != 0)
                {
                    return this.HuLotSize.Value;
                }
                else
                {
                    return this.UnitCount;
                }
            }
        }

        public string PutAwayBinCode { get; set; }
        public string HuLotNo { get; set; }

        public List<OrderTracer> OrderTracers { get; set; }
        #endregion
    }
}