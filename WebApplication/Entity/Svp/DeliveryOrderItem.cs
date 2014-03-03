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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://sd.domain.integration.symphony")]
    public partial class DeliveryOrderItem
    {

        private DeliveryOrder deliveryOrderField;

        private double deliveryQuantityField;

        private bool deliveryQuantityFieldSpecified;

        private string idField;

        private Material materialField;

        private string memoField;

        private RequisitionOrderItem requisitionOrderItemField;

        private int sequenceNoField;

        private bool sequenceNoFieldSpecified;

        /// <remarks/>
        
        public DeliveryOrder deliveryOrder
        {
            get
            {
                return this.deliveryOrderField;
            }
            set
            {
                this.deliveryOrderField = value;
            }
        }

        /// <remarks/>
        public double deliveryQuantity
        {
            get
            {
                return this.deliveryQuantityField;
            }
            set
            {
                this.deliveryQuantityField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deliveryQuantitySpecified
        {
            get
            {
                return this.deliveryQuantityFieldSpecified;
            }
            set
            {
                this.deliveryQuantityFieldSpecified = value;
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
        
        public RequisitionOrderItem requisitionOrderItem
        {
            get
            {
                return this.requisitionOrderItemField;
            }
            set
            {
                this.requisitionOrderItemField = value;
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
    }
}
