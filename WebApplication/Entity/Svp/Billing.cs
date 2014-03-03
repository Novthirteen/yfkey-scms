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
    public partial class Billing
    {

        private System.DateTime billingDateField;

        private bool billingDateFieldSpecified;

        private List<BillingItem> billingItemsField;

        private string billingNoField;

        private System.DateTime createDateField;

        private bool createDateFieldSpecified;

        private User createUserField;

        private string idField;

        private string memoField;

        private Party partyFromField;

        private Party partyToField;

        /// <remarks/>
        public System.DateTime billingDate
        {
            get
            {
                return this.billingDateField;
            }
            set
            {
                this.billingDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool billingDateSpecified
        {
            get
            {
                return this.billingDateFieldSpecified;
            }
            set
            {
                this.billingDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public BillingItem[] billingItems
        {
            get
            {
                if (this.billingItemsField != null)
                {
                    return this.billingItemsField.ToArray();
                } 
                else
                {
                    return null;
                }
            }
            set
            {
                this.billingItemsField = new List<BillingItem>();
                foreach(BillingItem item in billingItems)
                {
                    this.billingItemsField.Add(item);
                }
            }
        }

        public void AddBillingItem(BillingItem item)
        {
            if (this.billingItemsField == null) 
            {
                this.billingItemsField = new List<BillingItem>();
            }

            this.billingItemsField.Add(item);
        }

        /// <remarks/>
        public string billingNo
        {
            get
            {
                return this.billingNoField;
            }
            set
            {
                this.billingNoField = value;
            }
        }

        /// <remarks/>
        public System.DateTime createDate
        {
            get
            {
                return this.createDateField;
            }
            set
            {
                this.createDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool createDateSpecified
        {
            get
            {
                return this.createDateFieldSpecified;
            }
            set
            {
                this.createDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public User createUser
        {
            get
            {
                return this.createUserField;
            }
            set
            {
                this.createUserField = value;
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
        public string memo
        {
            get
            {
                return this.memoField;
            }
            set
            {
                this.memoField = value;
            }
        }

        /// <remarks/>
        public Party partyFrom
        {
            get
            {
                return this.partyFromField;
            }
            set
            {
                this.partyFromField = value;
            }
        }

        /// <remarks/>
        public Party partyTo
        {
            get
            {
                return this.partyToField;
            }
            set
            {
                this.partyToField = value;
            }
        }
    }
}
