using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IRegionMgr : IRegionBaseMgr
    {
        #region Customized Methods

        //IList FindAllRegion();

        IList<Region> GetRegion(string userCode);

        IList<Region> GetRegion(string userCode,bool includeInactive);

        void CreateRegion(Region entity, User currentUser);

        #endregion Customized Methods
    }
}
