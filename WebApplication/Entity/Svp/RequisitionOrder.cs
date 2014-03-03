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
    public partial class RequisitionOrder
    {

        private System.DateTime cancelDateField;

        private bool cancelDateFieldSpecified;

        private User cancelUserField;

        private System.DateTime createDateField;

        private bool createDateFieldSpecified;

        private User createUserField;

        private string demandDeliverAddrField;

        private System.DateTime demandDeliverDateField;

        private bool demandDeliverDateFieldSpecified;

        private System.DateTime effectiveDateField;

        private bool effectiveDateFieldSpecified;

        private string generateTypeField;

        private string idField;

        private string memoField;

        private string orderTypeField;

        private Party partyFromField;

        private Party partyToField;

        private User plannerField;

        private bool printField;

        private bool printFieldSpecified;

        private string priorityField;

        private List<RequisitionOrderItem> requisitionOrderItemsField;

        private string requisitionOrderNoField;

        private string statusField;

        private string typeField;

        /// <remarks/>
        public System.DateTime cancelDate
        {
            get
            {
                return this.cancelDateField;
            }
            set
            {
                this.cancelDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool cancelDateSpecified
        {
            get
            {
                return this.cancelDateFieldSpecified;
            }
            set
            {
                this.cancelDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public User cancelUser
        {
            get
            {
                return this.cancelUserField;
            }
            set
            {
                this.cancelUserField = value;
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
        
        public string demandDeliverAddr
        {
            get
            {
                return this.demandDeliverAddrField;
            }
            set
            {
                this.demandDeliverAddrField = value;
            }
        }

        /// <remarks/>
        public System.DateTime demandDeliverDate
        {
            get
            {
                return this.demandDeliverDateField;
            }
            set
            {
                this.demandDeliverDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool demandDeliverDateSpecified
        {
            get
            {
                return this.demandDeliverDateFieldSpecified;
            }
            set
            {
                this.demandDeliverDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        public System.DateTime effectiveDate
        {
            get
            {
                return this.effectiveDateField;
            }
            set
            {
                this.effectiveDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool effectiveDateSpecified
        {
            get
            {
                return this.effectiveDateFieldSpecified;
            }
            set
            {
                this.effectiveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public string generateType
        {
            get
            {
                return this.generateTypeField;
            }
            set
            {
                this.generateTypeField = value;
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
        
        public string orderType
        {
            get
            {
                return this.orderTypeField;
            }
            set
            {
                this.orderTypeField = value;
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

        /// <remarks/>
        
        public User planner
        {
            get
            {
                return this.plannerField;
            }
            set
            {
                this.plannerField = value;
            }
        }

        /// <remarks/>
        public bool print
        {
            get
            {
                return this.printField;
            }
            set
            {
                this.printField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool printSpecified
        {
            get
            {
                return this.printFieldSpecified;
            }
            set
            {
                this.printFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public string priority
        {
            get
            {
                return this.priorityField;
            }
            set
            {
                this.priorityField = value;
            }
        }

        /// <remarks/>
        
        public RequisitionOrderItem[] requisitionOrderItems
        {
            get
            {
                if (this.requisitionOrderItemsField != null)
                {
                    return this.requisitionOrderItemsField.ToArray();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                requisitionOrderItemsField = new List<RequisitionOrderItem>();
                foreach(RequisitionOrderItem item in value)
                {
                    requisitionOrderItemsField.Add(item);
                }
            }
        }

        public void addRequisitionOrderItem(RequisitionOrderItem item)
        {
            if (requisitionOrderItemsField == null)
            {
                requisitionOrderItemsField = new List<RequisitionOrderItem>();
            }
            requisitionOrderItemsField.Add(item);
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
