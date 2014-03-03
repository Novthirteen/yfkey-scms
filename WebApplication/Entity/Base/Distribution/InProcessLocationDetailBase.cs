using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Distribution
{
    [Serializable]
    public abstract class InProcessLocationDetailBase : EntityBase
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
        private com.Sconit.Entity.Distribution.InProcessLocation _inProcessLocation;
        public com.Sconit.Entity.Distribution.InProcessLocation InProcessLocation
        {
            get
            {
                return _inProcessLocation;
            }
            set
            {
                _inProcessLocation = value;
            }
        }
        private com.Sconit.Entity.MasterData.OrderLocationTransaction _orderLocationTransaction;
        public com.Sconit.Entity.MasterData.OrderLocationTransaction OrderLocationTransaction
        {
            get
            {
                return _orderLocationTransaction;
            }
            set
            {
                _orderLocationTransaction = value;
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
        private Decimal _qty;
        public Decimal Qty
        {
            get
            {
                return _qty;
            }
            set
            {
                _qty = value;
            }
        }
        private Decimal? _receivedQty;
        public Decimal? ReceivedQty
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
        private Boolean _isConsignment;
        public Boolean IsConsignment
        {
            get
            {
                return _isConsignment;
            }
            set
            {
                _isConsignment = value;
            }
        }
        private Int32? _plannedBill;
        public Int32? PlannedBill
        {
            get
            {
                return _plannedBill;
            }
            set
            {
                _plannedBill = value;
            }
        }
        private com.Sconit.Entity.MasterData.Location _locationFrom;
        public com.Sconit.Entity.MasterData.Location LocationFrom
        {
            get
            {
                return _locationFrom;
            }
            set
            {
                _locationFrom = value;
            }
        }
        private com.Sconit.Entity.MasterData.Location _locationTo;
        public com.Sconit.Entity.MasterData.Location LocationTo
        {
            get
            {
                return _locationTo;
            }
            set
            {
                _locationTo = value;
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
        #endregion

        public override int GetHashCode()
        {
            if (Id != 0)
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
            InProcessLocationDetailBase another = obj as InProcessLocationDetailBase;

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
