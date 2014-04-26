using System;

namespace com.Sconit.Entity.EDI
{
    [Serializable]
    public partial class EDIDailyPlanBase : EntityBase
    {
        #region O/R Mapping Properties

        public Int32 Id { get; set; }

        public Int32 TempId { get; set; }
        public Int32 BatchNo { get; set; }
        public string Control_Num { get; set; }
        public string SupplierCode { get; set; }
        public string CustomerCode { get; set; }
        public string ReleaseIssueDate { get; set; }
        public string Item { get; set; }
        public string ItemDesc { get; set; }
        public string RefItem { get; set; }
        public string Uom { get; set; }
        public string LastShippedQuantity { get; set; }
        public string LastShippedCumulative { get; set; }
        public string LastShippedDate { get; set; }
        public string DockCode { get; set; }
        public string LineFeed { get; set; }
        public string StorageLocation { get; set; }
        public string IntermediateConsignee { get; set; }
        public string ForecastQty { get; set; }
        public string ForecastCumQty { get; set; }
        public string ForecastDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserName { get; set; }
        
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
            EDIDailyPlanBase another = obj as EDIDailyPlanBase;

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
