using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for EntityProxy
/// </summary>
namespace com.Sconit.Web
{
    [Serializable]
    public class FlowDetailProxy
    {
        public FlowDetailProxy()
        {

        }

        private string _flowCode;
        public string FlowCode
        {
            get
            {
                return this._flowCode;
            }
            set
            {
                this._flowCode = value;
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

        private string _itemDescription;
        public string ItemDescription
        {
            get
            {
                return this._itemDescription;
            }
            set
            {
                this._itemDescription = value;
            }
        }

        private string _itemReferenceCode;
        public string ItemReferenceCode
        {
            get
            {
                return this._itemReferenceCode;
            }
            set
            {
                this._itemReferenceCode = value;
            }
        }

        private string _uomCode;
        public string UomCode
        {
            get
            {
                return this._uomCode;
            }
            set
            {
                this._uomCode = value;
            }
        }

        private decimal _unitCount;
        public decimal UnitCount
        {
            get
            {
                return this._unitCount;
            }
            set
            {
                this._unitCount = value;
            }
        }

        private decimal _unitPrice;
        public decimal UnitPrice
        {
            get
            {
                return this._unitPrice;
            }
            set
            {
                this._unitPrice = value;
            }
        }

        private string _priceListCode;
        public string PriceListCode
        {
            get
            {
                return this._priceListCode;
            }
            set
            {
                this._priceListCode = value;
            }
        }

        private Int32 _priceListDetailId;
        public Int32 PriceListDetailId
        {
            get
            {
                return this._priceListDetailId;
            }
            set
            {
                this._priceListDetailId = value;
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
                this._huLotSize = value;
            }
        }

    }


    [Serializable]
    public class TreeNodeProxy
    {
        public TreeNodeProxy()
        {

        }

        private string _textColumn;
        public string TextColumn
        {
            get
            {
                return this._textColumn;
            }
            set
            {
                this._textColumn = value;
            }
        }
        private string _valueColumn;
        public string ValueColumn
        {
            get
            {
                return this._valueColumn;
            }
            set
            {
                this._valueColumn = value;
            }
        }
        private string _parentValueColumn;
        public string ParentValueColumn
        {
            get
            {
                return this._parentValueColumn;
            }
            set
            {
                this._parentValueColumn = value;
            }
        }
    }

    [Serializable]
    public class FlowMstr
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
        public string Type { get; set; }
        public bool IsListDetail { get; set; }
        public bool IsReceiptScanHu { get; set; }
        public bool IsShipScanHu { get; set; }
        public bool AllowCreateDetail { get; set; }
        public DateTime LastModifyDate { get; set; }
    }

    [Serializable]
    public class OrderMstr
    {
        public string Code { get; set; }
        public string PartyFrom { get; set; }
        public string PartyTo { get; set; }
        public string Type { get; set; }
        public bool IsShipScanHu { get; set; }
        public bool IsReceiptScanHu { get; set; }
        public DateTime LastModifyDate { get; set; }
        public string FlowCode { get; set; }
        public bool AllowExceed { get; set; }
    }

    [Serializable]
    public class OrderLocationTransactionProxy
    {
        public int OrderLocTransId { get; set; }
        public decimal Qty { get; set; }
        public string HuId { get; set; }
        public string LotNo { get; set; }
    }

    [Serializable]
    public class FlowDet
    {
        public int Id { get; set; }
        public int Seq { get; set; }
        public string FlowNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public decimal UnitCount { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
    }

    [Serializable]
    public class OrderDet
    {
        public int Id { get; set; }
        public int Seq { get; set; }
        public string OrderNo { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string UomCode { get; set; }
        public string UomDescription { get; set; }
        public Decimal UnitCount { get; set; }
        public decimal OrderedQty { get; set; }
        public decimal ShippedQty { get; set; }
        public decimal ReceiveQty { get; set; }
        public decimal ShipQty { get; set; }
        public decimal ReceivedQty { get; set; }
        public decimal RejectQty { get; set; }
        public decimal ScrapQty { get; set; }
        public string HuId { get; set; }
        public string LocationFrom { get; set; }
        public string LocationTo { get; set; }
    }

    [Serializable]
    public class InvTransOrder
    {
        public int Id { get; set; }
        public int Sequence { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string ReferenceItemCode { get; set; }
        public decimal UnitCount { get; set; }
        public string UomCode { get; set; }
        public decimal OrderedQty { get; set; }
        public string HuId { get; set; }
        public decimal HuQty { get; set; }
    }

    [Serializable]
    public class HuProxy
    {
        public string HuId { get; set; }
        public string ItemCode { get; set; }
        public string ItemDescription { get; set; }
        public string UomCode { get; set; }
        public decimal UC {get; set; }
        public decimal Qty { get; set; }       
    }

    [Serializable]
    public class LocLotDet : HuProxy
    {
        public int Id { get; set; }
        public string Bin { get; set; }
    }

    [Serializable]
    public class Bin
    {
        public string Area { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

}
