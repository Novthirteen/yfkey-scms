using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public abstract class HuBase : EntityBase
    {
        #region O/R Mapping Properties

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
        private string _qualityLevel;
        public string QualityLevel
        {
            get
            {
                return _qualityLevel;
            }
            set
            {
                _qualityLevel = value;
            }
        }
        private com.Sconit.Entity.MasterData.Uom _uom;
        public com.Sconit.Entity.MasterData.Uom Uom
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
        private Decimal _unitQty;
        public Decimal UnitQty
        {
            get
            {
                return _unitQty;
            }
            set
            {
                _unitQty = value;
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
        private string _receiptNo;
        public string ReceiptNo
        {
            get
            {
                return _receiptNo;
            }
            set
            {
                _receiptNo = value;
            }
        }
        private DateTime _manufactureDate;
        public DateTime ManufactureDate
        {
            get
            {
                return _manufactureDate;
            }
            set
            {
                _manufactureDate = value;
            }
        }
        private com.Sconit.Entity.MasterData.Party _manufactureParty;
        public com.Sconit.Entity.MasterData.Party ManufactureParty
        {
            get
            {
                return _manufactureParty;
            }
            set
            {
                _manufactureParty = value;
            }
        }
        private string _remark;
        public string Remark
        {
            get
            {
                return _remark;
            }
            set
            {
                _remark = value;
            }
        }
        private string _parentHuId;
        public string ParentHuId
        {
            get
            {
                return _parentHuId;
            }
            set
            {
                _parentHuId = value;
            }
        }
        private Int32 _printCount;
        public Int32 PrintCount
        {
            get
            {
                return _printCount;
            }
            set
            {
                _printCount = value;
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
        private com.Sconit.Entity.MasterData.User _createUser;
        public com.Sconit.Entity.MasterData.User CreateUser
        {
            get
            {
                return _createUser;
            }
            set
            {
                _createUser = value;
            }
        }
        private Decimal _unitCount;
        public Decimal UnitCount
        {
            get
            {
                return _unitCount;
            }
            set
            {
                _unitCount = value;
            }
        }
        private DateTime? _expiredDate;
        public DateTime? ExpiredDate
        {
            get
            {
                return _expiredDate;
            }
            set
            {
                _expiredDate = value;
            }
        }
        public String Version { get; set; }
        public Decimal LotSize { get; set; }
        public String AntiResolveHu { get; set; }
        public String Location { get; set; }
        public String Status { get; set; }
        public String CustomerItemCode { get; set; }
        private String _storageBin;
        public String StorageBin
        {
            get
            {
                return _storageBin;
            }
            set
            {
                _storageBin = value;
            }
        }
        private Boolean _transferFlag;
        public Boolean TransferFlag
        {
            get
            {
                return _transferFlag;
            }
            set
            {
                _transferFlag = value;
            }
        }
        private Boolean _isMes;
        public Boolean IsMes
        {
            get
            {
                return _isMes;
            }
            set
            {
                _isMes = value;
            }
        }
        private Boolean _isInspect;

        public Boolean IsInspect
        {
            get
            {
                return _isInspect;
            }
            set
            {
                _isInspect = value;
            }
        }

        #endregion

        public override int GetHashCode()
        {
            if (HuId != null)
            {
                return HuId.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            HuBase another = obj as HuBase;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.HuId == another.HuId);
            }
        }
    }

}
