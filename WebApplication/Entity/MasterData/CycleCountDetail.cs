using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class CycleCountDetail : CycleCountDetailBase
    {
        #region Non O/R Mapping Properties

        //新加空行
        private Boolean _isBlankDetail = false;
        public Boolean IsBlankDetail
        {
            get
            {
                return _isBlankDetail;
            }
            set
            {
                _isBlankDetail = value;
            }
        }

        //包装数
        private int _ucQty;
        public int UCQty
        {
            get
            {
                if (Qty != null && Item != null && Item.UnitCount > 0)
                    return (int)(Qty / Item.UnitCount);
                else
                    return _ucQty;
            }
            set
            {
                _ucQty = value;
            }
        }
        //零头数
        private decimal _oddQty;
        public decimal OddQty
        {
            get
            {
                if (Qty != null && Item != null)
                {
                    if (Item.UnitCount > 0)
                        return (decimal)(Qty % Item.UnitCount);
                    else if (Item.UnitCount == 0)
                        return (decimal)Qty;
                    else
                        return _oddQty;
                }
                else
                    return _oddQty;
            }
            set
            {
                _oddQty = value;
            }
        }

        #endregion
    }
}