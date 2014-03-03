using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public class WoReceiptView : WoReceiptViewBase
    {
        #region Non O/R Mapping Properties

        private int _boxCount;
        public int BoxCount
        {
            get
            {
                return _boxCount;
            }
            set
            {
                _boxCount = value;
            }
        }
        #endregion
    }
}