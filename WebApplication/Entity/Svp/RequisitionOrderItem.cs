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
    public partial class RequisitionOrderItem
    {

        private double deliveredQuantityField;

        private bool deliveredQuantityFieldSpecified;

        private string idField;

        private Material materialField;

        private string memoField;

        private double receivedQuantityField;

        private bool receivedQuantityFieldSpecified;

        private double requiredQuantityField;

        private bool requiredQuantityFieldSpecified;

        private RequisitionOrder requisitionOrderField;

        private int sequenceNoField;

        private bool sequenceNoFieldSpecified;

        private double unitCountField;

        private bool unitCountFieldSpecified;

        /// <remarks/>
        public double deliveredQuantity
        {
            get
            {
                return this.deliveredQuantityField;
            }
            set
            {
                this.deliveredQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deliveredQuantitySpecified
        {
            get
            {
                return this.deliveredQuantityFieldSpecified;
            }
            set
            {
                this.deliveredQuantityFieldSpecified = value;
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
        public double receivedQuantity
        {
            get
            {
                return this.receivedQuantityField;
            }
            set
            {
                this.receivedQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool receivedQuantitySpecified
        {
            get
            {
                return this.receivedQuantityFieldSpecified;
            }
            set
            {
                this.receivedQuantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        public double requiredQuantity
        {
            get
            {
                return this.requiredQuantityField;
            }
            set
            {
                this.requiredQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool requiredQuantitySpecified
        {
            get
            {
                return this.requiredQuantityFieldSpecified;
            }
            set
            {
                this.requiredQuantityFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public RequisitionOrder requisitionOrder
        {
            get
            {
                return this.requisitionOrderField;
            }
            set
            {
                this.requisitionOrderField = value;
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
