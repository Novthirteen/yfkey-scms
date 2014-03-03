using System;
using System.Collections.Generic;

namespace com.Sconit.Entity.Transportation
{
    [Serializable]
    public class TransportationBill : TransportationBillBase
    {
        #region Non O/R Mapping Properties

        public void AddTransportationBillDetail(TransportationBillDetail transportationBillDetail)
        {
            if (this.TransportationBillDetails == null)
            {
                this.TransportationBillDetails = new List<TransportationBillDetail>();
            }
            this.TransportationBillDetails.Add(transportationBillDetail);
        }

        public decimal TotalBillDetailAmount
        {
            get
            {
                decimal billDetailAmount = 0;
                if (this.TransportationBillDetails != null)
                {
                    foreach (TransportationBillDetail transportationBillDetail in this.TransportationBillDetails)
                    {
                        billDetailAmount += transportationBillDetail.Amount;
                    }
                }
                return billDetailAmount;
            }
        }

        public decimal TotalBillAmount
        {
            get
            {
                return this.TotalBillDetailAmount - (this.Discount.HasValue ? this.Discount.Value : 0);
            }
        }

        public decimal TotalBillDiscountRate
        {
            get
            {
                if (TotalBillDetailAmount == 0)
                {
                    return 0;
                }
                return (this.Discount.HasValue ? this.Discount.Value : 0) / TotalBillDetailAmount * 100;
            }
        }
        #endregion
    }
}