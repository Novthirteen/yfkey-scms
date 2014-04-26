using System;

namespace com.Sconit.Entity.EDI
{
    [Serializable]
    public partial class TEMP_FORD_EDI_830Base : EntityBase
    {
        #region O/R Mapping Properties

        public Int32 Id { get; set; }


        public Int32 BatchNo { get; set; }
        public string Message_Type_Code { get; set; }
        public string Message_Type { get; set; }
        public string Sender_ID_Title { get; set; }
        public string Sender_ID { get; set; }
        public string Receiver_ID_Title { get; set; }
        public string Receiver_ID { get; set; }
        public string Interchange_Control_Num { get; set; }
        public string Message_Release_Num { get; set; }
        public string Message_Release_Date { get; set; }
        public string Message_Purpose { get; set; }
        public string Schedule_Type { get; set; }
        public string Horizon_Start_Date { get; set; }
        public string Horizon_End_Date { get; set; }
        public string Comment_Note { get; set; }
        public string Ship_To_GSDB_Code { get; set; }
        public string Ship_From_GSDB_Code { get; set; }
        public string Intermediate_Consignee { get; set; }
        public string Part_Num { get; set; }
        public string Purchase_Order_Num { get; set; }
        public string Part_Release_Status { get; set; }
        public string Dock_Code { get; set; }
        public string Line_Feed { get; set; }
        public string Reserve_Line_Feed { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_Telephone { get; set; }
        public string Fab_Auth_Qty { get; set; }
        public string Fab_Auth_Start_Date { get; set; }
        public string Fab_Auth_End_Date { get; set; }
        public string Mat_Auth_Qty { get; set; }
        public string Mat_Auth_Start_Date { get; set; }
        public string Mat_Auth_End_Date { get; set; }
        public string Last_Received_ASN_Num { get; set; }
        public string Last_Shipped_Qty { get; set; }
        public string Last_Shipped_Date { get; set; }
        public string Cum_Shipped_Qty { get; set; }
        public string Cum_Start_Date { get; set; }
        public string Cum_End_Date { get; set; }
        public string Forecast_Cum_Qty { get; set; }
        public string Forecast_Net_Qty { get; set; }
        public string UOM { get; set; }
        public string Forecast_Status { get; set; }
        public string Forecast_Date { get; set; }
        public string Flexible_Forcast_Start_Date { get; set; }
        public string Flexible_Forcast_End_Date { get; set; }
        public string Forecast_Date_Qual_r { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserName { get; set; }
        public bool IsHandle { get; set; }
        public string ReadFileName { get; set; }
      


        
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
            TEMP_FORD_EDI_830Base another = obj as TEMP_FORD_EDI_830Base;

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
