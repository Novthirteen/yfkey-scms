using System;
using com.Sconit.Entity.View;
using NHibernate.Expression;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.View
{
    public interface IOrderLocTransViewMgr : IOrderLocTransViewBaseMgr
    {
        #region Customized Methods

        IList<OrderLocTransView> GetProdIODataList(string flow, string region, string startDate, string endDate, string item, string userCode, string ioType);

        #endregion Customized Methods
    }
}
