using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IWorkCenterMgr : IWorkCenterBaseMgr
    {
        #region Customized Methods

        IList<WorkCenter> GetWorkCenter(string regionCode);

        IList<WorkCenter> GetWorkCenter(Region region);
        
        IList<WorkCenter> GetWorkCenter(string regionCode, bool includeInactive);

        IList<WorkCenter> GetWorkCenter(Region region, bool includeInactive);

        void DeleteWorkCenterByParent(String parentCode);

        #endregion Customized Methods
    }
}
