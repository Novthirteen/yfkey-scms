using com.Sconit.Service.Ext.MasterData;

//TODO: Add other using statements here.

using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using System;
namespace com.Sconit.Service.MasterData
{
    public interface IRoutingDetailMgr : IRoutingDetailBaseMgr
    {
        #region Customized Methods

        IList<RoutingDetail> GetRoutingDetail(string routingCode, DateTime effectiveDate);

        IList<RoutingDetail> GetRoutingDetail(Routing routing, DateTime effectiveDate);

        #endregion Customized Methods
    }
}





#region Extend Interface





namespace com.Sconit.Service.Ext.MasterData
{
    public partial interface IRoutingDetailMgrE : com.Sconit.Service.MasterData.IRoutingDetailMgr
    {
        
    }
}

#endregion
