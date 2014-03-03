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
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "http://mm.domain.integration.symphony")]
    public partial class InventoryItem
    {

        private UOM uOMField;

        private string binNoField;

        private UOM currencyUOMField;

        private string idField;

        private Location locationField;

        private Lot lotField;

        private System.DateTime manufacturedDateField;

        private bool manufacturedDateFieldSpecified;

        private Material materialField;

        private Party ownerPartyField;

        private double quantityOnHandField;

        private bool quantityOnHandFieldSpecified;

        private double quantityOnHandTotalField;

        private bool quantityOnHandTotalFieldSpecified;

        private System.DateTime receivedDateField;

        private bool receivedDateFieldSpecified;

        private string serialNoField;

        private string statusField;

        private Party supplierField;

        private double unitCostField;

        private bool unitCostFieldSpecified;

        private double unitCountField;

        private bool unitCountFieldSpecified;

        /// <remarks/>
        
        public UOM UOM
        {
            get
            {
                return this.uOMField;
            }
            set
            {
                this.uOMField = value;
            }
        }

        /// <remarks/>
        
        public string binNo
        {
            get
            {
                return this.binNoField;
            }
            set
            {
                this.binNoField = value;
            }
        }

        /// <remarks/>
        
        public UOM currencyUOM
        {
            get
            {
                return this.currencyUOMField;
            }
            set
            {
                this.currencyUOMField = value;
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
        
        public Location location
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
        
        public Lot lot
        {
            get
            {
                return this.lotField;
            }
            set
            {
                this.lotField = value;
            }
        }

        /// <remarks/>
        public System.DateTime manufacturedDate
        {
            get
            {
                return this.manufacturedDateField;
            }
            set
            {
                this.manufacturedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool manufacturedDateSpecified
        {
            get
            {
                return this.manufacturedDateFieldSpecified;
            }
            set
            {
                this.manufacturedDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public Material material
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
        
        public Party ownerParty
        {
            get
            {
                return this.ownerPartyField;
            }
            set
            {
                this.ownerPartyField = value;
            }
        }

        /// <remarks/>
        public double quantityOnHand
        {
            get
            {
                return this.quantityOnHandField;
            }
            set
            {
                this.quantityOnHandField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool quantityOnHandSpecified
        {
            get
            {
                return this.quantityOnHandFieldSpecified;
            }
            set
            {
                this.quantityOnHandFieldSpecified = value;
            }
        }

        /// <remarks/>
        public double quantityOnHandTotal
        {
            get
            {
                return this.quantityOnHandTotalField;
            }
            set
            {
                this.quantityOnHandTotalField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool quantityOnHandTotalSpecified
        {
            get
            {
                return this.quantityOnHandTotalFieldSpecified;
            }
            set
            {
                this.quantityOnHandTotalFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime receivedDate
        {
            get
            {
                return this.receivedDateField;
            }
            set
            {
                this.receivedDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool receivedDateSpecified
        {
            get
            {
                return this.receivedDateFieldSpecified;
            }
            set
            {
                this.receivedDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public string serialNo
        {
            get
            {
                return this.serialNoField;
            }
            set
            {
                this.serialNoField = value;
            }
        }

        /// <remarks/>
        
        public string status
        {
            get
            {
                return this.statusField;
            }
            set
            {
                this.statusField = value;
            }
        }

        /// <remarks/>
        
        public Party supplier
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
        public double unitCost
        {
            get
            {
                return this.unitCostField;
            }
            set
            {
                this.unitCostField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool unitCostSpecified
        {
            get
            {
                return this.unitCostFieldSpecified;
            }
            set
            {
                this.unitCostFieldSpecified = value;
            }
        }

        /// <remarks/>
        public double unitCount
        {
            get
            {
                return this.unitCountField;
            }
            set
            {
                this.unitCountField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool unitCountSpecified
        {
            get
            {
                return this.unitCountFieldSpecified;
            }
            set
            {
                this.unitCountFieldSpecified = value;
            }
        }
    }
}
