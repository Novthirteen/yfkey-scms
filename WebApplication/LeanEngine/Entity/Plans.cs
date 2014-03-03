using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeanEngine.Utility;

namespace LeanEngine.Entity
{
    public class Plans : EntityBase
    {
        #region Input data
        public int ID { get; set; }

        public string Loc { get; set; }
        public string Item { get; set; }
        public DateTime ReqTime { get; set; }

        public string OrderNo { get; set; }
        public string FlowCode { get; set; }

        public Enumerators.IRType IRType { get; set; }
        public Enumerators.PlanType PlanType { get; set; }
        public Enumerators.FlowType FlowType { get; set; }
        public Enumerators.TimeUnit TimeUnit { get; set; }

        public decimal OrderedQty { get; set; }
        public decimal FinishedQty { get; set; }
        public decimal AllocatedQty { get; set; }
        #endregion

        #region Output data
        /// <summary>
        /// Remain qty,calculate value, don't set it
        /// </summary>
        public decimal Qty
        {
            get
            {
                decimal compareQty = Math.Max(this.FinishedQty, this.AllocatedQty);
                return OrderedQty > compareQty ? (OrderedQty - compareQty) : 0;
            }
        }
        #endregion


        public override int GetHashCode()
        {
            if (ID != 0)
            {
                return ID.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            Plans another = obj as Plans;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.ID == another.ID);
            }
        }
    }
}
