using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Procurement
{
    public interface ISupplyChainMgr
    {
        #region Customized Methods

        IList<SupplyChain> GenerateSupplyChain(string flowCode, string itemCode);

        SupplyChain GenerateSupplyChain(FlowDetail flowDetail);

        SupplyChain GenerateSupplyChain(Flow flow, FlowDetail flowDetail);

        #endregion Customized Methods
    }
}
