using System;

namespace com.Sconit.Entity.EDI
{
    [Serializable]
    public partial class TEMP_FORD_EDI_856Base : EntityBase
    {
        #region O/R Mapping Properties

        public Int32 Id { get; set; }
        public Int32 BatchNo { get; set; }
        public string Message_Type_Code { get; set; }
        public string Message_Type { get; set; }
        public string ReleaseVersion { get; set; }
        public string Sender_ID { get; set; }
        public string Receiver_ID { get; set; }
        public string Interchange_Control_Num { get; set; }
        public string ASN_Creation_DateTime { get; set; }
        public string Ship_To_GSDB_Code { get; set; }
        public string Ship_From_GSDB_Code { get; set; }
        public string Intermediate_Consignee_Code { get; set; }
        public string Message_Purpose_Code { get; set; }
        public string Shipment_ID { get; set; }
        public string Shipped_DateTime { get; set; }
        public string Gross_Weight { get; set; }
        public string Net_Weight { get; set; }
        public string UOM { get; set; }
        public string Packaging_Type_Code { get; set; }
        public string Lading_Qty { get; set; }
        public string Carrier_SCAC_Code { get; set; }
        public string Transportation_Method_Code { get; set; }
        public string Equipment_Desc_Code { get; set; }
        public string Equipment_Num { get; set; }
        public string LadingNum { get; set; }
        public string Part_Num { get; set; }
        public string Purchase_Order_Num { get; set; }
        public string Shipped_Qty { get; set; }
        public string Cum_Shipped_Qty { get; set; }
        public string Cum_Shipped_UOM { get; set; }
        public string Number_of_Loads { get; set; }
        public string Qty_Per_Load { get; set; }
        public string Packaging_Code { get; set; }
        public string Airport_Code { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserName { get; set; }
        public bool IsHandle { get; set; }
        public string ReadFileName { get; set; }
        public string AsnNo { get; set; }
      


        
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
            TEMP_FORD_EDI_856Base another = obj as TEMP_FORD_EDI_856Base;

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
