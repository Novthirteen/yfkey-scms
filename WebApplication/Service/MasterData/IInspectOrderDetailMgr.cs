using System;
using System.Collections.Generic;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IInspectOrderDetailMgr : IInspectOrderDetailBaseMgr
    {
        #region Customized Methods

        IList<InspectOrderDetail> GetInspectOrderDetail(string inspectOrderNo);

        IList<InspectOrderDetail> GetInspectOrderDetail(InspectOrder inspectOrder);

        IList<InspectOrderDetail> ConvertTransformerToInspectDetail(IList<Transformer> transformerList);

        IList<InspectOrderDetail> ConvertTransformerToInspectDetail(IList<Transformer> transformerList, bool includeZero);

        InspectOrderDetail CheckAndGetInspectOrderDetail(string huId);

        #endregion Customized Methods
    }
}
