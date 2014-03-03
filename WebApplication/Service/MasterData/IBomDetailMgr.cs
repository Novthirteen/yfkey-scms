using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IBomDetailMgr : IBomDetailBaseMgr
    {
        #region Customized Methods

        IList<BomDetail> GetNextLevelBomDetail(string bomCode, DateTime efftiveDate);

        IList<BomDetail> GetFlatBomDetail(string bomCode, DateTime efftiveDate);

        bool CheckUniqueExist(string parCode, string compCode, int Operation, string Reference, DateTime startTime);

        IList<BomDetail> GetLastLevelBomDetail(string itemCode, DateTime effDate);

        IList<BomDetail> GetBomView_Nml(Item item, DateTime effDate);

        IList<BomDetail> GetBomView_Cost(string bomCode, DateTime effDate);

        IList<BomDetail> GetTopLevelBomDetail(string itemCode, DateTime effDate);

        IList<BomDetail> GetTreeBomDetail(string bomCode, DateTime effDate);

        BomDetail GetDefaultBomDetailForAbstractItem(Item abstractItem, Routing routing, DateTime effDate);

        BomDetail GetDefaultBomDetailForAbstractItem(string abstractItemCode, Routing routing, DateTime effDate);

        BomDetail GetDefaultBomDetailForAbstractItem(Item abstractItem, Routing routing, DateTime effDate, Location defaultLocationFrom);

        IList<BomDetail> GetBomDetailListForAbstractItem(Item abstractItem, Routing routing, DateTime effDate, Location defaultLocationFrom);

        IList<BomDetail> GetBomDetailListForAbstractItem(string abstractItemCode, Routing routing, DateTime effDate, Location defaultLocationFrom);

        BomDetail GetDefaultBomDetailForAbstractItem(string abstractItemCode, Routing routing, DateTime effDate, Location defaultLocationFrom);

        BomDetail GetDefaultBomDetailForAbstractItem(Item abstractItem, string routingCode, DateTime effDate);

        BomDetail GetDefaultBomDetailForAbstractItem(string abstractItemCode, string routingCode, DateTime effDate);

        BomDetail GetDefaultBomDetailForAbstractItem(Item abstractItem, string routingCode, DateTime effDate, Location defaultLocationFrom);

        BomDetail GetDefaultBomDetailForAbstractItem(string abstractItemCode, string routingCode, DateTime effDate, Location defaultLocationFrom);

        BomDetail LoadBomDetail(string bomCode, string itemCode, string reference, DateTime date);

        BomDetail LoadBomDetail(string bomCode, string itemCode, string reference);

        BomDetail GetBomDetail(string bomCode, string itemCode);

        #endregion Customized Methods
    }
}
