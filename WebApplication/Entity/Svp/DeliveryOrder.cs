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
    public partial class DeliveryOrder
    {

        private DeliveryOrderItem[] deliveryOrderItemsField;

        private string deliveryOrderNoField;

        private string idField;

        private string memoField;

        private Party partyFromField;

        private Party partyToField;

        private System.DateTime receiveDateField;

        private bool receiveDateFieldSpecified;

        private RequisitionOrder requisitionOrderField;

        private System.DateTime sendDateField;

        private bool sendDateFieldSpecified;

        /// <remarks/>
        
        public DeliveryOrderItem[] deliveryOrderItems
        {
            get
            {
                return this.deliveryOrderItemsField;
            }
            set
            {
                this.deliveryOrderItemsField = value;
            }
        }

        /// <remarks/>
        
        public string deliveryOrderNo
        {
            get
            {
                return this.deliveryOrderNoField;
            }
            set
            {
                this.deliveryOrderNoField = value;
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
        
        public Party partyFrom
        {
            get
            {
                return this.partyFromField;
            }
            set
            {
                this.partyFromField = value;
            }
        }

        /// <remarks/>
        
        public Party partyTo
        {
            get
            {
                return this.partyToField;
            }
            set
            {
                this.partyToField = value;
            }
        }

        /// <remarks/>
        public System.DateTime receiveDate
        {
            get
            {
                return this.receiveDateField;
            }
            set
            {
                this.receiveDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool receiveDateSpecified
        {
            get
            {
                return this.receiveDateFieldSpecified;
            }
            set
            {
                this.receiveDateFieldSpecified = value;
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
        public System.DateTime sendDate
        {
            get
            {
                return this.sendDateField;
            }
            set
            {
                this.sendDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sendDateSpecified
        {
            get
            {
                return this.sendDateFieldSpecified;
            }
            set
            {
                this.sendDateFieldSpecified = value;
            }
        }
    }
}
