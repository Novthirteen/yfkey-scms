using System.Collections.Generic;
using com.Sconit.Entity.MasterData;
using com.Sconit.Entity.Procurement;
using com.Sconit.Entity.Distribution;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IReceiptMgr : IReceiptBaseMgr
    {
        #region Customized Methods

        void CreateReceipt(Receipt receipt, User user);

        void CreateReceipt(Receipt receipt, User user, bool isOddCreateHu);

        Receipt LoadReceipt(string receiptNo, User user);

        Receipt LoadReceipt(string receiptNo, bool includeDetail);

        Receipt LoadReceipt(string receiptNo, User user, bool includeDetail);

        Receipt LoadReceipt(string receiptNo, bool includeDetail, bool includeInProcessLocations);

        Receipt LoadReceipt(string receiptNo, User user, bool includeDetail, bool includeInProcessLocations);

        Receipt CheckAndLoadReceipt(string receiptNo);

        IList<Receipt> GetReceipts(string userCode, int firstRow, int maxRows, params string[] orderTypes);

        #endregion Customized Methods
    }
}
