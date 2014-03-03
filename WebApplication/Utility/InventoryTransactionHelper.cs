using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using com.Sconit.Entity.MasterData;

namespace com.Sconit.Utility
{
    public static class InventoryTransactionHelper
    {
        public static InventoryTransaction CreateInventoryTransaction(LocationLotDetail locationLotDetail, decimal qty, bool isBillSettled)
        {
            InventoryTransaction inventoryTransaction = new InventoryTransaction();
            inventoryTransaction.LocationLotDetailId = locationLotDetail.Id;
            inventoryTransaction.Location = locationLotDetail.Location;
            inventoryTransaction.StorageBin = locationLotDetail.StorageBin;
            inventoryTransaction.Item = locationLotDetail.Item;
            inventoryTransaction.Hu = locationLotDetail.Hu;
            inventoryTransaction.LotNo = locationLotDetail.LotNo;
            inventoryTransaction.RefLocation = locationLotDetail.RefLocation;
            if (!isBillSettled && locationLotDetail.IsConsignment)
            {
                inventoryTransaction.IsConsignment = locationLotDetail.IsConsignment;
                inventoryTransaction.PlannedBill = locationLotDetail.PlannedBill;
            }
            else
            {
                inventoryTransaction.IsConsignment = false;
            }
            inventoryTransaction.Qty = qty;

            return inventoryTransaction;
        }
    }
}
