using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.Dss
{
    [Serializable]
    public abstract class DssImportHistoryBase : EntityBase
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
        private com.Sconit.Entity.Dss.DssInboundControl _dssInboundCtrl;
        public com.Sconit.Entity.Dss.DssInboundControl DssInboundCtrl
        {
            get
            {
                return _dssInboundCtrl;
            }
            set
            {
                _dssInboundCtrl = value;
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
        private string _item;
        public string ItemCode
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
        private Int32 _errorCount;
        public Int32 ErrorCount
        {
            get
            {
                return _errorCount;
            }
            set
            {
                _errorCount = value;
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
        private string _memo;
        public string Memo
        {
            get
            {
                return _memo;
            }
            set
            {
                _memo = value;
            }
        }
        private string _data0;
        public string data0
        {
            get
            {
                return _data0;
            }
            set
            {
                _data0 = value;
            }
        }
        private string _data1;
        public string data1
        {
            get
            {
                return _data1;
            }
            set
            {
                _data1 = value;
            }
        }
        private string _data2;
        public string data2
        {
            get
            {
                return _data2;
            }
            set
            {
                _data2 = value;
            }
        }
        private string _data3;
        public string data3
        {
            get
            {
                return _data3;
            }
            set
            {
                _data3 = value;
            }
        }
        private string _data4;
        public string data4
        {
            get
            {
                return _data4;
            }
            set
            {
                _data4 = value;
            }
        }
        private string _data5;
        public string data5
        {
            get
            {
                return _data5;
            }
            set
            {
                _data5 = value;
            }
        }
        private string _data6;
        public string data6
        {
            get
            {
                return _data6;
            }
            set
            {
                _data6 = value;
            }
        }
        private string _data7;
        public string data7
        {
            get
            {
                return _data7;
            }
            set
            {
                _data7 = value;
            }
        }
        private string _data8;
        public string data8
        {
            get
            {
                return _data8;
            }
            set
            {
                _data8 = value;
            }
        }
        private string _data9;
        public string data9
        {
            get
            {
                return _data9;
            }
            set
            {
                _data9 = value;
            }
        }
        private string _data10;
        public string data10
        {
            get
            {
                return _data10;
            }
            set
            {
                _data10 = value;
            }
        }
        private string _data11;
        public string data11
        {
            get
            {
                return _data11;
            }
            set
            {
                _data11 = value;
            }
        }
        private string _data12;
        public string data12
        {
            get
            {
                return _data12;
            }
            set
            {
                _data12 = value;
            }
        }
        private string _data13;
        public string data13
        {
            get
            {
                return _data13;
            }
            set
            {
                _data13 = value;
            }
        }
        private string _data14;
        public string data14
        {
            get
            {
                return _data14;
            }
            set
            {
                _data14 = value;
            }
        }
        private string _data15;
        public string data15
        {
            get
            {
                return _data15;
            }
            set
            {
                _data15 = value;
            }
        }
        private string _data16;
        public string data16
        {
            get
            {
                return _data16;
            }
            set
            {
                _data16 = value;
            }
        }
        private string _data17;
        public string data17
        {
            get
            {
                return _data17;
            }
            set
            {
                _data17 = value;
            }
        }
        private string _data18;
        public string data18
        {
            get
            {
                return _data18;
            }
            set
            {
                _data18 = value;
            }
        }
        private string _data19;
        public string data19
        {
            get
            {
                return _data19;
            }
            set
            {
                _data19 = value;
            }
        }
        private string _data20;
        public string data20
        {
            get
            {
                return _data20;
            }
            set
            {
                _data20 = value;
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
            DssImportHistoryBase another = obj as DssImportHistoryBase;

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
