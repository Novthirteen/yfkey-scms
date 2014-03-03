using System;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.Sconit.Entity.View
{
    [Serializable]
    public class OrderDetailView : OrderDetailViewBase
    {
        #region Non O/R Mapping Properties

        //计划完成率
        public Decimal CompleteRate
        {
            get
            {
                if (this.OrderedQty != 0)
                {
                    return this.ReceivedQty / this.OrderedQty;
                }
                else
                {
                    return 0;
                }
            }
        }

        //合格率
        public Decimal PassRate
        {
            get
            {
                decimal totalQty = this.ReceivedQty + this.RejectedQty + this.ScrapQty;
                if (totalQty != 0)
                {
                    return this.ReceivedQty / totalQty;
                }
                else
                {
                    return 0;
                }
            }
        }

        public IList<OrderLocTransView> OutList { get; set; }

        public IList<OrderLocTransView> InList { get; set; }

        #endregion
    }
}