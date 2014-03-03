using System;
using com.Sconit.Entity.Distribution;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class LocationDetail : LocationDetailBase
    {
        #region Non O/R Mapping Properties

        //收在途
        private decimal _inTransitQty;
        public decimal InTransitQty
        {
            get
            {
                return this._inTransitQty;
            }

            set
            {
                this._inTransitQty = value;
            }
        }

        //发在途
        private decimal _inTransitQtyOut;
        public decimal InTransitQtyOut
        {
            get
            {
                return this._inTransitQtyOut;
            }

            set
            {
                this._inTransitQtyOut = value;
            }
        }

        //待收
        private decimal _qtyToBeIn;
        public decimal QtyToBeIn
        {
            get
            {
                return this._qtyToBeIn;
            }

            set
            {
                this._qtyToBeIn = value;
            }
        }

        //待发
        private decimal _qtyToBeOut;
        public decimal QtyToBeOut
        {
            get
            {
                return this._qtyToBeOut;
            }

            set
            {
                this._qtyToBeOut = value;
            }
        }

        private decimal _invQty;
        public decimal InvQty
        {
            get
            {
                return this._invQty;
            }

            set
            {
                this._invQty = value;
            }
        }

        //预计可用库存
        private decimal _pAB;
        public decimal PAB
        {
            get
            {
                return this._pAB;
            }

            set
            {
                this._pAB = value;
            }
        }

        //期初库存
        private decimal _startInvQty;
        public decimal StartInvQty
        {
            get
            {
                return this._startInvQty;
            }

            set
            {
                this._startInvQty = value;
            }
        }

        //总计入库
        private decimal _totalInQty;
        public decimal TotalInQty
        {
            get
            {
                return this._totalInQty;
            }

            set
            {
                this._totalInQty = value;
            }
        }

        //总计出库
        private decimal _totalOutQty;
        public decimal TotalOutQty
        {
            get
            {
                return this._totalOutQty;
            }

            set
            {
                this._totalOutQty = value;
            }
        }

        //采购入库
        private decimal _RCTPO;
        public decimal RCTPO
        {
            get
            {
                return this._RCTPO;
            }

            set
            {
                this._RCTPO = value;
            }
        }

        //移库入库
        private decimal _RCTTR;
        public decimal RCTTR
        {
            get
            {
                return this._RCTTR;
            }

            set
            {
                this._RCTTR = value;
            }
        }

        //生产入库
        private decimal _RCTWO;
        public decimal RCTWO
        {
            get
            {
                return this._RCTWO;
            }

            set
            {
                this._RCTWO = value;
            }
        }

        //计划外入库
        private decimal _RCTUNP;
        public decimal RCTUNP
        {
            get
            {
                return this._RCTUNP;
            }

            set
            {
                this._RCTUNP = value;
            }
        }

        //检验入库
        private decimal _RCTINP;
        public decimal RCTINP
        {
            get
            {
                return this._RCTINP;
            }

            set
            {
                this._RCTINP = value;
            }
        }

        //销售出库
        private decimal _ISSSO;
        public decimal ISSSO
        {
            get
            {
                return this._ISSSO;
            }

            set
            {
                this._ISSSO = value;
            }
        }

        //移库出库
        private decimal _ISSTR;
        public decimal ISSTR
        {
            get
            {
                return this._ISSTR;
            }

            set
            {
                this._ISSTR = value;
            }
        }

        //生产消耗
        private decimal _ISSWO;
        public decimal ISSWO
        {
            get
            {
                return this._ISSWO;
            }

            set
            {
                this._ISSWO = value;
            }
        }

        //计划外出库
        private decimal _ISSUNP;
        public decimal ISSUNP
        {
            get
            {
                return this._ISSUNP;
            }

            set
            {
                this._ISSUNP = value;
            }
        }

        //检验出库
        private decimal _ISSINP;
        public decimal ISSINP
        {
            get
            {
                return this._ISSINP;
            }

            set
            {
                this._ISSINP = value;
            }
        }

        //盘点差异
        private decimal _CYCCNT;
        public decimal CYCCNT
        {
            get
            {
                return this._CYCCNT;
            }

            set
            {
                this._CYCCNT = value;
            }
        }

        //FlowDetail
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

        public Region Region { get; set; }

        //周转率
        public decimal? InvTurnRate
        {
            get
            {
                decimal avgInvQty = (this.StartInvQty + this.InvQty) / 2;
                if (avgInvQty != 0)
                {
                    return (-this.TotalOutQty) / avgInvQty;
                }
                else
                {
                    return null;
                }
            }
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 已拣数
        /// </summary>
        public decimal PickedQty { get; set; }

        /// <summary>
        /// 未统计
        /// </summary>
        public decimal NoStatsQty { get; set; }

        #endregion
    }
}