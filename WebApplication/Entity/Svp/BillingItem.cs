using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.Svp
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public partial class BillingItem
    {

        private Billing billingField;

        private double billingQuantityField;

        private bool billingQuantityFieldSpecified;

        private string idField;

        private Material materialField;

        private string memoField;

        private ReceivingNoteItem receivingNoteItemField;

        private int sequenceNoField;

        private bool sequenceNoFieldSpecified;

        private bool taxIncludeField;

        private bool taxIncludeFieldSpecified;

        private System.Nullable<decimal> unitPriceField;

        private bool unitPriceFieldSpecified;

        /// <remarks/>
        public Billing billing
        {
            get
            {
                return this.billingField;
            }
            set
            {
                this.billingField = value;
            }
        }

        /// <remarks/>
        public double billingQuantity
        {
            get
            {
                return this.billingQuantityField;
            }
            set
            {
                this.billingQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool billingQuantitySpecified
        {
            get
            {
                return this.billingQuantityFieldSpecified;
            }
            set
            {
                this.billingQuantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://domain.integration.symphony")]
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://domain.integration.symphony")]
        public Material material
        {
            get
            {
                return this.materialField;
            }
            set
            {
                this.materialField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://domain.integration.symphony")]
        public string memo
        {
            get
            {
                return this.memoField;
            }
            set
            {
                this.memoField = value;
            }
        }

        /// <remarks/>
        
        public ReceivingNoteItem receivingNoteItem
        {
            get
            {
                return this.receivingNoteItemField;
            }
            set
            {
                this.receivingNoteItemField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://domain.integration.symphony")]
        public int sequenceNo
        {
            get
            {
                return this.sequenceNoField;
            }
            set
            {
                this.sequenceNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sequenceNoSpecified
        {
            get
            {
                return this.sequenceNoFieldSpecified;
            }
            set
            {
                this.sequenceNoFieldSpecified = value;
            }
        }

        /// <remarks/>
        public bool taxInclude
        {
            get
            {
                return this.taxIncludeField;
            }
            set
            {
                this.taxIncludeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool taxIncludeSpecified
        {
            get
            {
                return this.taxIncludeFieldSpecified;
            }
            set
            {
                this.taxIncludeFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public System.Nullable<decimal> unitPrice
        {
            get
            {
                return this.unitPriceField;
            }
            set
            {
                this.unitPriceField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool unitPriceSpecified
        {
            get
            {
                return this.unitPriceFieldSpecified;
            }
            set
            {
                this.unitPriceFieldSpecified = value;
            }
        }
    }
}
