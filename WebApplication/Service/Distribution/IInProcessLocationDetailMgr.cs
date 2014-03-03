using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;

//TODO: Add other using statements here.

namespace com.Sconit.Service.Distribution
{
    public interface IInProcessLocationDetailMgr : IInProcessLocationDetailBaseMgr
    {
        #region Customized Methods

        void CreateInProcessLocationDetail(InProcessLocation inProcessLocation, OrderLocationTransaction outOrderLocationTransaction, IList<InventoryTransaction> inventoryTransactionList);

        void CreateInProcessLocationDetail(InProcessLocation inProcessLocation, OrderLocationTransaction outOrderLocationTransaction, IList<Hu> huList);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(string ipNo);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(InProcessLocation inProcessLocation);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(OrderLocationTransaction orderLocationTransaction);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(OrderLocationTransaction orderLocationTransaction, string asnType);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(OrderDetail orderDetail);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(OrderDetail orderDetail, bool includeGap);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(OrderHead orderHead);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(OrderHead orderHead, string status);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(string ipNo, string huId);

        IList<InProcessLocationDetail> SummarizeInProcessLocationDetails(IList<InProcessLocationDetail> inProcessLocationDetailList);

        InProcessLocationDetail GetNoneHuAndIsConsignmentInProcessLocationDetail(InProcessLocation inProcessLocation, OrderLocationTransaction orderLocationTransaction);

        IList<InProcessLocationDetail> GetInProcessLocationDetailOut(IList<string> itemList, IList<string> locList);

        IList<InProcessLocationDetail> GetInProcessLocationDetailIn(IList<string> itemList, IList<string> locList);

        IList<InProcessLocationDetail> GetInProcessLocationDetail(string ipNo, int outOrderLocationTransactionId);

        #endregion Customized Methods
    }
}
