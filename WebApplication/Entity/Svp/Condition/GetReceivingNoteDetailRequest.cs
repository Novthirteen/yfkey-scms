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
    public partial class GetReceivingNoteDetailRequest
    {

        private int beginRowIndexField;

        private bool beginRowIndexFieldSpecified;

        private string customerField;

        private string customerPartyCodeField;

        private System.DateTime deliveryDateFromField;

        private bool deliveryDateFromFieldSpecified;

        private System.DateTime deliveryDateToField;

        private bool deliveryDateToFieldSpecified;

        private string deliveryOrderNoField;

        private EntityPage entityPageField;

        private string idField;

        private string locationField;

        private string materialField;

        private EntityOrderby[] orderbyField;

        private System.DateTime receiveDateFromField;

        private bool receiveDateFromFieldSpecified;

        private System.DateTime receiveDateToField;

        private bool receiveDateToFieldSpecified;

        private string receivingNoField;

        private string requisitionOrderNoField;

        private int rowSizeField;

        private bool rowSizeFieldSpecified;

        private string supplierField;

        private string supplierPartyCodeField;

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
        public System.DateTime deliveryDateFrom
        {
            get
            {
                return this.deliveryDateFromField;
            }
            set
            {
                this.deliveryDateFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deliveryDateFromSpecified
        {
            get
            {
                return this.deliveryDateFromFieldSpecified;
            }
            set
            {
                this.deliveryDateFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime deliveryDateTo
        {
            get
            {
                return this.deliveryDateToField;
            }
            set
            {
                this.deliveryDateToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool deliveryDateToSpecified
        {
            get
            {
                return this.deliveryDateToFieldSpecified;
            }
            set
            {
                this.deliveryDateToFieldSpecified = value;
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
        
        public string location
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
        public System.DateTime receiveDateFrom
        {
            get
            {
                return this.receiveDateFromField;
            }
            set
            {
                this.receiveDateFromField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool receiveDateFromSpecified
        {
            get
            {
                return this.receiveDateFromFieldSpecified;
            }
            set
            {
                this.receiveDateFromFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime receiveDateTo
        {
            get
            {
                return this.receiveDateToField;
            }
            set
            {
                this.receiveDateToField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool receiveDateToSpecified
        {
            get
            {
                return this.receiveDateToFieldSpecified;
            }
            set
            {
                this.receiveDateToFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public string receivingNo
        {
            get
            {
                return this.receivingNoField;
            }
            set
            {
                this.receivingNoField = value;
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
    }
}
