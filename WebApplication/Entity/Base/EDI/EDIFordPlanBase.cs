using System;

namespace com.Sconit.Entity.EDI
{
    [Serializable]
    public partial class EDIFordPlanBase : EntityBase
    {
        #region O/R Mapping Properties

        public Int32 Id { get; set; }

        public Int32 TempId { get; set; }
        public Int32 BatchNo { get; set; }
        public string Control_Num { get; set; }
        public string SupplierCode { get; set; }
        public string CustomerCode { get; set; }
        public DateTime ReleaseIssueDate { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string RefItem { get; set; }
        public string Uom { get; set; }
        public decimal? LastShippedQuantity { get; set; }
        public decimal? LastShippedCumulative { get; set; }
        public DateTime? LastShippedDate { get; set; }
        public string DockCode { get; set; }
        public string LineFeed { get; set; }
        public string StorageLocation { get; set; }
        public string IntermediateConsignee { get; set; }
        public decimal ForecastQty { get; set; }
        public decimal ForecastCumQty { get; set; }
        public DateTime ForecastDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserName { get; set; }
        public string Type { get; set; }
        public string PurchaseOrder { get; set; }
        public decimal CurrenCumQty { get; set; }
        
        #endregion

		public override int GetHashCode()
        {
			if (Id != 0)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            EDIFordPlanBase another = obj as EDIFordPlanBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id);
            }
        } 
    }
	
}
