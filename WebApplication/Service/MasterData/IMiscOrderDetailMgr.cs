using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IMiscOrderDetailMgr : IMiscOrderDetailBaseMgr
    {
        #region Customized Methods

        IList<MiscOrderDetail> GetMiscOrderDetail(string orderNo);

        #endregion Customized Methods
    }
}
