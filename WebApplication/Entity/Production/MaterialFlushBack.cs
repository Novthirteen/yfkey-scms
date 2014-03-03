using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Distribution;

namespace com.Sconit.Entity.Production
{
    [Serializable]
    public class MaterialFlushBack : EntityBase
    {
        #region Non O/R Mapping Properties

        private OrderDetail _orderDetail;
        public OrderDetail OrderDetail
        {
            get
            {
                return this._orderDetail;
            }
            set
            {
                this._orderDetail = value;
            }
        }

        private Item _rawMaterial;
        public Item RawMaterial
        {
            get
            {
                return this._rawMaterial;
            }
            set
            {
                this._rawMaterial = value;
            }
        }

        private Uom _uom;
        public Uom Uom
        {
            get
            {
                return this._uom;
            }
            set
            {
                this._uom = value;
            }
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

        private Int32 _operation;
        public Int32 Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }

        private decimal _qty;
        public decimal Qty
        {
            get
            {
                return this._qty;
            }
            set
            {
                this._qty = value;
            }
        }

        private OrderLocationTransaction _orderLocationTransaction;
        public OrderLocationTransaction OrderLocationTransaction
        {
            get
            {
                return this._orderLocationTransaction;
            }
            set
            {
                this._orderLocationTransaction = value;
            }
        }

        private InProcessLocationDetail _inProcessLocationDetail;
        public InProcessLocationDetail InProcessLocationDetail
        {
            get
            {
                return this._inProcessLocationDetail;
            }
            set
            {
                this._inProcessLocationDetail = value;
            }
        }

        #endregion
    }
}
