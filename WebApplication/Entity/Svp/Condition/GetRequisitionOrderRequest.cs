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
    public partial class GetRequisitionOrderRequest
    {

        private int beginRowIndexField;

        private bool beginRowIndexFieldSpecified;

        private string customerField;

        private string customerPartyCodeField;

        private string demandDeliverAddrField;

        private System.DateTime effectiveDateFromField;

        private bool effectiveDateFromFieldSpecified;

        private System.DateTime effectiveDateToField;

        private bool effectiveDateToFieldSpecified;

        private EntityPage entityPageField;

        private string idField;

        private string[] orderTypeField;

        private EntityOrderby[] orderbyField;

        private string requisitionOrderNoField;

        private string[] requisitionOrderStatusField;

        private int rowSizeField;

        private bool rowSizeFieldSpecified;

        private string supplierField;

        private string supplierPartyCodeField;

        private bool hasPrintField;

        private bool hasPrintFieldSpecified;

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
        
        public string customer
        {
            get
            {
                return this.customerField;
            }
            set
            {
                this.customerField = value;
            }
        }

        /// <remarks/>
        
        public string customerPartyCode
        {
            get
            {
                return this.customerPartyCodeField;
            }
            set
            {
                this.customerPartyCodeField = value;
            }
        }

        /// <remarks/>
        
        public string demandDeliverAddr
        {
            get
            {
                return this.demandDeliverAddrField;
            }
            set
            {
                this.demandDeliverAddrField = value;
            }
        }

        /// <remarks/>
        public System.DateTime effectiveDateFrom
        {
            get
            {
                return this.effectiveDateFromField;
            }
            set
            {
                this.effectiveDateFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool effectiveDateFromSpecified
        {
            get
            {
                return this.effectiveDateFromFieldSpecified;
            }
            set
            {
                this.effectiveDateFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime effectiveDateTo
        {
            get
            {
                return this.effectiveDateToField;
            }
            set
            {
                this.effectiveDateToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool effectiveDateToSpecified
        {
            get
            {
                return this.effectiveDateToFieldSpecified;
            }
            set
            {
                this.effectiveDateToFieldSpecified = value;
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
        
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.service.integration.symphony")]
        public string[] orderType
        {
            get
            {
                return this.orderTypeField;
            }
            set
            {
                this.orderTypeField = value;
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
        
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.service.integration.symphony")]
        public string[] requisitionOrderStatus
        {
            get
            {
                return this.requisitionOrderStatusField;
            }
            set
            {
                this.requisitionOrderStatusField = value;
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
        
        public string supplier
        {
            get
            {
                return this.supplierField;
            }
            set
            {
                this.supplierField = value;
            }
        }

        /// <remarks/>
        
        public string supplierPartyCode
        {
            get
            {
                return this.supplierPartyCodeField;
            }
            set
            {
                this.supplierPartyCodeField = value;
            }
        }

        /// <remarks/>
        public bool hasPrint
        {
            get
            {
                return this.hasPrintField;
            }
            set
            {
                this.hasPrintField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool hasPrintSpecified
        {
            get
            {
                return this.hasPrintFieldSpecified;
            }
            set
            {
                this.hasPrintFieldSpecified = value;
            }
        }
    }
}
