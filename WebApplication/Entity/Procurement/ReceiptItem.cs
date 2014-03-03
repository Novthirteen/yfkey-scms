using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Production;

namespace com.Sconit.Entity.Procurement
{
    [Serializable]
    public class ReceiptItem : EntityBase
    {
        private string _orderNo;
        public string OrderNo
        {
            get
            {
                return this._orderNo;
            }

            set
            {
                this._orderNo = value;
            }
        }

        private int _orderDetailId;
        public int OrderDetailId
        {
            get
            {
                return this._orderDetailId;
            }

            set
            {
                this._orderDetailId = value;
            }
        }

        private int _inOrderLocationTransactionId;
        public int InOrderLocationTransactionId
        {
            get
            {
                return this._inOrderLocationTransactionId;
            }

            set
            {
                this._inOrderLocationTransactionId = value;
            }
        }

        private string _itemCode;
        public string ItemCode
        {
            get
            {
                return this._itemCode;
            }

            set
            {
                this._itemCode = value;
            }
        }

        //已分配回冲的物料
        private IList<MaterialFlushBack> _materialFlushBack;
        public IList<MaterialFlushBack> MaterialFlushBack
        {
            get
            {
                return this._materialFlushBack;
            }

            set
            {
                this._materialFlushBack = value;
            }
        }

        public void AddMaterialFlushBack(MaterialFlushBack materialFlushBack)
        {
            if (this._materialFlushBack == null)
            {
                this._materialFlushBack = new List<MaterialFlushBack>();
            }

            this._materialFlushBack.Add(materialFlushBack);
        }

        private IList<InProcessLocationDetail> _inProcessLocationDetailList;
        public IList<InProcessLocationDetail> InProcessLocationDetailList
        {
            get
            {
                return this._inProcessLocationDetailList;
            }

            set
            {
                this._inProcessLocationDetailList = value;
            }
        }

        public void AddInProcessLocationDetail(InProcessLocationDetail inProcessLocationDetail)
        {
            if (this._inProcessLocationDetailList == null)
            {
                this._inProcessLocationDetailList = new List<InProcessLocationDetail>();
            }

            this._inProcessLocationDetailList.Add(inProcessLocationDetail);
        }

        private string _huId;
        public string HuId
        {
            get
            {
                return this._huId;
            }

            set
            {
                this._huId = value;
            }
        }

        private string _lotNo;
        public string LotNo
        {
            get
            {
                return this._lotNo;
            }

            set
            {
                this._lotNo = value;
            }
        }

        private decimal _receiveQty;
        public decimal ReceiveQty
        {
            get
            {
                return this._receiveQty;
            }

            set
            {
                this._receiveQty = value;
            }
        }

        private decimal _rejectQty;
        public decimal RejectQty
        {
            get
            {
                return this._rejectQty;
            }

            set
            {
                this._rejectQty = value;
            }
        }

        private decimal _scrapQty;
        public decimal ScrapQty
        {
            get
            {
                return this._scrapQty;
            }

            set
            {
                this._scrapQty = value;
            }
        }

        public decimal PlannedAmount{ get; set; }
    }
}
