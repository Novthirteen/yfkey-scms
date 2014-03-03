using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IOrderHeadMgr : IOrderHeadBaseMgr
    {
        #region Customized Methods

        OrderHead CheckAndLoadOrderHead(string orderNo);

        OrderHead CheckAndLoadOrderHead(string orderNo, bool includeDetail);

        OrderHead LoadOrderHead(String orderNo, bool includeDetail);

        OrderHead LoadOrderHead(String orderNo, bool includeDetail, bool includeOperation);

        OrderHead LoadOrderHead(String orderNo, bool includeDetail, bool includeOperation,  bool includeLocTrans);

        IList<OrderHead> GetOrderHead(int count,string status);

        IList<OrderHead> GetOrderHead(int count);

        IList<OrderHead> GetOrderHead(string status);

        void GenerateOrderHeadSubsidiary(OrderHead orderHead);

        IList<OrderHead> GetOrderHead(DateTime lastModifyDate, int firstRow, int maxRows, string status);

        #endregion Customized Methods
    }
}
