using System;

//TODO: Add other using statements here

namespace com.Sconit.Persistence.MasterData
{
    public interface IWorkCenterDao : IWorkCenterBaseDao
    {
        #region Customized Methods

        void DeleteWorkCenterByParent(String parentCode);

        #endregion Customized Methods
    }
}