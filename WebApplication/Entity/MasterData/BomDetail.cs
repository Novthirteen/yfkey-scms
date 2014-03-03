using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class BomDetail : BomDetailBase
    {
        #region Non O/R Mapping Properties

        //ѡװ�������ǣ�Ĭ��ȡ������BomCode
        private string _optionalItemGroup;
        public string OptionalItemGroup
        {
            set
            {
                this._optionalItemGroup = value;
            }
            get
            {
                return this._optionalItemGroup;
            }
        }

        private decimal _calculatedQty = 1;
        public decimal CalculatedQty
        {
            set
            {
                this._calculatedQty = value;
            }
            get
            {
                return this._calculatedQty;
            }
        }

        //Ĭ��flow�����õĿ�λ
        private Location _defaultLocation;
        public Location DefaultLocation
        {
            set
            {
                this._defaultLocation = value;
            }
            get
            {
                return this._defaultLocation;
            }
        }
        #endregion
    }
}