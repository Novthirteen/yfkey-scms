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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://condition.integration.symphony")]
    public class EntityPage
    {

        private int beginRowIndexField;

        private bool beginRowIndexFieldSpecified;

        private int rowSizeField;

        private bool rowSizeFieldSpecified;

        /// <remarks/>
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
