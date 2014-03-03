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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://domain.integration.symphony")]
    public class AbstractSearchRequest
    {
        private int beginRowIndexField;

        private bool beginRowIndexFieldSpecified;

        private EntityPage entityPageField;

        private EntityOrderby[] orderbyField;

        private int rowSizeField;

        private bool rowSizeFieldSpecified;

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
    }
}
