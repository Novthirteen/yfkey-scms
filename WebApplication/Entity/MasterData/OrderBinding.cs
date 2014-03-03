using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class OrderBinding : OrderBindingBase
    {
        #region Non O/R Mapping Properties

        //¸¨Öú×Ö¶Î£¬ÓÃÓÚÌí¼Ó
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

        #endregion
    }
}