using com.Sconit.Service.Ext.MasterData;

//TODO: Add other using statements here.

using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
namespace com.Sconit.Service.MasterData
{
    public interface IRoutingMgr : IRoutingBaseMgr
    {
        #region Customized Methods

        IList<Routing> GetRouting(string type);

        Routing CheckAndLoadRouting(string routingCode);

        #endregion Customized Methods
    }
}





#region Extend Interface





namespace com.Sconit.Service.Ext.MasterData
{
    public partial interface IRoutingMgrE : com.Sconit.Service.MasterData.IRoutingMgr
    {
        
    }
}

#endregion
