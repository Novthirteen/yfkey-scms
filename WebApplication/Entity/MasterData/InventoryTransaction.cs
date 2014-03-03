using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class InventoryTransaction : EntityBase
    {
        public int LocationLotDetailId { get; set; }

        public Location Location { get; set; }

        public StorageBin StorageBin { get; set; }

        public Item Item { get; set; }

        public Hu Hu { get; set; }

        public decimal Qty { get; set; }

        public bool IsConsignment { get; set; }

        public int? PlannedBill { get; set; }

        public string LotNo { get; set; }

        public string RefLocation { get; set; }
    }
}
