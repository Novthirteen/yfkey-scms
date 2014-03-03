using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class BillDetail : BillDetailBase
    {
        #region Non O/R Mapping Properties

        public decimal DiscountRate
        {
            get
            {
                return ((this.Discount.HasValue ? this.Discount.Value : 0) / (this.BilledQty * this.UnitPrice)) * 100;
            }
        }

        public decimal Amount
        {
            get
            {
                return (this.BilledQty * this.UnitPrice - (this.Discount.HasValue ? this.Discount.Value : 0));
            }
        }

        #endregion
    }
}