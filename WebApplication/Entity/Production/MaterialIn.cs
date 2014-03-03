using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Entity.Production
{
    [Serializable]
    public class MaterialIn : EntityBase
    {
        private Item _rawMaterial;
        public Item RawMaterial
        {
            get
            {
                return this._rawMaterial;
            }
            set
            {
                this._rawMaterial = value;
            }
        }

        private Int32? _operation;
        public Int32? Operation
        {
            get
            {
                return _operation;
            }
            set
            {
                _operation = value;
            }
        }

        private decimal _qty;
        public decimal Qty
        {
            get
            {
                return this._qty;
            }
            set
            {
                this._qty = value;
            }
        }

        private string _huId;
        public string HuId
        {
            get
            {
                return this._huId;
            }
            set
            {
                this._huId = value;
            }
        }

        private Location _location;
        public Location Location
        {
            get
            {
                return this._location;
            }
            set
            {
                this._location = value;
            }
        }

        private bool _isBlank;
        public bool IsBlank
        {
            get
            {
                return this._isBlank;
            }
            set
            {
                this._isBlank = value;
            }
        }

        private string _lotNo;
        public string LotNo
        {
            get
            {
                return this._lotNo;
            }
            set
            {
                this._lotNo = value;
            }
        }
    }
}
