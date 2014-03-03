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
    public partial class ReceivingNote
    {

        private DeliveryOrder deliveryOrderField;

        private string idField;

        private Location locationField;

        private string memoField;

        private Party partyFromField;

        private Party partyToField;

        private System.DateTime receiveDateField;

        private bool receiveDateFieldSpecified;

        private User receiveUserField;

        private string receivingNoField;

        private List<ReceivingNoteItem> receivingNoteItemsField;

        private string transactionTypeField;

        /// <remarks/>
        
        public DeliveryOrder deliveryOrder
        {
            get
            {
                return this.deliveryOrderField;
            }
            set
            {
                this.deliveryOrderField = value;
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

        /// <remarks/>
        public System.DateTime receiveDate
        {
            get
            {
                return this.receiveDateField;
            }
            set
            {
                this.receiveDateField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool receiveDateSpecified
        {
            get
            {
                return this.receiveDateFieldSpecified;
            }
            set
            {
                this.receiveDateFieldSpecified = value;
            }
        }

        /// <remarks/>
        
        public User receiveUser
        {
            get
            {
                return this.receiveUserField;
            }
            set
            {
                this.receiveUserField = value;
            }
        }

        /// <remarks/>
        
        public string receivingNo
        {
            get
            {
                return this.receivingNoField;
            }
            set
            {
                this.receivingNoField = value;
            }
        }

        /// <remarks/>
        
        public ReceivingNoteItem[] receivingNoteItems
        {
            get
            {
                if (this.receivingNoteItemsField != null)
                {
                    return this.receivingNoteItemsField.ToArray();
                }
                else
                {
                    return null;
                }
            }
            set
            {
                receivingNoteItemsField = new List<ReceivingNoteItem>();
                foreach (ReceivingNoteItem item in receivingNoteItems) 
                {
                    receivingNoteItemsField.Add(item);
                }
            }
        }

        public void addReceivingNoteItem(ReceivingNoteItem item)
        {
            if (receivingNoteItemsField == null)
            {
                receivingNoteItemsField = new List<ReceivingNoteItem>();
            }
            receivingNoteItemsField.Add(item);
        }

        /// <remarks/>
        
        public string transactionType
        {
            get
            {
                return this.transactionTypeField;
            }
            set
            {
                this.transactionTypeField = value;
            }
        }
    }
}
