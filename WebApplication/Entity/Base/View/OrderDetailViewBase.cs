using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class OrderDetailViewBase : EntityBase
    {
        #region O/R Mapping Properties

        private Int32 _id;
        public Int32 Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        private string _flow;
        public string Flow
        {
            get
            {
                return _flow;
            }
            set
            {
                _flow = value;
            }
        }
        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        private string _type;
        public string Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;
            }
        }
        private com.Sconit.Entity.MasterData.Party _partyFrom;
        public com.Sconit.Entity.MasterData.Party PartyFrom
        {
            get
            {
                return _partyFrom;
            }
            set
            {
                _partyFrom = value;
            }
        }
        private com.Sconit.Entity.MasterData.Party _partyTo;
        public com.Sconit.Entity.MasterData.Party PartyTo
        {
            get
            {
                return _partyTo;
            }
            set
            {
                _partyTo = value;
            }
        }
        private DateTime _effDate;
        public DateTime EffDate
        {
            get
            {
                return _effDate;
            }
            set
            {
                _effDate = value;
            }
        }
        private com.Sconit.Entity.MasterData.Shift _shift;
        public com.Sconit.Entity.MasterData.Shift Shift
        {
            get
            {
                return _shift;
            }
            set
            {
                _shift = value;
            }
        }
        private com.Sconit.Entity.MasterData.Item _item;
        public com.Sconit.Entity.MasterData.Item Item
        {
            get
            {
                return _item;
            }
            set
            {
                _item = value;
            }
        }
        private string _uom;
        public string Uom
        {
            get
            {
                return _uom;
            }
            set
            {
                _uom = value;
            }
        }
        private Decimal _requiredQty;
        public Decimal RequiredQty
        {
            get
            {
                return _requiredQty;
            }
            set
            {
                _requiredQty = value;
            }
        }
        private Decimal _orderedQty;
        public Decimal OrderedQty
        {
            get
            {
                return _orderedQty;
            }
            set
            {
                _orderedQty = value;
            }
        }
        private Decimal _shippedQty;
        public Decimal ShippedQty
        {
            get
            {
                return _shippedQty;
            }
            set
            {
                _shippedQty = value;
            }
        }
        private Decimal _receivedQty;
        public Decimal ReceivedQty
        {
            get
            {
                return _receivedQty;
            }
            set
            {
                _receivedQty = value;
            }
        }
        private Decimal _rejectedQty;
        public Decimal RejectedQty
        {
            get
            {
                return _rejectedQty;
            }
            set
            {
                _rejectedQty = value;
            }
        }
        private Decimal _scrapQty;
        public Decimal ScrapQty
        {
            get
            {
                return _scrapQty;
            }
            set
            {
                _scrapQty = value;
            }
        }

        public string Status { get; private set; }

        #endregion

        public override int GetHashCode()
        {
            if (Id != null)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            OrderDetailViewBase another = obj as OrderDetailViewBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Id == another.Id);
            }
        }
    }

}
