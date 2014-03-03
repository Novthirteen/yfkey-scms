using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Distribution
{
    [Serializable]
    public class InProcessLocationDetail : InProcessLocationDetailBase
    {
        #region Non O/R Mapping Properties

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

        private string _currentReceiveHuId;
        public string CurrentReceiveHuId
        {
            get
            {
                return this._currentReceiveHuId;
            }
            set
            {
                this._currentReceiveHuId = value;
            }
        }

        private string _currentReceiveLotNo;
        public string CurrentReceiveLotNo
        {
            get
            {
                return this._currentReceiveLotNo;
            }
            set
            {
                this._currentReceiveLotNo = value;
            }
        }

        //¼Æ»®´ý·¢
        private decimal _qtyToShip;
        public decimal QtyToShip
        {
            get
            {
                decimal shippedQty = OrderLocationTransaction.OrderDetail.ShippedQty.HasValue ? (decimal)OrderLocationTransaction.OrderDetail.ShippedQty : 0;
                return OrderLocationTransaction.OrderDetail.OrderedQty > shippedQty ? OrderLocationTransaction.OrderDetail.OrderedQty - shippedQty : 0;
            }
            set
            {
                this._qtyToShip = value;
            }
        }

        private decimal _shippedQty;
        public decimal ShippedQty
        {
            get
            {
                return _shippedQty;
            }
            set
            {
                this._shippedQty = value;
            }
        }

        private IList<InProcessLocationDetail> _huInProcessLocationDetails;
        public IList<InProcessLocationDetail> HuInProcessLocationDetails
        {
            get
            {
                return _huInProcessLocationDetails;
            }
            set
            {
                this._huInProcessLocationDetails = value;
            }
        }
        public void AddHuInProcessLocationDetails(InProcessLocationDetail inProcessLocationDetail)
        {
            if (this.HuInProcessLocationDetails == null)
            {
                this.HuInProcessLocationDetails = new List<InProcessLocationDetail>();
            }

            this.HuInProcessLocationDetails.Add(inProcessLocationDetail);
        }

        public string ReturnPutAwaySorageBinCode { get; set; }

        public string LocationCode { get; set; }
        public string ItemCode { get; set; }
        #endregion
    }
}