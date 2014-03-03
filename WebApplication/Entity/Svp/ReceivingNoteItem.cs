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
    public partial class ReceivingNoteItem
    {

        private string billingStatusField;

        private string idField;

        private Location locationField;

        private Material materialField;

        private string memoField;

        private double receiveQuantityField;

        private bool receiveQuantityFieldSpecified;

        private ReceivingNote receivingNoteField;

        private int sequenceNoField;

        private bool sequenceNoFieldSpecified;

        private double totalBillingQuantityField;

        private bool totalBillingQuantityFieldSpecified;

        private double unitCountField;

        private bool unitCountFieldSpecified;

        /// <remarks/>
        
        public string billingStatus
        {
            get
            {
                return this.billingStatusField;
            }
            set
            {
                this.billingStatusField = value;
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
        
        public Location location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
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
        public double receiveQuantity
        {
            get
            {
                return this.receiveQuantityField;
            }
            set
            {
                this.receiveQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool receiveQuantitySpecified
        {
            get
            {
                return this.receiveQuantityFieldSpecified;
            }
            set
            {
                this.receiveQuantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public ReceivingNote receivingNote
        {
            get
            {
                return this.receivingNoteField;
            }
            set
            {
                this.receivingNoteField = value;
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
        public double totalBillingQuantity
        {
            get
            {
                return this.totalBillingQuantityField;
            }
            set
            {
                this.totalBillingQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool totalBillingQuantitySpecified
        {
            get
            {
                return this.totalBillingQuantityFieldSpecified;
            }
            set
            {
                this.totalBillingQuantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public double unitCount
        {
            get
            {
                return this.unitCountField;
            }
            set
            {
                this.unitCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool unitCountSpecified
        {
            get
            {
                return this.unitCountFieldSpecified;
            }
            set
            {
                this.unitCountFieldSpecified = value;
            }
        }
    }
}
