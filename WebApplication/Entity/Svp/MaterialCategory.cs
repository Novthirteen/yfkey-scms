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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://md.domain.integration.symphony")]
    public partial class MaterialCategory
    {

        private string descriptionField;

        private string idField;

        private string longDescriptionField;

        private string nameField;

        private MaterialCategory parentCategoryField;

        private string typeField;

        /// <remarks/>
        
        public string description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
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
        
        public string longDescription
        {
            get
            {
                return this.longDescriptionField;
            }
            set
            {
                this.longDescriptionField = value;
            }
        }

        /// <remarks/>
        
        public string name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        
        public MaterialCategory parentCategory
        {
            get
            {
                return this.parentCategoryField;
            }
            set
            {
                this.parentCategoryField = value;
            }
        }

        /// <remarks/>
        
        public string type
        {
            get
            {
                return this.typeField;
            }
            set
            {
                this.typeField = value;
            }
        }
    }
}
