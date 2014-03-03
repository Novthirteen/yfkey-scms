using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public abstract class WoReceiptViewBase : EntityBase
    {
        #region O/R Mapping Properties

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
        private string _item;
        public string Item
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
        private string _itemDesc;
        public string ItemDesc
        {
            get
            {
                return _itemDesc;
            }
            set
            {
                _itemDesc = value;
            }
        }
        private DateTime _createDate;
        public DateTime CreateDate
        {
            get
            {
                return _createDate;
            }
            set
            {
                _createDate = value;
            }
        }
        private string _partyFrom;
        public string PartyFrom
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
        private string _partyTo;
        public string PartyTo
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
        private Decimal? _recQty;
        public Decimal? RecQty
        {
            get
            {
                return _recQty;
            }
            set
            {
                _recQty = value;
            }
        }
        private string _orderNo;
        public string OrderNo
        {
            get
            {
                return _orderNo;
            }
            set
            {
                _orderNo = value;
            }
        }
        private string _huId;
        public string HuId
        {
            get
            {
                return _huId;
            }
            set
            {
                _huId = value;
            }
        }
        private string _lotNo;
        public string LotNo
        {
            get
            {
                return _lotNo;
            }
            set
            {
                _lotNo = value;
            }
        }
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
            WoReceiptViewBase another = obj as WoReceiptViewBase;

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
