using System;
using com.Sconit.Entity.MasterData;
using System.Collections.Generic;

//TODO: Add other using statements here.

namespace com.Sconit.Service.MasterData
{
    public interface IHuOddMgr : IHuOddBaseMgr
    {
        #region Customized Methods

        HuOdd CreateHuOdd(ReceiptDetail receiptDetail, LocationLotDetail locationLotDetail, User user);

        IList<HuOdd> GetHuOdd(Item item, decimal unitCount, Uom uom, Location locFrom, Location locTo, string status);

        #endregion Customized Methods
    }
}
