using System;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Service.MasterData
{
    public interface ILocationTransactionMgr : ILocationTransactionBaseMgr
    {
        #region Customized Methods

        void RecordLocationTransaction(OrderLocationTransaction orderLocationTransaction, InventoryTransaction inventoryTransaction, Receipt receipt, User user);

        void RecordLocationTransaction(OrderLocationTransaction orderLocationTransaction, InventoryTransaction inventoryTransaction, InProcessLocation inProcessLocation, User user);

        void RecordLocationTransaction(MiscOrderDetail miscOrderDetail, InventoryTransaction inventoryTransaction, User user);

        void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user);

        void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo);

        void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo, Location locationTo);

        void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo, Location refLocation, Flow productLine);

        void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo, Location refLocation, Flow productLine, string receiptNo);

        void RecordLocationTransaction(InventoryTransaction inventoryTransaction, string transType, User user, string orderNo, Location refLocation, Flow productLine, string receiptNo, int? orderDetailId);
       
        void RecordLocationTransaction(ProductLineInProcessLocationDetail productLineInProcessLocationDetail, string transType, User user, string ioType);

        void RecordWOBackflushLocationTransaction(OrderLocationTransaction orderLocationTransaction, string huId, string lotNo, decimal qty, string ipNo, User user, Location locationFrom);

        IList<LocationTransaction> GetLocationTransactionAfterEffDate(string itemCode, string loc, DateTime effDate);

        IList<LocationTransaction> GetPeriodLocationTransaction(string itemCode, string loc, string transType, DateTime startDate, DateTime endDate);

        IList<LocationTransaction> GetLocationTransaction(IList<string> itemList, IList<string> locList, DateTime? startDate);

        IList<LocationTransaction> GetProjectionLocationTransaction(IList<string> itemList, IList<string> locList, DateTime? endDate);

        #endregion Customized Methods
    }
}
