using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public abstract class DssExportHistoryBase : EntityBase
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
        private com.Sconit.Entity.Dss.DssOutboundControl _dssOutboundControl;
        public com.Sconit.Entity.Dss.DssOutboundControl DssOutboundControl
        {
            get
            {
                return _dssOutboundControl;
            }
            set
            {
                _dssOutboundControl = value;
            }
        }
        private string _eventCode;
        public string EventCode
        {
            get
            {
                return _eventCode;
            }
            set
            {
                _eventCode = value;
            }
        }
        private Boolean _isActive;
        public Boolean IsActive
        {
            get
            {
                return _isActive;
            }
            set
            {
                _isActive = value;
            }
        }
        private string _keyCode;
        public string KeyCode
        {
            get
            {
                return _keyCode;
            }
            set
            {
                _keyCode = value;
            }
        }
        private string _referenceKeyCode;
        public string ReferenceKeyCode
        {
            get
            {
                return _referenceKeyCode;
            }
            set
            {
                _referenceKeyCode = value;
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
        private string _location;
        public string Location
        {
            get
            {
                return _location;
            }
            set
            {
                _location = value;
            }
        }
        private string _referenceLocation;
        public string ReferenceLocation
        {
            get
            {
                return _referenceLocation;
            }
            set
            {
                _referenceLocation = value;
            }
        }
        private DateTime? _effectiveDate;
        public DateTime? EffectiveDate
        {
            get
            {
                return _effectiveDate;
            }
            set
            {
                _effectiveDate = value;
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
        private string _comments;
        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }
        private string _definedString1;
        public string DefinedString1
        {
            get
            {
                return _definedString1;
            }
            set
            {
                _definedString1 = value;
            }
        }
        private string _definedString2;
        public string DefinedString2
        {
            get
            {
                return _definedString2;
            }
            set
            {
                _definedString2 = value;
            }
        }
        private string _definedString3;
        public string DefinedString3
        {
            get
            {
                return _definedString3;
            }
            set
            {
                _definedString3 = value;
            }
        }
        private string _definedString4;
        public string DefinedString4
        {
            get
            {
                return _definedString4;
            }
            set
            {
                _definedString4 = value;
            }
        }
        private string _definedString5;
        public string DefinedString5
        {
            get
            {
                return _definedString5;
            }
            set
            {
                _definedString5 = value;
            }
        }
        private string _undefinedString1;
        public string UndefinedString1
        {
            get
            {
                return _undefinedString1;
            }
            set
            {
                _undefinedString1 = value;
            }
        }
        private string _undefinedString2;
        public string UndefinedString2
        {
            get
            {
                return _undefinedString2;
            }
            set
            {
                _undefinedString2 = value;
            }
        }
        private string _undefinedString3;
        public string UndefinedString3
        {
            get
            {
                return _undefinedString3;
            }
            set
            {
                _undefinedString3 = value;
            }
        }
        private string _undefinedString4;
        public string UndefinedString4
        {
            get
            {
                return _undefinedString4;
            }
            set
            {
                _undefinedString4 = value;
            }
        }
        private string _undefinedString5;
        public string UndefinedString5
        {
            get
            {
                return _undefinedString5;
            }
            set
            {
                _undefinedString5 = value;
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
        public string TransNo { get; set; }
        public string OrderNo { get; set; }

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
            DssExportHistoryBase another = obj as DssExportHistoryBase;

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
