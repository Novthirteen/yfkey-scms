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
    public partial class EntityOrderby
    {

        private bool ascField;

        private bool ascFieldSpecified;

        private string orderbyFieldField;

        /// <remarks/>
        public bool asc
        {
            get
            {
                return this.ascField;
            }
            set
            {
                this.ascField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ascSpecified
        {
            get
            {
                return this.ascFieldSpecified;
            }
            set
            {
                this.ascFieldSpecified = value;
            }
        }

        /// <remarks/>
        public string orderbyField
        {
            get
            {
                return this.orderbyFieldField;
            }
            set
            {
                this.orderbyFieldField = value;
            }
        }
    }
}
