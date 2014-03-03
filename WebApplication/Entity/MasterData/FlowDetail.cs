using System;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class FlowDetail : FlowDetailBase
    {
        #region Non O/R Mapping Properties
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public Location DefaultLocationFrom
        {
            get
            {
                if (this.LocationFrom == null)
                {
                    return this.Flow.LocationFrom;
                }
                else
                {
                    return this.LocationFrom;
                }
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public Location DefaultLocationTo
        {
            get
            {
                if (this.LocationTo == null)
                {
                    return this.Flow.LocationTo;
                }
                else
                {
                    return this.LocationTo;
                }
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public BillAddress DefaultBillFrom
        {
            get
            {
                if (this.BillFrom == null)
                {
                    return this.Flow.BillFrom;
                }
                else
                {
                    return this.BillFrom;
                }
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public BillAddress DefaultBillTo
        {
            get
            {
                if (this.BillTo == null)
                {
                    return this.Flow.BillTo;
                }
                else
                {
                    return this.BillTo;
                }
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public PurchasePriceList DefaultPriceListFrom
        {
            get
            {
                if (this.PriceListFrom == null)
                {
                    return this.Flow.PriceListFrom;
                }
                else
                {
                    return this.PriceListFrom;
                }
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public SalesPriceList DefaultPriceListTo
        {
            get
            {
                if (this.PriceListTo == null)
                {
                    return this.Flow.PriceListTo;
                }
                else
                {
                    return this.PriceListTo;
                }
            }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public string DefaultBillSettleTerm
        {
            get
            {
                if (this.BillSettleTerm == null)
                {
                    return this.Flow.BillSettleTerm;
                }
                else
                {
                    return this.BillSettleTerm;
                }
            }
        }

        private decimal _orderedQty;
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public decimal OrderedQty
        {
            get
            {
                return this._orderedQty;
            }
            set
            {
                this._orderedQty = value;
            }
        }

        public bool IsBlankDetail { get; set; }

        public string HuLotNo { get; set; }

        public string HuShiftCode { get; set; }

        public string ItemVersion { get; set; }
        #endregion
    }
}