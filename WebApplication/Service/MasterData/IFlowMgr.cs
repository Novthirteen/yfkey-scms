using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.View;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IFlowMgr : IFlowBaseMgr
    {
        #region Customized Methods

        Flow LoadFlow(string code, bool includeFlowDetail);

        Flow LoadFlow(string flowCode, string userCode);

        Flow LoadFlow(string code, bool includeFlowDetail, bool includeRefDetail);

        Flow LoadFlow(string flowCode, string userCode, bool includeFlowDetail);

        Flow LoadFlow(string flowCode, string userCode, bool includeFlowDetail, string moduleType);

        Flow CheckAndLoadFlow(string code);

        Flow CheckAndLoadFlow(string code, bool includeFlowDetail);

        Flow CheckAndLoadFlow(string code, bool includeFlowDetail, bool includeRefDetail);

        IList<Flow> GetProcurementFlow(string userCode);

        IList<Flow> GetProcurementFlow(string userCode, bool includeInactive);

        IList<Flow> GetProcurementFlow(string userCode, string partyAuthrizeOpt);

        IList<Flow> GetDistributionFlow(string userCode);

        IList<Flow> GetDistributionFlow(string userCode, bool includeInactive);

        IList<Flow> GetProductionFlow(string userCode);

        IList<Flow> GetProductionFlow(string userCode, bool includeInactive);

        IList<Flow> GetTransferFlow(string userCode, string partyAuthrizeOpt);

        IList<Flow> GetTransferFlow(string userCode);

        IList<Flow> GetTransferFlow(string userCode, bool includeInactive);

        IList<Flow> GetCustomerGoodsFlow(string userCode);

        IList<Flow> GetCustomerGoodsFlow(string userCode, bool includeInactive);

        IList<Flow> GetSubconctractingFlow(string userCode);

        IList<Flow> GetSubconctractingFlow(string userCode, bool includeInactive);

        IList<string> FindWinTime(Flow flow, DateTime date);

        IList<string> FindWinTime(string flowCode, DateTime date);

        IList<Flow> GetAllFlow(string userCode);

        IList<Flow> GetFlowList(string userCode, bool includeProcurement, bool includeDistribution, bool includeTransfer, bool includeProduction, bool includeCustomerGoods, bool includeSubconctracting, string orderAuthrizeOpt);

        IList<Flow> GetMRPFlow(string userCode);

        IList<Flow> GetAllFlow(DateTime lastModifyDate, int firstRow, int maxRows);

        IList<BomDetail> GetBatchFeedBomDetail(Flow flow);

        IList<BomDetail> GetBatchFeedBomDetail(string flowCode);

        //Flow CheckAndLoadTransferFlow(Location locationFrom, Location locationTo, Hu hu, string userCode);

        FlowView CheckAndLoadFlowView(string flowCode, string userCode, string locationFromCode, string locationToCode, Hu hu, List<string> flowTypes);

        //FlowView CheckAndLoadFlowView(string flowCode, string userCode, Location locationFrom, Location locationTo, Hu hu, params string[] flowTypes);

        void UpdateFlow(Flow flow, bool isTrack);

        IList<Flow> GetProductLinesNotInUser(string userCode);

        IList<Flow> GetProductLinesInUser(string userCode);

        Flow LoadFlowByDesc(string flowDesc);

        #endregion Customized Methods
    }
}
