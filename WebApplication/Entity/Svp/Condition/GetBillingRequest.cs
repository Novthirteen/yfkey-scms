using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.Svp.Condition
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public class GetBillingRequest
    {

        private int beginRowIndexField;

        private bool beginRowIndexFieldSpecified;

        private System.DateTime billingDateFromField;

        private bool billingDateFromFieldSpecified;

        private System.DateTime billingDateToField;

        private bool billingDateToFieldSpecified;

        private string billingNoField;

        private string deliveryOrderNoField;

        private EntityPage entityPageField;

        private string idField;

        private string materialField;

        private EntityOrderby[] orderbyField;

        private string requisitionOrderNoField;

        private int rowSizeField;

        private bool rowSizeFieldSpecified;

        private System.DateTime sendDateFromField;

        private bool sendDateFromFieldSpecified;

        private System.DateTime sendDateToField;

        private bool sendDateToFieldSpecified;

        private string supplierCodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://domain.integration.symphony")]
        public int beginRowIndex
        {
            get
            {
                return this.beginRowIndexField;
            }
            set
            {
                this.beginRowIndexField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool beginRowIndexSpecified
        {
            get
            {
                return this.beginRowIndexFieldSpecified;
            }
            set
            {
                this.beginRowIndexFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime billingDateFrom
        {
            get
            {
                return this.billingDateFromField;
            }
            set
            {
                this.billingDateFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool billingDateFromSpecified
        {
            get
            {
                return this.billingDateFromFieldSpecified;
            }
            set
            {
                this.billingDateFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime billingDateTo
        {
            get
            {
                return this.billingDateToField;
            }
            set
            {
                this.billingDateToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool billingDateToSpecified
        {
            get
            {
                return this.billingDateToFieldSpecified;
            }
            set
            {
                this.billingDateToFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public string billingNo
        {
            get
            {
                return this.billingNoField;
            }
            set
            {
                this.billingNoField = value;
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
        public EntityPage entityPage
        {
            get
            {
                return this.entityPageField;
            }
            set
            {
                this.entityPageField = value;
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
        
        public string material
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
        [System.Xml.Serialization.XmlArrayAttribute(Namespace = "http://domain.integration.symphony")]
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://condition.integration.symphony")]
        public EntityOrderby[] orderby
        {
            get
            {
                return this.orderbyField;
            }
            set
            {
                this.orderbyField = value;
            }
        }

        /// <remarks/>
        
        public string requisitionOrderNo
        {
            get
            {
                return this.requisitionOrderNoField;
            }
            set
            {
                this.requisitionOrderNoField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://domain.integration.symphony")]
        public int rowSize
        {
            get
            {
                return this.rowSizeField;
            }
            set
            {
                this.rowSizeField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool rowSizeSpecified
        {
            get
            {
                return this.rowSizeFieldSpecified;
            }
            set
            {
                this.rowSizeFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime sendDateFrom
        {
            get
            {
                return this.sendDateFromField;
            }
            set
            {
                this.sendDateFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sendDateFromSpecified
        {
            get
            {
                return this.sendDateFromFieldSpecified;
            }
            set
            {
                this.sendDateFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime sendDateTo
        {
            get
            {
                return this.sendDateToField;
            }
            set
            {
                this.sendDateToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool sendDateToSpecified
        {
            get
            {
                return this.sendDateToFieldSpecified;
            }
            set
            {
                this.sendDateToFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public string supplierCode
        {
            get
            {
                return this.supplierCodeField;
            }
            set
            {
                this.supplierCodeField = value;
            }
        }
    }
}
