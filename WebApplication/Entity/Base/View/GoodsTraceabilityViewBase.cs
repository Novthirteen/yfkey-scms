using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public   class GoodsTraceabilityViewBase : EntityBase
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


        private string  _item;
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

        private string _desc;
        public string Desc
        {
            get{return _desc;}
            set{_desc=value;}
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

        private string _location;
        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }


        private string _createUser;
        public string CreateUser
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
            GoodsTraceabilityViewBase another = obj as GoodsTraceabilityViewBase;

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
