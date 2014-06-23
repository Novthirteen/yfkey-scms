using System;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MRP
{
    public interface IFlatBomMgr : IFlatBomBaseMgr
    {
        #region Customized Methods

        void GenFlatBom(string userCode);

        #endregion Customized Methods
    }
}


