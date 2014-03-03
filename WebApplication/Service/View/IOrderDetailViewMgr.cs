using System;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View
{
    public interface IOrderDetailViewMgr : IOrderDetailViewBaseMgr
    {
        #region Customized Methods

        IList<OrderDetailView> GetProdIO(string flow, string region, string startDate, string endDate, string item, string userCode, int pageSize, int pageIndex);

        int GetProdIOCount(string flow, string region, string startDate, string endDate, string item, string userCode);

        #endregion Customized Methods
    }
}
