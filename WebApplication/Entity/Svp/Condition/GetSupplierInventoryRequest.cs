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
    public partial class GetSupplierInventoryRequest
    {

        private int beginRowIndexField;

        private bool beginRowIndexFieldSpecified;

        private string companyField;

        private bool convertUnitCountField;

        private bool convertUnitCountFieldSpecified;

        private EntityPage entityPageField;

        private string idField;

        private string locationField;

        private string materialField;

        private EntityOrderby[] orderbyField;

        private int rowSizeField;

        private bool rowSizeFieldSpecified;

        private string supplierCodeField;

        private string supplierMaterialField;

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
        
        public string company
        {
            get
            {
                return this.companyField;
            }
            set
            {
                this.companyField = value;
            }
        }

        /// <remarks/>
        public bool convertUnitCount
        {
            get
            {
                return this.convertUnitCountField;
            }
            set
            {
                this.convertUnitCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool convertUnitCountSpecified
        {
            get
            {
                return this.convertUnitCountFieldSpecified;
            }
            set
            {
                this.convertUnitCountFieldSpecified = value;
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

        /// <remarks/>
        
        public string supplierMaterial
        {
            get
            {
                return this.supplierMaterialField;
            }
            set
            {
                this.supplierMaterialField = value;
            }
        }
    }
}
