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
    public partial class Material
    {

        private string2MaterialMapEntry[] customerMaterialsField;

        private string descriptionField;

        private string idField;

        private string longDescriptionField;

        private Party manufacturePartyField;

        private MaterialCategory materialCategoryField;

        private string nameField;

        private string noField;

        private Party partyField;

        private UOM quantityUOMField;

        private string2MaterialMapEntry[] supplierMaterialsField;

        private string typeField;

        /// <remarks/>
        
        [System.Xml.Serialization.XmlArrayItemAttribute("entry", Namespace = "http://mm.service.integration.symphony", IsNullable = false)]
        public string2MaterialMapEntry[] customerMaterials
        {
            get
            {
                return this.customerMaterialsField;
            }
            set
            {
                this.customerMaterialsField = value;
            }
        }

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
        
        public Party manufactureParty
        {
            get
            {
                return this.manufacturePartyField;
            }
            set
            {
                this.manufacturePartyField = value;
            }
        }

        /// <remarks/>
        
        public MaterialCategory materialCategory
        {
            get
            {
                return this.materialCategoryField;
            }
            set
            {
                this.materialCategoryField = value;
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
        
        public string no
        {
            get
            {
                return this.noField;
            }
            set
            {
                this.noField = value;
            }
        }

        /// <remarks/>
        
        public Party party
        {
            get
            {
                return this.partyField;
            }
            set
            {
                this.partyField = value;
            }
        }

        /// <remarks/>
        
        public UOM quantityUOM
        {
            get
            {
                return this.quantityUOMField;
            }
            set
            {
                this.quantityUOMField = value;
            }
        }

        /// <remarks/>
        
        [System.Xml.Serialization.XmlArrayItemAttribute("entry", Namespace = "http://mm.service.integration.symphony", IsNullable = false)]
        public string2MaterialMapEntry[] supplierMaterials
        {
            get
            {
                return this.supplierMaterialsField;
            }
            set
            {
                this.supplierMaterialsField = value;
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

    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://mm.service.integration.symphony")]
    public partial class string2MaterialMapEntry
    {

        private string keyField;

        private Material valueField;

        /// <remarks/>
        public string key
        {
            get
            {
                return this.keyField;
            }
            set
            {
                this.keyField = value;
            }
        }

        /// <remarks/>
        public Material value
        {
            get
            {
                return this.valueField;
            }
            set
            {
                this.valueField = value;
            }
        }
    }
}
