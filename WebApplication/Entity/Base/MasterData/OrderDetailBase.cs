using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class OrderDetailBase : EntityBase
    {
        #region O/R Mapping Properties

        private Int32 _id;
        public Int32 Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private com.Sconit.Entity.MasterData.OrderHead _orderHead;
        public com.Sconit.Entity.MasterData.OrderHead OrderHead
        {
            get
            {
                return _orderHead;
            }
            set
            {
                _orderHead = value;
            }
        }
        private com.Sconit.Entity.MasterData.Item _item;
        public com.Sconit.Entity.MasterData.Item Item
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
        private string _referenceItemCode;
        public string ReferenceItemCode
        {
            get
            {
                return _referenceItemCode;
            }
            set
            {
                _referenceItemCode = value;
            }
        }

        public String CustomerItemCode { get; set; }

        private com.Sconit.Entity.MasterData.Uom _uom;
        public com.Sconit.Entity.MasterData.Uom Uom
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
        private Decimal _unitCount;
        public Decimal UnitCount
        {
            get
            {
                return _unitCount;
            }
            set
            {
                _unitCount = value;
            }
        }
        private Int32 _sequence;
        public Int32 Sequence
        {
            get
            {
                return _sequence;
            }
            set
            {
                _sequence = value;
            }
        }
        private Decimal _requiredQty;
        public Decimal RequiredQty
        {
            get
            {
                return _requiredQty;
            }
            set
            {
                _requiredQty = value;
            }
        }
        private Decimal _orderedQty;
        public Decimal OrderedQty
        {
            get
            {
                return _orderedQty;
            }
            set
            {
                _orderedQty = value;
            }
        }
        private Decimal? _shippedQty;
        public Decimal? ShippedQty
        {
            get
            {
                return _shippedQty;
            }
            set
            {
                _shippedQty = value;
            }
        }
        private Decimal? _receivedQty;
        public Decimal? ReceivedQty
        {
            get
            {
                return _receivedQty;
            }
            set
            {
                _receivedQty = value;
            }
        }
        private Decimal? _rejectedQty;
        public Decimal? RejectedQty
        {
            get
            {
                return _rejectedQty;
            }
            set
            {
                _rejectedQty = value;
            }
        }
        private Decimal? _scrapQty;
        public Decimal? ScrapQty
        {
            get
            {
                return _scrapQty;
            }
            set
            {
                _scrapQty = value;
            }
        }
        private Decimal? _goodsReceiptLotSize;
        public Decimal? GoodsReceiptLotSize
        {
            get
            {
                return _goodsReceiptLotSize;
            }
            set
            {
                _goodsReceiptLotSize = value;
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
        private Decimal? _discountFrom;
        public Decimal? DiscountFrom
        {
            get
            {
                return _discountFrom;
            }
            set
            {
                _discountFrom = value;
            }
        }
        private Decimal? _discountTo;
        public Decimal? DiscountTo
        {
            get
            {
                return _discountTo;
            }
            set
            {
                _discountTo = value;
            }
        }
        private string _billSettleTerm;
        public string BillSettleTerm
        {
            get
            {
                return _billSettleTerm;
            }
            set
            {
                _billSettleTerm = value;
            }
        }
        private IList<OrderLocationTransaction> _orderLocationTransactions;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public IList<OrderLocationTransaction> OrderLocationTransactions
        {
            get
            {
                return _orderLocationTransactions;
            }
            set
            {
                _orderLocationTransactions = value;
            }
        }
        private com.Sconit.Entity.MasterData.Bom _bom;
        public com.Sconit.Entity.MasterData.Bom Bom
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
        private com.Sconit.Entity.MasterData.Location _locationFrom;
        public com.Sconit.Entity.MasterData.Location LocationFrom
        {
            get
            {
                return _locationFrom;
            }
            set
            {
                _locationFrom = value;
            }
        }
        private com.Sconit.Entity.MasterData.Location _locationTo;
        public com.Sconit.Entity.MasterData.Location LocationTo
        {
            get
            {
                return _locationTo;
            }
            set
            {
                _locationTo = value;
            }
        }
        private com.Sconit.Entity.MasterData.BillAddress _billFrom;
        public com.Sconit.Entity.MasterData.BillAddress BillFrom
        {
            get
            {
                return _billFrom;
            }
            set
            {
                _billFrom = value;
            }
        }
        private com.Sconit.Entity.MasterData.BillAddress _billTo;
        public com.Sconit.Entity.MasterData.BillAddress BillTo
        {
            get
            {
                return _billTo;
            }
            set
            {
                _billTo = value;
            }
        }
        private com.Sconit.Entity.Procurement.PurchasePriceList _priceListFrom;
        public com.Sconit.Entity.Procurement.PurchasePriceList PriceListFrom
        {
            get
            {
                return _priceListFrom;
            }
            set
            {
                _priceListFrom = value;
            }
        }
        private com.Sconit.Entity.Distribution.SalesPriceList _priceListTo;
        public com.Sconit.Entity.Distribution.SalesPriceList PriceListTo
        {
            get
            {
                return _priceListTo;
            }
            set
            {
                _priceListTo = value;
            }
        }
        private com.Sconit.Entity.MasterData.PriceListDetail _priceListDetailFrom;
        public com.Sconit.Entity.MasterData.PriceListDetail PriceListDetailFrom
        {
            get
            {
                return _priceListDetailFrom;
            }
            set
            {
                _priceListDetailFrom = value;
            }
        }
        private com.Sconit.Entity.MasterData.PriceListDetail _priceListDetailTo;
        public com.Sconit.Entity.MasterData.PriceListDetail PriceListDetailTo
        {
            get
            {
                return _priceListDetailTo;
            }
            set
            {
                _priceListDetailTo = value;
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
        private Decimal? _packageVolumn;
        public Decimal? PackageVolumn
        {
            get
            {
                return _packageVolumn;
            }
            set
            {
                _packageVolumn = value;
            }
        }
        private string _packageType;
        public string PackageType
        {
            get
            {
                return _packageType;
            }
            set
            {
                _packageType = value;
            }
        }
        private Decimal? _totalAmountFrom;
        public Decimal? TotalAmountFrom
        {
            get
            {
                return _totalAmountFrom;
            }
            set
            {
                _totalAmountFrom = value;
            }
        }
        private Decimal? _totalAmountTo;
        public Decimal? TotalAmountTo
        {
            get
            {
                return _totalAmountTo;
            }
            set
            {
                _totalAmountTo = value;
            }
        }
        public com.Sconit.Entity.MasterData.Customer Customer { get; set; }
        public Boolean NeedInspection { get; set; }
        public String IdMark { get; set; }
        public String BarCodeType { get; set; }
        public String ItemVersion { get; set; }
        public String OddShipOption { get; set; }
        private Boolean _transferFlag;
        public Boolean TransferFlag
        {
            get
            {
                return _transferFlag;
            }
            set
            {
                _transferFlag = value;
            }
        }
        #endregion

        public override int GetHashCode()
        {
            if (Id != 0)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            OrderDetailBase another = obj as OrderDetailBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Id == another.Id);
            }
        }
    }

}
