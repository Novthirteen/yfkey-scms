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
    public partial class Party
    {

        private string addressField;

        private string codeField;

        private string contactField;

        private Party[] customersField;

        private Party[] departmentsField;

        private string descriptionField;

        private string emailField;

        private Party[] employeeField;

        private string faxField;

        private string idField;

        private string mobilephoneField;

        private Party parentPartyField;

        private string postCodeField;

        private string[] rolesField;

        private string statusField;

        private Party[] subsidiariesField;

        private Party[] suppliersField;

        private string telephoneField;

        private string typeField;

        /// <remarks/>
        
        public string address
        {
            get
            {
                return this.addressField;
            }
            set
            {
                this.addressField = value;
            }
        }

        /// <remarks/>
        
        public string code
        {
            get
            {
                return this.codeField;
            }
            set
            {
                this.codeField = value;
            }
        }

        /// <remarks/>
        
        public string contact
        {
            get
            {
                return this.contactField;
            }
            set
            {
                this.contactField = value;
            }
        }

        /// <remarks/>
        
        public Party[] customers
        {
            get
            {
                return this.customersField;
            }
            set
            {
                this.customersField = value;
            }
        }

        /// <remarks/>
        
        public Party[] departments
        {
            get
            {
                return this.departmentsField;
            }
            set
            {
                this.departmentsField = value;
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
        
        public string email
        {
            get
            {
                return this.emailField;
            }
            set
            {
                this.emailField = value;
            }
        }

        /// <remarks/>
        
        public Party[] employee
        {
            get
            {
                return this.employeeField;
            }
            set
            {
                this.employeeField = value;
            }
        }

        /// <remarks/>
        
        public string fax
        {
            get
            {
                return this.faxField;
            }
            set
            {
                this.faxField = value;
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
        
        public string mobilephone
        {
            get
            {
                return this.mobilephoneField;
            }
            set
            {
                this.mobilephoneField = value;
            }
        }

        /// <remarks/>
        
        public Party parentParty
        {
            get
            {
                return this.parentPartyField;
            }
            set
            {
                this.parentPartyField = value;
            }
        }

        /// <remarks/>
        
        public string postCode
        {
            get
            {
                return this.postCodeField;
            }
            set
            {
                this.postCodeField = value;
            }
        }

        /// <remarks/>
        
        [System.Xml.Serialization.XmlArrayItemAttribute(Namespace = "http://mm.service.integration.symphony")]
        public string[] roles
        {
            get
            {
                return this.rolesField;
            }
            set
            {
                this.rolesField = value;
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
        
        public Party[] subsidiaries
        {
            get
            {
                return this.subsidiariesField;
            }
            set
            {
                this.subsidiariesField = value;
            }
        }

        /// <remarks/>
        
        public Party[] suppliers
        {
            get
            {
                return this.suppliersField;
            }
            set
            {
                this.suppliersField = value;
            }
        }

        /// <remarks/>
        
        public string telephone
        {
            get
            {
                return this.telephoneField;
            }
            set
            {
                this.telephoneField = value;
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
