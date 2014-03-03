using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class KPOrder : KPOrderBase
    {
        #region Non O/R Mapping Properties

        public decimal Amount
        {
            get { 
            return TotalAmount-base.CLN_amount;
            }
        }

        public decimal TotalAmount
        {
            get
            {
                decimal totalAmount = 0;
                if (this.KPItems != null)
                {
                    foreach (KPItem kpItem in KPItems)
                    {
                        totalAmount += kpItem.PRICE2.HasValue ? kpItem.PRICE2.Value : 0;
                    }
                }
                return totalAmount;
            }
        }

        #endregion
    }
}