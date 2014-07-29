﻿using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MRP
{
    [Serializable]
    public class ShipPlanOpenOrder : ShipPlanOpenOrderBase
    {
        #region Non O/R Mapping Properties

        public string TransferOrderFormat { get { return IsTransferOrder.HasValue ? (IsTransferOrder.Value ? "☑" : "☒") : "☒"; } }

        #endregion
    }
}