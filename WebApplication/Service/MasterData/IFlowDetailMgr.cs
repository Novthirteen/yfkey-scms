using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IFlowDetailMgr : IFlowDetailBaseMgr
    {
        #region Customized Methods

        FlowDetail GetFlowDetailByItem(string flowCode, string itemCode, string locationFromCode, string locationToCode);

        FlowDetail GetFlowDetailBySeq(string flowCode, int seq);

        IList<FlowDetail> GetFlowDetail(string flowCode);

        IList<FlowDetail> GetFlowDetail(Flow flow);

        IList<FlowDetail> GetFlowDetail(string flowCode, bool includeRefDetail);

        IList<FlowDetail> GetFlowDetail(Flow flow, bool includeRefDetail);

        IList<Item> GetAllFlowDetailItem(string flowCode);

        FlowDetail LoadFlowDetail(string flowCode, string itemCode, int seq);

        #endregion Customized Methods
    }
}
