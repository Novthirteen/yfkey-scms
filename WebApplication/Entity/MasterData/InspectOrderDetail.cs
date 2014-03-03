using System;

//TODO: Add other using statements here

namespace com.Sconit.Entity.MasterData
{
    [Serializable]
    public class InspectOrderDetail : InspectOrderDetailBase
    {
        #region Non O/R Mapping Properties

        public decimal CurrentQualifiedQty { get; set; }

        public StorageBin CurrentStorageBin { get; set; }

        public decimal CurrentRejectedQty { get; set; }

        public decimal InspectedQty
        {
            get
            {
                decimal qualifiedQty = this.QualifiedQty.HasValue ? (decimal)this.QualifiedQty : 0;
                decimal rejectedQty = this.RejectedQty.HasValue ? (decimal)this.RejectedQty : 0;
                return this.InspectQty - qualifiedQty - rejectedQty;
            }
        }

        public bool? IsQualified { get; set; }

        public bool HasMatched
        {
            get
            {
                return (this.QualifiedQty.HasValue || this.RejectedQty.HasValue);
            }
        }

        public string LocationFromCode { get; set; }
        public string ItemCode { get; set; }

        #endregion
    }
}