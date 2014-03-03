using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IOrderPlannedBackflushMgr : IOrderPlannedBackflushBaseMgr
    {
        #region Customized Methods

        IList<object[]> GetActiveOrderPlannedBackflush(string prodLineCode);

        #endregion Customized Methods
    }
}
