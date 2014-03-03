using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IReceiptDetailMgr : IReceiptDetailBaseMgr
    {
        #region Customized Methods

        IList<ReceiptDetail> CreateReceiptDetail(Receipt receipt, OrderLocationTransaction inOrderLocationTransaction, IList<Hu> huList);

        IList<ReceiptDetail> SummarizeReceiptDetails(IList<ReceiptDetail> receiptDetailList);

        #endregion Customized Methods
    }
}
