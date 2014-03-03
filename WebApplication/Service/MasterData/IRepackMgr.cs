using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IRepackMgr : IRepackBaseMgr
    {
        #region Customized Methods

        Repack LoadRepack(String repackNo, bool includeDetail);

        Repack CreateRepack(IList<RepackDetail> repackDetailList, User user);

        Repack CreateDevanning(IList<RepackDetail> repackDetailList, User user);

        #endregion Customized Methods
    }
}
