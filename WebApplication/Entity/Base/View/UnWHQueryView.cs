using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public class UnWHQueryView : EntityBase
    {
        #region O/R Mapping Properties

        private String _location;
        public String Location
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
        private string _itemcode;
        public string ItemCode
        {
            get
            {
                return _itemcode;
            }
            set
            {
                _itemcode = value;
            }
        }
        private string _desc1;
        public string Desc1
        {
            get
            {
                return _desc1;
            }
            set
            {
                _desc1 = value;
            }
        }
        private string _huid;
        public string HuId
        {
            get
            {
                return _huid;
            }
            set
            {
                _huid = value;
            }
        }
        private string _lotno;
        public string LotNo
        {
            get { return _lotno; }
            set { _lotno = value; }
        }
        private DateTime _createdate;
        public DateTime CreateDate
        {
            get
            {
                return _createdate;
            }
            set
            {
                _createdate = value;
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
        //private Int32 _id;
        //public Int32 Id
        //{
        //    get
        //    {
        //        return _id;
        //    }
        //    set
        //    {
        //        _id = value;
        //    }
        //}

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
            UnWHQueryView another = obj as UnWHQueryView;

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
