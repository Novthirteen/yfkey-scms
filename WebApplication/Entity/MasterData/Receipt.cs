using System;
using System.Collections.Generic;
using com.Sconit.Entity.Distribution;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class Receipt : ReceiptBase
    {
        #region Non O/R Mapping Properties

        public void AddReceiptDetail(ReceiptDetail receiptDetail)
        {
            if (this.ReceiptDetails == null)
            {
                this.ReceiptDetails = new List<ReceiptDetail>();
            }

            this.ReceiptDetails.Add(receiptDetail);
        }

        public void AddInProcessLocation(InProcessLocation inProcessLocation)
        {
            if (this.InProcessLocations == null)
            {
                this.InProcessLocations = new List<InProcessLocation>();
            }

            this.InProcessLocations.Add(inProcessLocation);
        }
        #endregion
    }
}