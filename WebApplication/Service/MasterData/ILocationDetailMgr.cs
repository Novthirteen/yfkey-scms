using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.View;
using System.Collections;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface ILocationDetailMgr : ILocationDetailBaseMgr
    {
        #region Customized Methods

        LocationDetail GetLocationDetail(string location, string item);

        decimal GetCurrentInv(string location, string item);

        //IList<LocationDetail> FindLocationDetail(IList<string> locationCode, IList<string> itemCode, DateTime effectiveDate, User user);

        IList<LocationDetail> FindLocationDetail(IList<string> locationCode, IList<string> itemCode, DateTime? effectiveDate, string userCode);

        IList<LocationDetail> FindLocationDetail(IList<string> locationCode, IList<string> itemCode, DateTime? effectiveDate, string userCode, bool includeActiveOrder);

        IList<LocationDetail> FindLocationDetail(IList<string> locationCode, IList<string> itemCode, DateTime? effectiveDate, string userCode, bool includeActiveOrder, int pageSize, int pageIndex);

        int FindLocationDetailCount(IList<string> locationCode, IList<string> itemCode, DateTime? effectiveDate, string userCode, bool includeActiveOrder);

        LocationDetail FindLocationDetail(string loc, string itemCode, DateTime? effectiveDate);

        LocationDetail FindLocationDetail(string loc, string itemCode, DateTime? effectiveDate, bool includeActiveOrder);

        LocationDetail FindLocationDetail(Location location, Item item, DateTime? effectiveDate);

        LocationDetail FindLocationDetail(Location location, Item item, DateTime? effectiveDate, bool includeActiveOrder);

        IList<LocationDetail> GetInvIOB(IList<string> locationCode, IList<string> itemCode, DateTime startDate, DateTime endDate, string userCode);

        void PostProcessInvIOB(IList list, DateTime? startEffDate, DateTime? endEffDate);

        IList<LocationDetail> GetInvVisualBoard(string locCode, string itemCode, string flow, DateTime? date, User user);

        decimal GetDateInventory(string item, string loc, DateTime date);

        void PostProcessInvDetail(IList list);

        void PostProcessInvHistory(IList list, DateTime? date);

        void PostProcessInvVisualBoard(IList list, DateTime? date);

        #endregion Customized Methods
    }
}
